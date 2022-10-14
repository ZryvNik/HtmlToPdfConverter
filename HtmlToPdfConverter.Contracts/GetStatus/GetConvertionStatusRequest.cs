using MediatR;
using System;

namespace HtmlToPdfConverter.Contracts.GetStatus
{
    public class GetConvertionStatusRequest : IRequest<GetConvertionStatusResult>
    {
        public GetConvertionStatusRequest(Guid correlationId)
        {
            CorrelationId = correlationId;
        }
        public Guid CorrelationId { get; set; }
    }
}
