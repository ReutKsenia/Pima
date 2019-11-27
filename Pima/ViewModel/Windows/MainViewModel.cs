using GalaSoft.MvvmLight;
using Pima.View.Pages.AnonimPages;
using Pima.View.Pages.UserPages;
using System.Windows.Controls;

namespace Pima.ViewModel.Windows
{
    public class MainViewModel : ViewModelBase
    {
        #region поля и свойства
        private Page UserPage;
        private Page AnonimPage;


        private Page _currentPage;
        public Page CurrentPage
        {
            get { return _currentPage; }
            set { _currentPage = value; RaisePropertyChanged(() => CurrentPage); }
        }
        #endregion

        public MainViewModel()
        {
            //UserPage = new UserPage();
            AnonimPage = new AnonimPage();

            CurrentPage = AnonimPage;
        }
    }
}