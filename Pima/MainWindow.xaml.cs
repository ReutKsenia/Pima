using MaterialDesignThemes.Wpf;
using Pima.View.Pages.UserPages;
using Pima.View.Windows;
using Pima.ViewModel.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pima
{
    public partial class MainWindow : Window
    {
        MainViewModel main = new MainViewModel();
        public static Snackbar Snackbar;
        public static SnackbarMessage SnackbarMessage;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = main;
            Snackbar = SnackBar;
            SnackbarMessage = SnackBarMessage;
        }

        private void SnackbarMessage_ActionClick(object sender, RoutedEventArgs e)
        {
            SnackBar.IsActive = false;
        }
    }
}
