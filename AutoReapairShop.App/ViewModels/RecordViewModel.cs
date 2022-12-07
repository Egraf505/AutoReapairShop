using AutoReapairShop.DbContex.Models;
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
            set { _currentSchedule = value; OnPropertyChanged("CurrentSchedule"); }
        }
        private int _count;

        public int Count
        {
            get { return Schedules.Count; }
            set { _count = value; OnPropertyChanged("Count"); }
        }



    }
}
