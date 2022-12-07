using AutoReapairShop.App.ViewModels;
using AutoReapairShop.DbContex.Models;

namespace AutoReapairShop.App.Views;

public partial class RecordView : ContentPage
{
	public RecordView()
	{
		InitializeComponent();		
	}

	public RecordView(IEnumerable<Schedule> schedules)
		:this() 
	{
        BindingContext = new RecordViewModel(Navigation, schedules);
    }
}