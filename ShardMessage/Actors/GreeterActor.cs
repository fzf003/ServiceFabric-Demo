using Akka.Actor;
using System;

namespace ShardMessage.Actors
{
    public class GreeterActor : ReceiveActor
    {
       // private readonly StatelessServiceContext _context;

        public GreeterActor()
        {

           // _context = context;

            Receive<GreetMessage>(x => Greet(x.Who));
            this.Receive<Request>(x => ResponseMessage(x));
        }

        void ResponseMessage(Request request)
        {
            Console.WriteLine("收到:{0}",request);
            this.Sender.Tell(new Response() { Body = "fzf003" + Guid.NewGuid().ToString("N") },Self);
        }
        private void Greet(string name)
        {
            Console.WriteLine("收到:{0}", name);
            ///ServiceEventSource.Current.ServiceMessage(_context, "Hello from {0} to {1}!", _context.NodeContext.NodeName, name);

        }

    }
}
