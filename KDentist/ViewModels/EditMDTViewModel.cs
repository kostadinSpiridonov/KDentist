using KDentist.Commands;
using KDentist.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KDentist.ViewModels
{
    public class EditMDTViewModel:BaseViewModel,INotifyPropertyChanged
    {

        private string name;

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        private string phone;

        public string Phone
        {
            get
            {
                return phone;
            }
            set
            {
                phone = value;
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

        public void LoadData(int id)
        {
            var mdt = this.mdtService.GetById(id);

            this.name = mdt.Name;
            this.phone = mdt.Phone;
            this.id = mdt.Id;
            NotifyAll();
        }

        public void NotifyAll()
        {
            NotifyPropertyChanged("Name");
            NotifyPropertyChanged("Id");
            NotifyPropertyChanged("Phone");
        }
        private void SaveC(object obj)
        {
            this.mdtService.Update(new MDT
            {
                Id=this.id,
                Name = this.name,
                Phone = this.phone
            });
        }

        public bool IsModelValid()
        {
            var canExecute = true;

            if (String.IsNullOrEmpty(this.name))
            {
                canExecute = false;
            }

            if (!String.IsNullOrEmpty(this.phone))
            {
                var r = new Regex("^[0-9]*$");
                if (!r.IsMatch(Phone))
                {
                    canExecute = false;
                }
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
