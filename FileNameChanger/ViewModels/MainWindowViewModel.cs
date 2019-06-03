using System.Collections.Generic;
using System.ComponentModel;

namespace FileNameChanger.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string directoryPath;
        public string DirectoryPath
        {
            get { return directoryPath; }
            set
            {
                directoryPath = value;
                OnPropertyChanged("DirectoryPath");
            }
        }

        private List<PreviewFileFormat> previewFileList = new List<PreviewFileFormat>();
        public List<PreviewFileFormat> PreviewFileList
        {
            get { return previewFileList; }
            set
            {
                previewFileList = value;
                OnPropertyChanged("PreviewFileList");
            }
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }

    public class PreviewFileFormat
    {
        public string OriginalFileName { get; set; }
        public string ChangedFileName { get; set; }
    }
}
