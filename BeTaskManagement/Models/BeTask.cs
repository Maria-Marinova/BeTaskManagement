using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeTaskManagement.Models.Enums;

namespace BeTaskManagement.Models
{
    public class BeTask
    {
        [Key]
        public int BeTaskId {  get; set; } // PK, Auto-increment
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime? DueDate { get; set; }
        public DateTime? NextActionDate { get; set; }
        public BeTaskStatus Status { get; set; }
        public BeTaskType Type { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public int? AssignedToUserId { get; set; }
        public virtual User AssignedTo { get; set; }

    }
}
