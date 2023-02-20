namespace Edis.ViewModels.Base
{
    public class SocketResultModel : ResultModel
    {
        public const int SITE_BITNAPLO = 1;
        public const int SITE_KFFENAPLO = 2;
        public const int SITE_ESEMENYNAPLO = 3;
        

        public const int STATUS_SUCCESS = 1;
        public const int STATUS_INFO = 2;
        public const int STATUS_WARNING = 3;
        public const int STATUS_ERROR = 4;

        /// <summary>
        /// Csak a megjelölt sessionnak jelenik meg
        /// </summary>
        public bool OnlyMe { get; set; } = true;
        /// <summary>
        /// Melyik oldalon
        /// </summary>
        public int SiteId { get; set; }
        /// <summary>
        /// Melyik entitásnál
        /// </summary>
        public int EntityId { get; set; }
        /// <summary>
        /// Melyik sessionban jelenjen meg
        /// </summary>
        public string SessionID { get; set; }

        /// <summary>
        /// Ki indította
        /// </summary>
        public string User { get; set; }
    }
}
