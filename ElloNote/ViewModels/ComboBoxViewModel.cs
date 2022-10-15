using ElloNote.Infrastructure;
using ElloNote.Models;
using ElloNote.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ElloNote.ViewModels
{
    internal class ComboBoxViewModel : BaseVM
    {
        private uint _countFonts;
        private string _selectFont;
        FontsModel model = new FontsModel();

        public IEnumerable<InstalledFontCollection> SelectedFonts => model.AllFonts;
        public uint CountFonts { get => _countFonts; set { Set(ref _countFonts, value); } }

        public string SelectFont { get => _selectFont; set { Set(ref _selectFont, value); } }

        public ICommand SelectFontCommand { get; }
        private void OnSelectFontCommandExecuted (object p)
        {

        }
        private bool CanSelectFontCommandExecute(object p) => true;
        public ComboBoxViewModel()
        {
            SelectFontCommand = new LambdaCommand(OnSelectFontCommandExecuted, CanSelectFontCommandExecute);
        }

    }
}
