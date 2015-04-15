using System.Runtime.Serialization;

namespace SimpleToDoList.Models
{
    [DataContract]
    public class Task
    {
        public Task(string title, bool finished)
        {
            Title = title;
            Finished = finished;
        }

        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public bool Finished { get; set; }
    }
}