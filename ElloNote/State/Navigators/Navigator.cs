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
        private BaseVM _viewModel;
        public BaseVM viewModel
        {
            get { return _viewModel; }
            set { Set(ref _viewModel, value); }
        }
        public BaseVM CurrentViewModel { get; set; }

        public ICommand UpdateCurrentViewModelCommand => new UpdateCurrentViewModelCommand(this);
    }
}
