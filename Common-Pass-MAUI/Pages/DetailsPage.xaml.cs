using Common_Pass_MAUI.ViewModels;
using CommunityToolkit.Maui.Views;

namespace Common_Pass_MAUI.Pages;

public partial class DetailsPage : Popup
{
    private readonly DetailsPageViewModel _vm;
    public DetailsPage(DetailsPageViewModel vm)
    {
        InitializeComponent();
        _vm = vm;
        _vm.ClosePopupAction = () => Close();
        BindingContext = _vm;
        LoadDetails();
    }
    protected async void LoadDetails()
    {
        await _vm.LoadSettingData();
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        Close();
    }
}