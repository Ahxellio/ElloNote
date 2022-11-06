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
using System.Windows.Input;
using ElloNote.Infrastructure;
using Ookii.Dialogs.Wpf;
using System.Windows;
using System.IO;
using System.Windows.Navigation;
using System.Windows.Controls;
using static System.Net.WebRequestMethods;
using File = System.IO.File;

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
        private int _foldersSearched;
        public int FoldersSearched
        {
            get { return _foldersSearched; }
            set { Set(ref _foldersSearched, value); }
        }
        private int _filesSearched;
        public int FilesSearched
        {
            get { return _filesSearched; }
            set { Set(ref _filesSearched, value); }
        }

        private string _currentlySearching;
        public string CurrentlySearching
        {
            get { return _currentlySearching; }
            set { Set(ref _currentlySearching, value); }
        }
        private bool _isSearching;
        public bool IsSearching
        {
            get { return _isSearching; }
            set { Set(ref _isSearching, value); }
        }

        public bool CanSearch { get; set; }
        public ObservableCollection<ResultemViewModel> Results { get; set; }
        public ICommand SearchCommand { get; }
        public ICommand CancelSearchCommand { get; }
        public ICommand SelectStartFolderCommand { get; }
        public ICommand ExportResultsCommand { get; }
        public ICommand ClearResultsCommand { get; }



        public SearchPageViewModel()
        {
            Results = new ObservableCollection<ResultemViewModel>();
            SearchCommand = new SearchCommand(Find);
            CancelSearchCommand = new SearchCommand(CancelSearch);
            SelectStartFolderCommand = new SearchCommand(SelectStartFolderPath);
            ExportResultsCommand = new SearchCommand(ExportResultsToFolder);
            ClearResultsCommand = new SearchCommand(Clear);

            SearchRecursive = true;
            SearchPreference = SearchPreferences.Files;
            StartFolder = @"C:\"; //Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            SearchFor = "";
            IgnoreExtension = true;
        }
        public void ExportResultsToFolder()
        {

        }
        public void CancelSearch()
        {
            SetSearchingStatus(false);
            CanSearch = false;
        }
        public void SelectStartFolderPath()
        {
            VistaFolderBrowserDialog fbd = new VistaFolderBrowserDialog();
            fbd.UseDescriptionForTitle = true;
            fbd.Description = "Select a directory to start the search in";
            if(fbd.ShowDialog() == true)
            {
                if (fbd.SelectedPath.IsDirectory())
                {
                    StartFolder = fbd.SelectedPath;
                }
                else
                    MessageBox.Show("Selected folder doesn't exist");
            }
        }
        public void Clear()
        {
            Results.Clear();
        }

        public void ClearSearchCounters()
        {
            FilesSearched = 0;
            FoldersSearched = 0;
        }

        public void SetSearchingStatus(bool isSearching)
        {
            IsSearching = isSearching;
            CanSearch = isSearching;
            CurrentlySearching = "";
        }

        public void Find()
        {
            try
            {
                //Cancel any search currently active
                CancelSearch();
                ClearSearchCounters();
                if(!string.IsNullOrEmpty(SearchFor))
                {
                    if(StartFolder.IsDirectory())
                    {
                        Clear();
                        SetSearchingStatus(true);

                        Task.Run(() =>
                        {
                            string searchText = CaseSenstive ? SearchFor : SearchFor.ToLower();
                            if (SearchRecursive)
                            {
                                StartSearchRecursively(searchText);
                            }
                            else
                            {
                                StartSearchNonRecursively(searchText);
                            }
                            SetSearchingStatus(false);
                        });
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"{e.Message} -- Cancelling Search ");
                CancelSearch();
            }
        }
        public void StartSearchRecursively(string searchText)
        {
            string startFolder = StartFolder;
            switch (SearchPreference)
            {
                case SearchPreferences.Folders:
                    {
                        void DirectorySearch(string toSearchDir)
                        {
                            foreach (string folder in Directory.GetDirectories(toSearchDir))
                            {
                                if (!CanSearch) return;
                                SearchFolderName(folder, searchText);
                                DirectorySearch(folder);
                            }
                        }
                        DirectorySearch(startFolder);
                    }
                    break;
                case SearchPreferences.Files:
                    {
                        void DirectorySearch(string toSearchDir)
                        {
                            foreach (string folder in Directory.GetDirectories(toSearchDir))
                            {
                                if (!CanSearch) return;
                                foreach (string file in Directory.GetFiles(folder))
                                {
                                    if(!CanSearch) return;
                                    SearchFileName(file, searchText);
                                }
                                FoldersSearched++;
                                DirectorySearch(folder);
                            }
                            
                        }
                        DirectorySearch(startFolder);

                    }

                    break;
                case SearchPreferences.FileContents:
                    {
                        void DirectorySearch(string toSearchDir)
                        {
                            foreach (string folder in Directory.GetDirectories(toSearchDir))
                            {
                                if (!CanSearch) return;
                                foreach (string file in Directory.GetFiles(folder))
                                {
                                    if (!CanSearch) return;
                                    ReadAndSearchFile(file, searchText, true);
                                }
                                FoldersSearched++;
                                DirectorySearch(folder);
                            }
                        }
                        DirectorySearch(startFolder);
                        foreach (string file in Directory.GetFiles(startFolder))
                        {
                            if (!CanSearch) return;

                            ReadAndSearchFile(file, searchText, true);
                        }
                    }
                    break;
                case SearchPreferences.All:
                    {
                        void DirectorySearch(string toSearchDir)
                        {
                            foreach (string folder in Directory.GetDirectories(toSearchDir))
                            {
                                if (!CanSearch) return;
                                SearchFolderName(folder, searchText);
                                foreach (string file in Directory.GetFiles(folder))
                                {
                                    if (!CanSearch) return;
                                    bool hasFoundFile = SearchFileName(file, searchText);
                                    if(!hasFoundFile)
                                    {
                                        ReadAndSearchFile(file, searchText, false);
                                    }    
                                }
                                FoldersSearched++;
                                DirectorySearch(folder);
                            }
                        }
                        DirectorySearch(StartFolder);
                        foreach (string file in Directory.GetFiles(startFolder))
                        {
                            if (!CanSearch) return;

                            bool hasFoundFile = SearchFileName(file, searchText);

                            // this stops there from being duplicated items.
                            // if it's already found the item above, dont search
                            // the contents because that's just pointless.
                            if (!hasFoundFile)
                            {
                                ReadAndSearchFile(file, searchText, false);
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
        }
        public void StartSearchNonRecursively(string searchText)
        {
            string startFolder = StartFolder;
            switch (SearchPreference)
            {
                case SearchPreferences.Folders:
                    foreach (string folder in Directory.GetDirectories(startFolder))
                    {
                        if (!CanSearch) return;
                        SearchFolderName(folder, searchText);
                    }
                    break;
                case SearchPreferences.Files:
                    foreach (string file in Directory.GetFiles(startFolder))
                    {
                        if (!CanSearch) return;
                        SearchFileName(file, searchText);
                    }
                    break;
                case SearchPreferences.FileContents:
                    foreach (string file in Directory.GetFiles(startFolder))
                    {
                        if (!CanSearch) return;
                        ReadAndSearchFile(file, searchText, true);
                    }
                    break;
                case SearchPreferences.All:
                    foreach (string folder in Directory.GetDirectories(startFolder))
                    {
                        if (!CanSearch) return;
                        SearchFolderName(folder, searchText);
                    }
                    foreach (string file in Directory.GetFiles(startFolder))
                    {
                        if (!CanSearch) return;
                        bool hasFoundFile = SearchFileName(file, searchText);
                        if(!hasFoundFile)
                        {
                            ReadAndSearchFile(file, searchText, false);
                        }
                    }
                    break;
                default:
                    break;
            }
        }
        public bool SearchFileName(string name, string searchText)
        {
            CurrentlySearching = name;
            if(GetFileName(name).Contains(searchText))
            {
                ResultFound(name, searchText);
                FilesSearched++;
                return true;
            }
            FilesSearched++;
            return false;

        }
        public bool SearchFolderName(string name, string searchText)
        {
            if(name.GetDirectoryName().Contains(searchText))
            {
                ResultFound(name, searchText);
                FoldersSearched++;
                return true;
            }
            FoldersSearched++;
            return false;
        }
        public string GetFileName(string original)
        {
            if (IgnoreExtension)
                return Path.GetFileNameWithoutExtension(original);
            else
                return Path.GetFileName(original);
        }
        public void ResultFound(string path, string selection)
        {
            ResultemViewModel result = CreateResultFromPath(path, selection);
            if(result != null)
                AddResult(result);
        }
        public ResultemViewModel CreateResultFromPath(string path, string selectionText)
        {
            if (path.IsFile())
            {
                try
                {
                    FileInfo fInfo = new FileInfo(path);
                    ResultemViewModel result = new ResultemViewModel()
                    {
                        Image = IconHelper.GetIconOfFile(path, false, false),
                        FileName = fInfo.Name,
                        FilePath = fInfo.FullName,
                        Selection = selectionText,
                        FileSizeBytes = fInfo.Length,
                    };
                    return result;
                }
                catch (Exception e)
                {
                    MessageBox.Show($"{e.Message} -- Cancelling Search ");
                    return null;
                }

            }
            else if (path.IsDirectory())
            {
                try
                {
                    DirectoryInfo dInfo = new DirectoryInfo(path);
                    ResultemViewModel result = new ResultemViewModel()
                    {
                        Image = IconHelper.GetIconOfFile(path, false, true),
                        FileName = dInfo.Name,
                        FilePath = dInfo.FullName,
                        Selection = selectionText,
                        FileSizeBytes = long.MaxValue,
                    };
                    return result;
                }
                catch (Exception e)
                {
                    MessageBox.Show($"{e.Message} -- Cancelling Search ");
                    return null;
                }
            }
            return null;
        }

        public void ReadAndSearchFile(string file, string searchText, bool increasedSearchedFile)
        {
            try
            {
                CurrentlySearching = file;
                using (FileStream fs = File.OpenRead(file))
                {
                    byte[] b = new byte[1024];
                    while(fs.Read(b, 0, b.Length) > 0)
                    {
                        //Cancel Search
                        if (!CanSearch) return;

                        //Get Text from buffer
                        string txt = Encoding.ASCII.GetString(b);
                        

                        //Convert Text to lower if CaseSensetive if disabled
                        if((CaseSenstive ? txt:txt.ToLower()).Contains(searchText))
                        {
                            ResultFound(file, searchText);
                            break;
                        }

                    }
                }
                if (increasedSearchedFile)
                    FilesSearched++;
            }
            catch (Exception e)
            {
                MessageBox.Show($"{e.Message} -- Cancelling Search ");
                CancelSearch();
            }
        }

        public void AddResult(ResultemViewModel result)
        {
            Application.Current?.Dispatcher?.Invoke(() =>
            {
                Results.Add(result);
            });
        }
        public void RemoveResult(ResultemViewModel result)
        {
            Results.Remove(result);
        }
    }
}
