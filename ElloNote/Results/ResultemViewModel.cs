using ElloNote.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElloNote.Results
{
    internal class ResultemViewModel : BaseVM
    {
        private Icon _image;
        public Icon Image
        {
            get => _image;
            set => Set(ref _image, value);
        }

        private string _fileName;
        public string FileName
        {
            get => _fileName;
            set => Set(ref _fileName, value);
        }

        private string _filePath;
        public string FilePath
        {
            get => _filePath;
            set => Set(ref _filePath, value);
        }

        private long _fileSizeBytes;
        public long FileSizeBytes
        {
            get => _fileSizeBytes;
            set => Set(ref _fileSizeBytes, value);
        }

        private string _selection;
        public string Selection
        {
            get => _selection;
            set => Set(ref _selection, value);
        }

    }
}
