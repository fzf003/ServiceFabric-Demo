using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserActor.Interfaces;

namespace TestActorFabricApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceUri = new Uri("fabric:/B2BFabricApp/UserActorService");
           // IUserActor proxy = ActorProxy.Create<IUserActor>(new ActorId("fzf"), serviceUri);
            var factory = new ActorLocationService();
            var proxy=factory.Create<IUserActor>("fzf003", "B2BFabricApp");
            //var proxy = ServiceProxy.Create<IUserActor>(serviceUri);

            var actor = proxy.GetActorId<IUserActor>();
            for (;;)
            {
                 proxy.SetCountAsync(new UserStatus { Age=90, Name=Guid.NewGuid().ToString() }, CancellationToken.None);
                var ss = proxy.GetCountAsync(CancellationToken.None);
                //     { "应为来自命名空间“http://schemas.datacontract.org/2004/07/UserActor.Interfaces”的元素“UserStatus”。。遇到名称为“int”、命名空间为“http://schemas.microsoft.com/2003/10/Serialization/”的“Element”。"}
                Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(ss.Result));
                Console.ReadKey();
            }
                Console.ReadKey();

        }
    }
}
