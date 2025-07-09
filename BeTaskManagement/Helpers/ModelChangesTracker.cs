using BeTaskManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BeTaskManagement.Helpers
{
    public static class ModelChangesTracker
    {
        public static List<CommentHistory> TrackCommentChanges(Comment oldComment, Comment newComment, out bool isCommentChanged)
        {
            var audits = new List<CommentHistory>();

            var properties = typeof(Comment).GetProperties()
                .Where(p => p.Name != nameof(Comment.CommentId) &&
                            p.Name != nameof(Comment.BeTask) &&
                            p.Name != nameof(Comment.History) &&
                            p.Name != nameof(Comment.TaskId));

            foreach (var prop in properties)
            {
                var originalValue = prop.GetValue(oldComment)?.ToString() ?? "";
                var updatedValue = prop.GetValue(newComment)?.ToString() ?? "";

                if (originalValue != updatedValue)
                {
                    audits.Add(new CommentHistory
                    {
                        CommentId = oldComment.CommentId,
                        ChangedProperty = prop.Name,
                        OldText = originalValue,
                        NewText = updatedValue,
                        ModifiedOn = DateTime.Now
                    });
                }
            }

            isCommentChanged = audits.Count() > 0;

            return audits;
        }
    }
}
