using PuppeteerSharp;

namespace HtmlToPdfConverter.Infrustructure.Convertion
{
    public class HtmlToPdfConvertionSevice : IHtmlToPdfConvertionSevice
    {
        private SemaphoreSlim _sem = new SemaphoreSlim(1, 1);
        public async Task<Stream> ConvertAsync(string html, CancellationToken cancellationToken)
        {
            using var browserFetcher = new BrowserFetcher();
            //Чтобы два потока не блокировали друг другу скачивание файлов Chromium 
            //ставим семафор
            await _sem.WaitAsync(cancellationToken);
            try
            {
                await browserFetcher.DownloadAsync(BrowserFetcher.DefaultChromiumRevision);
            }
            finally
            {
                _sem.Release();
            }

            await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true });
            await using var page = await browser.NewPageAsync();
            await page.SetContentAsync(html);
            await page.EvaluateExpressionHandleAsync("document.fonts.ready"); 
            return await page.PdfStreamAsync();
        }
    }
}
