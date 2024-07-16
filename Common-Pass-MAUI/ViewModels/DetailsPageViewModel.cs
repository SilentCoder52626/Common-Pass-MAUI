
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Common_Pass_MAUI.ViewModels
{
    public partial class DetailsPageViewModel : ObservableObject
    {
        public Action ClosePopupAction { get; set; }


        [ObservableProperty]
        string _account;
        [ObservableProperty]
        string _userName;
        [ObservableProperty]
        string _pass;
        [ObservableProperty]
        bool _isBusy;

        public async Task LoadSettingData()
        {

            IsBusy = true;
            Account = "TMS 49";
            UserName = "20211013653";
            Pass = "Kaman";
            IsBusy = false;
        }
        [RelayCommand]
        public async Task SaveAccounts()
        {
            try
            {
                IsBusy = true;
               

                await Shell.Current.DisplayAlert("Success!", "Settings updated successfully!", "Ok");
                ClosePopupAction?.Invoke();

            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error!", $"Something Went Wrong: {ex.Message}", "Ok");


            }finally
            {
                IsBusy = false;
            }


        }
    }
}
