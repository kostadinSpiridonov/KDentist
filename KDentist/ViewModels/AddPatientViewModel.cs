using KDentist.Commands;
using KDentist.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KDentist.ViewModels
{
    public class AddPatientViewModel:BaseViewModel
    {
        public AddPatientViewModel()
        {
            age=1;
        }
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
            var id=patientService.Add(new Patient
                {
                    Age=this.age,
                    EGN=this.egn,
                    FirstName=this.firstName,
                    LastName=this.lastName,
                    Address=this.address,
                    SecondName=this.secondName,
                    Phone=this.Phone,
                    Profession=this.profession
                });

            this.proceduresTableService.Add(
                new ProceduresTable
                {
                    Id = id
                });

            var path=AppDomain.CurrentDomain.GetData("DataDirectory");
            Directory.CreateDirectory(path + "/Photoes/" + id);
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
            if(!String.IsNullOrEmpty(this.egn))
            {
                if(this.egn.Count()!=10)
                {
                    canExecute = false;
                }
                else
                {
                    if(this.patientService.Exist(egn))
                    {
                        canExecute = false;
                    }
                }
            }

            if(this.age<0)
            {
                canExecute = false;
            }

            return canExecute;
        } 
    }
}
