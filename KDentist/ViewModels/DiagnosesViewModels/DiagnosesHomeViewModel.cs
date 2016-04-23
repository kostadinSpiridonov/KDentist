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

namespace KDentist.ViewModels.DiagnosesViewModels
{
    public class DiagnosesHomeViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private AddDiagnoseViewModel addDiagnoseViewModel;

        public AddDiagnoseViewModel AddDiagnoseViewModel
        {
            get
            {
                return this.addDiagnoseViewModel;
            }
            set
            {
                this.addDiagnoseViewModel = value;
            }
        }

        private ObservableCollection<Diagnose> diagnoses;

        public ObservableCollection<Diagnose> Diagnoses
        {
            get
            {
                return this.diagnoses;
            }
        }

        public DiagnosesHomeViewModel()
        {
            GetDiagnosesFromDB();
            this.addDiagnoseViewModel = new AddDiagnoseViewModel();
        }

        public void GetDiagnosesFromDB()
        {
            var diagnosesDb = this.diagnoseService.GetAll()
               .Reverse()
               .ToList();

            diagnoses = new ObservableCollection<Diagnose>(diagnosesDb);

            NotifyDiagnosesChanged();
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
            MessageBoxResult question = MessageBox.Show("Искате ли да изтриете тази диагноза?", "Изтриване", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (question == MessageBoxResult.Yes)
            {
                int id = int.Parse(obj.ToString());
                var diagnose = this.diagnoseService.GetById(id);
                if (this.diagnoseService.Exist(diagnose.Code))
                {
                    try
                    {
                        this.diagnoseService.Remove(id);
                        Diagnoses.Remove(Diagnoses.Where(x => x.Id == id).FirstOrDefault());
                    }
                    catch
                    {
                        MessageBox.Show("Тази диагноза се използва и не може да бъде изтрита за момента !", "Проблем", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }


            NotifyDiagnosesChanged();
        }

        public void NotifyDiagnosesChanged()
        {
            NotifyPropertyChanged("Diagnoses");
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
