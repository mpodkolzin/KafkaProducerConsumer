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
        static void Main(string[] args)
        {
            var options = new CommandLineOptions();

            Parser.Default.ParseArgumentsStrict(args, options);

            var consumer = new KafkaConsumer(options.BrokerList.ToList(), options.Topic, options.Offset);
            consumer.Start();
            Console.ReadKey();
            consumer.Stop();
        }
    }
}
