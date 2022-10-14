using HtmlToPdfConverter.CrossCutting.Enums;

namespace HtmlToPdfConverter.Infrustructure.DataAccess
{
    public class FileInfo
    {
        public int Id { get; set; }
        public Guid CorrelationId { get; set; }
        public string HtmlFileStorageId { get; set; }
        public string? PdfFileStorageId { get; set; }
        public FileProcessStatus Status { get; set; } = FileProcessStatus.Added;
        public Guid ProceedByApplicationId { get; set; }
        public DateTime? UploadDate { get; set; }
    }
}
