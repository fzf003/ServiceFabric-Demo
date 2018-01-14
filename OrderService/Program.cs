﻿using System;
using System.Diagnostics;
using System.Fabric;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Runtime;
using Autofac;

namespace OrderService
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
                // ServiceManifest.XML 文件定义一个或多个服务类型名称。
                // 注册服务会将服务类型名称映射到 .NET 类型。
                // 在 Service Fabric 创建此服务类型的实例时，
                // 会在此主机进程中创建类的实例。

                ServiceRuntime.RegisterServiceAsync("OrderServiceType",
                    context => new OrderService(context)).GetAwaiter().GetResult();

                ServiceEventSource.Current.ServiceTypeRegistered(Process.GetCurrentProcess().Id, typeof(OrderService).Name);

                // 防止此主机进程终止，以使服务保持运行。
                Thread.Sleep(Timeout.Infinite);
            }
            catch (Exception e)
            {
                ServiceEventSource.Current.ServiceHostInitializationFailed(e.ToString());
                throw;
            }
        }

        public static IContainer RegisterServices()
        {
            ContainerBuilder builder = new ContainerBuilder();

            //builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            //builder.RegisterOwinApplicationContainer();

            //builder.RegisterType<Repository>()
            //       .As<IRepository>()
            //       .InstancePerLifetimeScope();

            return builder.Build();
        }
    }
}
