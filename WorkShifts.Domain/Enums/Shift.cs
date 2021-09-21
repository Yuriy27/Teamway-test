using System.ComponentModel;

namespace WorkShifts.Domain.Enums
{
    public enum Shift
    {
        [Description("0-8")]
        First,

        [Description("8-16")]
        Second,

        [Description("16-24")]
        Third
    }
}
