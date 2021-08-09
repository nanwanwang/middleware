using Confluent.Kafka;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Kafka_Consumer
{
    class Program
    {
        static async Task Main(string[] args)
        {
           
           await Task.Factory.StartNew(()=> CreateConsumer("99"));
            await Task.Factory.StartNew(() => CreateConsumer("99"));
          
            Console.ReadLine();
        }


        private static void CreateConsumer(string consumerGroupName)
        {
            var conf = new ConsumerConfig
            {
                GroupId = consumerGroupName,
                BootstrapServers = "172.19.60.199:9092",
                SessionTimeoutMs = 10000,
                AutoCommitIntervalMs = 1000,
                
                 
                // Note: The AutoOffsetReset property determines the start offset in the event
                // there are not yet any committed offsets for the consumer group for the
                // topic/partitions of interest. By default, offsets are committed
                // automatically, so in this example, consumption will only start from the
                // earliest message in the topic 'my-topic' the first time you run the program.
                AutoOffsetReset = AutoOffsetReset.Latest
            };

            using (var c = new ConsumerBuilder<Ignore, string>(conf).Build())
            {
                c.Subscribe("test-topic2");
                
                var s = c.Assignment;
             

                CancellationTokenSource cts = new CancellationTokenSource();
                Console.CancelKeyPress += (_, e) =>
                {
                    e.Cancel = true; // prevent the process from terminating.
                    cts.Cancel();
                };

                try
                {
                    while (true)
                    {
                        try
                        {
                            
                            var cr = c.Consume(cts.Token);
                       
                            Console.WriteLine($"{consumerGroupName}_Partition{cr.Partition}_Consumed message '{cr.Message.Value}' at: '{cr.TopicPartitionOffset}'.");

                    
                        }
                        catch (ConsumeException e)
                        {
                            Console.WriteLine($"Error occured: {e.Error.Reason}");
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    // Ensure the consumer leaves the group cleanly and final offsets are committed.
                    c.Close();
                }
            }
        }
    }
}
