using Microsoft.ServiceFabric.Actors.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
namespace TestActorFabricApp
{

    public interface IActorLocationService
    {
        TActorInterface Create<TActorInterface>(long actorId, string applicationName) where TActorInterface : IActor;
        TActorInterface Create<TActorInterface>(string actorId, string applicationName) where TActorInterface : IActor;
    }
    public class ActorLocationService : IActorLocationService
    {
        public TActorInterface Create<TActorInterface>(string actorId, string applicationName) where TActorInterface : IActor
        {
            return ActorProxy.Create<TActorInterface>(new ActorId(actorId), applicationName);
        }

        public TActorInterface Create<TActorInterface>(long actorId, string applicationName) where TActorInterface : IActor
        {
            return ActorProxy.Create<TActorInterface>(new ActorId(actorId), applicationName);
        }
    }
}
