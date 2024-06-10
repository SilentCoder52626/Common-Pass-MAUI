
using Common_Pass_MAUI.ViewModels;

namespace Common_Pass_MAUI.Pages;

public partial class HomePage : ContentPage
{
    private readonly HomeViewModel _vm;
    public HomePage(HomeViewModel vm)
    {
        InitializeComponent();
        _vm = vm;
        BindingContext = _vm;
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        SecureStorage.Remove(UIConstants.UserTokenKey);
        await Shell.Current.DisplayPromptAsync("Logout successfully", "Thank you for sticking around. See you soon.");
        await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");

    }
}