using LiteDB;

namespace HtmlToPdfConverter.Infrustructure.FileStorage
{
    public class LiteDbStorageService : IFileStorageService
    {
        private readonly ILiteDatabase _database;

        public LiteDbStorageService(ILiteDatabase database)
        {
            _database = database;
        }

        public string Upload(Stream fileStream)
        {
            var id = Guid.NewGuid().ToString();
            _database.FileStorage.Upload(id, id, fileStream);

            return id;
        }

        public Stream Get(string id)
        {
            var outputStream = new MemoryStream();
            _database.FileStorage.Download(id, outputStream);
            outputStream.Position = 0;

            return outputStream;
        }

        public bool Delete(string id)
        {
            return _database.FileStorage.Delete(id);
        }
    }
}
