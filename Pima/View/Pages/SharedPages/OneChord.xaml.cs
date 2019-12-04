using Microsoft.Win32;
using Oracle.ManagedDataAccess.Client;
using Pima.Model;
using Pima.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для OneChord.xaml
    /// </summary>
    public partial class OneChord : Page
    {
        private string nameNote;
        public OneChord()
        {
            InitializeComponent();
        }

        private void NewImage_Click(object sender, RoutedEventArgs e)
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

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            OracleDbContext db = new OracleDbContext();
            byte[] image = null;
            int id = Int32.Parse(ID.Content.ToString());  //костыль))
            var currentChord = db.Chords.FirstOrDefault(x => x.ChordsId == id);
            if (currentChord.Chord == null && Source.Source != null && nameNote != null)
            {
                image = Converter.ConvertImageToByteArray(nameNote);
            }
            else if(currentChord.Chord != null && Source.Source != null && nameNote != null)
            {
                image = Converter.ConvertImageToByteArray(nameNote);
            }
            else
            {
                image = currentChord.Chord;
            }

            var ChordsId = new OracleParameter("ChordsId", OracleDbType.Int32, currentChord.ChordsId, ParameterDirection.Input);
            var Name = new OracleParameter("Name", OracleDbType.NClob, NameTextBox.Text, ParameterDirection.Input);
            var Chord = new OracleParameter("Note", OracleDbType.Blob, image, ParameterDirection.Input);
            var sql = "BEGIN CHORDSUPDATE(:ChordsId, :Name, :Chord); END;";
            var update = db.Database.ExecuteSqlCommand(sql, ChordsId, Name, Chord);
        }
    }
}
