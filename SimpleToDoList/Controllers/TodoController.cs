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
        public static IList<Task> TaskList = new List<Task>
                                                 {
                           new Task(Guid.NewGuid(), "Fifth task", new DateTime(2015,5,5).ToUniversalTime(), true),
                           new Task(Guid.NewGuid(), "Sixth task", DateTime.Today.AddDays(15).ToUniversalTime(), false),
                           new Task(Guid.NewGuid(), "Seventh task", DateTime.Today.AddDays(7).ToUniversalTime(), true)
                       };

        //public TodoHub hub = new TodoHub();

        public IEnumerable<Task> GetToDoItems()
        {
            return TaskList;
        }

        [HttpPost]
        public HttpResponseMessage PostCreateTask([FromBody]Task task)
        {
            // Generate a new id
            task.Id = Guid.NewGuid();

            // Add the item to the repo
            TaskList.Add(task);

            // Notify the connected items
            TodoHub.AddItem(task);

            // Return the new item, complete with the new Id, inside a 201 response
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, new { Id = task.Id, Title = task.Title, Finished = task.Finished });
            string link = Url.Link("DefaultApi", new { controller = "Todo" });
            response.Headers.Location = new Uri(link);
            return response;
        }

        [HttpDelete]
        public void DeleteTask(Task taskParam)
        {
            Task task = TaskList.FirstOrDefault(t => t.Id == taskParam.Id);

            if (task == null)
            {
                throw new HttpResponseException(
                    Request.CreateResponse(HttpStatusCode.NotFound));
            }

            TaskList.Remove(task);

            // Notify the connected items
            TodoHub.DeleteItem(task);
        }

        [HttpPut]
        public Task PutUpdateTask(Task task)
        {
            if (task == null || string.IsNullOrEmpty(task.Title))
                return null;

            Task item = TaskList.FirstOrDefault(t => t.Id == task.Id);

            if (item == null)
            {
                throw new HttpResponseException(
                    Request.CreateResponse(HttpStatusCode.NotFound));
            }

            item.Finished = task.Finished;
            item.Title = task.Title.Trim();
            item.DueDate = task.DueDate;

            // Notify the connected items
            TodoHub.UpdateItem(task);

            return item;
        }
    }
}
