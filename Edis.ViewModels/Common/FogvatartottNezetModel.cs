using Edis.Utilities;
using Edis.ViewModels.Base;
using Edis.ViewModels.Fany;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace Edis.ViewModels.Common
{
    public class FogvatartottNezetModel : BaseViewModel
    {

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

        public bool Vedett { get; set; }

        public int RezsimKszId { get; set; }
        public string CsaladiNev { get; set; }
        public string Utonev { get; set; }
        public string SzuletesiCsaladiNev { get; set; }
        public string SzuletesiUtonev { get; set; }
        public string SzuletesiHelyNeve { get; set; }
        public DateTime? SzuletesiDatum { get; set; }
        public string AnyjaNeve { get; set; }
        public string TajSzam { get; set; }
        public int? AllandoLakcimIranyitoszam { get; set; }
        public string AllandoLakcimHelysegnev { get; set; }
        public string AllandoLakcimUtca { get; set; }
        public string AllandoLakcimHazszam { get; set; }
        public bool? IsDohanyzik { get; set; }


        public string SzuletesiNev
        {
            get
            {
                return SzuletesiCsaladiNev + " " + SzuletesiUtonev;
            }
        }
        public bool TOROLT_FL { get; set; }

        public FogvatartottSzemelyesAdataiViewModel FogvatartottSzemelyesAdatai { get; set; }


        public KodszotarModel NyilvantartasiStatusz { get; set; }

        public string NevelesiCsoport { get; set; }

        public IntezetModel NyilvantartoIntezet { get; set; }

        public IntezetModel AktualisIntezet { get; set; }

        public IntezetiObjektumViewModel IntezetiObjektum { get; set; }

        public KorletViewModel Korlet { get; set; }

        public ZarkaViewModel Zarka { get; set; }

        public KodszotarModel VegrehajtasiFok { get; set; }


        //public static explicit operator FogvatartottNezetModel(FogvatartottNezet item)
        //{
        //    FogvatartottNezetModel model = new FogvatartottNezetModel();
        //    model = ValueInjecterUtilities.InjectViewModel<FogvatartottNezet, FogvatartottNezetModel>(item);
        //    model.FogvatartottFenykepek = new List<FogvatartottFenykepModel>();
        //    if (item.FogvSzemAdatok.Count != 0)
        //        model.FogvatartottSzemelyesAdatai = (FogvatartottSzemelyesAdataiViewModel)item.FogvSzemAdatok.First();
        //    if (item.FogvatartottFenykep != null)
        //        model.FogvatartottFenykep = (FogvatartottFenykepModel)item.FogvatartottFenykep;
        //    if (model.FogvatartottFenykep != null)
        //        model.FogvatartottFenykepek.Add(model.FogvatartottFenykep);
        //    if (item.NyilvantartasiStatusz != null)
        //        model.NyilvantartasiStatusz = (KodszotarModel)item.NyilvantartasiStatusz;
        //    if (item.NyilvantartoIntezet != null)
        //        model.NyilvantartoIntezet = (IntezetModel)item.NyilvantartoIntezet;
        //    if (item.AktualisIntezet != null)
        //        model.AktualisIntezet = (IntezetModel)item.AktualisIntezet;
        //    if (item.IntezetiObjektum != null)
        //        model.IntezetiObjektum = (IntezetiObjektumViewModel)item.IntezetiObjektum;
        //    if (item.Korlet != null)
        //        model.Korlet = (KorletViewModel)item.Korlet;
        //    if (item.Zarka != null)
        //        model.Zarka = (ZarkaViewModel)item.Zarka;
        //    if (item.VegrehajtasiFok != null)
        //        model.VegrehajtasiFok = (KodszotarModel)item.VegrehajtasiFok;
        //    return model;
        //}

        //public static explicit operator FogvatartottNezet(FogvatartottNezetModel model)
        //{
        //    FogvatartottNezet entity = new FogvatartottNezet();
        //    entity = ValueInjecterUtilities.InjectViewModel<FogvatartottNezetModel, FogvatartottNezet>(model);

        //    return entity;
        //}

        //public static explicit operator FogvatartottNezetModel(FogvatartottModel item)
        //{
        //    FogvatartottNezetModel model = new FogvatartottNezetModel();
        //    model = ValueInjecterUtilities.InjectViewModel<FogvatartottModel, FogvatartottNezetModel>(item);
        //    return model;
        //}


    }
}
