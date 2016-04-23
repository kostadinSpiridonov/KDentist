using KDentist.Commands;
using KDentist.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace KDentist.ViewModels
{
    public class PatientViewModel
    {
        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string LastName { get; set; }

        public string EGN { get; set; }
    }

    public class AppointmentViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Hour { get; set; }

        public string Note { get; set; }
    }


    public class HomeViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private DateTime showAppointmentDate;
        public DateTime ShowAppointmentDate
        {
            get
            {
                return this.showAppointmentDate;
            }
            set
            {
                this.showAppointmentDate = value;
                GetApointmentsForDate(this.showAppointmentDate);
                NotifyAppointmentsChanged();
            }
        }

        private ObservableCollection<PatientViewModel> patients;

        public ObservableCollection<PatientViewModel> Patients
        {
            get
            {
                return this.patients;
            }
        }

        private ObservableCollection<AppointmentViewModel> appointments;

        public ObservableCollection<AppointmentViewModel> Appointments
        {
            get
            {
                return this.appointments;
            }
        }


        public HomeViewModel()
        {
            showAppointmentDate = DateTime.Now;
            GetPatientsFromDB();
            GetApointmentsForDate(showAppointmentDate);
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
        public void GetPatientsFromDB()
        {
            var patientsDB = this.patientService.GetAll()
               .Reverse()
               .ToList()
               .ConvertAll(x => new PatientViewModel
               {
                   FirstName = x.FirstName,
                   SecondName = x.SecondName,
                   LastName = x.LastName,
                   EGN = x.EGN
               });

            patients = new ObservableCollection<PatientViewModel>(patientsDB);

            NotifyPatientsChanged();
        }

        public void NotifyAppointmentsChanged()
        {
            GetApointmentsForDate(showAppointmentDate);
            NotifyPropertyChanged("Appointments");
        }

        public void NotifyPatientsChanged()
        {
            NotifyPropertyChanged("Patients");
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
            MessageBoxResult question = MessageBox.Show("Искате ли да изтриете тази час?", "Изтриване", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (question == MessageBoxResult.Yes)
            {
                var id = (int)obj;
                this.dentistAppointmentsService.Delete(id);
                var deletedElement = this.Appointments.Where(x => x.Id == id).FirstOrDefault();
                this.Appointments.Remove(deletedElement);
                NotifyPropertyChanged("Appointments");
            }
        }

        private ICommand deletePatient;
        public ICommand DeletePatient
        {
            get
            {
                if (this.deletePatient == null)
                {
                    this.deletePatient = new RelayCommand(this.DeletePatientC);
                }
                return this.deletePatient;
            }
        }
        private void DeletePatientC(object obj)
        {
            MessageBoxResult question = MessageBox.Show("Искате ли да изтриете тази пациент?", "Изтриване", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (question == MessageBoxResult.Yes)
            {
                var patient = this.patientService.GetByEGN(obj.ToString());
                this.patientService.Delete(patient.Id);
                var deletedElement = this.Patients.Where(x => x.EGN == patient.EGN).FirstOrDefault();
                this.Patients.Remove(deletedElement);
                NotifyPropertyChanged("Patients");
                NotifyPropertyChanged("Appointments");
                (new System.Threading.Thread(() =>
                {
                    while (true)
                    {
                        try
                        {
                            Directory.Delete(AppDomain.CurrentDomain.GetData("DataDirectory") + "/Photoes/" + patient.Id.ToString() + "/", true);
                            break;
                        }
                        catch { }
                    }
                })).Start();
            }
        }
    }
}
