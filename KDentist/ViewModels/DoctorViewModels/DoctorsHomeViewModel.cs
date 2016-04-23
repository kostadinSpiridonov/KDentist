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

namespace KDentist.ViewModels.DoctorViewModels
{
    public class DoctorsHomeViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private ObservableCollection<Doctor> doctors;

        public ObservableCollection<Doctor> Doctors
        {
            get
            {
                return this.doctors;
            }
        }

        private AddDoctorViewModel addDoctorViewModel;

        public AddDoctorViewModel AddDoctorViewModel
        {
            get
            {
                return this.addDoctorViewModel;
            }
            set
            {
                this.addDoctorViewModel = value;
            }
        }

        public DoctorsHomeViewModel()
        {
            this.addDoctorViewModel = new DoctorViewModels.AddDoctorViewModel();
            GetDoctorstFromDb();
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

        public void NotifyDoctorsChanged()
        {
            NotifyPropertyChanged("Doctors");
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
            MessageBoxResult question = MessageBox.Show("Искате ли да изтриете тoзи лекар?", "Изтриване", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (question == MessageBoxResult.Yes)
            {
                int id = int.Parse(obj.ToString());
                if (this.doctorService.Exist(id))
                {
                    if (this.dentalProceduresService.DoctorHasProcedures(id))
                    {
                        MessageBox.Show("Не може да изтриете този доктор, защото има дентални процедури !", "Проблем", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        this.doctorService.Remove(id);
                        Doctors.Remove(Doctors.Where(x => x.Id == id).FirstOrDefault());
                    }
                }
            }

            NotifyDoctorsChanged();
        }

        public void GetDoctorstFromDb()
        {
            this.doctors = new ObservableCollection<Doctor>(
                this.doctorService.GetAll().ToList());
            NotifyDoctorsChanged();
        }
    }
}
