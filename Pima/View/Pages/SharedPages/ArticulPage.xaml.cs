using Pima.View.Pages.AnonimPages;
using Pima.View.Windows;
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

namespace Pima.View.Pages.SharedPages
{
    /// <summary>
    /// Логика взаимодействия для ArticulPage.xaml
    /// </summary>
    public partial class ArticulPage : Page
    {
        public ArticulPage()
        {
            InitializeComponent();
        }

        private void ArticleCard_OpenArticleMouseClick(object sender, RoutedEventArgs e)
        {
            var currentPage = ((MainWindow)Application.Current.MainWindow).CurrentPage.Content;
            if (currentPage == null || (currentPage != null && currentPage.GetType().Name != "AnonimPage"))
            {
                OneArticle one = new OneArticle();
                (currentPage as UserPages.UserPage).CurrentPage.Navigate(one);
            }
            else if (currentPage == null || (currentPage != null && currentPage.GetType().Name != "UserPage"))
            {
                OneArticle one = new OneArticle();
                (currentPage as AnonimPage).CurrentPage.Navigate(one);
            }
        }
    }
}
