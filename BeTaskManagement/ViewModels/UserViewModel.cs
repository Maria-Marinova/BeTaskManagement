using BeTaskManagement.Base;
using BeTaskManagement.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeTaskManagement.ViewModels
{
    internal class UserViewModel : ViewModelBase
    {
        public RelayCommand AddCommand => new RelayCommand(execute => { }, canExecute => { return true; });
    }
}
