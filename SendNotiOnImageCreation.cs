using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;

namespace Noti.ImageCreation
{
    public class SendNotiOnImageCreation
    {
        private readonly ILogger<SendNotiOnImageCreation> _logger;

        public SendNotiOnImageCreation(ILogger<SendNotiOnImageCreation> logger)
        {
            _logger = logger;
        }

        [Function(nameof(SendNotiOnImageCreation))]
        [BlobOutput("resize-avatar/{name}", Connection = "peacoplazanotification_STORAGE")]
        public async Task<Stream> Run([BlobTrigger("avatar/{name}", Connection = "peacoplazanotification_STORAGE")] byte[] stream, string name)
        {
            _logger.LogInformation($"C# Blob trigger function Processed blob\n Name: {name}");
            using(var image = Image.Load(stream)){
                image.Mutate(c => c.Resize(200,200));
                var outStream = new MemoryStream();
                image.Save(outStream, new PngEncoder());
                outStream.Position = 0;
                _logger.LogInformation("Avatar has been resized and saved.");
                return outStream;
            }
        }
    }
}
