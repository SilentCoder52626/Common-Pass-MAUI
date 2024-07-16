
using Common_Pass_MAUI.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Common_Pass_MAUI.ViewModels
{

    public partial class DetailsPageViewModel : ObservableObject
    {
        private readonly IAccountDetailsService _accountService;

        public DetailsPageViewModel(IAccountDetailsService accountService)
        {
            _accountService = accountService;
        }

        public Action ClosePopupAction { get; set; }


        [ObservableProperty]
        string _account;
        [ObservableProperty]
        string _userName;
        [ObservableProperty]
        string _pass;
        [ObservableProperty]
        bool _isBusy;
        [ObservableProperty]
        int _id;

        public async Task LoadSettingData()
        {

            IsBusy = true;
            var data = await _accountService.GetDecryptedDetails(Id);
            Account = data.Account;
            UserName = data.UserName;
            Pass = data.Pass;
            Id = data.Id;
            IsBusy = false;
        }
        [RelayCommand]
        public async Task SaveAccounts()
        {
            try
            {
                IsBusy = true;

                await _accountService.AddOrUpdateAccounts(new Models.AccountDetailsDto()
                {
                    Account = Account,
                    Id = Id,
                    Pass = Pass,
                    UserName = UserName,
                });
                await Shell.Current.DisplayAlert("Success!", "Accounts Updated Successfully!", "Ok");
                ClosePopupAction?.Invoke();

            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error!", $"Something Went Wrong: {ex.Message}", "Ok");


            }
            finally
            {
                IsBusy = false;
            }


        }
    }
}
