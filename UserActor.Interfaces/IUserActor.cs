using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Services.Remoting;

namespace UserActor.Interfaces
{
    /// <summary>
    /// 此接口定义角色所公开的方法。
    /// 客户端使用此接口与实现它的角色进行交互。
    /// </summary>
    public interface IUserActor : IActor
    {
        /// <summary>
        /// TODO: 替换为你自己的角色方法。
        /// </summary>
        /// <returns></returns>
        Task<UserStatus> GetCountAsync(CancellationToken cancellationToken);

        /// <summary>
        /// TODO: 替换为你自己的角色方法。
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        Task SetCountAsync(UserStatus username, CancellationToken cancellationToken);
    }
}
