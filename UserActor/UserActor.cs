using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Runtime;
using Microsoft.ServiceFabric.Actors.Client;
using UserActor.Interfaces;

namespace UserActor
{
    /// <remarks>
    /// 此类代表一个角色。
    /// 每个 ActorID 都映射到此类的一个实例。
    ///StatePersistence 属性确定角色状态的暂留和复制:
    ///  - 持久化: 状态已写入磁盘并已复制。
    ///  - 易失: 状态仅保留在内存中并已复制。
    ///  - 无: 状态已保留在内存中且未复制。
    /// </remarks>
    [StatePersistence(StatePersistence.Persisted)]
    internal class UserActor : Actor, IUserActor
    {
        /// <summary>
        /// 初始化 UserActor 的新实例
        /// </summary>
        /// <param name="actorService">将托管此角色实例的 Microsoft.ServiceFabric.Actors.Runtime.ActorService。</param>
        /// <param name="actorId">此角色实例的 Microsoft.ServiceFabric.Actors.ActorId。</param>
        public UserActor(ActorService actorService, ActorId actorId)
            : base(actorService, actorId)
        {
        }

        public Task<UserStatus> GetCountAsync(CancellationToken cancellationToken)
        {
            return this.StateManager.GetStateAsync<UserStatus>("count");
        }

        public async  Task SetCountAsync(UserStatus username, CancellationToken cancellationToken)
        {
          await  this.StateManager.RemoveStateAsync("count");
             await this.StateManager.TryAddStateAsync("count", username);

            // this.StateManager.AddOrUpdateStateAsync("count", new UserStatus(), (key, value) => { value.Name = username;return value; }, cancellationToken);
        }

        /// <summary>
        /// 每当激活角色时，都会调用此方法。
        /// 首次调用任意角色方法时，都会激活角色。
        /// </summary>
        protected override Task OnActivateAsync()
        {
            ActorEventSource.Current.ActorMessage(this, "Actor activated.");

            // StateManager 是此角色的私有状态存储。
            // 将复制 StateManager 中存储的数据，以便使用可变或持久化状态存储的角色实现高可用性。
            // 任何可序列化的对象均可保存在 StateManager 中。
            //有关详细信息，请参阅 https://aka.ms/servicefabricactorsstateserialization

            return this.StateManager.TryAddStateAsync("count", new UserStatus { Age=90, Name="fzf003" });
        }

        /// <summary>
        /// TODO: 替换为你自己的角色方法。
        /// </summary>
        /// <returns></returns>
        //Task<int> IUserActor.GetCountAsync(CancellationToken cancellationToken)
        //{
        //    return this.StateManager.GetStateAsync<string>("count", cancellationToken);
        //}

        /// <summary>
        /// TODO: 替换为你自己的角色方法。
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        //Task IUserActor.SetCountAsync(int count, CancellationToken cancellationToken)
        //{
        //    // 无法保证会按顺序处理请求，也不保证最多处理一次。
        //    // 此处的更新函数验证了入站计数大于保留顺序的当前计数。
        //    return this.StateManager.AddOrUpdateStateAsync("count", count, (key, value) => count > value ? count : value, cancellationToken);
        //}
    }
}
