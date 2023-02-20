using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using System.Web.Cors;
using System.Web.Hosting;
using System.Web.Routing;
using Edis.Fenyites.Controllers.Base;
using Edis.Fenyites.Hubs;
using Edis.Functions.JFK;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;

[assembly: OwinStartup(typeof(Edis.Fenyites.Startup))]
namespace Edis.Fenyites
{
    public class Startup
    {        
        public void Configuration(IAppBuilder app)
        {
            app.Map("/signalr", map =>
            {                
                map.UseCors(CorsOptions.AllowAll);                
                var hubConfiguration = new HubConfiguration { };
                hubConfiguration.EnableDetailedErrors = true;
                map.RunSignalR(hubConfiguration);
                
            });            
            FenyitesDashboardFunctions FenyitesDashboardFunctions = new FenyitesDashboardFunctions();
            //FenyitesDashboardFunctions.Init();            
            AddStaticFolder(app, @"UI\dist", "/app");
            AddVirtualStaticFolder(app, "~/KozosKepek", "/kozoskepek");
            AddStaticFolder(app, "www", "/AppTest");            
        }

        public void AddStaticFolder(IAppBuilder app, string folder, string requestPath)
        {
            string root = AppDomain.CurrentDomain.BaseDirectory;            
            
            var physicalFileSystem = new PhysicalFileSystem(Path.Combine(root, folder));
            var options = new FileServerOptions
            {
                RequestPath = new PathString(requestPath),
                EnableDefaultFiles = true,
                EnableDirectoryBrowsing = false,
                FileSystem = physicalFileSystem,
                StaticFileOptions = { ContentTypeProvider = new CustomContentTypeProvider() },

            };
            options.StaticFileOptions.FileSystem = physicalFileSystem;
            options.StaticFileOptions.ServeUnknownFileTypes = false;
            //GlobalHost.HubPipeline.AddModule(new ErrorHandlingPipelineModule());
            
            app.UseFileServer(options);           
        }

        public void AddVirtualStaticFolder(IAppBuilder app, string folder, string requestPath)
        {
            string root = AppDomain.CurrentDomain.BaseDirectory;            

            var physicalFileSystem =new  PhysicalFileSystem (HostingEnvironment.MapPath(folder));
            var options = new FileServerOptions
            {
                RequestPath = new PathString(requestPath),
                EnableDefaultFiles = true,
                EnableDirectoryBrowsing = false,
                FileSystem = physicalFileSystem,
                StaticFileOptions = { ContentTypeProvider = new CustomContentTypeProvider() },

            };
            options.StaticFileOptions.FileSystem = physicalFileSystem;
            options.StaticFileOptions.ServeUnknownFileTypes = false;
            //GlobalHost.HubPipeline.AddModule(new ErrorHandlingPipelineModule());

            app.UseFileServer(options);
        }
    }

}