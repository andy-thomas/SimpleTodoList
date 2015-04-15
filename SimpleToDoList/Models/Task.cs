using System;
using System.Runtime.Serialization;

namespace SimpleToDoList.Models
{
    [DataContract]
    public class Task
    {
        public Task(Guid id, string title, DateTime dueDate, bool finished)
        {
            Id = id;
            Title = title;
            DueDate = dueDate;
            Finished = finished;
        }

        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public DateTime DueDate { get; set; }

        [DataMember]
        public bool Finished { get; set; }
    }
}