using HtmlToPdfConverter.Contracts.HtmlToPdfConvert;
using MassTransit;
using MediatR;
using Schemas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlToPdfConverter.Infrustructure.MassTransit.Consumers
{
    public class StartHtmlToPdfConversationConsumer : IConsumer<IStartHtmlToPdfConversationEvent>
    {
        private readonly IMediator _mediatr;

        public StartHtmlToPdfConversationConsumer(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        public async Task Consume(ConsumeContext<IStartHtmlToPdfConversationEvent> context)
        {
            var @event = context.Message;

            await _mediatr.Send(new HtmlToPdfConvertRequest()
            {
                CorrelationId = @event.CorrelationId
            });
        }
    }
}
