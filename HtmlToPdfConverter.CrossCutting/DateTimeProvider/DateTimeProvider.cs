using System;

namespace HtmlToPdfConverter.CrossCutting.DateTimeProvider
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now => DateTime.Now;
    }
}
