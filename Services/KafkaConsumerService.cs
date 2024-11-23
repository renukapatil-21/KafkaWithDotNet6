using Confluent.Kafka;
using System;
using System.Threading.Tasks;

namespace KafkaExample.Services
{
    public class KafkaConsumerService
    {
        private readonly IConsumer<Null, string> _consumer;

        public KafkaConsumerService()
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092",
                GroupId = "my-consumer-group",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            _consumer = new ConsumerBuilder<Null, string>(config).Build();
        }

        public void ConsumeMessages(string topic)
        {
            _consumer.Subscribe(topic);

            try
            {
                while (true)
                {
                    var consumeResult = _consumer.Consume();
                    Console.WriteLine($"Consumed message: {consumeResult.Message.Value}");
                }
            }
            catch (ConsumeException e)
            {
                Console.WriteLine($"Error consuming message: {e.Error.Reason}");
            }
        }
    }
}