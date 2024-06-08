using Common_Pass_MAUI.Pages;
using Common_Pass_MAUI.Services;

namespace Common_Pass_MAUI
{
    public partial class MainPage : ContentPage
    {
            private readonly IAccountService _accountService;
            public MainPage(IAccountService accountService)
            {
                InitializeComponent();
                _accountService = accountService;
            }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (DeviceInfo.Platform == DevicePlatform.WinUI)
            {
                //check if OnBoarding Shown
                if (Preferences.Default.ContainsKey(UIConstants.OnBoardingShown))
                {
                    AppShell.Current.Dispatcher.Dispatch(async () =>
                    {
                        if (await _accountService.IsUserValidated())
                        {
                            await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
                        }
                        else
                        {
                            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
                        }
                    });
                }
                else
                {
                    AppShell.Current.Dispatcher.Dispatch(async () =>
                    {
                        await Shell.Current.GoToAsync($"//{nameof(OnBoardingPage)}");

                    });

                }
            }
            else
            {
                //check if OnBoarding Shown
                if (Preferences.Default.ContainsKey(UIConstants.OnBoardingShown))
                {
                    //ToDo : check for authenticated or not to send to homepage or login page
                    if (await _accountService.IsUserValidated())
                    {
                        await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
                    }
                    else
                    {
                        await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
                    }
                }
                else
                {
                    await Shell.Current.GoToAsync($"//{nameof(OnBoardingPage)}");

                }
            }


        }
    }

}
