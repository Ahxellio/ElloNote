
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ElloNote.ViewModels;
using ElloNote.Views;
using Egor92.MvvmNavigation;
using Egor92.MvvmNavigation.Abstractions;
using ElloNote.Infrastructure;

namespace ElloNote
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Window window = new MainWindow();
            window.DataContext = new MainWindowViewModel();
            window.Show();
            base.OnStartup(e);
        }
    }
}
