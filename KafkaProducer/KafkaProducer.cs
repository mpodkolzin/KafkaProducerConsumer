using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KafkaProducer
{
    public class KafkaProducer
    {

        private string _topic;
        private Dictionary<string, object> _config;
        private int _delay;

        public KafkaProducer(List<string> brokerList, string topic, int delay = 2000)
        {
            if (brokerList == null || !brokerList.Any())
            {
                throw new ArgumentNullException(nameof(brokerList));
            }
            if (string.IsNullOrEmpty(topic))
            {
                throw new ArgumentNullException(nameof(topic));
            }

            _delay = delay;
            _config = new Dictionary<string, object>
            {
                { "bootstrap.servers", brokerList.Aggregate("", (list, srv) => list + " " + srv) }
            };

            _topic = topic;

        }

        public async Task Start()
        {
            var idx = 0;
            using (var producer = new Producer<Null, string>(_config, null, new StringSerializer(Encoding.UTF8)))
            {
                Console.WriteLine($"{producer.Name} producing on {_topic}. q to exit.");
                Console.WriteLine("\n");

                string text;
                while ((text = Console.ReadLine()) != "q")
                {
                    var deliveryReport = producer.ProduceAsync(_topic, null, text);
                    await deliveryReport.ContinueWith(task =>
                    {
                        Console.WriteLine($"Partition: {task.Result.Partition}, Offset: {task.Result.Offset}");
                    });
                }

                // Tasks are not waited on synchronously (ContinueWith is not synchronous),
                // so it's possible they may still in progress here.
                producer.Flush(TimeSpan.FromSeconds(10));
            }
            var message = "test message #" + idx.ToString();
            //await _client.SendMessageAsync(_topic, new[] { new Message(message)});
            Console.WriteLine("Message {0} has been sent", message);
            await Task.Delay(_delay);
            idx++;
        }
    }
}
