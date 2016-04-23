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
    public class AddDentalProcedureViewModel : BaseViewModel
    {
        public AddDentalProcedureViewModel()
        {
            this.mdtS = new ObservableCollection<MDT>(this.mdtService.GetAll());
            this.doctors = new ObservableCollection<Doctor>(this.doctorService.GetAll());
            this.activities = new ObservableCollection<Activity>(this.activityService.GetAll());
            this.date = DateTime.Now;
            this.mdtS.Insert(0, new MDT());
        }

        private string tooth;

        private decimal paidValue;

        public decimal PaidValue
        {
            get
            {
                return this.paidValue;
            }
            set
            {
                this.paidValue = value;
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

        public string Tooth
        {
            get
            {
                return this.tooth;
            }
            set
            {
                this.tooth = value;
            }
        }

        private string diagnosis;

        public string Diagnosis
        {
            get
            {
                return this.diagnosis;
            }
            set
            {
                this.diagnosis = value;
            }
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

        private int selectedMdtId;

        public int SelectedMdtId
        {
            get
            {
                return this.selectedMdtId;
            }
            set
            {
                this.selectedMdtId = value;
            }
        }
      
        private ObservableCollection<MDT> mdtS;

        public ObservableCollection<MDT> MDTs
        {
            get
            {
                return this.mdtS;
            }
            set
            {
                this.mdtS = value;
            }
        }

        private int selectedDoctorId;

        public int SelectedDoctorId
        {
            get
            {
                return this.selectedDoctorId;
            }
            set
            {
                this.selectedDoctorId = value;
            }
        }

        private ObservableCollection<Doctor> doctors;

        public ObservableCollection<Doctor> Doctors
        {
            get
            {
                return this.doctors;
            }
            set
            {
                this.doctors = value;
            }
        }

        private int selectedActivityId;

        public int SelectedActivityId
        {
            get
            {
                return this.selectedActivityId;
            }
            set
            {
                this.selectedActivityId = value;
            }
        }

        private ObservableCollection<Activity> activities;

        public ObservableCollection<Activity> Activities
        {
            get
            {
                return this.activities;
            }
            set
            {
                this.activities = value;
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
            var procedure=new DentalProcedure
                {
                    Date = this.date,
                    Diagnosis = this.diagnosis,
                    Note = this.note,
                    PatientId = this.patientId,
                    Tooth = this.tooth,
                    ActivityId=this.selectedActivityId,
                    DoctorId=this.selectedDoctorId,
                    PaidValue=this.paidValue
                };

            if(this.selectedMdtId>0)
            {
                procedure.MDTId = this.selectedMdtId;
            }

            this.dentalProceduresService.Add(procedure);
        }

        public bool IsModelValid()
        {
            var canExecute = true;

            if (String.IsNullOrEmpty(this.tooth))
            {
                canExecute = false;
            }
            if (String.IsNullOrEmpty(this.diagnosis))
            {
                canExecute = false;
            }
            if (this.date == null)
            {
                canExecute = false;
            }
            if (this.patientId <= 0)
            {
                canExecute = false;
            }
            if (this.paidValue < 0)
            {
                canExecute = false;
            }

            if (this.selectedDoctorId <= 0)
            {
                canExecute = false;
            }

            if (this.selectedActivityId <= 0)
            {
                canExecute = false;
            }

            return canExecute;
        }
    }
}
