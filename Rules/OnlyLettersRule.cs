using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Final_Practice.Rules
{
    public class OnlyLettersRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string nums = "0123456789";
            string input = string.Empty;
            try
            {
                input = Convert.ToString((string)value);
            }
            catch (Exception ex)
            {
                return new ValidationResult(false, $"Invalid characters or {ex.Message}");
                throw;
            }

            foreach (char c in nums)
                if(input.Contains(c))
                    return new ValidationResult(false, "Value cannot contain numbers");

            if (string.IsNullOrEmpty(input)) return new ValidationResult(false, "Value cannot be empty or non string type");
            return new ValidationResult(true, null);
        }
    }
}
