using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShardMessage.Actors
{
    public class GreetMessage
    {
        public string Who { get; private set; }

        public GreetMessage(string who)
        {
            Who = who;
        }
    }

    public class Request
    {
        public static Request instance = new Request();
    }

    public class Response
    {
        public string Body { get; set; }
    }
}
