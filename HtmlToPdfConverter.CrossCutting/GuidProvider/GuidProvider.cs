using System;

namespace HtmlToPdfConverter.CrossCutting.GuidProvider
{
    public class GuidProvider : IGuidProvider
    {
        public Guid NewGuid => Guid.NewGuid();
    }
}
