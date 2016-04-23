using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace KDentist.ValidationRules
{
    public class AddActivityRequiredString : ValidationRule
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

                var model = (dataItem as KDentist.ViewModels.ActivityViewModels.ActivitiesHomeViewModel)
                    .AddActivityViewModel;

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
}
