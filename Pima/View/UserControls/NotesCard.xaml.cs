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
    /// Логика взаимодействия для NotesCard.xaml
    /// </summary>
    public partial class NotesCard : UserControl
    {
        public NotesCard()
        {
            InitializeComponent();
        }

        public event RoutedEventHandler OpenNoteMouseClick;
        public event RoutedEventHandler DeleteNoteMouseClick;
        public event RoutedEventHandler UpdateNoteMouseClick;
        public event RoutedEventHandler AddNoteMouseClick;
        public event RoutedEventHandler UncheckedNoteMouseClick;

        private void OpenNote_Click(object sender, RoutedEventArgs e)
        {
            if (OpenNoteMouseClick != null)
                OpenNoteMouseClick(sender, e);
        }

        private void Add_Checked(object sender, RoutedEventArgs e)
        {
            if (AddNoteMouseClick != null)
                AddNoteMouseClick(sender, e);
        }

        private void Add_Unchecked(object sender, RoutedEventArgs e)
        {
            if (UncheckedNoteMouseClick != null)
                UncheckedNoteMouseClick(sender, e);
        }

        private void Pen_Click(object sender, RoutedEventArgs e)
        {
            if (UpdateNoteMouseClick != null)
                UpdateNoteMouseClick(sender, e);
        }

        private void Tresh_Click(object sender, RoutedEventArgs e)
        {
            if (DeleteNoteMouseClick != null)
                DeleteNoteMouseClick(sender, e);
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
