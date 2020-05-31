using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Core.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum @enum)
        {
            Type type = @enum.GetType();

            MemberInfo member = type
                .GetMembers()
                .Where(w => w.Name == Enum.GetName(type, @enum))
                .FirstOrDefault()
                ;

            var attribute = member?
                .GetCustomAttributes(typeof(DescriptionAttribute), false)
                .FirstOrDefault() as DescriptionAttribute
                ;

            return attribute?.Description ??  @enum.ToString();
        }
    }
}
