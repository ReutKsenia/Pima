using Oracle.ManagedDataAccess.Client;
using Pima.Model;
using Pima.View.Pages.AdminPages;
using Pima.View.Pages.AnonimPages;
using Pima.View.UserControls;
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
using System.Windows.Controls.Primitives;
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
    /// Логика взаимодействия для TABsPage.xaml
    /// </summary>
    public partial class TABsPage : Page
    {
        OracleDbContext db = null;
        public TABsPage()
        {
            InitializeComponent();
        }

        private void TABs_OpenTABMouseClick(object sender, RoutedEventArgs e)
        {
            var currentTAB = (TABs)((Button)sender).Tag;

            var currentPage = ((MainWindow)Application.Current.MainWindow).CurrentPage.Content;
            if (currentPage == null || (currentPage != null && currentPage.GetType().Name == "AnonimPage"))
            {
                OneTAB one = new OneTAB();
                one.Name.Text = currentTAB.Name;
                one.Author.Text = currentTAB.Author;
                if (currentTAB.TAB != null)
                {
                    one.Source.Source = Pima.ViewModel.Converter.ConvertByteArrayToImage(currentTAB.TAB);
                }
                else
                {
                    one.Source.Visibility = Visibility.Collapsed;
                }
                if (currentTAB.Description != null)
                {
                    one.Description.Text = currentTAB.Description;
                }
                else
                {
                    one.Description.Visibility = Visibility.Collapsed;
                }
                (currentPage as AnonimPage).CurrentPage.Navigate(one);
            }
            else if (currentPage == null || (currentPage != null && currentPage.GetType().Name == "UserPage"))
            {
                OneTAB one = new OneTAB();
                one.Name.Text = currentTAB.Name;
                one.Author.Text = currentTAB.Author;
                if (currentTAB.TAB != null)
                {
                    one.Source.Source = Pima.ViewModel.Converter.ConvertByteArrayToImage(currentTAB.TAB);
                }
                else
                {
                    one.Source.Visibility = Visibility.Collapsed;
                }
                if (currentTAB.Description != null)
                {
                    one.Description.Text = currentTAB.Description;
                }
                else
                {
                    one.Description.Visibility = Visibility.Collapsed;
                }
                (currentPage as UserPages.UserPage).CurrentPage.Navigate(one);
            }
            else if (currentPage == null || (currentPage != null && currentPage.GetType().Name == "AdminPage"))
            {
                OneTAB one = new OneTAB();
                one.Name.Text = currentTAB.Name;
                one.Author.Text = currentTAB.Author;
                if (currentTAB.TAB != null)
                {
                    one.Source.Source = Pima.ViewModel.Converter.ConvertByteArrayToImage(currentTAB.TAB);
                }
                else
                {
                    one.Source.Visibility = Visibility.Collapsed;
                }
                if (currentTAB.Description != null)
                {
                    one.Description.Text = currentTAB.Description;
                }
                else
                {
                    one.Description.Visibility = Visibility.Collapsed;
                }
                (currentPage as AdminPage).CurrentPage.Navigate(one);
            }
        }

        private void TABs_UpdateTABMouseClick(object sender, RoutedEventArgs e)
        {
            var currentTAB = (TABs)((Button)sender).Tag;
            var currentPage = ((MainWindow)Application.Current.MainWindow).CurrentPage.Content;
            OneTAB one = new OneTAB();
            one.Name.Text = currentTAB.Name;
            one.Author.Text = currentTAB.Author;
            if (currentTAB.TAB != null)
            {
                one.Source.Source = Pima.ViewModel.Converter.ConvertByteArrayToImage(currentTAB.TAB);
            }
            else
            {
                one.Source.Visibility = Visibility.Collapsed;
            }
            if (currentTAB.Description != null)
            {
                one.Description.Text = currentTAB.Description;
            }
            else
            {
                one.Description.Visibility = Visibility.Collapsed;
            }
                (currentPage as AdminPage).CurrentPage.Navigate(one);

            one.Name.Visibility = Visibility.Collapsed;
            one.NameTextBox.Visibility = Visibility.Visible;
            if (currentTAB.Name == null && currentTAB.Author == null && currentTAB.Description == null)
            {
                one.NameTextBox.Text = "Name";
                one.AuthorTextBox.Text = "Author";
                one.DescriptionEditor.Text = "Description";
            }
            one.Author.Visibility = Visibility.Collapsed;
            one.AuthorTextBox.Visibility = Visibility.Visible;
            one.Description.Visibility = Visibility.Collapsed;
            one.DescriptionEditor.Visibility = Visibility.Visible;

            one.SaveStackPanel.Visibility = Visibility.Visible;

            one.NewImage.Visibility = Visibility.Visible;

            one.ID.Content = currentTAB.TABsId.ToString();
        }

        private void TABs_DeleteTABMouseClick(object sender, RoutedEventArgs e)
        {
            var currentTAB = (TABs)((Button)sender).Tag;
            db = new OracleDbContext();
            var TABsId = new OracleParameter("TABsId", OracleDbType.Int32, currentTAB.TABsId, ParameterDirection.Input);
            var sql = "BEGIN TABSDELETE(:TABsId); END;";
            var update = db.Database.ExecuteSqlCommand(sql, TABsId);
            db.Notes.Load();
            SearchResultList.ItemsSource = db.TABs.Local;
        }

        private void TABs_AddTABMouseClick(object sender, RoutedEventArgs e)
        {
            var currentTAB = (TABs)((ToggleButton)sender).Tag;
            db = new OracleDbContext();
            var TABsId = new OracleParameter("TABsId", OracleDbType.Int32, currentTAB.TABsId, ParameterDirection.Input);
            var UserId = new OracleParameter("UserId", OracleDbType.Int32, CurrentUser.User.UserId, ParameterDirection.Input);
            var sql = "BEGIN ADDTABSUSER(:UserId, :TABsId); END;";
            var update = db.Database.ExecuteSqlCommand(sql, UserId, TABsId);
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            TABs tab1 = new TABs();
            TABsCard tab = new TABsCard()
            {
                Margin = new Thickness(25)
            };
            var currentPage = ((MainWindow)Application.Current.MainWindow).CurrentPage.Content;
            if (currentPage == null || (currentPage != null && currentPage.GetType().Name == "AnonimPage"))
            {
                tab.Add.Visibility = Visibility.Hidden;
                tab.Tresh.Visibility = Visibility.Hidden;
                tab.Pen.Visibility = Visibility.Hidden;
                tab.Visibility = Visibility.Collapsed;
            }
            else if (currentPage == null || (currentPage != null && currentPage.GetType().Name == "UserPage"))
            {
                tab.Add.Visibility = Visibility.Visible;
                tab.Tresh.Visibility = Visibility.Collapsed;
                tab.Pen.Visibility = Visibility.Collapsed;
                tab.Visibility = Visibility.Collapsed;
            }
            else if (currentPage == null || (currentPage != null && currentPage.GetType().Name == "AdminPage"))
            {
                tab.Add.Visibility = Visibility.Hidden;
                tab.Tresh.Visibility = Visibility.Visible;
                tab.Pen.Visibility = Visibility.Visible;
                tab.Visibility = Visibility.Visible;
            }

            db.TABs.Add(tab1);
            db.SaveChanges();
            db.TABs.Load();
            SearchResultList.ItemsSource = db.TABs.Local;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            db = new OracleDbContext();
            db.TABs.Load();
            SearchResultList.ItemsSource = db.TABs.Local;

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
    }
}
