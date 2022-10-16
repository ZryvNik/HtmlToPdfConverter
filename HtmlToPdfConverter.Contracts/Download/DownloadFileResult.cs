using System.IO;

namespace HtmlToPdfConverter.Contracts.Download
{
    public class DownloadFileResult
    {
        public Stream FileStream { get; set; }
        public string? DownloadName { get; set; }
    }
}
