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
using System.IO;
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
    /// Логика взаимодействия для SongsPage.xaml
    /// </summary>
    public partial class SongsPage : Page
    {
        OracleDbContext db = null;
        public static int musicID;
        //public static int mediaID = 11;
        public static string CurrDir = Environment.CurrentDirectory; // получаем текущую директорую
        public SongsPage()
        {
            InitializeComponent();
        }

        

        private void Song_OpenSongMouseClick(object sender, RoutedEventArgs e)
        {
            var currentSong = (Songs)((Button)sender).Tag;
            musicID = currentSong.SongsId;
            OneSong.songID = currentSong.SongsId;
            var currentPage = ((MainWindow)Application.Current.MainWindow).CurrentPage.Content;
            if (currentPage == null || (currentPage != null && currentPage.GetType().Name == "AnonimPage"))
            {
                OneSong one = new OneSong();
                one.Name.Text = currentSong.Name;
                one.Author.Text = currentSong.Author;
                one.Text.Text = currentSong.Text;
                one.ChordsComboBox.Visibility = Visibility.Collapsed;
                one.Source.Visibility = Visibility.Collapsed;
                if (currentSong.Music != null)
                {
                    one.Music.Visibility = Visibility.Visible;
                }
                else
                {
                    one.Music.Visibility = Visibility.Collapsed;
                }
                if (currentSong.Image != null)
                {
                    one.Source.Source = Pima.ViewModel.Converter.ConvertByteArrayToImage(currentSong.Image);
                }
                else
                {
                    one.Source.Visibility = Visibility.Collapsed;
                }
                if (currentSong.Description != null)
                {
                    one.Description.Text = currentSong.Description;
                }
                else
                {
                    one.Description.Visibility = Visibility.Collapsed;
                }
                (currentPage as AnonimPage).CurrentPage.Navigate(one);
            }
            else if (currentPage == null || (currentPage != null && currentPage.GetType().Name == "UserPage"))
            {
                OneSong one = new OneSong();
                one.Name.Text = currentSong.Name;
                one.Author.Text = currentSong.Author;
                one.Text.Text = currentSong.Text;
                one.ChordsComboBox.Visibility = Visibility.Collapsed;
                one.Source.Visibility = Visibility.Collapsed;
                if (currentSong.Music != null)
                {
                    one.Music.Visibility = Visibility.Visible;
                }
                else
                {
                    one.Music.Visibility = Visibility.Collapsed;
                }
                if (currentSong.Image != null)
                {
                    one.Source.Source = Pima.ViewModel.Converter.ConvertByteArrayToImage(currentSong.Image);
                }
                else
                {
                    one.Source.Visibility = Visibility.Collapsed;
                }
                if (currentSong.Description != null)
                {
                    one.Description.Text = currentSong.Description;
                }
                else
                {
                    one.Description.Visibility = Visibility.Collapsed;
                }
                (currentPage as UserPages.UserPage).CurrentPage.Navigate(one);
            }
            else if (currentPage == null || (currentPage != null && currentPage.GetType().Name == "AdminPage"))
            {
                OneSong one = new OneSong();
                one.Name.Text = currentSong.Name;
                one.Author.Text = currentSong.Author;
                one.Text.Text = currentSong.Text;
                one.ChordsComboBox.Visibility = Visibility.Collapsed;
                one.Source.Visibility = Visibility.Collapsed;
                if (currentSong.Music != null)
                {
                    one.Music.Visibility = Visibility.Visible;
                }
                else
                {
                    one.Music.Visibility = Visibility.Collapsed;
                }
                if (currentSong.Image != null)
                {
                    one.Source.Source = Pima.ViewModel.Converter.ConvertByteArrayToImage(currentSong.Image);
                }
                else
                {
                    one.Source.Visibility = Visibility.Collapsed;
                }
                if (currentSong.Description != null)
                {
                    one.Description.Text = currentSong.Description;
                }
                else
                {
                    one.Description.Visibility = Visibility.Collapsed;
                }
                (currentPage as AdminPage).CurrentPage.Navigate(one);
            }
        }

        private void Song_UpdateSongMouseClick(object sender, RoutedEventArgs e)
        {
            OracleDbContext db = new OracleDbContext();
            var currentSong = (Songs)((Button)sender).Tag;
            musicID = currentSong.SongsId;
            OneSong.songID = currentSong.SongsId;
            var currentPage = ((MainWindow)Application.Current.MainWindow).CurrentPage.Content;
            OneSong one = new OneSong();
            one.Name.Text = currentSong.Name;
            one.Author.Text = currentSong.Author;
            one.Text.Text = currentSong.Text;
            one.ChordsComboBox.Visibility = Visibility.Visible;
            one.Source.Visibility = Visibility.Visible;
            if (currentSong.Music != null)
            {
                //CreateFileMP3();
                //one.MediaElem.Source = new Uri("music.mp3", UriKind.Relative);
                one.Music.Visibility = Visibility.Visible;
            }
            else
            {
                one.Music.Visibility = Visibility.Collapsed;
            }
            if (currentSong.Image != null)
            {
                one.Source.Source = Pima.ViewModel.Converter.ConvertByteArrayToImage(currentSong.Image);
            }
            else
            {
                one.Source.Visibility = Visibility.Collapsed;
            }
            if (currentSong.Description != null)
            {
                one.Description.Text = currentSong.Description;
            }
            else
            {
                one.Description.Visibility = Visibility.Collapsed;
            }
                (currentPage as AdminPage).CurrentPage.Navigate(one);

            one.Name.Visibility = Visibility.Collapsed;
            one.NameTextBox.Visibility = Visibility.Visible;
            if (currentSong.Name == null && currentSong.Author == null && currentSong.Description == null && currentSong.Text == null)
            {
                one.NameTextBox.Text = "Name";
                one.AuthorTextBox.Text = "Author";
                one.DescriptionEditor.Text = "Description";
                one.TextEditor.Text = "Text";
            }
            one.Author.Visibility = Visibility.Collapsed;
            one.AuthorTextBox.Visibility = Visibility.Visible;
            one.Text.Visibility = Visibility.Collapsed;
            one.TextEditor.Visibility = Visibility.Visible;
            one.Description.Visibility = Visibility.Collapsed;
            one.DescriptionEditor.Visibility = Visibility.Visible;
            one.MusicAdd.Visibility = Visibility.Visible;
            one.AddImageButton.Visibility = Visibility.Visible;
            one.ChordSongCard.Visibility = Visibility.Visible;

            one.SaveStackPanel.Visibility = Visibility.Visible;

            one.NewImage.Visibility = Visibility.Visible;

            OneSong.songID = currentSong.SongsId;
        }

        private void Song_DeleteSongMouseClick(object sender, RoutedEventArgs e)
        {
            var currentSong = (Songs)((Button)sender).Tag;
            db = new OracleDbContext();
            var SongssId = new OracleParameter("SongsId", OracleDbType.Int32, currentSong.SongsId, ParameterDirection.Input);
            var sql = "BEGIN SONGSDELETE(:SongsId); END;";
            var update = db.Database.ExecuteSqlCommand(sql, SongssId);
            db.Songs.Load();
            SearchResultList.ItemsSource = db.Songs.Local;
        }

        private void Song_AddSongMouseClick(object sender, RoutedEventArgs e)
        {
            var currentSong = (Songs)((ToggleButton)sender).Tag;
            db = new OracleDbContext();
            var SongsId = new OracleParameter("SongsId", OracleDbType.Int32, currentSong.SongsId, ParameterDirection.Input);
            var UserId = new OracleParameter("UserId", OracleDbType.Int32, CurrentUser.User.UserId, ParameterDirection.Input);
            var sql = "BEGIN ADDSONGSUSER(:UserId, :SongsId); END;";
            var update = db.Database.ExecuteSqlCommand(sql, UserId, SongsId);

            MainWindow.SnackbarMessage.Content = "Песня добавлена!";
            MainWindow.Snackbar.IsActive = true;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            db = new OracleDbContext();
            db.Songs.Load();
            SearchResultList.ItemsSource = db.Songs.Local;

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
            Songs song1 = new Songs();
            SongCard song = new SongCard()
            {
                Margin = new Thickness(25)
            };
            var currentPage = ((MainWindow)Application.Current.MainWindow).CurrentPage.Content;
            if (currentPage == null || (currentPage != null && currentPage.GetType().Name == "AnonimPage"))
            {
                song.Add.Visibility = Visibility.Hidden;
                song.Tresh.Visibility = Visibility.Hidden;
                song.Pen.Visibility = Visibility.Hidden;
                song.Visibility = Visibility.Collapsed;
            }
            else if (currentPage == null || (currentPage != null && currentPage.GetType().Name == "UserPage"))
            {
                song.Add.Visibility = Visibility.Visible;
                song.Tresh.Visibility = Visibility.Collapsed;
                song.Pen.Visibility = Visibility.Collapsed;
                song.Visibility = Visibility.Collapsed;
            }
            else if (currentPage == null || (currentPage != null && currentPage.GetType().Name == "AdminPage"))
            {
                song.Add.Visibility = Visibility.Hidden;
                song.Tresh.Visibility = Visibility.Visible;
                song.Pen.Visibility = Visibility.Visible;
                song.Visibility = Visibility.Visible;
            }

            db.Songs.Add(song1);
            db.SaveChanges();
            db.Songs.Load();
            SearchResultList.ItemsSource = db.Songs.Local;
        }
    }
}
