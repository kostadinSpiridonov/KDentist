using KDentist.Service;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace KDentist.ValidationRules
{
    public class RequiredRuleString : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            string stringValue = GetBoundValue(value) as string;
            if (String.IsNullOrEmpty(stringValue))
            {
                return new ValidationResult(false, "Полето е задължително.");
            }
            return ValidationResult.ValidResult;
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

    public class RequiredRulePositiveInt : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            string stringValue = GetBoundValue(value).ToString();
            int intValue=0;
            if (int.TryParse(stringValue,out intValue))
            {
                if (intValue < 1)
                {
                    return new ValidationResult(false, "Стойността на полето трябва да бъде по-голяма от 0.");
                }
            }
            else
            {
                if (String.IsNullOrEmpty(stringValue))
                {
                    return new ValidationResult(false, "Полето е задължително.");
                }
                else
                {
                    return new ValidationResult(false, "Стойността на полето трябва да е число.");
                }
            }
            value = 101;
            return ValidationResult.ValidResult;
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

    public class PositiveIntRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            string stringValue = GetBoundValue(value).ToString();
            int intValue = 0;
            if (int.TryParse(stringValue, out intValue))
            {
                if (intValue < 1)
                {
                    return new ValidationResult(false, "Стойността на полето трябва да бъде по-голяма от 0.");
                }
            }
            else
            {
                    return new ValidationResult(false, "Стойността на полето трябва да е число.");
            }
            return ValidationResult.ValidResult;
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

    public class EGNRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            string stringValue = GetBoundValue(value).ToString();
            if (stringValue.Count()!=10)
            {
                    return new ValidationResult(false, "Полето ЕГН не е валидно.");
            }
            if (stringValue.Count() == 10)
            {
                PatientService ps = new PatientService();
                if (ps.Exist(stringValue))
                {
                    return new ValidationResult(false, "Има пациент със същото ЕГН !");
                }
            }
            return ValidationResult.ValidResult;
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

    public class RequiredRuleHour: ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            string stringValue = GetBoundValue(value).ToString();
            int intValue = 0;
            if (int.TryParse(stringValue, out intValue))
            {
                if (intValue < 0||intValue>24)
                {
                    return new ValidationResult(false, "Стойността на полето трябва да бъде по-голяма или равна на 0 и по-малка или равна на 24.");
                }
            }
            else
            {
                if (String.IsNullOrEmpty(stringValue))
                {
                    return new ValidationResult(false, "Полето е задължително.");
                }
                else
                {
                    return new ValidationResult(false, "Стойността на полето трябва да е число.");
                }
            }
            value = 101;
            return ValidationResult.ValidResult;
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

    public class RequiredRuleuMinute : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            string stringValue = GetBoundValue(value).ToString();
            int intValue = 0;
            if (int.TryParse(stringValue, out intValue))
            {
                if (intValue < 0 || intValue > 60)
                {
                    return new ValidationResult(false, "Стойността на полето трябва да бъде по-голяма или равна на 0 и по-малка или равна на 60.");
                }
            }
            else
            {
                if (String.IsNullOrEmpty(stringValue))
                {
                    return new ValidationResult(false, "Полето е задължително.");
                }
                else
                {
                    return new ValidationResult(false, "Стойността на полето трябва да е число.");
                }
            }
            value = 101;
            return ValidationResult.ValidResult;
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

    public class PositiveDecimalRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            string stringValue = GetBoundValue(value).ToString();
            decimal decValue = 0;
            if (decimal.TryParse(stringValue, out decValue))
            {
                if (decValue < 0)
                {
                    return new ValidationResult(false, "Стойността на полето трябва да бъде по-голяма от 0.");
                }
            }
            else
            {
                return new ValidationResult(false, "Стойността на полето трябва да е число.");
            }
            return ValidationResult.ValidResult;
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
