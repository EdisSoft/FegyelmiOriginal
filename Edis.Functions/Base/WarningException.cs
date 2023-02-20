using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Functions.Base
{
    public class WarningException : Exception
    {
        public WarningExceptionLevel WarningExceptionLevel { get; set; }
        public WarningException(string message, WarningExceptionLevel level = WarningExceptionLevel.Warning) : base(message)
        {
            WarningExceptionLevel = level;
        }

        public WarningExceptionLevel GetExceptionLevel()
        {
            return WarningExceptionLevel;
        }
    }

    public enum WarningExceptionLevel
    {
        Validation,
        Warning,
        Error
    }
}