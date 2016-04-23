using KDentist.Commands;
using KDentist.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace KDentist.ViewModels.ActivityViewModels
{
    public class AddActivityViewModel:BaseViewModel
    {
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

        private string desctiption;
        public string Description
        {
            get
            {
                return this.desctiption;
            }
            set
            {
                this.desctiption = value;
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
            this.activityService.Add(new Activity
            {
                Name = this.name,
                Description = this.desctiption,
                Price = this.price
            });
        }

        public bool IsModelValid()
        {
            var canExecute = true;

            if (String.IsNullOrEmpty(this.name))
            {
                canExecute = false;
            }

            if (price<0)
            {
                canExecute = false;
            }

            return canExecute;
        }
    }
}
