﻿using Microsoft.Win32;
using Oracle.ManagedDataAccess.Client;
using Pima.Model;
using Pima.View.Pages.AdminPages;
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
    /// Логика взаимодействия для OneArticle.xaml
    /// </summary>
    public partial class OneArticle : Page
    {
        private string nameImage;
        public static int articleId;
        public OneArticle()
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
                nameImage = selectedFileName;
                Source.Source = new BitmapImage(new Uri(selectedFileName));
                Source.Visibility = Visibility.Visible;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            byte[] image = null;
            OracleDbContext db = new OracleDbContext();
            var currentArticle = db.Articles.FirstOrDefault(x => x.ArticleId == articleId);
            if (currentArticle.Image == null && Source.Source != null && nameImage != null)
            {
                image = Converter.ConvertImageToByteArray(nameImage);
            }
            else if (currentArticle.Image != null && Source.Source != null && nameImage != null)
            {
                image = Converter.ConvertImageToByteArray(nameImage);
            }
            else
            {
                image = currentArticle.Image;
            }

            var ArticleId = new OracleParameter("ArticleId", OracleDbType.Int32, currentArticle.ArticleId, ParameterDirection.Input);
            var Title = new OracleParameter("Title", OracleDbType.NClob, TitleTextBox.Text, ParameterDirection.Input);
            var Text = new OracleParameter("Text", OracleDbType.NClob, TextEditor.Text, ParameterDirection.Input);
            var Image = new OracleParameter("Image", OracleDbType.Blob, image, ParameterDirection.Input);
            var sql = "BEGIN ARTICLEUPDATE(:ArticleId, :Title, :Text, :Image); END;";
            var update = db.Database.ExecuteSqlCommand(sql, ArticleId, Title, Text, Image);

            MainWindow.SnackbarMessage.Content = "Данные сохранены!";
            MainWindow.Snackbar.IsActive = true;
        }
    }
}
