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
    /// Логика взаимодействия для SongCard.xaml
    /// </summary>
    public partial class SongCard : UserControl
    {
        public SongCard()
        {
            InitializeComponent();
        }

        public event RoutedEventHandler OpenSongMouseClick;
        public event RoutedEventHandler DeleteSongMouseClick;
        public event RoutedEventHandler UpdateSongMouseClick;
        public event RoutedEventHandler AddSongMouseClick;

        private void OpenNote_Click(object sender, RoutedEventArgs e)
        {
            if (OpenSongMouseClick != null)
                OpenSongMouseClick(sender, e);
        }

        private void Add_Checked(object sender, RoutedEventArgs e)
        {
            if (AddSongMouseClick != null)
                AddSongMouseClick(sender, e);
        }

        private void Pen_Click(object sender, RoutedEventArgs e)
        {
            if (UpdateSongMouseClick != null)
                UpdateSongMouseClick(sender, e);
        }

        private void Tresh_Click(object sender, RoutedEventArgs e)
        {
            if (DeleteSongMouseClick != null)
                DeleteSongMouseClick(sender, e);
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
