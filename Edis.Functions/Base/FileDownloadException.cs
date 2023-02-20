using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Functions.Base
{
    public class FileDownloadException : Exception
    {
        public string UserMessage { get; set; }
        public string DeveloperMessage { get; set; }

        public bool IsWriteToLog { get; set; }
        public FileDownloadException(string message,bool isWriteToLog=true) : base(message)
        {
            UserMessage = message;
            IsWriteToLog = isWriteToLog;
        }

        public FileDownloadException(string userMessage, string developerMessage, bool isWriteToLog = true) : base(userMessage+"; "+developerMessage)
        {
            UserMessage = userMessage;
            DeveloperMessage = developerMessage;
            IsWriteToLog = isWriteToLog;
        }
    }
}
