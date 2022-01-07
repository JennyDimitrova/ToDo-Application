using Microsoft.Extensions.Configuration;
using System;
using ToDoApplication.Data;
using ToDoApplication.Services;

namespace ToDoApplication.UI
{
    class Program
    {
        private static ToDoAppContext _database;
        private static UserService _userService;
        private static ToDoService _toDoService;
        private static TaskService _taskService;
       
        static void Main(string[] args)
        {
            Start();
        }

        private static void Start()
        {
            InitApp();

            Console.WriteLine("Welcome to the TODO Managment System!");
            if (_userService.CurrentUser == null)
            {
                _userService.Login();
                Console.WriteLine("============================================");
            }
            else
            {
                RenderOptions(_userService.CurrentUser);
            }
            User CurrentUser = _userService.CurrentUser;
            RenderOptions(CurrentUser);
        }

        static void InitApp()
        {
            _database = new ToDoAppContext();
            _database.Database.EnsureCreated();

            _userService = new UserService(_database);
            _toDoService = new ToDoService(_database);
            _taskService = new TaskService(_database);

        }

        private static void RenderOptions(User CurrentUser)
        {
            if (CurrentUser.Role == "ADMIN")
            {
                Console.WriteLine("Pick up Option 1 for Users managements and Option 2 for ToDo List management:");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        RenderAdminManagement(CurrentUser);
                        break;
                    case "2":
                        RenderToDoManagement(CurrentUser);
                        break;
                    default:
                        Console.WriteLine("Try again");
                        RenderOptions(CurrentUser);
                        break;
                }
            }
            else
            {
                RenderToDoManagement(CurrentUser);
            }
        }

        private static void RenderAdminManagement(User user)
        {
            Console.WriteLine("Pick up a number of an option:");
            Console.WriteLine("1. Show all users");
            Console.WriteLine("2. Create new user");
            Console.WriteLine("3. Edit a user");
            Console.WriteLine("4. Delete a user");
            Console.WriteLine("5. Logout");
            Console.WriteLine("6. Return to start navigation");
            string input = Console.ReadLine();
            Console.WriteLine("============================================");
            switch (input)
            {
                case "1":
                    Console.WriteLine(_userService.ListAllUsers());
                    break;
                case "2":
                    Console.WriteLine(_userService.CreateUser());
                    Console.WriteLine("============================================");
                    RenderAdminManagement(user);
                    break;
                case "3":
                    Console.WriteLine("Enter user ID:");
                    int userID = int.Parse(Console.ReadLine());
                    Console.WriteLine(_userService.EditUser(userID));
                    Console.WriteLine("============================================");
                    RenderAdminManagement(user);
                    break;
                case "4":
                    Console.WriteLine("Enter user ID:");
                    int id = int.Parse(Console.ReadLine());
                    Console.WriteLine(_userService.DeleteUser(id));
                    Console.WriteLine("============================================");
                    RenderAdminManagement(user);
                    break;
                case "5":
                    _userService.Logout();
                    Console.WriteLine("============================================");
                    Start();
                    break;
                case "6":
                    RenderOptions(user);
                    Console.WriteLine("============================================");
                    break;
                default:
                    RenderAdminManagement(user);
                    break;
            }
            Console.WriteLine("============================================");
            RenderAdminManagement(user);
        }

        private static void RenderToDoManagement(User user)
        {
            Console.WriteLine("Pick up a number of an option:");
            Console.WriteLine("7. Show all To-Do Lists Created and Shared with you");
            Console.WriteLine("8. Show all To-Do Lists");
            Console.WriteLine("9. Create a To-Do List");
            Console.WriteLine("10. Edit a To-Do List");
            Console.WriteLine("11. Delete a To-Do List");
            Console.WriteLine("12. Share list with other users");
            Console.WriteLine("13. Show all tasks of a list");
            Console.WriteLine("14. Manage tasks");
            Console.WriteLine("15. Return to start navigation");
            Console.WriteLine("16. Logout");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "7":
                    Console.WriteLine(_toDoService.SelectAllListsByUserId(user));
                    Console.WriteLine("============================================");
                    RenderToDoManagement(user);
                    break;
                case "8":
                    Console.WriteLine(_toDoService.ListAllToDoLists());
                    Console.WriteLine("============================================");
                    RenderToDoManagement(user);
                    break;
                case "9":
                    Console.WriteLine(_toDoService.CreateToDoList(user));
                    Console.WriteLine("============================================");
                    RenderToDoManagement(user);
                    break;
                case "10":
                    Console.WriteLine(_toDoService.EditToDoList(user));
                    Console.WriteLine("============================================");
                    RenderToDoManagement(user);
                    break;
                case "11":
                    Console.WriteLine(_toDoService.DeleteToDoList(user));
                    Console.WriteLine("============================================");
                    RenderToDoManagement(user);
                    break;
                case "12":
                    Console.WriteLine(_toDoService.ShareList(user));
                    Console.WriteLine("============================================");
                    RenderToDoManagement(user);
                    break;
                case "13":
                    Console.WriteLine(_toDoService.GetAllTasksByListId());
                    Console.WriteLine("============================================");
                    RenderToDoManagement(user);
                    break;
                case "14":
                    RenderTaskManagement(user);
                    Console.WriteLine("============================================");
                    break;
                case "15":
                    RenderOptions(user);
                    Console.WriteLine("============================================");
                    break;
                case "16":
                    _userService.Logout();
                    Console.WriteLine("============================================");
                    Start();
                    break;
                default:
                    Console.WriteLine("Try again");
                    RenderToDoManagement(user);
                    break;
            }
        }

        private static void RenderTaskManagement(User user)
        {
            Console.WriteLine("Pick up an option to manage tasks:");
            Console.WriteLine("17. Show all tasks");
            Console.WriteLine("18. Create a task");
            Console.WriteLine("19. Edit a task");
            Console.WriteLine("20. Delete a task");
            Console.WriteLine("21. Assign task");
            Console.WriteLine("22. Compete task");
            Console.WriteLine("23. Return to start navigation");
            Console.WriteLine("24. Logout");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "17":
                    Console.WriteLine(_taskService.ListAllTasks());
                    Console.WriteLine("============================================");
                    RenderTaskManagement(user);
                    break;
                case "18":
                    Console.WriteLine(_taskService.CreateTask(user));
                    Console.WriteLine("============================================");
                    RenderTaskManagement(user);
                    break;
                case "19":
                    Console.WriteLine(_taskService.EditTask(user));
                    Console.WriteLine("============================================");
                    RenderTaskManagement(user);
                    break;
                case "20":
                    Console.WriteLine(_taskService.DeleteTask());
                    Console.WriteLine("============================================");
                    RenderTaskManagement(user);
                    break;
                case "21":
                    Console.WriteLine(_taskService.AssignTask());
                    Console.WriteLine("============================================");
                    RenderTaskManagement(user);
                    break;
                case "22":
                    Console.WriteLine(_taskService.CompleteTask());
                    Console.WriteLine("============================================");
                    RenderTaskManagement(user);
                    break;
                case "23":
                    RenderOptions(user);
                    Console.WriteLine("============================================");
                    break;
                case "24":
                    _userService.Logout();
                    Console.WriteLine("============================================");
                    Start();
                    break;
                default:
                    Console.WriteLine("Try again");
                    RenderTaskManagement(user);
                    break;
            }
        }
    }
}
