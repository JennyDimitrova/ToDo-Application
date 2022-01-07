using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToDoApplication.Data;

namespace ToDoApplication.Services
{
	public class UserService
	{
		private readonly ToDoAppContext _database;

		private List<User> Users = new();
		public User CurrentUser { get; set; }

		public UserService(ToDoAppContext database)
		{
			_database = database;
			List<User> usersFromBase = _database.Users.ToList();

            if (usersFromBase.Count == 0)
            {
                CreateFirstUser();
            }
            else
            {
                Users = usersFromBase;
            }
        }

        private void CreateFirstUser()
        {
			_database.Users
				.Add(new User()
				{ Username = "admin",
				  Password = "adminpassword",
				  Role = "ADMIN",
				  FirstName = "",
				  LastName = ""
				});
			_database.SaveChanges();
		}

		public string CreateUser()
		{
			Console.WriteLine("Enter username:");
			string username = Console.ReadLine();
			Console.WriteLine("Enter password:");
			string password = Console.ReadLine();
			Console.WriteLine("Enter first name:");
			string fName = Console.ReadLine();
			Console.WriteLine("Enter last name:");
			string lName = Console.ReadLine();
			Console.WriteLine("Enter role: ADMIN or USER");
			string role = Console.ReadLine().ToUpper();
			if (_database.Users.Any(u => u.Username == username))
			{
				return "User already exists";
			}
            else
            {    
                _database.Users.Add(new User()
                    {
                        Username = username,
                        Password = password,
                        FirstName = fName,
                        LastName = lName,
                        Role = role,
                        CreatorID = CurrentUser.ID,
                        EditorID = CurrentUser.ID
                    });
				_database.SaveChanges();
				return $"User {username} created successfully";
            }
        }

		public string EditUser(int id)
		{
			try
			{
				User userToEdit = _database.Users.FirstOrDefault(u => u.ID == id);
				Users.Remove(userToEdit);

				Console.WriteLine("Enter username: ");
				string username = Console.ReadLine();
				userToEdit.Username = username;

				Console.WriteLine("Enter password: ");
				string password = Console.ReadLine();
				userToEdit.Password = password;

				Console.WriteLine("Enter first name:");
				string fName = Console.ReadLine();
				userToEdit.FirstName = fName;

				Console.WriteLine("Enter last name:");
				string lName = Console.ReadLine();
				userToEdit.LastName = lName;
				
				userToEdit.EditedAt = DateTime.Now;
				userToEdit.EditorID = CurrentUser.ID;

				_database.SaveChanges();
				Users.Add(userToEdit);
				return "User updated successfully";
			}
			catch (ArgumentNullException)
			{
				return "No such user in database";
			}
		}

		public string DeleteUser(int id)
		{
            if (CurrentUser.ID == id)
            {
				return "You cannot delete yourself.";
            }
			if (_database.Tasks.Where(t => t.CreatorID == id).ToList().Count != 0)
			{
				return "This user has tasks. Delete them first.";
			}

			if (_database.ToDoLists.Where(l => l.CreatorID == id).ToList().Count != 0)
			{
			return "This user has lists. Delete them first.";		
            }

			try
			{
				var user = _database.Users.Find(id);//?
				_database.Users.Remove(user);
				Users.Remove(user);
				_database.SaveChanges();
				return "User deleted successfully";
			}
			catch (ArgumentNullException)
			{
				return $"User with ID = {id} doesn't exist";
			}
		}

		public string ListAllUsers()
		{
			StringBuilder sb = new();
			List<User> users = _database.Users.ToList();
			users.ForEach(u => sb.Append(u.ToString()).AppendLine());
			return sb.ToString();
		}

		public void Login()//TODO check if needed to change to: CurrentUser = _database.GetUserByName(userName);
		{
			string username;
			string password;

			while (true)
            {
				Console.WriteLine("Enter username: ");
				username = Console.ReadLine();
				
				if (_database.Users.FirstOrDefault(u => u.Username == username)  != null)
				{
				break;
				}
				Console.WriteLine("Username not found! Try again!");
			}

			CurrentUser = _database.Users.First(u => u.Username == username);

			while (true)
            {
				Console.WriteLine("Enter password: ");
				password = Console.ReadLine();
				if (CurrentUser.Password == password)
				{
					break;
				}
				Console.WriteLine("Wrong password! Try again!");
			}
			Console.WriteLine("Login successful!");
		}

		public void Logout()
		{
			CurrentUser = null;
		}
	}
}
