using HtmlToPdfConverter.CrossCutting.Configuration;
using HtmlToPdfConverter.CrossCutting.DateTimeProvider;
using HtmlToPdfConverter.Infrustructure.DataAccess;
using HtmlToPdfConverter.Infrustructure.FileStorage;
using Microsoft.Extensions.Hosting;

namespace HtmlToPdfConverter.Infrustructure.DatabaseCleaning
{
    public class CleanerHostedService : BackgroundService
    {
        private readonly IFileInfoRepository _repository;
        private readonly IFileStorageService _storage;
        private readonly IFileLifeTimeProvider _fileLifeTimeProvider;
        private readonly IDateTimeProvider _dateTimeProvider;

        public CleanerHostedService(IFileInfoRepository repository,
            IFileStorageService storage,
            IFileLifeTimeProvider fileLifeTimeProvider,
            IDateTimeProvider dateTimeProvider)
        {
            _repository = repository;
            _storage = storage;
            _fileLifeTimeProvider = fileLifeTimeProvider;
            _dateTimeProvider = dateTimeProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var dateFrom = _dateTimeProvider.Now.Add(-1 * TimeSpan.FromMinutes(_fileLifeTimeProvider.Minutes));
                var fileInfos = _repository.GetFileInfosOlderThen(dateFrom);

                foreach (var fileInfo in fileInfos!)
                {
                    _storage.Delete(fileInfo.HtmlFileStorageId);
                    if (!string.IsNullOrEmpty(fileInfo.PdfFileStorageId))
                        _storage.Delete(fileInfo.PdfFileStorageId);
                    _repository.Delete(fileInfo.Id);
                }

                await Task.Delay(50000, stoppingToken);
            }
        }
    }
}
