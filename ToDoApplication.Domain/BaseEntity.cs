using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoApplication.Domain
{
    public abstract class BaseEntity
    {
        
        public int ID { get; set; }
        public DateTime Created { get; set; }
        public int CreatorID { get; set; }
        public DateTime EditedAt { get; set; }
        public int EditorID { get; set; }
    }
}
