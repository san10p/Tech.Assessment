using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Tech.Assessment.API.ValidateAttribute
{
    public class ValidateEnumAttribute : RequiredAttribute
    {
        public ValidateEnumAttribute(Type enumType)
        {
            EnumType = enumType;
        }
        public Type EnumType { get; }
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            foreach (var item in EnumType.GetEnumValues())
            {
                DisplayAttribute displayAttribute = item.GetType()
                    .GetMember(item.ToString())
                    .First()
                    .GetCustomAttributes(typeof(DisplayAttribute), false)
                    .FirstOrDefault() as DisplayAttribute;

                if (displayAttribute != null)
                {
                    if (displayAttribute.Name.ToLower() == value.ToString().ToLower())
                    {
                        return true;
                    }
                }
                else
                {
                    if (item.ToString().ToLower() == value.ToString().ToLower())
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
