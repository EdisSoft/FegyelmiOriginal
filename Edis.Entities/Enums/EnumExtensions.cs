using Edis.Entities.Attributes;
using System;
using System.ComponentModel;

namespace Edis.Entities.Enums
{
    public static class EnumExtensions
    {
        //public static string ToDescriptionString(this FalezStatusz val)
        //{
        //    var attributes = (DescriptionAttribute[])val.GetType().GetField(val.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
        //    return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        //}

        public static string ToDescriptionString(this Enum val)
        {
            var attributes = (DescriptionAttribute[])val.GetType().GetField(val.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }

        public static string ToTitleString(this Enum val)
        {
            var attributes = (TitleAttribute[])val.GetType().GetField(val.ToString()).GetCustomAttributes(typeof(TitleAttribute), false);
            return attributes.Length > 0 ? attributes[0].Title : string.Empty;
        }

        public static string ToGroupName(this Enum val)
        {            
            var attributes = (GroupNameAttribute[])val.GetType().GetField(val.ToString()).GetCustomAttributes(typeof(GroupNameAttribute), false);
            return attributes.Length > 0 ? attributes[0].Name : string.Empty;
        }

        public static int CastToInt(this Enum val)
        {
            return Convert.ToInt32(val);
        }

        public static bool CastToBool(this Enum val)
        {
            var integer = Convert.ToInt32(val);
            if (integer > 1)
                throw new ArgumentException("Az enum értéke nagyobb, mint 1 a bool castolás során!");

            return Convert.ToBoolean(integer);
        }

        public static int GetSortOrder(this Enum val)
        {
            var attributes = (EnumOrderAttribute[])val.GetType().GetField(val.ToString()).GetCustomAttributes(typeof(EnumOrderAttribute), false);
            return attributes.Length > 0 ? attributes[0].Order : -1 ;
        }
    }
}
