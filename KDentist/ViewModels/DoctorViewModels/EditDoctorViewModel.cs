using KDentist.Commands;
using KDentist.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KDentist.ViewModels.DoctorViewModels
{
    public class EditDoctorViewModel:BaseViewModel,INotifyPropertyChanged
    {
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

        private string firstName;
        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                firstName = value;
            }
        }

        private string secondName;
        public string SecondName
        {
            get
            {
                return secondName;
            }
            set
            {
                secondName = value;
            }
        }

        private string lastName;
        public string LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                lastName = value;
            }
        }

        private ICommand save;
        public ICommand Save
        {
            get
            {
                if (this.save == null)
                {
                    this.save = new RelayCommand(this.AddC);
                }
                return this.save;
            }
        }
        private void AddC(object obj)
        {
            this.doctorService.Update(new Doctor
            {
                Id=this.id,
                FirstName = this.firstName,
                SecondName = this.secondName,
                LastName = this.lastName
            });
        }

        public void LoadData(int id)
        {
            if(this.doctorService.Exist(id))
            {
                var doctor = this.doctorService.GetById(id);

                this.id = doctor.Id;
                this.firstName = doctor.FirstName;
                this.secondName = doctor.SecondName;
                this.lastName = doctor.LastName;
                NotifyAll();
            }
        }

        public void NotifyAll()
        {
            NotifyPropertyChanged("FirstName");
            NotifyPropertyChanged("SecondName");
            NotifyPropertyChanged("LastName");
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
            if(id==0)
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
