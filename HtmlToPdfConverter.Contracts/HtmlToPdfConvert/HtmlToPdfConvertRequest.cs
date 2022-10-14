using MediatR;
using System;

namespace HtmlToPdfConverter.Contracts.HtmlToPdfConvert
{
    public class HtmlToPdfConvertRequest : IRequest
    {
        public Guid CorrelationId { get; set; }
    }
}
