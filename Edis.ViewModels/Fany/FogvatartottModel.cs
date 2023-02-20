using Edis.Entities.Common;
using Edis.Entities.Fany;
using Edis.Utilities;
using Edis.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Edis.ViewModels.Fany
{
    public class FogvatartottModel
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
        public int? SzakkepzettsegKszId { get; set; }
        private string csaladiNev;
        public string CsaladiNev
        {
            get
            {
                return csaladiNev;
            }
            set
            {
                csaladiNev = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.ToLower());
            }
        }
        private string utonev;
        public string Utonev
        {
            get
            {
                return utonev;
            }
            set
            {
                utonev = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.ToLower());
            }
        }
        //public string SzuletesiCsaladiNev { get; set; }
        private string szuletesiCsaladiNev;
        public string SzuletesiCsaladiNev
        {
            get
            {
                return szuletesiCsaladiNev;
            }
            set
            {
                szuletesiCsaladiNev = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.ToLower());
            }
        }
        //public string SzuletesiUtonev { get; set; }
        private string szuletesiUtonev;
        public string SzuletesiUtonev
        {
            get
            {
                return szuletesiUtonev;
            }
            set
            {
                szuletesiUtonev = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.ToLower());
            }
        }
        public string SzuletesiHelyNeve { get; set; }

        public DateTime? SzuletesiDatum { get; set; }
        //public string AnyjaNeve { get; set; }
        private string anyjaNeve;
        public string AnyjaNeve
        {
            get
            {
                return anyjaNeve;
            }
            set
            {
                anyjaNeve = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.ToLower());
            }
        }
        public string TajSzam { get; set; }
        public int? AllandoLakcimIranyitoszam { get; set; }
        public string AllandoLakcimHelysegnev { get; set; }
        public string AllandoLakcimUtca { get; set; }
        public string AllandoLakcimHazszam { get; set; }
        public string Elhelyezes { get; set; }

        #region Foglalkoztatas
        public bool IsOktatott { get; set; }
        public bool IsJovobeliMunkaltatas { get; set; }
        public bool IsJovobeliNemDolgozas { get; set; }
        public int? BiztonsagiKockazatKszId { get; set; }
        public KodszotarModel BiztonsagiKockazat { get; set; }
        public bool IsAktualisMunkaltatasNapiMunkaidoElszamolas { get; set; }
        public int? AktualisMunkaltatasIntezetId { get; set; }


        public int ElojegyzettMunkaltatasId { get; set; }
        public int? ElojegyzettMunkaltatasIntezetId { get; set; }

        #endregion

        public FogvatartottSzemelyesAdataiViewModel FogvatartottSzemelyesAdatai { get; set; }
        public KodszotarModel NyilvantartasiStatusz { get; set; }
        public KodszotarModel NevelesiCsoport { get; set; }
        public KodszotarModel OrizetbeVetelModja { get; set; }
        public KodszotarModel Rezsim { get; set; }
        public KodszotarModel Szakkepzettseg { get; set; }
        public IntezetModel NyilvantartoIntezet { get; set; }
        public IntezetModel AktualisIntezet { get; set; }
        public IntezetiObjektumViewModel IntezetiObjektum { get; set; }
        public KorletViewModel Korlet { get; set; }
        public ZarkaViewModel Zarka { get; set; }
        public KodszotarModel VegrehajtasiFok { get; set; }
        public KodszotarModel FogvatartasJellege { get; set; }

        public bool IsKizarolagOktatott => IsNemDolgozo && IsOktatott;

        public bool IsKizarolagMunkaltatott => !IsNemDolgozo && !IsOktatott;

        public bool IsFrissenBefogadott => BefogadasDatuma >= DateTime.Now.AddDays(-15);

        public bool IsNemDolgozo { get; set; } = true;

        //public bool VanFoglalkoztatasElem { get; set; }

        public string FoglalkoztatasiArany { get; set; }

        public DateTime? FeltetelesSzabadulasDatuma { get; set; }
        public DateTime? KitoltveSzabadulasDatuma { get; set; }

        public static explicit operator FogvatartottModel(FogvatartottNezet item)
        {
            FogvatartottModel model = new FogvatartottModel();
            model = ValueInjecterUtilities.InjectViewModel<FogvatartottNezet, FogvatartottModel>(item);
            if (item.NyilvantartasiStatusz != null)
                model.NyilvantartasiStatusz = (KodszotarModel)item.NyilvantartasiStatusz;

            if (item.Rezsim != null)
                model.Rezsim = (KodszotarModel)item.Rezsim;

            if (item.Szakkepzettseg != null)
                model.Szakkepzettseg = (KodszotarModel)item.Szakkepzettseg;

            if (item.NyilvantartoIntezet != null)
                model.NyilvantartoIntezet = (IntezetModel)item.NyilvantartoIntezet;
            if (item.AktualisIntezet != null)
                model.AktualisIntezet = (IntezetModel)item.AktualisIntezet;
            if (item.IntezetiObjektum != null)
                model.IntezetiObjektum = (IntezetiObjektumViewModel)item.IntezetiObjektum;
            if (item.Korlet != null)
                model.Korlet = (KorletViewModel)item.Korlet;
            if (item.VegrehajtasiFok != null)
                model.VegrehajtasiFok = (KodszotarModel)item.VegrehajtasiFok;
            if (item.FogvatartasJellege != null)
                model.FogvatartasJellege = (KodszotarModel)item.FogvatartasJellege;
            if (item.BiztonsagiKockazat != null)
                model.BiztonsagiKockazat = (KodszotarModel)item.BiztonsagiKockazat;
            if (item.OrizetbeVetelModja != null)
                model.OrizetbeVetelModja = (KodszotarModel)item.OrizetbeVetelModja;
            // Ideiglenesen kikommentezve mert a nevelési csoport tábla használatakor elszállt.
            //if (item.Zarka != null)
            //    model.Zarka = (ZarkaViewModel)item.Zarka;
            //if (item.NevelesiCsoport != null)
            //    model.NevelesiCsoport = (KodszotarModel)item.NevelesiCsoport;
           
            return model;
        }
    }
}
