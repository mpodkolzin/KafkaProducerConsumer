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

    }
}
