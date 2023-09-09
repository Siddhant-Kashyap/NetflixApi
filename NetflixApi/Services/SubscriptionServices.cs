using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoWithDotNetAPI.DAta;
using NetflixApi.Model;

namespace NetflixApi.Services
{
    public class SubscriptionServices
    {
        public readonly IMongoCollection<Subscription> _subscriptionCollection;

        public SubscriptionServices(IOptions<UserDataContext> userDataContext)
        {
            var mongoClient = new MongoClient(userDataContext.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(userDataContext.Value.DatabaseName);
            _subscriptionCollection= mongoDatabase.GetCollection<Subscription>(userDataContext.Value.SubscriptionCollectionName);
        }


        public List<Subscription> GetSubscriptionsByUserId(string userId)
        {
            var subscriptions = _subscriptionCollection.Find(s => s.UserId == userId).ToList();
            return subscriptions;
        }

        public void CreateSubscription(Subscription subscription)
        {
            _subscriptionCollection.InsertOne(subscription);
        }
    }
}
