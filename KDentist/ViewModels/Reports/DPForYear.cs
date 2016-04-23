using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDentist.ViewModels.Reports
{
    public class DPForYearViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private ObservableCollection<DPMonth> months;

        public ObservableCollection<DPMonth> Months
        {
            get
            {
                return this.months;
            }
            set
            {
                this.months = value;
            }
        }

        private string y;
        public string Year
        {
            get
            {
                return this.y;
            }
            set
            {
                this.y = value;
                LoadData();
            }
        }

        public decimal Total { get; set; }

        public void LoadData()
        {
            this.months = new ObservableCollection<DPMonth>();
            if (Year.Count() == 4)
            {
                int year;
                if (int.TryParse(this.Year, out year))
                {
                    var date = new DateTime(year, 1, 1);
                    Total = 0;
                    for (int i = 0; i < 12; i++)
                    {
                        months.Add(new DPMonth(date));
                        date=date.AddMonths(1);
                        Total += months.Last().Price;
                    }

                    NotifyPropertyChanged("Total");
                }
            }
            NotifyPropertyChanged("Months");
        }

        public DPForYearViewModel()
        {
            this.Year = DateTime.Now.Year.ToString();
            this.LoadData();
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

    public class DPMonth : BaseViewModel
    {
        public string Month { get; set; }

        public int CountProcedures { get; set; }

        public decimal Price { get; set; }

        public DPMonth(DateTime date)
        {
            this.Month = this.TranslateMonth(date.Month);
            var dps = this.dentalProceduresService.GetAll()
                .Where(x => x.Date.Year == date.Year && x.Date.Month == date.Month);
            this.CountProcedures = dps.Count();
            decimal pr = 0;
            foreach (var item in dps)
            {
                pr += item.Activity.Price;
            }
            this.Price = pr;
        }

        private string TranslateMonth(int index)
        {
            switch(index)
            {
                case 1:
                    {
                        return "Януари";
                    }
                case 2:
                    {
                        return "Февруари";
                    }
                case 3:
                    {
                        return "Март";
                    }
                case 4:
                    {
                        return "Април";
                    }
                case 5:
                    {
                        return "Май";
                    }
                case 6:
                    {
                        return "Юни";
                    }
                case 7:
                    {
                        return "Юли";
                    }
                case 8:
                    {
                        return "Август";
                    }
                case 9:
                    {
                        return "Септември";
                    }
                case 10:
                    {
                        return "Октомври";
                    }
                case 11:
                    {
                        return "Ноември";
                    }
                case 12:
                    {
                        return "Декември";
                    }
            }
            return "";
        }
    }
}
