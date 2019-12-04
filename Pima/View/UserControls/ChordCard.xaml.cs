using Pima.Model;
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
    /// Логика взаимодействия для ChordCard.xaml
    /// </summary>
    public partial class ChordCard : UserControl
    {
        public ChordCard()
        {
            
            
            InitializeComponent();
        }

        public event RoutedEventHandler DeleteChordMouseClick;
        public event RoutedEventHandler UpdateChordMouseClick;
        public event RoutedEventHandler AddChordMouseClick;



        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var currentPage = ((MainWindow)Application.Current.MainWindow).CurrentPage.Content;
            if (currentPage == null || (currentPage != null && currentPage.GetType().Name == "AnonimPage"))
            {
                //Add.Visibility = Visibility.Collapsed;
                Panel.Visibility = Visibility.Collapsed;
                Tresh.Visibility = Visibility.Collapsed;
                Pen.Visibility = Visibility.Collapsed;
            }
            else if (currentPage == null || (currentPage != null && currentPage.GetType().Name == "UserPage"))
            {
                //Add.Visibility = Visibility.Collapsed;
                Panel.Visibility = Visibility.Collapsed;
                Tresh.Visibility = Visibility.Collapsed;
                Pen.Visibility = Visibility.Collapsed;
            }
            else if (currentPage == null || (currentPage != null && currentPage.GetType().Name == "AdminPage"))
            {
                //Add.Visibility = Visibility.Collapsed;
                Panel.Visibility = Visibility.Visible;
                Tresh.Visibility = Visibility.Visible;
                Pen.Visibility = Visibility.Visible;
            }
        }

        private void AddChord_Click(object sender, RoutedEventArgs e)
        {
            if (AddChordMouseClick != null)
                AddChordMouseClick(sender, e);
        }

        private void Pen_Click(object sender, RoutedEventArgs e)
        {
            if (UpdateChordMouseClick != null)
                UpdateChordMouseClick(sender, e);
        }

        private void Tresh_Click(object sender, RoutedEventArgs e)
        {
            if (DeleteChordMouseClick != null)
                DeleteChordMouseClick(sender, e);
        }
    }
}
