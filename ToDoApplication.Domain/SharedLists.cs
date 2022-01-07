using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApplication.Domain
{
    public class SharedLists
    {
        public int Id { get; set; }
        public virtual int UserId { get; set; }
        public virtual int ToDoListId { get; set; }

        public SharedLists()
        {
        }
    }
    
}
