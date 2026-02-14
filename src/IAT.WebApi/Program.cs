using System;
using Microsoft.Owin.Hosting;

namespace IAT.WebApi
{
    class Program
    {
        static void Main(string[] args)
        {
            string baseAddress = "http://localhost:9000/";

            using (WebApp.Start<Startup>(baseAddress))
            {
                Console.WriteLine("BeautySquad API started at {0}", baseAddress);
                Console.WriteLine("Press ENTER to stop the server...");
                Console.ReadLine();
            }
        }
    }
}
