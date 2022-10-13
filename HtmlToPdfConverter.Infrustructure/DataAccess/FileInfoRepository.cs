using HtmlToPdfConverter.CrossCutting.Enums;
using HtmlToPdfConverter.Infrustructure.ApplicationId;
using HtmlToPdfConverter.Infrustructure.DataAccess.Exceptions;
using HtmlToPdfConverter.Infrustructure.DataAccess.Models;
using LiteDB;

namespace HtmlToPdfConverter.Infrustructure.DataAccess
{
    public class FileInfoRepository : IFileInfoRepository
    {
        private readonly ILiteDatabase _database;
        private readonly IApplicationIdProvider _applicationIdProvider;

        public FileInfoRepository(ILiteDatabase database,
            IApplicationIdProvider applicationIdProvider)
        {
            _database = database;
            _applicationIdProvider = applicationIdProvider;
        }

        public FileInfo? GetNextFileInfoForConvertion()
        {
            return _database.GetCollection<FileInfo>()
                //Находим либо свежедобавленный файл
                //Либо файл который взят в обработку, но идентификатор приложения изменился, а значит была перезагрузка приложения
                .Find(x => x.Status == FileProcessStatus.Added 
                  || (x.Status == FileProcessStatus.InProgress && x.ProceedByApplicationId != _applicationIdProvider.ApplicationId))
                .FirstOrDefault();
        }

        public GetFileProcessStatusByCorrelationDto GetFileProcessStatusByCorrelation(Guid correlationId)
        {
            var result = _database.GetCollection<FileInfo>()
                .FindOne(x => x.CorrelationId == correlationId);

            if (result == null)
                throw new EntityNotFoundException($"Entity with correlationId = {correlationId} is not found");

            return new GetFileProcessStatusByCorrelationDto()
            {
                Status = result.Status,
                PdfDownloadId = result.PdfFileStorageId
            };
        }

        public int Insert(string htmlFileStrorageId, Guid correlationId, DateTime uploadDate)
        {
            var id = _database
                .GetCollection<FileInfo>()
                .Insert(new FileInfo()
                {
                    HtmlFileStorageId = htmlFileStrorageId,
                    UploadDate = uploadDate,
                    CorrelationId = correlationId,
                    ProceedByApplicationId = _applicationIdProvider.ApplicationId
                });
            return id;
        }

        public void Update(FileInfo fileInfo)
        {
            _database.GetCollection<FileInfo>().Update(fileInfo);
        }
    }
}
