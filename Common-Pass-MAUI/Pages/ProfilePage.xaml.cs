using Common_Pass_MAUI.ViewModels;

namespace Common_Pass_MAUI.Pages;

public partial class ProfilePage : ContentPage
{
    private readonly ProfileViewModel _vm;

    public ProfilePage(ProfileViewModel vm)
    {
        InitializeComponent();
        _vm = vm;
        BindingContext = _vm;
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        SecureStorage.Remove(UIConstants.UserTokenKey);
        await Shell.Current.DisplayAlert("Logout successfully", "Thank you for sticking around. See you soon.","Ok");
        await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");

    }
}