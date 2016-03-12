using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Cashier.Util
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DiscountAttribute : Attribute
    {
        /// <summary>
        /// although nothing to use
        /// </summary>
        public const string DESC = "Can be used by discount activity.";

        public string UsedBy { get; set; }

        public string UsedByDesc { get; set; }

        public static DiscountAttribute[] Gets(Type type)
        {
            object[] attributes = type.GetCustomAttributes(typeof(DiscountAttribute), false);
            if (attributes.Any())
            {
                return (DiscountAttribute[])attributes;
            }
            else
            {
                return null;
            }
        }
    }
}
