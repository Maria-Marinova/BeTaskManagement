using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeTaskManagement.Models
{
    public class User
    {
        [Key]
        public int UserId {  get; set; } // PK, Auto-increment
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }

        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";
        public virtual ICollection<BeTask> BeTasks { get; set; }
    }
}
