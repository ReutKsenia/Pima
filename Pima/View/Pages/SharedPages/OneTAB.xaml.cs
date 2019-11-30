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
    /// Логика взаимодействия для OneTAB.xaml
    /// </summary>
    public partial class OneTAB : Page
    {
        private string nameTAB;
        public OneTAB()
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
                nameTAB = selectedFileName;
                Source.Source = new BitmapImage(new Uri(selectedFileName));
                Source.Visibility = Visibility.Visible;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            OracleDbContext db = new OracleDbContext();
            byte[] image = null;
            int id = Int32.Parse(ID.Content.ToString());  //костыль))
            var currentTAB = db.TABs.FirstOrDefault(x => x.TABsId == id);
            if (Source.Source != null)
            {
                image = Converter.ConvertImageToByteArray(nameTAB);
            }

            var TABsId = new OracleParameter("TABsId", OracleDbType.Int32, currentTAB.TABsId, ParameterDirection.Input);
            var Name = new OracleParameter("Name", OracleDbType.NClob, NameTextBox.Text, ParameterDirection.Input);
            var Author = new OracleParameter("Author", OracleDbType.NClob, AuthorTextBox.Text, ParameterDirection.Input);
            var TAB = new OracleParameter("TAB", OracleDbType.Blob, image, ParameterDirection.Input);
            var Description = new OracleParameter("Description", OracleDbType.NClob, DescriptionEditor.Text, ParameterDirection.Input);
            var sql = "BEGIN TABSUPDATE(:TABsId, :Name, :Author, :TAB, :Description); END;";
            var update = db.Database.ExecuteSqlCommand(sql, TABsId, Name, Author, TAB, Description);
        }
    }
}
