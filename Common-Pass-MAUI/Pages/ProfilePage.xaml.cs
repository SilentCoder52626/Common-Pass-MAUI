using Common_Pass_MAUI.ViewModels;
using CommunityToolkit.Maui.Views;

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

    private async void LogoutBtnClicked(object sender, EventArgs e)
    {
        SecureStorage.Remove(UIConstants.UserTokenKey);
        await Shell.Current.DisplayAlert("Logout successfully", "Thank you for sticking around. See you soon.", "Ok");
        await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");

    }
    private void SettingBtnClicked(object sender, EventArgs e)
    {
        var viewModel = Application.Current.Handler.MauiContext.Services.GetService<SettingPageViewModel>();

        var settingPage = new SettingPage(viewModel);
        this.ShowPopup(settingPage);
    }

}