using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoWithDotNetAPI.DAta;
using NetflixApi.Model;

namespace NetflixApi.Services
{
    public class HistoryServices
    {

        private readonly IMongoCollection<History> _historyCollection;
        public HistoryServices(IOptions<UserDataContext> userDataContext)
        {
            var mongoClient = new MongoClient(userDataContext.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(userDataContext.Value.DatabaseName);
            _historyCollection = mongoDatabase.GetCollection<History>(userDataContext.Value.HistoryCollectionName);

        }

        public async Task<ActionResult<IEnumerable<History>>> GetUserHistory(string userId)
        {
            try
            {
                var filter = Builders<History>.Filter.Eq(doc => doc.UserId, userId);
                var history = await _historyCollection.Find(filter).ToListAsync();

                if (history.Count == 0)
                {
                    return new NotFoundResult();
                }

                return new OkObjectResult(history);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine(ex);

                return new StatusCodeResult(500);
            }
        }

        public async Task<ActionResult<History>> AddHistory(History history)
        {
            try
            {


                await _historyCollection.InsertOneAsync(history);

                return new CreatedAtActionResult(
                    actionName: nameof(GetUserHistory),
                    controllerName: "History",
                    routeValues: new { userId = history.UserId },
                    value: history);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine(ex);

                return new StatusCodeResult(500);
            }
        }
    }
}

