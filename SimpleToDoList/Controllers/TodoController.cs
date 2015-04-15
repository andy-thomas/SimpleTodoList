using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SimpleToDoList.Hubs;
using SimpleToDoList.Models;

namespace SimpleToDoList.Controllers
{
    // see Brad Wilson - Microsoft’s Modern Web Stack, Starring ASP.NET Web API   
    // https://vimeo.com/43603472

    public class TodoController : ApiController
    {
        public static IList<Task> TaskList = new List<Task>()
                       {
                           new Task("Fifth task", true),
                           new Task("Sixth task", false),
                           new Task("Seventh task", true)
                       };

        //public TodoHub hub = new TodoHub();

        public IEnumerable<Task> GetToDoItems()
        {
            return TaskList;
        }

        [HttpPost]
        public HttpResponseMessage PostCreateTask([FromBody]Task task)
        {
            // Add the item to the repo
            TaskList.Add(task);

            // Notify the connected items
            TodoHub.AddItem(task);

            // Return the new item inside a 201 response
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, new { Title = task.Title, Finished = task.Finished});
            string link = Url.Link("DefaultApi", new { controller = "Todo" });
            response.Headers.Location = new Uri(link);
            return response;
        }

        [HttpDelete]
        public void DeleteTask(Task task)
        {
            if (task == null || string.IsNullOrEmpty(task.Title))
                return;

            Task item = TaskList.FirstOrDefault(t => t.Title == task.Title);

            if (item == null)
            {
                throw new HttpResponseException(
                    Request.CreateResponse(HttpStatusCode.NotFound));
            }

            TaskList.Remove(item);

            // Notify the connected items
            TodoHub.DeleteItem(task);
        }

        [HttpPut]
        public Task PutUpdateTask(Task task)
        {
            if (task == null || string.IsNullOrEmpty(task.Title))
                return null;

            Task item = TaskList.FirstOrDefault(t => t.Title == task.Title);

            if (item == null)
            {
                throw new HttpResponseException(
                    Request.CreateResponse(HttpStatusCode.NotFound));
            }

            item.Finished = task.Finished;

            // Notify the connected items
            TodoHub.UpdateItem(task);

            return item;
        }
    }
}
