using KDentist.ViewModels.ActivityViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace KDentist.ValidationRules
{
    public class PriceRequiredActivity : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            decimal decimalValue = 0;
            if (value is BindingExpression)
            {
                // ValidationStep was UpdatedValue or CommittedValue (Validate after setting)
                // Need to pull the value out of the BindingExpression.
                BindingExpression binding = (BindingExpression)value;

                // Get the bound object and name of the property
                object dataItem = binding.DataItem;

                var model = new object();

                var tp = binding.ResolvedSource.GetType().ToString();
                if (binding.ResolvedSource.GetType().ToString() == "KDentist.ViewModels.ActivityViewModels.AddActivityViewModel")
                {
                    model = (dataItem as ActivitiesHomeViewModel)
                    .AddActivityViewModel;

                }
                else if (binding.ResolvedSource.GetType().ToString() == "KDentist.ViewModels.ActivityViewModels.EditActivityViewModel")
                {
                    model = (dataItem as EditActivityViewModel);

                }

                if (!decimal.TryParse(model
                    .GetType()
                    .GetProperty(binding.ResolvedSourcePropertyName)
                    .GetValue(model, null).ToString(),
                     out decimalValue)
                    )
                {
                    return new ValidationResult(false, "Стойността на полето не е валидна.");
                }
                else
                {
                    if (decimalValue < 0)
                    {
                        return new ValidationResult(false, "Стойността на полето не е валидна.");
                    }
                }

            }
            return ValidationResult.ValidResult;
        }
    }
}
