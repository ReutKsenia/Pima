using Pima.Model;
using Pima.View.Pages.SharedPages;
using Pima.View.Windows;
using Pima.ViewModel.Pages;
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

namespace Pima.View.Pages.AnonimPages
{
    public partial class AnonimPage : Page
    {
        AnonimPageViewModel context = new AnonimPageViewModel();
        public AnonimPage()
        {
            InitializeComponent();
            DataContext = context;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Close();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            GridBackground.Opacity = 1;
            login.ShowDialog();
            GridBackground.Opacity = 0;
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            Registration registration = new Registration();
            GridBackground.Opacity = 1;
            registration.ShowDialog();
            GridBackground.Opacity = 0;
        }

        private void Admin_Click(object sender, RoutedEventArgs e)
        {
            Admin admin = new Admin();
            GridBackground.Opacity = 1;
            admin.ShowDialog();
            GridBackground.Opacity = 0;
        }

        public void SearchResults(string name)
        {
            OracleDbContext db = new OracleDbContext();
            if(ArticeButton.IsChecked == true)
            {
                SearchResultArticle.Visibility = Visibility.Visible;
                SearchResultNote.Visibility = Visibility.Collapsed;
                SearchResultSong.Visibility = Visibility.Collapsed;
                SearchResultTAB.Visibility = Visibility.Collapsed;
                SearchResultChords.Visibility = Visibility.Collapsed;
                if (!string.IsNullOrEmpty(name))
                {
                    var result = db.Articles.Where(p => p.Title.Contains(name));
                    SearchResultArticle.ItemsSource = result.ToList();
                }
                if (name == String.Empty || name == "")
                {
                    SearchResultArticle.ItemsSource = null;
                }
            } 
            else if (NoteButton.IsChecked == true)
            {
                SearchResultArticle.Visibility = Visibility.Collapsed;
                SearchResultNote.Visibility = Visibility.Visible;
                SearchResultSong.Visibility = Visibility.Collapsed;
                SearchResultChords.Visibility = Visibility.Collapsed;
                SearchResultTAB.Visibility = Visibility.Collapsed;
                if (!string.IsNullOrEmpty(name))
                {
                    var result = db.Notes.Where(p => p.Name.Contains(name));
                    SearchResultNote.ItemsSource = result.ToList();
                }
                if (name == String.Empty || name == "")
                {
                    SearchResultNote.ItemsSource = null;
                }
            }
            else if (SongButton.IsChecked == true)
            {
                SearchResultArticle.Visibility = Visibility.Collapsed;
                SearchResultNote.Visibility = Visibility.Collapsed;
                SearchResultTAB.Visibility = Visibility.Collapsed;
                SearchResultChords.Visibility = Visibility.Collapsed;
                SearchResultSong.Visibility = Visibility.Visible;
                if (!string.IsNullOrEmpty(name))
                {
                    var result = db.Songs.Where(p => p.Name.Contains(name));
                    SearchResultSong.ItemsSource = result.ToList();
                }
                if (name == String.Empty || name == "")
                {
                    SearchResultSong.ItemsSource = null;
                }
            }
            else if (TABButton.IsChecked == true)
            {
                SearchResultArticle.Visibility = Visibility.Collapsed;
                SearchResultNote.Visibility = Visibility.Collapsed;
                SearchResultSong.Visibility = Visibility.Collapsed;
                SearchResultChords.Visibility = Visibility.Collapsed;
                SearchResultTAB.Visibility = Visibility.Visible;
                if (!string.IsNullOrEmpty(name))
                {
                    var result = db.TABs.Where(p => p.Name.Contains(name));
                    SearchResultTAB.ItemsSource = result.ToList();
                }
                if (name == String.Empty || name == "")
                {
                    SearchResultTAB.ItemsSource = null;
                }
            }
            else if (ChordButton.IsChecked == true)
            {
                SearchResultArticle.Visibility = Visibility.Collapsed;
                SearchResultNote.Visibility = Visibility.Collapsed;
                SearchResultSong.Visibility = Visibility.Collapsed;
                SearchResultTAB.Visibility = Visibility.Collapsed;
                SearchResultChords.Visibility = Visibility.Visible;
                if (!string.IsNullOrEmpty(name))
                {
                    var result = db.Chords.Where(p => p.Name.Contains(name));
                    SearchResultChords.ItemsSource = result.ToList();
                }
                if (name == String.Empty || name == "")
                {
                    SearchResultChords.ItemsSource = null;
                }
            }
        }


        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchResults(Search.Text);
        }

        private void Search_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            popupSearch.IsPopupOpen = true;
        }

        private void LogoName_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        public static int? articleid;

        private void GridResult_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if(ArticeButton.IsChecked == true)
            {
                var currentArticle = (Model.Article)((Grid)sender).Tag;
                int? id = currentArticle.ArticleId;
                OneArticle one = new OneArticle();
                OneArticle.articleId = (int)id;
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
                CurrentPage.Navigate(one);
            }
            else if (NoteButton.IsChecked == true)
            {
                var currentArticle = (Model.Notes)((Grid)sender).Tag;
                int? id = currentArticle.NotesId;
                OneNote one = new OneNote();
                OneNote.articleId = (int)id;
                one.Name.Text = currentArticle.Name;
                
                if (currentArticle.Note != null)
                {
                    one.Source.Source = Pima.ViewModel.Converter.ConvertByteArrayToImage(currentArticle.Note);
                }
                else
                {
                    one.Source.Visibility = Visibility.Collapsed;
                }
                CurrentPage.Navigate(one);
            }
            else if (SongButton.IsChecked == true)
            {
                var currentSong = (Model.Songs)((Grid)sender).Tag;
                int? id = currentSong.SongsId;
                OneSong one = new OneSong();
                OneSong.songID = (int)id;
                one.Name.Text = currentSong.Name;
                //one.Text.Text = currentArticle.Text;
                if (currentSong.Image != null)
                {
                    one.Source.Source = Pima.ViewModel.Converter.ConvertByteArrayToImage(currentSong.Image);
                }
                else
                {
                    one.Source.Visibility = Visibility.Collapsed;
                }
                CurrentPage.Navigate(one);
            }
            else if (ChordButton.IsChecked == true)
            {
                var currentChord = (Model.Chords)((Grid)sender).Tag;
                int? id = currentChord.ChordsId;
                OneChord one = new OneChord();
                OneChord.ChordId = (int)id;
                one.Name.Text = currentChord.Name;
                //one.Text.Text = currentArticle.Text;
                if (currentChord.Chord != null)
                {
                    one.Source.Source = Pima.ViewModel.Converter.ConvertByteArrayToImage(currentChord.Chord);
                }
                else
                {
                    one.Source.Visibility = Visibility.Collapsed;
                }
                CurrentPage.Navigate(one);
            }
        }
    }
}
