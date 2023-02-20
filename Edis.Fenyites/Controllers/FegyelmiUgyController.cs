using Edis.Diagnostics;
using Edis.Entities.Enums;
using Edis.Entities.Enums.Fenyites;
using Edis.Fenyites.Controllers.Base;
using Edis.Functions.Base;
using Edis.Functions.Common;
using Edis.Functions.Fany;
using Edis.Functions.JFK;
using Edis.Functions.JFK.FENY;
using Edis.IoC.Attributes;
using Edis.Utilities;
using Edis.ViewModels.Base;
using Edis.ViewModels.Fany;
using Edis.ViewModels.JFK;
using Edis.ViewModels.JFK.FENY;
using Edis.ViewModels.JFK.FENY.FormModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using static Edis.Entities.Enums.Cimke.CimkeEnums;

namespace Edis.Fenyites.Controllers
{
    //[SessionState(System.Web.SessionState.SessionStateBehavior.ReadOnly)]
    [EnableCors]
    [TrackTimeFilter]
    public class FegyelmiUgyController : BaseController
    {
        [Inject]
        IFegyelmiUgyFunctions FegyelmiUgyFunctions { get; set; }

        [Inject]
        IAlkalmazasKontextusFunctions AlkalmazasKontextusFunctions { get; set; }

        [Inject]
        ISzemelyzetFunctions SzemelyzetFunctions { get; set; }

        [Inject]
        IActiveDirectoryFunctions ActiveDirectoryFunctions { get; set; }

        [Inject]
        IActiveDirectoryKezeloFunctions ActiveDirectoryKezeloFunctions { get; set; }

        [Inject]
        IJogosultsagCacheFunctions JogosultsagCacheFunctions { get; set; }

        [Inject]
        IEsemenyResztvevoFunctions EsemenyResztvevoFunctions { get; set; }

        [Inject]
        INaploFunctions NaploFunctions { get; set; }

        [Inject]
        IEsemenyekFunctions EsemenyekFunctions { get; set; }

        [Inject]
        IFeltoltesFunctions FeltoltesFunctions { get; set; }

        [Inject]
        ISzakteruletiVelemenyKeresekFunctions SzakteruletiVelemenyKeresekFunctions { get; set; }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.GetFegyelmiUgyek, DbModositasTortenik = false, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Jfk_fegyjutmegtekinto, Jogosultsagok.Fegyelmi_egyeb_szakterulet, Jogosultsagok.Fegyelmi_jogkor_gyakorloja, Jogosultsagok.Fegyelmi_reintegracios_tiszt, Jogosultsagok.Fegyelmi_fofelugyelo)]
        public JsonResult GetFegyelmiUgyek()
        {
            int intezetId = JogosultsagCacheFunctions.AktualisIntezet.Id;
            var fegyelmiUgyek = FegyelmiUgyFunctions.GetFegyelmiUgyek(intezetId);

            return Json(fegyelmiUgyek);
        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.GetFegyelmiUgyekArchiv, DbModositasTortenik = false, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Jfk_fegyjutmegtekinto, Jogosultsagok.Fegyelmi_egyeb_szakterulet, Jogosultsagok.Fegyelmi_jogkor_gyakorloja, Jogosultsagok.Fegyelmi_reintegracios_tiszt, Jogosultsagok.Fegyelmi_fofelugyelo)]
        public JsonResult GetFegyelmiUgyekArchiv(int? ev)
        {
            int intezetId = JogosultsagCacheFunctions.AktualisIntezet.Id;
            int ugyEve = ev ?? DateTime.Now.Year;
            Log.Info($"Archív fegyelmi ügyek {intezetId} intézethez {ugyEve} évre...");
            var fegyelmiUgyek = FegyelmiUgyFunctions.GetFegyelmiUgyekArchiv(intezetId, ugyEve);
            Log.Info($"Archív fegyelmi ügyek {intezetId} intézethez {ugyEve} évre száma: {fegyelmiUgyek.Count}");
            return Json(fegyelmiUgyek);
        }

        [RequirePermission(Jogosultsagok.Jfk_fegyjutmegtekinto, Jogosultsagok.Fegyelmi_egyeb_szakterulet, Jogosultsagok.Fegyelmi_jogkor_gyakorloja, Jogosultsagok.Fegyelmi_reintegracios_tiszt)]
        public JsonResult GetFegyelmiUgyListItemViewModelById(int fegyelmiUgyId)
        {
            int intezetId = JogosultsagCacheFunctions.AktualisIntezet.Id;
            var fegyelmiUgy = FegyelmiUgyFunctions.GetFegyelmiUgyListItemViewModelById(intezetId, fegyelmiUgyId);
            return Json(fegyelmiUgy);
        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.GetFegyelmiUgyDataElrendeleshez, DbModositasTortenik = false, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja)]
        public JsonResult GetFegyelmiUgyDataElrendeleshez(List<int> fegyelmiUgyIds)
        {
            if (fegyelmiUgyIds == null)
            {
                throw new WarningException("Nincs kiválasztva egy ügy sem.");
            }
            bool warning = EsemenyResztvevoFunctions.WarningTanuVagyEszleloByFegyelmiUgyIds(fegyelmiUgyIds);
            if (warning)
            {
                return Json(new { isSuccess = false, message = "Az ügyben Ön észlelőként vagy tanúként van megjelölve, ezért nem végezhet benne vizsgálati cselekményt." });
            }
            var currentEszleloId = AlkalmazasKontextusFunctions.Kontextus.SzemelyzetId;
            var currentEszleloSzemelyzet = SzemelyzetFunctions.FindById(currentEszleloId);
            var currentEszlelo = SzemelyzetFunctions.GetAdFegyelmiUser(currentEszleloSzemelyzet);
            List<AdFegyelmiUserItem> egyszemelyesNemModosithatoLista = new List<AdFegyelmiUserItem>();
            egyszemelyesNemModosithatoLista.Add(currentEszlelo);
            int intezetId = AlkalmazasKontextusFunctions.Kontextus.RogzitoIntezetId;
            var fegyelmiUgy = FegyelmiUgyFunctions.FindById(fegyelmiUgyIds.First());
            var reintegraciosTiszt = FegyelmiUgyFunctions.GetReintegraciosTiszt(fegyelmiUgy.FogvatartottId);
            var esemenyEszeloId = EsemenyekFunctions.FindById(fegyelmiUgy.EsemenyId).EszleloId;
            List<AdFegyelmiUserItem> fegyelmiUsers = SzemelyzetFunctions.GetFegyelmiAlkalmazottak();
            if (currentEszleloId == esemenyEszeloId)
            {
                var isEszeloInList = fegyelmiUsers.Any(a => a.Sid == currentEszlelo.Sid);
                if (isEszeloInList)
                {
                    fegyelmiUsers.RemoveAll(x => x.Sid == currentEszlelo.Sid);
                }
            }
            List<KSelect2ItemModel> szemelyzetSelectList = fegyelmiUsers.Select(x => new KSelect2ItemModel() { id = x.Sid, text = x.Displayname + (x.Rendfokozat == null ? "" : (" " + x.Rendfokozat)) }).ToList();
            List<KSelect2ItemModel> szemelyzetSelectList2 = egyszemelyesNemModosithatoLista.Select(x => new KSelect2ItemModel() { id = x.Sid, text = x.Displayname + (x.Rendfokozat == null ? "" : (" " + x.Rendfokozat)) }).ToList();

            DontesFegyelmiUgyElrendeleserolViewModel result = new DontesFegyelmiUgyElrendeleserolViewModel()
            {
                FegyelmiUgyIds = fegyelmiUgyIds,
                KivizsgalasiHatarido = FegyelmiUgyFunctions.GetFegyelmiHatarido(fegyelmiUgy.Id, false, (int)FegyelmiUgyStatusz.KivizsgalasFolyamatban) ?? DateTime.Now,
                KivizsgaloSzemelyOptions = szemelyzetSelectList,
                KivizsgaloSzemelySid = reintegraciosTiszt?.Sid,
                DontestHozoSzemelyOptions = szemelyzetSelectList2,
                DontestHozoSzemelySid = AlkalmazasKontextusFunctions.Kontextus.SzemelyzetSid,
                JogiKepviseletetKer = false
            };

            return Json(result);
        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.GetFegyelmiUgyDataKivizsgaloModositashoz, DbModositasTortenik = false, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja)]
        public JsonResult GetFegyelmiUgyDataKivizsgaloModositashoz(List<int> fegyelmiUgyIds)
        {
            if (fegyelmiUgyIds == null)
            {
                throw new WarningException("Nincs kiválasztva egy ügy sem.");
            }
            if (fegyelmiUgyIds.Count > 1)
            {
                throw new WarningException("Többes művelet nem engedélyezett");
            }
            bool warning = EsemenyResztvevoFunctions.WarningTanuVagyEszleloByFegyelmiUgyIds(fegyelmiUgyIds);
            if (warning)
            {
                return Json(new { isSuccess = false, message = "Az ügyben Ön észlelőként vagy tanúként van megjelölve, ezért nem végezhet benne vizsgálati cselekményt." });
            }
            var result = FegyelmiUgyFunctions.GetFegyelmiUgyDataKivizsgaloModositashoz(fegyelmiUgyIds);

            return Json(result);
        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.FegyelmiUgyKivizsgaloModositasaMentes, DbModositasTortenik = true, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja)]
        public JsonResult FegyelmiUgyKivizsgaloModositasaMentes(FegyelmiUgyKivizsgaloModositasaViewModel model)
        {
            if (model.FegyelmiUgyIds == null)
            {
                throw new WarningException("Nincs kiválasztva egy ügy sem.");
            }
            if (model.FegyelmiUgyIds.Count > 1)
            {
                throw new WarningException("Többes művelet nem engedélyezett");
            }

            string szemelyNev = FegyelmiUgyFunctions.SaveFegyelmiUgyKivizsgaloModositasa(model);
            return Json(new { isSuccess = true, message = $"A kivizsgálásra {szemelyNev}-t jelöltük ki." });
        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.ElrendelemKivizsgalast, DbModositasTortenik = false, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja)]
        public JsonResult ElrendelemKivizsgalast(DontesFegyelmiUgyElrendeleserolViewModel model)
        {
            if (model.FegyelmiUgyIds == null)
            {
                throw new WarningException("Nincs kiválasztva egy ügy sem.");
            }
            var torvenyiHatarido = FegyelmiUgyFunctions.GetFegyelmiHatarido(model.FegyelmiUgyIds.First(), false, (int)FegyelmiUgyStatusz.KivizsgalasFolyamatban);

            if (model.KivizsgalasiHatarido < DateTime.Now.Date.AddDays(1))
            {
                return Json(new { isSuccess = false, message = "A dátum nem lehet a mai nap vagy régebbi" });
            }

            if (model.KivizsgalasiHatarido > torvenyiHatarido)
            {
                return Json(new { isSuccess = false, message = $"A dátum nem lehet {torvenyiHatarido.Value.ToShortDateString()} napnál későbbi" });
            }

            if (model.KivizsgaloSzemelySid == AlkalmazasKontextusFunctions.Kontextus.SzemelyzetSid)
            {
                return Json(new { isSuccess = false, message = $"A kivizsgáló személy nem lehet az elrendelő." });
            }

            FegyelmiUgyFunctions.DontesFegyelmiUgyElrendeleserol(model, true);
            return Json(new { isSuccess = true, message = "Az esemény kivizsgálása elrendelésre került" });
        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.ReintegraciosJogkorbe, DbModositasTortenik = false, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja)]
        public JsonResult ReintegraciosJogkorbe(DontesFegyelmiUgyElrendeleserolViewModel model)
        {
            if (model.FegyelmiUgyIds == null)
                throw new WarningException("Nincs kiválasztva egy ügy sem.");

            if (model.KivizsgalasiHatarido.Date < DateTime.Today.AddDays(1))
                return Json(new { isSuccess = false, message = "A dátum nem lehet a mai nap vagy régebbi" });

            FegyelmiUgyFunctions.DontesFegyelmiUgyElrendeleserol(model, false);

            return Json(new { isSuccess = true, message = "A fegyelmi ügy reintegrációs tiszti jogkörbe került" });
        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.GetFegyelmiUgyTanuMeghallgatasiJegyzokonyv, DbModositasTortenik = false, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja, Jogosultsagok.Fegyelmi_reintegracios_tiszt, Jogosultsagok.Fegyelmi_egyeb_szakterulet)]
        public JsonResult GetFegyelmiUgyTanuMeghallgatasiJegyzokonyv(List<int> fegyelmiUgyIds, List<int> naplobejegyzesIds)
        {
            bool warning = EsemenyResztvevoFunctions.WarningTanuVagyEszleloByFegyelmiUgyIds(fegyelmiUgyIds);
            if (warning)
            {
                return Json(new { isSuccess = false, message = "Az ügyben Ön észlelőként vagy tanúként van megjelölve, ezért nem végezhet benne vizsgálati cselekményt." });
            }
            var model = FegyelmiUgyFunctions.GetFegyelmiUgyTanuMeghallgatasiJegyzokonyv(fegyelmiUgyIds, naplobejegyzesIds);
            if (model.Leiras != null) model.Leiras = model.Leiras.Replace("<br/>", "\n");
            if (model.Leiras != null) model.Leiras = model.Leiras.Replace("<br />", "\n");

            if (model.Leiras == null) model.Leiras = "";

            return Json(model);
        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.GetFegyelmiUgySzemelyiAllomanyiTanuMeghallgatasiJegyzokonyv, DbModositasTortenik = false, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja, Jogosultsagok.Fegyelmi_reintegracios_tiszt)]
        public JsonResult GetFegyelmiUgySzemelyiAllomanyiTanuMeghallgatasiJegyzokonyv(List<int> fegyelmiUgyIds, List<int> naplobejegyzesIds)
        {
            bool warning = EsemenyResztvevoFunctions.WarningTanuVagyEszleloByFegyelmiUgyIds(fegyelmiUgyIds);
            if (warning)
            {
                return Json(new { isSuccess = false, message = "Az ügyben Ön észlelőként vagy tanúként van megjelölve, ezért nem végezhet benne vizsgálati cselekményt." });
            }
            var model = FegyelmiUgyFunctions.GetFegyelmiUgySzemelyiAllomanyiTanuMeghallgatasiJegyzokonyv(fegyelmiUgyIds, naplobejegyzesIds);
            if (model.Leiras != null) model.Leiras = model.Leiras.Replace("<br/>", "\n");
            if (model.Leiras != null) model.Leiras = model.Leiras.Replace("<br />", "\n");

            if (model.Leiras == null) model.Leiras = "";

            return Json(model);
        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.GetFegyelmiUgyElsoFokuTargyalasiJegyzokonyv, DbModositasTortenik = false, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja, Jogosultsagok.Fegyelmi_reintegracios_tiszt)]
        public JsonResult GetFegyelmiUgyElsoFokuTargyalasiJegyzokonyv(List<int> fegyelmiUgyIds, List<int> naplobejegyzesIds)
        {
            bool warning = EsemenyResztvevoFunctions.WarningTanuVagyEszleloByFegyelmiUgyIds(fegyelmiUgyIds);
            if (warning)
            {
                return Json(new { isSuccess = false, message = "Az ügyben Ön észlelőként vagy tanúként van megjelölve, ezért nem végezhet benne vizsgálati cselekményt." });
            }
            var model = FegyelmiUgyFunctions.GetFegyelmiUgyElsoFokuTargyalasiJegyzokonyv(fegyelmiUgyIds, naplobejegyzesIds);
            if (model.Leiras != null) model.Leiras = model.Leiras.Replace("<br/>", "\n");
            if (model.Leiras != null) model.Leiras = model.Leiras.Replace("<br />", "\n");

            if (model.Leiras == null) model.Leiras = "";

            return Json(model);
        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.GetFegyelmiUgyHatarozatRogzitese, DbModositasTortenik = false, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja)]
        public JsonResult GetFegyelmiUgyHatarozatRogzitese(int fegyelmiUgyId, ModalTipusok? modalType, int? naplobejegyzesId)
        {
            bool warning = EsemenyResztvevoFunctions.WarningTanuVagyEszleloByFegyelmiUgyIds(new List<int>() { fegyelmiUgyId });
            if (warning)
            {
                return Json(new { isSuccess = false, message = "Az ügyben Ön észlelőként vagy tanúként van megjelölve, ezért nem végezhet benne vizsgálati cselekményt." });
            }
            var model = FegyelmiUgyFunctions.GetFegyelmiUgyHatarozatRogzitese(fegyelmiUgyId, naplobejegyzesId, modalType);
            if (model.Leiras != null) model.Leiras = model.Leiras.Replace("<br/>", "\n");
            if (model.Leiras != null) model.Leiras = model.Leiras.Replace("<br />", "\n");

            if (model.Leiras == null) model.Leiras = "";

            return Json(model);
        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.SaveFegyelmiUgyHatarozatRogzitese, DbModositasTortenik = true, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja)]
        public JsonResult SaveFegyelmiUgyHatarozatRogzitese(FegyelmiUgyHatarozatRogziteseModel model)
        {
            model.Leiras = model.Leiras.Replace("\n", "<br />");
            FegyelmiUgyFunctions.SaveFegyelmiUgyHatarozat(model);
            return Json(new
            {
                NaploId = model.NaplobejegyzesId
            });
        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.SaveFegyelmiUgyTanuMeghallgatasiJegyzokonyv, DbModositasTortenik = true, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja, Jogosultsagok.Fegyelmi_reintegracios_tiszt)]
        public JsonResult SaveFegyelmiUgyTanuMeghallgatasiJegyzokonyv(FegyelmiUgyTanuMeghallgatasiJegyzokonyvModel model)
        {
            model.Leiras = model.Leiras.Replace("\n", "<br />");

            FegyelmiUgyFunctions.ThrowExceptionIfFegyelmiUgyNemModosithato(model.FegyelmiUgyIds);
            var naplobejegyzesIds = FegyelmiUgyFunctions.SaveFegyelmiUgyTanuMeghallgatasiJegyzokonyv(model);

            var fegyelmiUgy = FegyelmiUgyFunctions.FindById(model.FegyelmiUgyIds.First());
            var tanu = EsemenyResztvevoFunctions.GetEsemenyResztvevok(fegyelmiUgy.EsemenyId).Single(x => x.Id == model.TanuId);
            var isTanu = tanu.FogvatartottId != fegyelmiUgy.FogvatartottId;


            if (model.AlairtaFl == true)
            {
                if (isTanu)
                {
                    foreach (var naplobejegyzesId in naplobejegyzesIds)
                        FegyelmiUgyFunctions.TanuMeghallgatasDokumentumAdatok(naplobejegyzesId);
                }
                else
                {
                    foreach (var naplobejegyzesId in naplobejegyzesIds)
                        FegyelmiUgyFunctions.EljarasAlaVontMeghallgatasDokumentumAdatok(naplobejegyzesId);
                }
            }

            return Json(new { naplobejegyzesIds = naplobejegyzesIds, isTanu = isTanu });
        }

        [HttpPost]
        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.SaveFegyelmiUgyElsoFokuTargyalasiJegyzokonyv, DbModositasTortenik = true, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja, Jogosultsagok.Fegyelmi_reintegracios_tiszt)]
        public JsonResult SaveFegyelmiUgyElsoFokuTargyalasiJegyzokonyv(FegyelmiUgyElsoFokuTargyalasiJegyzokonyvModel model)
        {
            FegyelmiUgyFunctions.ThrowExceptionIfFegyelmiUgyNemModosithato(model.FegyelmiUgyIds);
            model.Leiras = model.Leiras.Replace("\n", "<br />");
            var naplobejegyzesIds = FegyelmiUgyFunctions.SaveFegyelmiUgyElsoFokuTargyalasiJegyzokonyv(model);

            return Json(new { naplobejegyzesIds = naplobejegyzesIds });
        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.SaveFegyelmiUgySzemelyiAllomanyiTanuMeghallgatasiJegyzokonyv, DbModositasTortenik = true, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja, Jogosultsagok.Fegyelmi_reintegracios_tiszt)]
        public JsonResult SaveFegyelmiUgySzemelyiAllomanyiTanuMeghallgatasiJegyzokonyv(FegyelmiUgySzemelyiAllomanyiTanuMeghallgatasiJegyzokonyvModel model)
        {
            model.Leiras = model.Leiras.Replace("\n", "<br />");

            FegyelmiUgyFunctions.ThrowExceptionIfFegyelmiUgyNemModosithato(model.FegyelmiUgyIds);
            var naplobejegyzesIds = FegyelmiUgyFunctions.SaveFegyelmiUgySzemelyiAllomanyiTanuMeghallgatasiJegyzokonyv(model);

            if (model.AlairtaFl == true)
            {
                foreach (var naplobejegyzesId in naplobejegyzesIds)
                    FegyelmiUgyFunctions.SzemelyiAllomanyiTanuMeghallgatasDokumentumAdatok(naplobejegyzesId);

            }

            return Json(new { naplobejegyzesIds = naplobejegyzesIds });
        }

        public JsonResult GetReintegraciosTiszt(int fogvatarottId)
        {
            var reintTisz = FegyelmiUgyFunctions.GetReintegraciosTiszt(fogvatarottId);
            return Json(reintTisz);
        }

        public JsonResult GetReintegraciosTisztek()
        {
            var aktItnezet = JogosultsagCacheFunctions.AktualisIntezet.Id;
            var reintTisztek = FegyelmiUgyFunctions.GetReintegraciosTisztek(aktItnezet);
            return Json(reintTisztek);
        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.GetReintegraciosTisztDontesModalData, DbModositasTortenik = false, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja, Jogosultsagok.Fegyelmi_reintegracios_tiszt)]
        public JsonResult GetReintegraciosTisztDontesModalData(List<int> fegyelmiUgyIds, List<int> naploIds)
        {
            if (fegyelmiUgyIds == null)
            {
                throw new WarningException("Nincs kiválasztva egy ügy sem.");
            }
            bool warning = EsemenyResztvevoFunctions.WarningTanuVagyEszleloByFegyelmiUgyIds(fegyelmiUgyIds);
            if (warning)
            {
                return Json(new { isSuccess = false, message = "Az ügyben Ön észlelőként vagy tanúként van megjelölve, ezért nem végezhet benne vizsgálati cselekményt." });
            }
            var model = FegyelmiUgyFunctions.GetReintegraciosTisztDontesModel(fegyelmiUgyIds, naploIds);
            if (model.Indoklas != null) model.Indoklas = model.Indoklas.Replace("<br/>", "\n");
            if (model.Indoklas != null) model.Indoklas = model.Indoklas.Replace("<br />", "\n");

            if (model.Indoklas == null) model.Indoklas = "";

            return Json(model);
        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.ReintegraciosTisztDontesKioktatasModalMentes, DbModositasTortenik = true, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja, Jogosultsagok.Fegyelmi_reintegracios_tiszt)]
        public JsonResult ReintegraciosTisztDontesKioktatasModalMentes(ReintegraciosTisztDontesModel model)
        {

            if (model.FegyelmiUgyIds == null)
            {
                throw new WarningException("Nincs kiválasztva egy ügy sem.");
            }
            FegyelmiUgyFunctions.ThrowExceptionIfFegyelmiUgyNemModosithato(model.FegyelmiUgyIds);
            model.Indoklas = model.Indoklas.Replace("\n", "<br />");

            var naplobejegyzesIds = FegyelmiUgyFunctions.SaveReintegraciosTisztDontesKioktatasModel(model);
            return Json(new { naplobejegyzesIds });
        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.ReintegraciosTisztDontesFeddesModalMentes, DbModositasTortenik = true, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja, Jogosultsagok.Fegyelmi_reintegracios_tiszt)]
        public JsonResult ReintegraciosTisztDontesFeddesModalMentes(ReintegraciosTisztDontesModel model)
        {
            if (model.FegyelmiUgyIds == null)
            {
                throw new WarningException("Nincs kiválasztva egy ügy sem.");
            }
            FegyelmiUgyFunctions.ThrowExceptionIfFegyelmiUgyNemModosithato(model.FegyelmiUgyIds);
            model.Indoklas = model.Indoklas.Replace("\n", "<br />");

            var naplobejegyzesIds = FegyelmiUgyFunctions.SaveReintegraciosTisztDontesFeddesModel(model);
            return Json(new { naplobejegyzesIds = naplobejegyzesIds });
        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.GetReintegraciosTisztDontesVisszakuldesModalData, DbModositasTortenik = false, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja, Jogosultsagok.Fegyelmi_reintegracios_tiszt)]
        public JsonResult GetReintegraciosTisztDontesVisszakuldesModalData(List<int> fegyelmiUgyIds)
        {
            if (fegyelmiUgyIds == null)
            {
                throw new WarningException("Nincs kiválasztva egy ügy sem.");
            }
            bool warning = EsemenyResztvevoFunctions.WarningTanuVagyEszleloByFegyelmiUgyIds(fegyelmiUgyIds);
            if (warning)
            {
                return Json(new { isSuccess = false, message = "Az ügyben Ön észlelőként vagy tanúként van megjelölve, ezért nem végezhet benne vizsgálati cselekményt." });
            }
            var model = FegyelmiUgyFunctions.GetReintegraciosTisztDontesVisszakuldesModel(fegyelmiUgyIds);
            if (model.Indoklas != null) model.Indoklas = model.Indoklas.Replace("<br/>", "\n");
            if (model.Indoklas != null) model.Indoklas = model.Indoklas.Replace("<br />", "\n");

            if (model.Indoklas == null) model.Indoklas = "";

            return Json(model);
        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.ReintegraciosTisztDontesVisszakuldesModalMentes, DbModositasTortenik = true, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja, Jogosultsagok.Fegyelmi_reintegracios_tiszt)]
        public JsonResult ReintegraciosTisztDontesVisszakuldesModalMentes(ReintegraciosTisztDontesModelVisszakuldes model)
        {
            if (model.FegyelmiUgyIds == null)
            {
                throw new WarningException("Nincs kiválasztva egy ügy sem.");
            }
            FegyelmiUgyFunctions.ThrowExceptionIfFegyelmiUgyNemModosithato(model.FegyelmiUgyIds);
            if (model.Indoklas != null) model.Indoklas = model.Indoklas.Replace("\n", "<br />");

            FegyelmiUgyFunctions.SaveReintegraciosTisztDontesVisszakuldesModel(model);
            return Json(new { success = true });
        }

        [HttpPost]
        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.GetEljarasLefolytatasanakMegtagadasa, DbModositasTortenik = false, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja)]
        public JsonResult GetEljarasLefolytatasanakMegtagadasa(int fegyelmiUgyId)
        {
            bool warning = EsemenyResztvevoFunctions.WarningTanuVagyEszleloByFegyelmiUgyIds(new List<int>() { fegyelmiUgyId });
            if (warning)
            {
                return Json(new { isSuccess = false, message = "Az ügyben Ön észlelőként vagy tanúként van megjelölve, ezért nem végezhet benne vizsgálati cselekményt." });
            }
            var model = FegyelmiUgyFunctions.GetEljarasLefolytatasanakMegtagadasa();
            if (model.Leiras != null) model.Leiras = model.Leiras.Replace("<br/>", "\n");
            if (model.Leiras != null) model.Leiras = model.Leiras.Replace("<br />", "\n");

            if (model.Leiras == null) model.Leiras = "";

            return Json(model);
        }

        [HttpPost]
        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.SaveEljarasLefolytatasanakMegtagadasa, DbModositasTortenik = true, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja)]
        public JsonResult SaveEljarasLefolytatasanakMegtagadasa(EljarasLefolytatasanakMegtagadasModel model)
        {
            if (model.FegyelmiUgyIds == null)
            {
                throw new WarningException("Nincs kiválasztva egy ügy sem.");
            }
            // Mentés
            model.Leiras = model.Leiras.Replace("\n", "<br />");
            FegyelmiUgyFunctions.EljarasLefolytatasanakMegtagadasa(model);
            return Json(new { success = true });
        }

        [HttpPost]
        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.UjHataridoModositasiJavaslat, DbModositasTortenik = false, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja, Jogosultsagok.Fegyelmi_reintegracios_tiszt)]
        public JsonResult UjHataridoModositasiJavaslat(LeirasMegadasFormModel model)
        {
            ThrowValidationExceptionIfNotValid();
            model.Leiras = model.Leiras.Replace("\n", "<br />");
            FegyelmiUgyFunctions.HataridoModositasiJavaslat(model);
            return Json(new { success = true });
        }

        [HttpPost]
        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.UjFegyelmiNaplobejegyzesLetrehozasa, DbModositasTortenik = false, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja, Jogosultsagok.Fegyelmi_reintegracios_tiszt)]
        public JsonResult UjFegyelmiNaplobejegyzesLetrehozasa(FegyelmiNaplobejegyzesFelviteleModel model)
        {
            if (model.FegyelmiUgyIds.Count != 1 || (model.NaplobejegyzesIds != null && model.NaplobejegyzesIds.Count > 1))
                throw new WarningException("Egyszerre egyetlen naplóbejegyzés szerkeszthető");

            ThrowValidationExceptionIfNotValid();
            model.Leiras = model.Leiras.Replace("\n", "<br />");

            if (model.NaplobejegyzesIds == null || model.NaplobejegyzesIds.Count == 0)
                FegyelmiUgyFunctions.UjSzabadszovegesFegyelmiNaplobejegyzesFelvitele(model);
            else
                FegyelmiUgyFunctions.SzabadszovegesFegyelmiNaplobejegyzesSzerkesztese(model.NaplobejegyzesIds.First(), model.FegyelmiUgyIds.Single(), model.Leiras);

            return Json(new { success = true });
        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.GetFegyelmiNaplobejegyzesSzerkesztese, DbModositasTortenik = false, FeluletenLathato = true)]
        public JsonResult GetFegyelmiNaplobejegyzesSzerkesztese(List<int> fegyelmiUgyIds, List<int> naplobejegyzesIds)
        {
            bool warning = EsemenyResztvevoFunctions.WarningTanuVagyEszleloByFegyelmiUgyIds(fegyelmiUgyIds);
            if (warning)
            {
                return Json(new { isSuccess = false, message = "Az ügyben Ön észlelőként vagy tanúként van megjelölve, ezért nem végezhet benne vizsgálati cselekményt." });
            }
            if (naplobejegyzesIds != null && naplobejegyzesIds.Count > 1)
                throw new WarningException("Egyszerre egyetlen naplóbejegyzés szerkeszthető");

            if (naplobejegyzesIds != null)
            {
                FegyelmiNaplobejegyzesFelviteleModel model = FegyelmiUgyFunctions.GetSzabadszovegesFegyelmiNaplobejegyzesSzerkesztese(naplobejegyzesIds);

                return Json(model);
            }
            return Json(new FegyelmiNaplobejegyzesFelviteleModel());
        }

        [HttpPost]
        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.UjFelfuggesztesiJavaslat, DbModositasTortenik = false, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja, Jogosultsagok.Fegyelmi_reintegracios_tiszt)]
        public JsonResult UjFelfuggesztesiJavaslat(FelfuggesztesModel model)
        {
            ThrowValidationExceptionIfNotValid();
            FegyelmiUgyFunctions.FelfuggesztesiJavaslat(model);
            return Json(new { success = true });
        }

        [HttpPost]
        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.GetFelfuggesztesiJavaslat, DbModositasTortenik = false, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja, Jogosultsagok.Fegyelmi_reintegracios_tiszt)]
        public JsonResult GetFelfuggesztesiJavaslat(List<int> fegyelmiUgyIds)
        {
            ThrowValidationExceptionIfNotValid();
            bool warning = EsemenyResztvevoFunctions.WarningTanuVagyEszleloByFegyelmiUgyIds(fegyelmiUgyIds);
            if (warning)
            {
                return Json(new { isSuccess = false, message = "Az ügyben Ön észlelőként vagy tanúként van megjelölve, ezért nem végezhet benne vizsgálati cselekményt." });
            }
            var model = FegyelmiUgyFunctions.GetFelfuggesztesiJavaslat(fegyelmiUgyIds);
            return Json(model);
        }

        [HttpPost]
        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.SaveFegyelmiUgyUjResztvevo, DbModositasTortenik = true, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja, Jogosultsagok.Fegyelmi_reintegracios_tiszt)]
        public JsonResult SaveFegyelmiUgyUjResztvevo(int fegyelmiUgyId, int fogvatartottId)
        {
            ThrowValidationExceptionIfNotValid();
            var resztvevoId = FegyelmiUgyFunctions.SaveFegyelmiUgyUjResztvevo(fegyelmiUgyId, fogvatartottId);
            return Json(new { resztvevoId });
        }

        [HttpPost]
        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.SaveFegyelmiUgyUjAllomanyiResztvevo, DbModositasTortenik = true, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja, Jogosultsagok.Fegyelmi_reintegracios_tiszt)]
        public JsonResult SaveFegyelmiUgyUjAllomanyiResztvevo(List<int> fegyelmiUgyIds, string allomanyiTanuSid)
        {
            ThrowValidationExceptionIfNotValid();
            var resztvevoId = FegyelmiUgyFunctions.SaveFegyelmiUgyUjAllomanyiResztvevo(fegyelmiUgyIds, allomanyiTanuSid);
            return Json(new { resztvevoId });
        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.GetFegyelmiUgyDataHelysziniSzemlehez, DbModositasTortenik = false, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja, Jogosultsagok.Fegyelmi_reintegracios_tiszt)]
        public JsonResult GetFegyelmiUgyDataHelysziniSzemlehez(List<int> fegyelmiUgyIds, List<int> naplobejegyzesIds)
        {
            if (fegyelmiUgyIds == null)
            {
                throw new WarningException("Nincs kiválasztva egy ügy sem.");
            }
            bool warning = EsemenyResztvevoFunctions.WarningTanuVagyEszleloByFegyelmiUgyIds(fegyelmiUgyIds);
            if (warning)
            {

                //throw new WarningException("ááááááááááááááááááááááááááááááááááááááá");
                return Json(new { isSuccess = false, message = "Az ügyben Ön észlelőként vagy tanúként van megjelölve, ezért nem végezhet benne vizsgálati cselekményt." });
            }
            var model = FegyelmiUgyFunctions.GetFegyelmiUgyDataHelysziniSzemleModel(fegyelmiUgyIds, naplobejegyzesIds);
            if (model.Leiras != null) model.Leiras = model.Leiras.Replace("<br/>", "\n");
            if (model.Leiras != null) model.Leiras = model.Leiras.Replace("<br />", "\n");

            if (model.Leiras == null) model.Leiras = "";

            return Json(model);
        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.FegyelmiUgyHelysziniSzemleMentes, DbModositasTortenik = false, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja, Jogosultsagok.Fegyelmi_reintegracios_tiszt)]
        public JsonResult FegyelmiUgyHelysziniSzemleMentes(HelysziniSzemleModel model)
        {
            if (model.FegyelmiUgyIds == null)
            {
                throw new WarningException("Nincs kiválasztva egy ügy sem.");
            }
            FegyelmiUgyFunctions.ThrowExceptionIfFegyelmiUgyNemModosithato(model.FegyelmiUgyIds);
            model.Leiras = model.Leiras.Replace("\n", "<br />");

            var naplobejegyzesIds = FegyelmiUgyFunctions.FegyelmiUgyHelysziniSzemleMentes(model);
            return Json(new { success = true, message = "Az adatokat mentettük", naplobejegyzesIds });
        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.GetFegyelmiUgyDataSzakteruletiVelemenyhez, DbModositasTortenik = false, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja, Jogosultsagok.Fegyelmi_reintegracios_tiszt, Jogosultsagok.Fegyelmi_egyeb_szakterulet)]
        public JsonResult GetFegyelmiUgyDataSzakteruletiVelemenyhez(List<int> fegyelmiUgyIds, List<int> naplobejegyzesIds)
        {
            bool warning = EsemenyResztvevoFunctions.WarningTanuVagyEszleloByFegyelmiUgyIds(fegyelmiUgyIds);
            if (warning)
            {
                return Json(new { isSuccess = false, message = "Az ügyben Ön észlelőként vagy tanúként van megjelölve, ezért nem végezhet benne vizsgálati cselekményt." });
            }
            var model = FegyelmiUgyFunctions.GetFegyelmiUgyDataSzakteruletiVelemenyModelModel(fegyelmiUgyIds, naplobejegyzesIds);
            if (model.Leiras != null) model.Leiras = model.Leiras.Replace("<br/>", "\n");
            if (model.Leiras != null) model.Leiras = model.Leiras.Replace("<br />", "\n");

            if (model.Leiras == null) model.Leiras = "";

            return Json(model);
        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.FegyelmiUgySzakteruletiVelemenyMentes, DbModositasTortenik = false, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja, Jogosultsagok.Fegyelmi_reintegracios_tiszt, Jogosultsagok.Fegyelmi_egyeb_szakterulet)]
        public JsonResult FegyelmiUgySzakteruletiVelemenyMentes(SzakteruletiVelemenyModel model)
        {
            FegyelmiUgyFunctions.ThrowExceptionIfFegyelmiUgyNemModosithato(model.FegyelmiUgyIds);
            model.Leiras = model.Leiras.Replace("\n", "<br />");

            var naplobejegyzesIds = FegyelmiUgyFunctions.FegyelmiUgySzakteruletiVelemenyModelMentes(model);
            return Json(new { success = true, message = "Az adatokat mentettük", naplobejegyzesIds });
        }

        [HttpPost]
        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.FegyelmiUgySzakteruletiVelemenyFileDelete, DbModositasTortenik = false, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja, Jogosultsagok.Fegyelmi_reintegracios_tiszt, Jogosultsagok.Fegyelmi_egyeb_szakterulet)]
        public JsonResult FegyelmiUgySzakteruletiVelemenyFileDelete(int fileId)
        {
            var feltoltottFajl = FeltoltesFunctions.GetFeltoltottFajlById(fileId);
            if (feltoltottFajl == null)
            {
                throw new WarningException("Feltöltött fájl nem található");
            }
            FeltoltesFunctions.DeleteFile(fileId);
            return Json(new { success = true, fileId = feltoltottFajl.Id });
        }

        [HttpPost]
        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.GetOsszefoglaloJelentesModalData, DbModositasTortenik = false, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja, Jogosultsagok.Fegyelmi_reintegracios_tiszt)]
        public JsonResult GetOsszefoglaloJelentesModalData(List<int> fegyelmiUgyIds, List<int> naplobejegyzesIds)
        {
            bool warning = EsemenyResztvevoFunctions.WarningTanuVagyEszleloByFegyelmiUgyIds(fegyelmiUgyIds);
            if (warning)
            {
                return Json(new { isSuccess = false, message = "Az ügyben Ön észlelőként vagy tanúként van megjelölve, ezért nem végezhet benne vizsgálati cselekményt." });
            }
            var model = FegyelmiUgyFunctions.GetOsszefoglaloJelentesModalData(fegyelmiUgyIds, naplobejegyzesIds);

            if (model.Leiras == null) model.Leiras = "";

            model.Leiras = $"{model.Leiras}";

            return Json(model);
        }

        [HttpPost]
        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.SaveOsszefoglaloJelentes, DbModositasTortenik = true, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja, Jogosultsagok.Fegyelmi_reintegracios_tiszt)]
        public JsonResult SaveOsszefoglaloJelentes(OsszefoglaloJelentesModel model)
        {
            ThrowValidationExceptionIfNotValid();
            FegyelmiUgyFunctions.ThrowExceptionIfFegyelmiUgyNemModosithato(model.FegyelmiUgyIds);
            //model.Leiras = model.Leiras.Replace("\n", "<br />");

            var naplobejegyzesIds = FegyelmiUgyFunctions.SaveOsszefoglaloJelentes(model);
            return Json(new { naplobejegyzesIds });
        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.GetFegyelmiUgyDataOsszevonashoz, DbModositasTortenik = false, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja)]
        public JsonResult GetFegyelmiUgyDataOsszevonashoz(int fegyelmiUgyId)
        {
            bool warning = EsemenyResztvevoFunctions.WarningTanuVagyEszleloByFegyelmiUgyIds(new List<int>() { fegyelmiUgyId });
            if (warning)
            {
                return Json(new { isSuccess = false, message = "Az ügyben Ön észlelőként vagy tanúként van megjelölve, ezért nem végezhet benne vizsgálati cselekményt." });
            }
            var result = FegyelmiUgyFunctions.GetFegyelmiUgyDataOsszevonashoz(fegyelmiUgyId);
            return Json(result);
        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.FegyelmiUgyOsszevonasMentes, DbModositasTortenik = false, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja)]
        public JsonResult FegyelmiUgyOsszevonasMentes(int fegyelmiUgyId, List<int> alUgyList)
        {
            FegyelmiUgyFunctions.FegyelmiUgyOsszevonasMentes(fegyelmiUgyId, alUgyList);
            return Json(new { success = true, message = "Az adatokat mentettük" });
        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.GetElsoFokuFegyelmiTargyalasElokeszitese, DbModositasTortenik = false, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja)]
        public JsonResult GetElsoFokuFegyelmiTargyalasElokeszitese(int fegyelmiUgyId)
        {
            bool warning = EsemenyResztvevoFunctions.WarningTanuVagyEszleloByFegyelmiUgyIds(new List<int>() { fegyelmiUgyId });
            if (warning)
            {
                return Json(new { isSuccess = false, message = "Az ügyben Ön észlelőként vagy tanúként van megjelölve, ezért nem végezhet benne vizsgálati cselekményt." });
            }
            var result = FegyelmiUgyFunctions.GetElsoFokuFegyelmiTargyalasElokeszitese(fegyelmiUgyId);
            return Json(result);
        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.GetMasodFokuFegyelmiTargyalasElokeszitese, DbModositasTortenik = false, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja)]
        public JsonResult GetMasodFokuFegyelmiTargyalasElokeszitese(int fegyelmiUgyId)
        {
            bool warning = EsemenyResztvevoFunctions.WarningTanuVagyEszleloByFegyelmiUgyIds(new List<int>() { fegyelmiUgyId });
            if (warning)
            {
                return Json(new { isSuccess = false, message = "Az ügyben Ön észlelőként vagy tanúként van megjelölve, ezért nem végezhet benne vizsgálati cselekményt." });
            }
            var result = FegyelmiUgyFunctions.GetMasodFokuFegyelmiTargyalasElokeszitese(fegyelmiUgyId);
            return Json(result);
        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.SaveElsoFokuFegyelmiTargyalasElokeszitese, DbModositasTortenik = true, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja)]
        public JsonResult SaveElsoFokuFegyelmiTargyalasElokeszitese(ElsoFokuFegyelmiTargyalasElokeszitesModel model)
        {
            FegyelmiUgyFunctions.ThrowExceptionIfFegyelmiUgyNemModosithato(new List<int>() { model.FegyelmiUgyId });
            FegyelmiUgyFunctions.SaveElsoFokuTargyalasKituzese(model);
            return Json(new { isSuccess = true });
        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.SaveMasodFokuTargyalasKituzese, DbModositasTortenik = true, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja)]
        public JsonResult SaveMasodFokuTargyalasKituzese(ElsoFokuFegyelmiTargyalasElokeszitesModel model)
        {
            ThrowValidationExceptionIfNotValid();
            FegyelmiUgyFunctions.ThrowExceptionIfFegyelmiUgyNemModosithato(new List<int>() { model.FegyelmiUgyId });
            if (new DateTime(model.TargyalasIdopontja.Year, model.TargyalasIdopontja.Month, model.TargyalasIdopontja.Day) < DateTime.Today)
                throw new WarningException("A tárgyalás időpontja nem lehet múltbéli.");
            FegyelmiUgyFunctions.SaveMasodFokuTargyalasKituzese(model);
            return Json(new { isSuccess = true });
        }

        //[AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.GetOsszevontFegyelmiUgyekForFegyelmiUgy, DbModositasTortenik = false, FeluletenLathato = true)] // Aktivitás folyamról vagy tábláról nyitás
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja,
                           Jogosultsagok.Fegyelmi_reintegracios_tiszt,
                           Jogosultsagok.Fegyelmi_egyeb_szakterulet,
                           Jogosultsagok.Fegyelmi_fofelugyelo,
                           Jogosultsagok.Jfk_fegyjutmegtekinto)]
        public JsonResult GetOsszevontFegyelmiUgyekForFegyelmiUgy(int fegyelmiUgyId)
        {
            var result = new List<object>();

            var lista = FegyelmiUgyFunctions.GetOsszevontFegyelmiUgyekForId(fegyelmiUgyId);

            foreach (var fegyelmiugy in lista)
            {
                var naplobejegyzesek = NaploFunctions.GetNaplobejegyzesekByFegyelmiUgyId(fegyelmiugy.FegyelmiUgyId);
                var fegyelmiUgy = FegyelmiUgyFunctions.GetFegyelmiUgyById(fegyelmiugy.FegyelmiUgyId);
                var esemeny = EsemenyekFunctions.GetEsemenyById(fegyelmiUgy.EsemenyId);

                var resztvevok = EsemenyResztvevoFunctions.GetEsemenyResztvevokPanelra(esemeny.Id);
                esemeny.Tanuk = resztvevok.Where(w => w.ErintettsegFokaCimId == (int)ErintettsegFoka.Tanu).Select(x => x.NyilvantartasiAzonosito + " - " + x.FogvatartottNev).ToList();
                esemeny.Elkovetok = resztvevok.Where(w => w.ErintettsegFokaCimId == (int)ErintettsegFoka.Elkoveto).Select(x => x.NyilvantartasiAzonosito + " - " + x.FogvatartottNev).ToList();
                esemeny.Sertettek = resztvevok.Where(w => w.ErintettsegFokaCimId == (int)ErintettsegFoka.Sertett).Select(x => x.NyilvantartasiAzonosito + " - " + x.FogvatartottNev).ToList();

                result.Add(new { FegyelmiUgy = fegyelmiugy, Naplo = new { naplobejegyzesek, esemeny } });
            }

            return Json(result);
        }

        [HttpPost]
        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.GetKirendeltVedoKereseModalData, DbModositasTortenik = false, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja, Jogosultsagok.Fegyelmi_reintegracios_tiszt)]
        public JsonResult GetKirendeltVedoKereseModalData(List<int> fegyelmiUgyIds, List<int> naplobejegyzesIds)
        {
            bool warning = EsemenyResztvevoFunctions.WarningTanuVagyEszleloByFegyelmiUgyIds(fegyelmiUgyIds);
            if (warning)
            {
                return Json(new { isSuccess = false, message = "Az ügyben Ön észlelőként vagy tanúként van megjelölve, ezért nem végezhet benne vizsgálati cselekményt." });
            }
            var model = FegyelmiUgyFunctions.GetKirendeltVedoKereseModalData(fegyelmiUgyIds, naplobejegyzesIds);
            if (model.Leiras != null) model.Leiras = model.Leiras.Replace("<br/>", "\n");
            if (model.Leiras != null) model.Leiras = model.Leiras.Replace("<br />", "\n");

            if (model.Leiras == null) model.Leiras = "";

            return Json(model);
        }

        [HttpPost]
        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.SaveKirendeltVedoKereseModalData, DbModositasTortenik = true, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja, Jogosultsagok.Fegyelmi_reintegracios_tiszt)]
        public JsonResult SaveKirendeltVedoKereseModalData(KirendeltVedoKereseModel model)
        {
            ThrowValidationExceptionIfNotValid();
            model.Leiras = model.Leiras.Replace("\n", "<br />");
            FegyelmiUgyFunctions.ThrowExceptionIfFegyelmiUgyNemModosithato(model.FegyelmiUgyIds);
            var naplobejegyzesIds = FegyelmiUgyFunctions.SaveKirendeltVedoKereseModalData(model);
            return Json(new { naplobejegyzesIds = naplobejegyzesIds });
        }

        [HttpPost]
        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.GetMeghatalmazottVedoKereseModalData, DbModositasTortenik = false, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja, Jogosultsagok.Fegyelmi_reintegracios_tiszt)]
        public JsonResult GetMeghatalmazottVedoKereseModalData(List<int> fegyelmiUgyIds, List<int> naplobejegyzesIds)
        {
            bool warning = EsemenyResztvevoFunctions.WarningTanuVagyEszleloByFegyelmiUgyIds(fegyelmiUgyIds);
            if (warning)
            {
                return Json(new { isSuccess = false, message = "Az ügyben Ön észlelőként vagy tanúként van megjelölve, ezért nem végezhet benne vizsgálati cselekményt." });
            }
            var model = FegyelmiUgyFunctions.GetMeghatalmazottVedoKereseModalData(fegyelmiUgyIds, naplobejegyzesIds);
            if (model.Leiras != null) model.Leiras = model.Leiras.Replace("<br/>", "\n");
            if (model.Leiras != null) model.Leiras = model.Leiras.Replace("<br />", "\n");

            if (model.Leiras == null) model.Leiras = "";

            return Json(model);
        }

        [HttpPost]
        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.SaveMeghatalmazottVedoKereseModalData, DbModositasTortenik = true, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja, Jogosultsagok.Fegyelmi_reintegracios_tiszt)]
        public JsonResult SaveMeghatalmazottVedoKereseModalData(MeghatalmazottVedoKereseModel model)
        {
            ThrowValidationExceptionIfNotValid();
            FegyelmiUgyFunctions.ThrowExceptionIfFegyelmiUgyNemModosithato(model.FegyelmiUgyIds);
            if (model.Leiras != null)
                model.Leiras = model.Leiras.Replace("\n", "<br />");

            if (model.VedoElerhetosege == null) model.VedoElerhetosege = "";

            var naplobejegyzesIds = FegyelmiUgyFunctions.SaveMeghatalmazottVedoKereseModalData(model);
            return Json(new { naplobejegyzesIds = naplobejegyzesIds });
        }

        [HttpPost]
        //[AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.VanNaploTipusAFegyelmiUgyeknek, DbModositasTortenik = false, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja, Jogosultsagok.Fegyelmi_reintegracios_tiszt)]
        public JsonResult VanNaploTipusAFegyelmiUgyeknek(List<int> fegyelmiUgyIds, int napoTipusCimkeId)
        {
            var fegyelmiUgyek = FegyelmiUgyFunctions.GetFegyelmiUgyekByNaploTipus(fegyelmiUgyIds, napoTipusCimkeId);
            var bejegyzesAzonositok = fegyelmiUgyek.Select(s => $"{s.UgySorszamaIntezetAzon}/{s.UgySorszamaEv}/{s.UgySorszama}").ToList();
            return Json(bejegyzesAzonositok);
        }

        [HttpPost]
        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.GetHataridoModositasModalData, DbModositasTortenik = false, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja)]
        public JsonResult GetHataridoModositasModalData(List<int> fegyelmiUgyIds)
        {
            bool warning = EsemenyResztvevoFunctions.WarningTanuVagyEszleloByFegyelmiUgyIds(fegyelmiUgyIds);
            if (warning)
            {
                return Json(new { isSuccess = false, message = "Az ügyben Ön észlelőként vagy tanúként van megjelölve, ezért nem végezhet benne vizsgálati cselekményt." });
            }
            var model = FegyelmiUgyFunctions.GetHataridoModositasModalData(fegyelmiUgyIds);
            return Json(model);
        }

        [HttpPost]
        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.GetUjHataridoModositasiJavaslatModalData, DbModositasTortenik = false, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja, Jogosultsagok.Fegyelmi_reintegracios_tiszt)]
        public JsonResult GetUjHataridoModositasiJavaslatModalData(List<int> fegyelmiUgyIds)
        {
            bool warning = EsemenyResztvevoFunctions.WarningTanuVagyEszleloByFegyelmiUgyIds(fegyelmiUgyIds);
            if (warning)
            {
                return Json(new { isSuccess = false, message = "Az ügyben Ön észlelőként vagy tanúként van megjelölve, ezért nem végezhet benne vizsgálati cselekményt." });
            }
            var model = FegyelmiUgyFunctions.GetHataridoModositasModalData(fegyelmiUgyIds);
            return Json(model);
        }

        [HttpPost]
        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.SaveHataridoModositasModalData, DbModositasTortenik = true, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja)]
        public JsonResult SaveHataridoModositasModalData(HataridoModositasModel model)
        {
            ThrowValidationExceptionIfNotValid();
            FegyelmiUgyFunctions.SaveHataridoModositasModalData(model);
            return Json(new { success = true });
        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.GetFelfuggesztesMegszuntetes, DbModositasTortenik = false, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja)]
        public JsonResult GetFelfuggesztesMegszuntetes(List<int> fegyelmiUgyIds)
        {

            if (fegyelmiUgyIds == null)
            {
                throw new WarningException("Nincs kiválasztva egy ügy sem.");
            }
            bool warning = EsemenyResztvevoFunctions.WarningTanuVagyEszleloByFegyelmiUgyIds(fegyelmiUgyIds);
            if (warning)
            {
                return Json(new { isSuccess = false, message = "Az ügyben Ön észlelőként vagy tanúként van megjelölve, ezért nem végezhet benne vizsgálati cselekményt." });
            }
            var fegyelmiUgy = FegyelmiUgyFunctions.FindById(fegyelmiUgyIds.First());
            var getEszleloId = EsemenyekFunctions.GetEsemenyById(fegyelmiUgy.EsemenyId).EszleloId;
            var getEszlelo = SzemelyzetFunctions.FindById(getEszleloId);
            var fegyelmiUsers = SzemelyzetFunctions.GetFegyelmiAlkalmazottak();
            var aktIntezet = JogosultsagCacheFunctions.AktualisIntezet.Id;
            var currentEszleloSid = AlkalmazasKontextusFunctions.Kontextus.SzemelyzetSid;
            var currentEszlelo = AlkalmazasKontextusFunctions.Kontextus.SzemelyzetId;
            var isCurrentEszleloInFegyelmiUsersList = fegyelmiUsers.Any(w => w.Sid == currentEszleloSid);
            if (!isCurrentEszleloInFegyelmiUsersList)
            {
                var szemelyzet = SzemelyzetFunctions.FindById(currentEszlelo);
                var eszlelo = SzemelyzetFunctions.GetAdFegyelmiUser(szemelyzet);
                fegyelmiUsers.Add(eszlelo);
            }
            var isEszleloInFegyelmiUsersList = fegyelmiUsers.Any(w => w.Sid == getEszlelo.AdSid);
            if (isEszleloInFegyelmiUsersList)
            {
                var eszlelo = SzemelyzetFunctions.GetAdFegyelmiUser(getEszlelo);
                fegyelmiUsers.RemoveAll(x => x.Sid == eszlelo.Sid);
            }

            List<KSelect2ItemModel> szemelyzetSelectList = fegyelmiUsers.Select(x => new KSelect2ItemModel() { id = x.Sid, text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(x.Displayname.ToLower()) + (x.Rendfokozat == null ? "" : (" " + x.Rendfokozat)) }).ToList();

            FelfuggesztesMegszunteteseViewModel result = new FelfuggesztesMegszunteteseViewModel()
            {
                FegyelmiUgyIds = fegyelmiUgyIds,
                KivizsgalasiHatarido = FegyelmiUgyFunctions.GetFegyelmiHatarido(fegyelmiUgy.Id, false, (int)FegyelmiUgyStatusz.KivizsgalasFolyamatban) ?? DateTime.Now,
                KivizsgaloSzemelyOptions = szemelyzetSelectList,
                KivizsgaloSzemelySid = currentEszleloSid,
            };
            return Json(result);
        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.SaveFelfuggesztesMegszuntetes, DbModositasTortenik = true, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja)]
        public JsonResult SaveFelfuggesztesMegszuntetes(FelfuggesztesMegszunteteseViewModel model)
        {
            ThrowValidationExceptionIfNotValid();
            var result = FegyelmiUgyFunctions.SaveFelfuggesztesMegszuntetes(model);
            return Json(new { result });
        }

        [HttpPost]
        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.GetFelfuggesztesModalData, DbModositasTortenik = false, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja)]
        public JsonResult GetFelfuggesztesModalData(List<int> fegyelmiUgyIds)
        {
            bool warning = EsemenyResztvevoFunctions.WarningTanuVagyEszleloByFegyelmiUgyIds(fegyelmiUgyIds);
            if (warning)
            {
                return Json(new { isSuccess = false, message = "Az ügyben Ön észlelőként vagy tanúként van megjelölve, ezért nem végezhet benne vizsgálati cselekményt." });
            }
            var model = FegyelmiUgyFunctions.GetFelfuggesztesModalData(fegyelmiUgyIds);
            return Json(model);
        }

        [HttpPost]
        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.SaveFelfuggesztesModalData, DbModositasTortenik = true, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja)]
        public JsonResult SaveFelfuggesztesModalData(FelfuggesztesModel model)
        {
            ThrowValidationExceptionIfNotValid();
            FegyelmiUgyFunctions.SaveFelfuggesztesModalData(model);
            return Json(new { success = true });
        }

        [HttpPost]
        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.GetVedoTelefonosTajekoztatasaModalData, DbModositasTortenik = false, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja)]
        public JsonResult GetVedoTelefonosTajekoztatasaModalData(List<int> fegyelmiUgyIds, List<int> naplobejegyzesIds)
        {
            bool warning = EsemenyResztvevoFunctions.WarningTanuVagyEszleloByFegyelmiUgyIds(fegyelmiUgyIds);
            if (warning)
            {
                return Json(new { isSuccess = false, message = "Az ügyben Ön észlelőként vagy tanúként van megjelölve, ezért nem végezhet benne vizsgálati cselekményt." });
            }
            var model = FegyelmiUgyFunctions.GetVedoTelefonosTajekoztatasaModalData(fegyelmiUgyIds,naplobejegyzesIds);
            if (model.Leiras != null) model.Leiras = model.Leiras.Replace("<br/>", "\n");
            if (model.Leiras != null) model.Leiras = model.Leiras.Replace("<br />", "\n");

            if (model.Leiras == null) model.Leiras = "";

            return Json(model);
        }

        [HttpPost]
        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.SaveVedoTelefonosTajekoztatasaModalData, DbModositasTortenik = true, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja)]
        public JsonResult SaveVedoTelefonosTajekoztatasaModalData(VedoTelefonosTajekoztatasaModel model)
        {
            ThrowValidationExceptionIfNotValid();
            model.Leiras = model.Leiras.Replace("\n", "<br />");

            var naplobejegyzesIds = FegyelmiUgyFunctions.SaveVedoTelefonosTajekoztatasaModalData(model);
            return Json(new { naplobejegyzesIds = naplobejegyzesIds });
        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.GetMasodFokuTargyalasiJegyzokonyvModalData, DbModositasTortenik = false, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja, Jogosultsagok.Fegyelmi_reintegracios_tiszt)]
        public JsonResult GetMasodFokuTargyalasiJegyzokonyvModalData(List<int> fegyelmiUgyIds, List<int> naplobejegyzesIds)
        {
            bool warning = EsemenyResztvevoFunctions.WarningTanuVagyEszleloByFegyelmiUgyIds(fegyelmiUgyIds);
            if (warning)
            {
                return Json(new { isSuccess = false, message = "Az ügyben Ön észlelőként vagy tanúként van megjelölve, ezért nem végezhet benne vizsgálati cselekményt." });
            }
            var model = FegyelmiUgyFunctions.GetMasodFokuTargyalasiJegyzokonyv(fegyelmiUgyIds, naplobejegyzesIds);
            if (model.Leiras != null) model.Leiras = model.Leiras.Replace("<br/>", "\n");
            if (model.Leiras != null) model.Leiras = model.Leiras.Replace("<br />", "\n");

            if (model.Leiras == null) model.Leiras = "";

            return Json(model);
        }

        [HttpPost]
        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.SaveMasodFokuTargyalasiJegyzokonyvModalData, DbModositasTortenik = true, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja, Jogosultsagok.Fegyelmi_reintegracios_tiszt)]
        public JsonResult SaveMasodFokuTargyalasiJegyzokonyvModalData(FegyelmiUgyMasodFokuTargyalasiJegyzokonyvModel model)
        {
            FegyelmiUgyFunctions.ThrowExceptionIfFegyelmiUgyNemModosithato(model.FegyelmiUgyIds);
            model.Leiras = model.Leiras.Replace("\n", "<br />");
            var naplobejegyzesIds = FegyelmiUgyFunctions.SaveMasodFokuTargyalasiJegyzokonyv(model);

            var fegyelmiUgy = FegyelmiUgyFunctions.FindById(model.FegyelmiUgyIds.First());

            return Json(new { naplobejegyzesIds = naplobejegyzesIds });
        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.GetSzakteruletiVelemenyKereseModalData, DbModositasTortenik = false, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja, Jogosultsagok.Fegyelmi_reintegracios_tiszt)]
        public JsonResult GetSzakteruletiVelemenyKereseModalData(List<int> fegyelmiUgyIds, List<int> naplobejegyzesIds)
        {
            bool warning = EsemenyResztvevoFunctions.WarningTanuVagyEszleloByFegyelmiUgyIds(fegyelmiUgyIds);
            if (warning)
            {
                return Json(new { isSuccess = false, message = "Az ügyben Ön észlelőként vagy tanúként van megjelölve, ezért nem végezhet benne vizsgálati cselekményt." });
            }
            var model = FegyelmiUgyFunctions.GetSzakteruletiVelemenyKereseModalData(fegyelmiUgyIds, naplobejegyzesIds);
            if (model.Leiras != null) model.Leiras = model.Leiras.Replace("<br/>", "\n");
            if (model.Leiras != null) model.Leiras = model.Leiras.Replace("<br />", "\n");

            if (model.Leiras == null) model.Leiras = "";

            return Json(model);
        }

        [HttpPost]
        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.JogiKepviseletVisszavonasa, DbModositasTortenik = false, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja, Jogosultsagok.Fegyelmi_reintegracios_tiszt)]
        public JsonResult JogiKepviseletVisszavonasa(List<int> fegyelmiUgyIds)
        {
            FegyelmiUgyFunctions.JogiKepviseletVisszavonasa(fegyelmiUgyIds);
            return Json(new { success = true });
        }

        [HttpPost]
        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.GetSzembesitesiJegyzokonyvModalData, DbModositasTortenik = false, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja, Jogosultsagok.Fegyelmi_reintegracios_tiszt)]
        public JsonResult GetSzembesitesiJegyzokonyvModalData(List<int> fegyelmiUgyIds, List<int> naplobejegyzesIds)
        {
            bool warning = EsemenyResztvevoFunctions.WarningTanuVagyEszleloByFegyelmiUgyIds(fegyelmiUgyIds);
            if (warning)
            {
                return Json(new { isSuccess = false, message = "Az ügyben Ön észlelőként vagy tanúként van megjelölve, ezért nem végezhet benne vizsgálati cselekményt." });
            }
            try
            {
                var model = FegyelmiUgyFunctions.GetSzembesitesiJegyzokonyv(fegyelmiUgyIds, naplobejegyzesIds);
                if (model.Leiras != null) model.Leiras = model.Leiras.Replace("<br/>", "\n");
                if (model.Leiras != null) model.Leiras = model.Leiras.Replace("<br />", "\n");

                if (model.Leiras == null) model.Leiras = "";

                return Json(model);
            }
            catch (WarningException e)
            {
                return Json(new { isSuccess = false, message = e.Message });
            }
        }

        [HttpPost]
        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.SaveSzembesitesiJegyzokonyvModalData, DbModositasTortenik = true, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja, Jogosultsagok.Fegyelmi_reintegracios_tiszt)]
        public JsonResult SaveSzembesitesiJegyzokonyvModalData(SzembesitesiJegyzokonyvModel model)
        {
            model.Leiras = model.Leiras.Replace("\n", "<br />");
            var naplobejegyzesIds = FegyelmiUgyFunctions.SaveSzembesitesiJegyzokonyv(model);

            return Json(new { naplobejegyzesIds = naplobejegyzesIds });
        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.GetHatarozatRogziteseMasodfokonModalData, DbModositasTortenik = false, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja)]
        public JsonResult GetHatarozatRogziteseMasodfokonModalData(List<int> fegyelmiUgyIds, List<int> naplobejegyzesIds)
        {
            bool warning = EsemenyResztvevoFunctions.WarningTanuVagyEszleloByFegyelmiUgyIds(fegyelmiUgyIds);
            if (warning)
            {
                return Json(new { isSuccess = false, message = "Az ügyben Ön észlelőként vagy tanúként van megjelölve, ezért nem végezhet benne vizsgálati cselekményt." });
            }
            var model = FegyelmiUgyFunctions.GetHatarozatRogziteseMasodfokon(fegyelmiUgyIds, naplobejegyzesIds);
            return Json(model);
        }


        [HttpPost]
        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.SaveHatarozatRogziteseMasodfokonModalData, DbModositasTortenik = true, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja)]
        public JsonResult SaveHatarozatRogziteseMasodfokonModalData(FegyelmiUgyHatarozatRogziteseMasodfokonModel model)
        {
            FegyelmiUgyFunctions.ThrowExceptionIfFegyelmiUgyNemModosithato(model.FegyelmiUgyIds);
            model.Leiras = model.Leiras.Replace("\n", "<br />");
            FegyelmiUgyFunctions.SaveMasodFokuHatarozat(model);

            return Json(new { naplobejegyzesIds = model.NaplobejegyzesIds });
        }

        [HttpPost]
        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.GetUgyesziHatalyonKivulHelyezesModalData, DbModositasTortenik = false, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja)]
        public JsonResult GetUgyesziHatalyonKivulHelyezesModalData(List<int> fegyelmiUgyIds)
        {
            FegyelmiUgyFunctions.ThrowExceptionIfFegyelmiUgyNemModosithato(fegyelmiUgyIds);
            var model = FegyelmiUgyFunctions.GetUgyesziHatalyonKivulHelyezes(fegyelmiUgyIds);

            return Json(new { naplobejegyzesIds = model.NaplobejegyzesIds });
        }

        [HttpPost]
        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.SaveHatalyonKivulHelyezes, DbModositasTortenik = true, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja)]
        public JsonResult SaveHatalyonKivulHelyezes(FegyelmiNaplobejegyzesFelviteleModel model)
        {
            FegyelmiUgyFunctions.ThrowExceptionIfFegyelmiUgyNemModosithato(model.FegyelmiUgyIds);
            var ret = FegyelmiUgyFunctions.SaveHatalyonKivulHelyezes(model);

            return Json(new { naplobejegyzesIds = ret });
        }

        [HttpPost]
        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.SaveSzakteruletiVelemenyKereseModalData, DbModositasTortenik = true, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja)]
        public JsonResult SaveSzakteruletiVelemenyKereseModalData(SzakteruletiVelemenyFelkeresModel model)
        {
            ThrowValidationExceptionIfNotValid();
            model.Leiras = model.Leiras.Replace("\n", "<br />");

            var naplobejegyzesIds = FegyelmiUgyFunctions.SaveSzakteruletiVelemenyKereseModalData(model);
            return Json(new { naplobejegyzesIds = naplobejegyzesIds });
        }

        [HttpPost]
        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.FindSzakteruletiVezetokForSelect, DbModositasTortenik = false, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja)]
        public JsonResult FindSzakteruletiVezetokForSelect(string term)
        {
            var lista = FegyelmiUgyFunctions.FindSzakteruletiVezetokForSelect(term);
            return Json(lista);
        }

        //[AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.GetFegyelmiUgyIdsByVelemenykeresId, DbModositasTortenik = false, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_egyeb_szakterulet)]
        public JsonResult GetFegyelmiUgyIdsByVelemenykeresId(int velemenykeresId)
        {
            int intezetId = 0;
            var fegyelmiUgyek = SzakteruletiVelemenyKeresekFunctions.GetFegyelmiUgyIdsByVelemenykeresId(velemenykeresId);
            if (fegyelmiUgyek.Count > 0)
            {
                intezetId = FegyelmiUgyFunctions.FindById(fegyelmiUgyek[0]).IntezetId;
            }
            return Json(new { fegyelmiUgyek = fegyelmiUgyek, intezetId = intezetId });
        }

        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja,
                           Jogosultsagok.Fegyelmi_egyeb_szakterulet,
                           Jogosultsagok.Fegyelmi_fofelugyelo,
                           Jogosultsagok.Fegyelmi_reintegracios_tiszt,
                           Jogosultsagok.Jfk_fegyjutmegtekinto)]
        public JsonResult GetaktivitasFolyamList()
        {
            var model = FegyelmiUgyFunctions.GetAktivitasFolyamList(AlkalmazasKontextusFunctions.Kontextus.RogzitoIntezetId, null);
            return Json(model);
        }

        [HttpPost]
        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.GetMaganelzarasMegkezdesenekRogziteseModalData, DbModositasTortenik = false, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_fofelugyelo)]
        public ContentResult GetMaganelzarasMegkezdesenekRogziteseModalData(List<int> fegyelmiUgyIds, List<int> naplobejegyzesIds)
        {
            bool warning = EsemenyResztvevoFunctions.WarningTanuVagyEszleloByFegyelmiUgyIds(fegyelmiUgyIds);
            //var warnModel = new { isSuccess = false, message = "Az ügyben Ön észlelőként vagy tanúként van megjelölve, ezért nem végezhet benne vizsgálati cselekményt." };
            //var warn = JsonConvert.SerializeObject(warnModel,
            //                Newtonsoft.Json.Formatting.None,
            //                new JsonSerializerSettings
            //                {
            //                    NullValueHandling = NullValueHandling.Ignore
            //                });
            //if (warning)
            //{
            //    return Content(warn, "application/json");
            //}
            var modalData = FegyelmiUgyFunctions.GetMaganelzarasMegkezdesenekRogziteseModalData(fegyelmiUgyIds, naplobejegyzesIds);
            var model = JsonConvert.SerializeObject(modalData,
                            Newtonsoft.Json.Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });
            return Content(model, "application/json");
        }

        [HttpPost]
        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.SaveMaganelzarasMegkezdesenekRogziteseModalData, DbModositasTortenik = true, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_fofelugyelo)]
        public JsonResult SaveMaganelzarasMegkezdesenekRogziteseModalData(MaganelzarasMegkezdesenekRogziteseModel model)
        {
            ThrowValidationExceptionIfNotValid();
            //try
            //{
                var naplobejegyzesIds = FegyelmiUgyFunctions.SaveMaganelzarasMegkezdesenekRogziteseModalData(model);
                return Json(new { naplobejegyzesIds = naplobejegyzesIds });
            //}
            //catch (F3ZarkabaHelyezesFunctions.VegrehajtasiFokException ex)
            //{
            //    return Json(new ResultModel
            //    {
            //        Message = ex.Message,
            //        Status = 3,
            //        Details = new { Kerdes = true }
            //    });
            //}
        }

        [HttpPost]
        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.GetKozvetitoiEljarasKezdemenyezeseModalData, DbModositasTortenik = false, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja, Jogosultsagok.Fegyelmi_reintegracios_tiszt)]
        public JsonResult GetKozvetitoiEljarasKezdemenyezeseModalData(List<int> fegyelmiUgyIds, List<int> naplobejegyzesIds)
        {
            bool warning = EsemenyResztvevoFunctions.WarningTanuVagyEszleloByFegyelmiUgyIds(fegyelmiUgyIds);
            if (warning)
            {
                return Json(new { isSuccess = false, message = "Az ügyben Ön észlelőként vagy tanúként van megjelölve, ezért nem végezhet benne vizsgálati cselekményt." });
            }
            var model = FegyelmiUgyFunctions.GetKozvetitoiEljarasKezdemenyezeseModalData(fegyelmiUgyIds, naplobejegyzesIds);
            if (model.Leiras != null) model.Leiras = model.Leiras.Replace("<br/>", "\n");
            if (model.Leiras != null) model.Leiras = model.Leiras.Replace("<br />", "\n");

            if (model.Leiras == null) model.Leiras = "";

            return Json(model);
        }

        [HttpPost]
        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.SaveKozvetitoiEljarasKezdemenyezeseModalData, DbModositasTortenik = true, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja, Jogosultsagok.Fegyelmi_reintegracios_tiszt)]
        public JsonResult SaveKozvetitoiEljarasKezdemenyezeseModalData(KozvetitoiEljarasKezdemenyezeseModel model)
        {
            ThrowValidationExceptionIfNotValid();
            FegyelmiUgyFunctions.ThrowExceptionIfFegyelmiUgyNemModosithato(model.FegyelmiUgyIds);
            model.Leiras = model.Leiras.Replace("\n", "<br />");

            var naplobejegyzesIds = FegyelmiUgyFunctions.SaveKozvetitoiEljarasKezdemenyezeseModalData(model);
            return Json(new { naplobejegyzesIds });
        }

        [HttpPost]
        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.GetMaganelzarasVegrehajtvaModalData, DbModositasTortenik = false, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_fofelugyelo)]
        public ContentResult GetMaganelzarasVegrehajtvaModalData(List<int> fegyelmiUgyIds, List<int> naplobejegyzesIds)
        {
            //bool warning = EsemenyResztvevoFunctions.WarningTanuVagyEszleloByFegyelmiUgyIds(fegyelmiUgyIds);
            //var warnModel = new { isSuccess = false, message = "Az ügyben Ön észlelőként vagy tanúként van megjelölve, ezért nem végezhet benne vizsgálati cselekményt." };
            //var warn = JsonConvert.SerializeObject(warnModel,
            //                Newtonsoft.Json.Formatting.None,
            //                new JsonSerializerSettings
            //                {
            //                    NullValueHandling = NullValueHandling.Ignore
            //                });
            //if (warning)
            //{
            //    return Content(warn, "application/json");
            //}
            var modalData = FegyelmiUgyFunctions.GetMaganelzarasVegrehajtvaModalData(fegyelmiUgyIds, naplobejegyzesIds);
            var model = JsonConvert.SerializeObject(modalData,
                            Newtonsoft.Json.Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });
            return Content(model, "application/json");
        }

        [HttpPost]
        //[AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.CheckMaganelzarasVegrehajtvaModalData, DbModositasTortenik = false, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_fofelugyelo)]
        public JsonResult CheckMaganelzarasVegrehajtvaModalData(MaganelzarasVegrehajtvaModel model)
        {
            bool dohanyzoZarkaba = false;
            bool nemDohanyzoZarkaba = false;
            bool noFerfibe = false;
            bool ferfiNoibe = false;

            var zarkaId = Int32.Parse(model.ZarkabaHelyezes.Split('_')[2]);
            var zarka = FegyelmiUgyFunctions.GetZarkaById(zarkaId);

            var fegyelmiUgy = FegyelmiUgyFunctions.FindById(model.FegyelmiUgyIds[0]);
            var fogvatartott = FegyelmiUgyFunctions.GetFogvatartottZarkahoz(fegyelmiUgy.FogvatartottId);
            

            if (zarka.NemeKszId != null && zarka.NemeKszId != fogvatartott.FogvSzemAdatok.LastOrDefault()?.NemId)
            {
                if (zarka.NemeKszId == 823) noFerfibe = true;
                if (zarka.NemeKszId == 824) ferfiNoibe = true;
            }

            if (zarka.Dohanyzo == true && fogvatartott.IsDohanyzik == false)
                dohanyzoZarkaba = true;
            if (zarka.Dohanyzo == false && fogvatartott.IsDohanyzik == true)
                nemDohanyzoZarkaba = true;

            return Json(new { dohanyzoZarkaba, nemDohanyzoZarkaba, noFerfibe, ferfiNoibe });
        }

        [HttpPost]
        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.SaveMaganelzarasVegrehajtvaModalData, DbModositasTortenik = true, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_fofelugyelo)]
        public JsonResult SaveMaganelzarasVegrehajtvaModalData(MaganelzarasVegrehajtvaModel model)
        {
            ThrowValidationExceptionIfNotValid();
            //try
            //{
                var naplobejegyzesIds = FegyelmiUgyFunctions.SaveMaganelzarasVegrehajtvaModalData(model);
                return Json(new { naplobejegyzesIds });
            //}
            //catch (F3ZarkabaHelyezesFunctions.VegrehajtasiFokException ex)
            //{
            //    return Json(new ResultModel
            //    {
            //        Message = ex.Message,
            //        Status = 3,
            //        Details = new { Kerdes = true }
            //    });
            //}
        }

        [HttpPost]
        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.GetFenyitesNemVegrehajthatoModalData, DbModositasTortenik = false, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja)]
        public JsonResult GetFenyitesNemVegrehajthatoModalData(List<int> fegyelmiUgyIds, List<int> naplobejegyzesIds)
        {
            bool warning = EsemenyResztvevoFunctions.WarningTanuVagyEszleloByFegyelmiUgyIds(fegyelmiUgyIds);
            if (warning)
            {
                return Json(new { isSuccess = false, message = "Az ügyben Ön észlelőként vagy tanúként van megjelölve, ezért nem végezhet benne vizsgálati cselekményt." });
            }
            var model = FegyelmiUgyFunctions.GetFenyitesNemVegrehajthatoModalData(fegyelmiUgyIds, naplobejegyzesIds);
            if (model.Leiras != null) model.Leiras = model.Leiras.Replace("<br/>", "\n");
            if (model.Leiras != null) model.Leiras = model.Leiras.Replace("<br />", "\n");

            if (model.Leiras == null) model.Leiras = "";

            return Json(model);
        }

        [HttpPost]
        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.SaveFenyitesNemVegrehajthatoModalData, DbModositasTortenik = true, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja)]
        public JsonResult SaveFenyitesNemVegrehajthatoModalData(FenyitesNemVegrehajthatoModel model)
        {
            ThrowValidationExceptionIfNotValid();
            model.Leiras = model.Leiras.Replace("\n", "<br />");
            var naplobejegyzesIds = FegyelmiUgyFunctions.SaveFenyitesNemVegrehajthatoModalData(model);
            return Json(new { naplobejegyzesIds = naplobejegyzesIds });
        }

        [HttpPost]
        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.GetMaganelzarasEllenjavallataModalData, DbModositasTortenik = false, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja)]
        public JsonResult GetMaganelzarasEllenjavallataModalData(List<int> fegyelmiUgyIds, List<int> naplobejegyzesIds)
        {
            bool warning = EsemenyResztvevoFunctions.WarningTanuVagyEszleloByFegyelmiUgyIds(fegyelmiUgyIds);
            if (warning)
            {
                return Json(new { isSuccess = false, message = "Az ügyben Ön észlelőként vagy tanúként van megjelölve, ezért nem végezhet benne vizsgálati cselekményt." });
            }
            var model = FegyelmiUgyFunctions.GetMaganelzarasEllenjavallataModalData(fegyelmiUgyIds, naplobejegyzesIds);
            if (model.Leiras != null) model.Leiras = model.Leiras.Replace("<br/>", "\n");
            if (model.Leiras != null) model.Leiras = model.Leiras.Replace("<br />", "\n");

            if (model.Leiras == null) model.Leiras = "";

            return Json(model);
        }

        [HttpPost]
        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.SaveMaganelzarasEllenjavallataModalData, DbModositasTortenik = true, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja)]
        public JsonResult SaveMaganelzarasEllenjavallataModalData(MaganelzarasEllenjavallataModel model)
        {
            ThrowValidationExceptionIfNotValid();
            model.Leiras = model.Leiras.Replace("\n", "<br />");
            var naplobejegyzesIds = FegyelmiUgyFunctions.SaveMaganelzarasEllenjavallataModalData(model);
            return Json(new { naplobejegyzesIds = naplobejegyzesIds });
        }

        [HttpPost]
        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.GetDontesKozvetitoiEljarasrolModalData, DbModositasTortenik = false, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja)]
        public JsonResult GetDontesKozvetitoiEljarasrolModalData(List<int> fegyelmiUgyIds, List<int> naplobejegyzesIds)
        {
            bool warning = EsemenyResztvevoFunctions.WarningTanuVagyEszleloByFegyelmiUgyIds(fegyelmiUgyIds);
            if (warning)
            {
                return Json(new { isSuccess = false, message = "Az ügyben Ön észlelőként vagy tanúként van megjelölve, ezért nem végezhet benne vizsgálati cselekményt." });
            }
            var model = FegyelmiUgyFunctions.GetDontesKozvetitoiEljarasrolModalData(fegyelmiUgyIds, naplobejegyzesIds);
            return Json(model);
        }

        [HttpPost]
        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.SaveDontesKozvetitoiEljarasrolModalData, DbModositasTortenik = true, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja)]
        public JsonResult SaveDontesKozvetitoiEljarasrolModalData(DontesKozvetitoiEljarasrolModel model)
        {
            ThrowValidationExceptionIfNotValid();
            FegyelmiUgyFunctions.ThrowExceptionIfFegyelmiUgyNemModosithato(model.FegyelmiUgyIds);

            var naplobejegyzesIds = FegyelmiUgyFunctions.SaveDontesKozvetitoiEljarasrolModalData(model);
            return Json(new { naplobejegyzesIds });
        }

        [HttpPost]
        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.GetMaganelzarasMegszakitasaModalData, DbModositasTortenik = false, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_fofelugyelo)]
        public ContentResult GetMaganelzarasMegszakitasaModalData(List<int> fegyelmiUgyIds, List<int> naplobejegyzesIds)
        {
            //bool warning = EsemenyResztvevoFunctions.WarningTanuVagyEszleloByFegyelmiUgyIds(fegyelmiUgyIds);
            //var warnModel = new { isSuccess = false, message = "Az ügyben Ön észlelőként vagy tanúként van megjelölve, ezért nem végezhet benne vizsgálati cselekményt." };
            //var warn = JsonConvert.SerializeObject(warnModel,
            //                Newtonsoft.Json.Formatting.None,
            //                new JsonSerializerSettings
            //                {
            //                    NullValueHandling = NullValueHandling.Ignore
            //                });
            //if (warning)
            //{
            //    return Content(warn, "application/json");
            //}
            var modalData = FegyelmiUgyFunctions.GetMaganelzarasMegszakitasanakRogziteseModalData(fegyelmiUgyIds, naplobejegyzesIds);
            if (modalData.Indoklas != null) modalData.Indoklas = modalData.Indoklas.Replace("<br/>", "\n");
            if (modalData.Indoklas != null) modalData.Indoklas = modalData.Indoklas.Replace("<br />", "\n");

            if (modalData.Indoklas == null) modalData.Indoklas = "";

            var model = JsonConvert.SerializeObject(modalData,
                            Newtonsoft.Json.Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });
            return Content(model, "application/json");
        }

        [HttpPost]
        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.SaveMaganelzarasMegszakitasaModalData, DbModositasTortenik = true, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_fofelugyelo)]
        public JsonResult SaveMaganelzarasMegszakitasaModalData(MaganelzarasMegszakitasanakRogziteseModel model)
        {
            ThrowValidationExceptionIfNotValid();
            //try { 
                model.Indoklas = model.Indoklas.Replace("\n", "<br />");
                var naplobejegyzesIds = FegyelmiUgyFunctions.SaveMaganelzarasMegszakitasanakRogziteseModalData(model);
                return Json(new { naplobejegyzesIds = naplobejegyzesIds });
            //}
            //catch (F3ZarkabaHelyezesFunctions.VegrehajtasiFokException ex)
            //{
            //    return Json(new ResultModel
            //    {
            //        Message = ex.Message,
            //        Status = 3,
            //        Details = new { Kerdes = true }
            //    });
            //}
        }

        [HttpPost]
        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.GetKozvetitoiEljarasGaranciaTeljesulesenekRogziteseModalData, DbModositasTortenik = false, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja, Jogosultsagok.Fegyelmi_reintegracios_tiszt)]
        public JsonResult GetKozvetitoiEljarasGaranciaTeljesulesenekRogziteseModalData(List<int> fegyelmiUgyIds, List<int> naplobejegyzesIds)
        {
            bool warning = EsemenyResztvevoFunctions.WarningTanuVagyEszleloByFegyelmiUgyIds(fegyelmiUgyIds);
            if (warning)
            {
                return Json(new { isSuccess = false, message = "Az ügyben Ön észlelőként vagy tanúként van megjelölve, ezért nem végezhet benne vizsgálati cselekményt." });
            }
            var model = FegyelmiUgyFunctions.GetKozvetitoiEljarasGaranciaTeljesulesenekRogziteseModalData(fegyelmiUgyIds, naplobejegyzesIds);
            return Json(model);
        }

        [HttpPost]
        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.SaveKozvetitoiEljarasGaranciaTeljesulesenekRogziteseModalData, DbModositasTortenik = true, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja, Jogosultsagok.Fegyelmi_reintegracios_tiszt)]
        public JsonResult SaveKozvetitoiEljarasGaranciaTeljesulesenekRogziteseModalData(KozvetitoiEljarasGaranciaTeljesulesenekRogziteseModel model)
        {
            ThrowValidationExceptionIfNotValid();
            var naplobejegyzesIds = FegyelmiUgyFunctions.SaveKozvetitoiEljarasGaranciaTeljesulesenekRogziteseModalData(model);
            return Json(new { naplobejegyzesIds = naplobejegyzesIds });
        }

        [HttpPost]
        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.GetFeljegyzesEsMegallapodasModalData, DbModositasTortenik = false, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja, Jogosultsagok.Fegyelmi_reintegracios_tiszt)]
        public JsonResult GetFeljegyzesEsMegallapodasModalData(List<int> fegyelmiUgyIds, List<int> naplobejegyzesIds)
        {
            bool warning = EsemenyResztvevoFunctions.WarningTanuVagyEszleloByFegyelmiUgyIds(fegyelmiUgyIds);
            if (warning)
            {
                return Json(new { isSuccess = false, message = "Az ügyben Ön észlelőként vagy tanúként van megjelölve, ezért nem végezhet benne vizsgálati cselekményt." });
            }
            var model = FegyelmiUgyFunctions.GetFeljegyzesEsMegallapodasModalData(fegyelmiUgyIds, naplobejegyzesIds);
            if (model.Megallapodas != null) model.Megallapodas = model.Megallapodas.Replace("<br/>", "\n");
            if (model.Megallapodas != null) model.Megallapodas = model.Megallapodas.Replace("<br />", "\n");
            if (model.FeljegyzesMegbeszelesrol != null) model.FeljegyzesMegbeszelesrol = model.FeljegyzesMegbeszelesrol.Replace("<br/>", "\n");
            if (model.FeljegyzesMegbeszelesrol != null) model.FeljegyzesMegbeszelesrol = model.FeljegyzesMegbeszelesrol.Replace("<br />", "\n");

            if (model.Megallapodas == null) model.Megallapodas = "";

            return Json(model);
        }

        [HttpPost]
        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.SaveFeljegyzesEsMegallapodasModalData, DbModositasTortenik = true, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja, Jogosultsagok.Fegyelmi_reintegracios_tiszt)]
        public JsonResult SaveFeljegyzesEsMegallapodasModalData(FeljegyzesEsMegallapodasModel model)
        {
            ThrowValidationExceptionIfNotValid();
            FegyelmiUgyFunctions.ThrowExceptionIfFegyelmiUgyNemModosithato(model.FegyelmiUgyIds);
            model.Megallapodas = model.Megallapodas.Replace("\n", "<br />");
            model.FeljegyzesMegbeszelesrol = model.FeljegyzesMegbeszelesrol.Replace("\n", "<br />");

            var naplobejegyzesIds = FegyelmiUgyFunctions.SaveFeljegyzesEsMegallapodasModalData(model);
            return Json(new { naplobejegyzesIds });
        }
        [HttpPost]
        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.GetIndoklassalMegszuntetesModalData, DbModositasTortenik = false, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja)]
        public JsonResult GetIndoklassalMegszuntetesModalData(List<int> fegyelmiUgyIds, List<int> naplobejegyzesIds)
        {
            bool warning = EsemenyResztvevoFunctions.WarningTanuVagyEszleloByFegyelmiUgyIds(fegyelmiUgyIds);
            if (warning)
            {
                return Json(new { isSuccess = false, message = "Az ügyben Ön észlelőként vagy tanúként van megjelölve, ezért nem végezhet benne vizsgálati cselekményt." });
            }
            var model = FegyelmiUgyFunctions.GetIndoklassalMegszuntetesModalData(fegyelmiUgyIds, naplobejegyzesIds);
            if (model.Indoklas != null) model.Indoklas = model.Indoklas.Replace("<br/>", "\n");
            if (model.Indoklas != null) model.Indoklas = model.Indoklas.Replace("<br />", "\n");

            if (model.Indoklas == null) model.Indoklas = "";

            return Json(model);
        }

        [HttpPost]
        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.SaveIndoklassalMegszuntetesModalData, DbModositasTortenik = true, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja)]
        public JsonResult SaveIndoklassalMegszuntetesModalData(IndoklassalMegszuntetesModel model)
        {
            ThrowValidationExceptionIfNotValid();
            FegyelmiUgyFunctions.ThrowExceptionIfFegyelmiUgyNemModosithato(model.FegyelmiUgyIds);
            model.Indoklas = model.Indoklas.Replace("\n", "<br />");

            var naplobejegyzesIds = FegyelmiUgyFunctions.SaveIndoklassalMegszuntetesModalData(model);
            return Json(new { naplobejegyzesIds });
        }

        [HttpPost]
        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.GetKozvetitoiEljarasHataridoModositasKereseModalData, DbModositasTortenik = false, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja, Jogosultsagok.Fegyelmi_reintegracios_tiszt)]
        public JsonResult GetKozvetitoiEljarasHataridoModositasKereseModalData(List<int> fegyelmiUgyIds, List<int> naplobejegyzesIds)
        {
            bool warning = EsemenyResztvevoFunctions.WarningTanuVagyEszleloByFegyelmiUgyIds(fegyelmiUgyIds);
            if (warning)
            {
                return Json(new { isSuccess = false, message = "Az ügyben Ön észlelőként vagy tanúként van megjelölve, ezért nem végezhet benne vizsgálati cselekményt." });
            }
            var model = FegyelmiUgyFunctions.GetKozvetitoiEljarasHataridoModositasKereseModalData(fegyelmiUgyIds, naplobejegyzesIds);
            if (model.Leiras != null) model.Leiras = model.Leiras.Replace("<br/>", "\n");
            if (model.Leiras != null) model.Leiras = model.Leiras.Replace("<br />", "\n");

            if (model.Leiras == null) model.Leiras = "";

            return Json(model);
        }

        [HttpPost]
        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.SaveKozvetitoiEljarasHataridoModositasKereseModalData, DbModositasTortenik = true, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja, Jogosultsagok.Fegyelmi_reintegracios_tiszt)]
        public JsonResult SaveKozvetitoiEljarasHataridoModositasKereseModalData(KozvetitoiEljarasHataridoModositasKereseModel model)
        {
            ThrowValidationExceptionIfNotValid();
            model.Leiras = model.Leiras.Replace("\n", "<br />");
            var naplobejegyzesIds = FegyelmiUgyFunctions.SaveKozvetitoiEljarasHataridoModositasKereseModalData(model);
            return Json(new { naplobejegyzesIds = naplobejegyzesIds });
        }

        [HttpPost]
        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.GetKozvetitoiEljarasLezarasaModalData, DbModositasTortenik = false, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja)]
        public JsonResult GetKozvetitoiEljarasLezarasaModalData(List<int> fegyelmiUgyIds, List<int> naplobejegyzesIds)
        {
            bool warning = EsemenyResztvevoFunctions.WarningTanuVagyEszleloByFegyelmiUgyIds(fegyelmiUgyIds);
            if (warning)
            {
                return Json(new { isSuccess = false, message = "Az ügyben Ön észlelőként vagy tanúként van megjelölve, ezért nem végezhet benne vizsgálati cselekményt." });
            }
            var model = FegyelmiUgyFunctions.GetKozvetitoiEljarasLezarasaModalData(fegyelmiUgyIds, naplobejegyzesIds);
            return Json(model);
        }

        [HttpPost]
        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.SaveKozvetitoiEljarasLezarasaModalData, DbModositasTortenik = true, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja)]
        public JsonResult SaveKozvetitoiEljarasLezarasaModalData(KozvetitoiEljarasLezarasaModel model)
        {
            ThrowValidationExceptionIfNotValid();
            var naplobejegyzesIds = FegyelmiUgyFunctions.SaveKozvetitoiEljarasLezarasaModalData(model);
            return Json(new { naplobejegyzesIds });
        }


        [HttpPost]
        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.GetKozvetitoiEljarasHataridoModositasModalData, DbModositasTortenik = false, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja)]
        public JsonResult GetKozvetitoiEljarasHataridoModositasModalData(List<int> fegyelmiUgyIds, List<int> naplobejegyzesIds)
        {
            bool warning = EsemenyResztvevoFunctions.WarningTanuVagyEszleloByFegyelmiUgyIds(fegyelmiUgyIds);
            if (warning)
            {
                return Json(new { isSuccess = false, message = "Az ügyben Ön észlelőként vagy tanúként van megjelölve, ezért nem végezhet benne vizsgálati cselekményt." });
            }
            var model = FegyelmiUgyFunctions.GetKozvetitoiEljarasHataridoModositasModalData(fegyelmiUgyIds, naplobejegyzesIds);
            if (model.Leiras != null) model.Leiras = model.Leiras.Replace("<br/>", "\n");
            if (model.Leiras != null) model.Leiras = model.Leiras.Replace("<br />", "\n");

            if (model.Leiras == null) model.Leiras = "";

            return Json(model);
        }

        [HttpPost]
        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.SaveKozvetitoiEljarasHataridoModositasModalData, DbModositasTortenik = true, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja)]
        public JsonResult SaveKozvetitoiEljarasHataridoModositasModalData(KozvetitoiEljarasGaranciaHataridoModositasaModel model)
        {
            ThrowValidationExceptionIfNotValid();
            model.Leiras = model.Leiras.Replace("\n", "<br />");

            var naplobejegyzesIds = FegyelmiUgyFunctions.SaveKozvetitoiEljarasHataridoModositasModalData(model);
            return Json(new { naplobejegyzesIds = naplobejegyzesIds });
        }

        [HttpPost]
        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.FelfuggesztesMegszunteteseConfrimModal, DbModositasTortenik = false, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja, Jogosultsagok.Fegyelmi_reintegracios_tiszt)]
        public JsonResult FelfuggesztesMegszunteteseConfrimModal(List<int> fegyelmiUgyIds, DateTime hatarido)
        {
            FegyelmiUgyFunctions.JogiKepviseletVisszavonasa(fegyelmiUgyIds);
            return Json(new { success = true });
        }

        [HttpPost]
        //[AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.GetFegyelmiHatarido, DbModositasTortenik = false, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja, Jogosultsagok.Fegyelmi_reintegracios_tiszt)]
        public JsonResult GetFegyelmiHatarido(List<int> fegyelmiUgyIds)
        {
            List<DateTime> hartaridok = new List<DateTime>();
            foreach (var fegyelmiUgyId in fegyelmiUgyIds)
            {
                var hatarido = FegyelmiUgyFunctions.GetFegyelmiHatarido(fegyelmiUgyId, false);
                hartaridok.Add(hatarido.Value);
            }
            return Json(hartaridok);
        }

        [HttpPost]
        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.SaveFelfuggesztesMegszunteteseConfrimModal, DbModositasTortenik = true, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja)]
        public JsonResult SaveFelfuggesztesMegszunteteseConfrimModal(List<int> fegyelmiUgyIds)
        {
            FegyelmiUgyFunctions.FelfuggesztesMegszunteteseConfrimModal(fegyelmiUgyIds);
            return Json(new { success = true });
        }




        [HttpPost]
        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.GetBuntetoFeljelentesRogziteseModalData, DbModositasTortenik = false, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja)]
        public JsonResult GetBuntetoFeljelentesRogziteseModalData(List<int> fegyelmiUgyIds, List<int> naplobejegyzesIds)
        {
            bool warning = EsemenyResztvevoFunctions.WarningTanuVagyEszleloByFegyelmiUgyIds(fegyelmiUgyIds);
            if (warning)
            {
                return Json(new { isSuccess = false, message = "Az ügyben Ön észlelőként vagy tanúként van megjelölve, ezért nem végezhet benne vizsgálati cselekményt." });
            }
            var model = FegyelmiUgyFunctions.GetBuntetoFeljelentesRogziteseModalData(fegyelmiUgyIds, naplobejegyzesIds);

            if (model.Leiras == null) model.Leiras = "";

            model.Leiras = $"{model.Leiras}";

            return Json(model);
        }

        [HttpPost]
        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.SaveBuntetoFeljelentesRogziteseModalData, DbModositasTortenik = true, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja)]
        public JsonResult SaveBuntetoFeljelentesRogziteseModalData(FeljelentesRogziteseModel model)
        {
            ThrowValidationExceptionIfNotValid();
            var naplobejegyzesIds = FegyelmiUgyFunctions.SaveBuntetoFeljelentesRogziteseModalData(model);
            return Json(new { naplobejegyzesIds = naplobejegyzesIds });
        }

        [HttpPost]
        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.GetFegyelmiElkulonitesElrendeleseMegszuntetese, DbModositasTortenik = false, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja)]
        public ContentResult GetFegyelmiElkulonitesElrendeleseMegszuntetese(List<int> fegyelmiUgyIds, List<int> naploIds)
        {
            bool warning = EsemenyResztvevoFunctions.WarningTanuVagyEszleloByFegyelmiUgyIds(fegyelmiUgyIds);
            var warnModel = new { isSuccess = false, message = "Az ügyben Ön észlelőként vagy tanúként van megjelölve, ezért nem végezhet benne vizsgálati cselekményt." };
            var warn = JsonConvert.SerializeObject(warnModel,
                            Newtonsoft.Json.Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });
            if (warning)
            {
                return Content(warn, "application/json");
            }
            var modalData = FegyelmiUgyFunctions.GetFegyelmiElkulonitesElrendelesFelulvizsgataMegszuntetese(fegyelmiUgyIds, naploIds);
            if (modalData.ElkulonitesOka != null) modalData.ElkulonitesOka = modalData.ElkulonitesOka.Replace("<br/>", "\n");
            if (modalData.ElkulonitesOka != null) modalData.ElkulonitesOka = modalData.ElkulonitesOka.Replace("<br />", "\n");
            if (modalData.Rendelkezesek != null) modalData.Rendelkezesek = modalData.Rendelkezesek.Replace("<br/>", "\n");
            if (modalData.Rendelkezesek != null) modalData.Rendelkezesek = modalData.Rendelkezesek.Replace("<br />", "\n");

            if (modalData.ElkulonitesOka == null) modalData.ElkulonitesOka = "";
            if (modalData.Rendelkezesek == null) modalData.Rendelkezesek = "";

            var model = JsonConvert.SerializeObject(modalData,
                            Newtonsoft.Json.Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });

            return Content(model, "application/json");
        }

        [HttpPost]
        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.SaveElkulonitesElrendelesEsMegszuntetese, DbModositasTortenik = true, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja)]
        public JsonResult SaveElkulonitesElrendelesEsMegszuntetese(FegyelmiElkulonitesElrendeleseFelulvizsgalataMegszunteteseModel model)
        {
            //try
            //{
                if (model.ElkulonitesOka != null)
                {
                    model.ElkulonitesOka = model.ElkulonitesOka.Replace("\n", "<br />");
                }
                if (model.Rendelkezesek != null)
                {
                    model.Rendelkezesek = model.Rendelkezesek.Replace("\n", "<br />");
                }
                var naploIds = FegyelmiUgyFunctions.SaveElkulonitesElrendelesVagyElkulonitesMegszuntetes(model);
                return Json(naploIds);
            //}
            //catch (F3ZarkabaHelyezesFunctions.VegrehajtasiFokException ex)
            //{
            //    return Json(new ResultModel
            //    {
            //        Message = ex.Message,
            //        Status = 3,
            //        Details = new { Kerdes = true }
            //    });
            //}
        }

        [HttpPost]
        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.GetMaganelzarasElrendeleseModalData, DbModositasTortenik = false, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja)]
        public JsonResult GetMaganelzarasElrendeleseModalData(List<int> fegyelmiUgyIds, List<int> naplobejegyzesIds)
        {
            bool warning = EsemenyResztvevoFunctions.WarningTanuVagyEszleloByFegyelmiUgyIds(fegyelmiUgyIds);
            if (warning)
            {
                return Json(new { isSuccess = false, message = "Az ügyben Ön észlelőként vagy tanúként van megjelölve, ezért nem végezhet benne vizsgálati cselekményt." });
            }
            var fegyelmiJogkorGyakorlojaUsers = SzemelyzetFunctions.GetFegyelmiJogkorGyakorlojaAlkalmazottak();
            var user = fegyelmiJogkorGyakorlojaUsers.First(f => f.Sid == AlkalmazasKontextusFunctions.Kontextus.SzemelyzetSid);
            var fegyelmiUgy = FegyelmiUgyFunctions.FindById(fegyelmiUgyIds.First());
            var fofelugyelok = SzemelyzetFunctions.GetFegyelmiFofelugyelokAlkalmazottak();

            var modalData = new MaganelzarasElrendeleseModel()
            {
                FegyelmiJogkorGyakorloId = user.Sid,
                FegyelmiJogkorGyakorloNeve = user.Displayname.ToTitleCase() + ' ' + user.Rendfokozat,
                TervezettDatum = DateTime.Now,
                HatarozatDatum = fegyelmiUgy.HatarozatDatuma.Value,
                Fofelugyelok = new List<string>(),
                FofelugyelokDefault = new List<KSelect2ItemModel>(),
            };
            //modalData.FofelugyelokOptions = fofelugyelok
            //                       .Select(x => new KSelect2ItemModel()
            //                       {
            //                           id = x.Sid.ToString(),
            //                           text = x.Email != null ? x.Displayname + " " + ("<span class=\"fa-envelope\" title=\"Van e-mail címe.\"></span>") : x.Displayname + " " + ("<span class=\"fa-ban\" title=\"Nincs e-mail címe.\"></span>"),
            //                       })
            //                       .ToList();

            modalData.FofelugyelokOptions = fofelugyelok
                                   .Select(x => new KSelect2ItemModel()
                                   {
                                       id = x.Sid.ToString(),
                                       text = x.Displayname,
                                   })
                                   .ToList();
            return Json(modalData);
        }

        [HttpPost]
        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.SaveMaganelzarasElrendeleseModalData, DbModositasTortenik = true, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja)]
        public JsonResult SaveMaganelzarasElrendeleseModalData(MaganelzarasElrendeleseModel model)
        {
            ThrowValidationExceptionIfNotValid();
            var naplobejegyzesIds = FegyelmiUgyFunctions.SaveMaganelzarasElrendeleseModalData(model);
            return Json(new { naplobejegyzesIds });
        }

        [HttpPost]
        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.FenyitesVegrahajtasaKesz, DbModositasTortenik = false, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja)]
        public JsonResult FenyitesVegrahajtasaKesz(List<int> fegyelmiUgyIds)
        {
            ThrowValidationExceptionIfNotValid();
            FegyelmiUgyFunctions.ThrowExceptionIfFegyelmiUgyNemModosithato(fegyelmiUgyIds);
            var naplobejegyzesIds = FegyelmiUgyFunctions.FenyitesVegrahajtasaKesz(fegyelmiUgyIds);
            return Json(new { naplobejegyzesIds });
        }

        [HttpPost]
        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.GetFogvatartottOsszesFegyelmiUgy, DbModositasTortenik = false, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja)]
        public JsonResult GetFogvatartottOsszesFegyelmiUgy(int fegyelmiUgyId)
        {
            var nyitottUgyek = FegyelmiUgyFunctions.GetOsszesFegyelmiUgyByFegyelmiUgyId(fegyelmiUgyId);
            return Json(new { nyitottak = nyitottUgyek.Item1, zartak = nyitottUgyek.Item2 });
        }

        [HttpPost]
        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.GetLetoltottMaganelzarasPercekByFegyelmiUgyId, DbModositasTortenik = false, FeluletenLathato = true)]
        public JsonResult GetLetoltottMaganelzarasPercekByFegyelmiUgyId(int fegyelmiUgyId)
        {
            FenyitesVegrehajtasFunctions fenyitesVegrehajtasFunctions = new FenyitesVegrehajtasFunctions();
            var percek = fenyitesVegrehajtasFunctions.GetLetoltottMaganelzarasPercekByFegyelmiUgyId(fegyelmiUgyId);
            return Json(percek);
        }

        [HttpPost]
        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.GetFegyelmiUgyById, DbModositasTortenik = false, FeluletenLathato = true)]
        public JsonResult GetFegyelmiUgyById(int fegyelmiUgyId)
        {
            var fegyelmiUgy = FegyelmiUgyFunctions.GetFegyelmiUgyById(fegyelmiUgyId);
            return Json(fegyelmiUgy);
        }

        [HttpPost]
        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.GetFolyamatbanEsKiszabvaFegyelmiUgyekByFegyelmiUgyId, DbModositasTortenik = false, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja)]
        public JsonResult GetFolyamatbanEsKiszabvaFegyelmiUgyekByFegyelmiUgyId(int fegyelmiUgyId)
        {
            var fegyelmiLista = FegyelmiUgyFunctions.GetFolyamatbanEsKiszabvaFegyelmiUgyekByFegyelmiUgyId(fegyelmiUgyId);
            return Json(fegyelmiLista);
        }
    }
}
