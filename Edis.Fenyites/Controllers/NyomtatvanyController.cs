using Edis.Entities.Enums;
using Edis.Entities.Enums.Cimke;
using Edis.Fenyites.Controllers.Base;
using Edis.Functions.Base;
using Edis.Functions.Common;
using Edis.Functions.JFK;
using Edis.Functions.JFK.FENY;
using Edis.IoC;
using Edis.IoC.Attributes;
using Edis.Repositories;
using Edis.ViewModels.Base;
using Edis.ViewModels.Common;
using Edis.ViewModels.Fany;
using Edis.ViewModels.JFK.FENY;
using Edis.ViewModels.JFK.Nyomtatvany.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Edis.Entities.Enums.Cimke.CimkeEnums;

namespace Edis.Fenyites.Controllers
{
    [EnableCors]
    [TrackTimeFilter]
    public class NyomtatvanyController : BaseController
    {
        [Inject]
        public INyomtatvanySablonFunction NyomtatvanySablonFunctionJFK { get; set; }

        [Inject]
        public IFanyFegyelmiUgyFunctions FanyFegyelmiUgyFunctions { get; set; }

        [Inject]
        public IFegyelmiUgyFunctions FegyelmiUgyFunctions { get; set; }

        [Inject]
        public IIktatottNyomtatvanyokFunctions IktatottNyomtatvanyokFunctions { get; set; }

        [Inject]
        public IIktatottDokumentumokFunctions IktatottDokumentumokFunctions { get; set; }

        //public JsonResult GetNyomtatvanyok()
        //{
        //    var list = NyomtatvanySablonFunctionJFK.GetFegyelmiNyomtatvanyokList().Select(x => new SelectListItem() {Text = x.NyomtatvanyLeirasa, Value=x.Id.ToString() });

        //    return Json(list, JsonRequestBehavior.AllowGet);
        //}

        //[AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.GetNyomtatvanyLetoltesek, DbModositasTortenik = false, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Jfk_fegyjutmegtekinto)]
        public JsonResult GetNyomtatvanyLetoltesek(int fegyelmiUgyId)
        {
            var list = IktatottNyomtatvanyokFunctions.GetIktatottNyomtatvanyokTeljes(fegyelmiUgyId).Select(x => new
            {
                x.Alszam,
                x.Id,
                Datum = x.LetrehozasDatuma,
                Rogzito = x.RogzitoSzemely.Nev,
                Nyomtatvany = x.Dokumentum.NyomtatvanyLeirasa,
                Modosito = x.ModositoSzemely?.Nev,
                ModositasDatuma = $"{ x.ModositasDatuma?.ToShortDateString()} {x.ModositasDatuma?.ToShortTimeString()}",
                x.AktivFl,
                AktivSzoveg = x.AktivFl == true ? "Ügyfél mappában" : "Hibás, vagy teszt nyomtatás"
            });

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.ChangeIktatottNyomtatvanyStatusz, DbModositasTortenik = false, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Jfk_fegyjutmegtekinto)]
        public JsonResult ChangeIktatottNyomtatvanyStatusz(int iktatottNyomtatvanyId)
        {
            return null;
        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.NyomtatvanyGeneralas, DbModositasTortenik = false, FeluletenLathato = false)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja,
                           Jogosultsagok.Fegyelmi_egyeb_szakterulet,
                           Jogosultsagok.Fegyelmi_fofelugyelo,
                           Jogosultsagok.Fegyelmi_reintegracios_tiszt,
                           Jogosultsagok.Jfk_fegyjutmegtekinto)]
        public FileResult NyomtatvanyGeneralas(int nyomtatvanyId, int fegyelmiUgyId)
        {
            return null;
        }

        //public JsonResult FegyelmiLapNyomtatvany(int fegyelmiUgyId, int? iktatasId)
        //{
        //    FegyelmiLapViewModel result = FegyelmiUgyFunctions.GetFegyelmiLapById(fegyelmiUgyId, iktatasId);

        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult FegyelmiLapNyomtatvanyokEsemenyRogzitesnel(List<int> fegyelmiUgyIds)
        //{
        //    List<FegyelmiLapViewModel> result = FegyelmiUgyFunctions.GetFegyelmiLapokByFegyelmiUgyIds(fegyelmiUgyIds);

        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.FegyelmiLapNyomtatvanyByIktatasId, DbModositasTortenik = false, FeluletenLathato = false)]
        public JsonResult FegyelmiLapNyomtatvanyByIktatasId(int iktatasId)
        {
            return null;

        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.FegyelmiLapNyomtatvanyokForEsemenyRogzites, DbModositasTortenik = false, FeluletenLathato = false)]
        public JsonResult FegyelmiLapNyomtatvanyokForEsemenyRogzites(List<int> fegyelmiUgyIds)
        {
            return null;
        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.TajekoztatoNyomtatvany, DbModositasTortenik = false, FeluletenLathato = false)]
        public JsonResult TajekoztatoNyomtatvany(int fegyelmiUgyId, bool kerem, int? iktatasId)
        {
            return null;
        }

        //public FileResult ErtesitoLapNyomtatvany(int fegyelmiUgyId)
        //{

        //    IktatottDokumentumokViewModel iktatottNyomtatvany = new IktatottDokumentumokViewModel()
        //    {
        //        FegyelmiUgyId = fegyelmiUgyId,
        //        Alszam = 1,
        //        AktivFl = true,
        //    };

        //    var iktatottNyomtatvanyok = IktatottDokumentumokFunctions.CreateNyomtatvany(iktatottNyomtatvany);
        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        NyomtatvanySablonFunctionJFK.ErtesitoLap(fegyelmiUgyId).SaveAs(ms);
        //        return File(ms.ToArray(), System.Net.Mime.MediaTypeNames.Application.Octet, "Értesítő lap.docx");
        //    }

        //}

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.ErtesitoLapNyomtatvany, DbModositasTortenik = false, FeluletenLathato = false)]
        public JsonResult ErtesitoLapNyomtatvany(int fegyelmiUgyId, int? iktatasId=null)
        {
            return null;
        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.FegyelmiEljarastKezdemenyezoNyomtatvany, DbModositasTortenik = false, FeluletenLathato = false)]
        public JsonResult FegyelmiEljarastKezdemenyezoNyomtatvany(int fegyelmiUgyId, int? iktatasId = null)
        {
            return null;
        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.LefoglalasiJegyzokonyvNyomtatvany, DbModositasTortenik = false, FeluletenLathato = false)]
        public JsonResult LefoglalasiJegyzokonyvNyomtatvany(int fegyelmiUgyId, int? iktatasId = null)
        {
            return null;

        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.IratosszesitoFegyelmiUgybenNyomtatvany, DbModositasTortenik = false, FeluletenLathato = false)]
        public JsonResult IratosszesitoFegyelmiUgybenNyomtatvany(int fegyelmiUgyId, int? iktatasId = null)
        {
            return null;

        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.FegyelmiHatarozatNyomtatvany, DbModositasTortenik = false, FeluletenLathato = false)]
        public JsonResult FegyelmiHatarozatNyomtatvany(int fegyelmiUgyId, int? iktatasId = null, int? naploId = null)
        {
            FegyelmiHatarozatViewModel result = FegyelmiUgyFunctions.GetHatarozatById(fegyelmiUgyId, (int)IktatottDokumentumTipusok.FegyelmiHatarozat, iktatasId, naploId);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.FegyelmiHatarozatMegszuntetesNyomtatvany, DbModositasTortenik = false, FeluletenLathato = false)]
        public JsonResult FegyelmiHatarozatMegszuntetesNyomtatvany(int fegyelmiUgyId, int? iktatasId = null, int? naploId = null)
        {
            FegyelmiHatarozatViewModel result = FegyelmiUgyFunctions.GetHatarozatById(fegyelmiUgyId, (int)IktatottDokumentumTipusok.FegyelmiHatarozatMegszuntetese, iktatasId, naploId);

            return Json(result, JsonRequestBehavior.AllowGet);

        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.HatarozatReintegraciosNyomtatvany, DbModositasTortenik = false, FeluletenLathato = false)]
        public JsonResult HatarozatReintegraciosNyomtatvany(int fegyelmiUgyId, int? iktatasId = null, int? naploId = null)
        {
            FegyelmiHatarozatViewModel result = FegyelmiUgyFunctions.GetHatarozatById(fegyelmiUgyId, (int)IktatottDokumentumTipusok.ReintegraciosTisztiHatarozat, iktatasId, naploId);

            return Json(result, JsonRequestBehavior.AllowGet);

        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.KioktatasReintegraciosJogkorbenNyomtatvany, DbModositasTortenik = false, FeluletenLathato = false)]
        public JsonResult KioktatasReintegraciosJogkorbenNyomtatvany(int fegyelmiUgyId, int? iktatasId = null, int? naploId = null)
        {
            KioktatasReintegraciosTisztiJogkorbenViewModel result = FegyelmiUgyFunctions.GetKioktatasNyomtatvanyById(fegyelmiUgyId, iktatasId, naploId);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.VedoKirendeleseNyilatkozatNyomtatvanyByNaploIds, DbModositasTortenik = false, FeluletenLathato = false)]
        public JsonResult VedoKirendeleseNyilatkozatNyomtatvanyByNaploIds(List<int> naplobejegyzesIds)
        {
            List<VedoKirendeleseNyilatkozatViewModel> results = FegyelmiUgyFunctions.GetVedoKirendeleseNyilatkozatNyomtatvanyokByNaplok(naplobejegyzesIds);

            return Json(results, JsonRequestBehavior.AllowGet);
        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.VedoKirendeleseNyilatkozatNyomtatvanyByIktatasIds, DbModositasTortenik = false, FeluletenLathato = false)]
        public JsonResult VedoKirendeleseNyilatkozatNyomtatvanyByIktatasIds(List<int> iktatasIds)
        {
            List<VedoKirendeleseNyilatkozatViewModel> results = FegyelmiUgyFunctions.GetVedoKirendeleseNyilatkozatNyomtatvanyokByIktatasok(iktatasIds);

            return Json(results, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult VedoKirendeleseNyomtatvanyByNaploIds(List<int> naplobejegyzesIds) //1255
        //{
        //    List<VedoKirendeleseViewModel> result = FegyelmiUgyFunctions.GetVedoKirendeleseNyomtatvanyokByNaplok(naplobejegyzesIds);

        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult VedoKirendeleseNyomtatvanyByIktatasIds(List<int> iktatasIds)
        //{
        //    List<VedoKirendeleseViewModel> results = FegyelmiUgyFunctions.GetVedoKirendeleseNyomtatvanyokByIktatasok(iktatasIds);

        //    return Json(results, JsonRequestBehavior.AllowGet);
        //}

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.VedoKirendeleseNyomtatvany, DbModositasTortenik = false, FeluletenLathato = false)]
        public JsonResult VedoKirendeleseNyomtatvany(int? naplobejegyzesId, int? iktatasId = null)
        {
            return null;
        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.FegyemiVedoTelefononTortenoTajekoztatasa, DbModositasTortenik = false, FeluletenLathato = false)]
        public JsonResult FegyemiVedoTelefononTortenoTajekoztatasa(int? naplobejegyzesId, int? iktatasId = null)
        {
            return null;
        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.MeghatalmazottVedoKirendeleseNyilatkozatNyomtatvanyByNaploIds, DbModositasTortenik = false, FeluletenLathato = false)]
        public JsonResult MeghatalmazottVedoKirendeleseNyilatkozatNyomtatvanyByNaploIds(List<int> naplobejegyzesIds)
        {
            List<MeghatalmazottVedoKirendeleseNyilatkozatViewModel> results = FegyelmiUgyFunctions.GetMeghatalmazottVedoKirendeleseNyilatkozatNyomtatvanyokByNaplok(naplobejegyzesIds);

            return Json(results, JsonRequestBehavior.AllowGet);
        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.MeghatalmazottVedoKirendeleseNyilatkozatNyomtatvanyByIktatasIds, DbModositasTortenik = false, FeluletenLathato = false)]
        public JsonResult MeghatalmazottVedoKirendeleseNyilatkozatNyomtatvanyByIktatasIds(List<int> iktatasIds)
        {
            List<MeghatalmazottVedoKirendeleseNyilatkozatViewModel> results = FegyelmiUgyFunctions.GetMeghatalmazottVedoKirendeleseNyilatkozatNyomtatvanyokByIktatasok(iktatasIds);

            return Json(results, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult MeghatalmazottVedoKirendeleseNyomtatvanyByNaploIds(List<int> naplobejegyzesIds) //1256
        //{
        //    List<MeghatalmazottVedoKirendeleseViewModel> result = FegyelmiUgyFunctions.GetMeghatalmazottVedoKirendeleseNyomtatvanyokByNaplok(naplobejegyzesIds);

        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult MeghatalmazottVedoKirendeleseNyomtatvanyByIktatasIds(List<int> iktatasIds)
        //{
        //    List<MeghatalmazottVedoKirendeleseViewModel> results = FegyelmiUgyFunctions.GetMeghatalmazottVedoKirendeleseNyomtatvanyokByIktatasok(iktatasIds);

        //    return Json(results, JsonRequestBehavior.AllowGet);
        //}

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.MeghatalmazottVedoKirendeleseNyomtatvany, DbModositasTortenik = false, FeluletenLathato = false)]
        public JsonResult MeghatalmazottVedoKirendeleseNyomtatvany(int? naplobejegyzesId, int? iktatasId = null)
        {
            return null;
        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.VedoTelefonosTajekoztatasaNyomtatvanyByNaploIds, DbModositasTortenik = false, FeluletenLathato = false)]
        public JsonResult VedoTelefonosTajekoztatasaNyomtatvanyByNaploIds(List<int> naplobejegyzesIds)
        {
            List<VedoTelefonosTajekoztatasaViewModel> result = FegyelmiUgyFunctions.GetVedoTelefonosTajekoztatasaNyomtatvanyokByNaplok(naplobejegyzesIds);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.VedoTelefonosTajekoztatasaNyomtatvanyByIktatasIds, DbModositasTortenik = false, FeluletenLathato = false)]
        public JsonResult VedoTelefonosTajekoztatasaNyomtatvanyByIktatasIds(List<int> iktatasIds)
        {
            List<VedoTelefonosTajekoztatasaViewModel> results = FegyelmiUgyFunctions.GetVedoTelefonosTajekoztatasaNyomtatvanyokByIktatasok(iktatasIds);

            return Json(results, JsonRequestBehavior.AllowGet);
        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.MasodfokuTargyalasiJegyzokonyvNyomtatatvanyByNaploIds, DbModositasTortenik = false, FeluletenLathato = false)]
        public JsonResult MasodfokuTargyalasiJegyzokonyvNyomtatatvanyByNaploIds(List<int> naplobejegyzesIds)
        {
            List<MasodfokuTargyalasiJegyzokonyvViewModel> result = FegyelmiUgyFunctions.GetMasodfokuTargyalasiJegyzokonyvNyomtatvanyokByNaplok(naplobejegyzesIds);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.MasodfokuTargyalasiJegyzokonyvNyomtatatvanyByIktatasIds, DbModositasTortenik = false, FeluletenLathato = false)]
        public JsonResult MasodfokuTargyalasiJegyzokonyvNyomtatatvanyByIktatasIds(List<int> iktatasIds)
        {
            List<MasodfokuTargyalasiJegyzokonyvViewModel> results = FegyelmiUgyFunctions.GetMasodfokuTargyalasiJegyzokonyvNyomtatvanyokByIktatasok(iktatasIds);

            return Json(results, JsonRequestBehavior.AllowGet);
        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.MasodfokuFegyelmiHatarozatMegszunteteseNyomtatvanyByNaploIds, DbModositasTortenik = false, FeluletenLathato = false)]
        public JsonResult MasodfokuFegyelmiHatarozatMegszunteteseNyomtatvanyByNaploIds(List<int> naplobejegyzesIds)
        {
            List<MasodfokuFegyelmiHatarozatMegszunteteseViewModel> result = FegyelmiUgyFunctions.GetMasodfokuFegyelmiHatarozatMegszunteteseNyomtatvanyokByNaplok(naplobejegyzesIds);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.MasodfokuFegyelmiHatarozatMegszunteteseNyomtatvanyByIktatasIds, DbModositasTortenik = false, FeluletenLathato = false)]
        public JsonResult MasodfokuFegyelmiHatarozatMegszunteteseNyomtatvanyByIktatasIds(List<int> iktatasIds)
        {
            List<MasodfokuFegyelmiHatarozatMegszunteteseViewModel> results = FegyelmiUgyFunctions.GetMasodfokuFegyelmiHatarozatMegszunteteseNyomtatvanyokByIktatasok(iktatasIds);

            return Json(results, JsonRequestBehavior.AllowGet);
        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.MasodfokuFegyelmiHatarozatNyomtatvanyByNaploIds, DbModositasTortenik = false, FeluletenLathato = false)]
        public JsonResult MasodfokuFegyelmiHatarozatNyomtatvanyByNaploIds(List<int> naplobejegyzesIds)
        {
            List<MasodfokuFegyelmiHatarozatViewModel> result = FegyelmiUgyFunctions.GetMasodfokuFegyelmiHatarozatNyomtatvanyokByNaplok(naplobejegyzesIds);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.MasodfokuFegyelmiHatarozatNyomtatvanyByIktatasIds, DbModositasTortenik = false, FeluletenLathato = false)]
        public JsonResult MasodfokuFegyelmiHatarozatNyomtatvanyByIktatasIds(List<int> iktatasIds)
        {
            List<MasodfokuFegyelmiHatarozatViewModel> results = FegyelmiUgyFunctions.GetMasodfokuFegyelmiHatarozatNyomtatvanyokByIktatasok(iktatasIds);

            return Json(results, JsonRequestBehavior.AllowGet);
        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.MasodfokuFegyelmiHatarozatNyomtatvanyByFegyelmiUgyIds, DbModositasTortenik = false, FeluletenLathato = false)]
        public JsonResult MasodfokuFegyelmiHatarozatNyomtatvanyByFegyelmiUgyIds(List<int> fegyelmiUgyIds)
        {
            List<MasodfokuFegyelmiHatarozatViewModel> results = FegyelmiUgyFunctions.GetMasodfokuFegyelmiHatarozatNyomtatvanyokByFegyelmiUgyIds(fegyelmiUgyIds);

            return Json(results, JsonRequestBehavior.AllowGet);
        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.SzembesitesiJegyzokonyvNyomtatvanyByNaploIds, DbModositasTortenik = false, FeluletenLathato = false)]
        public JsonResult SzembesitesiJegyzokonyvNyomtatvanyByNaploIds(List<int> naplobejegyzesIds)
        {
            List<SzembesitesiJegyzokonyvViewModel> result = FegyelmiUgyFunctions.GetSzembesitesiJegyzokonyvNyomtatvanyokByNaplok(naplobejegyzesIds);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.SzembesitesiJegyzokonyvNyomtatvanyByIktatasIds, DbModositasTortenik = false, FeluletenLathato = false)]
        public JsonResult SzembesitesiJegyzokonyvNyomtatvanyByIktatasIds(List<int> iktatasIds)
        {
            List<SzembesitesiJegyzokonyvViewModel> results = FegyelmiUgyFunctions.GetSzembesitesiJegyzokonyvNyomtatvanyokByIktatasok(iktatasIds);

            return Json(results, JsonRequestBehavior.AllowGet);
        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.MaganelzarasMegkezdesenekRogziteseNyomtatasByFegyelmiUgyIds, DbModositasTortenik = false, FeluletenLathato = false)]
        public JsonResult MaganelzarasMegkezdesenekRogziteseNyomtatasByFegyelmiUgyIds(List<int> fegyelmiUgyIds)
        {
            List<MaganelzarasMegkezdeseNyomtatasViewModel> result = FegyelmiUgyFunctions.GetMaganelzarasMegkezdesenekRogziteseNyomtatasByFegyelmiUgyIds(fegyelmiUgyIds);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.MaganelzarasMegkezdesenekRogziteseNyomtatasByNaploIds, DbModositasTortenik = false, FeluletenLathato = false)]
        public JsonResult MaganelzarasMegkezdesenekRogziteseNyomtatasByNaploIds(List<int> naplobejegyzesIds)
        {
            List<MaganelzarasMegkezdeseNyomtatasViewModel> result = FegyelmiUgyFunctions.GetMaganelzarasMegkezdesenekRogziteseNyomtatasByNaploIds(naplobejegyzesIds);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.MaganelzarasMegkezdesenekRogziteseNyomtatasByIktatasIds, DbModositasTortenik = false, FeluletenLathato = false)]
        public JsonResult MaganelzarasMegkezdesenekRogziteseNyomtatasByIktatasIds(List<int> iktatasIds)
        {
            List<MaganelzarasMegkezdeseNyomtatasViewModel> results = FegyelmiUgyFunctions.GetMaganelzarasMegkezdesenekRogziteseNyomtatasByIktatasIds(iktatasIds);

            return Json(results, JsonRequestBehavior.AllowGet);
        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.MegallapodasEsFeljegyzesNyomtatasByNaploIds, DbModositasTortenik = false, FeluletenLathato = false)]
        public JsonResult MegallapodasEsFeljegyzesNyomtatasByNaploIds(List<int> naplobejegyzesIds)
        {
            List<MegallapodasEsFeljegyzesNyomtatasViewModel> result = FegyelmiUgyFunctions.GetMegallapodasEsFeljegyzesNyomtatasByNaploIds(naplobejegyzesIds);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.MegallapodasEsFeljegyzesNyomtatasByIktatasIds, DbModositasTortenik = false, FeluletenLathato = false)]
        public JsonResult MegallapodasEsFeljegyzesNyomtatasByIktatasIds(List<int> iktatasIds)
        {
            List<MegallapodasEsFeljegyzesNyomtatasViewModel> results = FegyelmiUgyFunctions.GetMegallapodasEsFeljegyzesNyomtatasByIktatasIds(iktatasIds);

            return Json(results, JsonRequestBehavior.AllowGet);
        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.KozvetitoiEljarasonReszvetelNyomtatasByNaploIds, DbModositasTortenik = false, FeluletenLathato = false)]
        public JsonResult KozvetitoiEljarasonReszvetelNyomtatasByNaploIds(List<int> naplobejegyzesIds)
        {
            List<KozvetitoiEljarasonReszvetelNyomtatasViewModel> result = FegyelmiUgyFunctions.KozvetitoiEljarasonReszvetelNyomtatasByNaploIds(naplobejegyzesIds);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.KozvetitoiEljarasonReszvetelNyomtatasByIktatasIds, DbModositasTortenik = false, FeluletenLathato = false)]
        public JsonResult KozvetitoiEljarasonReszvetelNyomtatasByIktatasIds(List<int> iktatasIds)
        {
            List<KozvetitoiEljarasonReszvetelNyomtatasViewModel> results = FegyelmiUgyFunctions.KozvetitoiEljarasonReszvetelNyomtatasByIktatasIds(iktatasIds);

            return Json(results, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult OsszefoglaloJelentesNyomtatasByFegyelmiUgyId(int fegyelmiUgyId)
        //{
        //    List<OsszefoglaloJelentesNyomtatasModel> results = FegyelmiUgyFunctions.GetOsszefoglalojelentesNyomtatasAdat(fegyelmiUgyId);
        //    var fegyelmiUgy = FegyelmiUgyFunctions.GetFegyelmiUgyEntityById(fegyelmiUgyId);

        //    OsszefoglaloJelentesNyomtatasModelTeljes result = new OsszefoglaloJelentesNyomtatasModelTeljes() {
        //        Naplok = results,
        //        Iktatoszam = $"{fegyelmiUgy.UgySorszamaIntezetAzon}/{fegyelmiUgy.UgySorszamaEv}/{fegyelmiUgy.UgySorszama}",
        //        Ugyszam = $"{fegyelmiUgy.UgySorszamaIntezetAzon}/{fegyelmiUgy.UgySorszamaEv}/{fegyelmiUgy.UgySorszama}",
        //        IntezetNev = fegyelmiUgy.Intezet.Nev
        //    };

        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}
        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.OsszefoglaloJelentesNyomtatasByNaplobejegyzesId, DbModositasTortenik = false, FeluletenLathato = false)]
        public JsonResult OsszefoglaloJelentesNyomtatasByNaplobejegyzesId(int naplobejegyzesId)
        {
            return null;
        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.VedoKirendeleseNyomtatvany, DbModositasTortenik = false, FeluletenLathato = false)]
        public JsonResult OsszefoglaloJelentesNyomtatasByIktatasId(int iktatasId)
        {
            return null;
        }
    }
}