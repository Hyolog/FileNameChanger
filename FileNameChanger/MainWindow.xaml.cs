using System.Collections.Generic;
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
                    viewModel.FolderPath = fbd.SelectedPath;

                    viewModel.FileNames = new List<string>(Directory.GetFiles(fbd.SelectedPath));
                }
            }
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FolderPathTextBox.Text))
            {
                System.Windows.MessageBox.Show("Please check folder path.");
                return;
            }

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
            if (string.IsNullOrWhiteSpace(FolderPathTextBox.Text))
            {
                System.Windows.MessageBox.Show("Please check folder path.");
                return;
            }

            if (!GetValidationOfCheckBox())
            {
                return;
            }

            var viewModel = DataContext as MainWindowViewModel;

            if (viewModel.NewFileNames.Any())
            {
                return;
            }
            else
            {
                var newFileNames = new List<string>();

                foreach (var fileName in viewModel.FileNames)
                {
                    newFileNames.Add(GetNewFileName(fileName));
                }

                viewModel.NewFileNames = new List<string>(newFileNames);
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

        // TODO : how can i know some method contain messagebox or not?
        private bool GetValidationOfCheckBox()
        {
            if (string.IsNullOrWhiteSpace(FromTextBox.Text) || string.IsNullOrWhiteSpace(ToTextBox.Text))
            {
                System.Windows.MessageBox.Show("Please check textbox.");
                return false;
            }

            if (ContainProhibitedCharacters(FromTextBox.Text) || ContainProhibitedCharacters(ToTextBox.Text))
            {
                System.Windows.MessageBox.Show($"Can not use these characters for fileName. {string.Join(" ",Constants.ProhibitedCharacters)}");
                return false;
            }

            return true;
        }
    }
}
