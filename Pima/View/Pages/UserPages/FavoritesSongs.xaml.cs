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
    /// Логика взаимодействия для FavoritesSongs.xaml
    /// </summary>
    public partial class FavoritesSongs : Page
    {
        public FavoritesSongs()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            OracleDbContext db = new OracleDbContext();
            int UserId = CurrentUser.User.UserId;
            var note = db.SongsUsers.Where(x => x.UserId_SongsUser == UserId);

            SearchResultList.ItemsSource = note.ToList();
        }

        private void Tresh_Click(object sender, RoutedEventArgs e)
        {
            OracleDbContext db = new OracleDbContext();
            var currentSong = (SongsUser)((Button)sender).Tag;
            var SongsUserId = new OracleParameter("SongsUserId", OracleDbType.Int32, currentSong.SongsUserId , ParameterDirection.Input);
            var sql = "BEGIN SONGSUSERDELETE(:SongsUserId); END;";
            var update = db.Database.ExecuteSqlCommand(sql, SongsUserId);
            db.Songs.Load();
            int UserId = CurrentUser.User.UserId;
            var song = db.SongsUsers.Where(x => x.UserId_SongsUser == UserId);

            SearchResultList.ItemsSource = song.ToList();
        }

        private void OpenNote_Click(object sender, RoutedEventArgs e)
        {
            OracleDbContext db = new OracleDbContext();
            int UserId = CurrentUser.User.UserId;
            var currentSong = (SongsUser)((Button)sender).Tag;
            var song = db.Songs.FirstOrDefault(x => x.SongsId == currentSong.SongId_SongsUser);
            OneSong.songID = song.SongsId;
            SongsPage.musicID = song.SongsId;
            var currentPage = ((MainWindow)Application.Current.MainWindow).CurrentPage.Content;
            if (currentPage == null || (currentPage != null && currentPage.GetType().Name == "UserPage"))
            {
                OneSong one = new OneSong();
                one.Name.Text = song.Name;
                one.Author.Text = song.Author;
                one.Text.Text = song.Text;
                one.ChordsComboBox.Visibility = Visibility.Collapsed;
                if (song.Music != null)
                {
                    one.Music.Visibility = Visibility.Visible;
                }
                else
                {
                    one.Music.Visibility = Visibility.Collapsed;
                }
                if (song.Image != null)
                {
                    one.Source.Source = Pima.ViewModel.Converter.ConvertByteArrayToImage(song.Image);
                }
                else
                {
                    one.Source.Visibility = Visibility.Collapsed;
                }
                if (song.Description != null)
                {
                    one.Description.Text = song.Description;
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
