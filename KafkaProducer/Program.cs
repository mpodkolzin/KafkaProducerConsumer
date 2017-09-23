using CommandLine;
using Kafka.CLI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafkaProducer
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new CommandLineOptions();
            Parser.Default.ParseArgumentsStrict(args, options);
            var producer = new KafkaProducer(options.BrokerList.ToList(), options.Topic);

            producer.Start().Wait();
        }
    }
}
