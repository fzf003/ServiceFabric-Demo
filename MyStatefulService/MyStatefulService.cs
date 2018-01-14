using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Data.Collections;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.Runtime;
namespace MyStatefulService
{
    /// <summary>
    /// 通过 Service Fabric 运行时为每个服务副本创建此类的一个实例。
    /// </summary>
    internal sealed class MyStatefulService : StatefulService
    {
        public MyStatefulService(StatefulServiceContext context)
            : base(context)
        { }

         

        /// <summary>
        ///可选择性地替代以创建侦听器(如 HTTP、服务远程、WCF 等)，从而使此服务副本可处理客户端或用户请求。
        /// </summary>
        /// <remarks>
        ///有关服务通信的详细信息，请参阅 https://aka.ms/servicefabricservicecommunication
        /// </remarks>
        /// <returns>侦听器集合。</returns>
        protected override IEnumerable<ServiceReplicaListener> CreateServiceReplicaListeners()
        {
 
            //return new List<ServiceReplicaListener>()
            //{

                 
            //    new ServiceReplicaListener(context => this.CreateServiceRemotingListener(context))


            //};

            return new ServiceReplicaListener[0];
        }

        /// <summary>
        /// 这是服务副本的主入口点。
        /// 在此服务副本成为主服务并具有写状态时，将执行此方法。
        /// </summary>
        /// <param name="cancellationToken">已在 Service Fabric 需要关闭此服务副本时取消。</param>
        protected override async Task RunAsync(CancellationToken cancellationToken)
        {
            // TODO: 将以下示例代码替换为你自己的逻辑 
            //       或者在服务不需要此 RunAsync 重写时删除它。

            var myDictionary = await this.StateManager.GetOrAddAsync<IReliableDictionary<string, long>>("myDictionary");

            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();

                using (var tx = this.StateManager.CreateTransaction())
                {
                    var result = await myDictionary.TryGetValueAsync(tx, "Counter");

                    ServiceEventSource.Current.ServiceMessage(this.Context, "Current Counter Value: {0}",
                        result.HasValue ? result.Value.ToString() : "Value does not exist.");

                    await myDictionary.AddOrUpdateAsync(tx, "Counter", 0, (key, value) => ++value);

                    // 如果在调用 CommitAsync 前引发异常，则将终止事务，放弃 
                    // 所有更改，并且辅助副本中不保存任何内容。
                    await tx.CommitAsync();
                }

                await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
            }
        }
    }
}
