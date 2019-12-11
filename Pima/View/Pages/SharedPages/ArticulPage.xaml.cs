using Pima.Model;
using Pima.View.Pages.AdminPages;
using Pima.View.Pages.AnonimPages;
using Pima.View.UserControls;
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
using System.Data.Entity;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using Pima.ViewModel;
using System.Windows.Controls.Primitives;

namespace Pima.View.Pages.SharedPages
{
    /// <summary>
    /// Логика взаимодействия для ArticulPage.xaml
    /// </summary>
    public partial class ArticulPage : Page
    {
        OracleDbContext db = null;
        public ArticulPage()
        {
            InitializeComponent();

        }

        private void ArticleCard_OpenArticleMouseClick(object sender, RoutedEventArgs e)
        {
            var currentArticle = (Article)((Button)sender).Tag;
            OneArticle.articleId = currentArticle.ArticleId;
            var currentPage = ((MainWindow)Application.Current.MainWindow).CurrentPage.Content;
            if (currentPage == null || (currentPage != null && currentPage.GetType().Name == "AnonimPage"))
            {
                OneArticle one = new OneArticle();
                one.Title.Text = currentArticle.Title;
                one.Text.Text = currentArticle.Text;
                
                if(currentArticle.Image != null)
                {
                    one.Source.Source = Pima.ViewModel.Converter.ConvertByteArrayToImage(currentArticle.Image);
                }
                else
                {
                    one.Source.Visibility = Visibility.Collapsed;
                }
                (currentPage as AnonimPage).CurrentPage.Navigate(one);
            }
            else if (currentPage == null || (currentPage != null && currentPage.GetType().Name == "UserPage"))
            {
                OneArticle one = new OneArticle();
                one.Title.Text = currentArticle.Title;
                one.Text.Text = currentArticle.Text;
                if (currentArticle.Image != null)
                {
                    one.Source.Source = Pima.ViewModel.Converter.ConvertByteArrayToImage(currentArticle.Image);
                }
                else
                {
                    one.Source.Visibility = Visibility.Collapsed;
                }
                (currentPage as UserPages.UserPage).CurrentPage.Navigate(one);
            }
            else if (currentPage == null || (currentPage != null && currentPage.GetType().Name == "AdminPage"))
            {
                OneArticle one = new OneArticle();
                one.Title.Text = currentArticle.Title;
                one.Text.Text = currentArticle.Text;
                if (currentArticle.Image != null)
                {
                    one.Source.Source = Pima.ViewModel.Converter.ConvertByteArrayToImage(currentArticle.Image);
                }
                else
                {
                    one.Source.Visibility = Visibility.Collapsed;
                }
                (currentPage as AdminPage).CurrentPage.Navigate(one);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            db = new OracleDbContext();
            db.Articles.Load();
            SearchResultList.ItemsSource = db.Articles.Local;

            var currentPage = ((MainWindow)Application.Current.MainWindow).CurrentPage.Content;
            if (currentPage == null || (currentPage != null && currentPage.GetType().Name == "AnonimPage"))
            {
                New.Visibility = Visibility.Collapsed;
            }
            else if (currentPage == null || (currentPage != null && currentPage.GetType().Name == "UserPage"))
            {
                New.Visibility = Visibility.Collapsed;
            }
            else if (currentPage == null || (currentPage != null && currentPage.GetType().Name == "AdminPage"))
            {
                New.Visibility = Visibility.Visible;
            }
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            Article article1 = new Article();
            ArticleCard article = new ArticleCard()
            {
                Margin = new Thickness(25)
            };
            article.OpenArticleMouseClick += ArticleCard_OpenArticleMouseClick;
            article.DeleteArticleMouseClick += Add_DeleteArticleMouseClick;
            article.UpdateArticleMouseClick += Add_UpdateArticleMouseClick;
            var currentPage = ((MainWindow)Application.Current.MainWindow).CurrentPage.Content;
            if (currentPage == null || (currentPage != null && currentPage.GetType().Name == "AnonimPage"))
            {
                article.Add.Visibility = Visibility.Hidden;
                article.Tresh.Visibility = Visibility.Hidden;
                article.Pen.Visibility = Visibility.Hidden;
                article.Visibility = Visibility.Collapsed;
            }
            else if (currentPage == null || (currentPage != null && currentPage.GetType().Name == "UserPage"))
            {
                article.Add.Visibility = Visibility.Visible;
                article.Tresh.Visibility = Visibility.Collapsed;
                article.Pen.Visibility = Visibility.Collapsed;
                article.Visibility = Visibility.Collapsed;
            }
            else if (currentPage == null || (currentPage != null && currentPage.GetType().Name == "AdminPage"))
            {
                article.Add.Visibility = Visibility.Hidden;
                article.Tresh.Visibility = Visibility.Visible;
                article.Pen.Visibility = Visibility.Visible;
                article.Visibility = Visibility.Visible;
            }

            db.Articles.Add(article1);
            db.SaveChanges();
            db.Articles.Load();
            SearchResultList.ItemsSource = db.Articles.Local;
        }

        private void Add_UpdateArticleMouseClick(object sender, RoutedEventArgs e)
        {
            var currentArticle = (Article)((Button)sender).Tag;
            var currentPage = ((MainWindow)Application.Current.MainWindow).CurrentPage.Content;
            OneArticle.articleId = currentArticle.ArticleId;
            OneArticle one = new OneArticle();
            one.Title.Text = currentArticle.Title;
            one.Text.Text = currentArticle.Text;
            if (currentArticle.Image != null)
            {
                one.Source.Source = Pima.ViewModel.Converter.ConvertByteArrayToImage(currentArticle.Image);
            }
            else
            {
                one.Source.Visibility = Visibility.Collapsed;
            }
                (currentPage as AdminPage).CurrentPage.Navigate(one);

            one.Title.Visibility = Visibility.Collapsed;
            one.TitleTextBox.Visibility = Visibility.Visible;
            if(currentArticle.Title == null && currentArticle.Text == null)
            {
                one.TitleTextBox.Text = "Title";
                one.TextEditor.Text = "Text";
            }

            one.Text.Visibility = Visibility.Collapsed;
            one.TextEditor.Visibility = Visibility.Visible;

            one.SaveStackPanel.Visibility = Visibility.Visible;

            one.NewImage.Visibility = Visibility.Visible;

            //one.ID.Content = currentArticle.ArticleId.ToString();
        }

        private void Add_DeleteArticleMouseClick(object sender, RoutedEventArgs e)
        {
            var currentArticle = (Article)((Button)sender).Tag;
            db = new OracleDbContext();
            var ArticleId = new OracleParameter("ArticleId", OracleDbType.Int32, currentArticle.ArticleId, ParameterDirection.Input);
            var sql = "BEGIN ARTICLEDELETE(:ArticleId); END;";
            var update = db.Database.ExecuteSqlCommand(sql, ArticleId);
            db.Articles.Load();
            SearchResultList.ItemsSource = db.Articles.Local;
        }

        private void Add_AddArticleMouseClick(object sender, RoutedEventArgs e)
        {
            var currentArticle = (Article)((ToggleButton)sender).Tag;
            db = new OracleDbContext();
            var ArticlesId = new OracleParameter("ArticlesId", OracleDbType.Int32, currentArticle.ArticleId, ParameterDirection.Input);
            var UserId = new OracleParameter("UserId", OracleDbType.Int32, CurrentUser.User.UserId, ParameterDirection.Input);
            var sql = "BEGIN ADDARTICLEUSER(:UserId, :ArticlesId); END;";
            var update = db.Database.ExecuteSqlCommand(sql, UserId, ArticlesId);

            MainWindow.SnackbarMessage.Content = "Статья добавленна!";
            MainWindow.Snackbar.IsActive = true;
        }

        private void Add_UncheckedArticleMouseClick(object sender, RoutedEventArgs e)
        {
            //OracleDbContext db = new OracleDbContext();
            //var currentArticle = (ArticlesUser)((ToggleButton)sender).Tag;
            //var ArticleId = new OracleParameter("ArticlesUserId", OracleDbType.Int32, currentArticle.ArticlesUserId, ParameterDirection.Input);
            //var sql = "BEGIN ARTICLEUSERDELETE(:ArticlesUserId); END;";
            //var update = db.Database.ExecuteSqlCommand(sql, ArticleId);
            //db.Articles.Load();
            //int UserId = CurrentUser.User.UserId;
            //var art = db.ArticlesUsers.Where(x => x.UserId_ArticlesUser == UserId);

            //SearchResultList.ItemsSource = art.ToList();
        }
    }
}
