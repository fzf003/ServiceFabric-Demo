using System;
using System.Diagnostics;
using System.Fabric;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors.Runtime;

namespace UserActor
{
    internal static class Program
    {
        /// <summary>
        /// 这是服务主机进程的入口点。
        /// </summary>
        private static void Main()
        {
            try
            {
                // 此行注册了一个角色服务，以使用 Service Fabric 运行时来托管角色类。
                // ServiceManifest.xml 和 ApplicationManifest.xml 文件的内容
                // 在生成此项目时自动填充。
                //有关详细信息，请参阅 https://aka.ms/servicefabricactorsplatform

                ActorRuntime.RegisterActorAsync<UserActor>(
                   (context, actorType) => new ActorService(context, actorType)).GetAwaiter().GetResult();

                Thread.Sleep(Timeout.Infinite);
            }
            catch (Exception e)
            {
                ActorEventSource.Current.ActorHostInitializationFailed(e.ToString());
                throw;
            }
        }
    }
}
