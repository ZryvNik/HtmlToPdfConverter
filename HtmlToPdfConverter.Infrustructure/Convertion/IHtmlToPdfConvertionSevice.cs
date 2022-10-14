namespace HtmlToPdfConverter.Infrustructure.Convertion
{
    public interface IHtmlToPdfConvertionSevice
    {
        Task<Stream> ConvertAsync(string html, CancellationToken cancellationToken);
    }
}
