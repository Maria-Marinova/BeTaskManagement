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
using System.Windows;

namespace BeTaskManagement.ViewModels
{
    public class CommentHistoryViewModel : ViewModelBase
    {
        public ObservableCollection<CommentHistory> CommentHistory { get; }

        public CommentHistoryViewModel(Comment comment)
        {
            CommentHistory = new ObservableCollection<CommentHistory>(
                comment.History.OrderByDescending(h => h.ModifiedOn));
        }
    }
}
