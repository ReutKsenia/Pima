using Pima.Model;
using System;
using System.Collections.Generic;
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

namespace Pima.View.Pages.UserPages
{
    /// <summary>
    /// Логика взаимодействия для PlayerPage.xaml
    /// </summary>
    public partial class PlayerPage : Page
    {
        public PlayerPage()
        {
            InitializeComponent();
            MediaElem.Stop();
            File.Delete(@"F:\PlayDemo.mp3");
            CreateFileMP3();
            MediaElem.Source = new Uri(@"F:\PlayDemo.mp3", UriKind.Relative);
        }
       

        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();

        private void MediaElem_MediaOpened(object sender, RoutedEventArgs e)
        {
            TimeSlider.Maximum = MediaElem.NaturalDuration.TimeSpan.TotalSeconds;

            var min = (int)(MediaElem.NaturalDuration.TimeSpan.TotalSeconds / 60);
            var sec = (int)(MediaElem.NaturalDuration.TimeSpan.TotalSeconds - min * 60);
            TimeMedia.Content = min + ":" + sec;
        }


        void timer_Tick(object sender, EventArgs e)
        {
            var time = MediaElem.Position.TotalSeconds;
            TimeSlider.Value = time;
        }


        private void TimeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            TimeSpan ts = TimeSpan.FromSeconds(e.NewValue);
            MediaElem.Position = ts;
            var seconds = ts.Seconds;

            CurrentTimeMedia.Content = ts.Minutes + ":" + ts.Seconds;
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
        public static void CreateFileMP3()
        {
            OracleDbContext db = new OracleDbContext();
            byte[] bytes; // переменная массива байт
            var select = db.Demos.Where(d => d.DemoId == DemoPage.DemoID).FirstOrDefault(); // ищем файл, который хотим воспроизвести
            if (select != null)
            {

                bytes = select.Record; // присваиваем массиву байт значение из бд
                if (bytes != null)
                {

                    string FullDirToFile = @"F:\PlayDemo.mp3"; // полный путь к файлу music.mp3 (его вначале нет, он создатся сам или перезапишется)
                    using (FileStream fstream = new FileStream(FullDirToFile, FileMode.OpenOrCreate))
                    {
                        // запись массива байтов в файл
                        fstream.Write(bytes, 0, bytes.Length);
                    }
                }
            }
        }
    }
}
