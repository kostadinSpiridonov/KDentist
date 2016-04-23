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

namespace KDentist.ViewModels
{
    public class ListMDTViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }
    }

    public class AllMDTViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private NewMDTViewModel newMdtViewModel;

        public NewMDTViewModel NewMDTViewModel
        {
            get
            {
                return this.newMdtViewModel;
            }
            set
            {
                this.newMdtViewModel = value;
            }
        }

        private ObservableCollection<ListMDTViewModel> mdts;

        public ObservableCollection<ListMDTViewModel> MDTs
        {
            get
            {
                return this.mdts;
            }
        }

        public AllMDTViewModel()
        {
            GetMDTsFromDB();

            this.newMdtViewModel = new NewMDTViewModel();
        }

        public void GetMDTsFromDB()
        {
            var patientsDB = this.mdtService.GetAll()
               .Reverse()
               .ToList()
               .ConvertAll(x => new ListMDTViewModel
               {
                   Id = x.Id,
                   Name = x.Name,
                   Phone = x.Phone
               });

            mdts = new ObservableCollection<ListMDTViewModel>(patientsDB);

            NotifyMDTsChanged();
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
            MessageBoxResult question = MessageBox.Show("Искате ли да изтриете тoзи зъботехник?", "Изтриване", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (question == MessageBoxResult.Yes)
            {
                int id = int.Parse(obj.ToString());
                if (this.mdtService.Exist(id))
                {
                    try
                    {
                        this.mdtService.Remove(id);
                        MDTs.Remove(MDTs.Where(x => x.Id == id).FirstOrDefault());
                    }
                    catch
                    {
                        MessageBox.Show("Този зъботехник се използва в дентални процедури и не може да бъде иззтрит за момента !", "Проблем", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }


            NotifyMDTsChanged();
        }

        public void NotifyMDTsChanged()
        {
            NotifyPropertyChanged("MDTs");
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
