using System;
using System.Collections.Generic;
using ToDoApplication.Domain;

namespace ToDoApplication.Domain
{
    public class ToDoList : BaseEntity
    {
        public string Title { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
        public virtual ICollection<UsersToDos> UsersToDos { get; set; }

        public ToDoList()
        {
            Users = new List<User>();
            Tasks = new List<Task>();
            UsersToDos = new List<UsersToDos>();
            Created = DateTime.Now;
            EditedAt = DateTime.Now;
        }

        public override string ToString()
        {
            return $"{ID} {Title}";
        }
    }
}
