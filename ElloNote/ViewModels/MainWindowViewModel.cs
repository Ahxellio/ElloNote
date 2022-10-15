using ElloNote.Infrastructure;
using ElloNote.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing.Text;
using System.Drawing.Imaging;
using WPF.ColorPicker;
using WPF.ColorPicker.Code;
using System.Drawing;
using System.Collections.ObjectModel;
using ElloNote.Models;

namespace ElloNote.ViewModels
{
    internal class MainWindowViewModel : BaseVM
    {
        #region Команды
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
        }
        private bool CanMinimizedApplicationCommandExecute(object p) => true;

        /// <summary>SaveFileCMD</summary>

        public ICommand SaveFileCommand { get; }
        private void OnSaveFileCommandExecuted(object p)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Text files (.txt)|*.txt|All files (*.*)|*.*";
            dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (dlg.ShowDialog() == true)
            {
                FileStream fs = new FileStream(dlg.FileName, FileMode.Create, FileAccess.Write);
            }
        }
        private bool CanSaveFileCommandExecute(object p) => true;

        /// <summary>OpenFileCMD</summary>

        public ICommand OpenFileCommand { get; }
        private void OnOpenFileCommandExecuted(object p)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Text files (.txt)|*.txt|All files (*.*)|*.*";
            dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (dlg.ShowDialog() == true)
            {
                FileStream fs = new FileStream(dlg.FileName, FileMode.Open);
            }
        }
        private bool CanOpenFileCommandExecute(object p) => true;

        private string _selectFont;
        FontsModel model = new FontsModel();

        public IEnumerable<InstalledFontCollection> SelectedFonts => model.AllFonts;

        public string SelectFont { get => _selectFont; set { Set(ref _selectFont, value); } }





        #endregion

        public MainWindowViewModel()
        {
            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
            SaveFileCommand = new LambdaCommand(OnSaveFileCommandExecuted, CanSaveFileCommandExecute);
            OpenFileCommand = new LambdaCommand(OnOpenFileCommandExecuted, CanOpenFileCommandExecute);

        }
    }
}
