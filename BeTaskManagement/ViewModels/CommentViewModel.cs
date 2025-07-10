using BeTaskManagement.Base;
using BeTaskManagement.Data;
using BeTaskManagement.Helpers;
using BeTaskManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using BeTaskManagement.Models.Enums;
using Microsoft.EntityFrameworkCore;
using BeTaskManagement.Events;

namespace BeTaskManagement.ViewModels
{
    public class CommentViewModel : ViewModelBase
    {
        private readonly AppDbContext _dbContext;
        private readonly bool _isEditMode;

        public int TaskId { get; set; }
        public string CommentText { get; set; }
        public CommentType CommentType { get; set; }
        public DateTime? ReminderDate { get; set; }

        private Comment _editingComment;

        bool isCommentChanged;

        List<CommentHistory> commentHistoryChanges;

        public ICommand SaveCommand => new RelayCommand(_ => Save());

        public CommentViewModel(AppDbContext dbContext, Comment commentToEdit, int taskId)
        {
            _dbContext = dbContext;
            TaskId = taskId;

            if (commentToEdit != null)
            {
                _isEditMode = true;
                _editingComment = commentToEdit;
                CommentText = commentToEdit.CommentText;
                CommentType = commentToEdit.CommentType;
                ReminderDate = commentToEdit.ReminderDate;
            }
        }

        private void Save()
        {
            if (_isEditMode)
            {
                _editingComment.CommentText = CommentText;
                _editingComment.CommentType = CommentType;
                _editingComment.ReminderDate = ReminderDate;
                _dbContext.Comments.Update(_editingComment);
                var originalComment = _dbContext.Comments
                    .AsNoTracking()
                    .FirstOrDefault(c => c.CommentId == _editingComment.CommentId);
                commentHistoryChanges = ModelChangesTracker.TrackCommentChanges(originalComment, _editingComment, out isCommentChanged);
                if (isCommentChanged)
                {
                    _dbContext.CommentHistories.AddRange(commentHistoryChanges);
                }
            }
            else
            {
                var newComment = new Comment
                {
                    CommentText = CommentText,
                    CommentType = CommentType,
                    ReminderDate = ReminderDate,
                    TaskId = TaskId
                };
                _dbContext.Comments.Add(newComment);
            }

            _dbContext.SaveChanges();
            //AppEventAggregator.RaiseCommentSaved();
            CloseWindow();
        }

        private void CloseWindow()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.DataContext == this)
                {
                    window.DialogResult = true;
                    window.Close();
                    break;
                }
            }
        }
    }
}
