namespace HtmlToPdfConverter.CrossCutting.Configuration
{
    public interface IFileLifeTimeProvider
    {
        int Minutes { get; }
    }
}
