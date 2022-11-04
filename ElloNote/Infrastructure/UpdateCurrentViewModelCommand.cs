using ElloNote.State.Navigators;
using ElloNote.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
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

        MainWindowViewModel _mainWindow = new MainWindowViewModel();
        DrawWindowViewModel _drawWindow = new DrawWindowViewModel();
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
                        _navigator.CurrentViewModel = _mainWindow;
                        break;
                    case ViewType.Draw:
                        _navigator.CurrentViewModel = _drawWindow;
                        break;
                    case ViewType.Start:
                        _navigator.CurrentViewModel = new StartPageViewModel();
                        break;
                    case ViewType.Search:
                        _navigator.CurrentViewModel = new SearchPageViewModel();
                        break;
                    case ViewType.Recent:
                        _navigator.CurrentViewModel = new RecentPageViewModel();
                        break;
                    case ViewType.Settings:
                        _navigator.CurrentViewModel = new SettingsPageViewModel();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}