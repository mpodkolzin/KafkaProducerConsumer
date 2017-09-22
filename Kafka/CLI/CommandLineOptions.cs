using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafka.CLI
{
    class CommandLineOptions
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

        [Option('f', "from-beginning", Required = false,
            HelpText = "Start consuming from offset 0")]
        public bool FromBeginning { get; set; }

    }
}
