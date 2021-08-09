using Confluent.Kafka;
using System;
using System.Threading.Tasks;

namespace Kafka_Producer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = "172.19.60.199:9092",
                Acks = Acks.All,
                MessageSendMaxRetries = 1,
                LingerMs = 1,
            };

            // If serializers are not specified, default serializers from
            // `Confluent.Kafka.Serializers` will be automatically used where
            // available. Note: by default strings are encoded as UTF8.
            using (var p = new ProducerBuilder<Null, string>(config).Build())
            {
                while (true)
                { 
                    try
                    {
                        var dr = await p.ProduceAsync(new TopicPartition ("test-topic2",new Partition(0)), new Message<Null, string> { Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")});
                        Console.WriteLine($"Delivered '{dr.Value}' to '{dr.TopicPartitionOffset}'");
                        var dr2 = await p.ProduceAsync(new TopicPartition("test-topic2", new Partition(1)), new Message<Null, string> { Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") });
                        Console.WriteLine($"Delivered '{dr2.Value}' to '{dr2.TopicPartitionOffset}'");
                    }
                    catch (ProduceException<Null, string> e)
                    {
                        Console.WriteLine($"Delivery failed: {e.Error.Reason}");
                       
                    }

                    await Task.Delay(1000);
                }
              
            }
        }
    }
}
