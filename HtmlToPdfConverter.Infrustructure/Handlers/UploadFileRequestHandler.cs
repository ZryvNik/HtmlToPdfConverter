using HtmlToPdfConverter.Contracts.Upload;
using HtmlToPdfConverter.Infrustructure.DataAccess;
using HtmlToPdfConverter.Infrustructure.FileStorage;
using MediatR;

namespace HtmlToPdfConverter.Infrustructure.Handlers
{
    public class UploadFileRequestHandler : IRequestHandler<UploadFileRequest, UploadFileResult>
    {
        private readonly IFileInfoRepository _fileInfoRepository;
        private readonly IFileStorageService _fileStorageService;

        public UploadFileRequestHandler(IFileInfoRepository fileInfoRepository,
            IFileStorageService fileStorageService)
        {
            _fileInfoRepository = fileInfoRepository;
            _fileStorageService = fileStorageService;
        }

        public async Task<UploadFileResult> Handle(UploadFileRequest request, CancellationToken cancellationToken)
        {
            //Сохраняем html-файл в хранилище
            var htmlfileId = _fileStorageService.Upload(request.FileStream);
            //Создаём запись о том, что есть файл для конвертации 
            var id = _fileInfoRepository.Insert(htmlfileId, request.CorrelationId, DateTime.Now);

            return await Task.FromResult(new UploadFileResult()
            {
                Id = id
            });
        }
    }
}
