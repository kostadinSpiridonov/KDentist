using KDentist.Commands;
using KDentist.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KDentist.ViewModels.DentalProcedureViewModels
{
    public class EditDentalProcedureViewModel:BaseViewModel,INotifyPropertyChanged
    {
        public EditDentalProcedureViewModel()
        {
            this.mdtS = new ObservableCollection<MDT>(this.mdtService.GetAll());
            this.mdtS.Insert(0,new MDT());
            this.doctors = new ObservableCollection<Doctor>(this.doctorService.GetAll());
            this.activities = new ObservableCollection<Activity>(this.activityService.GetAll());
            this.date = DateTime.Now;
        }

        private string tooth;

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

        private int id;

        public int Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
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

        private ICommand save;
        public ICommand Save
        {
            get
            {
                if (this.save == null)
                {
                    this.save = new RelayCommand(this.SaveC);
                }
                return this.save;
            }
        }
        private void SaveC(object obj)
        {
            var procedure=new DentalProcedure
                {
                    Date = this.date,
                    Diagnosis = this.diagnosis,
                    Note = this.note,
                    PatientId = this.patientId,
                    Tooth = this.tooth,
                    ActivityId=this.selectedActivityId,
                    MDTId=this.selectedMdtId,
                    DoctorId=this.selectedDoctorId,
                    Id=this.id,
                    PaidValue=this.paidValue
                };

            if(this.selectedMdtId>0)
            {
                procedure.MDTId = this.selectedMdtId;
            }
            else
            {
                procedure.MDTId = null;
            }

            this.dentalProceduresService.Update(procedure);
        }

        public void LoadData(int id)
        {
            var procedure = this.dentalProceduresService.GetById(id);
            this.date = procedure.Date;
            this.diagnosis = procedure.Diagnosis;
            this.note = procedure.Note;
            this.tooth = procedure.Tooth;
            this.selectedActivityId = procedure.ActivityId;
            this.selectedDoctorId = procedure.DoctorId;
            this.patientId = procedure.PatientId.Value;
            this.selectedMdtId = procedure.MDTId.HasValue?procedure.MDTId.Value:0;
            this.id = id;
            this.paidValue = procedure.PaidValue;
            NotifyAll();
        }

        public void NotifyAll()
        {
            NotifyPropertyChanged("Procedure");
            NotifyPropertyChanged("Date");
            NotifyPropertyChanged("Diagnosis");
            NotifyPropertyChanged("Note");
            NotifyPropertyChanged("Tooth");
            NotifyPropertyChanged("SelectedActivityId");
            NotifyPropertyChanged("SelectedDoctorId");
            NotifyPropertyChanged("Id");
            NotifyPropertyChanged("SelectedMDTId");
            NotifyPropertyChanged("PaidValue");
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

            if (this.selectedDoctorId <= 0)
            {
                canExecute = false;
            }

            if (this.selectedActivityId <= 0)
            {
                canExecute = false;
            }
            if (this.paidValue < 0)
            {
                canExecute = false;
            }
            return canExecute;
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
    }
}
