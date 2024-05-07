using System.Windows;
using DodoDinner.ViewModels;

namespace DodoDinner.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow(MainViewModel mainViewModel)
        {
            InitializeComponent();
        }
    }
}
