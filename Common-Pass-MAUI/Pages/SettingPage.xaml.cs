using Common_Pass_MAUI.ViewModels;
using CommunityToolkit.Maui.Views;

namespace Common_Pass_MAUI.Pages;

public partial class SettingPage : Popup
{
	private readonly SettingPageViewModel _vm;
	public SettingPage(SettingPageViewModel vm)
	{
		InitializeComponent();
		_vm = vm;
		BindingContext = _vm;
        LoadSettings();
	}
    protected async void LoadSettings()
    {
        await _vm.LoadSettingData();
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
		Close();
    }
}