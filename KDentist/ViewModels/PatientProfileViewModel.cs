using KDentist.Commands;
using KDentist.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace KDentist.ViewModels
{
    public class PatientProfileViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private string phone;

        public string Phone
        {
            get
            {
                return this.phone;
            }
            set
            {
                this.phone = value;
            }
        }

        private string firstName;

        public string FirstName
        {
            get
            {
                return this.firstName;
            }
            set
            {
                this.firstName = value;
            }
        }

        private string secondName;

        public string SecondName
        {
            get
            {
                return this.secondName;
            }
            set
            {
                this.secondName = value;
            }
        }

        private string lastName;

        public string LastName
        {
            get
            {
                return this.lastName;
            }
            set
            {
                this.lastName = value;
            }
        }

        private string egn;

        public string EGN
        {
            get
            {
                return this.egn;
            }
            set
            {
                this.egn = value;
            }
        }

        private int age;

        public int Age
        {
            get
            {
                return this.age;
            }
            set
            {
                this.age = value;
            }
        }

        private string profession;

        public string Profession
        {
            get
            {
                return this.profession;
            }
            set
            {
                this.profession = value;
            }
        }

        private string address;

        public string Address
        {
            get
            {
                return this.address;
            }
            set
            {
                this.address = value;
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

        private ObservableCollection<ListDentapProcedureViewModel> dentalProcedures;

        public ObservableCollection<ListDentapProcedureViewModel> DentalProcedures
        {
            get
            {
                return this.dentalProcedures;
            }
        }

        public void LoadData(string egnIn)
        {
            var patient = this.patientService.GetByEGN(egnIn);

            this.egn = patient.EGN;
            this.firstName = patient.FirstName;
            this.secondName = patient.SecondName;
            this.lastName = patient.LastName;
            this.profession = patient.Profession;
            this.phone = patient.Phone;
            this.address = patient.Address;
            this.id = patient.Id;
            this.age = patient.Age;
            this.dentalProcedures = new ObservableCollection<ListDentapProcedureViewModel>(
                this.dentalProceduresService.GetAll()
                .Where(x => x.PatientId == this.id)
                .ToList()
                .ConvertAll(x => new ListDentapProcedureViewModel
                {
                    Date = x.Date.ToShortDateString(),
                    Doctor = x.Doctor.FirstName + " " + x.Doctor.LastName,
                    Activity = x.Activity.Name,
                    Note = x.Note,
                    Tooth = x.Tooth,
                    Id = x.Id
                }));
            NotifyAll();
        }

        public void LoadData(int idIn)
        {
            var patient = this.patientService.GetById(idIn);

            this.egn = patient.EGN;
            this.firstName = patient.FirstName;
            this.secondName = patient.SecondName;
            this.lastName = patient.LastName;
            this.profession = patient.Profession;
            this.phone = patient.Phone;
            this.address = patient.Address;
            this.id = patient.Id;
            this.age = patient.Age;
            this.dentalProcedures = new ObservableCollection<ListDentapProcedureViewModel>(
                this.dentalProceduresService.GetAll()
                .Where(x => x.PatientId == this.id)
                .ToList()
                .ConvertAll(x => new ListDentapProcedureViewModel
                {
                    Date = x.Date.ToShortDateString(),
                    Doctor = x.Doctor.FirstName+" "+ x.Doctor.LastName,
                    Activity =x.Activity.Name,
                    Note = x.Note,
                    Tooth = x.Tooth,
                    Id=x.Id
                }));

            NotifyAll();
        }

        public void NotifyAll()
        {
            NotifyPropertyChanged("EGN");
            NotifyPropertyChanged("FirstName");
            NotifyPropertyChanged("SecondName");
            NotifyPropertyChanged("LastName");
            NotifyPropertyChanged("Profession");
            NotifyPropertyChanged("Phone");
            NotifyPropertyChanged("Address");
            NotifyPropertyChanged("Age");
            NotifyPropertyChanged("DentalProcedures");
            NotifyPropertyChanged("TableViewModel");
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
            patientService.Update(new Patient
            {
                Age = this.age,
                EGN = this.egn,
                FirstName = this.firstName,
                LastName = this.lastName,
                Profession = this.profession,
                SecondName = this.secondName,
                Address = this.address,
                Id = this.id,
                Phone = this.phone
            });
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
            MessageBoxResult question = MessageBox.Show("Искате ли да изтриете тази процедура?", "Изтриване", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (question == MessageBoxResult.Yes)
            {
                int id = int.Parse(obj.ToString());
                if (this.dentalProceduresService.Exist(id))
                {
                    this.dentalProceduresService.Remove(id);
                    dentalProcedures.Remove(dentalProcedures.Where(x => x.Id == id).FirstOrDefault());
                }
            }
            NotifyAll();
        }



        public bool IsModelValid()
        {
            var canExecute = true;

            if (String.IsNullOrEmpty(this.firstName))
            {
                canExecute = false;
            }
            if (String.IsNullOrEmpty(this.secondName))
            {
                canExecute = false;
            }
            if (String.IsNullOrEmpty(this.lastName))
            {
                canExecute = false;
            }
            if (String.IsNullOrEmpty(this.egn))
            {
                canExecute = false;
            }
            if (!String.IsNullOrEmpty(this.egn))
            {
                if (this.egn.Count() != 10)
                {
                    canExecute = false;
                }
                else
                {
                    if (this.patientService.ExistUpdate(id, egn))
                    {
                        canExecute = false;
                    }
                }
            }

            if (this.age < 0)
            {
                canExecute = false;
            }

            return canExecute;
        }
    }

    public class ListDentapProcedureViewModel
    {
        public string Tooth { get; set; }
        
        public string Activity { get; set; }

        public string Note { get; set; }

        public string Date { get; set; }

        public string Doctor { get; set; }

        public int Id { get; set; }
    }
}
