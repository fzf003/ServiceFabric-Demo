using System;
using System.Collections.Generic;
using System.Fabric.Query;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Remoting;

namespace MyStatefulService
{

   // [ServiceContract]
    public interface IConnector2 : IService
    {
        //[OperationContract]
        Task<bool> CloseSession(Guid clientSessionId);

        // TODO: Add your service operations here
    }
}
