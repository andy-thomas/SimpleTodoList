using Microsoft.AspNet.SignalR;
using SimpleToDoList.Models;

namespace SimpleToDoList.Hubs
{
    public class TodoHub : Hub
    {
        public static void AddItem(Task task)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<TodoHub>();
            hubContext.Clients.All.addItem(task);
        }
        
        public static void DeleteItem(Task task)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<TodoHub>();
            hubContext.Clients.All.deleteItem(task);
        }     
   
        public static void UpdateItem(Task task)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<TodoHub>();
            hubContext.Clients.All.updateItem(task);
        }
    }
}