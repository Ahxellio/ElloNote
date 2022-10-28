using ElloNote.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ElloNote.State.Navigators
{
    public enum ViewType
    {
        Main,
        Draw,
    }
    internal interface INavigator
    {
        BaseVM CurrentViewModel { get; set; }
        ICommand UpdateCurrentViewModelCommand { get; }
    }
}
