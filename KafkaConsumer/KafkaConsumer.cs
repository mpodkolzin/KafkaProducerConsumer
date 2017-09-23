﻿using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using Kafka.CLI;
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
        private int _receiveTimeout = 3; //receive timeout in seconds
        private Consumer<Null, string> _consumer;
        private Dictionary<string, object> _config;
        private int _delay;
        private long _offset;
       


        private CancellationTokenSource _cts = new CancellationTokenSource();
        string _topic;


        public KafkaConsumer(List<string> brokerList, string topic, long offset = 0, int delay=2000)
        {
            if(brokerList == null || !brokerList.Any())
            {
                throw new ArgumentNullException(nameof(brokerList));
            }
            if(string.IsNullOrEmpty(topic))
            {
                throw new ArgumentNullException(nameof(topic));
            }
            _offset = offset;
            _delay = delay;
            _topic = topic;
            _config = new Dictionary<string, object>
            {
                { "group.id", "simple-csharp-consumer" },
                { "bootstrap.servers", brokerList.Aggregate("",(list, srv) => list + " " + srv)},
                { "default.topic.config", new Dictionary<string, object>
                    {
                        { "acks", "all" }
                    }
                }
            };
        }

        public KafkaConsumer(CommandLineOptions options)
        {

        }

        public void Stop()
        {
            _cts.Cancel();
        }

        public async Task Start()
        {
            using (_consumer = new Consumer<Null, string>(_config, null, new StringDeserializer(Encoding.UTF8)))
            {
                _consumer.Assign(new List<TopicPartitionOffset> { new TopicPartitionOffset(_topic, 0, _offset) });
                _consumer.OnLog += (e, a) => {

                    Console.WriteLine("Error");

                };
                var metadata = _consumer.GetMetadata(true);
                while (!_cts.Token.IsCancellationRequested)
                {
                    Message<Null, string> msg;
                    if(_consumer.Consume(out msg, TimeSpan.FromSeconds(_receiveTimeout)))
                    {
                        Console.WriteLine($"{msg.Timestamp.UtcDateTime}: Received:: topic: [{msg.Topic}], Partition: [{msg.Partition}], {msg.Value}");

                    }

                    await Task.Delay(_delay);
                }

            }

        }
    }
}
