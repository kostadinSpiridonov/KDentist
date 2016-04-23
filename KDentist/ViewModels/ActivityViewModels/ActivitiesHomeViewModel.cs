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

namespace KDentist.ViewModels.ActivityViewModels
{
    public class ActivitiesHomeViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private ObservableCollection<Activity> activities;

        public ObservableCollection<Activity> Activities
        {
            get
            {
                return this.activities;
            }
        }

        private AddActivityViewModel addActivityViewModel;

        public AddActivityViewModel AddActivityViewModel
        {
            get
            {
                return this.addActivityViewModel;
            }
            set
            {
                this.addActivityViewModel = value;
            }
        }

        public ActivitiesHomeViewModel()
        {
            this.addActivityViewModel = new AddActivityViewModel();
            GetActivitiesFromDb();
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

        public void NotifyActivitiesChanged()
        {
            NotifyPropertyChanged("Activities");
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
            MessageBoxResult question = MessageBox.Show("Искате ли да изтриете тази дейност?", "Изтриване", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (question == MessageBoxResult.Yes)
            {
                int id = int.Parse(obj.ToString());
                if (this.activityService.Exist(id))
                {
                    if (this.dentalProceduresService.ActivityHasProcedures(id))
                    {
                        MessageBox.Show("Тази дейност се използва в някои дентални процедури и в момента не може да бъде изтрита !", "Проблем", MessageBoxButton.OK,
                            MessageBoxImage.Warning);
                    }
                    else
                    {
                        this.activityService.Remove(id);
                        Activities.Remove(Activities.Where(x => x.Id == id).FirstOrDefault());
                    }
                }
            }
            NotifyActivitiesChanged();
        }

        public void GetActivitiesFromDb()
        {
            this.activities = new ObservableCollection<Activity>(
                this.activityService.GetAll().ToList());
            NotifyActivitiesChanged();
        }
    }
}
