using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoApplication.Domain
{
    public class Task : BaseEntity
    {
        public virtual int ListId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsComplete { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<UsersTasks> UsersTasks { get; set; }

        public Task()
        {
            Users = new List<User>();
            UsersTasks = new List<UsersTasks>();
            IsComplete = false;
            Created = DateTime.Now;
            EditedAt = DateTime.Now;
        }

        public override string ToString()
        {
            return string.Format("Task {0} with id {1} is {2}competed.", Title, ID, IsComplete ? "" : "not " ) ;
        }
    }
}
