using System;
using Nancy.Hosting.Self;

namespace ConsoleApp
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            var url = "http://oldpocket.com:8888";

            using (var host = new NancyHost(new Uri(url)))
            {
                host.Start();
                Console.WriteLine("Nancy Server listening on {0}", url);
				Console.ReadLine();

            }           
        }
    }
}
