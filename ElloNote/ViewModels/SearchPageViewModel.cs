using ElloNote.Controls;
using ElloNote.Results;
using ElloNote.ViewModels.Base;
using System.Drawing;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FontAwesome.Sharp;
using System.Windows.Input;

namespace ElloNote.ViewModels
{
    internal class SearchPageViewModel : BaseVM
    {
        private string _searchFor;
        public string SearchFor
        {
            get { return _searchFor; }
            set { Set(ref _searchFor, value); }
        }
        private string _startFolder;
        public string StartFolder
        {
            get { return _startFolder; }
            set { Set(ref _startFolder, value); }
        }
        private SearchPreferences _searchPreference;
        public SearchPreferences SearchPreference
        {
            get { return _searchPreference; }
            set { Set(ref _searchPreference, value); }
        }
        private bool _caseSenstive;
        public bool CaseSenstive
        {
            get { return _caseSenstive; }
            set { Set(ref _caseSenstive, value); }
        }
        private bool _searchRecursive;
        public bool SearchRecursive
        {
            get { return _searchRecursive; }
            set { Set(ref _searchRecursive, value); }
        }
        private bool _ignoreExtension;
        public bool IgnoreExtension
        {
            get { return _ignoreExtension; }
            set { Set(ref _ignoreExtension, value); }
        }
        private bool _foldersSearched;
        public bool FoldersSearched
        {
            get { return _foldersSearched; }
            set { Set(ref _foldersSearched, value); }
        }
        private bool _filesSearched;
        public bool FilesSearched
        {
            get { return _filesSearched; }
            set { Set(ref _filesSearched, value); }
        }
        private bool _isSearching;
        public bool IsSearching
        {
            get { return _isSearching; }
            set { Set(ref _isSearching, value); }
        }

        public ObservableCollection<ResultemViewModel> Results { get; set; }
        public ICommand SearchCommand { get; }
        public ICommand CancelSearchCommand { get; }
        public ICommand SelectStartFolderCommand { get; }
        public ICommand ExportResultsCommand { get; }
        public ICommand ClearResultsCommand { get; }



        public SearchPageViewModel()
        {
            Results = new ObservableCollection<ResultemViewModel>();
        }
    }
}
