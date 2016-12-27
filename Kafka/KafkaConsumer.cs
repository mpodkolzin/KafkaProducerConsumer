using KafkaNet;
using KafkaNet.Common;
using KafkaNet.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kafka
{
    public class KafkaConsumer
    {
        private KafkaOptions _options;
        private BrokerRouter _router;
        private Consumer _consumer;

        private CancellationTokenSource _cts = new CancellationTokenSource();

        public KafkaConsumer(string url, string topic, int delay=2000)
        {
            if(string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("Url cannot be null");
            }
            if(string.IsNullOrEmpty(topic))
            {
                throw new ArgumentNullException("topic cannot be null");
            }
            _options = new KafkaOptions(new Uri(url));
            _router = new BrokerRouter(_options);
            _consumer = new Consumer(new ConsumerOptions(topic, _router));
        }

        public void Stop()
        {
            _cts.Cancel();
        }

        public async Task Start()
        {
            while(!_cts.Token.IsCancellationRequested)
            {
                foreach (var message in _consumer.Consume())
                {
                    Console.WriteLine("Response: P{0},O{1} : {2}", 
                        message.Meta.PartitionId, message.Meta.Offset, message.Value.ToUtf8String());  
                }

                await Task.Delay(2000);
            }
        }
    }
}
