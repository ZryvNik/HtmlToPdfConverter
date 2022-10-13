using HtmlToPdfConverter.CrossCutting.Enums;

namespace HtmlToPdfConverter.Infrustructure.DataAccess.Models
{
    public class GetFileProcessStatusByCorrelationDto
    {
        public FileProcessStatus Status { get; set; }
        public string? PdfDownloadId { get; set; }
    }
}
