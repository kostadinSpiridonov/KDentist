using KDentist.Commands;
using KDentist.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KDentist.ViewModels
{
    public class ComboBoxPatient
    {
        public int Id { get; set; }
        public string FullName { get; set; }
    }

    public class AddDentistAppointmentViewModel : BaseViewModel
    {
        public AddDentistAppointmentViewModel()
        {
            this.patients = new ObservableCollection<ComboBoxPatient>(this.patientService.GetAll().Reverse()
                .ToList().ConvertAll(
                                    x => new ComboBoxPatient
                                    {
                                        Id = x.Id,
                                        FullName = x.FirstName + " " + x.SecondName + " " + x.LastName
                                    }));
        }

        private string note;
        public string Note
        {
            get
            {
                return this.note;
            }
            set
            {
                this.note = value;
            }
        }

        private string unknownPatient;
        public string UnknownPatient
        {
            get
            {
                return this.unknownPatient;
            }
            set
            {
                this.unknownPatient = value;
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

        private int patientId;
        public int PatientId
        {
            get
            {
                return this.patientId;
            }
            set
            {
                this.patientId = value;
            }
        }

        private int hour;
        public int Hour
        {
            get
            {
                return this.hour;
            }
            set
            {
                this.hour = value;
            }
        }

        private int minutes;
        public int Minutes
        {
            get
            {
                return this.minutes;
            }
            set
            {
                this.minutes = value;
            }
        }

        private ObservableCollection<ComboBoxPatient> patients;

        public ObservableCollection<ComboBoxPatient> Patients
        {
            get
            {
                return this.patients;
            }
            set
            {
                this.patients = value;
            }
        }

        private ICommand add;
        public ICommand Add
        {
            get
            {
                if (this.add == null)
                {
                    this.add = new RelayCommand(this.AddC);
                }
                return this.add;
            }
        }
        private void AddC(object obj)
        {
            var hour = new DentistAppointment
            {
                Note = this.note,
            };
            if(patientId>=1)
            {
                hour.PatientId = patientId;
            }
            else if(this.unknownPatient!=null)
            {
                hour.UnknowPatientName = this.unknownPatient;
            }

            hour.DateTime = new DateTime(this.date.Year, this.date.Month, this.date.Day, this.hour, this.minutes, 0);

            this.dentistAppointmentsService.Add(hour);
        }

        public bool IsModelValid()
        {
            var canExecute = true;

            if (this.patientId<1
                &&this.unknownPatient==null)
            {
                canExecute = false;
            }
            if (this.hour>24||this.hour<0)
            {
                canExecute = false;
            }
            if (this.minutes > 60 || this.minutes < 0)
            {
                canExecute = false;
            }
            if (this.date == null)
            {
                canExecute = false;
            }

            return canExecute;
        }
    }
}
