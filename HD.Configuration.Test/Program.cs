using HD.Configuration.Abstractions;
using HD.Configuration.Binder;
using HD.Configuration.Consul;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HD.Configuration.Test
{
    class Program
    {
        public static IConfigurationRoot Configuration;

        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .SetConsulOptions(conf =>
                {
                    conf.Address = new Uri("http://192.168.0.59:8500");
                })
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddConsulConfig("hd/key1");
#if DEBUG
            builder.AddJsonFile("appsettings.Debug.json", optional: false, reloadOnChange: true);
#endif

            Configuration = builder.Build();

            //Console.WriteLine($"option1 = {Configuration["option1"]}");
            //Console.WriteLine($"option2 = {Configuration["option2"]}");
            //int opt2 = Configuration.GetValue<int>("option2");
            //Console.WriteLine($"suboption1 = {Configuration["subsection:suboption1"]}");
            //Console.WriteLine();
            //Console.ReadLine();
            //Console.WriteLine($"option2 = {Configuration["option2"]} .....");

            //Console.WriteLine("Wizards:");
            //Console.Write($"{Configuration["wizards:0:Name"]}, ");
            //Console.WriteLine($"age {Configuration["wizards:0:Age"]}");
            //Console.Write($"{Configuration["wizards:1:Name"]}, ");
            //Console.WriteLine($"age {Configuration["wizards:1:Age"]}");
            //var wizards = Configuration.GetSection("wizards").Get<List<Wizard>>();

            //集成consul测试
            var tOrder = Configuration["Trade:Order"];
            ChangeTokens.Instance.Reload("hd/key1");
            tOrder = Configuration["Trade:Order"];

            Console.ReadLine();
        }
    }


    class Wizard
    {
        public string Name { get; set; }

        public int Age { get; set; }
    }
}
