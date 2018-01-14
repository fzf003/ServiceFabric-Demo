using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace OwinApp
{
    public class TestController : ApiController
    {
        private readonly IlogHandler _logger;

        public TestController(IlogHandler logger)
        {
            this._logger = logger;
        }

        public dynamic Get()
        {
            this._logger.Log(string.Format("Inside the 'Get' method of the '{0}' controller.", GetType().Name));

            return new { name = "Hello, world!" };
        }
    }
}
