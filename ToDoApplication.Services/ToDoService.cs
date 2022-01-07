using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToDoApplication.Data;
using ToDoApplication.Domain;

namespace ToDoApplication.Services
{

    public class ToDoService
    {
        private readonly ToDoAppContext _database;
        
        public ToDoService(ToDoAppContext database)
        {
            _database = database;
        }
        public string CreateToDoList(User Creator)
        {
            Console.WriteLine("Enter title:");
            string title = Console.ReadLine();
            ToDoList list = new()
            {
                Title = title,
                CreatorID = Creator.ID,
                EditorID = Creator.ID
            };

            _database.ToDoLists.Add(list);
            _database.SaveChanges();
            return "List created successfully";
        }

        public string EditToDoList(User Editor)
        {
            Console.WriteLine("Enter ID of the list");
            int id = int.Parse(Console.ReadLine());

            try
            {
                ToDoList listToEdit = _database.ToDoLists.FirstOrDefault(l => l.ID == id);
                Console.WriteLine("Enter new title: ");
                string newTitle = Console.ReadLine();
                listToEdit.Title = newTitle;

                listToEdit.EditedAt = DateTime.Now;
                listToEdit.EditorID = Editor.ID;

                _database.SaveChanges();                
                return "List edited successfully!";
            }
            catch (NullReferenceException)
            {
                return "No such list in database";
            }
        }

        public string DeleteToDoList(User user)
        {
            Console.WriteLine("Enter ID of the list");
            int id = int.Parse(Console.ReadLine());

            try
            {
                var listToBeRemoved = _database.ToDoLists.FirstOrDefault(l => l.ID == id);

                if (user.ID != listToBeRemoved.CreatorID)
                {
                    return "Only owner has permissions to delete this list. You deleted it from your shared lists.";
                }

                if (_database.Tasks.Where(t => t.ListId == id).ToList().Count != 0)
                {
                    return "You have to remove the tasks first.";
                }

                _database.ToDoLists.Remove(listToBeRemoved);
                _database.SaveChanges();
                return "List deleted successfully";
            }
            catch (NullReferenceException)
            {
                return $"List with ID = {id} doesn't exist";
            }
        }

        public string SelectAllListsByUserId(User user)
        {
            var sb = new StringBuilder();

            List<int> list = (List<int>)_database.UsersToDos.Select(u => u.UsersID);
            List<ToDoList> userToDos = (List<ToDoList>)_database.ToDoLists.Where(l => list.Contains(l.ID));
            userToDos.ForEach(t => sb.Append(t.ToString()).AppendLine());
            return sb.ToString();
        }

        public string ListAllToDoLists()
        {
            var sb = new StringBuilder();
            var res = _database.ToDoLists.ToList();
            res.ForEach(l => sb.Append(l.ToString()).AppendLine());
            return sb.ToString();
        }

        public string ShareList(User owner)
        {
            Console.WriteLine("Write id of the user you want to share list with:");
            int receiver = int.Parse(Console.ReadLine());
            Console.WriteLine("Write id of the list you want to share");
            int listId = int.Parse(Console.ReadLine());
            try
            {
                var userReceiver = _database.Users.FirstOrDefault(u => u.ID == receiver);
                var list = _database.ToDoLists.FirstOrDefault(l => l.ID == listId);
                userReceiver.Lists.Add(list);
                _database.SaveChanges();
            }
            catch (Exception)
            {
                Console.WriteLine("Wrong input");
                ShareList(owner);
            }
            return "List successfully shared with user";
        }

        public string GetAllTasksByListId()
        {
            Console.WriteLine("Enter list ID");
            var sb = new StringBuilder();
            var res = new List<Task>();
            try
            {
                int listId = int.Parse(Console.ReadLine());
                res = _database.Tasks.Where(t => t.ListId == listId).ToList();
            }
            catch (Exception)
            {
                Console.WriteLine("Wrong input");
                GetAllTasksByListId();
            }
            res.ForEach(t => sb.Append(t.ToString()).AppendLine());
            return sb.ToString();
        }
    }
}
