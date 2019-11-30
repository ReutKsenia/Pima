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
    /// Логика взаимодействия для FavoritesTABs.xaml
    /// </summary>
    public partial class FavoritesTABs : Page
    {
        public FavoritesTABs()
        {
            InitializeComponent();
        }

        private void OpenNote_Click(object sender, RoutedEventArgs e)
        {
            OracleDbContext db = new OracleDbContext();
            int UserId = CurrentUser.User.UserId;
            var currentTAB = (TABsUser)((Button)sender).Tag;
            var tab = db.TABs.FirstOrDefault(x => x.TABsId == currentTAB.TABsId_TABsUser);

            var currentPage = ((MainWindow)Application.Current.MainWindow).CurrentPage.Content;
            if (currentPage == null || (currentPage != null && currentPage.GetType().Name == "UserPage"))
            {
                OneTAB one = new OneTAB();
                one.Name.Text = tab.Name;
                one.Author.Text = tab.Author;
                if (tab.TAB != null)
                {
                    one.Source.Source = Pima.ViewModel.Converter.ConvertByteArrayToImage(tab.TAB);
                }
                else
                {
                    one.Source.Visibility = Visibility.Collapsed;
                }
                if (tab.Description != null)
                {
                    one.Description.Text = tab.Description;
                }
                else
                {
                    one.Description.Visibility = Visibility.Collapsed;
                }
                (currentPage as UserPages.UserPage).CurrentPage.Navigate(one);
            }
        }

        private void Tresh_Click(object sender, RoutedEventArgs e)
        {
            OracleDbContext db = new OracleDbContext();
            var currentTAB = (TABsUser)((Button)sender).Tag;
            var TABsUserId = new OracleParameter("TABsUserId", OracleDbType.Int32, currentTAB.TABsUserId, ParameterDirection.Input);
            var sql = "BEGIN TABSUSERDELETE(:TABsUserId); END;";
            var update = db.Database.ExecuteSqlCommand(sql, TABsUserId);
            db.TABs.Load();
            int UserId = CurrentUser.User.UserId;
            var tab = db.TABsUsers.Where(x => x.UserId_TABsUser == UserId);

            SearchResultList.ItemsSource = tab.ToList();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            OracleDbContext db = new OracleDbContext();
            int UserId = CurrentUser.User.UserId;
            var tab = db.TABsUsers.Where(x => x.UserId_TABsUser == UserId);

            SearchResultList.ItemsSource = tab.ToList();
        }
    }
}
