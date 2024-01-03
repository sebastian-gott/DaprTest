using System.Net;
using System.Transactions;
using System.Text;
using Dapr.Client;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using producer_fa.Models;
using System.Text.Json.Serialization;
using System.Text.Json;
using Google.Protobuf;

namespace producer_fa
{
    public class Producer
    {
        private readonly ILogger _logger;

        public Producer(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<Producer>();
        }


        [Function("ProduceTestFa")]
        public async Task<HttpResponseData> ProduceMessage(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "trigger/Message/")] HttpRequestData req)
        {
            try
            {
                var daprClientBuilder = new DaprClientBuilder();
                var client = daprClientBuilder.Build();

                var httpResponseBody = await new StreamReader(req.Body).ReadToEndAsync();

                Message? kafkaMessage = JsonSerializer.Deserialize<Message>(httpResponseBody);

                Console.WriteLine("Publishing: {0}", kafkaMessage);

                await client.PublishEventAsync<Message>("sampletopic", "test", kafkaMessage);
                return req.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return req.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }
}
