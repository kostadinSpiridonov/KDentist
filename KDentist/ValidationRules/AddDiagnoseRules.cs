using KDentist.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace KDentist.ValidationRules
{
    public class AddDiagnoseRequiredString : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            string stringValue = "";
            if (value is BindingExpression)
            {
                // ValidationStep was UpdatedValue or CommittedValue (Validate after setting)
                // Need to pull the value out of the BindingExpression.
                BindingExpression binding = (BindingExpression)value;

                // Get the bound object and name of the property
                object dataItem = binding.DataItem;

                var model = (dataItem as KDentist.ViewModels.DiagnosesViewModels.DiagnosesHomeViewModel)
                    .AddDiagnoseViewModel;

                stringValue = model
                    .GetType()
                    .GetProperty(binding.ResolvedSourcePropertyName)
                    .GetValue(model, null)
                    .ToString();

            }
            if (String.IsNullOrEmpty(stringValue))
            {
                return new ValidationResult(false, "Полето е задължително.");
            }
            return ValidationResult.ValidResult;
        }
    }

    public class AddDiagnoseCodeRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            string stringValue = "";
            if (value is BindingExpression)
            {
                // ValidationStep was UpdatedValue or CommittedValue (Validate after setting)
                // Need to pull the value out of the BindingExpression.
                BindingExpression binding = (BindingExpression)value;

                // Get the bound object and name of the property
                object dataItem = binding.DataItem;

                var model = (dataItem as KDentist.ViewModels.DiagnosesViewModels.DiagnosesHomeViewModel)
                    .AddDiagnoseViewModel;

                stringValue = model
                    .GetType()
                    .GetProperty(binding.ResolvedSourcePropertyName)
                    .GetValue(model, null)
                    .ToString();

            }
            if (String.IsNullOrEmpty(stringValue))
            {
                return new ValidationResult(false, "Полето е задължително.");
            }
            if(stringValue.Count()>4)
            {
                return new ValidationResult(false, "Максималният брой символи е 4.");
            }
            var service = new DiagnoseService();

            if (service.Exist(stringValue))
            {
                return new ValidationResult(false, "Съществува диагноза с този код.");
            }
            return ValidationResult.ValidResult;
        }
    }

    public class EditDiagnoseRequiredString : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            string stringValue = "";
            if (value is BindingExpression)
            {
                // ValidationStep was UpdatedValue or CommittedValue (Validate after setting)
                // Need to pull the value out of the BindingExpression.
                BindingExpression binding = (BindingExpression)value;

                // Get the bound object and name of the property
                object dataItem = binding.DataItem;

                var model = (dataItem as KDentist.ViewModels.DiagnosesViewModels.EditDiagnoseViewModel);

                stringValue = model
                    .GetType()
                    .GetProperty(binding.ResolvedSourcePropertyName)
                    .GetValue(model, null)
                    .ToString();

            }
            if (String.IsNullOrEmpty(stringValue))
            {
                return new ValidationResult(false, "Полето е задължително.");
            }
            return ValidationResult.ValidResult;
        }
    }

    public class EditDiagnoseCodeRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            string stringValue = "";
            if (value is BindingExpression)
            {
                // ValidationStep was UpdatedValue or CommittedValue (Validate after setting)
                // Need to pull the value out of the BindingExpression.
                BindingExpression binding = (BindingExpression)value;

                // Get the bound object and name of the property
                object dataItem = binding.DataItem;

                var model = (dataItem as KDentist.ViewModels.DiagnosesViewModels.EditDiagnoseViewModel);

                stringValue = model
                    .GetType()
                    .GetProperty(binding.ResolvedSourcePropertyName)
                    .GetValue(model, null)
                    .ToString();

            }
            if (String.IsNullOrEmpty(stringValue))
            {
                return new ValidationResult(false, "Полето е задължително.");
            }
            if (stringValue.Count() > 4)
            {
                return new ValidationResult(false, "Максималният брой символи е 4.");
            }
            return ValidationResult.ValidResult;
        }
    }
}
