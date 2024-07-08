using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

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
        public async Task Run([BlobTrigger("samples-workitems/{name}", Connection = "peacoplazanotification_STORAGE")] Stream stream, string name)
        {
            using var blobStreamReader = new StreamReader(stream);
            var content = await blobStreamReader.ReadToEndAsync();
            _logger.LogInformation($"C# Blob trigger function Processed blob\n Name: {name} \n Data: {content}");
        }
    }
}
