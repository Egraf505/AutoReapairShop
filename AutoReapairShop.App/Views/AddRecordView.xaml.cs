using AutoReapairShop.App.ViewModels;
using AutoReapairShop.DbContex.Models;

namespace AutoReapairShop.App.Views;

public partial class AddRecordView : ContentPage
{
	public AddRecordView()
	{
		InitializeComponent();
	}

	public AddRecordView(Schedule shedule)
		:this()
	{
		BindingContext = new AddRecordViewModel(Navigation, shedule);
	}
}