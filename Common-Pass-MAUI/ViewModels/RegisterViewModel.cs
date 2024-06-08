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
    public partial class RegisterViewModel : ObservableObject
    {
        [RelayCommand]
        public async void GotoLoginPage()
        {
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}",animate: true);
        }
        [RelayCommand]
        public void Register()
        {

        }
    }
}
