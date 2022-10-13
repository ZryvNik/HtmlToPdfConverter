using HtmlToPdfConverter.Contracts.Download;
using HtmlToPdfConverter.Infrustructure.FileStorage;
using MediatR;

namespace HtmlToPdfConverter.Infrustructure.Handlers
{
    public class DownloadFileHandler : IRequestHandler<DownloadFileRequest, DownloadFileResult>
    {
        private readonly IFileStorageService _fileStorageService;

        public DownloadFileHandler(IFileStorageService fileStorageService)
        {
            _fileStorageService = fileStorageService;
        }
        public async Task<DownloadFileResult> Handle(DownloadFileRequest request, CancellationToken cancellationToken)
        {
            var result = _fileStorageService.Get(request.FileStorageId);
            result.Position = 0;

            return await Task.FromResult(new DownloadFileResult()
            {
                FileStream = result,
                DownloadName = request.FileStorageId
            });
        }
    }
}
