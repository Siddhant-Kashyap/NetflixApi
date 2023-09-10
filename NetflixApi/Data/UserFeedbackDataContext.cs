namespace NetflixApi.Data
{
    public class UserFeedbackDataContext
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string UserFeedbackCollectionName { get; set; } = null!;
    }
}
