using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeTaskManagement.Models
{
    public class CommentHistory
    {
        public int CommentHistoryId { get; set; } // PK
        public int CommentId { get; set; }
        public string ChangedProperty { get; set; }

        public string OldText { get; set; }
        public string NewText { get; set; }

        public DateTime ModifiedOn { get; set; } = DateTime.Now;

        public virtual Comment Comment { get; set; }
    }
}
