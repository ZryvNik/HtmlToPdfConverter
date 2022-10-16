using HtmlToPdfConverter.CrossCutting.Enums;
using HtmlToPdfConverter.Infrustructure.ApplicationId;
using HtmlToPdfConverter.Infrustructure.DataAccess;
using HtmlToPdfConverter.Infrustructure.MassTransit;
using HtmlToPdfConverter.Infrustructure.MassTransit.Events;
using Microsoft.Extensions.Hosting;

namespace HtmlToPdfConverter.Infrustructure.StartConverstion
{
    public class StartConversationHostedService : BackgroundService
    {
        private readonly IBusPublisher _busPublisher;
        private readonly IFileInfoRepository _repository;
        private readonly IApplicationIdProvider _applicationIdProvider;

        public StartConversationHostedService(IBusPublisher busPublisher, 
            IFileInfoRepository repository, 
            IApplicationIdProvider applicationIdProvider)
        {
            _busPublisher = busPublisher;
            _repository = repository;
            _applicationIdProvider = applicationIdProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var fileInfo = _repository.GetNextFileInfoForConvertion();

                if (fileInfo != null)
                {
                    await _busPublisher.Publish(new StartHtmlToPdfConversationEvent()
                    {
                        CorrelationId = fileInfo.CorrelationId
                    }, stoppingToken);
                    //Обновляем статус на "в процессе" и выставляяем идентификатор приложения, который взял в обработку
                    fileInfo.Status = FileProcessStatus.InProgress;
                    fileInfo.ProceedByApplicationId = _applicationIdProvider.ApplicationId;
                    _repository.Update(fileInfo);
                }

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
