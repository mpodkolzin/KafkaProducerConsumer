using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kafka;
using CommandLine;
using Kafka.CLI;

namespace Kafka
{
    class Program
    {
        const string url = "10.4.200.9:32768";
        const string topic = "TestOS_2";
        static void Main(string[] args)
        {
            var options = new CommandLineOptions();

            Parser.Default.ParseArgumentsStrict(args, options);

            var consumer = new KafkaConsumer(options.BrokerList.ToList(), options.Topic, options.Offset);
            Console.ReadKey();
            consumer.Stop();
        }
    }
}
