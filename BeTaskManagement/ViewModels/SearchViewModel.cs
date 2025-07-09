using BeTaskManagement.Base;
using BeTaskManagement.Data;
using BeTaskManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BeTaskManagement.ViewModels
{
    public class SearchViewModel : ViewModelBase
    {
        private readonly AppDbContext _dbContext;

        public ObservableCollection<Comment> AllComments { get; set; } = new ObservableCollection<Comment>();
        public ObservableCollection<Comment> FilteredComments { get; set; } = new ObservableCollection<Comment>();

        private string _searchText = string.Empty;
        public string SearchText
        {
            get => _searchText;
            set
            {
                if (SetProperty(ref _searchText, value))
                {
                    FilterComments();
                }
            }
        }

        private Comment _selectedComment;
        public Comment SelectedComment
        {
            get => _selectedComment;
            set => SetProperty(ref _selectedComment, value);
        }

        public ICommand OpenSelectedTaskCommand { get; }

        public SearchViewModel(AppDbContext dbContext)
        {
            _dbContext = dbContext;

            // Load all comments with their tasks
            var comments = _dbContext.Comments
                .Include(c => c.BeTask)
                .ToList();

            foreach (var comment in comments)
                AllComments.Add(comment);

            FilterComments();

            OpenSelectedTaskCommand = new Helpers.RelayCommand(_ => OpenSelectedTask(), _ => SelectedComment != null);
        }

        private void FilterComments()
        {
            FilteredComments.Clear();

            if (string.IsNullOrWhiteSpace(SearchText))
            {
                foreach (var c in AllComments)
                    FilteredComments.Add(c);
            }
            else
            {
                var lowerSearch = SearchText.ToLower();

                var filtered = AllComments.Where(c =>
                    (c.CommentText != null && c.CommentText.ToLower().Contains(lowerSearch)) ||
                    (c.BeTask != null && c.BeTask.Name.ToLower().Contains(lowerSearch)) ||
                    c.CommentType.ToString().ToLower().Contains(lowerSearch) ||
                    c.DateAdded.ToString("dd/MM/yyyy HH:mm").Contains(lowerSearch) ||
                    (c.ReminderDate.HasValue && c.ReminderDate.Value.ToString("dd/MM/yyyy HH:mm").Contains(lowerSearch))
                );

                foreach (var c in filtered)
                    FilteredComments.Add(c);
            }
        }

        private void OpenSelectedTask()
        {
            if (SelectedComment?.BeTask == null)
                return;

            var taskViewModel = new BeTaskViewModel(_dbContext, SelectedComment.BeTask.BeTaskId);
            var taskView = new Views.BeTaskView
            {
                DataContext = taskViewModel
            };
            taskView.ShowDialog();
        }
    }
}
