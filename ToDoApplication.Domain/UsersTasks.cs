using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApplication.Domain
{
    public class UsersTasks
    {
        public int ID;
        public int UsersID { get; set; }
        public virtual User User { get; set; }
        public int TasksID { get; set; }
        public virtual Task Task { get; set; }
    }
}
