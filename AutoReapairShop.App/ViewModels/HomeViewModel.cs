using AutoReapairShop.App.Views;
using AutoReapairShop.DbContex.Contex;
using AutoReapairShop.DbContex.Models;
using DevExpress.Mvvm;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AutoReapairShop.App.ViewModels
{
    public class HomeViewModel : INotifyPropertyChanged
    {
        public HomeViewModel()
        {
            CurrentDate = DateOnly.FromDateTime(DateTime.Now);
            InitDataBase();
        }
        public HomeViewModel(INavigation navigation)
            : this()
        {
            Navigation = navigation;            
        }

        ~HomeViewModel()
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
        private ObservableCollection<Master> _masters;
        public ObservableCollection<Master> Masters
        {
            get { return _masters; }
            set { _masters = value; OnPropertyChanged("Masters"); }
        }

        private Master _currentMaster;
        public Master CurrentMaster
        {
            get { return _currentMaster; }
            set 
            { 
                _currentMaster = value;
                if (_currentMaster != null)
                    GoToPageAndLoadSchedule();
                OnPropertyChanged("CurrentMaster");                
            }
        }
         

        private DateOnly _currentDate;
        public DateOnly CurrentDate
        {
            get { return _currentDate; }
            set 
            {
                _currentDate = value;
                OnPropertyChanged("CurrentDate");
            }
        }       
        public ICommand IncrementDaysBtn
        {
            get 
            {
                return new Command(() =>
                {
                    CurrentDate = CurrentDate.AddDays(1);
                });
            }            
        }
        public ICommand DecrementDaysBtn
        {
            get
            {
                return new Command(() =>
                {
                    CurrentDate = CurrentDate.AddDays(-1);
                });
            }
        }


        private void InitDataBase()
        {
            try
            {
                using garageContext garageContext = new garageContext();

                Masters = new ObservableCollection<Master>(garageContext.Masters.ToList());

                garageContext.Dispose();
            }
            catch (Exception ex)
            {

            }
        }

        private void GoToPageAndLoadSchedule()
        {
            try
            {
                using garageContext garageContex = new garageContext();

                var schedules = garageContex.Schedules.ToList();

                Navigation.PushAsync(new RecordView(schedules));

                garageContex.Dispose();
            }
            catch (Exception ex)
            {
                
            }            
        }     
    }
}
