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
                }
            }
        }
    }
}
