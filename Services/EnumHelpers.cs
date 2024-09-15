using System.ComponentModel;

namespace HappyNoodles_ManagementApp.Services
{
    public static class EnumHelpers
    {
        public static string GetEnumDescription(Enum value)
        {
            if (value == null) { return ""; }

            DescriptionAttribute attribute = value.GetType()
                    .GetField(value.ToString())
                    ?.GetCustomAttributes(typeof(DescriptionAttribute), false)
                    .SingleOrDefault() as DescriptionAttribute;
            return attribute == null ? value.ToString() : attribute.Description;
        }

    }
}
