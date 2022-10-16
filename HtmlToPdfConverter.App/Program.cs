using HtmlToPdfConverter.CrossCutting.Configuration;

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
builder.Services.AddSingleton<IFileStorageService, LiteDbStorageService>();

builder.Services.AddSingleton<IGuidProvider, GuidProvider>();
builder.Services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
builder.Services.AddSingleton<IApplicationIdProvider, ApplicationIdProvider>();

builder.Services.AddSingleton<IFileLifeTimeProvider>(
    new FileLifeTimeProvider()
    {
        Minutes = builder.Configuration.GetSection("FileLifeTime:Munutes").Get<int>()
    });


builder.Services.AddRazorPages();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
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
app.MapGet("/Download", async (string fileStorrageId, IMediator mediatr) =>
{
    var request = new DownloadFileRequest(fileStorrageId);
    var result = await mediatr.Send(request);
    return Results.File(result.FileStream, MediaTypeNames.Application.Pdf, $"{Guid.NewGuid()}.pdf");
});

app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();

app.Run();
