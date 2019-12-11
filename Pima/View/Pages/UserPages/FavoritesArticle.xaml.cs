using Oracle.ManagedDataAccess.Client;
using Pima.Model;
using Pima.View.Pages.SharedPages;
using Pima.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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

namespace Pima.View.Pages.UserPages
{
    /// <summary>
    /// Логика взаимодействия для FavoritesArticle.xaml
    /// </summary>
    public partial class FavoritesArticle : Page
    {
        public FavoritesArticle()
        {
            InitializeComponent();
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            OracleDbContext db = new OracleDbContext();
            int UserId = CurrentUser.User.UserId;
            var art = db.ArticlesUsers.Where(x => x.UserId_ArticlesUser == UserId);

            SearchResultList.ItemsSource = art.ToList();

        }

        private void Tresh_Click(object sender, RoutedEventArgs e)
        {
            OracleDbContext db = new OracleDbContext();
            var currentArticle = (ArticlesUser)((Button)sender).Tag;
            var ArticleId = new OracleParameter("ArticlesUserId", OracleDbType.Int32, currentArticle.ArticlesUserId, ParameterDirection.Input);
            var sql = "BEGIN ARTICLEUSERDELETE(:ArticlesUserId); END;";
            var update = db.Database.ExecuteSqlCommand(sql, ArticleId);
            db.Articles.Load();
            int UserId = CurrentUser.User.UserId;
            var art = db.ArticlesUsers.Where(x => x.UserId_ArticlesUser == UserId);

            SearchResultList.ItemsSource = art.ToList();
        }

        private void OpenArticle_Click(object sender, RoutedEventArgs e)
        {
            OracleDbContext db = new OracleDbContext();
            int UserId = CurrentUser.User.UserId;
            var currentArticle = (ArticlesUser)((Button)sender).Tag;
            var art = db.Articles.FirstOrDefault(x => x.ArticleId == currentArticle.ArticleId_ArticlesUser);

            var currentPage = ((MainWindow)Application.Current.MainWindow).CurrentPage.Content;
            if (currentPage == null || (currentPage != null && currentPage.GetType().Name == "UserPage"))
            {
                OneArticle one = new OneArticle();
                one.Title.Text = art.Title;
                one.Text.Text = art.Text;
                if (art.Image != null)
                {
                    one.Source.Source = Pima.ViewModel.Converter.ConvertByteArrayToImage(art.Image);
                }
                else
                {
                    one.Source.Visibility = Visibility.Collapsed;
                }
                (currentPage as UserPages.UserPage).CurrentPage.Navigate(one);
            }
        }
    }
}
