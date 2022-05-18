using GraduationProject.Enums;

namespace GraduationProject.Utilities.ExtensionMethods
{
    public static class GeneralExtenders
    {
        public static string ToEnumString(this PeriodType periodType)
        {
            if (periodType == PeriodType.OneTime)
                return "One Time";

            return periodType.ToString();
        }
    }
}
