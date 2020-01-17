using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Xamarin.Essentials;

namespace XamarinDotNetCoreIssue
{
    public static class Startup
    {
        private static void ExtractSaveResource(string filename, string location)
        {
            var a = Assembly.GetExecutingAssembly();
            using (var resFilestream = a.GetManifestResourceStream(filename))
            {
                if (resFilestream != null)
                {
                    var full = Path.Combine(location, filename);

                    using (var stream = File.Create(full))
                    {
                        resFilestream.CopyTo(stream);
                    }
                }
            }
        }

        public static IServiceProvider ServiceProvider { get; set; }

        public static void Init(Action<HostBuilderContext, IServiceCollection> nativeConfigureServices)
        {
            // var a = Assembly.GetExecutingAssembly();
            //using var stream = a.GetManifestResourceStream("XamarinDotNetCoreIssue.appsettings.json");

            var systemDir = FileSystem.CacheDirectory;
            ExtractSaveResource("XamarinDotNetCoreIssue.appsettings.json", FileSystem.CacheDirectory);
            var fullConfig = Path.Combine(systemDir, "XamarinDotNetCoreIssue.appsettings.json");

            var host = new HostBuilder()
                .ConfigureHostConfiguration(c =>
                {
                    //// Tell the host configuration where to file the file (this is required for Xamarin apps)
                    //c.AddCommandLine(new string[] { $"ContentRoot={FileSystem.AppDataDirectory}" });
                    ////read in the configuration file!
                    //c.AddJsonStream(stream);

                    c.AddCommandLine(new string[] { $"ContentRoot={FileSystem.AppDataDirectory}" });
                    c.AddJsonFile(fullConfig);
                })
                .ConfigureServices((c, x) =>
                {
                // Configure our local services and access the host configuration
                ConfigureServices(c, x);
                        })
                        .ConfigureLogging(l => l.AddConsole(o =>
                        {
                            //setup a console logger and disable colors since they don't have any colors in VS
                            o.DisableColors = true;
                        }))
                .Build();

            //Save our service provider so we can use it later.
            ServiceProvider = host.Services;

        }

        static void ConfigureServices(HostBuilderContext ctx, IServiceCollection services)
        {
            // The HostingEnvironment comes from the appsetting.json and could be optionally used to configure the mock service
            if (ctx.HostingEnvironment.IsDevelopment())
            {
                // add as a singleton so only one ever will be created.
                services.AddSingleton<IDataService, MockDataService>();
            }
            else
            {
                services.AddSingleton<IDataService, MyDataService>();
            }

            // add the ViewModel, but as a Transient, which means it will create a new one each time.
            services.AddTransient<MyViewModel>();

            //Another thing we can do is access variables from that json file
            var world = ctx.Configuration["Hello"];
        }
    }
}
