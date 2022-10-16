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
using System.Drawing;
using System.Collections.ObjectModel;
using Color = System.Windows.Media.Color;
using Brush = System.Windows.Media.Brush;
using WPF.ColorPicker;

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
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
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
        /// <summary>ColorPicker Window</summary>
        private bool CanOpenFileCommandExecute(object p) => true;


        public ICommand OpenColorPickerCommand { get; }
        private void OnOpenColorPickerCommandExecuted(object p)
        {
            Color color;
            ColorPickerWindow.ShowDialog(out color);
        }

        private bool CanOpenColorPickerCommandExecute(object p) => true;


        /// <summary>FontSize Values</summary>
        private List<int> _SourceList = new List<int> { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };
        public List<int> SourceList
        {
            get { return _SourceList; }
            set { Set(ref _SourceList, value); }
        }

        /// <summary></summary>
        public ICommand MoveApplicationCommand { get; }
        private void OnMoveApplicationCommandExecuted(object p)
        {
            var window = p as Window;
            window?.DragMove();
        }
        private bool CanMoveApplicationCommandExecute(object p) => true;


        public ICommand FontColorChangeCommand { get; }
        private void OnFontColorChangeCommandExecuted(object p)
        {

        }
        private bool CanColorChangeCommandExecute(object p) => true;
        #endregion

        public MainWindowViewModel()
        {
            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
            SaveFileCommand = new LambdaCommand(OnSaveFileCommandExecuted, CanSaveFileCommandExecute);
            OpenFileCommand = new LambdaCommand(OnOpenFileCommandExecuted, CanOpenFileCommandExecute);
            OpenColorPickerCommand = new LambdaCommand(OnOpenColorPickerCommandExecuted, CanOpenColorPickerCommandExecute);
            MinimizedApplicationCommand = new LambdaCommand(OnMinimizedApplicationCommandExecuted, CanMinimizedApplicationCommandExecute);
            MoveApplicationCommand = new LambdaCommand(OnMoveApplicationCommandExecuted, CanMoveApplicationCommandExecute);

        }
    }
}
