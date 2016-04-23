using KDentist.Commands;
using KDentist.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace KDentist.ViewModels.DiagnosesViewModels
{
    public class EditDiagnoseViewModel : BaseViewModel, INotifyPropertyChanged
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

        private string code;

        public string Code
        {
            get
            {
                return code;
            }
            set
            {
                code = value;
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
            var diagnose = this.diagnoseService.GetById(id);

            this.name = diagnose.Name;
            this.code = diagnose.Code;
            this.id = diagnose.Id;
            NotifyAll();
        }

        public void NotifyAll()
        {
            NotifyPropertyChanged("Name");
            NotifyPropertyChanged("Id");
            NotifyPropertyChanged("Code");
        }
        private void SaveC(object obj)
        {
            this.diagnoseService.Update(new Diagnose
            {
                Id = this.id,
                Name = this.name,
                Code = this.code
            });
        }

        public bool IsModelValid()
        {
            var canExecute = true;

            if (String.IsNullOrEmpty(this.name))
            {
                canExecute = false;
            }
            if (String.IsNullOrEmpty(this.code))
            {
                canExecute = false;
            }

            if (!String.IsNullOrEmpty(this.code))
            {
                if (code.Count() > 4)
                {
                    canExecute = false;
                }
            }

            if (diagnoseService.ExistUpdate(this.code, this.id))
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
