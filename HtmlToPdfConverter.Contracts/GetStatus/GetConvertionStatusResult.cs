using HtmlToPdfConverter.CrossCutting.Enums;

namespace HtmlToPdfConverter.Contracts.GetStatus
{
    public class GetConvertionStatusResult
    {
        public string? PdfFileStorageId { get; set; }
        public FileProcessStatus Status {get;set;}
    }
}
