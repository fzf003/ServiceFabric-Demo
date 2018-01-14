using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Client;
using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserActor.Interfaces;

namespace TestFabricActor
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new FabricClient();
            client.QueryManager.GetApplicationListAsync()
                .ContinueWith(t => {

                    Console.WriteLine(t.Result);
                });
            CancellationToken cts = new CancellationToken();
            // fabric:/ B2BFabricApp / UserActorService
            var proxy = ActorProxy.Create<IUserActor>(
                    new ActorId(Guid.NewGuid()), "B2BFabricApp");
            proxy.SetCountAsync(22, cts);
            Console.WriteLine(proxy.GetCountAsync(cts).Result);
            /// await proxy.SubscribeAsync<IGameEvents>(new GameEventsHandler());
            Console.ReadKey();
        }
    }
}
