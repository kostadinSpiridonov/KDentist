using KDentist.Commands;
using KDentist.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KDentist.ViewModels.ActivityViewModels
{
    public class EditActivityViewModel : BaseViewModel, INotifyPropertyChanged
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

        private string description;
        public string Description
        {
            get
            {
                return this.description;
            }
            set
            {
                this.description = value;
            }
        }

        private decimal price;
        public decimal Price
        {
            get
            {
                return this.price;
            }
            set
            {
                this.price = value;
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
            this.activityService.Update(new Activity
            {
                Id = this.id,
                Name=this.name,
                Description=this.description,
                Price=this.price
            });
        }

        public void LoadData(int id)
        {
            if (this.activityService.Exist(id))
            {
                var activity = this.activityService.GetById(id);

                this.id = activity.Id;
                this.name = activity.Name;
                this.description = activity.Description;
                this.price = activity.Price;
                NotifyAll();
            }
        }

        public void NotifyAll()
        {
            NotifyPropertyChanged("Name");
            NotifyPropertyChanged("Description");
            NotifyPropertyChanged("Price");
        }

        public bool IsModelValid()
        {
            var canExecute = true;

            if (String.IsNullOrEmpty(this.name))
            {
                canExecute = false;
            }

            if (price < 0)
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
