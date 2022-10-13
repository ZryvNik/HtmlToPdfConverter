namespace HtmlToPdfConverter.Infrustructure.FileStorage
{
    public interface IFileStorageService
    {
        string Upload(Stream fileStream);
        Stream Get(string id);
        bool Delete(string id);
    }
}
