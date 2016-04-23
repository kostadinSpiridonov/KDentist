using KDentist.Commands;
using KDentist.Model;
using KDentist.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KDentist.ViewModels.CalculatorViewModels
{
    public class CalculatorHomeViewModel:BaseViewModel,INotifyPropertyChanged
    {
        public CalculatorHomeViewModel()
        {
            this.activities = new ObservableCollection<Activity>(this.activityService.GetAll());
            this.rows = new ObservableCollection<CalculationRow>();
            rows.Add(new CalculationRow
                {
                    CountProcedures = 1,
                    Guid=Guid.NewGuid().ToString()
                });
            rows.Add(new CalculationRow
            {
                CountProcedures = 1,
                Guid = Guid.NewGuid().ToString()
            });

            NotifyPropertyChanged("Activities");
            NotifyPropertyChanged("Rows");

            total = 0;
        }

        private ObservableCollection<CalculationRow> rows;

        public ObservableCollection<CalculationRow> Rows
        {
            get
            {
                return this.rows;
            }
        }

        private ObservableCollection<Activity> activities;

        public ObservableCollection<Activity> Activities
        {
            get
            {
                return this.activities;
            }
        }

        private decimal total;

        public decimal Total
        {
            get
            {
                return this.total;
            }
            set
            {
                this.total = value;
            }
        }

        private string firstName;
        public string FirstName
        {
            get
            {
                return this.firstName;
            }
            set
            {
                this.firstName = value;
            }
        }

        private string lastName;
        public string LastName
        {
            get
            {
                return this.lastName;
            }
            set
            {
                this.lastName = value;
            }
        }

        private string doctor;
        public string Doctor
        {
            get
            {
                return this.doctor;
            }
            set
            {
                this.doctor = value;
            }
        }
      
        private string diagnose;
        public string Diagnose
        {
            get
            {
                return this.diagnose;
            }
            set
            {
                this.diagnose = value;
            }
        }
       
        private string note;
        public string Note
        {
            get
            {
                return this.note;
            }
            set
            {
                this.note = value;
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
            rows.Add(new CalculationRow
            {
                CountProcedures = 1,
                Guid = Guid.NewGuid().ToString()
            });
            NotifyPropertyChanged("Rows");
        }


        private ICommand remove;
        public ICommand Remove
        {
            get
            {
                if (this.remove == null)
                {
                    this.remove = new RelayCommand(this.RemoveC);
                }
                return this.remove;
            }
        }
        private void RemoveC(object obj)
        {
            var row = rows.Where(x => x.Guid == obj.ToString()).FirstOrDefault();
            rows.Remove(row);
            NotifyPropertyChanged("Rows");
        }

        public void CalculateTotal()
        {
            decimal tot = 0;
            foreach (var item in rows)
            {
                tot += item.RowPrice;
            }
            Total = tot;
            NotifyPropertyChanged("Total");
            NotifyPropertyChanged("Total");
        }
    }

    public class CalculationRow:BaseViewModel,INotifyPropertyChanged
    {
        public string Guid { get; set; }

        private Activity selectedActivity;

        public Activity SelectedActivity
        {
            get
            {
                return this.selectedActivity;
            }
            set
            {
                this.selectedActivity = value;
                CalculateRowPrice();
            }
        }

        public int CountProcedures { get; set; }

        public decimal RowPrice { get; set; }

        public CalculationRow()
        {
            this.RowPrice = 0;
        }

        public void CalculateRowPrice()
        {
            if (SelectedActivity != null)
            {
                RowPrice = decimal.Parse(CountProcedures.ToString()) * SelectedActivity.Price;
                NotifyPropertyChanged("RowPrice");
                NotifyPropertyChanged("SelectedActivity");
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
    }
}
