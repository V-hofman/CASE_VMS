using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.ComponentModel;

namespace CASE_VMS_Backend.DAL.Conversions
{
    public class DateTime_DateOnly : ValueConverter<DateOnly, DateTime>
    {
        public DateTime_DateOnly() : base(
           dateOnly => dateOnly.ToDateTime(TimeOnly.MinValue),
           dateTime => DateOnly.FromDateTime(dateTime))
        {}
    }
}
