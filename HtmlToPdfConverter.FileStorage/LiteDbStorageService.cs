using System;
using System.IO;

namespace HtmlToPdfConverter.FileStorage
{
    public class LiteDbStorageService: IFileStorageService
    {
        private readonly LiteDbContext _context;

        public LiteDbStorageService(LiteDbContext context)
        {
            _context = context;
        }

        public string Upload(Stream fileStream)
        {
            var id = Guid.NewGuid().ToString();
            _context.FileStorage.Upload(id, id, fileStream);

            return id;
        }

        public Stream Get(string id)
        {
            var outputStream = new MemoryStream();
            _context.FileStorage.Download(id, outputStream);
            outputStream.Position = 0;

            return outputStream;
        }

        public bool Delete(string id)
        {
            return _context.FileStorage.Delete(id);
        }
    }
}
