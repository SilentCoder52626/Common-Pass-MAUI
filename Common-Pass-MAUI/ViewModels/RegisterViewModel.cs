using Common_Pass_MAUI.Pages;
using Common_Pass_MAUI.Services;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UraniumUI.Dialogs;

namespace Common_Pass_MAUI.ViewModels
{
    public partial class RegisterViewModel : ObservableObject
    {
        private readonly IAccountService _accountService;
        private readonly IDialogService _dialogService;

        public RegisterViewModel(IAccountService accountService, IDialogService dialogService)
        {
            _accountService = accountService;
            _dialogService = dialogService;
        }
        [ObservableProperty]
        bool _isBusy;
        [ObservableProperty]
        string _name;
        [ObservableProperty]
        string _username;
        [ObservableProperty]
        string _email;
        [ObservableProperty]
        string _mobile;
        [ObservableProperty]
        string _password;
        [ObservableProperty]
        string _confirmPassword;
        [ObservableProperty]
        string _errorMessage;
        [ObservableProperty]
        bool _hasError;

        [RelayCommand]
        public async void GotoLoginPage()
        {
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}", animate: true);
        }
        [RelayCommand]
        public async Task Register()
        {
            try
            {
                IsBusy = true;
                HasError = false;

                if (Password != ConfirmPassword)
                {
                    IsBusy = false;
                    HasError = true;
                    ErrorMessage = "Password didnot match.";
                }
                else
                {

                    var isSuccess = await _accountService.Register(new Models.RegisterModel()
                    {
                        EmailAddress = Email,
                        Name = Name,
                        Password = Password,
                        ConfirmPassword = ConfirmPassword,
                        MobileNumber = Mobile,
                        UserName = Username
                    });
                    if (isSuccess)
                    {
                        await Toast.Make("User created successfully. Please Login.").Show();
                        await Shell.Current.GoToAsync($"//{nameof(LoginPage)}", animate: true);

                    }
                    else
                    {
                        await Toast.Make("Unable to create user.").Show();

                    }
                }
            }
            catch (Exception ex)
            {
                await Toast.Make("Could not connect to server. Pleaase contact adminstration").Show();

            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
