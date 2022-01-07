using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApplication.Domain
{
    public class AssignedTasks
    {
        public int Id { get; set; }
        public virtual int UserId { get; set; }
        public virtual int TaskId { get; set; }

        public AssignedTasks()
        {
        }
    }
}
