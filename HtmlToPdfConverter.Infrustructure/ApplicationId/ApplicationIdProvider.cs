using HtmlToPdfConverter.CrossCutting.GuidProvider;

namespace HtmlToPdfConverter.Infrustructure.ApplicationId
{
    public class ApplicationIdProvider : IApplicationIdProvider
    {
        public ApplicationIdProvider(IGuidProvider guidProvider)
        {
            ApplicationId = guidProvider.NewGuid;
        }
        public Guid ApplicationId { get; init; }
    }
}
