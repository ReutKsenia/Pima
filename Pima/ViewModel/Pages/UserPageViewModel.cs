using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using Oracle.ManagedDataAccess.Client;
using Pima.Model;
using Pima.View.Pages.SharedPages;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Pima.ViewModel.Pages
{
    class UserPageViewModel : ViewModelBase
    {
        #region поля и свойства
        private Page ArticulPage;
        private Page NotesPage;
        private Page SongsPage;
        private Page TABsPage;
        private Page ChordsPage;


        private Page _currentPage;
        public Page CurrentPage
        {
            get { return _currentPage; }
            set { _currentPage = value; RaisePropertyChanged(() => CurrentPage); }
        }

        private BitmapImage _image;
        public BitmapImage Image
        {
            get { return _image; }
            set { _image = value; RaisePropertyChanged(() => Image); }
        }

        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; RaisePropertyChanged(() => UserName); }
        }
        #endregion

        public UserPageViewModel()
        {
            ArticulPage = new ArticulPage();
            NotesPage = new NotesPage();
            SongsPage = new SongsPage();
            TABsPage = new TABsPage();
            ChordsPage = new ChordsPage();

            UserName = CurrentUser.User.Login;
            if (Converter.ConvertByteArrayToImage(CurrentUser.User.Foto) != null)
            {
                Image = Converter.ConvertByteArrayToImage(CurrentUser.User.Foto);
            }


        }

        #region команды
        public ICommand ArticlePage_Click
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if (CurrentPage != ArticulPage)
                    {
                        CurrentPage = ArticulPage;
                    }
                });
            }
        }

        public ICommand NotesPage_Click
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if (CurrentPage != NotesPage)
                    {
                        CurrentPage = NotesPage;
                    }
                });
            }
        }

        public ICommand TABsPage_Click
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if (CurrentPage != TABsPage)
                    {
                        CurrentPage = TABsPage;
                    }
                });
            }
        }

        public ICommand SongsPage_Click
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if (CurrentPage != SongsPage)
                    {
                        CurrentPage = SongsPage;
                    }
                });
            }
        }

        public ICommand ChordsPage_Click
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if (CurrentPage != ChordsPage)
                    {
                        CurrentPage = ChordsPage;
                    }
                });
            }
        }


        public ICommand AddFoto_Click
        {
            get
            {
                return new RelayCommand(() =>
                {
                    addFoto();
                    Image = Converter.ConvertByteArrayToImage(CurrentUser.User.Foto);
                });
            }
        }
        #endregion

        #region методы


        private void addFoto()
        {
            OracleDbContext db = new OracleDbContext();
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = "";
            dlg.Filter = "Image files (*.jpg,*.png,*.bmp)|*.jpg;*.png;*.bmp|All Files (*.*)|*.*";
            if (dlg.ShowDialog() == true)
            {
                string selectedFileName = dlg.FileName;
                CurrentUser.User.Foto = Converter.ConvertImageToByteArray(selectedFileName);
            }

            var UserId = new OracleParameter("UserId", OracleDbType.Int32, CurrentUser.User.UserId, ParameterDirection.Input);
            var Login = new OracleParameter("Login", OracleDbType.NVarchar2, CurrentUser.User.Login, ParameterDirection.Input);
            var Password = new OracleParameter("Password", OracleDbType.NVarchar2, CurrentUser.User.Password, ParameterDirection.Input);
            var Foto = new OracleParameter("Foto", OracleDbType.Blob, CurrentUser.User.Foto, ParameterDirection.Input);
            var sql = "BEGIN PERSONAINFOUPDATE(:UserId, :Login, :Password, :Foto); END;";
            var update = db.Database.ExecuteSqlCommand(sql, UserId, Login, Password, Foto);
        }
        #endregion
    }
}
