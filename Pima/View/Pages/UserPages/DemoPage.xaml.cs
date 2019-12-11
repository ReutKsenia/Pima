using NAudio.Wave;
using Pima.Model;
using Pima.ViewModel;
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
    /// Логика взаимодействия для DemoPage.xaml
    /// </summary>
    public partial class DemoPage : Page
    {
        public static int DemoID;
        WaveIn waveIn; //поток для записи
        WaveFileWriter waveWriter; // класс для записи в файл
        string putputFilename = "record.wav";
        public DemoPage()
        {

            InitializeComponent();
            ShowDemo();
            


        }
        public void ShowDemo()
        {
            OracleDbContext db = new OracleDbContext();
            var select = db.Demos.Where(demo => demo.UserId_Demo == CurrentUser.User.UserId);
            DemoCard.ItemsSource = select.ToList().OrderBy(o=>o.DemoId);
        }

        private void waveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            if (waveWriter == null) return;

            waveWriter.Write(e.Buffer, 0, e.BytesRecorded);
            waveWriter.Flush();
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            waveIn = new WaveIn();
            waveIn.DeviceNumber = 0;
            string outputFilename = @"F:\demo.mp3";
            waveIn.WaveFormat = new WaveFormat(44100, WaveIn.GetCapabilities(waveIn.DeviceNumber).Channels);

            waveIn.DataAvailable += new EventHandler<WaveInEventArgs>(waveIn_DataAvailable);
            waveWriter = new WaveFileWriter(outputFilename, waveIn.WaveFormat);
            waveIn.StartRecording();
            MainWindow.SnackbarMessage.Content = "Запись началась!";
            MainWindow.Snackbar.IsActive = true;
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            OracleDbContext db = new OracleDbContext();
            //var select = db.Demos.Where(demo => demo.UserId_Demo == CurrentUser.User.UserId);
            if (waveIn != null)
            {
                waveIn.StopRecording();
                waveIn.Dispose();
                waveIn = null;
            }
            if (waveWriter != null)
            {
                waveWriter.Dispose();
                waveWriter = null;
            }
            byte[] demo = null;
                FileStream fs = new FileStream(@"F:\demo.mp3", FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs); // 
                 demo = br.ReadBytes((Int32)fs.Length);
            Model.Demo demo1 = new Demo()
            {
                Record = demo,
                UserId_Demo = CurrentUser.User.UserId,
                Date = DateTime.Now
            };
            db.Demos.Add(demo1);
            db.SaveChanges();
            ShowDemo();
            var sel = db.Demos.Where(d => d.UserId_Demo == CurrentUser.User.UserId).OrderByDescending(o => o.DemoId).FirstOrDefault();
            DemoID = sel.DemoId;
            Player.Navigate(new PlayerPage());
            //File.Delete(@"F:\demo.mp3");


        }

        private void PlayDemo_Click(object sender, RoutedEventArgs e)
        {
            OracleDbContext db = new OracleDbContext();
            var fulldemo = (Model.Demo)((Button)sender).Tag;
            DemoID = fulldemo.DemoId;
            Player.Navigate(new PlayerPage());
            

        }


    }
}
