using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kafka
{
    //WORK IN PROGRESS
    public class KafkaProducer
    {

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
                //await _client.SendMessageAsync(_topic, new[] { new Message(message)});
                Console.WriteLine("Message {0} has been sent", message);
                await Task.Delay(2000);

                idx++;
            }

        }
    }
}
