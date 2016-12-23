using KafkaNet;
using KafkaNet.Model;
using KafkaNet.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kafka
{
    public class KafkaProducer
    {
        private KafkaOptions _options;
        private BrokerRouter _router;
        private Producer _client;

        private string _topic;

        private readonly CancellationTokenSource _cts = new CancellationTokenSource();

        public KafkaProducer(string url, string topic, int delay = 2000)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("Url cannot be null");
            }
            if (string.IsNullOrEmpty(topic))
            {
                throw new ArgumentNullException("topic cannot be null");
            }

            _options = new KafkaOptions(new Uri(url));
            _router = new BrokerRouter(_options);
            _client = new Producer(_router);
            _topic = topic;

        }

        public void Stop()
        {
            _cts.Cancel();
        }

        public async Task Start()
        {
            var idx = 0;
            while(!_cts.Token.IsCancellationRequested)
            {
                var message = "test message #" + idx.ToString();
                await _client.SendMessageAsync(_topic, new[] { new Message(message)});
                Console.WriteLine("Message {0} has been sent", message);
                Thread.Sleep(2000);

                idx++;
            }

        }
    }
}
