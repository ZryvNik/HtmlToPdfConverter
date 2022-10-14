using MediatR;
using System;
using System.IO;

namespace HtmlToPdfConverter.Contracts.Upload
{
    public class UploadFileRequest : IRequest<UploadFileResult>
    {
        public Stream FileStream { get; set; }
    }
}
