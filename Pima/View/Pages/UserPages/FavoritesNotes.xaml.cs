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
    /// Логика взаимодействия для FavoritesNotes.xaml
    /// </summary>
    public partial class FavoritesNotes : Page
    {
        public FavoritesNotes()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            OracleDbContext db = new OracleDbContext();
            int UserId = CurrentUser.User.UserId;
            var note = db.NotesUsers.Where(x => x.UserId_NotesUser == UserId);

            SearchResultList.ItemsSource = note.ToList();
        }

        private void Tresh_Click(object sender, RoutedEventArgs e)
        {
            OracleDbContext db = new OracleDbContext();
            var currentNote = (NotesUser)((Button)sender).Tag;
            var NotesUserId = new OracleParameter("NotesUserId", OracleDbType.Int32, currentNote.NotesUserId, ParameterDirection.Input);
            var sql = "BEGIN NOTESUSERDELETE(:NotesUserId); END;";
            var update = db.Database.ExecuteSqlCommand(sql, NotesUserId);
            db.Notes.Load();
            int UserId = CurrentUser.User.UserId;
            var note = db.NotesUsers.Where(x => x.UserId_NotesUser == UserId);

            SearchResultList.ItemsSource = note.ToList();
        }

        private void OpenNote_Click(object sender, RoutedEventArgs e)
        {
            OracleDbContext db = new OracleDbContext();
            int UserId = CurrentUser.User.UserId;
            var currentNote = (NotesUser)((Button)sender).Tag;
            var note = db.Notes.FirstOrDefault(x => x.NotesId == currentNote.NotesId_NotesUser);

            var currentPage = ((MainWindow)Application.Current.MainWindow).CurrentPage.Content;
            if (currentPage == null || (currentPage != null && currentPage.GetType().Name == "UserPage"))
            {
                OneNote one = new OneNote();
                one.Name.Text = note.Name;
                one.Author.Text = note.Author;
                if (note.Note != null)
                {
                    one.Source.Source = Pima.ViewModel.Converter.ConvertByteArrayToImage(note.Note);
                }
                else
                {
                    one.Source.Visibility = Visibility.Collapsed;
                }
                if (note.Description != null)
                {
                    one.Description.Text = note.Description;
                }
                else
                {
                    one.Description.Visibility = Visibility.Collapsed;
                }
                (currentPage as UserPages.UserPage).CurrentPage.Navigate(one);
            }
        }
    }
}
