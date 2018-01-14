using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace OwinApp
{
   public class AutofacModule:Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ConsoleLogHandler>().AsImplementedInterfaces();
            base.Load(builder);
        }
    }


    public interface IlogHandler
    {
        void Log(string str);
    }

    public class ConsoleLogHandler : IlogHandler
    {
        public void Log(string str)
        {
            Console.WriteLine(string.Format("[{0}]",DateTime.Now.ToString())+str);
        }
    }
}
