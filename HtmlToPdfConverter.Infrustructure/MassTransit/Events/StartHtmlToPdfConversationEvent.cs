using Schemas;

namespace HtmlToPdfConverter.Infrustructure.MassTransit.Events
{
    public class StartHtmlToPdfConversationEvent : IStartHtmlToPdfConversationEvent
    {
        public Guid CorrelationId { get; set; }
    }
}
