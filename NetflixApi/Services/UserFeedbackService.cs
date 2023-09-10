using Microsoft.Extensions.Options;
using MongoDB.Driver;
using NetflixApi.Data;
using NetflixApi.Model;

namespace NetflixApi.Services
{
    public class UserFeedbackService
    {
        private readonly IMongoCollection<UserFeedback> _UserFeedbackCollection;

        public UserFeedbackService(IOptions<UserFeedbackDataContext> UserFeedbackDataContext)
        {
            var mongoClient = new MongoClient(UserFeedbackDataContext.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(UserFeedbackDataContext.Value.DatabaseName);
            _UserFeedbackCollection = mongoDatabase.GetCollection<UserFeedback>(UserFeedbackDataContext.Value.DatabaseName);

        }

        //  public List<UserFeedback> GetUserFeedback(string userId)
        //  {
        // Filter feedback by the user's ID (or another identifier like username).
        //     var userFeedback = _UserFeedbackCollection.Find(f => f.UserId == userId).ToList();
        //    return userFeedback;
        // }


        // public void CreateFeedback(UserFeedback feedback)
        // {

        //     _UserFeedbackCollection.InsertOne(feedback);
        // }


        // public async Task<List<UserFeedback>> GetAsync() =>
        //    await _UserFeedbackCollection.Find(_=>true).ToListAsync();



        public async Task<List<UserFeedback>> GetAsync(string UserId) =>
          await _UserFeedbackCollection.Find(x=>x.UserId==UserId).ToListAsync();

        public async Task CreateAsync(UserFeedback newFeedback) =>
        await _UserFeedbackCollection.InsertOneAsync(newFeedback);

        public async Task UpdateAsync(string Userid, UserFeedback updatedUserFeedback) =>
            await _UserFeedbackCollection.ReplaceOneAsync(x => x.UserId == Userid, updatedUserFeedback);

    }
}
