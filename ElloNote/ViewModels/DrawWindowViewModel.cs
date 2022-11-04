using ElloNote.Infrastructure;
using ElloNote.ViewModels.Base;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using Xceed.Wpf.Toolkit;

namespace ElloNote.ViewModels
{
    internal class DrawWindowViewModel : BaseVM
    {
        private List<InkCanvasEditingMode> canvasEditingModes = new List<InkCanvasEditingMode>() { InkCanvasEditingMode.Ink,
        InkCanvasEditingMode.Select, InkCanvasEditingMode.EraseByPoint, InkCanvasEditingMode.EraseByStroke, InkCanvasEditingMode.InkAndGesture,
        InkCanvasEditingMode.None};

        private InkCanvasEditingMode _selectedDrawingMode;
        public InkCanvasEditingMode SelectedDrawingMode
        {
            get { return _selectedDrawingMode; }
            set { Set(ref _selectedDrawingMode, value); }
        }

        public List<InkCanvasEditingMode> CanvasEditingModes { get => canvasEditingModes; set => Set(ref canvasEditingModes, value); }

        private List<double> _SourceList = new List<double> { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72, 96 };
        public List<double> SourceList
        {
            get { return _SourceList; }
            set { Set(ref _SourceList, value); }
        }
        private int _SelectedFontSize;
        public int SelectedFontSize { get => _SelectedFontSize; set { Set(ref _SelectedFontSize, value); } }

        private Color _SelectedColorCanvas;
        public Color SelectedColorCanvas
        {

            get
            {
                return _SelectedColorCanvas;
            }
            set
            {
                Set(ref _SelectedColorCanvas, value);
            }
        }
        private string _SelectedObject;
        public string SelectedObject
        {
            get => _SelectedObject;
            set
            {
                _SelectedObject = _SelectedColorCanvas.ToString();
                Set(ref _SelectedObject, value);
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
                dlg.Filter = "Text files (.isf)|*.isf|All files (*.*)|*.*";
                dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                if (dlg.ShowDialog() != true) return;
                file_name = dlg.FileName;
            }
            using (var writer = new StreamWriter(new FileStream(file_name, FileMode.Create, FileAccess.Write)))
            {
                writer.Write(_strokesCollection);
            }
            MessageBox.Show("Images Saving is not working! :)", "Error");
        }
        private bool CanSaveFileCommandExecute(object p) => true;
        #endregion

        #region OpenFileCMD


        public ICommand OpenFileCommand { get; }
        private void OnOpenFileCommandExecuted(object p)
        {
            var file_name = p as string;
            if (file_name == null)
            {
                var dlg = new OpenFileDialog();
                dlg.Filter = "Text files (.isf)|*.isf|All files (*.*)|*.*";
                dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                if (dlg.ShowDialog() != true) return;
                file_name = dlg.FileName;
            }
            using (var reader = new FileStream(file_name, FileMode.Open, FileAccess.Read))
            {
                StrokeCollection strokeCollection = new StrokeCollection(reader) ;
                _strokesCollection = strokeCollection;
                
            }

        }
        private bool CanOpenFileCommandExecute(object p) => true;
        #endregion

        public DrawWindowViewModel()
        {
            SaveFileCommand = new LambdaCommand(OnSaveFileCommandExecutedAsync, CanSaveFileCommandExecute);
            OpenFileCommand = new LambdaCommand(OnOpenFileCommandExecuted, CanOpenFileCommandExecute);

        }

        private StrokeCollection _strokesCollection;

        public StrokeCollection StrokesCollection { get => _strokesCollection; set => Set(ref _strokesCollection, value); }
    }
}
