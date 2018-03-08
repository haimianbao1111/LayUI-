using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ
{
    class Program
    {
        static void Main(string[] args)
        {
        }
        public static void Producer(int value)
        {
            try
            {
                var qName = "lhtest1";
                var exchangeName = "fanoutchange1";
                var exchangeType = "fanout";//topic、fanout
                var routingKey = "*";
                var uri = new Uri("amqp://192.168.1.104/");
                var factory = new ConnectionFactory
                {
                    UserName = "123",
                    Password = "123",
                    RequestedHeartbeat = 0,
                    Endpoint = new AmqpTcpEndpoint(uri)
                };
                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        //设置交换器的类型
                        channel.ExchangeDeclare(exchangeName, exchangeType);
                        //声明一个队列，设置队列是否持久化，排他性，与自动删除
                        channel.QueueDeclare(qName, true, false, false, null);
                        //绑定消息队列，交换器，routingkey
                        channel.QueueBind(qName, exchangeName, routingKey);
                        var properties = channel.CreateBasicProperties();
                        //队列持久化
                        properties.Persistent = true;
                        var m = new IMessage(DateTime.Now, value + "");
                        var body = Encoding.UTF8.GetBytes(DoJson.ModelToJson<QMessage>(m));
                        //发送信息
                        channel.BasicPublish(exchangeName, routingKey, properties, body);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    internal class AmqpTcpEndpoint
    {
        private Uri uri;

        public AmqpTcpEndpoint(Uri uri)
        {
            this.uri = uri;
        }
    }
}
