using MediatR;

namespace HtmlToPdfConverter.Contracts.Download
{
    public class DownloadFileRequest : IRequest<DownloadFileResult>
    {
        public DownloadFileRequest(string fileStorageId)
        {
            FileStorageId = fileStorageId;
        }
        public string FileStorageId { get; set; }
    }
}
