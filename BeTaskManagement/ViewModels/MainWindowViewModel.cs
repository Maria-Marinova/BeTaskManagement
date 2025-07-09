using BeTaskManagement.Base;
using BeTaskManagement.Data;
using BeTaskManagement.Helpers;
using BeTaskManagement.Models;
using BeTaskManagement.Views;
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
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly AppDbContext _dbContext;
        public ObservableCollection<BeTask> Tasks { get; set; } = new ObservableCollection<BeTask>();

        //BeTaskListViewModel taskListViewModel;

        public MainWindowViewModel(AppDbContext db)
        {
            _dbContext = db;
            //taskListViewModel = new BeTaskListViewModel(_dbContext);
            LoadTasks();
        }

        public ICommand OpenBeTaskViewCommand =>  new RelayCommand(execute => OpenBeTaskView());
        public ICommand AddCommentCommand => new RelayCommand(execute => AddComment(), _ => SelectedTask != null);
        public ICommand EditCommentCommand => new RelayCommand(comment => EditComment(comment as Comment), comment => comment is Comment);
        public ICommand DeleteCommentCommand => new RelayCommand(comment => DeleteComment(comment as Comment), comment => comment is Comment);
        public ICommand ShowCommentHistoryCommand => new RelayCommand(OpenCommentHistory);

        private BeTask _selectedTask;
        public BeTask SelectedTask
        {
            get => _selectedTask;
            set
            {
                _selectedTask = value;
                OnPropertyChanged(nameof(SelectedTask));
            }
        }
        private void OpenBeTaskView()
        {
            var viewModel = new BeTaskViewModel(_dbContext);

            var taskView = new BeTaskView
            {
                DataContext = viewModel
            };

            taskView.ShowDialog();
        }

        private void LoadTasks()
        {
            var taskList = _dbContext.BeTasks
                .Include(t => t.AssignedTo)
                .Include(t => t.Comments)
                .ThenInclude(c => c.History)
                .ToList();
            Tasks = new ObservableCollection<BeTask>(taskList);
            OnPropertyChanged(nameof(Tasks));
        }

        private void LoadCommentsForSelectedTask()
        {
            if (SelectedTask != null)
            {
                _dbContext.Entry(SelectedTask).Collection(t => t.Comments).Load();
                foreach (var comment in SelectedTask.Comments)
                {
                    _dbContext.Entry(comment).Collection(c => c.History).Load();
                }
                OnPropertyChanged(nameof(SelectedTask));
            }
        }

        private void AddComment()
        {
            if (SelectedTask == null) return;

            var vm = new CommentViewModel(_dbContext, null, SelectedTask.BeTaskId);
            var view = new CommentView { DataContext = vm };
            if (view.ShowDialog() == true)
            {
                LoadCommentsForSelectedTask();
            }
        }

        private void EditComment(Comment comment)
        {
            if (comment == null) return;

            var vm = new CommentViewModel(_dbContext, comment, comment.TaskId);
            var view = new CommentView { DataContext = vm };
            if (view.ShowDialog() == true)
            {
                LoadCommentsForSelectedTask();
            }
        }

        private void DeleteComment(Comment comment)
        {
            if (comment == null) return;

            var result = MessageBox.Show("Are you sure you want to delete this comment?", "Confirm Delete", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                _dbContext.Comments.Remove(comment);
                _dbContext.SaveChanges();
                LoadCommentsForSelectedTask();
            }
        }
        private void OpenCommentHistory(object parameter)
        {
            if (parameter is Comment comment)
            {
                var viewModel = new CommentHistoryViewModel(comment);
                var window = new CommentHistoryView
                {
                    DataContext = viewModel
                };
                window.ShowDialog();
            }
        }

        public ICommand OpenSearchPageCommand => new RelayCommand(_ => OpenSearchPage());

        private void OpenSearchPage()
        {
            var vm = new SearchViewModel(_dbContext);
            var view = new Views.SearchView
            {
                DataContext = vm
            };
            view.ShowDialog();
        }

        public ICommand OpenDashboardCommand => new RelayCommand(_ => OpenDashboard());

        private void OpenDashboard()
        {
            var vm = new DashboardViewModel(_dbContext);
            var window = new DashboardView
            {
                DataContext = vm,
                Owner = Application.Current.MainWindow
            };
            window.ShowDialog();
        }
    }
}
