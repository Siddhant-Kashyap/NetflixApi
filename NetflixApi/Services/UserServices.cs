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
            _userCollection = mongoDatabase.GetCollection<LocalUsers>(userDataContext.Value.DatabaseName);

        }
        //user registration this is to register individual user 
        public async Task<LocalUsers> SignupAsync(RegistrationRequest newUser)
        {     //mapping the requried model with local users
            LocalUsers user = new LocalUsers()
            {
                UserName = newUser.UserName,
                Password = newUser.Password,
               
            };
            await _userCollection.InsertOneAsync(user);
           
            return user;
        }

        // User Login Find a user by username and password 
        public async Task<LocalUsers?> LoginAsync(string username, string password)

        {
            
            var user = await _userCollection.Find(x => x.UserName == username && x.Password == password).FirstOrDefaultAsync();

            return user;
        }
        public async Task<LocalUsers> GetAsync(string username)
        {
            // Define a filter to search for the user by username
            var filter = Builders<LocalUsers>.Filter.Eq(x => x.UserName, username);

            try
            {
                // Use the MongoDB collection to find the user matching the filter
                var user = await _userCollection.Find(filter).FirstOrDefaultAsync();

                return user; // Return the user if found, or null if not found
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
      
    }
}
