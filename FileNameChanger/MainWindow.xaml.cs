using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using FileNameChanger.ViewModels;

namespace FileNameChanger
{
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

                    foreach (var fileName in Directory.GetFiles(fbd.SelectedPath))
                    {
                        viewModel.FileNames.Add(fileName);
                    }
                }
            }
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            if (!GetValidationOfCheckBox())
            {
                return;
            }

            var viewModel = DataContext as MainWindowViewModel;

            foreach (var fileName in viewModel.FileNames)
            {
                var newFileName = GetNewFileName(fileName);

                if (!fileName.Equals(newFileName))
                {
                    File.Move(fileName, newFileName);
                }
            }
        }

        private void PreviewButton_Click(object sender, RoutedEventArgs e)
        {
            if (!GetValidationOfCheckBox())
            {
                return;
            }

            var viewModel = DataContext as MainWindowViewModel;

            foreach (var fileName in viewModel.FileNames)
            {
                viewModel.NewFileNames.Add(GetNewFileName(fileName));
            }
        }

        private string GetNewFileName(string oldFileName)
        {
            return oldFileName.Contains(FromTextBox.Text) ? oldFileName.Replace(FromTextBox.Text, ToTextBox.Text) : oldFileName;
        }

        private bool ContainProhibitedCharacters(string input)
        {
            return input.Any(d => Constants.ProhibitedCharacters.Contains(d));
        }

        // TODO : Renaming, 가독성 vs 라인 수
        private bool GetValidationOfCheckBox()
        {
            return !(string.IsNullOrEmpty(FromTextBox.Text) || string.IsNullOrEmpty(ToTextBox.Text) || ContainProhibitedCharacters(FromTextBox.Text) || ContainProhibitedCharacters(ToTextBox.Text));
        }
    }
}
