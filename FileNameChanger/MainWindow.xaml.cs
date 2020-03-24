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
            if (!GetValidationOfTextBox())
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

            System.Windows.MessageBox.Show("Finished");
        }

        private void PreviewButton_Click(object sender, RoutedEventArgs e)
        {
            if (!GetValidationOfTextBox())
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
            return oldFileName.Contains(ReplaceTextBox.Text) ? oldFileName.Replace(ReplaceTextBox.Text, ToTextBox.Text) : oldFileName;
        }

        private bool ContainProhibitedCharacters(string input)
        {
            return input.Any(d => Constants.ProhibitedCharacters.Contains(d));
        }

        // TODO : how can i know some method contain messagebox or not?
        private bool GetValidationOfTextBox()
        {
            if (string.IsNullOrWhiteSpace(FolderPathTextBox.Text))
            {
                System.Windows.MessageBox.Show("Please enter folder path.");
                FolderPathTextBox.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(ReplaceTextBox.Text))
            {
                System.Windows.MessageBox.Show("Please enter replace text.");
                ReplaceTextBox.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(ToTextBox.Text))
            {
                System.Windows.MessageBox.Show("Please enter to text.");
                ToTextBox.Focus();
                return false;
            }

            if (ContainProhibitedCharacters(ReplaceTextBox.Text) || ContainProhibitedCharacters(ToTextBox.Text))
            {
                System.Windows.MessageBox.Show($"Can not use these characters for fileName. {string.Join(" ",Constants.ProhibitedCharacters)}");
                return false;
            }

            return true;
        }
    }
}
