using KDentist.Commands;
using KDentist.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace KDentist.ViewModels.DiagnosesViewModels
{
    public class AddDiagnoseViewModel : BaseViewModel
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
            this.diagnoseService.Add(new Diagnose
            {
                Name = this.name,
                Code=this.code
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
                if (code.Count()>4)
                {
                    canExecute = false;
                }
            }

            if(diagnoseService.Exist(this.code))
            {
                canExecute = false;
            }
            return canExecute;
        }
    }
}
