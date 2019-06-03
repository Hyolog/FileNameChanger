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
            set { directoryPath = value; }
        }
    }
}
