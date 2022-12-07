using AutoReapairShop.App.Views;
using AutoReapairShop.DbContex.Contex;
using AutoReapairShop.DbContex.Models;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace AutoReapairShop.App.ViewModels
{
    public class RecordViewModel : INotifyPropertyChanged
    {
        public RecordViewModel()
        {
            IsRefreshing= false;
        }
        public RecordViewModel(INavigation navigation)
            : this()
        {
            _navigation = navigation;
        }

        public RecordViewModel(INavigation navigation, IEnumerable<Schedule> schedules)
            : this(navigation)
        {
            Schedules = new ObservableCollection<Schedule>(schedules);
        }
        ~RecordViewModel()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private INavigation _navigation;
        public INavigation Navigation
        {
            get { return _navigation; }
            set { _navigation = value; }
        }
        
        private ObservableCollection<Schedule> _schedules;
        public ObservableCollection<Schedule> Schedules
        {
            get { return _schedules; }
            set { _schedules = value; OnPropertyChanged("Schedules"); }
        }

        private Schedule _currentSchedule;
        public Schedule CurrentSchedule
        {
            get { return _currentSchedule; }
            set 
            { 
                _currentSchedule = value;
                if (CurrentSchedule != null)
                    GoToPageAddRecord();
                OnPropertyChanged("CurrentSchedule"); 
            }
        }  

        public ICommand PageAppearing
        {
            get
            {
                return new Command(() =>
                {
                    try
                    {
                        IsRefreshing= true;
                        using garageContext garageContext = new garageContext();

                        if (Schedules == null)
                        {
                            IsRefreshing = false;
                            return;
                        }
                        
                        Schedules = new ObservableCollection<Schedule>(garageContext.Schedules.Where(x => x.FkMasterId == Schedules.First().FkMasterId && x.FkRecordId == null).ToList());

                        garageContext.Dispose();

                        IsRefreshing = false;
                    }
                    catch (Exception ex)
                    {
                        App.Current.MainPage.DisplayAlert("Ошибка", ex.Message, "Ок");
                        IsRefreshing = false;
                    }
                    
                });
            }
        }

        private bool _isRefreshing;

        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set { _isRefreshing = value; OnPropertyChanged("IsRefreshing");}
        }

        private void GoToPageAddRecord()
        {
            Navigation.PushAsync(new AddRecordView(CurrentSchedule));
        }
    }
}
