using Common_Pass_MAUI.Pages;

namespace Common_Pass_MAUI
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
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
                        //ToDo : check for authenticated or not to send to homepage or login page
                        await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
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
                    await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
                }
                else
                {
                    await Shell.Current.GoToAsync($"//{nameof(OnBoardingPage)}");

                }
            }

            
        }
    }

}
