using ElloNote.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ElloNote.Infrastructure
{
    internal class ControlBarCommands : BaseVM
    {
        #region Close/Minimized Window Commands
        /// <summary>CloseApplicationCMD</summary>

        public ICommand CloseApplicationCommand { get; }
        private void OnCloseApplicationCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        }
        private bool CanCloseApplicationCommandExecute(object p) => true;

        /// <summary>MinimizeApplicationCMD</summary>
        public ICommand MinimizedApplicationCommand { get; }
        private void OnMinimizedApplicationCommandExecuted(object p)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }
        private bool CanMinimizedApplicationCommandExecute(object p) => true;
        #endregion
        public ControlBarCommands()
        {
            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
            MinimizedApplicationCommand = new LambdaCommand(OnMinimizedApplicationCommandExecuted, CanMinimizedApplicationCommandExecute);
        }

    }
}
