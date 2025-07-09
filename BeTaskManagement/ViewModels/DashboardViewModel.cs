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

namespace BeTaskManagement.ViewModels
{
    public class DashboardViewModel : ViewModelBase
    {
        public int TotalTasks { get; set; }
        public int OpenTasks { get; set; }
        public int InProgressTasks { get; set; }
        public int ClosedTasks { get; set; }

        public ObservableCollection<BeTask> TasksDueSoon { get; set; }

        public DashboardViewModel(AppDbContext dbContext)
        {
            var tasks = dbContext.BeTasks.AsNoTracking().ToList();

            TotalTasks = tasks.Count;
            OpenTasks = tasks.Count(t => t.Status.ToString() == "ToDo");
            InProgressTasks = tasks.Count(t => t.Status.ToString() == "InProgress");
            ClosedTasks = tasks.Count(t => t.Status.ToString() == "Completed");

            var upcoming = DateTime.Now.AddDays(7);
            TasksDueSoon = new ObservableCollection<BeTask>(
                tasks.Where(t => t.DueDate <= upcoming && t.DueDate >= DateTime.Now).OrderBy(t => t.DueDate));
        }
    }
}
