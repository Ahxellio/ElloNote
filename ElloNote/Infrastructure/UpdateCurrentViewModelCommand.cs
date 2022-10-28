using ElloNote.State.Navigators;
using ElloNote.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ElloNote.Infrastructure
{
    internal class UpdateCurrentViewModelCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private INavigator _navigator;

        public UpdateCurrentViewModelCommand(INavigator navigator)
        {
            _navigator = navigator;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (parameter is ViewType)
            {
                ViewType viewType = (ViewType)parameter;
                switch (viewType)
                {
                    case ViewType.Main:
                        _navigator.CurrentViewModel = new MainWindowViewModel();
                        break;
                    case ViewType.Draw:
                        _navigator.CurrentViewModel = new DrawWindowViewModel();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}