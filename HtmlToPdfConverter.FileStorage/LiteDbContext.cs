using LiteDB;

namespace HtmlToPdfConverter.FileStorage
{
    public class LiteDbContext
    {
        private readonly LiteDatabase _database;

        public LiteDbContext(string connectionString)
        {
            _database = new LiteDatabase(connectionString);
        }

        public ILiteStorage<string> FileStorage => _database.FileStorage;
    }
}
