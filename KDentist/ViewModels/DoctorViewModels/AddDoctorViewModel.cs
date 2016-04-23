using KDentist.Commands;
using KDentist.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KDentist.ViewModels.DoctorViewModels
{
    public class AddDoctorViewModel:BaseViewModel
    {
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
            this.doctorService.Add(new Doctor
            {
                FirstName=this.firstName,
                SecondName=this.secondName,
                LastName=this.lastName
            });
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

            return canExecute;
        }
    }
}
