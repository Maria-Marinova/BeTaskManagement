using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeTaskManagement.Events
{
    public static class AppEventAggregator
    {
        public static event Action TaskSaved;

        public static void RaiseTaskSaved()
        {
            TaskSaved?.Invoke();
        }

        public static event Action CommentSaved;

        public static void RaiseCommentSaved()
        {
            CommentSaved?.Invoke();
        }
    }
}
