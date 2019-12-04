using Microsoft.Win32;
using Oracle.ManagedDataAccess.Client;
using Pima.Model;
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
    /// Логика взаимодействия для OneSong.xaml
    /// </summary>
    public partial class OneSong : Page
    {
        public OneSong()
        {
            OracleDbContext db = new OracleDbContext();
           
            InitializeComponent();
            ShowChord();
        }
        public void ShowChord()
        {
            OracleDbContext db = new OracleDbContext();
            db.Chords.Load();
            db.Songs.Load();

            Chord.ItemsSource = db.Chords.Local;
            var ChordSongUser = db.ChordsSongs.Where(c => c.SongId_ChordsSong == songID);
            ChordSongCard.ItemsSource = ChordSongUser.ToList();
        }

        private string nameNote;
        public static int songID;
        public static string songPath;
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            OracleDbContext db = new OracleDbContext();
            byte[] image = null;
            byte[] music = null;
            var currentSong = db.Songs.FirstOrDefault(x => x.SongsId == songID);
            if (currentSong.Image == null && Source.Source != null && nameNote != null && currentSong.Music == null)
            {
                image = Converter.ConvertImageToByteArray(nameNote);
                FileStream fs = new FileStream(songPath, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs); // 
                music = br.ReadBytes((Int32)fs.Length);
            }
            else if (currentSong.Image != null && Source.Source != null && nameNote != null && currentSong.Music != null)
            {
                image = Converter.ConvertImageToByteArray(nameNote);
                FileStream fs = new FileStream(songPath, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs); // 
                music = br.ReadBytes((Int32)fs.Length);
            }
            else if (currentSong.Image == null && Source.Source == null && nameNote == null && currentSong.Music == null)
            {
                image = currentSong.Image;
                FileStream fs = new FileStream(songPath, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs); // 
                music = br.ReadBytes((Int32)fs.Length);
            }
            else
            {
                music = currentSong.Music;
                image = currentSong.Image;
            }

            var SongsId = new OracleParameter("SongsId", OracleDbType.Int32, currentSong.SongsId, ParameterDirection.Input);
            var Name = new OracleParameter("Name", OracleDbType.NClob, NameTextBox.Text, ParameterDirection.Input);
            var Author = new OracleParameter("Author", OracleDbType.NClob, AuthorTextBox.Text, ParameterDirection.Input);
            var Image = new OracleParameter("Image", OracleDbType.Blob, image, ParameterDirection.Input);
            var Music = new OracleParameter("Music", OracleDbType.Blob, music, ParameterDirection.Input);
            var Description = new OracleParameter("Description", OracleDbType.NClob, DescriptionEditor.Text, ParameterDirection.Input);
            var sql = "BEGIN SONGSUPDATE(:NotesId, :Name, :Author, :Muic, :Image, :Description); END;";
            var update = db.Database.ExecuteSqlCommand(sql, SongsId, Name, Author, Music, Image, Description);
        }

        private void AddMusic_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog(); //открывает диалоговое окно
            openFileDialog.ShowDialog();
            songPath = openFileDialog.FileName; // полный путь к файлу
            //SongsPage.CreateFileMP3();

            MediaElem.Source = new Uri(songPath, UriKind.Relative);
            Music.Visibility = Visibility.Visible;
        }

        private void MediaElem_MediaOpened(object sender, RoutedEventArgs e)
        {
            TimeSlider.Maximum = MediaElem.NaturalDuration.TimeSpan.TotalSeconds;

            var min = (int)(MediaElem.NaturalDuration.TimeSpan.TotalSeconds / 60);
            var sec = (int)(MediaElem.NaturalDuration.TimeSpan.TotalSeconds - min * 60);
            TimeMedia.Content = min + ":" + sec;
        }

        private void CurrentTimeMedia_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }

        private void TimeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            TimeSpan ts = TimeSpan.FromSeconds(e.NewValue);
            MediaElem.Position = ts;
            var seconds = ts.Seconds;

            CurrentTimeMedia.Content = ts.Minutes + ":" + ts.Seconds;
        }

        private void NewNote_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = "";
            dlg.Filter = "Image files (*.jpg,*.png,*.bmp)|*.jpg;*.png;*.bmp|All Files (*.*)|*.*";
            if (dlg.ShowDialog() == true)
            {
                string selectedFileName = dlg.FileName;
                nameNote = selectedFileName;
                Source.Source = new BitmapImage(new Uri(selectedFileName));
                Source.Visibility = Visibility.Visible;
            }
        }

        void timer_Tick(object sender, EventArgs e)
        {
            var time = MediaElem.Position.TotalSeconds;
            TimeSlider.Value = time;
        }

        private void PlayMP3_Click(object sender, RoutedEventArgs e)
        {
            MediaElem.Play();
            dispatcherTimer.Tick += new EventHandler(timer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
            PlayMP3.Visibility = Visibility.Collapsed;
            PauseMP3.Visibility = Visibility.Visible;
        }

        private void PauseMP3_Click(object sender, RoutedEventArgs e)
        {
            MediaElem.Pause();
            dispatcherTimer.Stop();
            PauseMP3.Visibility = Visibility.Collapsed;
            PlayMP3.Visibility = Visibility.Visible;
        }

        private void StopMP3_Click(object sender, RoutedEventArgs e)
        {
            MediaElem.Stop();
            PauseMP3.Visibility = Visibility.Collapsed;
            PlayMP3.Visibility = Visibility.Visible;
        }

        private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            MediaElem.Volume = VolumeSlider.Value;
        }

        private void AddChordSong_Click(object sender, RoutedEventArgs e)
        {
            Chord.DisplayMemberPath = "ChordsId";
            OracleDbContext db = new OracleDbContext();
            int CS = Convert.ToInt32(Chord.Text);
            Model.ChordsSong chordsSong = new ChordsSong()
            {
                SongId_ChordsSong = songID,
                ChordId_ChordsSong = CS
            };
            db.ChordsSongs.Add(chordsSong);
            db.SaveChanges();
            Chord.DisplayMemberPath = "Name";
            MessageBox.Show("Good");
            ShowChord();

        }

        private void Card_DeleteChordSongMouseClick(object sender, RoutedEventArgs e)
        {
            OracleDbContext db = new OracleDbContext();
            var currentChord = (ChordsSong)((Button)sender).Tag;
            var ChordSong = db.ChordsSongs.FirstOrDefault(c => c.Id == currentChord.Id );

            var Id = new OracleParameter("Id", OracleDbType.Int32, ChordSong.Id , ParameterDirection.Input);
            
            var sql = "BEGIN CHORDSONGDELETE(:Id); END;";
            var update = db.Database.ExecuteSqlCommand(sql, Id);
            ShowChord();
        }
    }
}
