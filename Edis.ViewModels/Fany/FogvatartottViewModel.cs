using Edis.Entities.Fany;
using Edis.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.ViewModels.Fany
{
    public class FogvatartottViewModel
    {

        public int Id { get; set; }
        public string AktualisAzonosito { get; set; }

        public string NyilvantartasiAzonosito { get; set; }

        public string BefogadasIndoka { get; set; }

        public DateTime OrizetbevetelDatuma { get; set; }

        public DateTime ElsoBefogadasDatuma { get; set; }

        public DateTime BefogadasDatuma { get; set; }

        public DateTime? TavolletKezdete { get; set; }

        public DateTime? TavozasDatuma { get; set; }

        public DateTime? AktualisSzabadulasDatuma { get; set; }

        public int? ZarkaAgy { get; set; }

        public int NyilvantartoIntezetId { get; set; }

        public int ElsoBefogadoIntezetId { get; set; }

        public int AktualisIntezetId { get; set; }

        public int BefogadoSzervezetKszId { get; set; }

        public int OrizetbeVetelModjaKszId { get; set; }

        public int ElsoBefogadasModjaKszId { get; set; }

        public int BefogadasModjaKszId { get; set; }

        public int FogvatartasJellegeKszId { get; set; }

        public int VegrehajtasiFokKszId { get; set; }

        public int NyilvantartasiStatuszKszId { get; set; }

        public int? BuncselekmenyId { get; set; }

        public int VisszaesesFokaKszId { get; set; }

        public int? EnyhVegrehajtasKszId { get; set; }

        public int BiztonsagiCsoportKszId { get; set; }

        public int? NevelesiCsoportId { get; set; }

        public int? IntezetiObjektumId { get; set; }

        public int? KorletId { get; set; }

        public int? ZarkaId { get; set; }

        public int? SpecialisBehelyezesTipusaKszId { get; set; }

        public int? IteletId { get; set; }

        public int? ElozetesLetartoztatasId { get; set; }

        public int? ElzarasiHatarozatId { get; set; }

        public int? OrizetId { get; set; }

        public int? ZarkabaHelyezesId { get; set; }

        public int BefogadasId { get; set; }

        public int? OrizetbeVetelId { get; set; }

        public int? TavolletId { get; set; }

        public int? FogvFenykepId { get; set; }

        public int? SzabadSzovegId { get; set; }

        public bool? Vedett { get; set; }

        public int RezsimKszId { get; set; }

        public int FogvSzemelyId { get; set; }

        public FogvatartottSzemelyesAdataiViewModel FogvatartottSzemelyesAdatai { get; set; }


        public KodszotarModel NyilvantartasiStatusz { get; set; }
        public IntezetiObjektumViewModel IntezetiObjektum { get; set; }
        public KorletViewModel Korlet { get; set; }
        public ZarkaViewModel Zarka { get; set; }

        public IntezetModel AktualisIntezet { get; set; }

        public string Elhelyezes()
        {
            var szoveg = "";
            if (IntezetiObjektum != null)
                szoveg += IntezetiObjektum.Nev;
            else
                szoveg += "-";
            szoveg += "/";
            if (Korlet != null)
                szoveg += Korlet.Nev;
            else
                szoveg += "-";
            szoveg += "/";
            if (Zarka != null)
                szoveg += Zarka.Azonosito;
            else
                szoveg += "-";

            return szoveg;
        }

        public static explicit operator FogvatartottViewModel(Fogvatartott item)
        {
            FogvatartottViewModel model = new FogvatartottViewModel();
            model = ValueInjecterUtilities.InjectViewModel<Fogvatartott, FogvatartottViewModel>(item);
            if (item.FogvSzemAdatok.Count != 0)
                model.FogvatartottSzemelyesAdatai = (FogvatartottSzemelyesAdataiViewModel)item.FogvSzemAdatok.First();
            if (item.NyilvantartasiStatusz != null)
                model.NyilvantartasiStatusz = (KodszotarModel)item.NyilvantartasiStatusz;

            if (item.AktualisIntezet != null)
                model.AktualisIntezet = (IntezetModel)item.AktualisIntezet;

            if (item.IntezetiObjektum != null)
                model.IntezetiObjektum = (IntezetiObjektumViewModel)item.IntezetiObjektum;
            if (item.Korlet != null)
                model.Korlet = (KorletViewModel)item.Korlet;
            if (item.Zarka != null)
                model.Zarka = (ZarkaViewModel)item.Zarka;
           
            return model;
        }



        public static explicit operator Fogvatartott(FogvatartottViewModel model)
        {
            Fogvatartott entity = new Fogvatartott();
            entity = ValueInjecterUtilities.InjectViewModel<FogvatartottViewModel, Fogvatartott>(model);
           
            return entity;
        }

    }
}
