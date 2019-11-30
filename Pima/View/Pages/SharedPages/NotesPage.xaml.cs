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
    /// Логика взаимодействия для NotesPage.xaml
    /// </summary>
    public partial class NotesPage : Page
    {
        OracleDbContext db = null;
        public NotesPage()
        {
            InitializeComponent();
        }

        private void Notes_OpenNoteMouseClick(object sender, RoutedEventArgs e)
        {
            var currentNote = (Notes)((Button)sender).Tag;

            var currentPage = ((MainWindow)Application.Current.MainWindow).CurrentPage.Content;
            if (currentPage == null || (currentPage != null && currentPage.GetType().Name == "AnonimPage"))
            {
                OneNote one = new OneNote();
                one.Name.Text = currentNote.Name;
                one.Author.Text = currentNote.Author;
                if (currentNote.Note != null)
                {
                    one.Source.Source = Pima.ViewModel.Converter.ConvertByteArrayToImage(currentNote.Note);
                }
                else
                {
                    one.Source.Visibility = Visibility.Collapsed;
                }
                if(currentNote.Description != null)
                {
                    one.Description.Text = currentNote.Description;
                }
                else
                {
                    one.Description.Visibility = Visibility.Collapsed;
                }
                (currentPage as AnonimPage).CurrentPage.Navigate(one);
            }
            else if (currentPage == null || (currentPage != null && currentPage.GetType().Name == "UserPage"))
            {
                OneNote one = new OneNote();
                one.Name.Text = currentNote.Name;
                one.Author.Text = currentNote.Author;
                if (currentNote.Note != null)
                {
                    one.Source.Source = Pima.ViewModel.Converter.ConvertByteArrayToImage(currentNote.Note);
                }
                else
                {
                    one.Source.Visibility = Visibility.Collapsed;
                }
                if (currentNote.Description != null)
                {
                    one.Description.Text = currentNote.Description;
                }
                else
                {
                    one.Description.Visibility = Visibility.Collapsed;
                }
                (currentPage as UserPages.UserPage).CurrentPage.Navigate(one);
            }
            else if (currentPage == null || (currentPage != null && currentPage.GetType().Name == "AdminPage"))
            {
                OneNote one = new OneNote();
                one.Name.Text = currentNote.Name;
                one.Author.Text = currentNote.Author;
                if (currentNote.Note != null)
                {
                    one.Source.Source = Pima.ViewModel.Converter.ConvertByteArrayToImage(currentNote.Note);
                }
                else
                {
                    one.Source.Visibility = Visibility.Collapsed;
                }
                if (currentNote.Description != null)
                {
                    one.Description.Text = currentNote.Description;
                }
                else
                {
                    one.Description.Visibility = Visibility.Collapsed;
                }
                (currentPage as AdminPage).CurrentPage.Navigate(one);
            }
        }

        private void Notes_UpdateNoteMouseClick(object sender, RoutedEventArgs e)
        {
            var currentNote = (Notes)((Button)sender).Tag;
            var currentPage = ((MainWindow)Application.Current.MainWindow).CurrentPage.Content;
            OneNote one = new OneNote();
            one.Name.Text = currentNote.Name;
            one.Author.Text = currentNote.Author;
            if (currentNote.Note != null)
            {
                one.Source.Source = Pima.ViewModel.Converter.ConvertByteArrayToImage(currentNote.Note);
            }
            else
            {
                one.Source.Visibility = Visibility.Collapsed;
            }
            if (currentNote.Description != null)
            {
                one.Description.Text = currentNote.Description;
            }
            else
            {
                one.Description.Visibility = Visibility.Collapsed;
            }
                (currentPage as AdminPage).CurrentPage.Navigate(one);

            one.Name.Visibility = Visibility.Collapsed;
            one.NameTextBox.Visibility = Visibility.Visible;
            if (currentNote.Name == null && currentNote.Author == null && currentNote.Description == null)
            {
                one.NameTextBox.Text = "Name";
                one.AuthorTextBox.Text = "Author";
                one.DescriptionEditor.Text = "Description";
            }
            one.Author.Visibility = Visibility.Collapsed;
            one.AuthorTextBox.Visibility = Visibility.Visible;
            one.Description.Visibility = Visibility.Collapsed;
            one.DescriptionEditor.Visibility = Visibility.Visible;

            one.SaveStackPanel.Visibility = Visibility.Visible;

            one.NewImage.Visibility = Visibility.Visible;

            one.ID.Content = currentNote.NotesId.ToString();
        }

        private void Notes_DeleteNoteMouseClick(object sender, RoutedEventArgs e)
        {
            var currentNote = (Notes)((Button)sender).Tag;
            db = new OracleDbContext();
            var NotesId = new OracleParameter("NotesId", OracleDbType.Int32, currentNote.NotesId, ParameterDirection.Input);
            var sql = "BEGIN NOTESDELETE(:NotesId); END;";
            var update = db.Database.ExecuteSqlCommand(sql, NotesId);
            db.Notes.Load();
            SearchResultList.ItemsSource = db.Notes.Local;
        }

        private void Notes_AddNoteMouseClick(object sender, RoutedEventArgs e)
        {
            var currentNote = (Notes)((ToggleButton)sender).Tag;
            db = new OracleDbContext();
            var NotesId = new OracleParameter("NotesId", OracleDbType.Int32, currentNote.NotesId, ParameterDirection.Input);
            var UserId = new OracleParameter("UserId", OracleDbType.Int32, CurrentUser.User.UserId, ParameterDirection.Input);
            var sql = "BEGIN ADDNOTESUSER(:UserId, :NotesId); END;";
            var update = db.Database.ExecuteSqlCommand(sql, UserId, NotesId);
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            Notes note1 = new Notes();
            NotesCard note = new NotesCard()
            {
                Margin = new Thickness(25)
            };
            //note.OpenNoteMouseClick += Notes_OpenNoteMouseClick;
            //note.DeleteArticleMouseClick += Add_DeleteArticleMouseClick;
            //note.UpdateArticleMouseClick += Add_UpdateArticleMouseClick;
            var currentPage = ((MainWindow)Application.Current.MainWindow).CurrentPage.Content;
            if (currentPage == null || (currentPage != null && currentPage.GetType().Name == "AnonimPage"))
            {
                note.Add.Visibility = Visibility.Hidden;
                note.Tresh.Visibility = Visibility.Hidden;
                note.Pen.Visibility = Visibility.Hidden;
                note.Visibility = Visibility.Collapsed;
            }
            else if (currentPage == null || (currentPage != null && currentPage.GetType().Name == "UserPage"))
            {
                note.Add.Visibility = Visibility.Visible;
                note.Tresh.Visibility = Visibility.Collapsed;
                note.Pen.Visibility = Visibility.Collapsed;
                note.Visibility = Visibility.Collapsed;
            }
            else if (currentPage == null || (currentPage != null && currentPage.GetType().Name == "AdminPage"))
            {
                note.Add.Visibility = Visibility.Hidden;
                note.Tresh.Visibility = Visibility.Visible;
                note.Pen.Visibility = Visibility.Visible;
                note.Visibility = Visibility.Visible;
            }

            db.Notes.Add(note1);
            db.SaveChanges();
            db.Notes.Load();
            SearchResultList.ItemsSource = db.Notes.Local;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            db = new OracleDbContext();
            db.Notes.Load();
            SearchResultList.ItemsSource = db.Notes.Local;

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
