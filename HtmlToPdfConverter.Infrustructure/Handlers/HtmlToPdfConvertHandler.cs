using HtmlToPdfConverter.Contracts.HtmlToPdfConvert;
using HtmlToPdfConverter.CrossCutting.Enums;
using HtmlToPdfConverter.Infrustructure.ApplicationId;
using HtmlToPdfConverter.Infrustructure.Convertion;
using HtmlToPdfConverter.Infrustructure.DataAccess;
using HtmlToPdfConverter.Infrustructure.FileStorage;
using MediatR;

namespace HtmlToPdfConverter.Infrustructure.Handlers
{
    public class HtmlToPdfConvertHandler : IRequestHandler<HtmlToPdfConvertRequest, Unit>
    {
        private readonly IFileInfoRepository _fileInfoRepository;
        private readonly IFileStorageService _fileStorageService;
        private readonly IHtmlToPdfConvertionSevice _htmlToPdfConvertionSevice;

        public HtmlToPdfConvertHandler(IFileInfoRepository fileInfoRepository,
            IFileStorageService fileStorageService,
            IHtmlToPdfConvertionSevice htmlToPdfConvertionSevice)
        {
            _fileInfoRepository = fileInfoRepository;
            _fileStorageService = fileStorageService;
            _htmlToPdfConvertionSevice = htmlToPdfConvertionSevice;
        }

        public async Task<Unit> Handle(HtmlToPdfConvertRequest request, CancellationToken cancellationToken)
        {
            //Достаём информацию о файлах по CorrelationId
            var fileInfo = _fileInfoRepository.GetFileInfoByCorrelationId(request.CorrelationId);

            //Получаем stream html-файла и получаем из него строку
            var htmlFileStream = _fileStorageService.Get(fileInfo!.HtmlFileStorageId);

            var html = string.Empty;
            using (StreamReader sr = new(htmlFileStream))
            {
                html = await sr.ReadToEndAsync();
            }

            //Конвертируем строку в pdf
            var pdfFileStream = await _htmlToPdfConvertionSevice.ConvertAsync(html, cancellationToken);

            //Сохраняем pdf-файл
            var pdfFileStorageId = _fileStorageService.Upload(pdfFileStream);

            //Обновляем статус и прикрепляем идентификатор pdf-файла
            fileInfo.Status = FileProcessStatus.Proceed;
            fileInfo.PdfFileStorageId = pdfFileStorageId;
            _fileInfoRepository.Update(fileInfo);

            return await Unit.Task;
        }
    }
}
