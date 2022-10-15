using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElloNote.Models
{
    internal class FontsModel
    {
        private static readonly Font font;
        public ObservableCollection<InstalledFontCollection> AllFonts;
        //public ObservableCollection<InstalledFontCollection> GetAllFonts()
        //{
        //    var fontCollections = new InstalledFontCollection();
        //    var ff = fontCollections.Families;
        //    foreach (var font in ff)
        //    {
        //        AllFont.Add(font.Name);
        //    }
        //}

}
}
