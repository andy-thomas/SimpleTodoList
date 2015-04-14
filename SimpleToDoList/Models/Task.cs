using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

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