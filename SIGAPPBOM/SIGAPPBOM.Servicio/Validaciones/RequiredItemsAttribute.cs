using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SIGAPPBOM.Servicio.Validaciones
{
    public class RequiredItemsAttribute : ValidationAttribute, IClientValidatable
    {
        public RequiredItemsAttribute(double minValue)
            : base("El {0} debe ser contener al menos un item")
        {}

        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessageString, name);
        }

        public override bool IsValid(object value)
        {
            var doubleValue = Convert.ToDouble(value);
            return doubleValue > 0;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
                           {
                               ErrorMessage = this.FormatErrorMessage(metadata.PropertyName),
                               ValidationType = "requiredItems"
                           };
            rule.ValidationParameters.Add("value", 0);

            yield return rule;
        }
    }
}
