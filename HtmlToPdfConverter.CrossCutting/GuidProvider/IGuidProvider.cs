using System;

namespace HtmlToPdfConverter.CrossCutting.GuidProvider
{
    public interface IGuidProvider
    {
        Guid NewGuid { get; }
    }
}
