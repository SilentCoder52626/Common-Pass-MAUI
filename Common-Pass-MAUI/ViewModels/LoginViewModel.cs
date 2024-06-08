using Common_Pass_MAUI.Pages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common_Pass_MAUI.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        [RelayCommand]
        public async void GotoRegisterPage()
        {
            await Shell.Current.GoToAsync($"//{nameof(RegisterPage)}",animate:true);
        }
        [RelayCommand]
        public void Login()
        {

        }
    }
}
