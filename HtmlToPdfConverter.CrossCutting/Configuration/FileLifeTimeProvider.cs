namespace HtmlToPdfConverter.CrossCutting.Configuration
{
    public class FileLifeTimeProvider : IFileLifeTimeProvider
    {
        public int Minutes { get; set; }
    }
}
