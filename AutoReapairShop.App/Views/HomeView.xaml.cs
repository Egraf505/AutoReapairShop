using AutoReapairShop.App.ViewModels;

namespace AutoReapairShop.App.Views;

public partial class HomePage : ContentPage
{
	public HomePage()
	{     
        InitializeComponent();
        BindingContext = new HomeViewModel(Navigation);
    }
}