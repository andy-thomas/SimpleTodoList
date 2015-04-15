using Microsoft.Owin;
using Owin;

// Andy - This is required as part of SignalR - see the Readme.txt file
[assembly: OwinStartup(typeof(SimpleToDoList.Startup))]
namespace SimpleToDoList
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}