using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeTaskManagement.Models.Enums;

namespace BeTaskManagement.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; } // PK, Auto-increment
        public int TaskId { get; set; }
        public string CommentText { get; set; }
        public CommentType CommentType { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.Now;
        public DateTime? ReminderDate { get; set; }
        public virtual BeTask BeTask { get; set; }
        public virtual ICollection<CommentHistory> History { get; set; } = new List<CommentHistory>();
    }
}
