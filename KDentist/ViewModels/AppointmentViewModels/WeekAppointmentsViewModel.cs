using KDentist.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KDentist.ViewModels.AppointmentViewModels
{
    public class WeekAppointmentsAllViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private ObservableCollection<DayAppointemtsViewModel> days;

        public ObservableCollection<DayAppointemtsViewModel> Days
        {
            get
            {
                return this.days;
            }
        }

        public WeekAppointmentsAllViewModel()
        {
            this.days = new ObservableCollection<DayAppointemtsViewModel>();

            var firstMonday = DateTime.Now;

            while (firstMonday.DayOfWeek != DayOfWeek.Monday)
            {
                firstMonday = firstMonday.AddDays(-1);
            }

            for (int i = 0; i < 7; i++)
            {
                days.Add(new DayAppointemtsViewModel(firstMonday));
                firstMonday = firstMonday.AddDays(1);
            }
        }


        private ICommand next;
        public ICommand Next
        {
            get
            {
                if (this.next == null)
                {
                    this.next = new RelayCommand(this.NextC);
                }
                return this.next;
            }
        }
        private void NextC(object obj)
        {

            var firstMonday = this.days.Last().Date;
            firstMonday = firstMonday.AddDays(1);

            this.days = new ObservableCollection<DayAppointemtsViewModel>();
            for (int i = 0; i < 7; i++)
            {
                days.Add(new DayAppointemtsViewModel(firstMonday));
                firstMonday = firstMonday.AddDays(1);
            }

            NotifyPropertyChanged("Days");
        }

        private ICommand previous;
        public ICommand Previous
        {
            get
            {
                if (this.previous == null)
                {
                    this.previous = new RelayCommand(this.PreviousC);
                }
                return this.previous;
            }
        }
        private void PreviousC(object obj)
        {

            var firstMonday = this.days.First().Date;
            firstMonday = firstMonday.AddDays(-7);

            this.days = new ObservableCollection<DayAppointemtsViewModel>();
            for (int i = 0; i < 7; i++)
            {
                days.Add(new DayAppointemtsViewModel(firstMonday));
                firstMonday = firstMonday.AddDays(1);
            }

            NotifyPropertyChanged("Days");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String info)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(info));
            }
        }

        public void UpdateDays()
        {
            foreach (var item in days)
            {
                item.UpdateAppointments();
            }
        }

        private ICommand delete;
        public ICommand Delete
        {
            get
            {
                if (this.delete == null)
                {
                    this.delete = new RelayCommand(this.DeleteC);
                }
                return this.delete;
            }
        }
        private void DeleteC(object obj)
        {
            var appointment = this.dentistAppointmentsService.GetById((int)obj);
            bool tran = false;

            foreach (var item in days)
            {
                if(item.Date.ToShortDateString()==appointment.DateTime.ToShortDateString())
                {
                    var ap = item.Appointments.Where(x => x.Id == appointment.Id).FirstOrDefault(); ;
                    item.Appointments.Remove(ap);
                    item.OnlyNotifyAppointments();
                    tran = true;
                }
            }

            if(tran)
            {
                this.dentistAppointmentsService.Delete(appointment.Id);
            }
        }
    }

    public class DayAppointemtsViewModel : BaseViewModel, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String info)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(info));
            }
        }

        private DateTime date;
        public DateTime Date
        {
            get
            {
                return this.date;
            }
            set
            {
                this.date = value;
            }
        }

        public DayAppointemtsViewModel(DateTime day)
        {
            this.date = day;
            this.GetApointmentsForDate(day);
        }

        private ObservableCollection<AppointmentViewModel> appointments;

        public ObservableCollection<AppointmentViewModel> Appointments
        {
            get
            {
                return this.appointments;
            }
        }

        public void GetApointmentsForDate(DateTime date)
        {
            this.appointments = new ObservableCollection<AppointmentViewModel>(
                            this.dentistAppointmentsService.GetAll().OrderBy(x => x.DateTime).Where(x => x.DateTime.ToShortDateString() == date.ToShortDateString())
                            .ToList().ConvertAll(x => new AppointmentViewModel
                            {
                                Name = x.Patient==null?x.UnknowPatientName: x.Patient.FirstName + " " + x.Patient.SecondName + " " + x.Patient.LastName,
                                Hour = x.DateTime.ToShortTimeString(),
                                Note = x.Note == null || x.Note == "" ? "Няма записана бележка !" : x.Note,
                                Id = x.Id
                            }));
        }

        public void OnlyNotifyAppointments()
        {
            NotifyPropertyChanged("Appointments");
        }

        public void UpdateAppointments()
        {
            GetApointmentsForDate(this.date);
            NotifyPropertyChanged("Appointments");
        }
    }

    public class AppointmentViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Hour { get; set; }

        public string Note { get; set; }
    }

}
