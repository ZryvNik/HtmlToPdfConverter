using MediatR;
using System;

namespace HtmlToPdfConverter.Contracts.GetStatus
{
    public class GetConvertionStatusRequest : IRequest<GetConvertionStatusResult>
    {
        public Guid CorrelationId { get; set; }
    }
}
