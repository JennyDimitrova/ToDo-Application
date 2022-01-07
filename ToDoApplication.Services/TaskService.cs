using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToDoApplication.Data;
using ToDoApplication.Domain;

namespace ToDoApplication.Services
{
    public class TaskService
    {
        private readonly ToDoAppContext _database;

        public TaskService(ToDoAppContext database)
        {
            _database = database;
        }

        public string CreateTask(User user)
        {
            Console.WriteLine("Enter task title");
            string title = Console.ReadLine();
            Console.WriteLine("Enter task description");
            string description = Console.ReadLine();
            Console.WriteLine("Enter ToDoList id");
            int listId = int.Parse(Console.ReadLine());
            var newTask = new Task()
            {
                ListId = listId,
                Title = title,
                Description = description,
                CreatorID = user.ID
            };
            _database.Tasks.Add(newTask);
            _database.SaveChanges();
            return "Task succedssfully added to ToDo-List";
        }

        public string EditTask(User user)
        {
            Console.WriteLine("Enter task id");
            int id = int.Parse(Console.ReadLine());

            try
            {
                var taskToUpdate = _database.Tasks.FirstOrDefault(t => t.ID == id);

                Console.WriteLine("Enter new title:");
                string newTitle = Console.ReadLine();

                Console.WriteLine("Enter true or false for task completion:");
                string complete = Console.ReadLine();

                taskToUpdate.EditedAt = DateTime.Now;
                taskToUpdate.EditorID = user.ID;
                _database.SaveChanges();

                return "Task edited successfully!";
            }
            catch (NullReferenceException)
            {
                return $"Task with id:{id} wasn't found!";
            }
        }

        public string DeleteTask()
        {
            Console.WriteLine("Enter task id");
            int id = int.Parse(Console.ReadLine());
            var taskToDelete = _database.Tasks.FirstOrDefault(t => t.ID == id);
            _database.Tasks.Remove(taskToDelete);
            _database.SaveChanges();
            return $"Task with id: {id} successfully deleted";
        }

        public string AssignTask()
        {
            Console.WriteLine("Enter user id");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter task id");
            var taskID = int.Parse(Console.ReadLine());
            var task = _database.Tasks.FirstOrDefault(t => t.ID == taskID);
            var user = _database.Users.FirstOrDefault(u => u.ID == id);
            user.Tasks.Add(task);
            _database.SaveChanges();
            return $"Task with id {taskID} is successfully assigned to user {user.Username}.";
        }
        public string CompleteTask()
        {
            Console.WriteLine("Enter task id");
            int id = int.Parse(Console.ReadLine());
            var taskToUpdate = _database.Tasks.FirstOrDefault(t => t.ID == id);
            taskToUpdate.IsComplete = true;
            _database.Tasks.Update(taskToUpdate);
            return $"Task with id {id} successfully completed.";
        }

        public string ListAllTasks()
        {
            var sb = new StringBuilder();
            var res = _database.Tasks.ToList();
            res.ForEach(t => sb.Append(t.ToString()).AppendLine());
            return sb.ToString();
        }

    }
}
