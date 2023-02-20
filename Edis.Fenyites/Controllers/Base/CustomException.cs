using System;

namespace Edis.Fenyites.Controllers.Base
{
    public class CustomException:Exception
    {
        public CustomException(string message) : base(message)
        {

        }
    }
    
}