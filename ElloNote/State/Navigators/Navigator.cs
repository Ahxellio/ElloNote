using ElloNote.Infrastructure;
using ElloNote.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ElloNote.State.Navigators
{
    internal class Navigator : BaseVM, INavigator
    {
        private BaseVM _currentViewModel;
        public BaseVM CurrentViewModel
        {
            get { return _currentViewModel; }
            set { Set(ref _currentViewModel, value); }
        }

        public ICommand UpdateCurrentViewModelCommand => new UpdateCurrentViewModelCommand(this);
    }
}
