using HtmlToPdfConverter.Contracts.Upload;
using HtmlToPdfConverter.CrossCutting.DateTimeProvider;
using HtmlToPdfConverter.CrossCutting.GuidProvider;
using HtmlToPdfConverter.Infrustructure.DataAccess;
using HtmlToPdfConverter.Infrustructure.FileStorage;
using MediatR;

namespace HtmlToPdfConverter.Infrustructure.Handlers
{
    public class UploadFileRequestHandler : IRequestHandler<UploadFileRequest, UploadFileResult>
    {
        private readonly IFileInfoRepository _fileInfoRepository;
        private readonly IFileStorageService _fileStorageService;
        private readonly IGuidProvider _guidProvider;
        private readonly IDateTimeProvider _dateTimeProvider;

        public UploadFileRequestHandler(IFileInfoRepository fileInfoRepository, 
            IFileStorageService fileStorageService, 
            IGuidProvider guidProvider, 
            IDateTimeProvider dateTimeProvider)
        {
            _fileInfoRepository = fileInfoRepository;
            _fileStorageService = fileStorageService;
            _guidProvider = guidProvider;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<UploadFileResult> Handle(UploadFileRequest request, CancellationToken cancellationToken)
        {
            //Сохраняем html-файл в хранилище
            var htmlfileId = _fileStorageService.Upload(request.FileStream);
            var correlationId = _guidProvider.NewGuid;
            //Создаём запись о том, что есть файл для конвертации 
            _fileInfoRepository.Insert(htmlfileId, correlationId, _dateTimeProvider.Now);

            return await Task.FromResult(new UploadFileResult()
            {
                CorrelationId = correlationId
            });
        }
    }
}
