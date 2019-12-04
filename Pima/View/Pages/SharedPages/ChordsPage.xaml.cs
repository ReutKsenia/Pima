using Oracle.ManagedDataAccess.Client;
using Pima.Model;
using Pima.View.Pages.AdminPages;
using Pima.View.UserControls;
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

namespace Pima.View.Pages.SharedPages
{

    public partial class ChordsPage : Page
    {
        OracleDbContext db = null;
        public ChordsPage()
        {
            InitializeComponent();
        }

        private void Chord_DeleteChordMouseClick(object sender, RoutedEventArgs e)
        {
            var currentChord = (Chords)((Button)sender).Tag;
            db = new OracleDbContext();
            var ChordId = new OracleParameter("ChordId", OracleDbType.Int32, currentChord.ChordsId, ParameterDirection.Input);
            var sql = "BEGIN CHORDDELETE(:ChordId); END;";
            var update = db.Database.ExecuteSqlCommand(sql, ChordId);
            db.Chords.Load();
            SearchResultList.ItemsSource = db.Chords.Local;
        }

        private void Chord_UpdateChordMouseClick(object sender, RoutedEventArgs e)
        {
            var currentChord = (Chords)((Button)sender).Tag;
            var currentPage = ((MainWindow)Application.Current.MainWindow).CurrentPage.Content;
            OneChord one = new OneChord();
            one.Name.Text = currentChord.Name;
            if (currentChord.Chord != null)
            {
                one.Source.Source = Pima.ViewModel.Converter.ConvertByteArrayToImage(currentChord.Chord);
            }
            else
            {
                one.Source.Visibility = Visibility.Collapsed;
            }
                (currentPage as AdminPage).CurrentPage.Navigate(one);

            one.Name.Visibility = Visibility.Collapsed;
            one.NameTextBox.Visibility = Visibility.Visible;
            if (currentChord.Name == null)
            {
                one.NameTextBox.Text = "Name";
            }

            one.SaveStackPanel.Visibility = Visibility.Visible;

            one.NewImage.Visibility = Visibility.Visible;

            one.ID.Content = currentChord.ChordsId.ToString();
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            db = new OracleDbContext();
            Chords chord1 = new Chords();
            ChordCard chord = new ChordCard()
            {
                Margin = new Thickness(25)
            };
            var currentPage = ((MainWindow)Application.Current.MainWindow).CurrentPage.Content;
            if (currentPage == null || (currentPage != null && currentPage.GetType().Name == "AnonimPage"))
            {
                chord.Panel.Visibility = Visibility.Collapsed;
                chord.Tresh.Visibility = Visibility.Hidden;
                chord.Pen.Visibility = Visibility.Hidden;
            }
            else if (currentPage == null || (currentPage != null && currentPage.GetType().Name == "UserPage"))
            {
                chord.Panel.Visibility = Visibility.Collapsed;
                chord.Tresh.Visibility = Visibility.Collapsed;
                chord.Pen.Visibility = Visibility.Collapsed;
            }
            else if (currentPage == null || (currentPage != null && currentPage.GetType().Name == "AdminPage"))
            {
                chord.Panel.Visibility = Visibility.Visible;
                chord.Tresh.Visibility = Visibility.Visible;
                chord.Pen.Visibility = Visibility.Visible;
            }

            db.Chords.Add(chord1);
            db.SaveChanges();
            db.Chords.Load();
            SearchResultList.ItemsSource = db.Chords.Local;
        }

        private void Page_Loaded_1(object sender, RoutedEventArgs e)
        {
            db = new OracleDbContext();
            db.Chords.Load();
            SearchResultList.ItemsSource = db.Chords.Local;

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
