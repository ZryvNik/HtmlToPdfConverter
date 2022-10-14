using HtmlToPdfConverter.Contracts.Download;
using HtmlToPdfConverter.Contracts.GetStatus;
using HtmlToPdfConverter.Contracts.Upload;
using HtmlToPdfConverter.CrossCutting.DateTimeProvider;
using HtmlToPdfConverter.CrossCutting.GuidProvider;
using HtmlToPdfConverter.Infrustructure.ApplicationId;
using HtmlToPdfConverter.Infrustructure.Convertion;
using HtmlToPdfConverter.Infrustructure.DataAccess;
using HtmlToPdfConverter.Infrustructure.FileStorage;
using HtmlToPdfConverter.Infrustructure.MassTransit;
using HtmlToPdfConverter.Infrustructure.MassTransit.Consumers;
using HtmlToPdfConverter.Infrustructure.StartConverstion;
using LiteDB;
using MassTransit;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(
    typeof(UploadFileRequest).Assembly,
    typeof(StartHtmlToPdfConversationConsumer).Assembly);

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<StartHtmlToPdfConversationConsumer>();
    x.UsingInMemory((context, cfg) =>
    {
        cfg.ConfigureEndpoints(context);
    });
});
builder.Services.AddSingleton<ILiteDatabase>(new LiteDatabase("fileInfo.db"));

builder.Services.AddSingleton<IBusPublisher, BusPublisher>();
builder.Services.AddHostedService<StartConversationHostedService>();
builder.Services.AddScoped<IHtmlToPdfConvertionSevice, HtmlToPdfConvertionSevice>();
builder.Services.AddSingleton<IFileInfoRepository, FileInfoRepository>();
builder.Services.AddScoped<IFileStorageService, LiteDbStorageService>();

builder.Services.AddSingleton<IGuidProvider, GuidProvider>();
builder.Services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
builder.Services.AddSingleton<IApplicationIdProvider, ApplicationIdProvider>();


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", async (context) => await context.Response.SendFileAsync("html/index.html"));
app.MapPost("/upload", async (HttpContext context, IMediator mediatr) =>
{
    var request = context.Request;

    var stream = new MemoryStream();
    await request.Form.Files[0].CopyToAsync(stream);

    var uploadRequest = new UploadFileRequest()
    {
        FileStream = stream
    };

    return await mediatr.Send(uploadRequest);
});
app.MapGet("/GetStatus", async (Guid correlationId, IMediator mediatr) =>
{
    var request = new GetConvertionStatusRequest(correlationId);
    return await mediatr.Send(request);
});
app.MapGet("/Download", async (string pdfFileStorrageId, IMediator mediatr) =>
{
    var request = new DownloadFileRequest(pdfFileStorrageId);
    var result = await mediatr.Send(request);
    return Results.File(result.FileStream, "application/pdf");
});
app.Run();
