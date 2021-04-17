using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Threading.Tasks;

namespace ProfileBook.ViewModel
{
    public class BaseViewModel : BindableBase, INavigatedAware, IInitializeAsync
    {
        protected INavigationService _navigationService;
        public BaseViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
        public BaseViewModel()
        {

        }
        public virtual Task InitializeAsync(INavigationParameters parameters)
        {
            throw new NotImplementedException();
        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {
            throw new NotImplementedException();
        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {
            throw new NotImplementedException();
        }
    }
}
