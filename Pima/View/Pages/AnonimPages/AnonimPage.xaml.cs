using Pima.View.Windows;
using Pima.ViewModel.Pages;
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

namespace Pima.View.Pages.AnonimPages
{
    public partial class AnonimPage : Page
    {
        AnonimPageViewModel context = new AnonimPageViewModel();
        public AnonimPage()
        {
            InitializeComponent();
            DataContext = context;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Close();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            GridBackground.Opacity = 1;
            login.ShowDialog();
            GridBackground.Opacity = 0;
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            Registration registration = new Registration();
            GridBackground.Opacity = 1;
            registration.ShowDialog();
            GridBackground.Opacity = 0;
        }

        private void Admin_Click(object sender, RoutedEventArgs e)
        {
            Admin admin = new Admin();
            GridBackground.Opacity = 1;
            admin.ShowDialog();
            GridBackground.Opacity = 0;
        }
    }
}
