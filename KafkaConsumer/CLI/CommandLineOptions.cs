using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafka.CLI
{
    public class CommandLineOptions
    {
        [OptionList('b', "brokers", Required = true,
            HelpText = "Kafka broker", Separator = '\0')]
        public IEnumerable<string> BrokerList { get; set; }

        [Option('t', "topic", Required = true,
            HelpText = "Kafka topic")]
        public string Topic { get; set; }

        [Option('o', "offset", Required = false,
            HelpText = "Topic offset")]
        public long Offset { get; set; } = 0;

        [Option("from-beginning", Required = false,
            HelpText = "Start consuming from offset 0")]
        public bool FromBeginning { get; set; }

        [Option("consumer-group", Required = false,
            HelpText = "Kafka topic", DefaultValue = "test_consumer")]
        public string ConsumerGroup { get; set; }

    }
}
