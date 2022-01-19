using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TP_PWEB.Validators
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class DateAttribute: ValidationAttribute
    {
        string EndDate;
        public DateAttribute(string endDate, string errorMessage): base(errorMessage)
        {
            this.EndDate = endDate;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ValidationResult validationResult = ValidationResult.Success;
            try
            {

                var prop = validationContext.ObjectType.GetProperty(this.EndDate);
                
                if (prop.PropertyType.Equals(typeof(DateTime)))
                {

                    DateTime endDate =  (DateTime) prop.GetValue(validationContext.ObjectInstance, null);

                    DateTime startDate = (DateTime)value;
                   
                    if (endDate < startDate)
                    {
                        return  new ValidationResult(ErrorMessageString);
                    }

                    return ValidationResult.Success;
                }
              
                    return new ValidationResult("An error occurred while validating the property. OtherProperty is not of type DateTime");
            }
            catch (Exception ex)
            {
             
                throw ex;
            }
            
        }
       


    }
}
