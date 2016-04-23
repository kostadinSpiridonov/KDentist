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

namespace KDentist.ViewModels.DiseasesViewModels
{
    public class HomeDiseasesViewModel:BaseViewModel,INotifyPropertyChanged
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

        public void UpdateContext()
        {
            this.diseaseService.UpdateContext();
        }

        public HomeDiseasesViewModel()
        {
            GetDiseasesFromDb();
        }

        private ObservableCollection<Disease> diseases;

        public ObservableCollection<Disease> Diseases
        {
            get
            {
                return this.diseases;
            }
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

        public void NotifyDiseasesChanged()
        {
            NotifyPropertyChanged("Diseases");
        }

        public void GetDiseasesFromDb()
        {
            this.diseases = new ObservableCollection<Disease>(this.diseaseService
                 .GetForPatient(id)
                .ToList());
            NotifyDiseasesChanged();
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
            MessageBoxResult question = MessageBox.Show("Искате ли да изтриете това заболяване?", "Изтриване", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (question == MessageBoxResult.Yes)
            {
                int id = int.Parse(obj.ToString());
                this.diseaseService.Remove(id);
                diseases.Remove(diseases.Where(x => x.Id == id).FirstOrDefault());
                NotifyDiseasesChanged();
            }
        }

    }
}
