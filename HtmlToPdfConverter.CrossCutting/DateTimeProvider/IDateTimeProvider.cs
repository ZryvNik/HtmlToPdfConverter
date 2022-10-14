using System;

namespace HtmlToPdfConverter.CrossCutting.DateTimeProvider
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
    }
}
