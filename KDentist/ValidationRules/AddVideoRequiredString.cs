using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace KDentist.ValidationRules
{
    public class AddVideoRequiredString : ValidationRule
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

                var model = (dataItem as KDentist.ViewModels.VideoViewModels.VideosHomeViewModel)
                    .AddVideoViewModel;

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

    public class AddVideoUrl : ValidationRule
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

                var model = (dataItem as KDentist.ViewModels.VideoViewModels.VideosHomeViewModel)
                    .AddVideoViewModel;

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
            Match regexMatch = Regex.Match(stringValue, "^[^v]+v=(.{11}).*",
                        RegexOptions.IgnoreCase);
            if (!regexMatch.Success)
            {
                return new ValidationResult(false, "Адреса е невалиден.");
            }
            return ValidationResult.ValidResult;
        }
    }


    public class EditVideoUrl : ValidationRule
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

                stringValue = dataItem
                    .GetType()
                    .GetProperty(binding.ResolvedSourcePropertyName)
                    .GetValue(dataItem, null)
                    .ToString();

            }
            if (String.IsNullOrEmpty(stringValue))
            {
                return new ValidationResult(false, "Полето е задължително.");
            }
            Match regexMatch = Regex.Match(stringValue, "^[^v]+v=(.{11}).*",
                        RegexOptions.IgnoreCase);
            if (!regexMatch.Success)
            {
                return new ValidationResult(false, "Адреса е невалиден.");
            }
            return ValidationResult.ValidResult;
        }
    }
}
