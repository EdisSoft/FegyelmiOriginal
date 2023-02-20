using Edis.Entities.Enums;
using Edis.Fenyites.Controllers.Base;
using Edis.Functions.Base;
using Edis.Functions.JFK;
using Edis.Functions.JFK.FENY;
using Edis.IoC.Attributes;
using Edis.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Edis.Entities.Enums.Cimke.CimkeEnums;

namespace Edis.Fenyites.Controllers
{
    [EnableCors]
    [TrackTimeFilter]
    public class NaploBejegyzesController : BaseController
    {
        [Inject]
        IEsemenyekFunctions EsemenyekFunctions { get; set; }
        [Inject]
        IEsemenyResztvevoFunctions EsemenyResztvevoFunctions { get; set; }

        [Inject]
        INaploFunctions NaploFunctions { get; set; }
        [Inject]
        IFegyelmiUgyFunctions FegyelmiUgyFunctions { get; set; }

        [Inject]
        IFeltoltesFunctions FeltoltesFunctions { get; set; }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.GetNaplobejegyzesekByFegyelmiUgyId, DbModositasTortenik = false, FeluletenLathato = true)]
        [RequirePermission(Jogosultsagok.Fegyelmi_jogkor_gyakorloja,
                           Jogosultsagok.Fegyelmi_egyeb_szakterulet,
                           Jogosultsagok.Fegyelmi_fofelugyelo,
                           Jogosultsagok.Fegyelmi_reintegracios_tiszt,
                           Jogosultsagok.Jfk_fegyjutmegtekinto)]
        public JsonResult GetNaplobejegyzesekByFegyelmiUgyId(int fegyelmiUgyId)
        {
            var naplobejegyzesek = NaploFunctions.GetNaplobejegyzesekByFegyelmiUgyId(fegyelmiUgyId);

            ViewModels.JFK.FENY.EsemenyViewModel esemeny;
            if (fegyelmiUgyId > 0)
            {
                var fegyelmiUgy = FegyelmiUgyFunctions.GetFegyelmiUgyById(fegyelmiUgyId);
                esemeny = EsemenyekFunctions.GetEsemenyById(fegyelmiUgy.EsemenyId);
                var naploIds = naplobejegyzesek.Select(x => x.Id).ToList();

                var resztvevok = EsemenyResztvevoFunctions.GetEsemenyResztvevokPanelra(esemeny.Id);
                esemeny.Tanuk = resztvevok.Where(w => w.ErintettsegFokaCimId == (int)ErintettsegFoka.Tanu).Select(x => x.NyilvantartasiAzonosito + " - " + x.FogvatartottNev).ToList();
                esemeny.Elkovetok = resztvevok.Where(w => w.ErintettsegFokaCimId == (int)ErintettsegFoka.Elkoveto).Select(x => x.NyilvantartasiAzonosito + " - " + x.FogvatartottNev).ToList();
                esemeny.Sertettek = resztvevok.Where(w => w.ErintettsegFokaCimId == (int)ErintettsegFoka.Sertett).Select(x => x.NyilvantartasiAzonosito + " - " + x.FogvatartottNev).ToList();
                esemeny.Feltoltesek = FeltoltesFunctions.GetFeltoltottFilesByIds(naploIds, esemeny.Id);
                esemeny.TovabbiElkovetok = resztvevok.Where(w => w.ErintettsegFokaCimId == (int)ErintettsegFoka.Elkoveto && w.FogvatartottId != fegyelmiUgy.FogvatartottId).Select(x => x.NyilvantartasiAzonosito + " - " + x.FogvatartottNev).ToList();
            }
            else
                esemeny = new ViewModels.JFK.FENY.EsemenyViewModel() { Id = 1, ErvenyessegKezd = DateTime.Now, EsemenyDatuma = DateTime.Now, ToroltFl = false };

            return Json(new { naplobejegyzesek, esemeny });
        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.NaploBejegyzesInaktivalasa, DbModositasTortenik = true, FeluletenLathato = true)]
        public JsonResult NaploBejegyzesInaktivalasa(int naploBejegyzesId)
        {
            NaploFunctions.NaploBejegyzesInaktivalasa(naploBejegyzesId);
            return Json(new { naploBejegyzesId });

        }

        [AktivitasLogFilter(AktivitasTipus = AktivitasTipusok.NaploBejegyzesAktivalasa, DbModositasTortenik = true, FeluletenLathato = true)]
        public JsonResult NaploBejegyzesAktivalasa(int naploBejegyzesId)
        {
            NaploFunctions.NaploBejegyzesAktivalasa(naploBejegyzesId);
            return Json(new { naploBejegyzesId });
        }
    }
}