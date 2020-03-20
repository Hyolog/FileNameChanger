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
                        viewModel.PreviewFileList.Add(new PreviewFileFormat() { OriginalFileName = file, ChangedFileName = file + "ex"});
                    }
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as MainWindowViewModel;

            foreach (var file in viewModel.PreviewFileList)
            {

                //1. file명에 Before가 포함되어있으면
                //2. 해당 파일명의 그 부분을 after로 변경

                if (file.OriginalFileName.Contains(Before.Text))
                {
                    var oldFileName = file.OriginalFileName;
                    var newFileName = file.OriginalFileName.Replace(Before.Text, After.Text);
                    // 실제로 파일 명 변경
                    File.Move(oldFileName, newFileName);
                }
            }
        }
    }
}
