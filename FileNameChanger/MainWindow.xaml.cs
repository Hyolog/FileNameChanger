using System.IO;
using System.Windows;
using System.Windows.Forms;
using FileNameChanger.ViewModels;

namespace FileNameChanger
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    var viewModel = DataContext as MainWindowViewModel;
                    viewModel.DirectoryPath = fbd.SelectedPath;

                    var fileList = Directory.GetFiles(fbd.SelectedPath);

                    foreach(var file in fileList)
                    {
                        //TODO: 수식 받아와서 변경하도록
                        viewModel.PreviewFileList.Add(new PreviewFileFormat() { OriginalFileName = file, ChangedFileName = file + "ex"});
                    }
                }
            }
        }
    }
}
