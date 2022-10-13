using MediatR;

namespace HtmlToPdfConverter.Contracts.Download
{
    public class DownloadFileRequest : IRequest<DownloadFileResult>
    {
        public string FileStorageId { get; set; }
    }
}
