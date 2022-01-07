using System;
using System.Collections.Generic;
using ToDoApplication.Domain;

namespace ToDoApplication
{
    public class User : BaseEntity
    {
       
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public virtual ICollection<ToDoList> Lists { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
        public virtual ICollection<UsersTasks> UsersTasks { get; set; }
        public virtual ICollection<UsersToDos> UsersToDos { get; set; }
        public User()
        {
            Created = DateTime.Now;
            EditedAt = DateTime.Now;
            Lists = new List<ToDoList>();
            Tasks = new List<Task>();
            UsersTasks = new List<UsersTasks>();
            UsersToDos = new List<UsersToDos>();
        }

        public override string ToString()
        {
            return $"{ID} {Username} {FirstName} {LastName}";
        }
    }
}
