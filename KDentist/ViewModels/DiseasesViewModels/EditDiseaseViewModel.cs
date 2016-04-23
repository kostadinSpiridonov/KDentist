using KDentist.Commands;
using KDentist.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KDentist.ViewModels.DiseasesViewModels
{
    public class EditDiseaseViewModel : BaseViewModel, INotifyPropertyChanged
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
            var reminder = new Disease
            {
                Id = this.id,
                Name=this.name
            };

            this.diseaseService.Update(reminder);
        }

        public void LoadData(int id)
        {
            var disease = this.diseaseService.GetById(id);
            this.id = id;
            this.name = disease.Name;
        }
        public bool IsModelValid()
        {
            var canExecute = true;

            if (String.IsNullOrEmpty(this.name))
            {
                canExecute = false;
            }


            if (this.id < 1)
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

        public void NotifyAllChanged()
        {
            NotifyPropertyChanged("Id");
            NotifyPropertyChanged("Name");
        }
    }
}
