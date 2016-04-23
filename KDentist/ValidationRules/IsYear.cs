﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace KDentist.ValidationRules
{
    public class IsYear : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            string stringValue = GetBoundValue(value) as string;
            if (String.IsNullOrEmpty(stringValue))
            {
                return new ValidationResult(false, "Полето е задължително.");
            }
            if (stringValue.Count() == 4)
            {
                int year;
                if (int.TryParse(stringValue, out year))
                {
                    return ValidationResult.ValidResult;
                }
            }
            return new ValidationResult(false, "Стойността на полето е невалидна!");
        }

        private object GetBoundValue(object value)
        {
            if (value is BindingExpression)
            {
                // ValidationStep was UpdatedValue or CommittedValue (Validate after setting)
                // Need to pull the value out of the BindingExpression.
                BindingExpression binding = (BindingExpression)value;

                // Get the bound object and name of the property
                object dataItem = binding.DataItem;
                string propertyName = binding.ParentBinding.Path.Path;

                // Extract the value of the property.
                object propertyValue = dataItem.GetType().GetProperty(propertyName).GetValue(dataItem, null);

                // This is what we want.
                return propertyValue;
            }
            else
            {
                // ValidationStep was RawProposedValue or ConvertedProposedValue
                // The argument is already what we want!
                return value;
            }
        }
    }
}