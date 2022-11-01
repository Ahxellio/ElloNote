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
using Xceed.Wpf.Toolkit;
using WindowState = System.Windows.WindowState;
using System.Reflection.Metadata;
using MessageBox = Xceed.Wpf.Toolkit.MessageBox;
using ElloNote.State.Navigators;

namespace ElloNote.ViewModels
{
    internal class MainWindowViewModel : BaseVM
    {


        #region Команды


        #region Save/Open File Command
        /// <summary>SaveFileCMD</summary>
        public string _fileName;
        public string FileName { get => _fileName; set
            {
                Set(ref _fileName, value);
            }
        }

        #region Save File Command
        public ICommand SaveFileCommand { get; }
        private void OnSaveFileCommandExecutedAsync(object p)
        {
            var file_name = p as string;
            if (file_name == null)
            {
                var dlg = new SaveFileDialog();
                dlg.Filter = "Text files (.txt)|*.txt|All files (*.*)|*.*";
                dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                if (dlg.ShowDialog() != true) return;
                file_name = dlg.FileName;
            }
            using (var writer = new StreamWriter(new FileStream(file_name, FileMode.Create, FileAccess.Write)))
            {
                writer.Write(_Notes);
            }
            MessageBox.Show("Text File Saved");
        }
        private bool CanSaveFileCommandExecute(object p) 
        {
            return !string.IsNullOrEmpty(_Notes);
        }
        #endregion

        #region OpenFileCMD


        public ICommand OpenFileCommand { get; }
        private void OnOpenFileCommandExecuted(object p)
        {
            var file_name = p as string;
            if (file_name == null)
            {
                var dlg = new OpenFileDialog();
                dlg.Filter = "Text files (.txt)|*.txt|All files (*.*)|*.*";
                dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                if (dlg.ShowDialog() != true) return;
                file_name = dlg.FileName;
            }
            using (var reader = new StreamReader(new FileStream(file_name, FileMode.Open, FileAccess.Read)))
            {
                Notes = reader.ReadToEnd();
            }

        }
        private bool CanOpenFileCommandExecute(object p) => true;
        #endregion

        #endregion

        #region ColorPicker
        //ColorModel colorModel = new ColorModel();
        private Color _SelectedColor;
        public Color SelectedColor
        {

            get 
            {
                return _SelectedColor; 
            }
            set
            {
                Set(ref _SelectedColor, value);
            }
        }


        private Brush _brushColor;
        public Brush BrushColor
        {
            get 
            { 
                return _brushColor; 
            }
            set
            {
                SolidColorBrush brush = new SolidColorBrush(_SelectedColor);
                _brushColor = brush;
                Set(ref _brushColor, value);

            }
        }

        #endregion

        #region FontSizeProp

        /// <summary>FontSize Values</summary>
        private List<int> _SourceList = new List<int> { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72, 96 };
        public List<int> SourceList
        {
            get { return _SourceList; }
            set { Set(ref _SourceList, value); }
        }
        private int _SelectedFontSize;
        public int SelectedFontSize { get => _SelectedFontSize; set { Set(ref _SelectedFontSize, value); } }
        #endregion

        #region Navigator
        public INavigator Navigator { get; set; } = new Navigator();
        

        #endregion

        #region Text
        private string _Notes;

        public string Notes { get => _Notes; set => Set(ref _Notes, value); }

        #endregion




        #endregion
        public MainWindowViewModel()
        {
            SaveFileCommand = new LambdaCommand(OnSaveFileCommandExecutedAsync, CanSaveFileCommandExecute);
            OpenFileCommand = new LambdaCommand(OnOpenFileCommandExecuted, CanOpenFileCommandExecute);
        }
    }
}
