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
    /// Логика взаимодействия для ChordSongCard.xaml
    /// </summary>
    public partial class ChordSongCard : UserControl
    {
        public ChordSongCard()
        {
            InitializeComponent();
        }

        public event RoutedEventHandler DeleteChordSongMouseClick;

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            if (DeleteChordSongMouseClick != null)
                DeleteChordSongMouseClick(sender, e);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var currentPage = ((MainWindow)Application.Current.MainWindow).CurrentPage.Content;
            if (currentPage == null || (currentPage != null && (currentPage.GetType().Name == "AnonimPage" || currentPage.GetType().Name == "UserPage")))
            {
                ButtonClose.Visibility = Visibility.Collapsed;
            }
            else if (currentPage == null || (currentPage != null && currentPage.GetType().Name == "AdminPage"))
            {
                ButtonClose.Visibility = Visibility.Visible;
            }
        }
    }
}
