using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSU_CarService.Services
{
    public interface IValidationService
    {
        bool ValidateStringPropertyLenght<T>(string propName, string propValue);
    }

    public class ValidationService : IValidationService
    {
        public bool ValidateStringPropertyLenght<T>(string propName, string propValue)
        {        
            if(string.IsNullOrEmpty(propName)) throw new ArgumentException(nameof(propName));
            if (propValue == null) return true;

            var props = typeof(T).GetProperties();
            var targetProp = props.FirstOrDefault(c => c.Name == propName);
            if (targetProp != null)
            {
                var attributes = targetProp.GetCustomAttributes(true);
                var targetAttr = attributes.FirstOrDefault(c => c is StringLengthAttribute);
                if (targetAttr != null)
                {
                    var maxLen = ((StringLengthAttribute)targetAttr).MaximumLength;
                    return propValue.Length < maxLen;
                }
                throw new Exception($"StringLengthAttribute not found in property {propName}, type {nameof(T)}");
            }
            throw new Exception($"Property {propName} not found in type {nameof(T)}");
        }
    }
}
