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
    /// Логика взаимодействия для OneNote.xaml
    /// </summary>
    public partial class OneNote : Page
    {
        private string nameNote;
        public static int articleId;
        public OneNote()
        {
            InitializeComponent();
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

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            OracleDbContext db = new OracleDbContext();
            byte[] image = null;
            int id = Int32.Parse(ID.Content.ToString());  //костыль))
            var currentNote = db.Notes.FirstOrDefault(x => x.NotesId == id);
            if (currentNote.Note == null && Source.Source != null && nameNote != null)
            {
                image = Converter.ConvertImageToByteArray(nameNote);
            }
            else if (currentNote.Note != null && Source.Source != null && nameNote != null)
            {
                image = Converter.ConvertImageToByteArray(nameNote);
            }
            else
            {
                image = currentNote.Note;
            }

            var NotesId = new OracleParameter("NotesId", OracleDbType.Int32, currentNote.NotesId, ParameterDirection.Input);
            var Name = new OracleParameter("Name", OracleDbType.NClob, NameTextBox.Text, ParameterDirection.Input);
            var Author = new OracleParameter("Author", OracleDbType.NClob, AuthorTextBox.Text, ParameterDirection.Input);
            var Note = new OracleParameter("Note", OracleDbType.Blob, image, ParameterDirection.Input);
            var Description = new OracleParameter("Description", OracleDbType.NClob, DescriptionEditor.Text, ParameterDirection.Input);
            var sql = "BEGIN NOTESUPDATE(:NotesId, :Name, :Author, :Note, :Description); END;";
            var update = db.Database.ExecuteSqlCommand(sql, NotesId, Name, Author, Note, Description);

            MainWindow.SnackbarMessage.Content = "Данные сохранены!";
            MainWindow.Snackbar.IsActive = true;
        }
    }
}
