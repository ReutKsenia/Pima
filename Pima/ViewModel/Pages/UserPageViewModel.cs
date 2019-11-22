using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Pima.View.Pages.SharedPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

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
        #endregion

        public UserPageViewModel()
        {
            ArticulPage = new ArticulPage();
            NotesPage = new NotesPage();
            SongsPage = new SongsPage();
            TABsPage = new TABsPage();
            ChordsPage = new ChordsPage();


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

        #endregion
    }
}
