using System;

namespace Schemas
{
    public interface IStartHtmlToPdfConversationEvent
    {
        public Guid CorrelationId { get; set; }
    }
}
