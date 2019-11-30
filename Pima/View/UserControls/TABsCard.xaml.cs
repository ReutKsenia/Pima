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

namespace Pima.View.UserControls
{
    /// <summary>
    /// Логика взаимодействия для TABsCard.xaml
    /// </summary>
    public partial class TABsCard : UserControl
    {
        public TABsCard()
        {
            InitializeComponent();
        }

        public event RoutedEventHandler OpenTABMouseClick;
        public event RoutedEventHandler DeleteTABMouseClick;
        public event RoutedEventHandler UpdateTABMouseClick;
        public event RoutedEventHandler AddTABMouseClick;

        private void Add_Checked(object sender, RoutedEventArgs e)
        {
            if (AddTABMouseClick != null)
                AddTABMouseClick(sender, e);
        }

        private void Pen_Click(object sender, RoutedEventArgs e)
        {
            if (UpdateTABMouseClick != null)
                UpdateTABMouseClick(sender, e);
        }

        private void Tresh_Click(object sender, RoutedEventArgs e)
        {
            if (DeleteTABMouseClick != null)
                DeleteTABMouseClick(sender, e);
        }

        private void OpenTAB_Click(object sender, RoutedEventArgs e)
        {
            if (OpenTABMouseClick != null)
                OpenTABMouseClick(sender, e);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var currentPage = ((MainWindow)Application.Current.MainWindow).CurrentPage.Content;
            if (currentPage == null || (currentPage != null && currentPage.GetType().Name == "AnonimPage"))
            {
                Add.Visibility = Visibility.Collapsed;
                Tresh.Visibility = Visibility.Collapsed;
                Pen.Visibility = Visibility.Collapsed;
            }
            else if (currentPage == null || (currentPage != null && currentPage.GetType().Name == "UserPage"))
            {
                Add.Visibility = Visibility.Visible;
                Tresh.Visibility = Visibility.Collapsed;
                Pen.Visibility = Visibility.Collapsed;
            }
            else if (currentPage == null || (currentPage != null && currentPage.GetType().Name == "AdminPage"))
            {
                Add.Visibility = Visibility.Hidden;
                Tresh.Visibility = Visibility.Visible;
                Pen.Visibility = Visibility.Visible;
            }
        }
    }
}
