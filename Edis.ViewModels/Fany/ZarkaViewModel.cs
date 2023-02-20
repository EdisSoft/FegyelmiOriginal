using Edis.Entities.Fany;
using Edis.Utilities;
using Edis.ViewModels.Base;
using System.Collections.Generic;

namespace Edis.ViewModels.Fany
{
    public class ZarkaViewModel:BaseViewModel
    {
        #region mezők

        #endregion

        #region jellemzők
        public string Azonosito { get; set; }

        public double LegterM3 { get; set; }

        public int AgyDb { get; set; }

        public int BefogadoKepesseg { get; set; }

        public double AlapteruletM2 { get; set; }

        public bool Dohanyzo { get; set; }


        public int IntezetId { get; set; }


        public int IntezetiObjektumId { get; set; }

        public int KorletId { get; set; }


        public int? NevelesiCsoportId { get; set; }

        public int? NemeKszId { get; set; }

        //[KodszotarCsoport(Azonosito = "195")]
        //[KulsoKulcs(KesleltetettBetoltesuJellemzoNeve = "ZarkaJelleg")]
        public int ZarkaJellegKszId { get; set; }


        //[KodszotarCsoport(Azonosito = "117")]
        //[KulsoKulcs(KesleltetettBetoltesuJellemzoNeve = "ZarkaTipus")]
        public int ZarkaTipusKszId { get; set; }

        public int Letszam { get; set; } // Onnan töltjük ahonnan akarjuk, de a cast nem tölti :)

        //public int SzabadHelyekSzama
        //{
        //    get
        //    {
        //        int retval = AgyDb;
        //        if (Fogvatartottak.Any())
        //            retval = AgyDb - Fogvatartottak.Count;
        //        return retval;
        //    }
        //}

        //public int Letszam
        //{
        //    get { return Fogvatartottak.Count; }
        //}

        public virtual IntezetModel Intezet { get; set; }


        public virtual IntezetiObjektumViewModel IntezetiObjektum { get; set; }


        public virtual KorletViewModel Korlet { get; set; }



        //[KesleltetettBetoltesu(KulsoKulcsJellemzoNeve = "ZarkaJellegKszId")]
        public virtual KodszotarModel ZarkaJelleg { get; set; }

        //[KesleltetettBetoltesu(KulsoKulcsJellemzoNeve = "ZarkaTipusKszId")]
        public virtual KodszotarModel ZarkaTipus { get; set; }

        public virtual KodszotarModel Neme { get; set; }



        #endregion jellemzők

        public ZarkaViewModel()
        {
        }

        public static explicit operator ZarkaViewModel(Zarka item)
        {
            var model = ValueInjecterUtilities.InjectViewModel<Zarka, ZarkaViewModel>(item);
            //if (item.VegrehajtasiFokok != null)
            //{
            //    foreach (var vegrFokitem in item.VegrehajtasiFokok)
            //    {
            //        model.VegrehajtasiFokok.Add((ZarkaVegrFokModel)vegrFokitem);
            //    }
            //}
            if (item.IntezetiObjektum != null)
                model.IntezetiObjektum = (IntezetiObjektumViewModel)item.IntezetiObjektum;

            if (item.Korlet != null)
                model.Korlet = (KorletViewModel)item.Korlet;

            return model;
        }
    }
}
