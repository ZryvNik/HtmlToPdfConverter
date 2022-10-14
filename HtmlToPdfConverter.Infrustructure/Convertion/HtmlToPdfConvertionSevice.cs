using PuppeteerSharp;

namespace HtmlToPdfConverter.Infrustructure.Convertion
{
    public class HtmlToPdfConvertionSevice : IHtmlToPdfConvertionSevice
    {
        public HtmlToPdfConvertionSevice()
        {
            using var browserFetcher = new BrowserFetcher();
                browserFetcher.DownloadAsync(BrowserFetcher.DefaultChromiumRevision).GetAwaiter().GetResult();
        }
        public async Task<Stream> ConvertAsync(string html, CancellationToken cancellationToken)
        {
            await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true });
            await using var page = await browser.NewPageAsync();
            await page.SetContentAsync(html); // In case of fonts being loaded from a CDN, use WaitUntilNavigation.Networkidle0 as a second param.
            await page.EvaluateExpressionHandleAsync("document.fonts.ready"); // Wait for fonts to be loaded. Omitting this might result in no text rendered in pdf.
            return await page.PdfStreamAsync();
        }
    }
}
