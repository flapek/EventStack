using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventStack_API.UnitTest.Helpers
{
    public static class ModelValidator
    {
        private static IList<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults;
        }

        public static bool isValid(this object input, string propertyName, string errorMessage)
        {
            return !ValidateModel(input).Any(a => a.MemberNames.Contains(propertyName)
                                                 && a.ErrorMessage.Contains(errorMessage));
        }
    }
}