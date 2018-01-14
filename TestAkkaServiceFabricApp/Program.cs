using Akka.Actor;
using Akka.Configuration;
using ShardMessage.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAkkaServiceFabricApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = ConfigurationFactory.ParseString(@"
                    akka {
                        actor.provider = ""Akka.Remote.RemoteActorRefProvider, Akka.Remote""

                        remote {
                             dot-netty.tcp {
                                port = 0
                                hostname = localhost
                            }
                        }
                    }
                ");

            using (var system = ActorSystem.Create("MyStatelessGreetee", config))
            {
                //akka.tcp://MyStatelessGreeter@localhost:34009
                string primaryReplicaAddress = "akka.tcp://MyStatelessGreeter@localhost:9987";
                for (;;)
                {
                    var greeter = system.ActorSelection(primaryReplicaAddress + "/user/greeter");
                    greeter.Ask<Response>(Request.instance)
                        .ContinueWith(task =>
                        {
                            Console.WriteLine(task.Result.Body);
                        });
                    Console.ReadKey();
                }
            }
            Console.WriteLine("客户端关闭...");
            Console.ReadKey();
           
        }
    }
}
