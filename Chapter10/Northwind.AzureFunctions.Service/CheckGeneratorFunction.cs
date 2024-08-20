using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker; // To use [Function] and so on
using Microsoft.Extensions.Logging; // To use ILogger
using SixLabors.Fonts; // To use Font
using SixLabors.ImageSharp.Drawing; // To use IPath
using SixLabors.ImageSharp.Drawing.Processing; // To use Brush, Pen

namespace Northwind.AzureFunctions.Service;

public class CheckGeneratorFunction
{
    private readonly ILogger _logger;

    public CheckGeneratorFunction(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<NumbersToWordsFunction>();
    }

    [Function(nameof(CheckGeneratorFunction))]
    [BlobOutput("checks-blob-container/check.png")]
    public byte[] Run([QueueTrigger("checksQueue")] string message)
    {
        _logger.LogInformation("C# Queue trigger function executed.");
        _logger.LogInformation($"Body: {message}.");

        // Create a new blank image with a white background
        using (Image<Rgba32> image = new(width: 1200, height: 600, backgroundColor: new Rgba32(r: 255, g:255, b: 255, a: 100)))
        {
            // Load the font file and create a large font
            SixLabors.Fonts.FontCollection collection = new();
            SixLabors.Fonts.FontFamily family = collection.Add(@"fonts\Caveat\static\Caveat-Regular.ttf");

            SixLabors.Fonts.Font font = family.CreateFont(72);

            DrawingOptions options = new()
            {
                GraphicsOptions = new()
                {
                    ColorBlendingMode = PixelColorBlendingMode.Multiply
                }
            };

            // Define some pens and brushes

            SixLabors.ImageSharp.Drawing.Processing.Pen blackPen = SixLabors.ImageSharp.Drawing.Processing.Pens.Solid(SixLabors.ImageSharp.Color.Black, 2);
            SixLabors.ImageSharp.Drawing.Processing.Pen blackThickPen = SixLabors.ImageSharp.Drawing.Processing.Pens.Solid(SixLabors.ImageSharp.Color.Black, 8);
            SixLabors.ImageSharp.Drawing.Processing.Pen greenPen = SixLabors.ImageSharp.Drawing.Processing.Pens.Solid(SixLabors.ImageSharp.Color.Green, 3);
            SixLabors.ImageSharp.Drawing.Processing.Brush redBrush = SixLabors.ImageSharp.Drawing.Processing.Brushes.Solid(SixLabors.ImageSharp.Color.Red);
            SixLabors.ImageSharp.Drawing.Processing.Brush blueBrush = SixLabors.ImageSharp.Drawing.Processing.Brushes.Solid(SixLabors.ImageSharp.Color.Blue);

            // Define some paths and draw them

            IPath border = new RectangularPolygon(x: 50, y: 50, width: 1100, height: 500);

            image.Mutate(x => x.Draw(options, blackPen, border));

            IPath star = new Star(x: 150.0f, y: 150.0f, prongs: 5, innerRadii: 20.0f, outerRadii: 30.0f);

            image.Mutate(x => x.Fill(options, redBrush, star).Draw(options, greenPen, star));

            IPath line1 = new Polygon(new LinearLineSegment(new SixLabors.ImageSharp.PointF(x: 100, y: 275), new SixLabors.ImageSharp.PointF(x: 1050, y: 275)));

            image.Mutate(x => x.Draw(options, blackPen, line1));

            IPath line2 = new Polygon(new LinearLineSegment(new SixLabors.ImageSharp.PointF(x: 100, y: 365), new SixLabors.ImageSharp.PointF(x: 1050, y: 365)));

            image.Mutate(x => x.Draw(options, blackPen, line2));

            RichTextOptions textOptions = new(font)
            {
                Origin = new SixLabors.ImageSharp.PointF(100, 200),
                WrappingLength = 1000,
                HorizontalAlignment = HorizontalAlignment.Left
            };

            image.Mutate(x => x.DrawText(textOptions, message, blueBrush, blackPen));

            string blobName = $"{DateTime.UtcNow:yyyy-MM-dd-hh-mm-ss}.png";
            _logger.LogInformation($"Blob filename: {blobName}.");

            try
            {
                if (Environment.GetEnvironmentVariable("IS_LOCAL") == "true")
                {
                    // Create blob in the Local filesystem

                    string folder = $@"{System.Environment.CurrentDirectory}\blobs";
                    
                    if (!Directory.Exists(folder))
                    {
                        Directory.CreateDirectory(folder);
                    }
                    _logger.LogInformation($"Blobs folder: {folder}");

                    string blobPath = $@"{folder}\{blobName}";

                    image.SaveAsPng(blobPath);
                }

                // Create BLOB in Blob Storage via a memory stream

                MemoryStream stream = new();

                image.SaveAsPng(stream);

                stream.Seek(0, SeekOrigin.Begin);

                return stream.ToArray();
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return Array.Empty<byte>();
        }
    }
}
