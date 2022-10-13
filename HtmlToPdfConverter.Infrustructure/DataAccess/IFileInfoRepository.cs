using HtmlToPdfConverter.Infrustructure.DataAccess.Models;

namespace HtmlToPdfConverter.Infrustructure.DataAccess
{
    public interface IFileInfoRepository
    {
        FileInfo? GetNextFileInfoForConvertion();
        GetFileProcessStatusByCorrelationDto GetFileProcessStatusByCorrelation(Guid correlationId);
        int Insert(string htmlFileStrorageId, Guid correlationId, DateTime uploadDate);
        void Update(FileInfo fileInfo);
    }
}