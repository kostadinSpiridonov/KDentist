using KDentist.Commands;
using KDentist.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KDentist.ViewModels.DiseasesViewModels
{
    public class AddDiseaseViewModel: BaseViewModel
    {

        private string name;

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
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
            var reminder = new Disease
            {
                Name=this.name,
                PatientId=this.id
            };

            this.diseaseService.Add(reminder);
        }

        public bool IsModelValid()
        {
            var canExecute = true;

            if (String.IsNullOrEmpty(this.name))
            {
                canExecute = false;
            }
            

            if (this.id <1)
            {
                canExecute = false;
            }

            return canExecute;
        }
    }
}
