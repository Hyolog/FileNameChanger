using System.Collections.Generic;
using System.ComponentModel;

namespace FileNameChanger.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string folderPath;
        public string FolderPath
        {
            get { return folderPath; }
            set
            {
                folderPath = value;
                OnPropertyChanged("FolderPath");
            }
        }

        private List<string> fileNames = new List<string>();
        public List<string> FileNames
        {
            get { return fileNames; }
            set
            {
                fileNames = value;
                OnPropertyChanged("FileNames");
            }
        }

        private List<string> newFileNames = new List<string>();
        public List<string> NewFileNames
        {
            get { return newFileNames; }
            set
            {
                newFileNames = value;
                OnPropertyChanged("NewFileNames");
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
}
