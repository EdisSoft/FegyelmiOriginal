namespace Edis.Functions.JFK
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using Edis.Entities.Enums.Cimke;
    using Edis.Entities.Enums.Kodszotar;
    using Edis.Entities.Fany;
    using Edis.Functions.Base;
    using Edis.Functions.Common;
    using Edis.Functions.Fany;
    using Edis.Functions.JFK;
    using Edis.Functions.JFK.FENY;
    using Edis.IoC.Attributes;
    using Edis.Repositories.Contexts;
    using Edis.ViewModels.Fany;
    using Edis.ViewModels.JFK.Nyomtatvany;

    public class FanyFegyelmiUgyFunctions : KonasoftBVFonixFunctionsBase<FanyFegyelmiUgyViewModel, FegyelmiFenyites>, IFanyFegyelmiUgyFunctions
    {
        [Inject]
        public IIntezetFunctions IntezetFunctions { get; set; }

        [Inject]
        public IIktatottNyomtatvanyokFunctions IktatottNyomtatvanyokFunctions { get; set; }

        [Inject]
        public IAlkalmazasKontextusFunctions AlkalmazasKontextusFunctions { get; set; }

        [Inject]
        public IFogvatartottNezetFunctions FogvatartottNezetFunctions { get; set; }
        

        //private FanyContext FanyContext { get; set; } = new FanyContext();

        //public FanyFegyelmiUgyViewModel GetFegyelmiUgy(int fegyelmiUgyId)
        //{
        //    KonasoftBVFonixContext.Configuration.LazyLoadingEnabled = false;

        //    var fegyelmiUgy = (FanyFegyelmiUgyViewModel)FanyContext.FegyelmiUgy.AsNoTracking()
        //        .Include(x => x.Intezet)
        //        .Single(x => x.Id == fegyelmiUgyId);

        //    //var fogvatartott = FogvatartottNezetFunctions.FindFogvatartottNezetById(fegyelmiUgy.FogvatartottId);

        //    //fegyelmiUgy.Fogvatartott = fogvatartott;

        //    return fegyelmiUgy;
        //}

        //public EljarasAlaVontMeghallgatasiJegyzokonyvFegyelmiUgyKivizsgalasahoz GetEljarasAlaVontMeghallgatasiJegyzokonyvFegyelmiUgyKivizsgalasahoz(int fegyelmiUgyId)
        //{
        //    var fegyelmiUgy = GetFegyelmiUgy(fegyelmiUgyId);
        //    var iktatas = IktatottNyomtatvanyokFunctions.GetLastIktatottNyomtatvany(fegyelmiUgyId);

        //    var model = new EljarasAlaVontMeghallgatasiJegyzokonyvFegyelmiUgyKivizsgalasahoz
        //    {
        //        FegyelmiUgy = new JFKFegyelmiUgyModel
        //        {
        //            Intezet = fegyelmiUgy.Intezet,
        //            UgyIntezetiSorszam = fegyelmiUgy.UgyIntSorszam,
        //            UgyEv = fegyelmiUgy.UgyEv
        //        },
        //        IktatasiAlszam = iktatas.Alszam,
        //        //Fogvatartott = fegyelmiUgy.Fogvatartott
        //    };

        //    return model;
        //}

        //public TanuMeghallgatasiJegyzokonyvFegyelmiUgyKivizsgalasahoz GetTanuMeghallgatasiJegyzokonyvFegyelmiUgyKivizsgalasahoz(int fegyelmiUgyId)
        //{
        //    var fegyelmiUgy = GetFegyelmiUgy(fegyelmiUgyId);
        //    var iktatas = IktatottNyomtatvanyokFunctions.GetLastIktatottNyomtatvany(fegyelmiUgyId);

        //    var model = new TanuMeghallgatasiJegyzokonyvFegyelmiUgyKivizsgalasahoz
        //    {
        //        FegyelmiUgy = new JFKFegyelmiUgyModel
        //        {
        //            Intezet = fegyelmiUgy.Intezet,
        //            UgyIntezetiSorszam = fegyelmiUgy.UgyIntSorszam,
        //            UgyEv = fegyelmiUgy.UgyEv
        //        },
        //        IktatasiAlszam = iktatas.Alszam,
        //        //Fogvatartott = fegyelmiUgy.Fogvatartott
        //    };

        //    return model;
        //}

        //public JegyzokonyvTanuSzemelyiAllomanyiTagMeghallgatasarol GetJegyzokonyvTanuSzemelyiAllomanyiTagMeghallgatasarol(int fegyelmiUgyId)
        //{
        //    var fegyelmiUgy = GetFegyelmiUgy(fegyelmiUgyId);
        //    var iktatas = IktatottNyomtatvanyokFunctions.GetLastIktatottNyomtatvany(fegyelmiUgyId);

        //    var model = new JegyzokonyvTanuSzemelyiAllomanyiTagMeghallgatasarol
        //    {
        //        FegyelmiUgy = new JFKFegyelmiUgyModel
        //        {
        //            Intezet = fegyelmiUgy.Intezet,
        //            UgyIntezetiSorszam = fegyelmiUgy.UgyIntSorszam,
        //            UgyEv = fegyelmiUgy.UgyEv
        //        },
        //        IktatasiAlszam = iktatas.Alszam,
        //    };

        //    return model;
        //}

        //public FegyelmiTargyalasiJegyzokonyv GetFegyelmiTargyalasiJegyzokonyv(int fegyelmiUgyId)
        //{
        //    var fegyelmiUgy = GetFegyelmiUgy(fegyelmiUgyId);
        //    var iktatas = IktatottNyomtatvanyokFunctions.GetLastIktatottNyomtatvany(fegyelmiUgyId);

        //    var model = new FegyelmiTargyalasiJegyzokonyv
        //    {
        //        FegyelmiUgy = new JFKFegyelmiUgyModel
        //        {
        //            Intezet = fegyelmiUgy.Intezet,
        //            UgyIntezetiSorszam = fegyelmiUgy.UgyIntSorszam,
        //            UgyEv = fegyelmiUgy.UgyEv
        //        },
        //        IktatasiAlszam = iktatas.Alszam,
        //        //Fogvatartott = fegyelmiUgy.Fogvatartott
        //    };

        //    return model;
        //}
        //public JegyzokonyvFogvatartottakSzembesiteserol GetJegyzokonyvFogvatartottakSzembesiteserol(int fegyelmiUgyId)
        //{
        //    var fegyelmiUgy = GetFegyelmiUgy(fegyelmiUgyId);
        //    var iktatas = IktatottNyomtatvanyokFunctions.GetLastIktatottNyomtatvany(fegyelmiUgyId);

        //    var model = new JegyzokonyvFogvatartottakSzembesiteserol
        //    {
        //        FegyelmiUgy = new JFKFegyelmiUgyModel
        //        {
        //            Intezet = fegyelmiUgy.Intezet,
        //            UgyIntezetiSorszam = fegyelmiUgy.UgyIntSorszam,
        //            UgyEv = fegyelmiUgy.UgyEv
        //        },
        //        IktatasiAlszam = iktatas.Alszam,
        //        //Fogvatartott = fegyelmiUgy.Fogvatartott
        //    };

        //    return model;
        //}
        //public JegyzokonyvAlairasMegtagadasarol GetJegyzokonyvAlairasMegtagadasarol(int fegyelmiUgyId)
        //{
        //    var fegyelmiUgy = GetFegyelmiUgy(fegyelmiUgyId);
        //    var iktatas = IktatottNyomtatvanyokFunctions.GetLastIktatottNyomtatvany(fegyelmiUgyId);

        //    var model = new JegyzokonyvAlairasMegtagadasarol
        //    {
        //        FegyelmiUgy = new JFKFegyelmiUgyModel
        //        {
        //            Intezet = fegyelmiUgy.Intezet,
        //            UgyIntezetiSorszam = fegyelmiUgy.UgyIntSorszam,
        //            UgyEv = fegyelmiUgy.UgyEv
        //        },
        //        IktatasiAlszam = iktatas.Alszam,
        //        //Fogvatartott = fegyelmiUgy.Fogvatartott
        //    };

        //    return model;
        //}
        //public ErtesitoLap GetErtesitoLap(int fegyelmiUgyId)
        //{
        //    var fegyelmiUgy = GetFegyelmiUgy(fegyelmiUgyId);
        //    var iktatas = IktatottNyomtatvanyokFunctions.GetLastIktatottNyomtatvany(fegyelmiUgyId);

        //    var model = new ErtesitoLap
        //    {
        //        FegyelmiUgy = new JFKFegyelmiUgyModel
        //        {
        //            Intezet = fegyelmiUgy.Intezet,
        //            UgyIntezetiSorszam = fegyelmiUgy.UgyIntSorszam,
        //            UgyEv = fegyelmiUgy.UgyEv
        //        },
        //        IktatasiAlszam = iktatas.Alszam,
        //        //Fogvatartott = fegyelmiUgy.Fogvatartott
        //    };

        //    return model;
        //}
        //public FegyelmiElkulonitesiLap GetFegyelmiElkulonitesiLap(int fegyelmiUgyId)
        //{
        //    var fegyelmiUgy = GetFegyelmiUgy(fegyelmiUgyId);
        //    var iktatas = IktatottNyomtatvanyokFunctions.GetLastIktatottNyomtatvany(fegyelmiUgyId);

        //    var model = new FegyelmiElkulonitesiLap
        //    {
        //        FegyelmiUgy = new JFKFegyelmiUgyModel
        //        {
        //            Intezet = fegyelmiUgy.Intezet,
        //            UgyIntezetiSorszam = fegyelmiUgy.UgyIntSorszam,
        //            UgyEv = fegyelmiUgy.UgyEv
        //        },
        //        IktatasiAlszam = iktatas.Alszam,
        //        //Fogvatartott = fegyelmiUgy.Fogvatartott
        //    };

        //    return model;
        //}
        //public TajekoztatoAFegyelmiEljarassalOsszefuggoJogokrolKotelezettsegekrol GetTajekoztatoAFegyelmiEljarassalOsszefuggoJogokrolKotelezettsegekrol(int fegyelmiUgyId)
        //{
        //    var fegyelmiUgy = GetFegyelmiUgy(fegyelmiUgyId);
        //    var iktatas = IktatottNyomtatvanyokFunctions.GetLastIktatottNyomtatvany(fegyelmiUgyId);

        //    var model = new TajekoztatoAFegyelmiEljarassalOsszefuggoJogokrolKotelezettsegekrol
        //    {
        //        FegyelmiUgy = new JFKFegyelmiUgyModel
        //        {
        //            Intezet = fegyelmiUgy.Intezet,
        //            UgyIntezetiSorszam = fegyelmiUgy.UgyIntSorszam,
        //            UgyEv = fegyelmiUgy.UgyEv
        //        },
        //        IktatasiAlszam = iktatas.Alszam,
        //        //Fogvatartott = fegyelmiUgy.Fogvatartott
        //    };

        //    return model;
        //}
        //public VegrehajtasiLap GetVegrehajtasiLap(int fegyelmiUgyId)
        //{
        //    var fegyelmiUgy = GetFegyelmiUgy(fegyelmiUgyId);
        //    var iktatas = IktatottNyomtatvanyokFunctions.GetLastIktatottNyomtatvany(fegyelmiUgyId);

        //    var model = new VegrehajtasiLap
        //    {
        //        FegyelmiUgy = new JFKFegyelmiUgyModel
        //        {
        //            Intezet = fegyelmiUgy.Intezet,
        //            UgyIntezetiSorszam = fegyelmiUgy.UgyIntSorszam,
        //            UgyEv = fegyelmiUgy.UgyEv
        //        },
        //        IktatasiAlszam = iktatas.Alszam,
        //        //Fogvatartott = fegyelmiUgy.Fogvatartott
        //    };

        //    return model;
        //}
        //public NyilatkozatKozvetitoiEljarasonValoReszvetelrolEljarasAlaVontFogvatartott GetNyilatkozatKozvetitoiEljarasonValoReszvetelrolEljarasAlaVontFogvatartott(int fegyelmiUgyId)
        //{
        //    var fegyelmiUgy = GetFegyelmiUgy(fegyelmiUgyId);
        //    var iktatas = IktatottNyomtatvanyokFunctions.GetLastIktatottNyomtatvany(fegyelmiUgyId);

        //    var model = new NyilatkozatKozvetitoiEljarasonValoReszvetelrolEljarasAlaVontFogvatartott
        //    {
        //        FegyelmiUgy = new JFKFegyelmiUgyModel
        //        {
        //            Intezet = fegyelmiUgy.Intezet,
        //            UgyIntezetiSorszam = fegyelmiUgy.UgyIntSorszam,
        //            UgyEv = fegyelmiUgy.UgyEv
        //        },
        //        IktatasiAlszam = iktatas.Alszam,
        //        //EljarasAlaVontFogvatartott = fegyelmiUgy.Fogvatartott,
        //        //SertettFogvatartott = fegyelmiUgy.SertettFogvatartott
        //    };

        //    return model;
        //}
        //public NyilatkozatKozvetitoiEljarasonValoReszvetelrolSertett GetNyilatkozatKozvetitoiEljarasonValoReszvetelrolSertett(int fegyelmiUgyId)
        //{
        //    var fegyelmiUgy = GetFegyelmiUgy(fegyelmiUgyId);
        //    var iktatas = IktatottNyomtatvanyokFunctions.GetLastIktatottNyomtatvany(fegyelmiUgyId);

        //    var model = new NyilatkozatKozvetitoiEljarasonValoReszvetelrolSertett
        //    {
        //        FegyelmiUgy = new JFKFegyelmiUgyModel
        //        {
        //            Intezet = fegyelmiUgy.Intezet,
        //            UgyIntezetiSorszam = fegyelmiUgy.UgyIntSorszam,
        //            UgyEv = fegyelmiUgy.UgyEv
        //        },
        //        IktatasiAlszam = iktatas.Alszam,
        //        //EljarasAlaVontFogvatartott = fegyelmiUgy.Fogvatartott,
        //        //SertettFogvatartott = fegyelmiUgy.SertettFogvatartott
        //    };

        //    return model;
        //}
        //public FeljegyzesKozvetitoiMegbeszelesrol GetFeljegyzesKozvetitoiMegbeszelesrol(int fegyelmiUgyId)
        //{
        //    var fegyelmiUgy = GetFegyelmiUgy(fegyelmiUgyId);
        //    var iktatas = IktatottNyomtatvanyokFunctions.GetLastIktatottNyomtatvany(fegyelmiUgyId);

        //    var model = new FeljegyzesKozvetitoiMegbeszelesrol
        //    {
        //        FegyelmiUgy = new JFKFegyelmiUgyModel
        //        {
        //            Intezet = fegyelmiUgy.Intezet,
        //            UgyIntezetiSorszam = fegyelmiUgy.UgyIntSorszam,
        //            UgyEv = fegyelmiUgy.UgyEv
        //        },
        //        IktatasiAlszam = iktatas.Alszam,
        //        //EljarasAlaVontFogvatartott = fegyelmiUgy.Fogvatartott,
        //        //SertettFogvatartott = fegyelmiUgy.SertettFogvatartott
        //    };

        //    return model;
        //}
        //public MegallapodasKozvetitoiEljarasEredmenyerol GetMegallapodasKozvetitoiEljarasEredmenyerol(int fegyelmiUgyId)
        //{
        //    var fegyelmiUgy = GetFegyelmiUgy(fegyelmiUgyId);
        //    var iktatas = IktatottNyomtatvanyokFunctions.GetLastIktatottNyomtatvany(fegyelmiUgyId);

        //    var model = new MegallapodasKozvetitoiEljarasEredmenyerol
        //    {
        //        FegyelmiUgy = new JFKFegyelmiUgyModel
        //        {
        //            Intezet = fegyelmiUgy.Intezet,
        //            UgyIntezetiSorszam = fegyelmiUgy.UgyIntSorszam,
        //            UgyEv = fegyelmiUgy.UgyEv
        //        },
        //        IktatasiAlszam = iktatas.Alszam,
        //        //EljarasAlaVontFogvatartott = fegyelmiUgy.Fogvatartott,
        //        //SertettFogvatartott = fegyelmiUgy.SertettFogvatartott
        //        MegallapodasIdeje = DateTime.Now,
                
        //    };

        //    return model;
        //}
        //public NyilatkozatVedoErtesitesehez GetNyilatkozatVedoErtesitesehez(int fegyelmiUgyId)
        //{
        //    var fegyelmiUgy = GetFegyelmiUgy(fegyelmiUgyId);
        //    var iktatas = IktatottNyomtatvanyokFunctions.GetLastIktatottNyomtatvany(fegyelmiUgyId);

        //    var model = new NyilatkozatVedoErtesitesehez
        //    {
        //        FegyelmiUgy = new JFKFegyelmiUgyModel
        //        {
        //            Intezet = fegyelmiUgy.Intezet,
        //            UgyIntezetiSorszam = fegyelmiUgy.UgyIntSorszam,
        //            UgyEv = fegyelmiUgy.UgyEv
        //        },
        //        IktatasiAlszam = iktatas.Alszam,
        //        //Fogvatartott = fegyelmiUgy.Fogvatartott
        //    };

        //    return model;
        //}
        //public NyilatkozatVedoKirendelesehez GetNyilatkozatVedoKirendelesehez(int fegyelmiUgyId)
        //{
        //    var fegyelmiUgy = GetFegyelmiUgy(fegyelmiUgyId);
        //    var iktatas = IktatottNyomtatvanyokFunctions.GetLastIktatottNyomtatvany(fegyelmiUgyId);

        //    var model = new NyilatkozatVedoKirendelesehez
        //    {
        //        FegyelmiUgy = new JFKFegyelmiUgyModel
        //        {
        //            Intezet = fegyelmiUgy.Intezet,
        //            UgyIntezetiSorszam = fegyelmiUgy.UgyIntSorszam,
        //            UgyEv = fegyelmiUgy.UgyEv
        //        },
        //        IktatasiAlszam = iktatas.Alszam,
        //        //Fogvatartott = fegyelmiUgy.Fogvatartott
        //    };

        //    return model;
        //}
        //public VedoKiertesitese GetVedoKiertesitese(int fegyelmiUgyId)
        //{
        //    var fegyelmiUgy = GetFegyelmiUgy(fegyelmiUgyId);
        //    var iktatas = IktatottNyomtatvanyokFunctions.GetLastIktatottNyomtatvany(fegyelmiUgyId);

        //    var model = new VedoKiertesitese
        //    {
        //        FegyelmiUgy = new JFKFegyelmiUgyModel
        //        {
        //            Intezet = fegyelmiUgy.Intezet,
        //            UgyIntezetiSorszam = fegyelmiUgy.UgyIntSorszam,
        //            UgyEv = fegyelmiUgy.UgyEv
        //        },
        //        IktatasiAlszam = iktatas.Alszam,
        //        //Fogvatartott = fegyelmiUgy.Fogvatartott
        //    };

        //    return model;
        //}
        //public VedoKirendelese GetVedoKirendelese(int fegyelmiUgyId)
        //{
        //    var fegyelmiUgy = GetFegyelmiUgy(fegyelmiUgyId);
        //    var iktatas = IktatottNyomtatvanyokFunctions.GetLastIktatottNyomtatvany(fegyelmiUgyId);

        //    var model = new VedoKirendelese
        //    {
        //        FegyelmiUgy = new JFKFegyelmiUgyModel
        //        {
        //            Intezet = fegyelmiUgy.Intezet,
        //            UgyIntezetiSorszam = fegyelmiUgy.UgyIntSorszam,
        //            UgyEv = fegyelmiUgy.UgyEv
        //        },
        //        IktatasiAlszam = iktatas.Alszam,
        //        //Fogvatartott = fegyelmiUgy.Fogvatartott
        //    };

        //    return model;
        //}
        //public VedoTelefononTortenoTajekoztatasa GetVedoTelefononTortenoTajekoztatasa(int fegyelmiUgyId)
        //{
        //    var fegyelmiUgy = GetFegyelmiUgy(fegyelmiUgyId);
        //    var iktatas = IktatottNyomtatvanyokFunctions.GetLastIktatottNyomtatvany(fegyelmiUgyId);

        //    var model = new VedoTelefononTortenoTajekoztatasa
        //    {
        //        FegyelmiUgy = new JFKFegyelmiUgyModel
        //        {
        //            Intezet = fegyelmiUgy.Intezet,
        //            UgyIntezetiSorszam = fegyelmiUgy.UgyIntSorszam,
        //            UgyEv = fegyelmiUgy.UgyEv
        //        },
        //        IktatasiAlszam = iktatas.Alszam
        //    };

        //    return model;
        //}
    }
}
