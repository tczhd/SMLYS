using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace SMLYS.ApplicationCore.Extensions
{
    public static class EnumExtension
    {

        /// <summary>
        /// Retrieve any attribute you have on an Enum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns>Gets the value of the attribute</returns>
        public static T GetAttribute<T>(this Enum value) where T : Attribute
        {
            var type = value.GetType();
            var memberInfo = type.GetMember(value.ToString());

            var attributes = memberInfo[0].GetCustomAttributes(typeof(T), false);

            if (attributes.Any() && attributes[0] is T)
            {
                return (T)attributes[0];
            }

            throw new Exception($"The custom attribute {typeof(T)} has not been implemented on the '{value}' enumeration. ");
        }

        /// <summary>
        /// Retrieve the description Attribute from an Enum
        /// </summary>
        /// <param name="value"></param>
        /// <returns>The description if it exists other wise the enum name</returns>
        public static string GetDescription(this Enum value)
        {
            var attribute = value.GetAttribute<DescriptionAttribute>();
            return attribute == null ? value.ToString() : attribute.Description;
        }

    }
}
