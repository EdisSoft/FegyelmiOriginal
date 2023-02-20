using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edis.Entities.Fany;
using Edis.Utilities;

namespace Edis.ViewModels.Fany
{
    public class KorletViewModel
    {
        #region mezők
        #endregion

        #region jellemzők

        public int Id { get; set; }
        public string Azonosito { get; set; }

        public string Nev { get; set; }


        public int IntezetId { get; set; }


        public int IntezetiObjektumId { get; set; }


        //[KodszotarCsoport(Azonosito = "074")]
        //[KulsoKulcs(KesleltetettBetoltesuJellemzoNeve = "KorletTipus")]
        public int KorletTipusKszId { get; set; }


        public virtual Intezet Intezet { get; set; }


        public virtual IntezetiObjektum IntezetiObjektum { get; set; }

        //[KesleltetettBetoltesu(KulsoKulcsJellemzoNeve = "KorletTipusKszId")]
        public virtual Kodszotar KorletTipus { get; set; }


        //[KesleltetettBetoltesu(KulsoKulcsJellemzoNeve = "Id")]
        public virtual List<ZarkaViewModel> Zarkak { get; set; }
        
        public string AzonositoNevTipus
        {
            get { return string.Format("{0} - {1} - {2}", Azonosito, Nev, KorletTipus==null?"":KorletTipus.Nev); }
        }

        #endregion

        public KorletViewModel()
        {
            Zarkak = new List<ZarkaViewModel>();
        }

        public static explicit operator KorletViewModel(Korlet item)
        {
            KorletViewModel model = new KorletViewModel();
            model = ValueInjecterUtilities.InjectViewModel<Korlet, KorletViewModel>(item);

            return model;
        }
    }
}
