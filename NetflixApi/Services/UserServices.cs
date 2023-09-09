using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoWithDotNetAPI.DAta;
using NetflixApi.Model;

namespace NetflixApi.Services
{
    public class UserServices
    {

        private readonly IMongoCollection<LocalUsers> _userCollection;
        public UserServices(IOptions<UserDataContext> userDataContext)
        {
            var mongoClient = new MongoClient(userDataContext.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(userDataContext.Value.DatabaseName);
            _userCollection = mongoDatabase.GetCollection<LocalUsers>(userDataContext.Value.UserCollectionName);

        }
        public async Task<LocalUsers> SignupAsync(LocalUsers newUser)
        {
            await _userCollection.InsertOneAsync(newUser);
            return newUser;
        }

        // User Login: Find a user by username and password (you should hash passwords)
        public async Task<LocalUsers?> LoginAsync(string username, string password)
        {
            var user = await _userCollection.Find(x => x.UserName == username && x.Password == password).FirstOrDefaultAsync();
            return user;
        }
    }
}
