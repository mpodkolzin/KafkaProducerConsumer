using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kafka;
using KafkaNet.Common;
using KafkaNet.Model;
using KafkaNet;

namespace Kafka
{
    class Program
    {
        const string url = "http://10.4.200.9:32768";
        const string topic = "TestOS_2";
        static void Main(string[] args)
        {
            //Uncomment it if you need producer as well
            //var producer = new KafkaProducer(url, topic);
            //producer.Start();

            var consumer = new KafkaConsumer(url, topic);
            consumer.Start();

            Console.ReadLine();

        }
    }
}
