using System;
using System.ComponentModel.DataAnnotations;

namespace ABS_Models.Common
{
    public class CompareDateAttribute : ValidationAttribute
    {
        /// <summary>
        /// Comapre date must be given as valid string.
        /// </summary>
        public string ComapreDate { get; set; }

        public CompareDateAttribute() => ComapreDate = DateTime.Now.ToString();

        public override bool IsValid(object value)
        {
            DateTime date;
            if ((value == null) || !DateTime.TryParse(value.ToString(), out date))
            {
                return false;
            }
            var startDate = DateTime.Parse(ComapreDate);

            return DateTime.Compare(date.Date, startDate.Date) > 0;
        }

    }
}
