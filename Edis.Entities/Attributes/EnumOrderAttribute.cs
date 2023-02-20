using System;

namespace Edis.Entities
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class EnumOrderAttribute : Attribute
    {
        public int Order { get; set; }
    }
}
