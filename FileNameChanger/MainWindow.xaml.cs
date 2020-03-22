using System.Collections.Generic;
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

                    foreach (var fileName in Directory.GetFiles(fbd.SelectedPath))
                    {
                        viewModel.FileNames.Add(fileName);
                    }
                }
            }
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as MainWindowViewModel;

            foreach (var fileName in viewModel.FileNames)
            {
                if (fileName.Contains(FromTextBox.Text))
                {
                    var oldFileName = fileName;
                    var newFileName = fileName.Replace(FromTextBox.Text, ToTextBox.Text);
                    
                    File.Move(oldFileName, newFileName);
                }
            }
        }

        private void PreviewButton_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as MainWindowViewModel;

            foreach (var fileName in viewModel.FileNames)
            {
                if (fileName.Contains(FromTextBox.Text))
                {
                    // 파일 전체 돌면서 조건에 맞는 파일이 있는경우
                    
                    // (만약 new list 가 비어있다면) 만들고 바인딩
                    viewModel.NewFileNames.Add(fileName.Replace(FromTextBox.Text, ToTextBox.Text));
                }
            }
        }
    }
}
