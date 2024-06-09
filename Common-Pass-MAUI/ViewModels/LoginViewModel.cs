using Common_Pass_MAUI.Models;
using Common_Pass_MAUI.Pages;
using Common_Pass_MAUI.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using UraniumUI.Dialogs;

namespace Common_Pass_MAUI.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly IAccountService _accountService;
        private readonly IDialogService _dialogService;

        public LoginViewModel(IAccountService accountService, IDialogService dialogService)
        {
            _accountService = accountService;
            _dialogService = dialogService;
        }
        [ObservableProperty]
        bool _isBusy;
        [ObservableProperty]
        string _email;
        [ObservableProperty]
        string _password;

        [RelayCommand]
        public async void GotoRegisterPage()
        {
            await Shell.Current.GoToAsync($"//{nameof(RegisterPage)}", animate: true);
        }
        [RelayCommand]
        public async Task Login()
        {
            try
            {
                IsBusy = true;
                var IsSuccess = await _accountService.Login(new LoginModel()
                {
                    Email = Email,
                    Password = Password
                });
                if (IsSuccess)
                {
                    await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
                }
                else
                {
                    await _dialogService.ConfirmAsync("Incorrect Credentials.", "Incorrect Email or Password.");
                }
            }
            catch (Exception ex)
            {
                await _dialogService.ConfirmAsync("Could not connect to server.", "Please contact adminstration.");
            }
            finally
            {
                IsBusy = false;
            }

        }
    }
}
