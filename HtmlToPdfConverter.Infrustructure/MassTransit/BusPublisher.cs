using MassTransit;

namespace HtmlToPdfConverter.Infrustructure.MassTransit
{
    public class BusPublisher : IBusPublisher
    {
        private readonly IBus _bus;

        public BusPublisher(IBus bus)
        {
            _bus = bus;
        }

        public async Task Publish<T>(T @event, CancellationToken cancellationToken)
        {
            await _bus.Publish(@event, cancellationToken);
        }
    }
}
