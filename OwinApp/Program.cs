using Microsoft.Owin.Hosting;
using System;

namespace OwinApp
{
    class Program
    {
        static void Main(string[] args)
        {



            

            const string baseAddress = "http://localhost:9123/";

          
            using (WebApp.Start<Startup>(baseAddress))
            {
                Console.WriteLine("启动....");
                Console.ReadKey();
            }
                

        }
    }
}
