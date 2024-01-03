namespace producer
{
    using Dapr.Client;
    using producer.Models;

    public class Program
    {
        static async Task Main(string[] args)
        {
            await GenerateMessagesAsync();
        }

        static async Task GenerateMessagesAsync()
        {
            var daprClientBuilder = new DaprClientBuilder();
            var client = daprClientBuilder.Build();

            while (true)
            {
                var message = GenerateNewMessage();
                Console.WriteLine("Publishing: {0}", message);

                try
                {
                    await client.PublishEventAsync<SampleMessage>("sampletopic", "test", message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

                // Delay 10 seconds
                await Task.Delay(TimeSpan.FromSeconds(10.0));
            }
        }

        static public SampleMessage GenerateNewMessage()
        {
            return new SampleMessage()
            {
                CorrelationId = Guid.NewGuid(),
                MessageId = Guid.NewGuid(),
                Message = GenerateRandomMessage(),
                CreationDate = DateTime.UtcNow,
                PreviousAppTimestamp = DateTime.UtcNow
            };
        }

        static public string GenerateRandomMessage()
        {
            var random = new Random();
            var HashTags = new string[]
            {
                "circle",
                "ellipse",
                "square",
                "rectangle",
                "triangle",
                "star",
                "cardioid",
                "epicycloid",
                "limocon",
                "hypocycoid"
            };

            int length = random.Next(5, 10);
            string s = "";
            for (int i = 0; i < length; i++)
            {
                int j = random.Next(26);
                char c = (char)('a' + j);
                s += c;
            }
            s += " #" + HashTags[random.Next(HashTags.Length)];
            return s;
        }
    }
}
