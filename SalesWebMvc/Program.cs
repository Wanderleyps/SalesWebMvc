using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System;
//https://github.com/acenelio/workshop-asp-net-core-mvc
namespace SalesWebMvc
{
    public class Program
    {
        public static void Main(string[] args)
        {          
            CreateWebHostBuilder(args).Build().Run();         
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
