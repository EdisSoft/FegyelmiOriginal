using Edis.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Functions.Base
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ModulNameAttribute : Attribute
    {
        public ModulCimke ModulName { get; set; }
    }
}
