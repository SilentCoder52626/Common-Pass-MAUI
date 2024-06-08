namespace Common_Pass_MAUI.Pages;

public partial class OnBoardingPage : ContentPage
{
	public OnBoardingPage()
	{
		InitializeComponent();
		Preferences.Default.Set(UIConstants.OnBoardingShown, true);
	}

    private async void Explore_Btn_Clicked(object sender, EventArgs e)
    {
		await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
    }
}