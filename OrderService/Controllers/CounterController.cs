using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Client;
using Microsoft.ServiceFabric.Services.Client;
using System;
using System.Fabric;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using UserActor.Interfaces;

namespace OrderService.Controllers
{
    public class CounterController : ApiController
    {
       static  string actorid = "fzf003";
        static int i = 0;
        /*private readonly IQueryCounter _queryCounter;
        private readonly IIncrementCounter _incrementCounter;
           */
       // private readonly StatelessServiceContext serviceContext;
        public CounterController()
        {
          
           // var partitionKey = new ServicePartitionKey(0);



            // var  _queryCounter = ServiceProxy.Create<IQueryCounter>(serviceUri, partitionKey);
            //_incrementCounter = ServiceProxy.Create<IIncrementCounter>(serviceUri, partitionKey);
        }
        [HttpGet]
        //[Route("api/counter/add",Name ="ffz")]
        public async Task<IHttpActionResult> Add()
        {
            var serviceUri = new Uri("fabric:/B2BFabricApp/UserActorService");
            IUserActor proxy = ActorProxy.Create<IUserActor>(new ActorId(actorid), serviceUri);

            await proxy.SetCountAsync(new UserStatus { Age=Interlocked.Increment(ref i), Name=Guid.NewGuid().ToString("N") }, CancellationToken.None);
            return Ok(new { name = "OK" });

        }
        [HttpGet]
       [Route("api/counter/get",Name ="ff")]
        public async Task<IHttpActionResult> Get()
        {
           /// string serviceUri = this.serviceContext.CodePackageActivationContext.ApplicationName + "/" ;
            var serviceUri = new Uri("fabric:/B2BFabricApp/UserActorService");
            IUserActor proxy = ActorProxy.Create<IUserActor>(new ActorId(actorid), serviceUri);
            //var user = new UserStatus() { Age=988, Name=Guid.NewGuid().ToString() };
           // await proxy.SetCountAsync(user, CancellationToken.None);
            var userinfo= await proxy.GetCountAsync(CancellationToken.None);
            return this.Json<UserStatus>(userinfo);
            //return Ok(new { name= });
        }

        public async Task<IHttpActionResult> Post([FromBody] int incrementCounterBy)
        {
            //await _incrementCounter.IncrementCounterAsync(incrementCounterBy).ConfigureAwait(false);
            return Ok();
        }
    }
}
