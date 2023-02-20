using Edis.Entities.Enums;
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
using Edis.ViewModels.JFK.Dokumentum;
using Edis.ViewModels.JFK.FENY;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Edis.Fenyites.Controllers
{
    [EnableCors]
    [TrackTimeFilter]
    public class DokumentumController : BaseController
    {
        [Inject]
        public IIktatottDokumentumokFunctions IktatottDokumentumokFunctions { get; set; }

        [Inject]
        public INyomtatvanySablonFunction NyomtatvanySablonFunctionJFK { get; set; }

        [Inject]
        public IFegyelmiUgyFunctions FegyelmiUgyFunctions { get; set; }

        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja,
                           Jogosultsagok.Fegyelmi_egyeb_szakterulet,
                           Jogosultsagok.Fegyelmi_fofelugyelo,
                           Jogosultsagok.Fegyelmi_reintegracios_tiszt,
                           Jogosultsagok.Jfk_fegyjutmegtekinto)]
        //[AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.GetDokumentumLetoltesek, DbModositasTortenik = false, FeluletenLathato = true)]
        public JsonResult GetDokumentumLetoltesek(int fegyelmiUgyId)
        {
            var list = IktatottDokumentumokFunctions.GetIktatottDokumentumokTeljes(fegyelmiUgyId).Select(x => new
            {
                x.Alszam,
                x.Id,
                x.FegyelmiUgyId,
                Datum = x.LetrehozasDatuma,
                //Rogzito = x.RogzitoSzemely?.Nev,
                DokumentumTipus = x.DokumentumTipus?.Nev,
                DokumentumTipusCimkeId = x.DokumentumTipus?.Id,
                //Modosito = x.ModositoSzemely?.Nev,
                ModositasDatuma = $"{ x.ModositasDatuma?.ToShortDateString()} {x.ModositasDatuma?.ToShortTimeString()}",
                x.AktivFl,
                AktivSzoveg = x.AktivFl == true ? "Ügyfél mappában" : "Hibás, vagy teszt nyomtatás",
                x.NaploId,
                x.InaktivFl
            }).OrderByDescending(x => x.Alszam);

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.ElkulonitesiLapNyomtatvany, DbModositasTortenik = false, FeluletenLathato = false)]
        public JsonResult ElkulonitesiLapNyomtatvany(int? fegyelmiUgyId, int? iktatasId = null)
        {
            return null;
        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.EljarasAlaVontMeghallgatasDokumentumAdatok, DbModositasTortenik = false, FeluletenLathato = false)] // biztos?
        public JsonResult EljarasAlaVontMeghallgatasDokumentumAdatok(List<int> naplobejegyzesIds, List<int> iktatasIds)
        {
            List<MeghallgatasiJegyzokonyv> data = new List<MeghallgatasiJegyzokonyv>();
            if (naplobejegyzesIds != null) foreach (var naploId in naplobejegyzesIds)
            {
                data.Add(FegyelmiUgyFunctions.EljarasAlaVontMeghallgatasDokumentumAdatok(naploId));
            }
            if (iktatasIds != null) foreach (var iktatasId in iktatasIds)
                {
                    data.Add(FegyelmiUgyFunctions.EljarasAlaVontMeghallgatasDokumentumAdatokByIktatasId(iktatasId));
                }
            return Json(new
            {
                naplobejegyzesIds,
                isTanu = false,
                data
            });
        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.TanuMeghallgatasDokumentumAdatok, DbModositasTortenik = false, FeluletenLathato = false)] // biztos?
        public JsonResult TanuMeghallgatasDokumentumAdatok(List<int> naplobejegyzesIds, List<int> iktatasIds)
        {
            List<MeghallgatasiJegyzokonyv> data = new List<MeghallgatasiJegyzokonyv>();
            if (naplobejegyzesIds != null) foreach (var naploId in naplobejegyzesIds)
            {
                data.Add(FegyelmiUgyFunctions.TanuMeghallgatasDokumentumAdatok(naploId));
            }
            if (iktatasIds != null) foreach (var iktatasId in iktatasIds)
                {
                    data.Add(FegyelmiUgyFunctions.TanuMeghallgatasDokumentumAdatokByIktatasId(iktatasId));
                }
            return Json(new
            {
                naplobejegyzesIds,
                isTanu = true,
                data
            });
        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.SzemelyiAllomanyiTanuMeghallgatasDokumentumAdatok, DbModositasTortenik = false, FeluletenLathato = false)]
        public JsonResult SzemelyiAllomanyiTanuMeghallgatasDokumentumAdatok(List<int> naplobejegyzesIds, List<int> iktatasIds)
        {
            List<MeghallgatasiJegyzokonyv> data = new List<MeghallgatasiJegyzokonyv>();
            if (naplobejegyzesIds != null) foreach (var naploId in naplobejegyzesIds)
            {
                data.Add(FegyelmiUgyFunctions.SzemelyiAllomanyiTanuMeghallgatasDokumentumAdatok(naploId));
            }
            if (iktatasIds != null) foreach (var iktatasId in iktatasIds)
            {
                data.Add(FegyelmiUgyFunctions.SzemelyiAllomanyiTanuMeghallgatasDokumentumAdatokByIktatasId(iktatasId));
            }
            return Json(new
            {
                naplobejegyzesIds,
                isTanu = true,
                data
            });
        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.ElsoFokuTargyalasiDokumentumAdatok, DbModositasTortenik = false, FeluletenLathato = false)]
        public JsonResult ElsoFokuTargyalasiDokumentumAdatok(List<int> naplobejegyzesIds, List<int> iktatasIds)
        {
            List<TargyalasiJegyzokonyv> data = new List<TargyalasiJegyzokonyv>();
            if (naplobejegyzesIds != null) foreach (var naploId in naplobejegyzesIds)
            {
                data.Add(FegyelmiUgyFunctions.ElsoFokuTargyalasiDokumentumAdatok(naploId));
            }
            if (iktatasIds != null) foreach (var iktatasId in iktatasIds)
                {
                    data.Add(FegyelmiUgyFunctions.ElsoFokuTargyalasiDokumentumAdatokByIktatasId(iktatasId));
                }
            return Json(new
            {
                naplobejegyzesIds,
                isTanu = true,
                data
            });
        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.AlairasMegtagadasaNyomtatvany, DbModositasTortenik = false, FeluletenLathato = false)]
        public JsonResult AlairasMegtagadasaNyomtatvany(int? fegyelmiUgyId, int? iktatasId = null)
        {
            return null;
        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.OsszefoglaloJelentesDocxNyomtatvany, DbModositasTortenik = false, FeluletenLathato = false)]
        public JsonResult OsszefoglaloJelentesDocxNyomtatvany(int? naplobejegyzesId, int? iktatasId = null)
        {
            return null;
        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.BuntetoFeljelentesDocxNyomtatvany, DbModositasTortenik = false, FeluletenLathato = false)]
        public JsonResult BuntetoFeljelentesDocxNyomtatvany(int? naplobejegyzesId, int? iktatasId = null)
        {
            return null;
        }

        //[AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.OtosSzamuMellekletDocxNyomtatvany, DbModositasTortenik = false, FeluletenLathato = false)]
        //public JsonResult OtosSzamuMellekletDocxNyomtatvany(int fegyelmiUgyId)
        //{
        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        NyomtatvanySablonFunctionJFK.OtosSzamuMellekletDocxNyomtatvany(fegyelmiUgyId).SaveAs(ms);

        //        return Json(new FileDownloadModel
        //        {
        //            File = ms.ToArray(),
        //            MimeType = System.Net.Mime.MediaTypeNames.Application.Octet,
        //            FileName = "Kártérítési melléklet.docx"
        //        });
        //    }
        //}

        //[AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.FeljelentesNyomtatasDocxNyomtatvany, DbModositasTortenik = false, FeluletenLathato = false)]
        //public JsonResult FeljelentesNyomtatasDocxNyomtatvany(int fegyelmiUgyId)
        //{
        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        NyomtatvanySablonFunctionJFK.KarteritesFeljelentoDocxNyomtatvany(fegyelmiUgyId).SaveAs(ms);

        //        return Json(new FileDownloadModel
        //        {
        //            File = ms.ToArray(),
        //            MimeType = System.Net.Mime.MediaTypeNames.Application.Octet,
        //            FileName = "Kártérítési feljelentés.docx"
        //        });
        //    }
        //}

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.KarjelentoLapDocxNyomtatvany, DbModositasTortenik = false, FeluletenLathato = false)]
        public JsonResult KarjelentoLapDocxNyomtatvany(int? esemenyId, int? iktatasId = null, int? fegyelmiUgyId = null)
        {
            return null;
        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.KarjelentoLapErtesitoLapDocxNyomtatvany, DbModositasTortenik = false, FeluletenLathato = false)]
        public JsonResult KarjelentoLapErtesitoLapDocxNyomtatvany(int esemenyId)
        {
            return null;
        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.ErtesitoLapByEsemenyIdDocxNyomtatvany, DbModositasTortenik = false, FeluletenLathato = false)]
        public JsonResult ErtesitoLapByEsemenyIdDocxNyomtatvany(int esemenyId)
        {
            return null;
        }

    }
}