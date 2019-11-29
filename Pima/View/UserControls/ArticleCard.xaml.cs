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
    /// Логика взаимодействия для ArticleCard.xaml
    /// </summary>
    public partial class ArticleCard : UserControl
    {
        public ArticleCard()
        {
            InitializeComponent();
        }

        public event RoutedEventHandler OpenArticleMouseClick;
        public event RoutedEventHandler DeleteArticleMouseClick;
        public event RoutedEventHandler UpdateArticleMouseClick;
        public event RoutedEventHandler AddArticleMouseClick;
        public event RoutedEventHandler UncheckedArticleMouseClick;

        private void OpenArticle_Click(object sender, RoutedEventArgs e)
        {
            if (OpenArticleMouseClick != null)
                OpenArticleMouseClick(sender, e);
        }

        private void Tresh_Click(object sender, RoutedEventArgs e)
        {
            if (DeleteArticleMouseClick != null)
                DeleteArticleMouseClick(sender, e);
        }

        private void Pen_Click(object sender, RoutedEventArgs e)
        {
            if (UpdateArticleMouseClick != null)
                UpdateArticleMouseClick(sender, e);
        }

        private void Add_Checked(object sender, RoutedEventArgs e)
        {
            if (AddArticleMouseClick != null)
                AddArticleMouseClick(sender, e);
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

        private void Add_Unchecked(object sender, RoutedEventArgs e)
        {
            if (UncheckedArticleMouseClick != null)
                UncheckedArticleMouseClick(sender, e);
        }
    }
}
