using AutoReapairShop.DbContex.Contex;
using AutoReapairShop.DbContex.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace AutoReapairShop.App.ViewModels
{
    public class AddRecordViewModel : INotifyPropertyChanged
    {
        public AddRecordViewModel()
        {

        }

        public AddRecordViewModel(INavigation navigation, Schedule schedule)
            :this()
        {
            Navigation = navigation;
            Schedule = schedule;
        }
        ~AddRecordViewModel()
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

        private Schedule _schedule;

        public Schedule Schedule
        {
            get { return _schedule; }
            set { _schedule = value; OnPropertyChanged("Schedule"); }
        }

        private string _title;

        public string Title
        {
            get { return _title; }
            set { _title = value; OnPropertyChanged("Title"); }
        }

        private string _autoMark;

        public string AutoMark
        {
            get { return _autoMark; }
            set { _autoMark = value; OnPropertyChanged("AutoMark"); }
        }

        private string _description;

        public string Description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged("Description"); }
        }

        public ICommand AddRecordBtn
        {
            get
            {
                return new Command(() =>
                {
                    if (String.IsNullOrWhiteSpace(Title))
                    {
                        App.Current.MainPage.DisplayAlert("Ошибка", "Заголовок не должен быть пустым", "Ок");
                        return;
                    }

                    if (String.IsNullOrWhiteSpace(AutoMark))
                    {
                        App.Current.MainPage.DisplayAlert("Ошибка", "Марка авто не должна быть пустым", "Ок");
                        return;
                    }

                    if (String.IsNullOrWhiteSpace(Description))
                    {
                        App.Current.MainPage.DisplayAlert("Ошибка", "Описание не должно быть пустым", "Ок");
                        return;
                    }

                    try
                    {
                        using garageContext db = new garageContext();

                        Record record = new Record() { Title = Title, AutoMark = AutoMark, Description = Description };

                        db.Records.Add(record);                        

                        db.SaveChanges();

                        var schedule = db.Schedules.FirstOrDefault(x => x.ScheduleId == Schedule.ScheduleId);

                        schedule.FkRecord = record;

                        db.SaveChanges();

                        App.Current.MainPage.DisplayAlert("Успешно","Запись добавлена", "Ок");

                        Navigation.PopAsync();
                    }
                    catch (Exception ex)
                    {
                        App.Current.MainPage.DisplayAlert("Ошибка", ex.Message , "Ок");
                    }

                });
            }
        }

    }
}
