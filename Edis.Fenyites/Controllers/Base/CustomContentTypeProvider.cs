using Microsoft.Owin.StaticFiles.ContentTypes;

namespace Edis.Fenyites.Controllers.Base
{
    public class CustomContentTypeProvider : FileExtensionContentTypeProvider
    {
        public CustomContentTypeProvider()
        {
            Mappings.Add(".json", "application/json");
        }
    }

}