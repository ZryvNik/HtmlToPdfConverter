namespace HtmlToPdfConverter.Infrustructure.MassTransit
{
    public interface IBusPublisher
    {
        Task Publish<T>(T @event, CancellationToken cancellationToken);
    }
}
