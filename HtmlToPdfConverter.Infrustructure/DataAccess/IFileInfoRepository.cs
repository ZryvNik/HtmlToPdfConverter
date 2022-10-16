using HtmlToPdfConverter.Infrustructure.DataAccess.Models;

namespace HtmlToPdfConverter.Infrustructure.DataAccess
{
    public interface IFileInfoRepository
    {
        FileInfo? GetNextFileInfoForConvertion();
        IEnumerable<FileInfo>? GetFileInfosOlderThen(DateTime date);
        FileInfo? GetFileInfoByCorrelationId(Guid correlationId);
        GetFileProcessStatusByCorrelationDto GetFileProcessStatusByCorrelation(Guid correlationId);
        int Insert(string htmlFileStrorageId, Guid correlationId, DateTime uploadDate);
        void Update(FileInfo fileInfo);
        void Delete(int id);
    }
}