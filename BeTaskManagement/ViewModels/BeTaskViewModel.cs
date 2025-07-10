using BeTaskManagement.Base;
using BeTaskManagement.Data;
using BeTaskManagement.Events;
using BeTaskManagement.Helpers;
using BeTaskManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BeTaskManagement.ViewModels
{
    public class BeTaskViewModel : ViewModelBase
    {
        private readonly AppDbContext _dbContext;
        public BeTask Task { get; set; } = new BeTask();
        public ObservableCollection<Comment> Comments { get; set; } = new ObservableCollection<Comment>();
        public ObservableCollection<User> Users { get; set; } = new ObservableCollection<User>();

        public ICommand SaveTaskCommand => new RelayCommand(execute => SaveTask());
        public ICommand AddCommentCommand => new RelayCommand(execute => AddComment());
        public ICommand DeleteCommentCommand => new RelayCommand(execute => DeleteComment());

        

        private Comment _selectedComment;
        public Comment SelectedComment
        {
            get => _selectedComment;
            set { _selectedComment = value; OnPropertyChanged(nameof(SelectedComment)); }
        }

        private User _selectedUser;
        public User SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;
                Task.AssignedToUserId = _selectedUser?.UserId;
                OnPropertyChanged(nameof(SelectedUser));
            }
        }

        public BeTaskViewModel(AppDbContext db)
        {
            _dbContext = db;
            LoadUsers();
        }

        public BeTaskViewModel(AppDbContext db, int taskId)
        {
            _dbContext = db;
            LoadTask(taskId);
        }

        public void LoadTask(int taskId)
        {
            Task = _dbContext.BeTasks
                .Include(t => t.AssignedTo)
                .Include(t => t.Comments)
                .FirstOrDefault(t => t.BeTaskId == taskId) ?? new BeTask();
            Comments = new ObservableCollection<Comment>(Task.Comments ?? new List<Comment>());
            OnPropertyChanged(nameof(Task));
        }

        private async void SaveTask()
        {
            if (Task.BeTaskId == 0)
            {
                Task.Comments = Comments;
                Task.CreatedOn = DateTime.Now;
                Task.NextActionDate = Comments
                    .Where(c => c.ReminderDate.HasValue)
                    .OrderBy(c => c.ReminderDate)
                    .Select(c => c.ReminderDate)
                    .FirstOrDefault();

                _dbContext.BeTasks.Add(Task);
            }
            else
            {
                var dbTask = _dbContext.BeTasks.Include("Comments").FirstOrDefault(t => t.BeTaskId == Task.BeTaskId);
                if (dbTask != null)
                {
                    dbTask.Description = Task.Description;
                    dbTask.DueDate = Task.DueDate;
                    dbTask.Status = Task.Status;
                    dbTask.Type = Task.Type;
                    dbTask.AssignedTo = Task.AssignedTo;
                    dbTask.NextActionDate = Comments
                        .Where(c => c.ReminderDate.HasValue)
                        .OrderBy(c => c.ReminderDate)
                        .Select(c => c.ReminderDate)
                        .FirstOrDefault();

                    _dbContext.Comments.RemoveRange(dbTask.Comments);
                    dbTask.Comments = Comments;
                }
            }

            await _dbContext.SaveChangesAsync();
            AppEventAggregator.RaiseTaskSaved();
            CloseWindow();
        }

        private void AddComment()
        {
            Comment newComment = new Comment { TaskId = Task.BeTaskId };
            Comments.Add(newComment);
            SelectedComment = newComment;
        }

        private void DeleteComment()
        {
            if (SelectedComment != null)
            {
                Comments.Remove(SelectedComment);
                SelectedComment = null;
            }
        }

        private void LoadUsers()
        {
            var userList = _dbContext.Users.ToList();
            Users = new ObservableCollection<User>(userList);
            OnPropertyChanged(nameof(Users));
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
