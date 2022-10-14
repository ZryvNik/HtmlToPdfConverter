using HtmlToPdfConverter.Infrustructure.DataAccess.Models;

namespace HtmlToPdfConverter.Infrustructure.DataAccess
{
    public interface IFileInfoRepository
    {
        FileInfo? GetNextFileInfoForConvertion();
        FileInfo? GetFileInfoByCorrelationId(Guid correlationId);
        GetFileProcessStatusByCorrelationDto GetFileProcessStatusByCorrelation(Guid correlationId);
        int Insert(string htmlFileStrorageId, Guid correlationId, DateTime uploadDate);
        void Update(FileInfo fileInfo);
    }
}