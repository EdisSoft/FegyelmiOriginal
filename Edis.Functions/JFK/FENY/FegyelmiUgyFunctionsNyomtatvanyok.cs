namespace Edis.Functions.JFK
{
    using Edis.Diagnostics;
    using Edis.Entities.Common;
    using Edis.Entities.Enums;
    using Edis.Entities.Enums.Cimke;
    using Edis.Entities.Enums.Fenyites;
    using Edis.Entities.Enums.Kodszotar;
    using Edis.Entities.Fany;
    using Edis.Entities.JFK.FENY;
    using Edis.Functions.Base;
    using Edis.Functions.Common;
    using Edis.Functions.Fany;
    using Edis.Functions.JFK.FENY;
    using Edis.IoC.Attributes;
    using Edis.Repositories.Contexts;
    using Edis.Utilities;
    using Edis.ViewModels;
    using Edis.ViewModels.Common;
    using Edis.ViewModels.Fany;
    using Edis.ViewModels.JFK;
    using Edis.ViewModels.JFK.Dokumentum;
    using Edis.ViewModels.JFK.FENY;
    using Edis.ViewModels.JFK.FENY.Email;
    using Edis.ViewModels.JFK.FENY.FormModel;
    using Edis.ViewModels.JFK.Nyomtatvany.Pdf;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Data.Entity;
    using System.Data.SqlClient;
    using System.Data.SqlTypes;
    using System.Diagnostics;
    using System.Linq;
    using System.Security.Principal;
    using static Edis.Entities.Enums.Cimke.CimkeEnums;
    using static Edis.Entities.Enums.Cimke.Fegyelmi.CimkeEnums;

    public partial class FegyelmiUgyFunctions : KonasoftBVFonixFunctionsBase<FegyelmiUgyViewModel, FegyelmiUgy>, IFegyelmiUgyFunctions
    {



        public FegyelmiLapViewModel GetFegyelmiLapNyomtatvanyByIktatasId(int iktatasId)
        {
            Log.Info("GetFegyelmiLapNyomtatvanyByIktatasId: " + iktatasId);
            KonasoftBVFonixContext.Configuration.LazyLoadingEnabled = false;
            IktatottDokumentumokViewModel iktatas;
            FegyelmiLapViewModel model;

            try
            {
                iktatas = (IktatottDokumentumokViewModel)IktatottDokumentumokFunctions.Table.FirstOrDefault(f => f.Id == iktatasId);
                model = IktatottDokumentumokFunctions.DeserializeIktatottNyomtatvanyAdat<FegyelmiLapViewModel>(iktatas.DokumentumAdat);

            }
            catch (Exception e)
            {

                Log.Error($"Hiba az iktatás betöltése során (iktatasId: {iktatasId})", e);
                throw;
            }

            return model;
        }

        public List<FegyelmiLapViewModel> GetFegyelmiLapNyomtatvanyokForEsemenyRogzites(List<int> fegyelmiUgyIds)
        {
            Log.Info("GetFegyelmiLapNyomtatvanyByIktatasId: " + string.Join(", ", fegyelmiUgyIds.ToArray()));
            KonasoftBVFonixContext.Configuration.LazyLoadingEnabled = false;
            int aktualisFegyelmiUgyId = 0;
            FegyelmiUgy fegyelmiUgy;
            FegyelmiLapViewModel model;
            IktatottDokumentumokViewModel iktatas;
            FogvatartottViewModel fogvatartott;
            List<FegyelmiLapViewModel> results = new List<FegyelmiLapViewModel>();

            foreach (var fegyelmiUgyId in fegyelmiUgyIds)
            {

                aktualisFegyelmiUgyId = fegyelmiUgyId;

                fegyelmiUgy = Table
                    .AsNoTracking()
                    .Include(x => x.Esemeny)
                    .Include(x => x.Esemeny.Eszlelo)
                    .Include(x => x.Esemeny.Eszlelo.Rendfokozat)
                    .Include(x => x.Intezet)
                    .Include(x => x.Intezet.CimHelyseg)
                    .SingleOrDefault(s => s.Id == fegyelmiUgyId);

                if (fegyelmiUgy == null)
                {
                    return null;
                }

                fogvatartott = FogvatartottFunctions.GetSzemelyesAdatokElhelyezessel(fegyelmiUgy.FogvatartottId);

                var iktatasEntity = IktatottDokumentumokFunctions.Table.FirstOrDefault(f => f.FegyelmiUgyId == fegyelmiUgyId && f.DokumentumTipusCimkeId == (int)IktatottDokumentumTipusok.FegyelmiLap);

                //using (DbContextTransaction transaction = KonasoftBVFonixContext.Database.BeginTransaction())
                //{
                try
                {

                    // ha nincs iktatás
                    if (iktatasEntity == null)
                    {
                        IktatottDokumentumokViewModel ujIktatas = new IktatottDokumentumokViewModel()
                        {
                            FegyelmiUgyId = fegyelmiUgyId,
                            Alszam = 1,
                            AktivFl = true,
                            DokumentumTipusCimkeId = (int)IktatottDokumentumTipusok.FegyelmiLap
                        };

                        IktatottDokumentumokFunctions.CreateNyomtatvany(ujIktatas);
                        iktatas = (IktatottDokumentumokViewModel)KonasoftBVFonixContext.IktatottDokumentumok.Single(x => x.Id == ujIktatas.Id);

                        model = new FegyelmiLapViewModel()
                        {
                            AktualisSzabadulasiIdo = fogvatartott.AktualisSzabadulasDatuma?.ToString("yyyy. MM. dd."),
                            AnyjaNeve = fogvatartott.FogvatartottSzemelyesAdatai.AnyjaNeve,
                            Azonosito = fogvatartott.NyilvantartasiAzonosito,
                            EsemenyEszlelese = fegyelmiUgy.Esemeny.EsemenyDatuma.ToString("yyyy. MM. dd. HH:mm"),
                            EsemenyLeirasa = fegyelmiUgy.Esemeny.Leiras,
                            EsemenySorszama = fegyelmiUgy.Esemeny.Id,
                            EsemenytEszlelte = $"{fegyelmiUgy.Esemeny.Eszlelo.Nev}{(fegyelmiUgy.Esemeny.Eszlelo.RendfokozatKszId == 0 ? "" : " " + fegyelmiUgy.Esemeny.Eszlelo.Rendfokozat.Nev)}",
                            IntezetNev = fegyelmiUgy.Intezet.Nev,
                            FogvElhelyezese = fogvatartott.IntezetiObjektum != null ? $"{fogvatartott.IntezetiObjektum.Nev}/{fogvatartott.Korlet.Nev}/{fogvatartott.Zarka.Azonosito}" : "",
                            Nev = $"{fogvatartott.FogvatartottSzemelyesAdatai.SzuletesiCsaladiNev} {fogvatartott.FogvatartottSzemelyesAdatai.SzuletesiUtonev}",
                            SzuletesiHely = fogvatartott.FogvatartottSzemelyesAdatai.SzuletesiHelyNeve,
                            SzuletesiIdo = fogvatartott.FogvatartottSzemelyesAdatai.SzuletesiDatum.ToString("yyyy. MM. dd."),
                            TartozkodasFokaJogcime = fogvatartott.VegrehajtasiFokKszId != 0 ? KodszotarFunctions.FindById(fogvatartott.VegrehajtasiFokKszId).Nev : "",
                            UgySzama = $"{fegyelmiUgy.UgySorszamaIntezetAzon}/{fegyelmiUgy.UgySorszamaEv}/{fegyelmiUgy.UgySorszama}",
                            Telepules = fegyelmiUgy.Intezet.CimHelyseg.Nev,
                            Iktatoszam = $"{fegyelmiUgy.UgySorszamaIntezetAzon}/{fegyelmiUgy.UgySorszamaEv}/{fegyelmiUgy.UgySorszama}/{ujIktatas.Alszam}",
                        };

                        iktatas.DokumentumAdat = IktatottDokumentumokFunctions.SerializeIktatottNyomtatvanyAdat(model);
                        IktatottDokumentumokFunctions.Modify(iktatas);
                        results.Add(model);
                        KonasoftBVFonixContext.SaveChanges();

                    }
                    else
                    {
                        iktatas = (IktatottDokumentumokViewModel)iktatasEntity;
                        results.Add(IktatottDokumentumokFunctions.DeserializeIktatottNyomtatvanyAdat<FegyelmiLapViewModel>(iktatas.DokumentumAdat));
                    }

                    //transaction.Commit();

                }
                catch (Exception ex)
                {
                    Log.Error($"Hiba a fegyelmi lap iktatása során (naplóId: {aktualisFegyelmiUgyId})", ex);
                    //transaction.Rollback();
                    throw;
                }
                //}
            }
            return results;
        }

        public TajekoztatoViewModel GetTajekoztatoById(int fegyelmiUgyId, bool kerem, int? iktatasId = null)
        {
            KonasoftBVFonixContext.Configuration.LazyLoadingEnabled = false;
            FegyelmiUgy fegyelmiUgy;
            IktatottDokumentumok iktatas;
            TajekoztatoViewModel model;

            int dokumentumTipusCimkeId = kerem ? (int)IktatottDokumentumTipusok.TajekozatasKer : (int)IktatottDokumentumTipusok.TajekoztatasNemKer;

            if (iktatasId != null)
                iktatas = IktatottDokumentumokFunctions.Table.FirstOrDefault(f => f.Id == iktatasId.Value);
            else
                iktatas = IktatottDokumentumokFunctions.Table.FirstOrDefault(f => f.FegyelmiUgyId == fegyelmiUgyId && f.DokumentumTipusCimkeId == dokumentumTipusCimkeId);


            if (iktatas == null)
            {
                fegyelmiUgy = Table
                .AsNoTracking()
                .Include(x => x.Esemeny)
                .Include(x => x.Fogvatartott)
                .Include(x => x.Esemeny.Eszlelo)
                .Include(x => x.Intezet)
                .Include(x => x.Intezet.CimHelyseg)
                .SingleOrDefault(s => s.Id == fegyelmiUgyId);

                if (fegyelmiUgy == null)
                {
                    return null;
                }

                IktatottDokumentumokViewModel ujIktatas = new IktatottDokumentumokViewModel()
                {
                    FegyelmiUgyId = fegyelmiUgyId,
                    Alszam = 1,
                    AktivFl = true,
                    DokumentumTipusCimkeId = dokumentumTipusCimkeId
                };

                IktatottDokumentumokFunctions.CreateNyomtatvany(ujIktatas);

                ujIktatas = (IktatottDokumentumokViewModel)KonasoftBVFonixContext.IktatottDokumentumok.Single(x => x.Id == ujIktatas.Id);

                model = new TajekoztatoViewModel()
                {
                    Azonosito = fegyelmiUgy.Fogvatartott.NyilvantartasiAzonosito,
                    IntezetNev = fegyelmiUgy.Intezet.Nev,
                    Nev = $"{fegyelmiUgy.Fogvatartott.SzuletesiCsaladiNev} {fegyelmiUgy.Fogvatartott.SzuletesiUtonev}",
                    SzuletesiIdo = fegyelmiUgy.Fogvatartott.SzuletesiDatum.ToString("yyyy. MM. dd."),
                    UgySzama = $"{fegyelmiUgy.UgySorszamaIntezetAzon}/{fegyelmiUgy.UgySorszamaEv}/{fegyelmiUgy.UgySorszama}",
                    Telepules = fegyelmiUgy.Intezet.CimHelyseg.Nev,
                    Kerem = kerem,
                    Iktatoszam = $"{fegyelmiUgy.UgySorszamaIntezetAzon}/{fegyelmiUgy.UgySorszamaEv}/{fegyelmiUgy.UgySorszama}/{ujIktatas.Alszam}"
                };

                ujIktatas.DokumentumAdat = IktatottDokumentumokFunctions.SerializeIktatottNyomtatvanyAdat(model);
                IktatottDokumentumokFunctions.Modify(ujIktatas);
            }
            else
            {
                model = IktatottDokumentumokFunctions.DeserializeIktatottNyomtatvanyAdat<TajekoztatoViewModel>(iktatas.DokumentumAdat);
            }

            return model;
        }

        public MeghallgatasiJegyzokonyv EljarasAlaVontMeghallgatasDokumentumAdatok(int naploId)
        {

            MeghallgatasiJegyzokonyv model;
            int? iktatasId = null;

            var naplo = KonasoftBVFonixContext.Naplo
                        .Include(x => x.JegyzokonyvVezetoSzemely)
                        .Include(x => x.JegyzokonyvVezetoSzemely.Rendfokozat)
                        .Include(x => x.MeghallgatoSzemely)
                        .Include(x => x.MeghallgatoSzemely.Rendfokozat)
                        .SingleOrDefault(x => x.Id == naploId);
            var elozoIktatas = KonasoftBVFonixContext.IktatottDokumentumok.SingleOrDefault(x => x.NaploId == naploId);

            if (elozoIktatas != null)
            {
                iktatasId = elozoIktatas.Id;
            }

            //if (iktatasId == null || naplo.AlairtaFl == false)
            //{
            IktatottDokumentumokViewModel iktatas = new IktatottDokumentumokViewModel()
            {
                FegyelmiUgyId = naplo.FegyelmiUgyId,
                Alszam = 1,
                AktivFl = true,
                DokumentumTipusCimkeId = (int)CimkeEnums.IktatottDokumentumTipusok.EljarasAlaVontMeghallgatasa
            };

            if (iktatasId == null)
                IktatottDokumentumokFunctions.CreateNyomtatvany(iktatas);
            else
                iktatas = (IktatottDokumentumokViewModel)elozoIktatas;

            iktatas = (IktatottDokumentumokViewModel)KonasoftBVFonixContext.IktatottDokumentumok.Single(x => x.Id == iktatas.Id);

            var fegyelmiUgy = KonasoftBVFonixContext.FegyelmiUgyek.Single(x => x.Id == naplo.FegyelmiUgyId);
            var esemeny = KonasoftBVFonixContext.Esemenyek.Single(x => x.Id == fegyelmiUgy.EsemenyId);
            var intezet = KonasoftBVFonixContext.Intezet.Single(x => x.Id == fegyelmiUgy.IntezetId);
            var helyseg = KonasoftBVFonixContext.Helyseg.Single(x => x.Id == intezet.CimHelysegId);
            var fogvSzemAdat = KonasoftBVFonixContext.FogvatartottSzemelyesAdatai.Include(x => x.Fogvatartott).Single(x => x.FogvatartottId == fegyelmiUgy.FogvatartottId);
            var fogvatartott = fogvSzemAdat.Fogvatartott;

            var datum = DateTime.Now;

                model = new MeghallgatasiJegyzokonyv()
                {
                    IntezetNev = intezet.Nev,
                    Iktatoszam = fegyelmiUgy.UgySorszamaIntezetAzon + "/" + fegyelmiUgy.UgySorszamaEv + "/" + fegyelmiUgy.UgySorszama + "/" + iktatas.Alszam.ToString(),
                    UgySzam = fegyelmiUgy.UgySorszamaIntezetAzon + "/" + fegyelmiUgy.UgySorszamaEv + "/" + fegyelmiUgy.UgySorszama,
                    MeghallgatottNev = fogvSzemAdat.SzuletesiCsaladiNev + " " + fogvSzemAdat.SzuletesiUtonev,
                    MeghallgatottAzon = fogvatartott.NyilvantartasiAzonosito,
                    MeghallgatottSzulIdeje = fogvSzemAdat.SzuletesiDatum.ToShortDateString(),
                    MeghallgatottSzulHelye = fogvSzemAdat.SzuletesiHelyNeve,
                    MeghallgatottAnyjaNeve = fogvSzemAdat.AnyjaNeve,
                    //Elfogult = "Nem",
                    EgyebJelenlevo = naplo.TovabbiJelenlevok,
                    JegyzoKonyvText = naplo.JegyzokonyvTartalma,
                    JegyzoKonyvZarasOra = datum.Hour.ToString(),
                    JegyzoKonyvZarasPerc = datum.Minute.ToString(),
                    JegyzoNev = naplo.JegyzokonyvVezetoSzemely?.Nev,
                    JegyzoRang = naplo.JegyzokonyvVezetoSzemely?.RendfokozatKszId == 0 ? "" : naplo.JegyzokonyvVezetoSzemely?.Rendfokozat?.Nev,
                    MeghallgatasEve = naplo.LetrehozasDatuma.Year.ToString(),
                    MeghallgatasHava = naplo.LetrehozasDatuma.Month.ToString(),
                    MeghallgatasNapja = naplo.LetrehozasDatuma.Day.ToString(),
                    //MeghallgatasOraja = naplo.LetrehozasDatuma.Hour.ToString(),
                    //MeghallgatasPerce = naplo.LetrehozasDatuma.Minute.ToString(),
                    MeghallgatoNev = naplo.MeghallgatoSzemely.Nev,
                    MeghallgatoRang = naplo.MeghallgatoSzemely?.RendfokozatKszId == 0 ? "" : naplo.MeghallgatoSzemely.Rendfokozat.Nev,
                };

                iktatas.DokumentumAdat = IktatottDokumentumokFunctions.SerializeIktatottNyomtatvanyAdat(model);
                iktatas.NaploId = naploId;
                IktatottDokumentumokFunctions.Modify(iktatas);

            return model;
        }

        public MeghallgatasiJegyzokonyv EljarasAlaVontMeghallgatasDokumentumAdatokByIktatasId(int iktatasId)
        {
            var iktatas = KonasoftBVFonixContext.IktatottDokumentumok.Single(x => x.Id == iktatasId);
            var model = IktatottDokumentumokFunctions.DeserializeIktatottNyomtatvanyAdat<MeghallgatasiJegyzokonyv>(iktatas.DokumentumAdat);

            return model;
        }

        public MeghallgatasiJegyzokonyv TanuMeghallgatasDokumentumAdatok(int naploId)
        {

            MeghallgatasiJegyzokonyv model;
            int? iktatasId = null;

            var naplo = KonasoftBVFonixContext.Naplo
                        .Include(x => x.JegyzokonyvVezetoSzemely)
                        .Include(x => x.JegyzokonyvVezetoSzemely.Rendfokozat)
                        .Include(x => x.MeghallgatoSzemely)
                        .Include(x => x.MeghallgatoSzemely.Rendfokozat)
                        .SingleOrDefault(x => x.Id == naploId);
            var elozoIktatas = KonasoftBVFonixContext.IktatottDokumentumok.SingleOrDefault(x => x.NaploId == naploId);

            if (elozoIktatas != null)
            {
                iktatasId = elozoIktatas.Id;
            }


            IktatottDokumentumokViewModel iktatas = new IktatottDokumentumokViewModel()
            {
                FegyelmiUgyId = naplo.FegyelmiUgyId,
                Alszam = 1,
                AktivFl = true,
                DokumentumTipusCimkeId = (int)CimkeEnums.IktatottDokumentumTipusok.TanuMeghallgatasa
            };

            if (iktatasId == null)
                IktatottDokumentumokFunctions.CreateNyomtatvany(iktatas);
            else
                iktatas = (IktatottDokumentumokViewModel)elozoIktatas;

            iktatas = (IktatottDokumentumokViewModel)KonasoftBVFonixContext.IktatottDokumentumok.Single(x => x.Id == iktatas.Id);

            var fegyelmiUgy = KonasoftBVFonixContext.FegyelmiUgyek.Single(x => x.Id == naplo.FegyelmiUgyId);
            //var esemeny = KonasoftBVFonixContext.Esemenyek.Single(x => x.Id == fegyelmiUgy.EsemenyId);
            var intezet = KonasoftBVFonixContext.Intezet.Single(x => x.Id == fegyelmiUgy.IntezetId);
            //var helyseg = KonasoftBVFonixContext.Helyseg.Single(x => x.Id == intezet.CimHelysegId);
            var fogvSzemAdat = KonasoftBVFonixContext.FogvatartottSzemelyesAdatai.Include(x => x.Fogvatartott).Single(x => x.FogvatartottId == naplo.FogvatartottId);
            var fogvatartott = fogvSzemAdat.Fogvatartott;
            int? esemenyResztvevoId = null;
            if (naplo.InkognitoFl == true)
                esemenyResztvevoId = KonasoftBVFonixContext.EsemenyResztvevok.Single(x => x.FogvatartottId == naplo.FogvatartottId && x.EsemenyId == fegyelmiUgy.EsemenyId)?.Id;

            var datum = DateTime.Now;

                model = new MeghallgatasiJegyzokonyv()
                {
                    IntezetNev = intezet.Nev,
                    Iktatoszam = fegyelmiUgy.UgySorszamaIntezetAzon + "/" + fegyelmiUgy.UgySorszamaEv + "/" + fegyelmiUgy.UgySorszama + "/" + iktatas.Alszam.ToString(),
                    UgySzam = fegyelmiUgy.UgySorszamaIntezetAzon + "/" + fegyelmiUgy.UgySorszamaEv + "/" + fegyelmiUgy.UgySorszama,
                    MeghallgatottNev = naplo.InkognitoFl == true ? $"Tanú {esemenyResztvevoId.Value}" : fogvSzemAdat.SzuletesiCsaladiNev + " " + fogvSzemAdat.SzuletesiUtonev,
                    MeghallgatottAzon = naplo.InkognitoFl == true ? "" : fogvatartott.NyilvantartasiAzonosito,
                    MeghallgatottSzulIdeje = naplo.InkognitoFl == true ? "-" : fogvSzemAdat.SzuletesiDatum.ToShortDateString(),
                    MeghallgatottSzulHelye = naplo.InkognitoFl == true ? "-" : fogvSzemAdat.SzuletesiHelyNeve,
                    MeghallgatottAnyjaNeve = naplo.InkognitoFl == true ? "-" : fogvSzemAdat.AnyjaNeve,
                    EgyebJelenlevo = naplo.TovabbiJelenlevok,
                    JegyzoKonyvText = naplo.JegyzokonyvTartalma,
                    JegyzoKonyvZarasOra = datum.Hour.ToString(),
                    JegyzoKonyvZarasPerc = datum.Minute.ToString(),
                    JegyzoNev = naplo.JegyzokonyvVezetoSzemely?.Nev,
                    JegyzoRang = naplo.JegyzokonyvVezetoSzemely?.RendfokozatKszId == 0 ? "" : naplo.JegyzokonyvVezetoSzemely?.Rendfokozat?.Nev,
                    MeghallgatasEve = naplo.LetrehozasDatuma.Year.ToString(),
                    MeghallgatasHava = naplo.LetrehozasDatuma.Month.ToString(),
                    MeghallgatasNapja = naplo.LetrehozasDatuma.Day.ToString(),
                    //MeghallgatasOraja = naplo.LetrehozasDatuma.Hour.ToString(),
                    //MeghallgatasPerce = naplo.LetrehozasDatuma.Minute.ToString(),
                    MeghallgatoNev = naplo.MeghallgatoSzemely.Nev,
                    MeghallgatoRang = naplo.MeghallgatoSzemely?.RendfokozatKszId == 0 ? "" : naplo.MeghallgatoSzemely.Rendfokozat.Nev
                };

                iktatas.DokumentumAdat = IktatottDokumentumokFunctions.SerializeIktatottNyomtatvanyAdat(model);
                iktatas.NaploId = naploId;
                IktatottDokumentumokFunctions.Modify(iktatas);
        
            return model;
        }

        public MeghallgatasiJegyzokonyv TanuMeghallgatasDokumentumAdatokByIktatasId(int iktatasId)
        {
            var iktatas = KonasoftBVFonixContext.IktatottDokumentumok.Single(x => x.Id == iktatasId);
            var model = IktatottDokumentumokFunctions.DeserializeIktatottNyomtatvanyAdat<MeghallgatasiJegyzokonyv>(iktatas.DokumentumAdat);
            return model;
        }

        public MeghallgatasiJegyzokonyv SzemelyiAllomanyiTanuMeghallgatasDokumentumAdatok(int naploId)
        {

            MeghallgatasiJegyzokonyv model;
            int? iktatasId = null;

            var naplo = KonasoftBVFonixContext.Naplo
                        .Include(x => x.SzemelyiAllomanyiTanuSzemely)
                        .Include(x => x.SzemelyiAllomanyiTanuSzemely.Rendfokozat)
                        .Include(x => x.MeghallgatoSzemely)
                        .Include(x => x.MeghallgatoSzemely.Rendfokozat)
                        .SingleOrDefault(x => x.Id == naploId);
            var elozoIktatas = KonasoftBVFonixContext.IktatottDokumentumok.SingleOrDefault(x => x.NaploId == naploId);

            if (elozoIktatas != null)
            {
                iktatasId = elozoIktatas.Id;
            }

            IktatottDokumentumokViewModel iktatas = new IktatottDokumentumokViewModel()
            {
                FegyelmiUgyId = naplo.FegyelmiUgyId,
                Alszam = 1,
                AktivFl = true,
                DokumentumTipusCimkeId = (int)CimkeEnums.IktatottDokumentumTipusok.SzemelyiAllomanyiTanuMeghallgatasa
            };

            if (iktatasId == null)
                IktatottDokumentumokFunctions.CreateNyomtatvany(iktatas);
            else
                iktatas = (IktatottDokumentumokViewModel)elozoIktatas;

            iktatas = (IktatottDokumentumokViewModel)KonasoftBVFonixContext.IktatottDokumentumok.Single(x => x.Id == iktatas.Id);

            var fegyelmiUgy = KonasoftBVFonixContext.FegyelmiUgyek.Single(x => x.Id == naplo.FegyelmiUgyId);
            //var esemeny = KonasoftBVFonixContext.Esemenyek.Single(x => x.Id == fegyelmiUgy.EsemenyId);
            var intezet = KonasoftBVFonixContext.Intezet.Single(x => x.Id == fegyelmiUgy.IntezetId);
            //var helyseg = KonasoftBVFonixContext.Helyseg.Single(x => x.Id == intezet.CimHelysegId);

            var datum = DateTime.Now;

                model = new MeghallgatasiJegyzokonyv()
                {
                    IntezetNev = intezet.Nev,
                    Iktatoszam = fegyelmiUgy.UgySorszamaIntezetAzon + "/" + fegyelmiUgy.UgySorszamaEv + "/" + fegyelmiUgy.UgySorszama + "/" + iktatas.Alszam.ToString(),
                    UgySzam = fegyelmiUgy.UgySorszamaIntezetAzon + "/" + fegyelmiUgy.UgySorszamaEv + "/" + fegyelmiUgy.UgySorszama,
                    MeghallgatottNev = naplo.SzemelyiAllomanyiTanuSzemely.Azonosito,
                    MeghallgatottRang = "",//naplo.SzemelyiAllomanyiTanuSzemely.Rendfokozat.Nev,
                                           //Elfogult = "Nem",
                    EgyebJelenlevo = naplo.TovabbiJelenlevok,
                    JegyzoKonyvText = naplo.JegyzokonyvTartalma,
                    JegyzoKonyvZarasOra = datum.Hour.ToString(),
                    JegyzoKonyvZarasPerc = datum.Minute.ToString(),
                    JegyzoNev = naplo.JegyzokonyvVezetoSzemely?.Nev,
                    JegyzoRang = naplo.JegyzokonyvVezetoSzemely?.RendfokozatKszId == 0 ? "" : naplo.JegyzokonyvVezetoSzemely?.Rendfokozat?.Nev,
                    MeghallgatasEve = naplo.LetrehozasDatuma.Year.ToString(),
                    MeghallgatasHava = naplo.LetrehozasDatuma.Month.ToString(),
                    MeghallgatasNapja = naplo.LetrehozasDatuma.Day.ToString(),
                    //MeghallgatasOraja = naplo.LetrehozasDatuma.Hour.ToString(),
                    //MeghallgatasPerce = naplo.LetrehozasDatuma.Minute.ToString(),
                    MeghallgatoNev = naplo.MeghallgatoSzemely.Nev,
                    MeghallgatoRang = naplo.MeghallgatoSzemely?.RendfokozatKszId == 0 ? "" : naplo.MeghallgatoSzemely.Rendfokozat.Nev
                };

                iktatas.DokumentumAdat = IktatottDokumentumokFunctions.SerializeIktatottNyomtatvanyAdat(model);
                iktatas.NaploId = naploId;
                IktatottDokumentumokFunctions.Modify(iktatas);

            return model;
        }

        public MeghallgatasiJegyzokonyv SzemelyiAllomanyiTanuMeghallgatasDokumentumAdatokByIktatasId(int iktatasId)
        {
            var iktatas = KonasoftBVFonixContext.IktatottDokumentumok.Single(x => x.Id == iktatasId);
            var model = IktatottDokumentumokFunctions.DeserializeIktatottNyomtatvanyAdat<MeghallgatasiJegyzokonyv>(iktatas.DokumentumAdat);
            return model;
        }

        public TargyalasiJegyzokonyv ElsoFokuTargyalasiDokumentumAdatok(int naploId)
        {
            IktatottDokumentumokViewModel iktatas;
            TargyalasiJegyzokonyv model;
            int? iktatasId = null;

            KonasoftBVFonixContext.DisableToroltFlFilter(x => x.Intezet);

            var naplo = KonasoftBVFonixContext.Naplo
                        .Include(x => x.JegyzokonyvVezetoSzemely)
                        .Include(x => x.JegyzokonyvVezetoSzemely.Rendfokozat)
                        .Include(x => x.DonteshozoSzemely)
                        .Include(x => x.DonteshozoSzemely.Rendfokozat)
                        .SingleOrDefault(x => x.Id == naploId);
            var elozoIktatas = KonasoftBVFonixContext.IktatottDokumentumok.SingleOrDefault(x => x.NaploId == naploId);

            if (elozoIktatas != null)
            {
                iktatasId = elozoIktatas.Id;
            }
            else
            {
                iktatas = new IktatottDokumentumokViewModel()
                {
                    FegyelmiUgyId = naplo.FegyelmiUgyId,
                    Alszam = 1,
                    AktivFl = true,
                    DokumentumTipusCimkeId = (int)CimkeEnums.IktatottDokumentumTipusok.TargyalasiJegyzokonyv
                };
                IktatottDokumentumokFunctions.CreateNyomtatvany(iktatas);
                iktatasId = iktatas.Id;
            }

            iktatas = (IktatottDokumentumokViewModel)KonasoftBVFonixContext.IktatottDokumentumok.Single(x => x.Id == iktatasId);

            var fegyelmiUgy = KonasoftBVFonixContext.FegyelmiUgyek.Single(x => x.Id == naplo.FegyelmiUgyId);
            var esemeny = KonasoftBVFonixContext.Esemenyek.Single(x => x.Id == fegyelmiUgy.EsemenyId);
            var intezet = KonasoftBVFonixContext.Intezet.Single(x => x.Id == fegyelmiUgy.IntezetId);
            var helyseg = KonasoftBVFonixContext.Helyseg.Single(x => x.Id == intezet.CimHelysegId);
            var fogvSzemAdat = KonasoftBVFonixContext.FogvatartottSzemelyesAdatai.Include(x => x.Fogvatartott).Single(x => x.FogvatartottId == fegyelmiUgy.FogvatartottId);
            var fogvatartott = fogvSzemAdat.Fogvatartott;

            var datum = DateTime.Now;

            model = new TargyalasiJegyzokonyv()
            {
                IntezetNev = intezet.Nev,
                Iktatoszam = fegyelmiUgy.UgySorszamaIntezetAzon + "/" + fegyelmiUgy.UgySorszamaEv + "/" + fegyelmiUgy.UgySorszama + "/" + iktatas.Alszam.ToString(),
                UgySzam = fegyelmiUgy.UgySorszamaIntezetAzon + "/" + fegyelmiUgy.UgySorszamaEv + "/" + fegyelmiUgy.UgySorszama,
                EljarasAlaVontNev = fogvSzemAdat.SzuletesiCsaladiNev + " " + fogvSzemAdat.SzuletesiUtonev,
                EljarasAlaVontAzon = fogvatartott.NyilvantartasiAzonosito,
                EljarasAlaVontSzulIdeje = fogvSzemAdat.SzuletesiDatum.ToShortDateString(),
                EljarasAlaVontSzulHelye = fogvSzemAdat.SzuletesiHelyNeve,
                EljarasAlaVontAnyjaNeve = fogvSzemAdat.AnyjaNeve,
                EgyebJelenlevo = naplo.TovabbiJelenlevok ?? "",
                JegyzoKonyvText = naplo.JegyzokonyvTartalma,
                JegyzoKonyvZarasOra = datum.Hour.ToString(),
                JegyzoKonyvZarasPerc = datum.Minute.ToString(),
                JegyzoNev = naplo.JegyzokonyvVezetoSzemely?.Nev,
                JegyzoRang = naplo.JegyzokonyvVezetoSzemely?.RendfokozatKszId == 0 ? "" : naplo.JegyzokonyvVezetoSzemely?.Rendfokozat?.Nev,
                MeghallgatasEve = naplo.LetrehozasDatuma.Year.ToString(),
                MeghallgatasHava = naplo.LetrehozasDatuma.Month.ToString(),
                MeghallgatasNapja = naplo.LetrehozasDatuma.Day.ToString(),
                FegyelmiJogkorGyakorloNev = naplo.DonteshozoSzemely?.Nev,
                FegyelmiJogkorGyakorloRang = naplo.DonteshozoSzemely?.RendfokozatKszId == 0 ? "" : naplo.DonteshozoSzemely?.Rendfokozat?.Nev
            };

            iktatas.DokumentumAdat = IktatottDokumentumokFunctions.SerializeIktatottNyomtatvanyAdat(model);
            iktatas.NaploId = naploId;
            IktatottDokumentumokFunctions.Modify(iktatas);

            KonasoftBVFonixContext.EnableToroltFlFilter(x => x.Intezet);

            return model;
        }

        public TargyalasiJegyzokonyv ElsoFokuTargyalasiDokumentumAdatokByIktatasId(int iktatasId)
        {
            var iktatas = KonasoftBVFonixContext.IktatottDokumentumok.Single(x => x.Id == iktatasId);
            var model = IktatottDokumentumokFunctions.DeserializeIktatottNyomtatvanyAdat<TargyalasiJegyzokonyv>(iktatas.DokumentumAdat);
            return model;
        }

        public MasodfokuFegyelmiHatarozatViewModel GetMasodfokuArchivHatarozat(int fegyelmiUgyId)
        {
            KonasoftBVFonixContext.Configuration.LazyLoadingEnabled = false;
            MasodfokuFegyelmiHatarozatViewModel model;

            //var f3FegyelmiUgy = new KonasoftBVFonix3Context().FanyUgyek.AsNoTracking().FirstOrDefault(f => f.Id == Math.Abs(fegyelmiUgyId));

            //if (f3FegyelmiUgy.EljarasFoka == null || f3FegyelmiUgy.EljarasFoka < 2)
            //{
            //    Log.Error($"Nincs elsőfokú határozat a fegyelmi ügyhöz: {fegyelmiUgyId}.");
            //    throw new WarningException("Nincs másodfokú határozat a fegyelmi ügyhöz.");
            //}

            //var fogvatartott = FogvatartottFunctions.GetSzemelyesAdatokElhelyezessel(f3FegyelmiUgy.FogvatartottFanyId.Value);
            //KonasoftBVFonixContext.DisableToroltFlFilter(f => f.Intezet);
            //var intezet = KonasoftBVFonixContext.Intezet.AsNoTracking().Include(i => i.CimHelyseg).FirstOrDefault(f => f.Id == f3FegyelmiUgy.IntezetFanyId);
            //KonasoftBVFonixContext.EnableToroltFlFilter(f => f.Intezet);
            //Barat hatarozatHozo = null;
            //if (f3FegyelmiUgy.MasodfokuHatBarat != null)
            //    hatarozatHozo = new KonasoftBVFonix3Context().Baratok.AsNoTracking().FirstOrDefault(f => f.ID == f3FegyelmiUgy.MasodfokuHatBaratJson.Id);

            model = new MasodfokuFegyelmiHatarozatViewModel()
            {
                FegyelmiUgyId = fegyelmiUgyId,
                //IntezetNev = f3FegyelmiUgy.IntezetJson?.Nev ?? "-",
                //Iktatoszam = $"{f3FegyelmiUgy.IntezetFanyAzon}/{f3FegyelmiUgy.UgyEv}/{f3FegyelmiUgy.UgySorszam}",
                //FegyelmiUgy = $"{f3FegyelmiUgy.IntezetFanyAzon}/{f3FegyelmiUgy.UgyEv}/{f3FegyelmiUgy.UgySorszam}",
                //MeghallgatasEve = naplo.ElsofokuTargyalasIdopontja?.Year,
                //MeghallgatasHava = naplo.ElsofokuTargyalasIdopontja?.Month,
                //MeghallgatasNapja = naplo.ElsofokuTargyalasIdopontja?.Day,
                //MeghallgatasIdeje = naplo.ElsofokuTargyalasIdopontja?.ToShortTimeString(),
                //Fogvatartott = $"{fogvatartott.FogvatartottSzemelyesAdatai.SzuletesiCsaladiNev} {fogvatartott.FogvatartottSzemelyesAdatai.SzuletesiUtonev}",
                //FogvatartottSzulIdeje = fogvatartott.FogvatartottSzemelyesAdatai.SzuletesiDatum.ToString("yyyy. MM. dd."),
                //FogvatartottSzulHelye = fogvatartott.FogvatartottSzemelyesAdatai.SzuletesiHelyNeve,
                //FogvatartottAnyjaNeve = fogvatartott.FogvatartottSzemelyesAdatai.AnyjaNeve,
                //Elhelyezes = fogvatartott.IntezetiObjektum != null ? $"{fogvatartott.IntezetiObjektum?.Nev ?? "-"}/{fogvatartott.Korlet?.Nev ?? "-"}/{fogvatartott.Zarka?.Azonosito ?? "-"}" : "",
                //FogvSzabadul = fogvatartott.AktualisSzabadulasDatuma?.ToString("yyyy. MM. dd.") ?? "-",
                //FogvAzon = fogvatartott.NyilvantartasiAzonosito,
                //BvFok = fogvatartott.FogvatartasJellegeKszId != 0 ? KodszotarFunctions.FindById(fogvatartott.FogvatartasJellegeKszId).Nev : "",
                //VegrehajtasiFok = fogvatartott.VegrehajtasiFokKszId != 0 ? KodszotarFunctions.FindById(fogvatartott.VegrehajtasiFokKszId).Nev : "",
                //FegyVetseg = f3FegyelmiUgy.VetsegTipusJson?.Nev ?? "-",
                //FenyTipus = f3FegyelmiUgy.FenyitesTipusaJson?.Nev ?? "-",
                //FenyIdotart = f3FegyelmiUgy.FenyitesTartamEv != null || f3FegyelmiUgy.FenyitesTartamHo != null || f3FegyelmiUgy.FenyitesTartamNap != null ?
                //$"{(f3FegyelmiUgy.FenyitesTartamEv != null && f3FegyelmiUgy.FenyitesTartamEv > 0 ? f3FegyelmiUgy.FenyitesTartamEv + " év " : "")}" +
                //$"{(f3FegyelmiUgy.FenyitesTartamHo != null && f3FegyelmiUgy.FenyitesTartamHo > 0 ? f3FegyelmiUgy.FenyitesTartamHo + " hónap " : "")}" +
                //$"{(f3FegyelmiUgy.FenyitesTartamNap != null && f3FegyelmiUgy.FenyitesTartamNap > 0 ? f3FegyelmiUgy.FenyitesTartamNap + " nap" : "")}" : "",
                //KietkezesCsokkentes = f3FegyelmiUgy.KietkezesCsokkentesHo != null && f3FegyelmiUgy.KietkezesCsokkentesHo > 0 ? $"{f3FegyelmiUgy.KietkezesCsokkentesHo} hónap" : "", // nincs mérték
                //HatarozathozoNev = f3FegyelmiUgy.ElsoFokuHatBaratJson?.Nev ?? "",
                //HatarozathozoRang = hatarozatHozo?.RendfokozatJson?.Nev ?? "",
                //HatarozathozoTitulus = hatarozatHozo?.Megszolitas ?? "",
                //HatarozathozoSzam = null,
                //HatarozatHely = $"{intezet?.CimHelysegNev ?? "-"}",
                //HatarozatDatum = $"{f3FegyelmiUgy.ElsoFokHatarozatDatum?.ToString("yyyy. MM. dd. HH:mm") ?? DateTime.Now.ToString("yyyy. MM. dd. HH:mm")}",
                //IndoklasText = f3FegyelmiUgy.EfokIndokSzoveg ?? "",
                //Torvenyszek = f3FegyelmiUgy.BirosagSzervezet,
                //TartozkodasFokaJogcime = fogvatartott.VegrehajtasiFokKszId != 0 ? KodszotarFunctions.FindById(fogvatartott.VegrehajtasiFokKszId).Nev : ""
                //FegyelmiUgyekOsszevont = null,
                //HatarozatHozoJogkoreCimkeId = elsoFokuHatJogkore.Fonix2Id,
                //MegszuntetesOka = null,
                //PanasszalElt = f3FegyelmiUgy.EljarasFoka != null && f3FegyelmiUgy.EljarasFoka > 1
            };

            if (model.FenyIdotart == "" && model.KietkezesCsokkentes == "")
                model.FenyIdotart = "nincs megadva";

            //if (f3FegyelmiUgy.MaganelzarasNap != null && f3FegyelmiUgy.MaganelzarasNap > 0)
            //    model.FenyIdotart = $"{f3FegyelmiUgy.MaganelzarasNap} nap";

            return model;

        }

        public FegyelmiHatarozatViewModel GetHatarozatById(int fegyelmiUgyId, int dokumentumTipusCimkeId, int? iktatasId = null, int? naploId = null)
        {
            KonasoftBVFonixContext.Configuration.LazyLoadingEnabled = false;
            Naplo naplo = null;
            FegyelmiUgy fegyelmiUgy;
            IktatottDokumentumokViewModel iktatas = null;
            FegyelmiHatarozatViewModel model;

            var fenyitesHosszanakMennyisegiEgysegei = CimkeFunctions.GetFegyelmiFenyitesHosszanakMennyisegiEgysegei();

            if (fegyelmiUgyId < 0)
            {
                //var f3FegyelmiUgy = new KonasoftBVFonix3Context().FanyUgyek.AsNoTracking().FirstOrDefault(f => f.Id == Math.Abs(fegyelmiUgyId));

                //if (f3FegyelmiUgy.EljarasFoka == null || f3FegyelmiUgy.EljarasFoka == 0)
                //{
                //    Log.Error($"Nincs elsőfokú határozat a fegyelmi ügyhöz: {fegyelmiUgyId}.");
                //    throw new WarningException("Nincs elsőfokú határozat a fegyelmi ügyhöz.");
                //}

                //var fogvatartott = FogvatartottFunctions.GetSzemelyesAdatokElhelyezessel(f3FegyelmiUgy.FogvatartottFanyId.Value);
                //KonasoftBVFonixContext.DisableToroltFlFilter(f => f.Intezet);
                //var intezet = KonasoftBVFonixContext.Intezet.AsNoTracking().Include(i => i.CimHelyseg).FirstOrDefault(f => f.Id == f3FegyelmiUgy.IntezetFanyId);
                //KonasoftBVFonixContext.EnableToroltFlFilter(f => f.Intezet);

                //Entities.Fonix3.Cimke elsoFokuHatJogkore = null;
                //if (f3FegyelmiUgy.ElsofokuHatJogkore != null)
                //    elsoFokuHatJogkore = new KonasoftBVFonix3Context().Cimkek.AsNoTracking().FirstOrDefault(f => f.Id == f3FegyelmiUgy.ElsofokuHatJogkoreJson.Id);

                //Barat hatarozatHozo = null;
                //if (f3FegyelmiUgy.ElsofokuHatBarat != null)
                //    hatarozatHozo = new KonasoftBVFonix3Context().Baratok.AsNoTracking().FirstOrDefault(f => f.ID == f3FegyelmiUgy.ElsoFokuHatBaratJson.Id);

                model = new FegyelmiHatarozatViewModel()
                {
                    //FegyelmiUgyId = fegyelmiUgyId,
                    //IntezetNev = f3FegyelmiUgy.IntezetJson?.Nev ?? "-",
                    //Iktatoszam = $"{f3FegyelmiUgy.IntezetFanyAzon}/{f3FegyelmiUgy.UgyEv}/{f3FegyelmiUgy.UgySorszam}",
                    //UgySzam = $"{f3FegyelmiUgy.IntezetFanyAzon}/{f3FegyelmiUgy.UgyEv}/{f3FegyelmiUgy.UgySorszam}",
                    ////MeghallgatasEve = naplo.ElsofokuTargyalasIdopontja?.Year,
                    ////MeghallgatasHava = naplo.ElsofokuTargyalasIdopontja?.Month,
                    ////MeghallgatasNapja = naplo.ElsofokuTargyalasIdopontja?.Day,
                    ////MeghallgatasIdeje = naplo.ElsofokuTargyalasIdopontja?.ToShortTimeString(),
                    //FogvatartottNev = $"{fogvatartott.FogvatartottSzemelyesAdatai.SzuletesiCsaladiNev} {fogvatartott.FogvatartottSzemelyesAdatai.SzuletesiUtonev}",
                    //FogvatartottSzulIdeje = fogvatartott.FogvatartottSzemelyesAdatai.SzuletesiDatum.ToString("yyyy. MM. dd."),
                    //FogvatartottSzulHelye = fogvatartott.FogvatartottSzemelyesAdatai.SzuletesiHelyNeve,
                    //FogvatartottAnyjaNeve = fogvatartott.FogvatartottSzemelyesAdatai.AnyjaNeve,
                    //FogvElhelyezese = fogvatartott.IntezetiObjektum != null ? $"{fogvatartott.IntezetiObjektum?.Nev ?? "-"}/{fogvatartott.Korlet?.Nev ?? "-"}/{fogvatartott.Zarka?.Azonosito ?? "-"}" : "",
                    //FogvSzabadul = fogvatartott.AktualisSzabadulasDatuma?.ToString("yyyy. MM. dd.") ?? "-",
                    //FogvAzon = fogvatartott.NyilvantartasiAzonosito,
                    //BvFok = fogvatartott.FogvatartasJellegeKszId != 0 ? KodszotarFunctions.FindById(fogvatartott.FogvatartasJellegeKszId).Nev : "",
                    //VegrehajtasiFok = fogvatartott.VegrehajtasiFokKszId != 0 ? KodszotarFunctions.FindById(fogvatartott.VegrehajtasiFokKszId).Nev : "",
                    //FegyVetseg = f3FegyelmiUgy.VetsegTipusJson?.Nev ?? "-",
                    //FenyTipus = f3FegyelmiUgy.FenyitesTipusaJson?.Nev ?? "-",
                    //FenyIdotart = f3FegyelmiUgy.FenyitesTartamEv != null || f3FegyelmiUgy.FenyitesTartamHo != null || f3FegyelmiUgy.FenyitesTartamNap != null ?
                    //$"{(f3FegyelmiUgy.FenyitesTartamEv != null && f3FegyelmiUgy.FenyitesTartamEv > 0 ? f3FegyelmiUgy.FenyitesTartamEv + " év " : "")}" +
                    //$"{(f3FegyelmiUgy.FenyitesTartamHo != null && f3FegyelmiUgy.FenyitesTartamHo > 0 ? f3FegyelmiUgy.FenyitesTartamHo + " hónap " : "")}" +
                    //$"{(f3FegyelmiUgy.FenyitesTartamNap != null && f3FegyelmiUgy.FenyitesTartamNap > 0 ? f3FegyelmiUgy.FenyitesTartamNap + " nap" : "")}" : "",
                    //KietkezesCsokkentes = f3FegyelmiUgy.KietkezesCsokkentesHo != null && f3FegyelmiUgy.KietkezesCsokkentesHo > 0 ? $"{f3FegyelmiUgy.KietkezesCsokkentesHo} hónap" : "", // nincs mérték
                    //HatarozathozoNev = f3FegyelmiUgy.ElsoFokuHatBaratJson?.Nev ?? "",
                    //HatarozathozoRang = hatarozatHozo?.RendfokozatJson?.Nev ?? "",
                    //HatarozathozoTitulus = hatarozatHozo?.Megszolitas ?? "",
                    //HatarozathozoSzam = null,
                    //HatarozatHelyDatum = $"{intezet?.CimHelysegNev ?? "-"}, {f3FegyelmiUgy.ElsoFokHatarozatDatum?.ToString("yyyy. MM. dd. HH:mm") ?? DateTime.Now.ToString("yyyy. MM. dd. HH:mm")}",
                    //IndoklasText = f3FegyelmiUgy.EfokIndokSzoveg ?? "",
                    //FegyelmiUgyekOsszevont = null,
                    //HatarozatHozoJogkoreCimkeId = elsoFokuHatJogkore?.Fonix2Id ?? 0,
                    //MegszuntetesOka = null,
                    //PanasszalElt = f3FegyelmiUgy.EljarasFoka != null && f3FegyelmiUgy.EljarasFoka > 1
                };

                if (model.FenyIdotart == "" && model.KietkezesCsokkentes == "")
                    model.FenyIdotart = "nincs megadva";

                //if (f3FegyelmiUgy.MaganelzarasNap != null && f3FegyelmiUgy.MaganelzarasNap > 0)
                //    model.FenyIdotart = $"{f3FegyelmiUgy.MaganelzarasNap} nap";

                return model;
            }

            if (iktatasId == null)
            {
                //using (DbContextTransaction transaction = KonasoftBVFonixContext.Database.BeginTransaction())
                //{
                    try
                    {
                        KonasoftBVFonixContext.DisableToroltFlFilter(x =>x.Intezet);

                        if (naploId != null)
                            naplo = KonasoftBVFonixContext.Naplo.AsNoTracking().Include(x => x.FegyelmiVetsegTipusaCimke)
                                .Include(x => x.FenyitesTipusCimke)
                                .Include(x => x.DonteshozoSzemely)
                                .Include(x => x.DonteshozoSzemely.Rendfokozat)
                                .Include(x => x.DonteshozoSzemely.Beosztas)
                                .SingleOrDefault(s => s.Id == naploId);

                        fegyelmiUgy = Table
                            .AsNoTracking()
                            .Include(x => x.FegyelmiVetsegTipusa)
                            .Include(x => x.FenyitesTipusa)
                            .Include(x => x.Intezet)
                            .Include(x => x.Intezet.CimHelyseg)
                            .Include(x => x.HatarozatotHozottSzemely)
                            .Include(x => x.HatarozatotHozottSzemely.Rendfokozat)
                            .Include(x => x.HatarozatotHozottSzemely.Beosztas)
                            .SingleOrDefault(s => s.Id == fegyelmiUgyId);

                        var fogvatartott = FogvatartottFunctions.GetSzemelyesAdatokElhelyezessel(fegyelmiUgy.FogvatartottId);

                        if (fegyelmiUgy == null)
                        {
                            Log.Info("Fegyelmi ügy nem található: " + fegyelmiUgyId);
                            throw new WarningException("A fegyelmi ügy nem található.");
                        }

                        // megnézzük van-e iktatás már, ha nincs újat generálunk

                        var iktatasKereses = IktatottDokumentumokFunctions.Table.FirstOrDefault(f => f.NaploId == naploId);

                        if (iktatasKereses == null)
                        {
                            IktatottDokumentumokViewModel ujIktatas = new IktatottDokumentumokViewModel()
                            {
                                FegyelmiUgyId = fegyelmiUgyId,
                                Alszam = 1,
                                AktivFl = true,
                                DokumentumTipusCimkeId = dokumentumTipusCimkeId,
                                NaploId = naploId
                            };

                            IktatottDokumentumokFunctions.CreateNyomtatvany(ujIktatas);

                            iktatas = (IktatottDokumentumokViewModel)KonasoftBVFonixContext.IktatottDokumentumok.Single(x => x.Id == ujIktatas.Id);
                        }
                        else
                            iktatas = (IktatottDokumentumokViewModel)iktatasKereses;
                        
                        if (naplo != null)
                        {
                            model = new FegyelmiHatarozatViewModel()
                            {
                                FegyelmiUgyId = fegyelmiUgy.Id,
                                IntezetNev = fegyelmiUgy.Intezet?.Nev ?? "-",
                                Iktatoszam = $"{fegyelmiUgy.UgySorszamaIntezetAzon}/{fegyelmiUgy.UgySorszamaEv}/{fegyelmiUgy.UgySorszama}/{iktatas.Alszam}",
                                UgySzam = $"{fegyelmiUgy.UgySorszamaIntezetAzon}/{fegyelmiUgy.UgySorszamaEv}/{fegyelmiUgy.UgySorszama}",
                                //MeghallgatasEve = naplo.ElsofokuTargyalasIdopontja?.Year,
                                //MeghallgatasHava = naplo.ElsofokuTargyalasIdopontja?.Month,
                                //MeghallgatasNapja = naplo.ElsofokuTargyalasIdopontja?.Day,
                                //MeghallgatasIdeje = naplo.ElsofokuTargyalasIdopontja?.ToShortTimeString(),
                                FogvatartottNev = $"{fogvatartott.FogvatartottSzemelyesAdatai.SzuletesiCsaladiNev} {fogvatartott.FogvatartottSzemelyesAdatai.SzuletesiUtonev}",
                                FogvatartottSzulIdeje = fogvatartott.FogvatartottSzemelyesAdatai.SzuletesiDatum.ToString("yyyy. MM. dd."),
                                FogvatartottSzulHelye = fogvatartott.FogvatartottSzemelyesAdatai.SzuletesiHelyNeve,
                                FogvatartottAnyjaNeve = fogvatartott.FogvatartottSzemelyesAdatai.AnyjaNeve,
                                FogvElhelyezese = fogvatartott.IntezetiObjektum != null ? $"{fogvatartott.IntezetiObjektum.Nev}/{fogvatartott.Korlet.Nev}/{fogvatartott.Zarka.Azonosito}" : "",
                                FogvSzabadul = fogvatartott.AktualisSzabadulasDatuma?.ToString("yyyy. MM. dd.") ?? "-",
                                FogvAzon = fogvatartott.NyilvantartasiAzonosito,
                                BvFok = fogvatartott.FogvatartasJellegeKszId != 0 ? KodszotarFunctions.FindById(fogvatartott.FogvatartasJellegeKszId).Nev : "",
                                VegrehajtasiFok = fogvatartott.VegrehajtasiFokKszId != 0 ? KodszotarFunctions.FindById(fogvatartott.VegrehajtasiFokKszId).Nev : "",
                                FegyVetseg = naplo.FegyelmiVetsegTipusaCimke?.Nev ?? "-",
                                FenyTipus = naplo.FenyitesTipusCimke?.Nev ?? "-",
                                FenyIdotart = naplo.FenyitesHossza != null ? $"{naplo.FenyitesHossza.Value} ({SzamValtasaBeture(naplo.FenyitesHossza.Value)}) {fenyitesHosszanakMennyisegiEgysegei.SingleOrDefault(s => s.Id == naplo.FenyitesHosszaMennyisegiEgysegCimkeId)?.Nev.ToLower() ?? "Nincs megadva"}" : "-",
                                KietkezesCsokkentes = naplo.KietkezesCsokkentes != null ? $", {naplo.KietkezesCsokkentes.Value.ToString()} ({SzamValtasaBeture(naplo.KietkezesCsokkentes.Value)}) %" : "",
                                HatarozathozoNev = naplo.DonteshozoSzemely?.Nev ?? "",
                                HatarozathozoRang = naplo.DonteshozoSzemely?.RendfokozatKszId == 0 ? "" : (naplo.DonteshozoSzemely?.Rendfokozat?.Nev ?? ""),
                                HatarozathozoTitulus = naplo.DonteshozoSzemely?.BeosztasKszId == 0 ? "" : (naplo.DonteshozoSzemely?.Beosztas?.Nev ?? ""),
                                HatarozathozoSzam = fegyelmiUgy.FenyitesTipusCimkeId == (int)CimkeEnums.FenyitesTipusok.Megszuntetes ? "" : naplo.DonteshozoSzemely?.Azonosito ?? "",
                                HatarozatHelyDatum = $"{fegyelmiUgy.Intezet.CimHelyseg}, {naplo.LetrehozasDatuma.ToString("yyyy. MM. dd. HH:mm")}",
                                IndoklasText = naplo.JegyzokonyvTartalma ?? "",
                                FegyelmiUgyekOsszevont = String.Join(", ", GetOsszevontFegyelmiUgyekForId(fegyelmiUgyId).Select(x => $"{x.UgyIntezetAzon}/{x.UgyEve}/{x.UgySzama}")),
                                HatarozatHozoJogkoreCimkeId = fegyelmiUgy.HatarozatHozoJogkoreCimkeId,
                                MegszuntetesOka = naplo.MegszuntetesOkaCimkeId != null ? CimkeFunctions.FindById(naplo.MegszuntetesOkaCimkeId.Value).Nev : null,
                                PanasszalElt = !naplo.AlairtaFl
                            };
                        }
                        else
                        {
                            Log.Info("Napló nem található: " + naploId);
                            throw new WarningException("A napló nem található.");
                            //return null;
                        }

                        iktatas.DokumentumAdat = IktatottDokumentumokFunctions.SerializeIktatottNyomtatvanyAdat(model);
                        IktatottDokumentumokFunctions.Modify(iktatas);
                        KonasoftBVFonixContext.SaveChanges();
                        //transaction.Commit();
                    }
                    catch (Exception e)
                    {

                        Log.Error($"Hiba az határozat iktatása során (fegyelmiUgyId: {fegyelmiUgyId})", e);
                        //transaction.Rollback();
                        throw;
                    }
                    finally
                    {
                        KonasoftBVFonixContext.EnableToroltFlFilter(x => x.Intezet);
                    }
                //}
            }
            else
            {
                iktatas = (IktatottDokumentumokViewModel)IktatottDokumentumokFunctions.Table.FirstOrDefault(f => f.Id == iktatasId.Value);
                model = IktatottDokumentumokFunctions.DeserializeIktatottNyomtatvanyAdat<FegyelmiHatarozatViewModel>(iktatas.DokumentumAdat);
            }

            return model;
        }

        public List<int> GetFogvatartottIdsWithNyitottFegyelmiUgy(List<int> fogvatartottIds)
        {
            return Table.Where(w => fogvatartottIds.Contains(w.FogvatartottId) && w.Lezarva != true).Select(s => s.FogvatartottId).ToList();
        }

        public KioktatasReintegraciosTisztiJogkorbenViewModel GetKioktatasNyomtatvanyById(int fegyelmiUgyId, int? iktatasId = null, int? naploId = null)
        {
            KonasoftBVFonixContext.Configuration.LazyLoadingEnabled = false;
            Naplo naplo = null;
            FegyelmiUgy fegyelmiUgy;
            IktatottDokumentumokViewModel iktatas = null;
            KioktatasReintegraciosTisztiJogkorbenViewModel model;

            if (iktatasId == null)
            {
                //using (DbContextTransaction transaction = KonasoftBVFonixContext.Database.BeginTransaction())
                //{
                try
                {
                    if (naploId != null)
                        naplo = KonasoftBVFonixContext.Naplo.AsNoTracking().Include(x => x.FegyelmiVetsegTipusaCimke) // átnézni
                            .Include(x => x.RogzitoSzemely)
                            .SingleOrDefault(s => s.Id == naploId);

                    fegyelmiUgy = Table // átnézni
                        .AsNoTracking()
                        .Include(x => x.Intezet)
                        .Include(x => x.Intezet.CimHelyseg)
                        .SingleOrDefault(s => s.Id == fegyelmiUgyId);

                    var fogvatartott = FogvatartottFunctions.GetSzemelyesAdatokElhelyezessel(fegyelmiUgy.FogvatartottId); // talán sok is

                    if (fegyelmiUgy == null)
                    {
                        Log.Info("Fegyelmi ügy nem található: " + fegyelmiUgyId);
                        throw new WarningException("A fegyelmi ügy nem található.");
                    }

                    // megnézzük van-e iktatás már, ha nincs újat generálunk

                    var iktatasKereses = IktatottDokumentumokFunctions.Table.FirstOrDefault(f => f.NaploId == naploId);

                    if (iktatasKereses == null)
                    {
                        IktatottDokumentumokViewModel ujIktatas = new IktatottDokumentumokViewModel()
                        {
                            FegyelmiUgyId = fegyelmiUgyId,
                            Alszam = 1,
                            AktivFl = true,
                            DokumentumTipusCimkeId = (int)IktatottDokumentumTipusok.ReintegraciosTisztiKioktatas,
                            NaploId = naploId
                        };

                        IktatottDokumentumokFunctions.CreateNyomtatvany(ujIktatas);

                        iktatas = (IktatottDokumentumokViewModel)KonasoftBVFonixContext.IktatottDokumentumok.Single(x => x.Id == ujIktatas.Id);
                    }
                    else
                        iktatas = (IktatottDokumentumokViewModel)iktatasKereses;

                    if (naplo != null)
                    {
                        model = new KioktatasReintegraciosTisztiJogkorbenViewModel()
                        {
                            FegyelmiUgyId = fegyelmiUgy.Id,
                            IntezetNev = fegyelmiUgy.Intezet.Nev,
                            Iktatoszam = $"{fegyelmiUgy.UgySorszamaIntezetAzon}/{fegyelmiUgy.UgySorszamaEv}/{fegyelmiUgy.UgySorszama}/{iktatas.Alszam}",
                            UgySzam = $"{fegyelmiUgy.UgySorszamaIntezetAzon}/{fegyelmiUgy.UgySorszamaEv}/{fegyelmiUgy.UgySorszama}",
                            Nev = $"{fogvatartott.FogvatartottSzemelyesAdatai.SzuletesiCsaladiNev} {fogvatartott.FogvatartottSzemelyesAdatai.SzuletesiUtonev}",
                            SzulDatum = fogvatartott.FogvatartottSzemelyesAdatai.SzuletesiDatum.ToString("yyyy. MM. dd."),
                            AnyjaNeve = fogvatartott.FogvatartottSzemelyesAdatai.AnyjaNeve,
                            FogvAzon = fogvatartott.NyilvantartasiAzonosito,
                            VegrehajtasFoka = fogvatartott.VegrehajtasiFokKszId != 0 ? KodszotarFunctions.FindById(fogvatartott.VegrehajtasiFokKszId).Nev : "-",
                            Hely = fegyelmiUgy.Intezet.CimHelyseg.Nev,
                            Ev = naplo.FenyitesKiszabasDatuma?.Year,
                            Honap = naplo.FenyitesKiszabasDatuma?.Month,
                            Nap = naplo.FenyitesKiszabasDatuma?.Day,
                            Elhelyezes = fogvatartott.IntezetiObjektum != null ? $"{fogvatartott.IntezetiObjektum.Nev}/{fogvatartott.Korlet.Nev}/{fogvatartott.Zarka.Azonosito}" : "",
                            FoglalkozasText = naplo.JegyzokonyvTartalma,
                            FoglalkozastTartotta = naplo.RogzitoSzemely.Nev
                        };
                    }
                    else
                    {
                        Log.Info("Napló nem található: " + naploId);
                        throw new WarningException("A napló nem található.");
                        //return null;
                    }

                    iktatas.DokumentumAdat = IktatottDokumentumokFunctions.SerializeIktatottNyomtatvanyAdat(model);
                    IktatottDokumentumokFunctions.Modify(iktatas);
                    KonasoftBVFonixContext.SaveChanges();
                    //transaction.Commit();
                }
                catch (Exception e)
                {

                    Log.Error($"Hiba az határozat iktatása során (fegyelmiUgyId: {fegyelmiUgyId})", e);
                    //transaction.Rollback();
                    throw;
                }
                //}
            }
            else
            {
                iktatas = (IktatottDokumentumokViewModel)IktatottDokumentumokFunctions.Table.FirstOrDefault(f => f.Id == iktatasId.Value);
                model = IktatottDokumentumokFunctions.DeserializeIktatottNyomtatvanyAdat<KioktatasReintegraciosTisztiJogkorbenViewModel>(iktatas.DokumentumAdat);
            }

            return model;
        }

        public List<VedoKirendeleseNyilatkozatViewModel> GetVedoKirendeleseNyilatkozatNyomtatvanyokByNaplok(List<int> naploIds)
        {
            KonasoftBVFonixContext.Configuration.LazyLoadingEnabled = false;
            NaploViewModel naplo = null;
            FegyelmiUgy fegyelmiUgy;
            IktatottDokumentumokViewModel iktatas;
            FogvatartottViewModel fogvatartott;
            int aktualisNaploId = 0;
            VedoKirendeleseNyilatkozatViewModel model;
            List<VedoKirendeleseNyilatkozatViewModel> results = new List<VedoKirendeleseNyilatkozatViewModel>();

            //using (DbContextTransaction transaction = KonasoftBVFonixContext.Database.BeginTransaction())
            //{
            try
            {
                foreach (int naploId in naploIds)
                {
                    aktualisNaploId = naploId;
                    naplo = NaploFunctions.FindById(naploId);

                    fegyelmiUgy = Table // átnézni
                        .AsNoTracking()
                        .Include(x => x.Intezet)
                        .Include(x => x.Intezet.CimHelyseg)
                        .SingleOrDefault(s => s.Id == naplo.FegyelmiUgyId);

                    fogvatartott = FogvatartottFunctions.GetSzemelyesAdatokElhelyezessel(fegyelmiUgy.FogvatartottId);

                    // megnézzük van-e iktatás már, ha nincs újat generálunk

                    var iktatasKereses = IktatottDokumentumokFunctions.Table.FirstOrDefault(f => f.NaploId == naploId && f.DokumentumTipusCimkeId == (int)IktatottDokumentumTipusok.KirendeltVedoKereseNyilatkozat);

                    if (iktatasKereses == null)
                    {
                        IktatottDokumentumokViewModel ujIktatas = new IktatottDokumentumokViewModel()
                        {
                            FegyelmiUgyId = naplo.FegyelmiUgyId,
                            Alszam = 1,
                            AktivFl = true,
                            DokumentumTipusCimkeId = (int)IktatottDokumentumTipusok.KirendeltVedoKereseNyilatkozat,
                            NaploId = naploId
                        };

                        IktatottDokumentumokFunctions.CreateNyomtatvany(ujIktatas);

                        iktatas = (IktatottDokumentumokViewModel)KonasoftBVFonixContext.IktatottDokumentumok.Single(x => x.Id == ujIktatas.Id);
                    }
                    else
                        iktatas = (IktatottDokumentumokViewModel)iktatasKereses;

                    if (naplo != null)
                    {
                        model = new VedoKirendeleseNyilatkozatViewModel()
                        {
                            FegyelmiUgyId = fegyelmiUgy.Id,
                            IntezetNev = fegyelmiUgy.Intezet.Nev,
                            Iktatoszam = $"{fegyelmiUgy.UgySorszamaIntezetAzon}/{fegyelmiUgy.UgySorszamaEv}/{fegyelmiUgy.UgySorszama}/{iktatas.Alszam}",
                            UgySzam = $"{fegyelmiUgy.UgySorszamaIntezetAzon}/{fegyelmiUgy.UgySorszamaEv}/{fegyelmiUgy.UgySorszama}",
                            Nev = $"{fogvatartott.FogvatartottSzemelyesAdatai.SzuletesiCsaladiNev} {fogvatartott.FogvatartottSzemelyesAdatai.SzuletesiUtonev}",
                            FogvAzon = fogvatartott.NyilvantartasiAzonosito,
                            Varos = fegyelmiUgy.Intezet.CimHelyseg.Nev,
                            Ev = naplo.LetrehozasDatuma.Year,
                            Honap = naplo.LetrehozasDatuma.Month,
                            Nap = naplo.LetrehozasDatuma.Day,
                        };
                    }
                    else
                    {
                        Log.Info("Napló nem található: " + naploId);
                        throw new WarningException("A napló nem található.");
                    }

                    iktatas.DokumentumAdat = IktatottDokumentumokFunctions.SerializeIktatottNyomtatvanyAdat(model);
                    IktatottDokumentumokFunctions.Modify(iktatas);
                    results.Add(model);
                    KonasoftBVFonixContext.SaveChanges();

                }
                //transaction.Commit();
            }
            catch (Exception e)
            {

                Log.Error($"Hiba a dokumentum iktatása során (naplóId: {aktualisNaploId})", e);
                //transaction.Rollback();
                throw;
            }
            //}

            return results;
        }

        public List<VedoKirendeleseNyilatkozatViewModel> GetVedoKirendeleseNyilatkozatNyomtatvanyokByIktatasok(List<int> iktatasIds)
        {
            KonasoftBVFonixContext.Configuration.LazyLoadingEnabled = false;
            IktatottDokumentumokViewModel iktatas;
            int aktualisIktatasId = 0;
            VedoKirendeleseNyilatkozatViewModel model;
            List<VedoKirendeleseNyilatkozatViewModel> results = new List<VedoKirendeleseNyilatkozatViewModel>();

            try
            {
                foreach (var iktatasId in iktatasIds)
                {
                    aktualisIktatasId = iktatasId;
                    iktatas = (IktatottDokumentumokViewModel)IktatottDokumentumokFunctions.Table.FirstOrDefault(f => f.Id == iktatasId);
                    model = IktatottDokumentumokFunctions.DeserializeIktatottNyomtatvanyAdat<VedoKirendeleseNyilatkozatViewModel>(iktatas.DokumentumAdat);
                    results.Add(model);
                }
            }
            catch (Exception e)
            {

                Log.Error($"Hiba az iktatás betöltése során (iktatasId: {aktualisIktatasId})", e);
                throw;
            }

            return results;
        }

        public List<VedoKirendeleseViewModel> GetVedoKirendeleseNyomtatvanyokByNaplok(List<int> naploIds)
        {
            KonasoftBVFonixContext.Configuration.LazyLoadingEnabled = false;
            NaploViewModel naplo = null;
            FegyelmiUgy fegyelmiUgy;
            IktatottDokumentumokViewModel iktatas;
            FogvatartottViewModel fogvatartott;
            int aktualisNaploId = 0;
            VedoKirendeleseViewModel model;
            List<VedoKirendeleseViewModel> results = new List<VedoKirendeleseViewModel>();

            //using (DbContextTransaction transaction = KonasoftBVFonixContext.Database.BeginTransaction())
            //{
                try
                {
                    foreach (int naploId in naploIds)
                    {
                        aktualisNaploId = naploId;
                        naplo = NaploFunctions.FindById(naploId);

                        fegyelmiUgy = Table // átnézni
                            .AsNoTracking()
                            .Include(x => x.Intezet)
                            .Include(x => x.Intezet.CimHelyseg)
                            .SingleOrDefault(s => s.Id == naplo.FegyelmiUgyId);

                        fogvatartott = FogvatartottFunctions.GetSzemelyesAdatokElhelyezessel(fegyelmiUgy.FogvatartottId);

                        // megnézzük van-e iktatás már, ha nincs újat generálunk

                        var iktatasKereses = IktatottDokumentumokFunctions.Table.FirstOrDefault(f => f.NaploId == naploId && f.DokumentumTipusCimkeId == (int)IktatottDokumentumTipusok.KirendeltVedoKerese);

                        if (iktatasKereses == null)
                        {
                            IktatottDokumentumokViewModel ujIktatas = new IktatottDokumentumokViewModel()
                            {
                                FegyelmiUgyId = naplo.FegyelmiUgyId,
                                Alszam = 1,
                                AktivFl = true,
                                DokumentumTipusCimkeId = (int)IktatottDokumentumTipusok.KirendeltVedoKerese,
                                NaploId = naploId
                            };

                            IktatottDokumentumokFunctions.CreateNyomtatvany(ujIktatas);

                            iktatas = (IktatottDokumentumokViewModel)KonasoftBVFonixContext.IktatottDokumentumok.Single(x => x.Id == ujIktatas.Id);
                        }
                        else
                            iktatas = (IktatottDokumentumokViewModel)iktatasKereses;

                        if (naplo != null)
                        {
                            model = new VedoKirendeleseViewModel()
                            {
                                FegyelmiUgyId = fegyelmiUgy.Id,
                                IntezetNev = fegyelmiUgy.Intezet.Nev,
                                Iktatoszam = $"{fegyelmiUgy.UgySorszamaIntezetAzon}/{fegyelmiUgy.UgySorszamaEv}/{fegyelmiUgy.UgySorszama}/{iktatas.Alszam}",
                                UgySzam = $"{fegyelmiUgy.UgySorszamaIntezetAzon}/{fegyelmiUgy.UgySorszamaEv}/{fegyelmiUgy.UgySorszama}",
                                Nev = $"{fogvatartott.FogvatartottSzemelyesAdatai.SzuletesiCsaladiNev} {fogvatartott.FogvatartottSzemelyesAdatai.SzuletesiUtonev}",
                                SzulDatum = fogvatartott.FogvatartottSzemelyesAdatai.SzuletesiDatum.ToString("yyyy. MM. dd."),
                                AnyjaNeve = fogvatartott.FogvatartottSzemelyesAdatai.AnyjaNeve,
                                FogvAzon = fogvatartott.NyilvantartasiAzonosito,
                                NyomtatoSzemely = AlkalmazasKontextusFunctions.Kontextus.SzemelyzetNev,
                                //Torvenyszek = TorvenyszekFunctions.FindById(naplo.TorvenyszekId.Value).TorvenyszekNeve
                            };
                        }
                        else
                        {
                            Log.Info("Napló nem található: " + naploId);
                            throw new WarningException("A napló nem található.");
                        }

                        iktatas.DokumentumAdat = IktatottDokumentumokFunctions.SerializeIktatottNyomtatvanyAdat(model);
                        IktatottDokumentumokFunctions.Modify(iktatas);
                        results.Add(model);
                        KonasoftBVFonixContext.SaveChanges();
                    }
                    //transaction.Commit();
                }
                catch (Exception e)
                {

                    Log.Error($"Hiba a dokumentum iktatása során (naplóId: {aktualisNaploId})", e);
                    //transaction.Rollback();
                    throw;
                }
            //}

            return results;
        }

        public List<VedoKirendeleseViewModel> GetVedoKirendeleseNyomtatvanyokByIktatasok(List<int> iktatasIds)
        {
            KonasoftBVFonixContext.Configuration.LazyLoadingEnabled = false;
            IktatottDokumentumokViewModel iktatas;
            int aktualisIktatasId = 0;
            VedoKirendeleseViewModel model;
            List<VedoKirendeleseViewModel> results = new List<VedoKirendeleseViewModel>();

            try
            {
                foreach (var iktatasId in iktatasIds)
                {
                    aktualisIktatasId = iktatasId;
                    iktatas = (IktatottDokumentumokViewModel)IktatottDokumentumokFunctions.Table.FirstOrDefault(f => f.Id == iktatasId);
                    model = IktatottDokumentumokFunctions.DeserializeIktatottNyomtatvanyAdat<VedoKirendeleseViewModel>(iktatas.DokumentumAdat);
                    results.Add(model);
                }
            }
            catch (Exception e)
            {

                Log.Error($"Hiba az iktatás betöltése során (iktatasId: {aktualisIktatasId})", e);
                throw;
            }

            return results;
        }

        public List<MeghatalmazottVedoKirendeleseNyilatkozatViewModel> GetMeghatalmazottVedoKirendeleseNyilatkozatNyomtatvanyokByNaplok(List<int> naploIds)
        {
            KonasoftBVFonixContext.Configuration.LazyLoadingEnabled = false;
            NaploViewModel naplo = null;
            FegyelmiUgy fegyelmiUgy;
            IktatottDokumentumokViewModel iktatas;
            FogvatartottViewModel fogvatartott;
            int aktualisNaploId = 0;
            MeghatalmazottVedoKirendeleseNyilatkozatViewModel model;
            List<MeghatalmazottVedoKirendeleseNyilatkozatViewModel> results = new List<MeghatalmazottVedoKirendeleseNyilatkozatViewModel>();

            //using (DbContextTransaction transaction = KonasoftBVFonixContext.Database.BeginTransaction())
            //{
                try
                {
                    foreach (int naploId in naploIds)
                    {
                        aktualisNaploId = naploId;
                        naplo = NaploFunctions.FindById(naploId);

                        fegyelmiUgy = Table // átnézni
                            .AsNoTracking()
                            .Include(x => x.Intezet)
                            .Include(x => x.Intezet.CimHelyseg)
                            .SingleOrDefault(s => s.Id == naplo.FegyelmiUgyId);

                        fogvatartott = FogvatartottFunctions.GetSzemelyesAdatokElhelyezessel(fegyelmiUgy.FogvatartottId);

                        // megnézzük van-e iktatás már, ha nincs újat generálunk

                        var iktatasKereses = IktatottDokumentumokFunctions.Table.FirstOrDefault(f => f.NaploId == naploId && f.DokumentumTipusCimkeId == (int)IktatottDokumentumTipusok.MeghatalmazottVedoKereseNyilatkozat);

                        if (iktatasKereses == null)
                        {
                            IktatottDokumentumokViewModel ujIktatas = new IktatottDokumentumokViewModel()
                            {
                                FegyelmiUgyId = naplo.FegyelmiUgyId,
                                Alszam = 1,
                                AktivFl = true,
                                DokumentumTipusCimkeId = (int)IktatottDokumentumTipusok.MeghatalmazottVedoKereseNyilatkozat,
                                NaploId = naploId
                            };

                            IktatottDokumentumokFunctions.CreateNyomtatvany(ujIktatas);

                            iktatas = (IktatottDokumentumokViewModel)KonasoftBVFonixContext.IktatottDokumentumok.Single(x => x.Id == ujIktatas.Id);
                        }
                        else
                            iktatas = (IktatottDokumentumokViewModel)iktatasKereses;

                        if (naplo != null)
                        {
                            model = new MeghatalmazottVedoKirendeleseNyilatkozatViewModel()
                            {
                                FegyelmiUgyId = fegyelmiUgy.Id,
                                IntezetNev = fegyelmiUgy.Intezet.Nev,
                                Iktatoszam = $"{fegyelmiUgy.UgySorszamaIntezetAzon}/{fegyelmiUgy.UgySorszamaEv}/{fegyelmiUgy.UgySorszama}/{iktatas.Alszam}",
                                UgySzam = $"{fegyelmiUgy.UgySorszamaIntezetAzon}/{fegyelmiUgy.UgySorszamaEv}/{fegyelmiUgy.UgySorszama}",
                                Nev = $"{fogvatartott.FogvatartottSzemelyesAdatai.SzuletesiCsaladiNev} {fogvatartott.FogvatartottSzemelyesAdatai.SzuletesiUtonev}",
                                FogvAzon = fogvatartott.NyilvantartasiAzonosito,
                                Varos = fegyelmiUgy.Intezet.CimHelyseg.Nev,
                                Ev = naplo.LetrehozasDatuma.Year,
                                Honap = naplo.LetrehozasDatuma.Month,
                                Nap = naplo.LetrehozasDatuma.Day,
                                UrAsszony = CimkeFunctions.FindById(naplo.TitulusCimkeId.Value).Nev,
                                VedoCime = naplo.VedoCime,
                                VedoElerhetosege = naplo.VedoElerhetosege,
                                VedoNeve = naplo.VedoNeve,
                            };

                            model.UrAsszony = model.UrAsszony.Replace("Úr", "Ura") + "t";

                        }
                        else
                        {
                            Log.Info("Napló nem található: " + naploId);
                            throw new WarningException("A napló nem található.");
                        }

                        iktatas.DokumentumAdat = IktatottDokumentumokFunctions.SerializeIktatottNyomtatvanyAdat(model);
                        IktatottDokumentumokFunctions.Modify(iktatas);
                        results.Add(model);
                        KonasoftBVFonixContext.SaveChanges();
                    }
                    //transaction.Commit();
                }
                catch (Exception e)
                {

                    Log.Error($"Hiba a dokumentum iktatása során (naplóId: {aktualisNaploId})", e);
                    //transaction.Rollback();
                    throw;
                }
            //}

            return results;
        }

        public List<MeghatalmazottVedoKirendeleseNyilatkozatViewModel> GetMeghatalmazottVedoKirendeleseNyilatkozatNyomtatvanyokByIktatasok(List<int> iktatasIds)
        {
            KonasoftBVFonixContext.Configuration.LazyLoadingEnabled = false;
            IktatottDokumentumokViewModel iktatas;
            int aktualisIktatasId = 0;
            MeghatalmazottVedoKirendeleseNyilatkozatViewModel model;
            List<MeghatalmazottVedoKirendeleseNyilatkozatViewModel> results = new List<MeghatalmazottVedoKirendeleseNyilatkozatViewModel>();

            try
            {
                foreach (var iktatasId in iktatasIds)
                {
                    aktualisIktatasId = iktatasId;
                    iktatas = (IktatottDokumentumokViewModel)IktatottDokumentumokFunctions.Table.FirstOrDefault(f => f.Id == iktatasId);
                    model = IktatottDokumentumokFunctions.DeserializeIktatottNyomtatvanyAdat<MeghatalmazottVedoKirendeleseNyilatkozatViewModel>(iktatas.DokumentumAdat);
                    results.Add(model);
                }
            }
            catch (Exception e)
            {

                Log.Error($"Hiba az iktatás betöltése során (iktatasId: {aktualisIktatasId})", e);
                throw;
            }

            return results;
        }

        public List<MeghatalmazottVedoKirendeleseViewModel> GetMeghatalmazottVedoKirendeleseNyomtatvanyokByNaplok(List<int> naploIds)
        {
            KonasoftBVFonixContext.Configuration.LazyLoadingEnabled = false;
            NaploViewModel naplo = null;
            FegyelmiUgy fegyelmiUgy;
            IktatottDokumentumokViewModel iktatas;
            FogvatartottViewModel fogvatartott;
            int aktualisNaploId = 0;
            MeghatalmazottVedoKirendeleseViewModel model;
            List<MeghatalmazottVedoKirendeleseViewModel> results = new List<MeghatalmazottVedoKirendeleseViewModel>();

            //using (DbContextTransaction transaction = KonasoftBVFonixContext.Database.BeginTransaction())
            //{
                try
                {
                    foreach (int naploId in naploIds)
                    {
                        aktualisNaploId = naploId;
                        naplo = NaploFunctions.FindById(naploId);

                        fegyelmiUgy = Table // átnézni
                            .AsNoTracking()
                            .Include(x => x.Intezet)
                            .Include(x => x.Intezet.CimHelyseg)
                            .SingleOrDefault(s => s.Id == naplo.FegyelmiUgyId);

                        fogvatartott = FogvatartottFunctions.GetSzemelyesAdatokElhelyezessel(fegyelmiUgy.FogvatartottId);

                        // megnézzük van-e iktatás már, ha nincs újat generálunk

                        var iktatasKereses = IktatottDokumentumokFunctions.Table.FirstOrDefault(f => f.NaploId == naploId && f.DokumentumTipusCimkeId == (int)IktatottDokumentumTipusok.MeghatalmazottVedoKerese);

                        if (iktatasKereses == null)
                        {
                            IktatottDokumentumokViewModel ujIktatas = new IktatottDokumentumokViewModel()
                            {
                                FegyelmiUgyId = naplo.FegyelmiUgyId,
                                Alszam = 1,
                                AktivFl = true,
                                DokumentumTipusCimkeId = (int)IktatottDokumentumTipusok.MeghatalmazottVedoKerese,
                                NaploId = naploId
                            };

                            IktatottDokumentumokFunctions.CreateNyomtatvany(ujIktatas);

                            iktatas = (IktatottDokumentumokViewModel)KonasoftBVFonixContext.IktatottDokumentumok.Single(x => x.Id == ujIktatas.Id);
                        }
                        else
                            iktatas = (IktatottDokumentumokViewModel)iktatasKereses;

                        if (naplo != null)
                        {
                            model = new MeghatalmazottVedoKirendeleseViewModel()
                            {
                                FegyelmiUgyId = fegyelmiUgy.Id,
                                IntezetNev = fegyelmiUgy.Intezet.Nev,
                                Iktatoszam = $"{fegyelmiUgy.UgySorszamaIntezetAzon}/{fegyelmiUgy.UgySorszamaEv}/{fegyelmiUgy.UgySorszama}/{iktatas.Alszam}",
                                UgySzam = $"{fegyelmiUgy.UgySorszamaIntezetAzon}/{fegyelmiUgy.UgySorszamaEv}/{fegyelmiUgy.UgySorszama}",
                                Nev = $"{fogvatartott.FogvatartottSzemelyesAdatai.SzuletesiCsaladiNev} {fogvatartott.FogvatartottSzemelyesAdatai.SzuletesiUtonev}",
                                SzulDatum = fogvatartott.FogvatartottSzemelyesAdatai.SzuletesiDatum.ToString("yyyy. MM. dd."),
                                AnyjaNeve = fogvatartott.FogvatartottSzemelyesAdatai.AnyjaNeve,
                                FogvAzon = fogvatartott.NyilvantartasiAzonosito,
                                NyomtatoSzemely = AlkalmazasKontextusFunctions.Kontextus.SzemelyzetNev,
                                ElsofokuTargyalasIdopont = fegyelmiUgy.ElsofokuTargyalasIdopontja?.ToShortDateString() ?? "-",
                                MasodfokuTargyalasIdopont = fegyelmiUgy.MasodfokuTargyalasIdopontja?.ToShortDateString() ?? "-",
                                IntezetCime = $"{fegyelmiUgy.Intezet.CimIranyitoszam} {fegyelmiUgy.Intezet.CimHelyseg.Nev}, {fegyelmiUgy.Intezet.CimUtca} {fegyelmiUgy.Intezet.CimHazszam}",
                                UrAsszony = CimkeFunctions.FindById(naplo.TitulusCimkeId.Value).Nev,
                                VedoCime = naplo.VedoCime,
                                VedoElerhetosege = naplo.VedoElerhetosege,
                                VedoNeve = naplo.VedoNeve
                            };
                        }
                        else
                        {
                            Log.Info("Napló nem található: " + naploId);
                            throw new WarningException("A napló nem található.");
                        }

                        iktatas.DokumentumAdat = IktatottDokumentumokFunctions.SerializeIktatottNyomtatvanyAdat(model);
                        IktatottDokumentumokFunctions.Modify(iktatas);
                        results.Add(model);
                        KonasoftBVFonixContext.SaveChanges();

                    }
                    //transaction.Commit();
                }
                catch (Exception e)
                {

                    Log.Error($"Hiba a dokumentum iktatása során (naplóId: {aktualisNaploId})", e);
                    //transaction.Rollback();
                    throw;
                }
            //}

            return results;
        }

        public List<MeghatalmazottVedoKirendeleseViewModel> GetMeghatalmazottVedoKirendeleseNyomtatvanyokByIktatasok(List<int> iktatasIds)
        {
            KonasoftBVFonixContext.Configuration.LazyLoadingEnabled = false;
            IktatottDokumentumokViewModel iktatas;
            int aktualisIktatasId = 0;
            MeghatalmazottVedoKirendeleseViewModel model;
            List<MeghatalmazottVedoKirendeleseViewModel> results = new List<MeghatalmazottVedoKirendeleseViewModel>();

            try
            {
                foreach (var iktatasId in iktatasIds)
                {
                    aktualisIktatasId = iktatasId;
                    iktatas = (IktatottDokumentumokViewModel)IktatottDokumentumokFunctions.Table.FirstOrDefault(f => f.Id == iktatasId);
                    model = IktatottDokumentumokFunctions.DeserializeIktatottNyomtatvanyAdat<MeghatalmazottVedoKirendeleseViewModel>(iktatas.DokumentumAdat);
                    results.Add(model);
                }
            }
            catch (Exception e)
            {

                Log.Error($"Hiba az iktatás betöltése során (iktatasId: {aktualisIktatasId})", e);
                throw;
            }

            return results;
        }

        public List<VedoTelefonosTajekoztatasaViewModel> GetVedoTelefonosTajekoztatasaNyomtatvanyokByNaplok(List<int> naploIds)
        {
            KonasoftBVFonixContext.Configuration.LazyLoadingEnabled = false;
            NaploViewModel naplo = null;
            Szemelyzet szemelyzet = null;
            FegyelmiUgy fegyelmiUgy;
            IktatottDokumentumokViewModel iktatas;
            FogvatartottViewModel fogvatartott;
            int aktualisNaploId = 0;
            VedoTelefonosTajekoztatasaViewModel model;
            List<VedoTelefonosTajekoztatasaViewModel> results = new List<VedoTelefonosTajekoztatasaViewModel>();

            //using (DbContextTransaction transaction = KonasoftBVFonixContext.Database.BeginTransaction())
            //{
                try
                {
                    foreach (int naploId in naploIds)
                    {
                        aktualisNaploId = naploId;
                        naplo = NaploFunctions
                            .FindById(naploId);

                        szemelyzet = SzemelyzetFunctions.Table.AsNoTracking()
                            .Include(i => i.Rendfokozat)
                            .SingleOrDefault(x => x.Id == naplo.TajekoztatastNyujtoId.Value);

                        fegyelmiUgy = Table // átnézni
                            .AsNoTracking()
                            .Include(x => x.Intezet)
                            .Include(x => x.Intezet.CimHelyseg)
                            .SingleOrDefault(s => s.Id == naplo.FegyelmiUgyId);

                        fogvatartott = FogvatartottFunctions.GetSzemelyesAdatokElhelyezessel(fegyelmiUgy.FogvatartottId);

                        // megnézzük van-e iktatás már, ha nincs újat generálunk

                        var iktatasKereses = IktatottDokumentumokFunctions.Table.FirstOrDefault(f => f.NaploId == naploId && f.DokumentumTipusCimkeId == (int)IktatottDokumentumTipusok.VedoTelefonosTajekoztatasa);

                        if (iktatasKereses == null)
                        {
                            IktatottDokumentumokViewModel ujIktatas = new IktatottDokumentumokViewModel()
                            {
                                FegyelmiUgyId = naplo.FegyelmiUgyId,
                                Alszam = 1,
                                AktivFl = true,
                                DokumentumTipusCimkeId = (int)IktatottDokumentumTipusok.VedoTelefonosTajekoztatasa,
                                NaploId = naploId,
                            };

                            IktatottDokumentumokFunctions.CreateNyomtatvany(ujIktatas);

                            iktatas = (IktatottDokumentumokViewModel)KonasoftBVFonixContext.IktatottDokumentumok.Single(x => x.Id == ujIktatas.Id);
                        }
                        else
                            iktatas = (IktatottDokumentumokViewModel)iktatasKereses;

                        if (naplo != null)
                        {
                            model = new VedoTelefonosTajekoztatasaViewModel()
                            {
                                FegyelmiUgyId = fegyelmiUgy.Id,
                                IntezetNev = fegyelmiUgy.Intezet.Nev,
                                Iktatoszam = $"{fegyelmiUgy.UgySorszamaIntezetAzon}/{fegyelmiUgy.UgySorszamaEv}/{fegyelmiUgy.UgySorszama}/{iktatas.Alszam}",
                                UgySzam = $"{fegyelmiUgy.UgySorszamaIntezetAzon}/{fegyelmiUgy.UgySorszamaEv}/{fegyelmiUgy.UgySorszama}",
                                Nev = $"{fogvatartott.FogvatartottSzemelyesAdatai.SzuletesiCsaladiNev} {fogvatartott.FogvatartottSzemelyesAdatai.SzuletesiUtonev}",
                                SzulDatum = fogvatartott.FogvatartottSzemelyesAdatai.SzuletesiDatum.ToString("yyyy. MM. dd."),
                                AnyjaNeve = fogvatartott.FogvatartottSzemelyesAdatai.AnyjaNeve,
                                FogvAzon = fogvatartott.NyilvantartasiAzonosito,
                                NyomtatoSzemely = AlkalmazasKontextusFunctions.Kontextus.SzemelyzetNev,
                                IntezetCime = $"{fegyelmiUgy.Intezet.CimIranyitoszam} {fegyelmiUgy.Intezet.CimHelyseg.Nev}, {fegyelmiUgy.Intezet.CimUtca} {fegyelmiUgy.Intezet.CimHazszam}",
                                SikertelenText = naplo.TajekoztatasSikertelensegenekOka != null ? "A tájékoztatás sikertelenségének oka: " + naplo.TajekoztatasSikertelensegenekOka : "",
                                TajekoztatoIdopontja = naplo.TajekoztatasIdeje?.ToShortDateString() ?? "-",
                                TajekoztatoSzemelyNev = $"{szemelyzet.Nev}{(szemelyzet.RendfokozatKszId == 0 ? "" : " " + szemelyzet.Rendfokozat.Nev)}",
                                TajekoztatoTartalma = naplo.JegyzokonyvTartalma,
                                TajekoztatottSzemelyNev = naplo.Tajekoztatott,
                                TajekoztatottSzemelyTel = naplo.Telefonszam
                            };
                        }
                        else
                        {
                            Log.Info("Napló nem található: " + naploId);
                            throw new WarningException("A napló nem található.");
                        }

                        iktatas.DokumentumAdat = IktatottDokumentumokFunctions.SerializeIktatottNyomtatvanyAdat(model);
                        IktatottDokumentumokFunctions.Modify(iktatas);
                        results.Add(model);
                        KonasoftBVFonixContext.SaveChanges();
                        //transaction.Commit();
                    }
                }
                catch (Exception e)
                {

                    Log.Error($"Hiba a dokumentum iktatása során (naplóId: {aktualisNaploId})", e);
                    //transaction.Rollback();
                    throw;
                }
            //}

            return results;
        }

        public List<VedoTelefonosTajekoztatasaViewModel> GetVedoTelefonosTajekoztatasaNyomtatvanyokByIktatasok(List<int> iktatasIds)
        {
            KonasoftBVFonixContext.Configuration.LazyLoadingEnabled = false;
            IktatottDokumentumokViewModel iktatas;
            int aktualisIktatasId = 0;
            VedoTelefonosTajekoztatasaViewModel model;
            List<VedoTelefonosTajekoztatasaViewModel> results = new List<VedoTelefonosTajekoztatasaViewModel>();

            try
            {
                foreach (var iktatasId in iktatasIds)
                {
                    aktualisIktatasId = iktatasId;
                    iktatas = (IktatottDokumentumokViewModel)IktatottDokumentumokFunctions.Table.FirstOrDefault(f => f.Id == iktatasId);
                    model = IktatottDokumentumokFunctions.DeserializeIktatottNyomtatvanyAdat<VedoTelefonosTajekoztatasaViewModel>(iktatas.DokumentumAdat);
                    results.Add(model);
                }
            }
            catch (Exception e)
            {

                Log.Error($"Hiba az iktatás betöltése során (iktatasId: {aktualisIktatasId})", e);
                throw;
            }

            return results;
        }

        public List<MasodfokuTargyalasiJegyzokonyvViewModel> GetMasodfokuTargyalasiJegyzokonyvNyomtatvanyokByNaplok(List<int> naploIds)
        {
            KonasoftBVFonixContext.Configuration.LazyLoadingEnabled = false;
            NaploViewModel naplo = null;
            Szemelyzet jegyzokonyvVezeto = null;
            Szemelyzet donteshozo = null;
            FegyelmiUgy fegyelmiUgy;
            IktatottDokumentumokViewModel iktatas;
            FogvatartottViewModel fogvatartott;
            int aktualisNaploId = 0;
            MasodfokuTargyalasiJegyzokonyvViewModel model;
            List<MasodfokuTargyalasiJegyzokonyvViewModel> results = new List<MasodfokuTargyalasiJegyzokonyvViewModel>();

            //using (DbContextTransaction transaction = KonasoftBVFonixContext.Database.BeginTransaction())
            //{
                try
                {
                    foreach (int naploId in naploIds)
                    {
                        aktualisNaploId = naploId;
                        naplo = NaploFunctions
                            .FindById(naploId);

                        jegyzokonyvVezeto = SzemelyzetFunctions.Table.AsNoTracking()
                            .Include(i => i.Rendfokozat)
                            .SingleOrDefault(x => x.Id == naplo.JegyzokonyvVezetoSzemelyId.Value);

                        donteshozo = SzemelyzetFunctions.Table.AsNoTracking()
                            .Include(i => i.Rendfokozat)
                            .SingleOrDefault(x => x.Id == naplo.DonteshozoSzemelyId.Value);

                        fegyelmiUgy = Table
                            .AsNoTracking()
                            .Include(x => x.Intezet)
                            .Include(x => x.Intezet.CimHelyseg)
                            .SingleOrDefault(s => s.Id == naplo.FegyelmiUgyId);

                        fogvatartott = FogvatartottFunctions.GetSzemelyesAdatokElhelyezessel(fegyelmiUgy.FogvatartottId);

                        // megnézzük van-e iktatás már, ha nincs újat generálunk

                        var iktatasKereses = IktatottDokumentumokFunctions.Table.FirstOrDefault(f => f.NaploId == naploId && f.DokumentumTipusCimkeId == (int)IktatottDokumentumTipusok.MasodfokuTargyalasiJegyzokonyv);

                        if (iktatasKereses == null)
                        {
                            IktatottDokumentumokViewModel ujIktatas = new IktatottDokumentumokViewModel()
                            {
                                FegyelmiUgyId = naplo.FegyelmiUgyId,
                                Alszam = 1,
                                AktivFl = true,
                                DokumentumTipusCimkeId = (int)IktatottDokumentumTipusok.MasodfokuTargyalasiJegyzokonyv,
                                NaploId = naploId,
                            };

                            IktatottDokumentumokFunctions.CreateNyomtatvany(ujIktatas);

                            iktatas = (IktatottDokumentumokViewModel)KonasoftBVFonixContext.IktatottDokumentumok.Single(x => x.Id == ujIktatas.Id);
                        }
                        else
                            iktatas = (IktatottDokumentumokViewModel)iktatasKereses;

                        if (naplo != null)
                        {
                            model = new MasodfokuTargyalasiJegyzokonyvViewModel()
                            {
                                FegyelmiUgyId = fegyelmiUgy.Id,
                                Iktatoszam = $"{fegyelmiUgy.UgySorszamaIntezetAzon}/{fegyelmiUgy.UgySorszamaEv}/{fegyelmiUgy.UgySorszama}/{iktatas.Alszam}",
                                UgySzam = $"{fegyelmiUgy.UgySorszamaIntezetAzon}/{fegyelmiUgy.UgySorszamaEv}/{fegyelmiUgy.UgySorszama}",
                                Fogvatartott = $"{fogvatartott.FogvatartottSzemelyesAdatai.SzuletesiCsaladiNev} {fogvatartott.FogvatartottSzemelyesAdatai.SzuletesiUtonev}",
                                FogvatartottSzulIdeje = fogvatartott.FogvatartottSzemelyesAdatai.SzuletesiDatum.ToString("yyyy. MM. dd."),
                                FogvatartottAnyja = fogvatartott.FogvatartottSzemelyesAdatai.AnyjaNeve,
                                FogvatartottSzulHelye = fogvatartott.FogvatartottSzemelyesAdatai.SzuletesiHelyNeve,
                                FogvAzon = fogvatartott.NyilvantartasiAzonosito,
                                Egyeb = naplo.TovabbiJelenlevok ?? "",
                                JegyzokonyvText = naplo.JegyzokonyvTartalma,
                                JegyzokonyvVezeto = $"{jegyzokonyvVezeto.Nev}{(jegyzokonyvVezeto.RendfokozatKszId == 0 ? "" : " " + jegyzokonyvVezeto.Rendfokozat.Nev)}",
                                IntezetParancsnok = $"{donteshozo.Nev}{(donteshozo.RendfokozatKszId == 0 ? "" : " " + donteshozo.Rendfokozat.Nev)}",
                                Ev = naplo.LetrehozasDatuma.Year,
                                Honap = naplo.LetrehozasDatuma.Month,
                                Nap = naplo.LetrehozasDatuma.Day,
                                Ora = naplo.LetrehozasDatuma.Hour,
                                Perc = naplo.LetrehozasDatuma.Minute,
                                IntezetNev = fegyelmiUgy.Intezet.Nev
                            };
                        }
                        else
                        {
                            Log.Info("Napló nem található: " + naploId);
                            throw new WarningException("A napló nem található.");
                        }

                        iktatas.DokumentumAdat = IktatottDokumentumokFunctions.SerializeIktatottNyomtatvanyAdat(model);
                        IktatottDokumentumokFunctions.Modify(iktatas);
                        results.Add(model);
                        KonasoftBVFonixContext.SaveChanges();
                        //transaction.Commit();
                    }
                }
                catch (Exception e)
                {

                    Log.Error($"Hiba a dokumentum iktatása során (naplóId: {aktualisNaploId})", e);
                    //transaction.Rollback();
                    throw;
                }
            //}

            return results;
        }

        public List<MasodfokuTargyalasiJegyzokonyvViewModel> GetMasodfokuTargyalasiJegyzokonyvNyomtatvanyokByIktatasok(List<int> iktatasIds)
        {
            KonasoftBVFonixContext.Configuration.LazyLoadingEnabled = false;
            IktatottDokumentumokViewModel iktatas;
            int aktualisIktatasId = 0;
            MasodfokuTargyalasiJegyzokonyvViewModel model;
            List<MasodfokuTargyalasiJegyzokonyvViewModel> results = new List<MasodfokuTargyalasiJegyzokonyvViewModel>();

            try
            {
                foreach (var iktatasId in iktatasIds)
                {
                    aktualisIktatasId = iktatasId;
                    iktatas = (IktatottDokumentumokViewModel)IktatottDokumentumokFunctions.Table.FirstOrDefault(f => f.Id == iktatasId);
                    model = IktatottDokumentumokFunctions.DeserializeIktatottNyomtatvanyAdat<MasodfokuTargyalasiJegyzokonyvViewModel>(iktatas.DokumentumAdat);
                    results.Add(model);
                }
            }
            catch (Exception e)
            {

                Log.Error($"Hiba az iktatás betöltése során (iktatasId: {aktualisIktatasId})", e);
                throw;
            }

            return results;
        }

        public List<MasodfokuFegyelmiHatarozatMegszunteteseViewModel> GetMasodfokuFegyelmiHatarozatMegszunteteseNyomtatvanyokByNaplok(List<int> naploIds)
        {
            KonasoftBVFonixContext.Configuration.LazyLoadingEnabled = false;
            NaploViewModel naplo = null;
            Szemelyzet donteshozo = null;
            FegyelmiUgy fegyelmiUgy;
            IktatottDokumentumokViewModel iktatas;
            FogvatartottViewModel fogvatartott;
            int aktualisNaploId = 0;
            MasodfokuFegyelmiHatarozatMegszunteteseViewModel model;
            List<MasodfokuFegyelmiHatarozatMegszunteteseViewModel> results = new List<MasodfokuFegyelmiHatarozatMegszunteteseViewModel>();

            //using (DbContextTransaction transaction = KonasoftBVFonixContext.Database.BeginTransaction())
            //{
                try
                {
                    foreach (int naploId in naploIds)
                    {
                        aktualisNaploId = naploId;
                        naplo = NaploFunctions
                            .FindById(naploId);

                        donteshozo = SzemelyzetFunctions.Table.AsNoTracking()
                            .Include(i => i.Rendfokozat)
                            .Include(i => i.Beosztas)
                            .SingleOrDefault(x => x.Id == naplo.DonteshozoSzemelyId.Value);

                        fegyelmiUgy = Table
                            .AsNoTracking()
                            .Include(x => x.Intezet)
                            .Include(x => x.Intezet.CimHelyseg)
                            .SingleOrDefault(s => s.Id == naplo.FegyelmiUgyId);

                        fogvatartott = FogvatartottFunctions.GetSzemelyesAdatokElhelyezessel(fegyelmiUgy.FogvatartottId);

                        // megnézzük van-e iktatás már, ha nincs újat generálunk

                        var iktatasKereses = IktatottDokumentumokFunctions.Table.FirstOrDefault(f => f.NaploId == naploId && f.DokumentumTipusCimkeId == (int)IktatottDokumentumTipusok.MasodfokuHatarozatMegszuntetese);

                        if (iktatasKereses == null)
                        {
                            IktatottDokumentumokViewModel ujIktatas = new IktatottDokumentumokViewModel()
                            {
                                FegyelmiUgyId = naplo.FegyelmiUgyId,
                                Alszam = 1,
                                AktivFl = true,
                                DokumentumTipusCimkeId = (int)IktatottDokumentumTipusok.MasodfokuHatarozatMegszuntetese,
                                NaploId = naploId,
                            };

                            IktatottDokumentumokFunctions.CreateNyomtatvany(ujIktatas);

                            iktatas = (IktatottDokumentumokViewModel)KonasoftBVFonixContext.IktatottDokumentumok.Single(x => x.Id == ujIktatas.Id);
                        }
                        else
                            iktatas = (IktatottDokumentumokViewModel)iktatasKereses;

                        if (naplo != null)
                        {
                            model = new MasodfokuFegyelmiHatarozatMegszunteteseViewModel()
                            {
                                FegyelmiUgyId = fegyelmiUgy.Id,
                                Iktatoszam = $"{fegyelmiUgy.UgySorszamaIntezetAzon}/{fegyelmiUgy.UgySorszamaEv}/{fegyelmiUgy.UgySorszama}/{iktatas.Alszam}",
                                Fogvatartott = $"{fogvatartott.FogvatartottSzemelyesAdatai.SzuletesiCsaladiNev} {fogvatartott.FogvatartottSzemelyesAdatai.SzuletesiUtonev}",
                                FogvatartottSzulIdeje = fogvatartott.FogvatartottSzemelyesAdatai.SzuletesiDatum.ToString("yyyy. MM. dd."),
                                FogvatartottAnyjaNeve = fogvatartott.FogvatartottSzemelyesAdatai.AnyjaNeve,
                                FogvatartottSzulHelye = fogvatartott.FogvatartottSzemelyesAdatai.SzuletesiHelyNeve,
                                FogvAzon = fogvatartott.NyilvantartasiAzonosito,
                                BvFok = fogvatartott.FogvatartasJellegeKszId != 0 ? KodszotarFunctions.FindById(fogvatartott.FogvatartasJellegeKszId).Nev : "",
                                FogvSzabadul = fogvatartott.AktualisSzabadulasDatuma?.ToString("yyyy. MM. dd.") ?? "-",
                                HatarozatDatum = $"{naplo.LetrehozasDatuma.ToString("yyyy. MM. dd.")}",
                                HatarozatHely = $"{fegyelmiUgy.Intezet.CimHelyseg}",
                                HatarozathozoNev = donteshozo != null ? donteshozo?.Nev : "",
                                HatarozathozoRang = donteshozo?.RendfokozatKszId == 0 ? "" : donteshozo?.Rendfokozat?.Nev,
                                HatarozathozoSzam = fegyelmiUgy.FenyitesTipusCimkeId == (int)CimkeEnums.FenyitesTipusok.Megszuntetes ? "" : donteshozo != null ? donteshozo?.Azonosito : "",
                                HatarozathozoTitulus = donteshozo?.BeosztasKszId == 0 ? "" : (donteshozo?.Beosztas?.Nev ?? ""),
                                MegszuntetesOka = CimkeFunctions.FindById(naplo.MegszuntetesOkaCimkeId.Value).Nev,
                                IndoklasText = naplo.JegyzokonyvTartalma,
                                IntezetNev = fegyelmiUgy.Intezet.Nev,
                                Elhelyezes = fogvatartott.IntezetiObjektum != null ? $"{fogvatartott.IntezetiObjektum.Nev}/{fogvatartott.Korlet.Nev}/{fogvatartott.Zarka.Azonosito}" : "",
                                //Torvenyszek = naplo.TorvenyszekId.HasValue ? TorvenyszekFunctions.FindById(naplo.TorvenyszekId.Value).TorvenyszekNeve : "",
                                VegrehajtasiFok = fogvatartott.VegrehajtasiFokKszId != 0 ? KodszotarFunctions.FindById(fogvatartott.VegrehajtasiFokKszId).Nev : ""
                            };
                        }
                        else
                        {
                            Log.Info("Napló nem található: " + naploId);
                            throw new WarningException("A napló nem található.");
                        }

                        iktatas.DokumentumAdat = IktatottDokumentumokFunctions.SerializeIktatottNyomtatvanyAdat(model);
                        IktatottDokumentumokFunctions.Modify(iktatas);
                        results.Add(model);
                        KonasoftBVFonixContext.SaveChanges();
                        //transaction.Commit();
                    }
                }
                catch (Exception e)
                {

                    Log.Error($"Hiba a dokumentum iktatása során (naplóId: {aktualisNaploId})", e);
                    //transaction.Rollback();
                    throw;
                }
            //}

            return results;
        }

        public List<MasodfokuFegyelmiHatarozatMegszunteteseViewModel> GetMasodfokuFegyelmiHatarozatMegszunteteseNyomtatvanyokByIktatasok(List<int> iktatasIds)
        {
            KonasoftBVFonixContext.Configuration.LazyLoadingEnabled = false;
            IktatottDokumentumokViewModel iktatas;
            int aktualisIktatasId = 0;
            MasodfokuFegyelmiHatarozatMegszunteteseViewModel model;
            List<MasodfokuFegyelmiHatarozatMegszunteteseViewModel> results = new List<MasodfokuFegyelmiHatarozatMegszunteteseViewModel>();

            try
            {
                foreach (var iktatasId in iktatasIds)
                {
                    aktualisIktatasId = iktatasId;
                    iktatas = (IktatottDokumentumokViewModel)IktatottDokumentumokFunctions.Table.FirstOrDefault(f => f.Id == iktatasId);
                    model = IktatottDokumentumokFunctions.DeserializeIktatottNyomtatvanyAdat<MasodfokuFegyelmiHatarozatMegszunteteseViewModel>(iktatas.DokumentumAdat);
                    results.Add(model);
                }
            }
            catch (Exception e)
            {

                Log.Error($"Hiba az iktatás betöltése során (iktatasId: {aktualisIktatasId})", e);
                throw;
            }

            return results;
        }

        public List<MasodfokuFegyelmiHatarozatViewModel> GetMasodfokuFegyelmiHatarozatNyomtatvanyokByNaplok(List<int> naploIds)
        {
            KonasoftBVFonixContext.Configuration.LazyLoadingEnabled = false;
            NaploViewModel naplo = null;
            Szemelyzet donteshozo = null;
            FegyelmiUgy fegyelmiUgy;
            IktatottDokumentumokViewModel iktatas;
            FogvatartottViewModel fogvatartott;
            int aktualisNaploId = 0;
            MasodfokuFegyelmiHatarozatViewModel model;
            List<MasodfokuFegyelmiHatarozatViewModel> results = new List<MasodfokuFegyelmiHatarozatViewModel>();

            //using (DbContextTransaction transaction = KonasoftBVFonixContext.Database.BeginTransaction())
            //{
                try
                {
                    foreach (int naploId in naploIds)
                    {
                        aktualisNaploId = naploId;
                        naplo = NaploFunctions
                            .FindById(naploId);

                        donteshozo = SzemelyzetFunctions.Table.AsNoTracking()
                            .Include(i => i.Rendfokozat)
                            .Include(i => i.Beosztas)
                            .SingleOrDefault(x => x.Id == naplo.DonteshozoSzemelyId.Value);

                        fegyelmiUgy = Table
                            .AsNoTracking()
                            .Include(x => x.Intezet)
                            .Include(x => x.Intezet.CimHelyseg)
                            .Include(x => x.StatuszCimke)
                            .SingleOrDefault(s => s.Id == naplo.FegyelmiUgyId);

                        fogvatartott = FogvatartottFunctions.GetSzemelyesAdatokElhelyezessel(fegyelmiUgy.FogvatartottId);

                        // megnézzük van-e iktatás már, ha nincs újat generálunk

                        var iktatasKereses = IktatottDokumentumokFunctions.Table.FirstOrDefault(f => f.NaploId == naploId && f.DokumentumTipusCimkeId == (int)IktatottDokumentumTipusok.MasodfokuHatarozat);

                        if (iktatasKereses == null)
                        {
                            IktatottDokumentumokViewModel ujIktatas = new IktatottDokumentumokViewModel()
                            {
                                FegyelmiUgyId = naplo.FegyelmiUgyId,
                                Alszam = 1,
                                AktivFl = true,
                                DokumentumTipusCimkeId = (int)IktatottDokumentumTipusok.MasodfokuHatarozat,
                                NaploId = naploId,
                            };

                            IktatottDokumentumokFunctions.CreateNyomtatvany(ujIktatas);

                            iktatas = (IktatottDokumentumokViewModel)KonasoftBVFonixContext.IktatottDokumentumok.Single(x => x.Id == ujIktatas.Id);
                        }
                        else
                            iktatas = (IktatottDokumentumokViewModel)iktatasKereses;

                        if (naplo != null)
                        {

                            model = new MasodfokuFegyelmiHatarozatViewModel()
                            {
                                FegyelmiUgyId = fegyelmiUgy.Id,
                                Iktatoszam = $"{fegyelmiUgy.UgySorszamaIntezetAzon}/{fegyelmiUgy.UgySorszamaEv}/{fegyelmiUgy.UgySorszama}/{iktatas.Alszam}",
                                Fogvatartott = $"{fogvatartott.FogvatartottSzemelyesAdatai.SzuletesiCsaladiNev} {fogvatartott.FogvatartottSzemelyesAdatai.SzuletesiUtonev}",
                                FogvatartottSzulIdeje = fogvatartott.FogvatartottSzemelyesAdatai.SzuletesiDatum.ToString("yyyy. MM. dd."),
                                FogvatartottAnyjaNeve = fogvatartott.FogvatartottSzemelyesAdatai.AnyjaNeve,
                                FogvatartottSzulHelye = fogvatartott.FogvatartottSzemelyesAdatai.SzuletesiHelyNeve,
                                FogvAzon = fogvatartott.NyilvantartasiAzonosito,
                                BvFok = fogvatartott.FogvatartasJellegeKszId != 0 ? KodszotarFunctions.FindById(fogvatartott.FogvatartasJellegeKszId).Nev : "",
                                FogvSzabadul = fogvatartott.AktualisSzabadulasDatuma?.ToString("yyyy. MM. dd.") ?? "-",
                                HatarozatDatum = $"{naplo.LetrehozasDatuma.ToString("yyyy. MM. dd.")}",
                                HatarozatHely = $"{fegyelmiUgy.Intezet.CimHelyseg?.Nev}" ?? "-",
                                HatarozathozoNev = donteshozo?.Nev ?? "",
                                HatarozathozoRang = donteshozo?.RendfokozatKszId == 0 ? "" : (donteshozo?.Rendfokozat?.Nev ?? ""),
                                HatarozathozoSzam = fegyelmiUgy.FenyitesTipusCimkeId == (int)CimkeEnums.FenyitesTipusok.Megszuntetes ? "" : donteshozo?.Azonosito ?? "",
                                HatarozathozoTitulus = donteshozo?.BeosztasKszId == 0 ? "" : (donteshozo?.Beosztas?.Nev ?? ""),
                                IndoklasText = naplo.JegyzokonyvTartalma,
                                IntezetNev = fegyelmiUgy.Intezet.Nev,
                                FegyelmiUgy = naplo.FenyitesTipusCimkeId == (int)FenyitesTipusok.Maganelzaras ? "felülvizsgálati kérelem" : "panasz",
                                FegyVetseg = CimkeFunctions.FindById(naplo.FegyelmiVetsegTipusaCimkeId.Value).Nev ?? "-",
                                FenyIdotart = $"{naplo.FenyitesHossza?.ToString() ?? "-"} {(naplo.FenyitesHosszaMennyisegiEgysegCimkeId != null ? CimkeFunctions.FindById(naplo.FenyitesHosszaMennyisegiEgysegCimkeId.Value).Nev.ToLower() : "")}",
                                KietkezesCsokkentes = naplo.KietkezesCsokkentes != null ? $", {naplo.KietkezesCsokkentes.Value.ToString()} ({SzamValtasaBeture(naplo.KietkezesCsokkentes.Value)}) %" : "",
                                FenyTipus = CimkeFunctions.FindById(naplo.FenyitesTipusCimkeId.Value).Nev ?? "-",
                                Elhelyezes = fogvatartott.IntezetiObjektum != null ? $"{fogvatartott.IntezetiObjektum.Nev}/{fogvatartott.Korlet.Nev}/{fogvatartott.Zarka.Azonosito}" : "",
                                //Torvenyszek = naplo.TorvenyszekId.HasValue ? TorvenyszekFunctions.FindById(naplo.TorvenyszekId.Value).TorvenyszekNeve : "",
                                TartozkodasFokaJogcime = fogvatartott.VegrehajtasiFokKszId != 0 ? KodszotarFunctions.FindById(fogvatartott.VegrehajtasiFokKszId).Nev : ""
                            };
                        }
                        else
                        {
                            Log.Info("Napló nem található: " + naploId);
                            throw new WarningException("A napló nem található.");
                        }

                        iktatas.DokumentumAdat = IktatottDokumentumokFunctions.SerializeIktatottNyomtatvanyAdat(model);
                        IktatottDokumentumokFunctions.Modify(iktatas);
                        results.Add(model);
                        KonasoftBVFonixContext.SaveChanges();
                        //transaction.Commit();
                    }
                }
                catch (Exception e)
                {

                    Log.Error($"Hiba a dokumentum iktatása során (naplóId: {aktualisNaploId})", e);
                    //transaction.Rollback();
                    throw;
                }
            //}

            return results;
        }

        public List<MasodfokuFegyelmiHatarozatViewModel> GetMasodfokuFegyelmiHatarozatNyomtatvanyokByIktatasok(List<int> iktatasIds)
        {
            KonasoftBVFonixContext.Configuration.LazyLoadingEnabled = false;
            IktatottDokumentumokViewModel iktatas;
            int aktualisIktatasId = 0;
            MasodfokuFegyelmiHatarozatViewModel model;
            List<MasodfokuFegyelmiHatarozatViewModel> results = new List<MasodfokuFegyelmiHatarozatViewModel>();

            try
            {
                foreach (var iktatasId in iktatasIds)
                {
                    aktualisIktatasId = iktatasId;
                    iktatas = (IktatottDokumentumokViewModel)IktatottDokumentumokFunctions.Table.FirstOrDefault(f => f.Id == iktatasId);
                    model = IktatottDokumentumokFunctions.DeserializeIktatottNyomtatvanyAdat<MasodfokuFegyelmiHatarozatViewModel>(iktatas.DokumentumAdat);
                    results.Add(model);
                }
            }
            catch (Exception e)
            {

                Log.Error($"Hiba az iktatás betöltése során (iktatasId: {aktualisIktatasId})", e);
                throw;
            }

            return results;
        }

        public List<MasodfokuFegyelmiHatarozatViewModel> GetMasodfokuFegyelmiHatarozatNyomtatvanyokByFegyelmiUgyIds(List<int> fegyelmiUgyIds)
        {
            KonasoftBVFonixContext.Configuration.LazyLoadingEnabled = false;
            int aktualisIktatasId = 0;
            MasodfokuFegyelmiHatarozatViewModel model;
            List<MasodfokuFegyelmiHatarozatViewModel> results = new List<MasodfokuFegyelmiHatarozatViewModel>();

            try
            {
                foreach (var fegyelmiUgyId in fegyelmiUgyIds)
                {
                    aktualisIktatasId = fegyelmiUgyId;
                    model = GetMasodfokuArchivHatarozat(fegyelmiUgyId);
                    results.Add(model);
                }
            }
            catch (Exception e)
            {

                Log.Error($"Hiba az iktatás betöltése során (iktatasId: {aktualisIktatasId})", e);
                throw;
            }

            return results;
        }

        public List<SzembesitesiJegyzokonyvViewModel> GetSzembesitesiJegyzokonyvNyomtatvanyokByNaplok(List<int> naploIds)
        {
            KonasoftBVFonixContext.Configuration.LazyLoadingEnabled = false;
            NaploViewModel naplo = null;
            Szemelyzet jegyzokonyvVezeto = null;
            FegyelmiUgy fegyelmiUgy;
            IktatottDokumentumokViewModel iktatas;
            FogvatartottViewModel fogvatartott;
            int aktualisNaploId = 0;
            SzembesitesiJegyzokonyvViewModel model;
            FogvatartottViewModel elsoFogvSzemAdat;
            FogvatartottViewModel masodikFogvSzemAdat;
            List<SzembesitesiJegyzokonyvViewModel> results = new List<SzembesitesiJegyzokonyvViewModel>();

            //using (DbContextTransaction transaction = KonasoftBVFonixContext.Database.BeginTransaction())
            //{
                try
                {
                    foreach (int naploId in naploIds)
                    {
                        aktualisNaploId = naploId;
                        naplo = NaploFunctions
                            .FindById(naploId);

                        jegyzokonyvVezeto = SzemelyzetFunctions.Table.AsNoTracking()
                            .Include(i => i.Rendfokozat)
                            .SingleOrDefault(x => x.Id == naplo.JegyzokonyvVezetoSzemelyId.Value);

                        fegyelmiUgy = Table
                            .AsNoTracking()
                            .Include(x => x.Intezet)
                            .SingleOrDefault(s => s.Id == naplo.FegyelmiUgyId);

                        elsoFogvSzemAdat = FogvatartottFunctions.GetSzemelyesAdatokElhelyezessel(naplo.Szembesitett1FogvatartottId.Value);
                        masodikFogvSzemAdat = FogvatartottFunctions.GetSzemelyesAdatokElhelyezessel(naplo.Szembesitett2FogvatartottId.Value);

                        fogvatartott = FogvatartottFunctions.GetSzemelyesAdatokElhelyezessel(fegyelmiUgy.FogvatartottId);

                        // megnézzük van-e iktatás már, ha nincs újat generálunk

                        var iktatasKereses = IktatottDokumentumokFunctions.Table.FirstOrDefault(f => f.NaploId == naploId && f.DokumentumTipusCimkeId == (int)IktatottDokumentumTipusok.SzembesitesiJegyzokonyv);

                        if (iktatasKereses == null)
                        {
                            IktatottDokumentumokViewModel ujIktatas = new IktatottDokumentumokViewModel()
                            {
                                FegyelmiUgyId = naplo.FegyelmiUgyId,
                                Alszam = 1,
                                AktivFl = true,
                                DokumentumTipusCimkeId = (int)IktatottDokumentumTipusok.SzembesitesiJegyzokonyv,
                                NaploId = naploId,
                            };

                            IktatottDokumentumokFunctions.CreateNyomtatvany(ujIktatas);

                            iktatas = (IktatottDokumentumokViewModel)KonasoftBVFonixContext.IktatottDokumentumok.Single(x => x.Id == ujIktatas.Id);
                        }
                        else
                            iktatas = (IktatottDokumentumokViewModel)iktatasKereses;

                        if (naplo != null)
                        {
                            model = new SzembesitesiJegyzokonyvViewModel()
                            {
                                FegyelmiUgyId = fegyelmiUgy.Id,
                                UgySzam = $"{fegyelmiUgy.UgySorszamaIntezetAzon}/{fegyelmiUgy.UgySorszamaEv}/{fegyelmiUgy.UgySorszama}",
                                Iktatoszam = $"{fegyelmiUgy.UgySorszamaIntezetAzon}/{fegyelmiUgy.UgySorszamaEv}/{fegyelmiUgy.UgySorszama}/{iktatas.Alszam}",
                                ElsoSzembesitett = $"{elsoFogvSzemAdat.FogvatartottSzemelyesAdatai.SzuletesiCsaladiNev} {elsoFogvSzemAdat.FogvatartottSzemelyesAdatai.SzuletesiUtonev}",
                                ElsoSzembesitettSzulIdeje = elsoFogvSzemAdat.FogvatartottSzemelyesAdatai.SzuletesiDatum.ToString("yyyy. MM. dd."),
                                ElsoSzembesitettAnyja = elsoFogvSzemAdat.FogvatartottSzemelyesAdatai.AnyjaNeve,
                                ElsoSzembesitettSzulHelye = elsoFogvSzemAdat.FogvatartottSzemelyesAdatai.SzuletesiHelyNeve,
                                ElsoSzembesitettAzon = elsoFogvSzemAdat.NyilvantartasiAzonosito,
                                ElsoSzembesitettTipus = naplo.Szembesitett1FogvatartottId == naplo.FogvatartottId ? "Eljárás alá vont" : "Tanú",
                                MasodikSzembesitett = $"{masodikFogvSzemAdat.FogvatartottSzemelyesAdatai.SzuletesiCsaladiNev} {masodikFogvSzemAdat.FogvatartottSzemelyesAdatai.SzuletesiUtonev}",
                                MasodikSzembesitettSzulIdeje = masodikFogvSzemAdat.FogvatartottSzemelyesAdatai.SzuletesiDatum.ToString("yyyy. MM. dd."),
                                MasodikSzembesitettAnyja = masodikFogvSzemAdat.FogvatartottSzemelyesAdatai.AnyjaNeve,
                                MasodikSzembesitettSzulHelye = masodikFogvSzemAdat.FogvatartottSzemelyesAdatai.SzuletesiHelyNeve,
                                MasodikSzembesitettAzon = masodikFogvSzemAdat.NyilvantartasiAzonosito,
                                MasodikSzembesitettTipus = naplo.Szembesitett2FogvatartottId == naplo.FogvatartottId ? "Eljárás alá vont" : "Tanú",
                                EgyebJelenlevo = naplo.TovabbiJelenlevok ?? "",
                                JegyzokonyvText = naplo.JegyzokonyvTartalma,
                                JegyzokonyvVezeto = $"{jegyzokonyvVezeto?.Nev}{(jegyzokonyvVezeto?.RendfokozatKszId == 0 ? "" : " " + jegyzokonyvVezeto?.Rendfokozat?.Nev)}",
                                Meghallgato = naplo.MeghallgatoSzemelyId != null ? SzemelyzetFunctions.FindById(naplo.MeghallgatoSzemelyId.Value).Nev : "-",
                                IntezetNev = fegyelmiUgy.Intezet.Nev,
                                Ev = naplo.LetrehozasDatuma.Year,
                                Honap = naplo.LetrehozasDatuma.Month,
                                Nap = naplo.LetrehozasDatuma.Day,
                                Ora = naplo.LetrehozasDatuma.Hour,
                                Perc = naplo.LetrehozasDatuma.Minute,
                            };
                        }
                        else
                        {
                            Log.Info("Napló nem található: " + naploId);
                            throw new WarningException("A napló nem található.");
                        }

                        iktatas.DokumentumAdat = IktatottDokumentumokFunctions.SerializeIktatottNyomtatvanyAdat(model);
                        IktatottDokumentumokFunctions.Modify(iktatas);
                        results.Add(model);
                        KonasoftBVFonixContext.SaveChanges();
                        //transaction.Commit();
                    }
                }
                catch (Exception e)
                {

                    Log.Error($"Hiba a dokumentum iktatása során (naplóId: {aktualisNaploId})", e);
                    //transaction.Rollback();
                    throw;
                }
            //}

            return results;
        }

        public List<SzembesitesiJegyzokonyvViewModel> GetSzembesitesiJegyzokonyvNyomtatvanyokByIktatasok(List<int> iktatasIds)
        {
            KonasoftBVFonixContext.Configuration.LazyLoadingEnabled = false;
            IktatottDokumentumokViewModel iktatas;
            int aktualisIktatasId = 0;
            SzembesitesiJegyzokonyvViewModel model;
            List<SzembesitesiJegyzokonyvViewModel> results = new List<SzembesitesiJegyzokonyvViewModel>();

            try
            {
                foreach (var iktatasId in iktatasIds)
                {
                    aktualisIktatasId = iktatasId;
                    iktatas = (IktatottDokumentumokViewModel)IktatottDokumentumokFunctions.Table.FirstOrDefault(f => f.Id == iktatasId);
                    model = IktatottDokumentumokFunctions.DeserializeIktatottNyomtatvanyAdat<SzembesitesiJegyzokonyvViewModel>(iktatas.DokumentumAdat);
                    results.Add(model);
                }
            }
            catch (Exception e)
            {

                Log.Error($"Hiba az iktatás betöltése során (iktatasId: {aktualisIktatasId})", e);
                throw;
            }

            return results;
        }
        
        public List<MaganelzarasMegkezdeseNyomtatasViewModel> GetMaganelzarasMegkezdesenekRogziteseNyomtatasByFegyelmiUgyIds(List<int> fegyelmiUgyIds)
        {
            KonasoftBVFonixContext.Configuration.LazyLoadingEnabled = false;
            //NaploViewModel naplo = null;
            FegyelmiUgy fegyelmiUgy;
            IktatottDokumentumokViewModel iktatas;
            FogvatartottViewModel fogvatartott;
            int aktualisFegyelmiUgyId = 0;
            MaganelzarasMegkezdeseNyomtatasViewModel model;
            List<MaganelzarasMegkezdeseNyomtatasViewModel> results = new List<MaganelzarasMegkezdeseNyomtatasViewModel>();

            //using (DbContextTransaction transaction = KonasoftBVFonixContext.Database.BeginTransaction())
            //{
                try
                {
                    foreach (int fegyelmiUgyId in fegyelmiUgyIds)
                    {
                        aktualisFegyelmiUgyId = fegyelmiUgyId;
                        //naplo = NaploFunctions.FindById(fegyelmiUgyId);

                        fegyelmiUgy = Table // átnézni
                            .AsNoTracking()
                            .Include(x => x.Intezet)
                            .Include(x => x.Intezet.CimHelyseg)
                            .SingleOrDefault(s => s.Id == fegyelmiUgyId);

                        fogvatartott = FogvatartottFunctions.GetSzemelyesAdatokElhelyezessel(fegyelmiUgy.FogvatartottId);

                        // megnézzük van-e iktatás már, ha nincs újat generálunk

                        var iktatasKereses = IktatottDokumentumokFunctions.Table.FirstOrDefault(f => f.FegyelmiUgyId == fegyelmiUgyId && f.DokumentumTipusCimkeId == (int)IktatottDokumentumTipusok.MaganelzarasMegkezdese);

                        if (iktatasKereses == null)
                        {
                            IktatottDokumentumokViewModel ujIktatas = new IktatottDokumentumokViewModel()
                            {
                                FegyelmiUgyId = fegyelmiUgyId,
                                Alszam = 1,
                                AktivFl = true,
                                DokumentumTipusCimkeId = (int)IktatottDokumentumTipusok.MaganelzarasMegkezdese,
                                //NaploId = fegyelmiUgyId
                            };

                            IktatottDokumentumokFunctions.CreateNyomtatvany(ujIktatas);

                            iktatas = (IktatottDokumentumokViewModel)KonasoftBVFonixContext.IktatottDokumentumok.Single(x => x.Id == ujIktatas.Id);
                        }
                        else
                            iktatas = (IktatottDokumentumokViewModel)iktatasKereses;

                        if (fegyelmiUgy != null)
                        {
                            model = new MaganelzarasMegkezdeseNyomtatasViewModel()
                            {
                                IntezetNev = JogosultsagCacheFunctions.AktualisIntezet.Nev,
                                Iktatoszam = $"{fegyelmiUgy.UgySorszamaIntezetAzon}/{fegyelmiUgy.UgySorszamaEv}/{fegyelmiUgy.UgySorszama}/{iktatas.Alszam}",
                                UgySzam = $"{fegyelmiUgy.UgySorszamaIntezetAzon}/{fegyelmiUgy.UgySorszamaEv}/{fegyelmiUgy.UgySorszama}",
                                Fogvatartott = $"{fogvatartott.FogvatartottSzemelyesAdatai.SzuletesiCsaladiNev} {fogvatartott.FogvatartottSzemelyesAdatai.SzuletesiUtonev}",
                                FogvAzon = fogvatartott.NyilvantartasiAzonosito,
                                SzulDatum = fogvatartott.FogvatartottSzemelyesAdatai.SzuletesiDatum.ToString("yyyy. MM. dd.")
                            };

                        }
                        else
                        {
                            Log.Info("Fegyelmi ügy nem található: " + fegyelmiUgyId);
                            throw new WarningException("A fegyelmi ügy nem található.");
                        }

                        iktatas.DokumentumAdat = IktatottDokumentumokFunctions.SerializeIktatottNyomtatvanyAdat(model);
                        IktatottDokumentumokFunctions.Modify(iktatas);
                        results.Add(model);
                        KonasoftBVFonixContext.SaveChanges();
                        //transaction.Commit();
                    }
                }
                catch (Exception e)
                {

                    Log.Error($"Hiba a dokumentum iktatása során (naplóId: {aktualisFegyelmiUgyId})", e);
                    //transaction.Rollback();
                    throw;
                }
            //}

            return results;
        }

        public List<MaganelzarasMegkezdeseNyomtatasViewModel> GetMaganelzarasMegkezdesenekRogziteseNyomtatasByNaploIds(List<int> naploIds)
        {
            KonasoftBVFonixContext.Configuration.LazyLoadingEnabled = false;
            NaploViewModel naplo = null;
            FegyelmiUgy fegyelmiUgy;
            IktatottDokumentumokViewModel iktatas;
            FogvatartottViewModel fogvatartott;
            int aktualisNaploId = 0;
            MaganelzarasMegkezdeseNyomtatasViewModel model;
            List<MaganelzarasMegkezdeseNyomtatasViewModel> results = new List<MaganelzarasMegkezdeseNyomtatasViewModel>();

            //using (DbContextTransaction transaction = KonasoftBVFonixContext.Database.BeginTransaction())
            //{
                try
                {
                    foreach (int naploId in naploIds)
                    {
                        aktualisNaploId = naploId;
                        naplo = NaploFunctions.FindById(naploId);

                        fegyelmiUgy = Table // átnézni
                            .AsNoTracking()
                            .Include(x => x.Intezet)
                            .Include(x => x.Intezet.CimHelyseg)
                            .SingleOrDefault(s => s.Id == naplo.FegyelmiUgyId);

                        fogvatartott = FogvatartottFunctions.GetSzemelyesAdatokElhelyezessel(fegyelmiUgy.FogvatartottId);

                        // megnézzük van-e iktatás már, ha nincs újat generálunk

                        var iktatasKereses = IktatottDokumentumokFunctions.Table.FirstOrDefault(f => f.NaploId == naploId && f.DokumentumTipusCimkeId == (int)IktatottDokumentumTipusok.MaganelzarasMegkezdese);

                        if (iktatasKereses == null)
                        {
                            IktatottDokumentumokViewModel ujIktatas = new IktatottDokumentumokViewModel()
                            {
                                FegyelmiUgyId = naplo.FegyelmiUgyId,
                                Alszam = 1,
                                AktivFl = true,
                                DokumentumTipusCimkeId = (int)IktatottDokumentumTipusok.MaganelzarasMegkezdese,
                                NaploId = naploId
                            };

                            IktatottDokumentumokFunctions.CreateNyomtatvany(ujIktatas);

                            iktatas = (IktatottDokumentumokViewModel)KonasoftBVFonixContext.IktatottDokumentumok.Single(x => x.Id == ujIktatas.Id);
                        }
                        else
                            iktatas = (IktatottDokumentumokViewModel)iktatasKereses;

                        if (naplo != null)
                        {
                            model = new MaganelzarasMegkezdeseNyomtatasViewModel()
                            {
                                IntezetNev = JogosultsagCacheFunctions.AktualisIntezet.Nev,
                                Iktatoszam = $"{fegyelmiUgy.UgySorszamaIntezetAzon}/{fegyelmiUgy.UgySorszamaEv}/{fegyelmiUgy.UgySorszama}/{iktatas.Alszam}",
                                UgySzam = $"{fegyelmiUgy.UgySorszamaIntezetAzon}/{fegyelmiUgy.UgySorszamaEv}/{fegyelmiUgy.UgySorszama}",
                                Fogvatartott = $"{fogvatartott.FogvatartottSzemelyesAdatai.SzuletesiCsaladiNev} {fogvatartott.FogvatartottSzemelyesAdatai.SzuletesiUtonev}",
                                FogvAzon = fogvatartott.NyilvantartasiAzonosito,
                                SzulDatum = fogvatartott.FogvatartottSzemelyesAdatai.SzuletesiDatum.ToString("yyyy. MM. dd.")
                            };

                        }
                        else
                        {
                            Log.Info("Napló nem található: " + naploId);
                            throw new WarningException("A napló nem található.");
                        }

                        iktatas.DokumentumAdat = IktatottDokumentumokFunctions.SerializeIktatottNyomtatvanyAdat(model);
                        IktatottDokumentumokFunctions.Modify(iktatas);
                        results.Add(model);
                        KonasoftBVFonixContext.SaveChanges();
                        //transaction.Commit();
                    }
                }
                catch (Exception e)
                {

                    Log.Error($"Hiba a dokumentum iktatása során (naplóId: {aktualisNaploId})", e);
                    //transaction.Rollback();
                    throw;
                }
            //}

            return results;
        }

        public List<MaganelzarasMegkezdeseNyomtatasViewModel> GetMaganelzarasMegkezdesenekRogziteseNyomtatasByIktatasIds(List<int> iktatasIds)
        {
            KonasoftBVFonixContext.Configuration.LazyLoadingEnabled = false;
            IktatottDokumentumokViewModel iktatas;
            int aktualisIktatasId = 0;
            MaganelzarasMegkezdeseNyomtatasViewModel model;
            List<MaganelzarasMegkezdeseNyomtatasViewModel> results = new List<MaganelzarasMegkezdeseNyomtatasViewModel>();

            try
            {
                foreach (var iktatasId in iktatasIds)
                {
                    aktualisIktatasId = iktatasId;
                    iktatas = (IktatottDokumentumokViewModel)IktatottDokumentumokFunctions.Table.FirstOrDefault(f => f.Id == iktatasId);
                    model = IktatottDokumentumokFunctions.DeserializeIktatottNyomtatvanyAdat<MaganelzarasMegkezdeseNyomtatasViewModel>(iktatas.DokumentumAdat);

                    if (model.IntezetNev != JogosultsagCacheFunctions.AktualisIntezet.Nev)
                        model.IntezetNev = JogosultsagCacheFunctions.AktualisIntezet.Nev;

                    results.Add(model);
                }
            }
            catch (Exception e)
            {

                Log.Error($"Hiba az iktatás betöltése során (iktatasId: {aktualisIktatasId})", e);
                throw;
            }

            return results;
        }

        public List<MegallapodasEsFeljegyzesNyomtatasViewModel> GetMegallapodasEsFeljegyzesNyomtatasByNaploIds(List<int> naploIds)
        {
            KonasoftBVFonixContext.Configuration.LazyLoadingEnabled = false;
            Naplo naplo = null;
            FegyelmiUgy fegyelmiUgy;
            IktatottDokumentumokViewModel iktatas;
            FogvatartottViewModel fogvatartott;
            int aktualisNaploId = 0;
            MegallapodasEsFeljegyzesNyomtatasViewModel model;
            List<MegallapodasEsFeljegyzesNyomtatasViewModel> results = new List<MegallapodasEsFeljegyzesNyomtatasViewModel>();

            //using (DbContextTransaction transaction = KonasoftBVFonixContext.Database.BeginTransaction())
            //{
                try
                {
                    foreach (int naploId in naploIds)
                    {
                        aktualisNaploId = naploId;
                        naplo = KonasoftBVFonixContext.Naplo.Include(x => x.RogzitoSzemely).Include(x => x.RogzitoSzemely.Rendfokozat).AsNoTracking().Single(x => x.Id == aktualisNaploId);

                        fegyelmiUgy = Table // átnézni
                            .AsNoTracking()
                            .Include(x => x.Intezet)
                            .Include(x => x.Intezet.CimHelyseg)
                            .Include(x => x.KozvetitoSzemely)
                            .Include(x => x.KozvetitoSzemely.Rendfokozat)
                            .Include(x => x.RogzitoSzemely)
                            .Include(x => x.RogzitoSzemely.Rendfokozat)
                            .SingleOrDefault(s => s.Id == naplo.FegyelmiUgyId);

                        fogvatartott = FogvatartottFunctions.GetSzemelyesAdatokElhelyezessel(fegyelmiUgy.FogvatartottId);
                        var resztvevo = KonasoftBVFonixContext.EsemenyResztvevok.FirstOrDefault(x => x.EsemenyId == fegyelmiUgy.EsemenyId && x.ErintettsegFokaCimkeId == (int)CimkeEnums.ErintettsegFoka.Sertett);

                        FogvatartottViewModel sertett = null;
                        if (resztvevo != null && resztvevo.FogvatartottId != null)
                        {
                            sertett = FogvatartottFunctions.GetSzemelyesAdatokElhelyezessel((int)resztvevo.FogvatartottId);
                        }

                        // megnézzük van-e iktatás már, ha nincs újat generálunk

                        var iktatasKereses = IktatottDokumentumokFunctions.Table.FirstOrDefault(f => f.NaploId == naploId && f.DokumentumTipusCimkeId == (int)IktatottDokumentumTipusok.FeljegyzesMegbeszelesrolMegallapodasNyomtatvany);

                        if (iktatasKereses == null)
                        {
                            IktatottDokumentumokViewModel ujIktatas = new IktatottDokumentumokViewModel()
                            {
                                FegyelmiUgyId = naplo.FegyelmiUgyId,
                                Alszam = 1,
                                AktivFl = true,
                                DokumentumTipusCimkeId = (int)IktatottDokumentumTipusok.FeljegyzesMegbeszelesrolMegallapodasNyomtatvany,
                                NaploId = naploId
                            };

                            IktatottDokumentumokFunctions.CreateNyomtatvany(ujIktatas);

                            iktatas = (IktatottDokumentumokViewModel)KonasoftBVFonixContext.IktatottDokumentumok.Single(x => x.Id == ujIktatas.Id);
                        }
                        else
                            iktatas = (IktatottDokumentumokViewModel)iktatasKereses;

                        if (naplo != null)
                        {
                            var reintegraciostiszt = "";
                            if (!string.IsNullOrWhiteSpace(naplo.TovabbiJelenlevok))
                            {
                                var fegyelmiJogkorGyakorlojaUsers = SzemelyzetFunctions.GetFegyelmiJogkorGyakorlojaAlkalmazottak();
                                var reintUsers = SzemelyzetFunctions.GetFegyelmiReintegraciosTisztiAlkalmazottak();
                                var kozvetito = new List<AdFegyelmiUserItem>();
                                kozvetito.AddRange(reintUsers);
                                kozvetito.AddRange(fegyelmiJogkorGyakorlojaUsers);
                                kozvetito = kozvetito.GroupBy(x => x.Sid).Select(x => x.First()).ToList();
                                reintegraciostiszt = kozvetito.Where(x => x.Sid == naplo.TovabbiJelenlevok).Select(x => x.Displayname + " " + x.Rendfokozat).FirstOrDefault();
                            }
                            model = new MegallapodasEsFeljegyzesNyomtatasViewModel()
                            {
                                IntezetNev = fegyelmiUgy.Intezet.Nev,
                                Iktatoszam = $"{fegyelmiUgy.UgySorszamaIntezetAzon}/{fegyelmiUgy.UgySorszamaEv}/{fegyelmiUgy.UgySorszama}/{iktatas.Alszam}",
                                UgySzam = $"{fegyelmiUgy.UgySorszamaIntezetAzon}/{fegyelmiUgy.UgySorszamaEv}/{fegyelmiUgy.UgySorszama}",
                                EljarasAlaVontFogvatartott = $"{fogvatartott.FogvatartottSzemelyesAdatai.SzuletesiCsaladiNev} {fogvatartott.FogvatartottSzemelyesAdatai.SzuletesiUtonev}",
                                EljarasAlaVontFogvAzon = fogvatartott.NyilvantartasiAzonosito,
                                SertettFogvatartott = sertett != null ? $"{sertett?.FogvatartottSzemelyesAdatai.SzuletesiCsaladiNev} {sertett?.FogvatartottSzemelyesAdatai.SzuletesiUtonev}" : "",
                                SertettFogvAzon = sertett?.NyilvantartasiAzonosito ?? "",
                                EljarasAlaVontkepviselo = naplo.EljarasAlaVontKepviseloje,
                                Sertettkepviselo = naplo.SertettKepviseloje,
                                FegyelmiUgyId = fegyelmiUgy.Id,
                                EljarasAlaVontatErintoKoltsegek = string.IsNullOrWhiteSpace(naplo.VedoCime) ? null : int.Parse(naplo.VedoCime).ToForintFormat(),
                                SertettetErintoKoltsegek = string.IsNullOrWhiteSpace(naplo.VedoNeve) ? null : int.Parse(naplo.VedoNeve).ToForintFormat(),
                                ReintegraciosTiszt = reintegraciostiszt,
                                FeljegyzesMegbeszelesrol = naplo.VedoElerhetosege,
                                Megallapodas = naplo.JegyzokonyvTartalma,
                                Hatarido = naplo.Hatarido.HasValue ? naplo.Hatarido.Value.ToString("yyyy. MM. dd.") : "",
                                Kozvetito = fegyelmiUgy.KozvetitoSzemely != null ? $"{fegyelmiUgy.KozvetitoSzemely.Nev}{(fegyelmiUgy.KozvetitoSzemely.RendfokozatKszId == 0 ? "" : " " + fegyelmiUgy.KozvetitoSzemely.Rendfokozat.Nev)}" : "",
                                FegyelmiJogkorGyakorloja = naplo.RogzitoSzemely.Nev + " " + naplo.RogzitoSzemely.Rendfokozat.Nev,
                                MegallapodasKelte = naplo.LetrehozasDatuma.ToString("yyyy. MM. dd."),
                                MegbeszelesEv = naplo.LetrehozasDatuma.ToString("yyyy."),
                                MegbeszelesHonap = naplo.LetrehozasDatuma.ToString("MM."),
                                MegbeszelesNap = naplo.LetrehozasDatuma.ToString("dd."),
                                MegbeszelesOra = naplo.LetrehozasDatuma.ToString("HH"),
                                MegbeszelesPerc = naplo.LetrehozasDatuma.ToString("mm"),
                            };

                        }
                        else
                        {
                            Log.Info("Napló nem található: " + naploId);
                            throw new WarningException("A napló nem található.");
                        }

                        iktatas.DokumentumAdat = IktatottDokumentumokFunctions.SerializeIktatottNyomtatvanyAdat(model);
                        IktatottDokumentumokFunctions.Modify(iktatas);
                        results.Add(model);
                        KonasoftBVFonixContext.SaveChanges();
                    }
                    //transaction.Commit();
                }
                catch (Exception e)
                {

                    Log.Error($"Hiba a dokumentum iktatása során (naplóId: {aktualisNaploId})", e);
                    //transaction.Rollback();
                    throw;
                }
            //}

            return results;
        }

        public List<MegallapodasEsFeljegyzesNyomtatasViewModel> GetMegallapodasEsFeljegyzesNyomtatasByIktatasIds(List<int> iktatasIds)
        {
            KonasoftBVFonixContext.Configuration.LazyLoadingEnabled = false;
            IktatottDokumentumokViewModel iktatas;
            int aktualisIktatasId = 0;
            MegallapodasEsFeljegyzesNyomtatasViewModel model;
            List<MegallapodasEsFeljegyzesNyomtatasViewModel> results = new List<MegallapodasEsFeljegyzesNyomtatasViewModel>();

            try
            {
                foreach (var iktatasId in iktatasIds)
                {
                    aktualisIktatasId = iktatasId;
                    iktatas = (IktatottDokumentumokViewModel)IktatottDokumentumokFunctions.Table.FirstOrDefault(f => f.Id == iktatasId);
                    model = IktatottDokumentumokFunctions.DeserializeIktatottNyomtatvanyAdat<MegallapodasEsFeljegyzesNyomtatasViewModel>(iktatas.DokumentumAdat);
                    results.Add(model);
                }
            }
            catch (Exception e)
            {

                Log.Error($"Hiba az iktatás betöltése során (iktatasId: {aktualisIktatasId})", e);
                throw;
            }

            return results;
        }

        public List<KozvetitoiEljarasonReszvetelNyomtatasViewModel> KozvetitoiEljarasonReszvetelNyomtatasByNaploIds(List<int> naploIds)
        {
            KonasoftBVFonixContext.Configuration.LazyLoadingEnabled = false;
            Naplo naplo = null;
            FegyelmiUgy fegyelmiUgy;
            IktatottDokumentumokViewModel iktatas;
            FogvatartottViewModel fogvatartott;
            int aktualisNaploId = 0;
            KozvetitoiEljarasonReszvetelNyomtatasViewModel model;
            List<KozvetitoiEljarasonReszvetelNyomtatasViewModel> results = new List<KozvetitoiEljarasonReszvetelNyomtatasViewModel>();

            //using (DbContextTransaction transaction = KonasoftBVFonixContext.Database.BeginTransaction())
            //{
                try
                {
                    foreach (int naploId in naploIds)
                    {
                        aktualisNaploId = naploId;
                        naplo = KonasoftBVFonixContext.Naplo.Include(i => i.RogzitoSzemely).Include(i => i.RogzitoSzemely.Beosztas).AsNoTracking().Single(s => s.Id == aktualisNaploId);



                        fegyelmiUgy = Table // átnézni
                            .AsNoTracking()
                            .Include(x => x.Intezet)
                            .Include(x => x.Intezet.CimHelyseg)
                            .SingleOrDefault(s => s.Id == naplo.FegyelmiUgyId);


                        fogvatartott = FogvatartottFunctions.GetSzemelyesAdatokElhelyezessel(fegyelmiUgy.FogvatartottId);

                        var erintettsegFoka = KonasoftBVFonixContext.EsemenyResztvevok.AsNoTracking().Include(i => i.ErintettsegFoka).
                            FirstOrDefault(f => f.EsemenyId == fegyelmiUgy.EsemenyId && f.FogvatartottId == f.FogvatartottId).ErintettsegFoka;

                        var iktatasKereses = IktatottDokumentumokFunctions.Table.FirstOrDefault(f => f.NaploId == naploId && f.DokumentumTipusCimkeId == (int)IktatottDokumentumTipusok.KozvetitoiEljarasKezdemenyezese);

                        if (iktatasKereses == null)
                        {
                            IktatottDokumentumokViewModel ujIktatas = new IktatottDokumentumokViewModel()
                            {
                                FegyelmiUgyId = naplo.FegyelmiUgyId,
                                Alszam = 1,
                                AktivFl = true,
                                DokumentumTipusCimkeId = (int)IktatottDokumentumTipusok.KozvetitoiEljarasKezdemenyezese,
                                NaploId = naploId
                            };

                            IktatottDokumentumokFunctions.CreateNyomtatvany(ujIktatas);

                            iktatas = (IktatottDokumentumokViewModel)KonasoftBVFonixContext.IktatottDokumentumok.Single(x => x.Id == ujIktatas.Id);
                        }
                        else
                            iktatas = (IktatottDokumentumokViewModel)iktatasKereses;

                        if (naplo != null)
                        {
                            model = new KozvetitoiEljarasonReszvetelNyomtatasViewModel()
                            {
                                IntezetNev = fegyelmiUgy.Intezet.Nev,
                                Iktatoszam = $"{fegyelmiUgy.UgySorszamaIntezetAzon}/{fegyelmiUgy.UgySorszamaEv}/{fegyelmiUgy.UgySorszama}/{iktatas.Alszam}",
                                UgySzam = $"{fegyelmiUgy.UgySorszamaIntezetAzon}/{fegyelmiUgy.UgySorszamaEv}/{fegyelmiUgy.UgySorszama}",
                                Fogvatartott = $"{fogvatartott.FogvatartottSzemelyesAdatai.SzuletesiCsaladiNev} {fogvatartott.FogvatartottSzemelyesAdatai.SzuletesiUtonev}",
                                FogvAzon = fogvatartott.NyilvantartasiAzonosito,
                                SzulDatum = fogvatartott.FogvatartottSzemelyesAdatai.SzuletesiDatum.ToShortDateString(),
                                ReintegraciosTiszt = naplo.RogzitoSzemely.Nev + " " + naplo.RogzitoSzemely.Beosztas.Nev,
                                ErintettsegFoka = erintettsegFoka.Nev,
                                Javaslat = naplo.JegyzokonyvTartalma,
                                Telepules = fegyelmiUgy.Intezet.CimHelyseg.Nev,
                                IktatasDatum = DateTime.Now.Date.ToShortDateString()
                            };

                        }
                        else
                        {
                            Log.Info("Napló nem található: " + naploId);
                            throw new WarningException("A napló nem található.");
                        }

                        iktatas.DokumentumAdat = IktatottDokumentumokFunctions.SerializeIktatottNyomtatvanyAdat(model);
                        IktatottDokumentumokFunctions.Modify(iktatas);
                        results.Add(model);
                        KonasoftBVFonixContext.SaveChanges();
                        //transaction.Commit();
                    }
                }
                catch (Exception e)
                {

                    Log.Error($"Hiba a dokumentum iktatása során (naplóId: {aktualisNaploId})", e);
                    //transaction.Rollback();
                    throw;
                }
            //}

            return results;
        }

        public List<KozvetitoiEljarasonReszvetelNyomtatasViewModel> KozvetitoiEljarasonReszvetelNyomtatasByIktatasIds(List<int> iktatasIds)
        {
            KonasoftBVFonixContext.Configuration.LazyLoadingEnabled = false;
            IktatottDokumentumokViewModel iktatas;
            int aktualisIktatasId = 0;
            KozvetitoiEljarasonReszvetelNyomtatasViewModel model;
            List<KozvetitoiEljarasonReszvetelNyomtatasViewModel> results = new List<KozvetitoiEljarasonReszvetelNyomtatasViewModel>();

            try
            {
                foreach (var iktatasId in iktatasIds)
                {
                    aktualisIktatasId = iktatasId;
                    iktatas = (IktatottDokumentumokViewModel)IktatottDokumentumokFunctions.Table.FirstOrDefault(f => f.Id == iktatasId);
                    model = IktatottDokumentumokFunctions.DeserializeIktatottNyomtatvanyAdat<KozvetitoiEljarasonReszvetelNyomtatasViewModel>(iktatas.DokumentumAdat);
                    results.Add(model);
                }
            }
            catch (Exception e)
            {

                Log.Error($"Hiba az iktatás betöltése során (iktatasId: {aktualisIktatasId})", e);
                throw;
            }

            return results;
        }

        public List<OsszefoglaloJelentesNyomtatasModel> GetOsszefoglalojelentesNyomtatasAdat(int fegyelmiUgyId)
        {
            List<NaploListaViewmodel> naplobejegyzesek = NaploFunctions.GetNaplobejegyzesekByFegyelmiUgyId(fegyelmiUgyId).Where(w => w.InaktivFl != true).OrderBy(o => o.LetrehozasDatuma).ToList(); ;

            var relevansNaplobejegyzesek = new List<OsszefoglaloJelentesNyomtatasModel>();

            naplobejegyzesek.ForEach(naplobejegyzes =>
            {

                if (naplobejegyzes.TipusCimkeId == (int)FegyelmiNaploTipus.EljarasAlaVontMeghallgatasa)
                {
                    var alairtaSzoveg = naplobejegyzes.AlairtaFl == true ? "" : "A fogvatartott még nem írta alá.";
                    relevansNaplobejegyzesek.Add(new OsszefoglaloJelentesNyomtatasModel()
                    {
                        Fejlec = $"{naplobejegyzes.LetrehozasDatuma.ToString("yyyy.MM.dd HH:mm")} Az eljárás alá vont meghallgatási jegyzőkönyve rögzítésre került. {alairtaSzoveg}",
                        JegyzokonyvTartalma = naplobejegyzes.JegyzokonyvTartalma
                    });
                }
                if (naplobejegyzes.TipusCimkeId == (int)FegyelmiNaploTipus.TanuMeghallgatas)
                {
                    var alairtaSzoveg = naplobejegyzes.AlairtaFl == true ? "" : "A fogvatartott még nem írta alá.";
                    relevansNaplobejegyzesek.Add(new OsszefoglaloJelentesNyomtatasModel()
                    {
                        Fejlec = $"{naplobejegyzes.LetrehozasDatuma.ToString("yyyy.MM.dd HH:mm")} {naplobejegyzes.AktNyilvantartasiSzam} {naplobejegyzes.FogvatartottNeve.ToTitleCase()} tanú meghallgatási jegyzőkönyve rögzítésre került. {alairtaSzoveg}",
                        JegyzokonyvTartalma = naplobejegyzes.JegyzokonyvTartalma
                    });
                }
                if (naplobejegyzes.TipusCimkeId == (int)FegyelmiNaploTipus.SzemelyiAllomanyiTanuMeghallgatasa)
                {
                    var alairtaSzoveg = naplobejegyzes.AlairtaFl == true ? "" : "A tanú még nem írta alá.";
                    relevansNaplobejegyzesek.Add(new OsszefoglaloJelentesNyomtatasModel()
                    {
                        Fejlec = $"{naplobejegyzes.LetrehozasDatuma.ToString("yyyy.MM.dd HH:mm")} {naplobejegyzes.SzemelyiAllomanyiTanuSzemely.ToTitleCase()} {naplobejegyzes.SzemelyiAllomanyiTanuRendfokozat} tanú meghallgatási jegyzőkönyve rögzítésre került. {alairtaSzoveg}",
                        JegyzokonyvTartalma = naplobejegyzes.JegyzokonyvTartalma
                    });
                }
                if (naplobejegyzes.TipusCimkeId == (int)FegyelmiNaploTipus.HelysziniSzemle)
                {
                    var alairtaSzoveg = naplobejegyzes.AlairtaFl == true ? "" : "Jelenleg szerkesztés alatt.";
                    relevansNaplobejegyzesek.Add(new OsszefoglaloJelentesNyomtatasModel()
                    {
                        Fejlec = $"{naplobejegyzes.LetrehozasDatuma.ToString("yyyy.MM.dd HH:mm")} {naplobejegyzes.JegyzokonyvVezetoSzemely.ToTitleCase()} {naplobejegyzes.JegyzokonyvVezetoSzemelyRendfokozat} helyszíni szemlét tartott, melynek ideje {naplobejegyzes.JegyzokonyvLezarasDatuma?.ToShortDateString() ?? "nincs kitöltve"}. {alairtaSzoveg}",
                        JegyzokonyvTartalma = naplobejegyzes.JegyzokonyvTartalma
                    });
                }
                if (naplobejegyzes.TipusCimkeId == (int)FegyelmiNaploTipus.SzakteruletiVelemeny)
                {
                    var alairtaSzoveg = naplobejegyzes.AlairtaFl == true ? "" : "Jelenleg szerkesztés alatt.";
                    relevansNaplobejegyzesek.Add(new OsszefoglaloJelentesNyomtatasModel()
                    {
                        Fejlec = $"{naplobejegyzes.LetrehozasDatuma.ToString("yyyy.MM.dd HH:mm")} {naplobejegyzes.JegyzokonyvVezetoSzemely.ToTitleCase()} {naplobejegyzes.JegyzokonyvVezetoSzemelyRendfokozat} szakterületi véleményt készített, melynek ideje {naplobejegyzes.LetrehozasDatuma.ToShortDateString()}. {alairtaSzoveg}",
                        JegyzokonyvTartalma = naplobejegyzes.JegyzokonyvTartalma
                    });
                }
                // Korábbi összefoglaló jelentés nem kell
                //if (naplobejegyzes.TipusCimkeId == (int)FegyelmiNaploTipus.OsszefoglaloJelentes)
                //{
                //    var alairtaSzoveg = naplobejegyzes.AlairtaFl == true ? "" : "Jelenleg szerkesztés alatt.";
                //    relevansNaplobejegyzesek.Add(new OsszefoglaloJelentesNyomtatasModel()
                //    {
                //        Fejlec = $"{naplobejegyzes.LetrehozasDatuma.ToString("yyyy.MM.dd HH:mm")} {naplobejegyzes.RogzitoSzemely.ToTitleCase()} {naplobejegyzes.RogzitoSzemelyRendfokozat} elkészítette az összefoglaló jelentést. {alairtaSzoveg}",
                //        JegyzokonyvTartalma = naplobejegyzes.JegyzokonyvTartalma
                //    });
                //}
            });

            return relevansNaplobejegyzesek;
        }

        public OsszefoglaloJelentesNyomtatasModelTeljes GetOsszefoglalojelentesNyomtatasByNaploId(int naplobejegyzesId)
        {
            KonasoftBVFonixContext.Configuration.LazyLoadingEnabled = false;
            NaploViewModel naplo = null;
            FegyelmiUgy fegyelmiUgy;
            IktatottDokumentumokViewModel iktatas;
            FogvatartottViewModel fogvatartott;
            int aktualisNaploId = 0;
            //OsszefoglaloJelentesNyomtatasModel model;
            OsszefoglaloJelentesNyomtatasModelTeljes result = new OsszefoglaloJelentesNyomtatasModelTeljes();

            //using (DbContextTransaction transaction = KonasoftBVFonixContext.Database.BeginTransaction())
            //{
                try
                {
                    //foreach (int naploId in naploIds)
                    //{
                    aktualisNaploId = naplobejegyzesId;
                    naplo = NaploFunctions.FindById(naplobejegyzesId);

                    List<OsszefoglaloJelentesNyomtatasModel> results = GetOsszefoglalojelentesNyomtatasAdat(naplo.FegyelmiUgyId);
                    fegyelmiUgy = GetFegyelmiUgyEntityById(naplo.FegyelmiUgyId);

                    fogvatartott = FogvatartottFunctions.GetSzemelyesAdatokElhelyezessel(fegyelmiUgy.FogvatartottId);

                    // megnézzük van-e iktatás már, ha nincs újat generálunk

                    var iktatasKereses = IktatottDokumentumokFunctions.Table.FirstOrDefault(f => f.NaploId == naplobejegyzesId && f.DokumentumTipusCimkeId == (int)IktatottDokumentumTipusok.OsszefoglaloJelentes);

                    if (iktatasKereses == null)
                    {
                        IktatottDokumentumokViewModel ujIktatas = new IktatottDokumentumokViewModel()
                        {
                            FegyelmiUgyId = naplo.FegyelmiUgyId,
                            Alszam = 1,
                            AktivFl = true,
                            DokumentumTipusCimkeId = (int)IktatottDokumentumTipusok.OsszefoglaloJelentes,
                            NaploId = naplobejegyzesId
                        };

                        IktatottDokumentumokFunctions.CreateNyomtatvany(ujIktatas);

                        iktatas = (IktatottDokumentumokViewModel)KonasoftBVFonixContext.IktatottDokumentumok.Single(x => x.Id == ujIktatas.Id);
                    }
                    else
                        iktatas = (IktatottDokumentumokViewModel)iktatasKereses;

                    if (naplo != null)
                    {
                        result = new OsszefoglaloJelentesNyomtatasModelTeljes()
                        {
                            Naplok = results,
                            Iktatoszam = $"{fegyelmiUgy.UgySorszamaIntezetAzon}/{fegyelmiUgy.UgySorszamaEv}/{fegyelmiUgy.UgySorszama}/{iktatas.Alszam}",
                            Ugyszam = $"{fegyelmiUgy.UgySorszamaIntezetAzon}/{fegyelmiUgy.UgySorszamaEv}/{fegyelmiUgy.UgySorszama}",
                            IntezetNev = fegyelmiUgy.Intezet.Nev
                        };

                    }
                    else
                    {
                        Log.Info("Napló nem található: " + naplobejegyzesId);
                        throw new WarningException("A napló nem található.");
                    }

                    iktatas.DokumentumAdat = IktatottDokumentumokFunctions.SerializeIktatottNyomtatvanyAdat(result);
                    IktatottDokumentumokFunctions.Modify(iktatas);
                    //results.Add(model);
                    KonasoftBVFonixContext.SaveChanges();
                    //transaction.Commit();
                    //}
                }
                catch (Exception e)
                {

                    Log.Error($"Hiba a dokumentum iktatása során (naplóId: {aktualisNaploId})", e);
                    //transaction.Rollback();
                    throw;
                }
            //}

            return result;
        }

        public OsszefoglaloJelentesNyomtatasModelTeljes GetOsszefoglalojelentesNyomtatasByIktatasIds(int iktatasId)
        {
            KonasoftBVFonixContext.Configuration.LazyLoadingEnabled = false;
            IktatottDokumentumokViewModel iktatas;
            int aktualisIktatasId = 0;
            //OsszefoglaloJelentesNyomtatasModel model;
            OsszefoglaloJelentesNyomtatasModelTeljes result = new OsszefoglaloJelentesNyomtatasModelTeljes();

            try
            {
                aktualisIktatasId = iktatasId;
                iktatas = (IktatottDokumentumokViewModel)IktatottDokumentumokFunctions.Table.FirstOrDefault(f => f.Id == iktatasId);
                result = IktatottDokumentumokFunctions.DeserializeIktatottNyomtatvanyAdat<OsszefoglaloJelentesNyomtatasModelTeljes>(iktatas.DokumentumAdat);
                //results.Add(model);
            }
            catch (Exception e)
            {

                Log.Error($"Hiba az iktatás betöltése során (iktatasId: {aktualisIktatasId})", e);
                throw;
            }

            return result;
        }

        public OsszefoglaloJelentesDocxNyomtatasModel GetOsszefoglalojelentesDocxNyomtatasByNaploId(int naplobejegyzesId)
        {
            KonasoftBVFonixContext.Configuration.LazyLoadingEnabled = false;
            NaploViewModel naplo = null;
            FegyelmiUgy fegyelmiUgy;
            IktatottDokumentumokViewModel iktatas;
            FogvatartottViewModel fogvatartott;
            int aktualisNaploId = 0;
            //OsszefoglaloJelentesNyomtatasModel model;
            OsszefoglaloJelentesDocxNyomtatasModel result = new OsszefoglaloJelentesDocxNyomtatasModel();

            //using (DbContextTransaction transaction = KonasoftBVFonixContext.Database.BeginTransaction())
            //{
                try
                {
                    //foreach (int naploId in naploIds)
                    //{
                    aktualisNaploId = naplobejegyzesId;
                    naplo = NaploFunctions.FindById(naplobejegyzesId);

                    //List<OsszefoglaloJelentesNyomtatasModel> results = GetOsszefoglalojelentesNyomtatasAdat(naplo.FegyelmiUgyId);
                    fegyelmiUgy = GetFegyelmiUgyEntityById(naplo.FegyelmiUgyId);

                    fogvatartott = FogvatartottFunctions.GetSzemelyesAdatokElhelyezessel(fegyelmiUgy.FogvatartottId);

                    // megnézzük van-e iktatás már, ha nincs újat generálunk

                    var iktatasKereses = IktatottDokumentumokFunctions.Table.FirstOrDefault(f => f.NaploId == naplobejegyzesId && f.DokumentumTipusCimkeId == (int)IktatottDokumentumTipusok.OsszefoglaloJelentesDocx);

                    if (iktatasKereses == null)
                    {
                        IktatottDokumentumokViewModel ujIktatas = new IktatottDokumentumokViewModel()
                        {
                            FegyelmiUgyId = naplo.FegyelmiUgyId,
                            Alszam = 1,
                            AktivFl = true,
                            DokumentumTipusCimkeId = (int)IktatottDokumentumTipusok.OsszefoglaloJelentesDocx,
                            NaploId = naplobejegyzesId
                        };

                        IktatottDokumentumokFunctions.CreateNyomtatvany(ujIktatas);

                        iktatas = (IktatottDokumentumokViewModel)KonasoftBVFonixContext.IktatottDokumentumok.Single(x => x.Id == ujIktatas.Id);
                    }
                    else
                        iktatas = (IktatottDokumentumokViewModel)iktatasKereses;

                    if (naplo != null)
                    {
                        result = new OsszefoglaloJelentesDocxNyomtatasModel()
                        {
                            Naplok = naplo.JegyzokonyvTartalma,
                            Iktatoszam = $"{fegyelmiUgy.UgySorszamaIntezetAzon}/{fegyelmiUgy.UgySorszamaEv}/{fegyelmiUgy.UgySorszama}/{iktatas.Alszam}",
                            Ugyszam = $"{fegyelmiUgy.UgySorszamaIntezetAzon}/{fegyelmiUgy.UgySorszamaEv}/{fegyelmiUgy.UgySorszama}",
                            IntezetNev = fegyelmiUgy.Intezet.Nev,
                            IntezetCime = $"{fegyelmiUgy.Intezet.CimIranyitoszam} {fegyelmiUgy.Intezet.CimHelyseg.Nev}, {fegyelmiUgy.Intezet.CimUtca} {fegyelmiUgy.Intezet.CimHazszam}",
                        };

                    }
                    else
                    {
                        Log.Info("Napló nem található: " + naplobejegyzesId);
                        throw new WarningException("A napló nem található.");
                    }

                    iktatas.DokumentumAdat = IktatottDokumentumokFunctions.SerializeIktatottNyomtatvanyAdat(result);
                    IktatottDokumentumokFunctions.Modify(iktatas);
                    //results.Add(model);
                    KonasoftBVFonixContext.SaveChanges();
                    //transaction.Commit();
                    //}
                }
                catch (Exception e)
                {

                    Log.Error($"Hiba a dokumentum iktatása során (naplóId: {aktualisNaploId})", e);
                    //transaction.Rollback();
                    throw;
                }
            //}

            return result;
        }

        public OsszefoglaloJelentesDocxNyomtatasModel GetOsszefoglalojelentesDocxNyomtatasByIktatasId(int iktatasId)
        {
            KonasoftBVFonixContext.Configuration.LazyLoadingEnabled = false;
            IktatottDokumentumokViewModel iktatas;
            int aktualisIktatasId = 0;
            //OsszefoglaloJelentesNyomtatasModel model;
            OsszefoglaloJelentesDocxNyomtatasModel result = new OsszefoglaloJelentesDocxNyomtatasModel();

            try
            {
                aktualisIktatasId = iktatasId;
                iktatas = (IktatottDokumentumokViewModel)IktatottDokumentumokFunctions.Table.FirstOrDefault(f => f.Id == iktatasId);
                result = IktatottDokumentumokFunctions.DeserializeIktatottNyomtatvanyAdat<OsszefoglaloJelentesDocxNyomtatasModel>(iktatas.DokumentumAdat);

                if (result.IntezetCime == null)
                {
                    var fegyelmiUgy = GetFegyelmiUgyEntityById(iktatas.FegyelmiUgyId);
                    result.IntezetCime = $"{fegyelmiUgy.Intezet.CimIranyitoszam} {fegyelmiUgy.Intezet.CimHelyseg.Nev}, {fegyelmiUgy.Intezet.CimUtca} {fegyelmiUgy.Intezet.CimHazszam}";
                }
                //results.Add(model);
            }
            catch (Exception e)
            {

                Log.Error($"Hiba az iktatás betöltése során (iktatasId: {aktualisIktatasId})", e);
                throw;
            }

            return result;
        }

        public BuntetoFeljelentesDocxNyomtatasModel GetBuntetoFeljelentesDocxNyomtatasByNaploId(int naplobejegyzesId)
        {
            KonasoftBVFonixContext.Configuration.LazyLoadingEnabled = false;
            NaploViewModel naplo = null;
            FegyelmiUgy fegyelmiUgy;
            IktatottDokumentumokViewModel iktatas;
            FogvatartottViewModel fogvatartott;
            int aktualisNaploId = 0;
            BuntetoFeljelentesDocxNyomtatasModel result = new BuntetoFeljelentesDocxNyomtatasModel();

            //using (DbContextTransaction transaction = KonasoftBVFonixContext.Database.BeginTransaction())
            //{
                try
                {
                    aktualisNaploId = naplobejegyzesId;
                    naplo = NaploFunctions.FindById(naplobejegyzesId);

                    fegyelmiUgy = GetFegyelmiUgyEntityById(naplo.FegyelmiUgyId);
                    var esemeny = new EsemenyekFunctions().GetEsemenyById(fegyelmiUgy.EsemenyId);
                    var donteshozo = SzemelyzetFunctions.Table.AsNoTracking()
                            .Include(i => i.Rendfokozat)
                            .SingleOrDefault(x => x.Id == naplo.DonteshozoSzemelyId.Value);

                    fogvatartott = FogvatartottFunctions.GetSzemelyesAdatokElhelyezessel(fegyelmiUgy.FogvatartottId);

                    // megnézzük van-e iktatás már, ha nincs újat generálunk

                    var iktatasKereses = IktatottDokumentumokFunctions.Table.FirstOrDefault(f => f.NaploId == naplobejegyzesId && f.DokumentumTipusCimkeId == (int)IktatottDokumentumTipusok.BuntetoFeljelentes);

                    if (iktatasKereses == null)
                    {
                        IktatottDokumentumokViewModel ujIktatas = new IktatottDokumentumokViewModel()
                        {
                            FegyelmiUgyId = naplo.FegyelmiUgyId,
                            Alszam = 1,
                            AktivFl = true,
                            DokumentumTipusCimkeId = (int)IktatottDokumentumTipusok.BuntetoFeljelentes,
                            NaploId = naplobejegyzesId
                        };

                        IktatottDokumentumokFunctions.CreateNyomtatvany(ujIktatas);

                        iktatas = (IktatottDokumentumokViewModel)KonasoftBVFonixContext.IktatottDokumentumok.Single(x => x.Id == ujIktatas.Id);
                    }
                    else
                        iktatas = (IktatottDokumentumokViewModel)iktatasKereses;

                    if (naplo != null)
                    {
                        result = new BuntetoFeljelentesDocxNyomtatasModel()
                        {
                            Naplo = naplo.JegyzokonyvTartalma,
                            Iktatoszam = $"{fegyelmiUgy.UgySorszamaIntezetAzon}/{fegyelmiUgy.UgySorszamaEv}/{fegyelmiUgy.UgySorszama}/{iktatas.Alszam}",
                            Ugyszam = $"{fegyelmiUgy.UgySorszamaIntezetAzon}/{fegyelmiUgy.UgySorszamaEv}/{fegyelmiUgy.UgySorszama}",
                            IntezetNev = fegyelmiUgy.Intezet.Nev,
                            FogvatartottNev = $"{fogvatartott.FogvatartottSzemelyesAdatai.SzuletesiCsaladiNev} {fogvatartott.FogvatartottSzemelyesAdatai.SzuletesiUtonev}",
                            FogvSzulDatum = fogvatartott.FogvatartottSzemelyesAdatai.SzuletesiDatum.ToShortDateString(),
                            FogvSzulHely = fogvatartott.FogvatartottSzemelyesAdatai.SzuletesiHelyNeve,
                            FogvAnyjaNeve = fogvatartott.FogvatartottSzemelyesAdatai.AnyjaNeve,
                            FegyelmiEsemenyDatum = esemeny.EsemenyDatuma.ToShortDateString(),
                            FegyelmiEsemenyNev = esemeny.Jelleg,
                            IntezetSzekhelyDatum = $"{fegyelmiUgy.Intezet.CimHelyseg.Nev}, {naplo.LetrehozasDatuma.ToShortDateString()}",
                            Ugyintezo = AlkalmazasKontextusFunctions.Kontextus.SzemelyzetNev,
                            IntezetCime = $"{fegyelmiUgy.Intezet.CimIranyitoszam} {fegyelmiUgy.Intezet.CimHelyseg.Nev}, {fegyelmiUgy.Intezet.CimUtca} {fegyelmiUgy.Intezet.CimHazszam}",
                        };
                    }
                    else
                    {
                        Log.Info("Napló nem található: " + naplobejegyzesId);
                        throw new WarningException("A napló nem található.");
                    }

                    iktatas.DokumentumAdat = IktatottDokumentumokFunctions.SerializeIktatottNyomtatvanyAdat(result);
                    IktatottDokumentumokFunctions.Modify(iktatas);
                    KonasoftBVFonixContext.SaveChanges();
                    //transaction.Commit();
                }
                catch (Exception e)
                {
                    Log.Error($"Hiba a dokumentum iktatása során (naplóId: {aktualisNaploId})", e);
                    //transaction.Rollback();
                    throw;
                }
            //}

            return result;
        }

        public BuntetoFeljelentesDocxNyomtatasModel GetBuntetoFeljelentesDocxNyomtatasByIktatasId(int iktatasId)
        {
            KonasoftBVFonixContext.Configuration.LazyLoadingEnabled = false;
            IktatottDokumentumokViewModel iktatas;
            int aktualisIktatasId = 0;
            BuntetoFeljelentesDocxNyomtatasModel result = new BuntetoFeljelentesDocxNyomtatasModel();

            try
            {
                aktualisIktatasId = iktatasId;
                iktatas = (IktatottDokumentumokViewModel)IktatottDokumentumokFunctions.Table.FirstOrDefault(f => f.Id == iktatasId);
                result = IktatottDokumentumokFunctions.DeserializeIktatottNyomtatvanyAdat<BuntetoFeljelentesDocxNyomtatasModel>(iktatas.DokumentumAdat);

                if (result.IntezetCime == null)
                {
                    var fegyelmiUgy = GetFegyelmiUgyEntityById(iktatas.FegyelmiUgyId);
                    result.IntezetCime = $"{fegyelmiUgy.Intezet.CimIranyitoszam} {fegyelmiUgy.Intezet.CimHelyseg.Nev}, {fegyelmiUgy.Intezet.CimUtca} {fegyelmiUgy.Intezet.CimHazszam}";
                }
            }
            catch (Exception e)
            {

                Log.Error($"Hiba az iktatás betöltése során (iktatasId: {aktualisIktatasId})", e);
                throw;
            }

            return result;
        }

        public List<KarjelentoLapDocxNyomtatasModel> GetKarjelentoLapDocxNyomtatasByEsemenyId(int esemenyId, int? fegyelmiUgyId = null)
        {
            KonasoftBVFonixContext.Configuration.LazyLoadingEnabled = false;
            //FegyelmiUgy fegyelmiUgy;
            IktatottDokumentumokViewModel iktatas;
            FogvatartottViewModel fogvatartott;
            int aktualisEsemenyId = 0;

            List<KarjelentoLapDocxNyomtatasModel> results = new List<KarjelentoLapDocxNyomtatasModel>();

            //using (DbContextTransaction transaction = KonasoftBVFonixContext.Database.BeginTransaction())
            //{
                try
                {
                    //foreach (int naploId in naploIds)
                    //{
                    aktualisEsemenyId = esemenyId;
                    //naplo = NaploFunctions.FindById(esemenyId);

                    var esemeny = KonasoftBVFonixContext.Esemenyek
                        .Include(x => x.RogzitoIntezet)
                        .Include(x => x.RogzitoIntezet.CimHelyseg)
                        .Include(x => x.Hely)
                        .Single(x => x.Id == esemenyId);

                    List<FegyelmiUgy> fegyelmiUgyek = new List<FegyelmiUgy>();
                    if (fegyelmiUgyId != null)
                    {
                        var fegyelmiUgy = new FegyelmiUgyFunctions().Table.FirstOrDefault(x => x.Id == fegyelmiUgyId.Value);
                        fegyelmiUgyek.Add(fegyelmiUgy);
                    }
                    else
                        fegyelmiUgyek = new FegyelmiUgyFunctions().GetFegyelmiUgyekByEsemenyId(esemenyId);

                    //List<OsszefoglaloJelentesNyomtatasModel> results = GetOsszefoglalojelentesNyomtatasAdat(naplo.FegyelmiUgyId);
                    //fegyelmiUgy = GetFegyelmiUgyEntityById(naplo.FegyelmiUgyId);
                    foreach (var fegyelmiUgy in fegyelmiUgyek)
                    {
                        KarjelentoLapDocxNyomtatasModel result;
                        fogvatartott = FogvatartottFunctions.GetSzemelyesAdatokElhelyezessel(fegyelmiUgy.FogvatartottId);

                        // megnézzük van-e iktatás már, ha nincs újat generálunk

                        var iktatasKereses = IktatottDokumentumokFunctions.Table.FirstOrDefault(f => f.FegyelmiUgyId == fegyelmiUgy.Id && f.DokumentumTipusCimkeId == (int)IktatottDokumentumTipusok.KarjelentoLap);

                        if (iktatasKereses == null)
                        {
                            IktatottDokumentumokViewModel ujIktatas = new IktatottDokumentumokViewModel()
                            {
                                FegyelmiUgyId = fegyelmiUgy.Id,
                                Alszam = 1,
                                AktivFl = true,
                                DokumentumTipusCimkeId = (int)IktatottDokumentumTipusok.KarjelentoLap,
                                NaploId = null
                            };

                            IktatottDokumentumokFunctions.CreateNyomtatvany(ujIktatas);

                            iktatas = (IktatottDokumentumokViewModel)KonasoftBVFonixContext.IktatottDokumentumok.Single(x => x.Id == ujIktatas.Id);
                        }
                        else
                            iktatas = (IktatottDokumentumokViewModel)iktatasKereses;

                        if (fegyelmiUgy != null)
                        {

                            result = new KarjelentoLapDocxNyomtatasModel()
                            {
                                IntezetNev = esemeny.RogzitoIntezet.Nev,
                                IntezetCim = $"{esemeny.RogzitoIntezet.CimIranyitoszam} {esemeny.RogzitoIntezet.CimHelyseg.Nev}, {esemeny.RogzitoIntezet.CimUtca} {esemeny.RogzitoIntezet.CimHazszam}",
                                Ugyszam = fegyelmiUgy.UgySorszamaIntezetAzon + "/" + fegyelmiUgy.UgySorszamaEv + "/" + fegyelmiUgy.UgySorszama,
                                KaresetHelye = esemeny.RogzitoIntezet.Nev,
                                KaresetIdeje = $"{esemeny.EsemenyDatuma.ToShortDateString()}",
                                FogvatartottNeve = $"{fogvatartott.FogvatartottSzemelyesAdatai.SzuletesiCsaladiNev} {fogvatartott.FogvatartottSzemelyesAdatai.SzuletesiUtonev}",
                                Nytsz = $"{fogvatartott.NyilvantartasiAzonosito}",
                                FogvNevAzon = $"{fogvatartott.FogvatartottSzemelyesAdatai.SzuletesiCsaladiNev} {fogvatartott.FogvatartottSzemelyesAdatai.SzuletesiUtonev} - {fogvatartott.NyilvantartasiAzonosito}",
                                SzulhelyDatum = $"{fogvatartott.FogvatartottSzemelyesAdatai.SzuletesiHelyNeve} {fogvatartott.FogvatartottSzemelyesAdatai.SzuletesiDatum.ToShortDateString()}",
                                AnyjaNeve = fogvatartott.FogvatartottSzemelyesAdatai.AnyjaNeve,
                                Lakcim = $"{fogvatartott.FogvatartottSzemelyesAdatai.AllandoLakcimIranyitoszam} {fogvatartott.FogvatartottSzemelyesAdatai.AllandoLakcimHelysegnev}, " +
                                    $"{fogvatartott.FogvatartottSzemelyesAdatai.AllandoLakcimUtca} {fogvatartott.FogvatartottSzemelyesAdatai.AllandoLakcimHazszam}",
                                KaresetLeirasa = esemeny.Leiras,
                                BejelentoKeltezes = $"{esemeny.RogzitoIntezet.CimHelyseg.Nev}, {esemeny.LetrehozasDatuma.ToShortDateString()}",
                                AtvevoKeltezes = $"{esemeny.RogzitoIntezet.CimHelyseg.Nev}, {esemeny.LetrehozasDatuma.ToShortDateString()}"
                            };
                            

                        }
                        else
                        {
                            Log.Info("Esemény nem található: " + esemenyId);
                            throw new WarningException("Az esemény nem található.");
                        }

                        iktatas.DokumentumAdat = IktatottDokumentumokFunctions.SerializeIktatottNyomtatvanyAdat(result);
                        IktatottDokumentumokFunctions.Modify(iktatas);
                        results.Add(result);
                        KonasoftBVFonixContext.SaveChanges();
                    }
                    //transaction.Commit();
                    //}
                }
                catch (Exception e)
                {

                    Log.Error($"Hiba a dokumentum iktatása során (naplóId: {aktualisEsemenyId})", e);
                    //transaction.Rollback();
                    throw;
                }
            //}

            return results;
        }

        public KarjelentoLapDocxNyomtatasModel GetKarjelentoLapDocxNyomtatasByIktatasId(int iktatasId)
        {
            KonasoftBVFonixContext.Configuration.LazyLoadingEnabled = false;
            IktatottDokumentumokViewModel iktatas;
            int aktualisIktatasId = 0;

            KarjelentoLapDocxNyomtatasModel result = new KarjelentoLapDocxNyomtatasModel();

            try
            {
                aktualisIktatasId = iktatasId;
                iktatas = (IktatottDokumentumokViewModel)IktatottDokumentumokFunctions.Table.FirstOrDefault(f => f.Id == iktatasId);
                result = IktatottDokumentumokFunctions.DeserializeIktatottNyomtatvanyAdat<KarjelentoLapDocxNyomtatasModel>(iktatas.DokumentumAdat);

                if (result.IntezetCim == null)
                {
                    var fegyelmiUgy = GetFegyelmiUgyEntityById(iktatas.FegyelmiUgyId);
                    result.IntezetCim = $"{fegyelmiUgy.Intezet.CimIranyitoszam} {fegyelmiUgy.Intezet.CimHelyseg.Nev}, {fegyelmiUgy.Intezet.CimUtca} {fegyelmiUgy.Intezet.CimHazszam}";
                }
                //results.Add(model);
            }
            catch (Exception e)
            {

                Log.Error($"Hiba az iktatás betöltése során (iktatasId: {aktualisIktatasId})", e);
                throw;
            }

            return result;
        }
    }
}
