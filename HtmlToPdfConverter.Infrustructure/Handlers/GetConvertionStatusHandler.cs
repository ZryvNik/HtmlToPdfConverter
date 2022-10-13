using HtmlToPdfConverter.Contracts.GetStatus;
using HtmlToPdfConverter.Infrustructure.DataAccess;
using MediatR;

namespace HtmlToPdfConverter.Infrustructure.Handlers
{
    public class GetConvertionStatusHandler : IRequestHandler<GetConvertionStatusRequest, GetConvertionStatusResult>
    {
        private readonly IFileInfoRepository _fileInfoRepository;

        public GetConvertionStatusHandler(IFileInfoRepository fileInfoRepository)
        {
            _fileInfoRepository = fileInfoRepository;
        }

        public async Task<GetConvertionStatusResult> Handle(GetConvertionStatusRequest request, CancellationToken cancellationToken)
        {
            var result = _fileInfoRepository.GetFileProcessStatusByCorrelation(request.CorrelationId);

            return await Task.FromResult(new GetConvertionStatusResult()
            {
                Status = result.Status,
                PdfFileStorageId = result.PdfDownloadId
            });
        }
    }
}
