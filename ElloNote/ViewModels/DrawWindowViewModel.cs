using ElloNote.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;

namespace ElloNote.ViewModels
{
    internal class DrawWindowViewModel : BaseVM
    {
        private List<InkCanvasEditingMode> canvasEditingModes = new List<InkCanvasEditingMode>() { InkCanvasEditingMode.Ink,
        InkCanvasEditingMode.Select, InkCanvasEditingMode.EraseByPoint, InkCanvasEditingMode.EraseByStroke, InkCanvasEditingMode.InkAndGesture};

        private InkCanvasEditingMode _selectedDrawingMode;
        public InkCanvasEditingMode SelectedDrawingMode
        {
            get { return _selectedDrawingMode; }
            set { Set(ref _selectedDrawingMode, value); }
        }

        public List<InkCanvasEditingMode> CanvasEditingModes { get => canvasEditingModes; set => Set(ref canvasEditingModes, value); }

        public DrawWindowViewModel()
        {


        }
    }
}
