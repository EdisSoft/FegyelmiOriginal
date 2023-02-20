namespace Edis.Functions.JFK
{
    using Edis.Diagnostics;
    using Edis.Entities.Common;
    using Edis.Entities.Enums;
    using Edis.Entities.Enums.Cimke;
    using Edis.Entities.Enums.Cimke.Aktivitas;
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
    using Edis.ViewModels.JFK.FENY;
    using Edis.ViewModels.JFK.FENY.Email;
    using Edis.ViewModels.JFK.FENY.FormModel;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Data.Entity;
    using System.Data.SqlClient;
    using System.Data.SqlTypes;
    using System.Linq;
    using System.Security.Principal;
    using static Edis.Entities.Enums.Cimke.CimkeEnums;
    using static Edis.Entities.Enums.Cimke.Fegyelmi.CimkeEnums;

    //using Edis.ViewModels.JFK.FENY.Email.VegszallitottFogvatartottEmail;

    public partial class FegyelmiUgyFunctions : KonasoftBVFonixFunctionsBase<FegyelmiUgyViewModel, FegyelmiUgy>, IFegyelmiUgyFunctions
    {
        public delegate void FegyelmiUgyValtozasEvent(List<int> ujUgyIdList, List<int> valtozottUgyIdList, List<int> megszuntUgyIdList);
        public static event FegyelmiUgyValtozasEvent OnFegyelmiUgyValtozas;

        /// <summary>
        /// _BVOP-FONIXAPP
        /// </summary>
        public static string AdUserSid= "S-1-5-21-2660942491-3950059201-758058536-51478";
        public DbSet<FegyelmiUgy> Table => KonasoftBVFonixContext.FegyelmiUgyek;

        [Inject]
        IFogvatartottFunctions FogvatartottFunctions { get; set; }

        [Inject]
        ICimkeFunctions CimkeFunctions { get; set; }

        [Inject]
        IAlkalmazasKontextusFunctions AlkalmazasKontextusFunctions { get; set; }
        [Inject]
        public IAlkalmazasKontextusFunctions AlkalmazasFunctions { get; set; }
        [Inject]
        IActiveDirectoryFunctions ActiveDirectoryFunctions { get; set; }

        [Inject]
        IActiveDirectoryKezeloFunctions ActiveDirectoryKezeloFunctions { get; set; }

        [Inject]
        IJogosultsagCacheFunctions JogosultsagCacheFunctions { get; set; }

        [Inject]
        ISzemelyzetFunctions SzemelyzetFunctions { get; set; }



        [Inject]
        INaploFunctions NaploFunctions { get; set; }

        [Inject]
        IKodszotarFunctions KodszotarFunctions { get; set; }

        [Inject]
        IIktatottDokumentumokFunctions IktatottDokumentumokFunctions { get; set; }

        [Inject]
        IFelfuggesztesFunctions FelfuggesztesFunctions { get; set; }

      

        [Inject]
        IEsemenyResztvevoFunctions EsemenyResztvevoFunctions { get; set; }

      

        [Inject]
        ISzakteruletiVelemenyKeresekFunctions SzakteruletiVelemenyKeresekFunctions { get; set; }

        [Inject]
        IFenyitesVegrehajtasFunctions FenyitesVegrehajtasFunctions { get; set; }
        [Inject]
        IMaganelzarasFofelugyelokFunctions MaganelzarasFofelugyelokFunctions { get; set; }

        [Inject]
        IFogvatartottNezetFunctions FogvatartottNezetFunctions { get; set; }

      
        public List<int> jelenlevoStatuszok = new List<int>() { 2775, 2776, 2778, 2779, 2780, 2781, 2782, 2783, 2784, 2785, 2786, 2787, 2788, 2789, 2790, 2791, 2792, 2793, 2794, 2795, 2796, 4788, 4823 };

        #region SzamValtasaBeture
        private string[] aOne = { "", "egy", "kettő", "három", "négy", "öt", "hat", "hét", "nyolc", "kilenc" };
        private string[] aTwo = { "", "tíz", "húsz", "harminc", "negyven", "ötven", "hatvan", "hetven", "nyolcvan", "kilencven" };
        private int[] ai = new int[7];

        private string S1()
        {
            return aOne[ai[0]];
        }

        private string S2()
        {
            if (ai[0] == 0)
            {
                return aTwo[ai[1]] + S1();
            }
            else
            {
                switch (ai[1])
                {
                    case 1:
                        return "tizen" + S1();
                    case 2:
                        return "huszon" + S1();
                    default:
                        return aTwo[ai[1]] + S1();
                }
            }
        }

        private string S3()
        {
            if (ai[2] == 0)
            {
                return S2();
            }
            else
            {
                return aOne[ai[2]] + "száz" + S2();
            }
        }

        private string S4()
        {
            if (ai[3] == 0)
            {
                return S3();
            }
            else
            {
                return aOne[ai[3]] + "ezer" + S3();
            }
        }


        private string S5()
        {
            string s;
            if (ai[3] == 0)
            {
                s = aTwo[ai[4]];
            }
            else
            {
                switch (ai[4])
                {
                    case 1:
                        s = "tizen";
                        break;
                    case 2:
                        s = "huszon";
                        break;
                    default:
                        s = aTwo[ai[4]];
                        break;
                }
            }
            if (ai[3] == 0)
            {
                s = s + "ezer";
            }
            return s + S4();
        }

        private string S6()
        {
            if (ai[5] == 0)
            {
                return aOne[ai[5]] + S5();
            }
            else
            {
                return aOne[ai[5]] + "száz" + S5();
            }
        }

        private string S7()
        {
            return aOne[ai[6]] + "millió" + S6();
        }

        public string SzamValtasaBeture(int CurrencyValue)
        {
            if (CurrencyValue == 0)
            {
                return "nulla";
            }
            else
            {
                if (Math.Abs(CurrencyValue) < 10000000)
                {
                    int i = 0;
                    string s = "";
                    if (CurrencyValue < 0)
                    {
                        CurrencyValue = Math.Abs(CurrencyValue);
                        s = "minusz ";
                    }
                    int l = CurrencyValue;
                    while (l > 0)
                    {
                        ai[i] = l % 10;
                        l = l / 10;
                        i++;
                    }
                    switch (i)
                    {
                        case 7:
                            s = s + S7();
                            break;
                        case 6:
                            s = s + S6();
                            break;
                        case 5:
                            s = s + S5();
                            break;
                        case 4:
                            s = s + S4();
                            break;
                        case 3:
                            s = s + S3();
                            break;
                        case 2:
                            s = s + S2();
                            break;
                        case 1:
                            s = s + S1();
                            break;
                    }
                    return s;
                }
                else
                {
                    throw new Exception("Maximum 7 jegyű szám lehet!");
                }
            }
        }

        #endregion SzamValtasaBeture

        public void NotifyUseresOnFegyelmiUgyValtozas(List<int> ujUgyIdList, List<int> valtozottUgyIdList, List<int> megszuntUgyIdList)
        {
            OnFegyelmiUgyValtozas?.Invoke(ujUgyIdList, valtozottUgyIdList, megszuntUgyIdList);
        }

        public List<FegyelmiUgyListItemViewModel> GetFegyelmiUgyek(int intezetId)
        {
            KonasoftBVFonixContext.Configuration.LazyLoadingEnabled = false;
            KonasoftBVFonixContext.Database.CommandTimeout = int.MaxValue;

            DataTable dataTable = new DataTable("IdList");
            dataTable.Columns.Add("Id", typeof(int));

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@fegyelmiUgyIds";
            parameter.SqlDbType = SqlDbType.Structured;
            parameter.Value = dataTable;
            parameter.TypeName = "dbo.IdList";

            var query = KonasoftBVFonixContext.Database.SqlQuery<FegyelmiUgyListItemViewModel>
                     ("Fegyelmi.GetFegyelmiUgyek @IntezetId, @fegyelmiUgyIds, @lezarvaFl, @ugyEve",
                         new SqlParameter("@IntezetId", intezetId),
                         parameter,
                         new SqlParameter("@lezarvaFL", false),
                         new SqlParameter("@ugyEve", SqlInt32.Null)
                     ).ToList();

            return query;
        }
        public List<FegyelmiUgyListItemViewModel> GetFegyelmiUgyekArchiv(int intezetId, int ugyEve)
        {
            KonasoftBVFonixContext.Configuration.LazyLoadingEnabled = false;
            KonasoftBVFonixContext.Database.CommandTimeout = int.MaxValue;

            //Dictionary<string, object> parameters = new Dictionary<string, object>();
            //parameters.Add("@IntezetId", intezetId);            
            //parameters.Add("@lezarvaFL", true);
            //parameters.Add("@ugyEve", ugyEve);

            DataTable dataTable = new DataTable("IdList");
            dataTable.Columns.Add("Id", typeof(int));

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@fegyelmiUgyIds";
            parameter.SqlDbType = SqlDbType.Structured;
            parameter.Value = dataTable;
            parameter.TypeName = "dbo.IdList";

            Log.Info("GetFegyelmiUgyek START");
            var query = KonasoftBVFonixContext.Database.SqlQuery<FegyelmiUgyListItemViewModel>
                     ("Fegyelmi.GetFegyelmiUgyek @IntezetId, @fegyelmiUgyIds, @lezarvaFl, @ugyEve",
                         new SqlParameter("@IntezetId", intezetId),
                         parameter,
                         new SqlParameter("@lezarvaFL", true),
                         new SqlParameter("@ugyEve", ugyEve)
                     ).ToList();
            Log.Info("GetFegyelmiUgyek END, count:" + query.Count());

            //var ugyeveAParameter = new SqlParameter("@ugyeve", ugyEve);
            //var intezetIdParameter = new SqlParameter("@intezetId", intezetId);
            //var fogvId = 0;
            //var fogvatartottIdParameter = new SqlParameter("@fogvatartottId", fogvId);

            SqlParameter archivParameter = new SqlParameter();
            archivParameter.ParameterName = "@fegyelmiUgyIds";
            archivParameter.SqlDbType = SqlDbType.Structured;
            archivParameter.Value = dataTable;
            archivParameter.TypeName = "dbo.IdList";
            Log.Info("GetArchivFegyelmiUgyek START");
            List<FegyelmiUgyListItemViewModel> fanyList;
            fanyList = KonasoftBVFonixContext.Database.SqlQuery<FegyelmiUgyListItemViewModel>
                ("Fegyelmi.GetArchivFegyelmiUgyek @IntezetId, @fegyelmiUgyIds, @lezarvaFl, @ugyEve, @fogvatartottId",
                         new SqlParameter("@IntezetId", intezetId),
                         archivParameter,
                         new SqlParameter("@lezarvaFL", true),
                         new SqlParameter("@ugyEve", ugyEve),
                         new SqlParameter("@fogvatartottId", SqlInt32.Null)
                     ).ToList();
            Log.Info("GetArchivFegyelmiUgyek END, count: " + fanyList.Count);

            Log.Info("AddRange START");
            query.AddRange(fanyList);
            Log.Info("AddRange END, count:" + query.Count);

            return query;
        }
        public List<FegyelmiUgyListItemViewModel> GetFegyelmiUgyekByFogvatartottId(int fogvatartottId)
        {
            KonasoftBVFonixContext.Configuration.LazyLoadingEnabled = false;
            KonasoftBVFonixContext.Database.CommandTimeout = int.MaxValue;

            DataTable dataTable = new DataTable("IdList");
            dataTable.Columns.Add("Id", typeof(int));

            SqlParameter parUgyIds = new SqlParameter();
            parUgyIds.ParameterName = "@fegyelmiUgyIds";
            parUgyIds.SqlDbType = SqlDbType.Structured;
            parUgyIds.Value = dataTable;
            parUgyIds.TypeName = "dbo.IdList";

            var query = KonasoftBVFonixContext.Database.SqlQuery<FegyelmiUgyListItemViewModel>
                     ("Fegyelmi.GetFegyelmiUgyek @IntezetId, @fegyelmiUgyIds, @lezarvaFl, @ugyEve, @fogvatartottId",
                         new SqlParameter("@IntezetId", SqlInt32.Null),
                         parUgyIds,
                         new SqlParameter("@lezarvaFL", SqlBinary.Null),
                         new SqlParameter("@ugyEve", SqlInt32.Null),
                         new SqlParameter("@fogvatartottId", fogvatartottId)
                     ).ToList();

            SqlParameter archivParameter = new SqlParameter();
            archivParameter.ParameterName = "@fegyelmiUgyIds";
            archivParameter.SqlDbType = SqlDbType.Structured;
            archivParameter.Value = dataTable;
            archivParameter.TypeName = "dbo.IdList";

            List<FegyelmiUgyListItemViewModel> fanyList;
            fanyList = KonasoftBVFonixContext.Database.SqlQuery<FegyelmiUgyListItemViewModel>
                ("Fegyelmi.GetArchivFegyelmiUgyek @IntezetId, @fegyelmiUgyIds, @lezarvaFl, @ugyEve, @fogvatartottId",
                         new SqlParameter("@IntezetId", SqlInt32.Null),
                         archivParameter,
                         new SqlParameter("@lezarvaFL", SqlBinary.Null),
                         new SqlParameter("@ugyEve", SqlInt32.Null),
                         new SqlParameter("@fogvatartottId", fogvatartottId)
                     ).ToList();

            query.AddRange(fanyList);

            return query;
        }

        public List<int> GetFegyelmiUgyeIdsByEsemenyId(int esemenyId)
        {
            KonasoftBVFonixContext.Configuration.LazyLoadingEnabled = false;
            KonasoftBVFonixContext.Database.CommandTimeout = int.MaxValue;

            var fegyelmiUgyek = Table.Include(x => x.Fogvatartott).AsNoTracking().Where(w => w.EsemenyId == esemenyId).Select(x => x.Id).ToList();
            if (fegyelmiUgyek == null)
            {
                return new List<int>();
            }
            return fegyelmiUgyek;
        }

        public List<FegyelmiUgy> GetFegyelmiUgyekByEsemenyId(int esemenyId)
        {
            KonasoftBVFonixContext.Configuration.LazyLoadingEnabled = false;
            KonasoftBVFonixContext.Database.CommandTimeout = int.MaxValue;

            var fegyelmiUgyek = Table.Include(x => x.Fogvatartott).AsNoTracking().Where(s => s.EsemenyId == esemenyId).ToList();
            if (fegyelmiUgyek == null)
            {
                return null;
            }
            return fegyelmiUgyek;
        }

        public List<BvBankFegyelmiUgyListaModel> GetNyitottFegyelmiUgyekForBvBankByFogvatartottId(int fogvatartottId)
        {
            KonasoftBVFonixContext.Configuration.LazyLoadingEnabled = false;
            KonasoftBVFonixContext.Database.CommandTimeout = int.MaxValue;
            var query = KonasoftBVFonixContext.Database.SqlQuery<BvBankFegyelmiUgyListaModel>
                     ("Fegyelmi.GetNyitottFegyelmiUgyekByFogvatartottId @FogvatartottId",
                         new SqlParameter("@FogvatartottId", fogvatartottId)
                     ).ToList();

            return query;
        }


        public List<FegyelmiUgy> GetFegyelmiUgyekForTimeline(int? fogvatartottId)
        {
            var konasoftBVFonixContext = new KonasoftBVFonixContext();
            konasoftBVFonixContext.Configuration.LazyLoadingEnabled = false;
            konasoftBVFonixContext.Database.CommandTimeout = int.MaxValue;

            List<int> felhoIds = new List<int>() {
                (int)Felhok.FegyelmiVetsegTipusa,
                (int)Felhok.FenyitesTipusa,
                (int)Felhok.FegyelmiUgyStatusza,
                (int)Felhok.VetsegRendeletSzerint,
                //(int)Felhok.HatarozatHozoJogkore,
                //(int)Felhok.EsemenyJellege,
                //(int)Felhok.EsemenyNapszaka,
                //(int)Felhok.EsemenyHelyszine
            };
            var cimkekList = new CimkeFunctions().GetCimkekByFelhoIds(felhoIds);
            var cimkek = cimkekList.ToDictionary(x => x.Id, y => y);

            var rendfokozatokList = new KodszotarFunctions().GetRendfokozatokEntities();
            var rendfokozatok = rendfokozatokList.ToDictionary(x => x.Id, y => y);

            var result = konasoftBVFonixContext.FegyelmiUgyek
                //.Include(i => i.ElrendeloSzemely)
                //.Include(i => i.RogzitoSzemely)
                //.Include(i => i.Esemeny)
                //.Include(i => i.Esemeny.RogzitoSzemely)
                //.Include(i => i.Esemeny.RogzitoIntezet)
                //.Include(i => i.Esemeny.Eszlelo)
                //.Include(i => i.KivizsgaloSzemely)
                //.Include(i => i.HatarozatotHozottSzemely)
                //.Include(i => i.ElrendeloSzemely.Rendfokozat)
                //.Include(i => i.RogzitoSzemely.Rendfokozat)
                //.Include(i => i.FegyelmiVetsegTipusa)
                //.Include(i => i.FenyitesTipusa)
                //.Include(i => i.StatuszCimke)
                //.Include(i => i.VetsegRendeletSzerintCimke)
                //.Include(i => i.Esemeny.RogzitoSzemely.Rendfokozat)
                //.Include(i => i.Esemeny.Jelleg)
                //.Include(i => i.Esemeny.Hely)
                //.Include(i => i.Esemeny.Napszak)
                //.Include(i => i.Esemeny.Eszlelo.Rendfokozat)
                //.Include(i => i.KivizsgaloSzemely.Rendfokozat)
                //.Include(i => i.HatarozatHozoJogkoreCimke)
                //.Include(i => i.HatarozatotHozottSzemely.Rendfokozat)
                .Include(i => i.Intezet)
                .AsNoTracking().Where(w => w.FogvatartottId == fogvatartottId).ToList();

            Log.Info("ArchivFegyelmiUgyek: " + result.Count);

            foreach (var res in result)
            {
                //Kodszotar kszvalue;
                Cimke cimkevalue;

                //if (res.ElrendeloSzemely != null && rendfokozatok.TryGetValue(res.ElrendeloSzemely.RendfokozatKszId, out kszvalue))
                //{
                //    res.ElrendeloSzemely.Rendfokozat = rendfokozatok[res.ElrendeloSzemely.RendfokozatKszId];
                //}

                //if (res.RogzitoSzemely != null && rendfokozatok.TryGetValue(res.RogzitoSzemely.RendfokozatKszId, out kszvalue))
                //{
                //    res.RogzitoSzemely.Rendfokozat = rendfokozatok[res.RogzitoSzemely.RendfokozatKszId];
                //}

                //if (res.Esemeny.RogzitoSzemely != null && rendfokozatok.TryGetValue(res.Esemeny.RogzitoSzemely.RendfokozatKszId, out kszvalue))
                //{
                //    res.Esemeny.RogzitoSzemely.Rendfokozat = rendfokozatok[res.Esemeny.RogzitoSzemely.RendfokozatKszId];
                //}

                //if (res.Esemeny.Eszlelo != null && rendfokozatok.TryGetValue(res.Esemeny.Eszlelo.RendfokozatKszId, out kszvalue))
                //{
                //    res.Esemeny.Eszlelo.Rendfokozat = rendfokozatok[res.Esemeny.Eszlelo.RendfokozatKszId];
                //}

                //if (res.KivizsgaloSzemely != null && rendfokozatok.TryGetValue(res.KivizsgaloSzemely.RendfokozatKszId, out kszvalue))
                //{
                //    res.KivizsgaloSzemely.Rendfokozat = rendfokozatok[res.KivizsgaloSzemely.RendfokozatKszId];
                //}

                //if (res.HatarozatotHozottSzemely != null && rendfokozatok.TryGetValue(res.HatarozatotHozottSzemely.RendfokozatKszId, out kszvalue))
                //{
                //    res.HatarozatotHozottSzemely.Rendfokozat = rendfokozatok[res.HatarozatotHozottSzemely.RendfokozatKszId];
                //}

                if (res.FegyelmiVetsegTipusaCimkeId != null && cimkek.TryGetValue(res.FegyelmiVetsegTipusaCimkeId.Value, out cimkevalue))
                {
                    res.FegyelmiVetsegTipusa = res.FegyelmiVetsegTipusaCimkeId != null ? cimkek[res.FegyelmiVetsegTipusaCimkeId.Value] : null;
                }

                if (res.FenyitesTipusCimkeId != null && cimkek.TryGetValue(res.FenyitesTipusCimkeId.Value, out cimkevalue))
                {
                    res.FenyitesTipusa = res.FenyitesTipusCimkeId != null ? cimkek[res.FenyitesTipusCimkeId.Value] : null;
                }

                if (cimkek.TryGetValue(res.StatuszCimkeId, out cimkevalue))
                {
                    res.StatuszCimke = cimkek[res.StatuszCimkeId];
                }

                //if (res.VetsegRendeletSzerintCimkeId != null && cimkek.TryGetValue(res.VetsegRendeletSzerintCimkeId.Value, out cimkevalue))
                //{
                //    res.VetsegRendeletSzerintCimke = res.VetsegRendeletSzerintCimkeId != null ? cimkek[res.VetsegRendeletSzerintCimkeId.Value] : null;
                //}

                //if (res.HatarozatHozoJogkoreCimkeId != null && cimkek.TryGetValue(res.HatarozatHozoJogkoreCimkeId.Value, out cimkevalue))
                //{
                //    res.HatarozatHozoJogkoreCimke = res.HatarozatHozoJogkoreCimkeId != null ? cimkek[res.HatarozatHozoJogkoreCimkeId.Value] : null;
                //}

                //if (cimkek.TryGetValue(res.Esemeny.JellegCimkeId, out cimkevalue))
                //{
                //    res.Esemeny.Jelleg = cimkek[res.Esemeny.JellegCimkeId];
                //}

                //if (cimkek.TryGetValue(res.Esemeny.HelyCimkeId, out cimkevalue))
                //{
                //    res.Esemeny.Hely = cimkek[res.Esemeny.HelyCimkeId];
                //}

                //if (cimkek.TryGetValue(res.Esemeny.NapszakCimkeId, out cimkevalue))
                //{
                //    res.Esemeny.Napszak = cimkek[res.Esemeny.NapszakCimkeId];
                //}
            }

            return result;
        }

        public List<FegyelmiUgyListItemViewModel> GetOsszevontFegyelmiUgyekForId(int FegyelmiUgyId)
        {
            List<FegyelmiUgyListItemViewModel> query = new List<FegyelmiUgyListItemViewModel>();

            KonasoftBVFonixContext.Configuration.LazyLoadingEnabled = false;
            KonasoftBVFonixContext.Database.CommandTimeout = int.MaxValue;

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@fegyelmiUgyId", FegyelmiUgyId);
            query = KonasoftBVFonixContext.RunStoredProcedureByNev<FegyelmiUgyListItemViewModel>("Fegyelmi.GetOsszevontFegyelmiUgyekForId", parameters).ToList();

            return query;
        }

        public FegyelmiUgyListItemViewModel GetFegyelmiUgyListItemViewModelById(int intezetId, int fegyelmiUgyId)
        {
            KonasoftBVFonixContext.Configuration.LazyLoadingEnabled = false;
            KonasoftBVFonixContext.Database.CommandTimeout = int.MaxValue;

            //Dictionary<string, object> parameters = new Dictionary<string, object>();
            //parameters.Add("@IntezetId", intezetId);
            //parameters.Add("@fegyelmiUgyId", fegyelmiUgyId);

            DataTable dataTable = new DataTable("IdList");
            dataTable.Columns.Add("Id", typeof(int));
            dataTable.Rows.Add(fegyelmiUgyId);

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@fegyelmiUgyIds";
            parameter.SqlDbType = SqlDbType.Structured;
            parameter.Value = dataTable;
            parameter.TypeName = "dbo.IdList";

            var query = KonasoftBVFonixContext.Database.SqlQuery<FegyelmiUgyListItemViewModel>
                     ("Fegyelmi.GetFegyelmiUgyek @IntezetId, @fegyelmiUgyIds, @lezarvaFl, @ugyEve",
                         new SqlParameter("@IntezetId", intezetId),
                         parameter,
                         new SqlParameter("@lezarvaFL", SqlBoolean.Null),
                         new SqlParameter("@ugyEve", SqlInt32.Null)
                     ).SingleOrDefault();

            return query;
        }

        public FegyelmiUgyViewModel GetFegyelmiUgyById(int fegyelmiUgyId)
        {
            KonasoftBVFonixContext.Configuration.LazyLoadingEnabled = false;

            var fegyelmiUgy = Table.Include(x => x.Fogvatartott).AsNoTracking().SingleOrDefault(s => s.Id == fegyelmiUgyId);
            if (fegyelmiUgy == null)
            {
                return null;
            }
            return (FegyelmiUgyViewModel)fegyelmiUgy;
        }

        public void SaveFegyelmiUgyJutalombolTorolt(int fegyelmiUgyId)
        {
            KonasoftBVFonixContext.Configuration.LazyLoadingEnabled = false;

            var fegyelmiUgy = Table.SingleOrDefault(s => s.Id == fegyelmiUgyId);
            fegyelmiUgy.StatuszCimkeId = 6;
            fegyelmiUgy.ErvenyessegKezdete = DateTime.Now;
            KonasoftBVFonixContext.SaveChanges();
        }

        public FegyelmiUgy GetFegyelmiUgyEsemennyel(int fegyelmiUgyId)
        {
            KonasoftBVFonixContext.Configuration.LazyLoadingEnabled = false;

            var fegyelmiUgy = Table.Include(x => x.Fogvatartott).Include(x => x.Esemeny).Include(x => x.Intezet).Include(x => x.Esemeny.Eszlelo).Include(x => x.Esemeny.Eszlelo.Rendfokozat).Include(x => x.Esemeny.Jelleg).AsNoTracking().SingleOrDefault(s => s.Id == fegyelmiUgyId);
            if (fegyelmiUgy == null)
            {
                return null;
            }
            return fegyelmiUgy;
        }

        public FegyelmiUgy GetFegyelmiUgyEntityById(int fegyelmiUgyId)
        {
            KonasoftBVFonixContext.Configuration.LazyLoadingEnabled = false;

            var fegyelmiUgy = Table.Include(x => x.Fogvatartott).Include(i => i.Intezet).Include(i => i.Intezet.CimHelyseg).AsNoTracking().SingleOrDefault(s => s.Id == fegyelmiUgyId);
            if (fegyelmiUgy == null)
            {
                return null;
            }
            return fegyelmiUgy;
        }

        //public FegyelmiLapViewModel GetFegyelmiLapById(int fegyelmiUgyId, int? iktatasId = null)
        //{
        //    KonasoftBVFonixContext.Configuration.LazyLoadingEnabled = false;
        //    FegyelmiUgy fegyelmiUgy;
        //    IktatottDokumentumok iktatas;
        //    FegyelmiLapViewModel model;
        //    // megnézzük van-e iktatott nyomtatvány

        //    if (iktatasId != null)
        //        iktatas = IktatottDokumentumokFunctions.Table.FirstOrDefault(f => f.Id == iktatasId.Value);
        //    else
        //        iktatas = IktatottDokumentumokFunctions.Table.FirstOrDefault(f => f.FegyelmiUgyId == fegyelmiUgyId && f.DokumentumTipusCimkeId == (int)IktatottDokumentumTipusok.FegyelmiLap);

        //    // ha nincs iktatás
        //    if (iktatas == null)
        //    {
        //        fegyelmiUgy = Table
        //            .AsNoTracking()
        //            .Include(x => x.Esemeny)
        //            .Include(x => x.Esemeny.Eszlelo)
        //            .Include(x => x.Intezet)
        //            .Include(x => x.Intezet.CimHelyseg)
        //            .SingleOrDefault(s => s.Id == fegyelmiUgyId);

        //        if (fegyelmiUgy == null)
        //        {
        //            return null;
        //        }

        //        var fogvatartott = FogvatartottFunctions.GetSzemelyesAdatokElhelyezessel(fegyelmiUgy.FogvatartottId);

        //        IktatottDokumentumokViewModel ujIktatas = new IktatottDokumentumokViewModel()
        //        {
        //            FegyelmiUgyId = fegyelmiUgyId,
        //            Alszam = 1,
        //            AktivFl = true,
        //            DokumentumTipusCimkeId = (int)IktatottDokumentumTipusok.FegyelmiLap
        //        };

        //        IktatottDokumentumokFunctions.CreateNyomtatvany(ujIktatas);

        //        ujIktatas = (IktatottDokumentumokViewModel)KonasoftBVFonixContext.IktatottDokumentumok.Single(x => x.Id == ujIktatas.Id);

        //        // nézet kibõvítése inkább?
        //        model = new FegyelmiLapViewModel()
        //        {
        //            AktualisSzabadulasiIdo = fogvatartott.AktualisSzabadulasDatuma?.ToString("yyyy. MM. dd."),
        //            AnyjaNeve = fogvatartott.FogvatartottSzemelyesAdatai.AnyjaNeve,
        //            Azonosito = fogvatartott.NyilvantartasiAzonosito,
        //            EsemenyEszlelese = fegyelmiUgy.Esemeny.EsemenyDatuma.ToString("yyyy. MM. dd. HH:mm"),
        //            EsemenyLeirasa = fegyelmiUgy.Esemeny.Leiras,
        //            EsemenySorszama = fegyelmiUgy.Esemeny.Id,
        //            EsemenytEszlelte = $"{fegyelmiUgy.Esemeny.Eszlelo.Nev} {fegyelmiUgy.Esemeny.Eszlelo.Rendfokozat}",
        //            IntezetNev = fegyelmiUgy.Intezet.Nev,
        //            FogvElhelyezese = fogvatartott.IntezetiObjektum != null ? $"{fogvatartott.IntezetiObjektum.Nev}/{fogvatartott.Korlet.Nev}/{fogvatartott.Zarka.Azonosito}" : "",
        //            Nev = $"{fogvatartott.FogvatartottSzemelyesAdatai.SzuletesiCsaladiNev} {fogvatartott.FogvatartottSzemelyesAdatai.SzuletesiUtonev}",
        //            SzuletesiHely = fogvatartott.FogvatartottSzemelyesAdatai.SzuletesiHelyNeve,
        //            SzuletesiIdo = fogvatartott.FogvatartottSzemelyesAdatai.SzuletesiDatum.ToString("yyyy. MM. dd."),
        //            TartozkodasFokaJogcime = fogvatartott.FogvatartasJellegeKszId != 0 ? KodszotarFunctions.FindById(fogvatartott.FogvatartasJellegeKszId).Nev : "",
        //            UgySzama = $"{fegyelmiUgy.UgySorszamaIntezetAzon}/{fegyelmiUgy.UgySorszamaEv}/{fegyelmiUgy.UgySorszama}",
        //            Telepules = fegyelmiUgy.Intezet.CimHelyseg.Nev,
        //            Iktatoszam = $"{fegyelmiUgy.UgySorszamaIntezetAzon}/{fegyelmiUgy.UgySorszamaEv}/{fegyelmiUgy.UgySorszama}/{ujIktatas.Alszam}",
        //        };

        //        ujIktatas.DokumentumAdat = IktatottDokumentumokFunctions.SerializeIktatottNyomtatvanyAdat(model);
        //        IktatottDokumentumokFunctions.Modify(ujIktatas);
        //    }
        //    else
        //    {
        //        model = IktatottDokumentumokFunctions.DeserializeIktatottNyomtatvanyAdat<FegyelmiLapViewModel>(iktatas.DokumentumAdat);
        //    }

        //    return model;

        //}

        public List<FegyelmiUgyListItemViewModel> GetFegyelmiUgyekByIds(List<int> fegyelmiUgyIds)
        {
            if (fegyelmiUgyIds == null)
            {
                //ha esetleg nem zártad volna be a szerver localhostját - http://localhost:13300/ - teszteléskor, akkor ide is behív a socket                
                return null;
            }

            KonasoftBVFonixContext.Configuration.LazyLoadingEnabled = false;
            KonasoftBVFonixContext.Database.CommandTimeout = int.MaxValue;


            DataTable dataTable = new DataTable("IdList");
            dataTable.Columns.Add("Id", typeof(int));
            fegyelmiUgyIds.ForEach(f => dataTable.Rows.Add(f));

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@fegyelmiUgyIds";
            parameter.SqlDbType = SqlDbType.Structured;
            parameter.Value = dataTable;
            parameter.TypeName = "dbo.IdList";

            var query = KonasoftBVFonixContext.Database.SqlQuery<FegyelmiUgyListItemViewModel>
                     ("Fegyelmi.GetFegyelmiUgyek @IntezetId, @fegyelmiUgyIds, @lezarvaFl, @ugyEve",
                         new SqlParameter("@IntezetId", SqlInt32.Null),
                         parameter,
                         new SqlParameter("@lezarvaFL", SqlBoolean.Null),
                         new SqlParameter("@ugyEve", SqlInt32.Null)
                     ).ToList();

            return query;
        }

        /// <param name="elrendelem">
        /// true: elrendelem a kivizsgálást, false: reintegrációs jogkörbe 
        /// </param>
        public void DontesFegyelmiUgyElrendeleserol(DontesFegyelmiUgyElrendeleserolViewModel model, bool elrendelem)
        {
            if (model.JogiKepviseletetKer && !elrendelem)
            {
                throw new WarningException("Jogi képviseletet csak ügy elrendelésekor lehet kérni");
            }
            List<int> ujFegyelmi = new List<int>(0);
            List<int> megvaltozottFegyelmi = new List<int>();
            List<int> toroltFegyelmi = new List<int>(0);
            using (DbContextTransaction transaction = KonasoftBVFonixContext.Database.BeginTransaction())
            {
                int currentId = 0;
                try
                {
                    var timestamp = DateTime.Now;

                    foreach (var fegyelmiUgyId in model.FegyelmiUgyIds)
                    {
                        currentId = fegyelmiUgyId;
                        var fegyelmiUgy = (FegyelmiUgyViewModel)Table.AsNoTracking().FirstOrDefault(f => f.Id == fegyelmiUgyId);
                        var esemeny = KonasoftBVFonixContext.Esemenyek.AsNoTracking().Include("Eszlelo").FirstOrDefault(x => x.Id == fegyelmiUgy.EsemenyId);
                        var tanuk = KonasoftBVFonixContext.EsemenyResztvevok
                            .Include(i => i.AllomanyiSzemely)
                            .Where(x => x.EsemenyId == fegyelmiUgy.EsemenyId && x.ErintettsegFokaCimkeId == (int)CimkeEnums.ErintettsegFoka.SzemelyiAllomanyiTanu)
                            .Select(x => x.AllomanyiSzemely)
                            .ToList();
                        foreach (var tanu in tanuk)
                        {
                            if (tanu.AdSid == model.KivizsgaloSzemelySid)
                            {
                                throw new WarningException("A tanú nem vizsgálhatja ki az ügyet");
                            }
                        }

                        // "Eljárási mód változtatása" kérelem
                        if (fegyelmiUgy.StatuszCimkeId == (int)FegyelmiUgyStatusz.KivizsgalasFolyamatban ||
                            fegyelmiUgy.StatuszCimkeId == (int)FegyelmiUgyStatusz.ReintegraciosTisztiJogkorben)
                        {
                            // "Eljárási mód változtatása" csak a tárgynapon lehetséges
                            if (fegyelmiUgy?.DontesDatuma.Value.Date != DateTime.Today)
                            {
                                throw new WarningException("Eljárási mód változtatása csak a tárgynapon lehetséges");
                            }
                        }

                        if (esemeny.Eszlelo?.AdSid == model.KivizsgaloSzemelySid)
                        {
                            throw new WarningException("Az esemény észlelője nem lehet kivizsgáló.");
                        }
                        if (esemeny.Eszlelo?.AdSid == model.DontestHozoSzemelySid)
                        {
                            if (model.FegyelmiUgyIds.Count > 1)
                            {
                                throw new WarningException("Az esemény észlelője nem lehet az elrendelő. Fegyelmi ügy: " + fegyelmiUgy.UgySorszamaIntezetAzon + "/" + fegyelmiUgy.UgySorszamaEv + "/" + fegyelmiUgy.UgySorszama + ". A tömeges elrendelés nem lett végrehajtva.");
                            }
                            else
                            {
                                throw new WarningException("Az esemény észlelője nem lehet az elrendelő. Fegyelmi ügy: " + fegyelmiUgy.UgySorszamaIntezetAzon + "/" + fegyelmiUgy.UgySorszamaEv + "/" + fegyelmiUgy.UgySorszama);
                            }

                        }

                        fegyelmiUgy.KivizsgaloSzemelyId = SzemelyzetFunctions.SzemelyzetLekeresVagyLetrehozas(model.KivizsgaloSzemelySid, null, null).Id;
                        //fegyelmiUgy.ElsofokonHatarozatotHozoSzemelyId = SzemelyzetFunctions.SzemelyzetLekeresVagyLetrehozas(model.DontestHozoSzemelySid, null, null).Id;{
                        fegyelmiUgy.ElrendeloSzemelyId = AlkalmazasKontextusFunctions.Kontextus.SzemelyzetId;

                        fegyelmiUgy.Visszakuldve = false;
                        fegyelmiUgy.DontesDatuma = timestamp;

                        if (elrendelem)
                        {
                            fegyelmiUgy.Hatarido = GetFegyelmiHatarido(fegyelmiUgy.Id, false, (int)FegyelmiUgyStatusz.KivizsgalasFolyamatban).Value;
                            fegyelmiUgy.JogiKepviseletetKer = model.JogiKepviseletetKer;
                            fegyelmiUgy.StatuszCimkeId = (int)FegyelmiUgyStatusz.KivizsgalasFolyamatban;
                        }
                        else
                        {
                            fegyelmiUgy.Hatarido = GetFegyelmiHatarido(fegyelmiUgy.Id, false, (int)FegyelmiUgyStatusz.ReintegraciosTisztiJogkorben).Value;
                            fegyelmiUgy.StatuszCimkeId = (int)FegyelmiUgyStatusz.ReintegraciosTisztiJogkorben;
                        }
                        fegyelmiUgy.KivizsgalasiHatarido = model.KivizsgalasiHatarido < fegyelmiUgy.Hatarido ? model.KivizsgalasiHatarido : fegyelmiUgy.Hatarido;
                        Modify(fegyelmiUgy);
                    }
                    transaction.Commit();

                    megvaltozottFegyelmi.AddRange(model.FegyelmiUgyIds);
                }
                catch (WarningException e)
                {
                    throw e;
                }
                catch (Exception e)
                {
                    Log.Error($"Hiba az eljárás lefolyásának elrendelése során (fegyelmiUgyId: {currentId})", e);
                    transaction.Rollback();

                    throw e;
                }
            }
            NaploFunctions.CreateNaploBejegyzesDontesFegyelmiUgyElrendeleserol(model.FegyelmiUgyIds);

            if (model.JogiKepviseletetKer)
                NaploFunctions.CreateNaploBejegyzesJogiKepviseletrol(model.FegyelmiUgyIds);

            OnFegyelmiUgyValtozas?.Invoke(ujFegyelmi, megvaltozottFegyelmi, toroltFegyelmi);
        }

        public FegyelmiUgyTanuMeghallgatasiJegyzokonyvModel GetFegyelmiUgyTanuMeghallgatasiJegyzokonyv(List<int> fegyelmiUgyIds, List<int> naplobejegyzesIds)
        {

            var fegyelmiUgyId = fegyelmiUgyIds.First();

            FegyelmiUgyTanuMeghallgatasiJegyzokonyvModel model;
            var fegyelmiUgy = FindById(fegyelmiUgyId);
            model = new FegyelmiUgyTanuMeghallgatasiJegyzokonyvModel()
            {
                FegyelmiUgyIds = fegyelmiUgyIds,
                JegyzokonyvVezetoSid = null,
                MeghallgatoSid = WindowsIdentity.GetCurrent().User.Value
            };
            model.AlairtaFl = false;
            // selectek feltöltése
            model.TanuOptions = (from resztvevo in KonasoftBVFonixContext.EsemenyResztvevok
                                 join foka in KonasoftBVFonixContext.Cimkek on resztvevo.ErintettsegFokaCimkeId equals foka.Id
                                 join fogv in KonasoftBVFonixContext.Fogvatartottak on resztvevo.FogvatartottId equals fogv.Id
                                 join szemely in KonasoftBVFonixContext.FogvatartottSzemelyesAdatai on fogv.Id equals szemely.FogvatartottId
                                 join fegyelmi in KonasoftBVFonixContext.FegyelmiUgyek on
                                                    new { resztvevo.EsemenyId, FogvatartottId = (int)resztvevo.FogvatartottId } equals new { fegyelmi.EsemenyId, fegyelmi.FogvatartottId } into fegyelmiL
                                 from fegyelmiLeft in fegyelmiL.DefaultIfEmpty()
                                 where resztvevo.EsemenyId == fegyelmiUgy.EsemenyId
                                 select new KSelect2ItemModel()
                                 {
                                     id = resztvevo.Id.ToString(),
                                     text = (resztvevo.FogvatartottId == fegyelmiUgy.FogvatartottId ? "Eljárás alá vont" : "Tanú") + " – " + fogv.NyilvantartasiAzonosito + " " + szemely.SzuletesiCsaladiNev_NE_HASZNALD + " " + szemely.SzuletesiUtonev_NE_HASZNALD
                                 }).ToList();


            var intezetiUsers = SzemelyzetFunctions.GetIntezetiAlkalmazottak();
            var fegyelmiUsers = SzemelyzetFunctions.GetFegyelmiAlkalmazottak();

            model.TovabbiSzemelyekOptions = intezetiUsers.Select(x => new KSelect2ItemModel() { id = x.Displayname + (x.Rendfokozat == null ? "" : (" " + x.Rendfokozat)), text = x.Displayname + (x.Rendfokozat == null ? "" : (" " + x.Rendfokozat)) }).ToList();
            model.MeghallgatoOptions = fegyelmiUsers.Select(x => new KSelect2ItemModel() { id = x.Sid, text = x.Displayname + (x.Rendfokozat == null ? "" : (" " + x.Rendfokozat)) }).ToList();
            model.JegyzokonyvVezetoOptions = fegyelmiUsers.Select(x => new KSelect2ItemModel() { id = x.Sid, text = x.Displayname + (x.Rendfokozat == null ? "" : (" " + x.Rendfokozat)) }).ToList();
            model.NaplobejegyzesIds = new List<int>();
            if (!naplobejegyzesIds.IsNullOrEmpty())
            {
                var naplobejegyzesId = naplobejegyzesIds.First();
                var naploBejegyzes = NaploFunctions.FindById(naplobejegyzesId);
                var tanu = KonasoftBVFonixContext.EsemenyResztvevok.Single(x => x.FogvatartottId == naploBejegyzes.FogvatartottId && x.EsemenyId == fegyelmiUgy.EsemenyId);

                if (naploBejegyzes.JegyzokonyvVezetoSzemelyId.HasValue)
                {
                    var jegyzokonyvVezeto = SzemelyzetFunctions.FindById(naploBejegyzes.JegyzokonyvVezetoSzemelyId.Value);
                    model.JegyzokonyvVezetoSid = jegyzokonyvVezeto.AdSid;
                }
                if (naploBejegyzes.MeghallgatoSzemelyId.HasValue)
                {
                    var meghallgatoSzemely = SzemelyzetFunctions.FindById(naploBejegyzes.MeghallgatoSzemelyId.Value);
                    model.MeghallgatoSid = meghallgatoSzemely.AdSid;
                }
                model.NaplobejegyzesIds = naplobejegyzesIds;
                model.TanuId = tanu.Id;
                model.Leiras = naploBejegyzes.JegyzokonyvTartalma;
                if (string.IsNullOrWhiteSpace(naploBejegyzes.TovabbiJelenlevok))
                    model.TovabbiSzemelyekList = new List<string>();
                else
                    model.TovabbiSzemelyekList = naploBejegyzes.TovabbiJelenlevok.Split(new string[] { ", " }, StringSplitOptions.None).Where(w => !string.IsNullOrWhiteSpace(w)).ToList();

                var hianyzoSzemelyek = model.TovabbiSzemelyekList.Where(w => !model.TovabbiSzemelyekOptions.Any(a => a.id == w)).ToList();
                model.TovabbiSzemelyekOptions.AddRange(hianyzoSzemelyek.Select(x => new KSelect2ItemModel() { id = x, text = x }));
            }


            return model;
        }

        public FegyelmiUgySzemelyiAllomanyiTanuMeghallgatasiJegyzokonyvModel GetFegyelmiUgySzemelyiAllomanyiTanuMeghallgatasiJegyzokonyv(List<int> fegyelmiUgyIds, List<int> naplobejegyzesIds)
        {
            FegyelmiUgySzemelyiAllomanyiTanuMeghallgatasiJegyzokonyvModel model;
            var fegyelmiUgyId = fegyelmiUgyIds.First();
            var fegyelmiUgy = FindById(fegyelmiUgyId);
            model = new FegyelmiUgySzemelyiAllomanyiTanuMeghallgatasiJegyzokonyvModel()
            {
                FegyelmiUgyIds = fegyelmiUgyIds,
                MeghallgatoSid = WindowsIdentity.GetCurrent().User.Value
            };
            model.AlairtaFl = false;

            // selectek feltöltése
            model.TanuOptions = (from esemeny in KonasoftBVFonixContext.Esemenyek
                                 join ugy in KonasoftBVFonixContext.FegyelmiUgyek on esemeny.Id equals ugy.EsemenyId
                                 join resztvevo in KonasoftBVFonixContext.EsemenyResztvevok on esemeny.Id equals resztvevo.EsemenyId
                                 join szemely in KonasoftBVFonixContext.Szemelyzet on resztvevo.AllomanyiSzemelyId equals szemely.Id
                                 join rendfokozat in KonasoftBVFonixContext.Kodszotar on szemely.RendfokozatKszId equals rendfokozat.Id
                                 where fegyelmiUgyIds.Contains(ugy.Id) && resztvevo.AllomanyiSzemelyId != null
                                 select new KSelect2ItemModel()
                                 {
                                     id = szemely.AdSid,
                                     text = szemely.Nev + " " + rendfokozat.Nev
                                 }).Distinct().ToList();

            var reintUsers = SzemelyzetFunctions.GetFegyelmiReintegraciosTisztiAlkalmazottak();
            var jogkorGyakorloUsers = SzemelyzetFunctions.GetFegyelmiJogkorGyakorlojaAlkalmazottak();
            var intezetiUsers = SzemelyzetFunctions.GetIntezetiAlkalmazottak();

            var meghallgato = new List<AdFegyelmiUserItem>();
            meghallgato.AddRange(reintUsers);
            meghallgato.AddRange(jogkorGyakorloUsers);
            meghallgato = meghallgato.GroupBy(x => x.Sid).Select(x => x.First()).ToList();

            model.MeghallgatoOptions = meghallgato.ToList().Select(x => new KSelect2ItemModel() { id = x.Sid, text = x.Displayname + (x.Rendfokozat == null ? "" : (" " + x.Rendfokozat)) }).ToList();
            model.IntezetiUsersOptions = intezetiUsers.ToList().Select(x => new KSelect2ItemModel() { id = x.Sid, text = x.Displayname + (x.Rendfokozat == null ? "" : (" " + x.Rendfokozat)) }).ToList();

            if (!naplobejegyzesIds.IsNullOrEmpty())
            {
                var naplobejegyzesId = naplobejegyzesIds.First();
                var naploBejegyzes = NaploFunctions.FindById(naplobejegyzesId);

                if (naploBejegyzes.MeghallgatoSzemelyId.HasValue)
                {
                    var meghallgatoSzemely = SzemelyzetFunctions.FindById(naploBejegyzes.MeghallgatoSzemelyId.Value);
                    model.MeghallgatoSid = meghallgatoSzemely.AdSid;
                }
                if (naploBejegyzes.SzemelyiAllomanyiTanuSzemelyId.HasValue)
                {
                    var szemelyiAllomanyiTanuSzemely = SzemelyzetFunctions.FindById(naploBejegyzes.SzemelyiAllomanyiTanuSzemelyId.Value);
                    model.TanuSid = szemelyiAllomanyiTanuSzemely.AdSid;
                }
                model.NaplobejegyzesIds = naplobejegyzesIds;
                model.Leiras = naploBejegyzes.JegyzokonyvTartalma;
            }


            return model;
        }
        public FegyelmiUgyElsoFokuTargyalasiJegyzokonyvModel GetFegyelmiUgyElsoFokuTargyalasiJegyzokonyv(List<int> fegyelmiUgyIds, List<int> naplobejegyzesIds)
        {
            FegyelmiUgyElsoFokuTargyalasiJegyzokonyvModel model;
            var fegyelmiUgyId = fegyelmiUgyIds.First();
            var fegyelmiUgy = FindById(fegyelmiUgyId);
            model = new FegyelmiUgyElsoFokuTargyalasiJegyzokonyvModel()
            {
                FegyelmiUgyIds = fegyelmiUgyIds,
                JegyzokonyvVezetoSid = WindowsIdentity.GetCurrent().User.Value
            };

            if (fegyelmiUgy.ElrendeloSzemelyId.HasValue)
                model.FegyelmiJogkorGyakorlojaSid = SzemelyzetFunctions.FindById(fegyelmiUgy.ElrendeloSzemelyId.Value).AdSid;

            var fegyelmiJogkorGyakorlojaUsers = SzemelyzetFunctions.GetFegyelmiJogkorGyakorlojaAlkalmazottak();
            var reintUsers = SzemelyzetFunctions.GetFegyelmiReintegraciosTisztiAlkalmazottak();
            var jogkorGyakorloUsers = SzemelyzetFunctions.GetFegyelmiJogkorGyakorlojaAlkalmazottak();

            var reintJogkorGyakorloUsers = new List<AdFegyelmiUserItem>();
            reintJogkorGyakorloUsers.AddRange(reintUsers);
            reintJogkorGyakorloUsers.AddRange(jogkorGyakorloUsers);
            reintJogkorGyakorloUsers = reintJogkorGyakorloUsers.GroupBy(x => x.Sid).Select(x => x.First()).ToList();


            model.TovabbiSzemelyekOptions = reintJogkorGyakorloUsers.Select(x => new KSelect2ItemModel() { id = x.Displayname + (x.Rendfokozat == null ? "" : (" " + x.Rendfokozat)), text = x.Displayname + (x.Rendfokozat == null ? "" : (" " + x.Rendfokozat)) }).ToList();
            model.FegyelmiJogkorGyakorlojaOptions = fegyelmiJogkorGyakorlojaUsers.Select(x => new KSelect2ItemModel() { id = x.Sid, text = x.Displayname + (x.Rendfokozat == null ? "" : (" " + x.Rendfokozat)) }).ToList();
            model.JegyzokonyvVezetoOptions = reintJogkorGyakorloUsers.Select(x => new KSelect2ItemModel() { id = x.Sid, text = x.Displayname + (x.Rendfokozat == null ? "" : (" " + x.Rendfokozat)) }).ToList();

            if (!naplobejegyzesIds.IsNullOrEmpty())
            {
                var naplobejegyzesId = naplobejegyzesIds.First();
                var naploBejegyzes = NaploFunctions.FindById(naplobejegyzesId);

                if (naploBejegyzes.JegyzokonyvVezetoSzemelyId.HasValue)
                    model.JegyzokonyvVezetoSid = SzemelyzetFunctions.FindById(naploBejegyzes.JegyzokonyvVezetoSzemelyId.Value).AdSid;

                if (naploBejegyzes.DonteshozoSzemelyId.HasValue)
                    model.FegyelmiJogkorGyakorlojaSid = SzemelyzetFunctions.FindById(naploBejegyzes.DonteshozoSzemelyId.Value).AdSid;

                model.NaplobejegyzesIds = naplobejegyzesIds;
                model.Leiras = naploBejegyzes.JegyzokonyvTartalma;

                if (!string.IsNullOrWhiteSpace(naploBejegyzes.TovabbiJelenlevok))
                {
                    if (string.IsNullOrWhiteSpace(naploBejegyzes.TovabbiJelenlevok))
                        model.TovabbiSzemelyekList = new List<string>();
                    else
                        model.TovabbiSzemelyekList = naploBejegyzes.TovabbiJelenlevok.Split(new string[] { ", " }, StringSplitOptions.None).ToList();

                    var nemLetezoTovabbiSzemelyek = model.TovabbiSzemelyekList.Except(model.TovabbiSzemelyekOptions.Select(s => s.text));
                    var nemLetezoTovabbiSzemelyekSelect2ItemModel = nemLetezoTovabbiSzemelyek.Select(x => new KSelect2ItemModel() { id = x, text = x }).ToList();
                    model.TovabbiSzemelyekOptions.AddRange(nemLetezoTovabbiSzemelyekSelect2ItemModel);
                }
            }

            return model;
        }

        public FegyelmiUgyHatarozatRogziteseModel GetFegyelmiUgyHatarozatRogzitese(int fegyelmiUgyId, int? naplobejegyzesId, ModalTipusok? modalType)
        {
            FegyelmiUgyHatarozatRogziteseModel model;
            var fegyelmiUgy = FindById(fegyelmiUgyId);
            var elsofokonHatarozathozoSzemely = SzemelyzetFunctions.SzemelyzetLekeresVagyLetrehozas(WindowsIdentity.GetCurrent().User.Value, null, null).Nev;
            model = new FegyelmiUgyHatarozatRogziteseModel()
            {
                FegyelmiUgyId = fegyelmiUgyId,
                ElsofokonHatarozathozoSzemely = elsofokonHatarozathozoSzemely,
                FenyitesTartamaKietkezesCsokkentesOptions = new List<KSelect2ItemModel>(),
                FenyitesTartamaMaganElzarasOptions = new List<KSelect2ItemModel>(),
                FenyitesTartamaMaganalTarthatoTargyakKorlatozasaOptions = new List<KSelect2ItemModel>(),
                FenyitesTartamaProgaromokonValoResztvetelKorlatozasaOptions = new List<KSelect2ItemModel>(),
                FenyitesTartamaProgaromokonValoResztvetelTiltasaOptions = new List<KSelect2ItemModel>(),
                FenyitesTartamaTobbletSzolgaltatasokMegvonasaOptions = new List<KSelect2ItemModel>(),
                FenyitesTartamaKimaradasMegvonasaOptions = new List<KSelect2ItemModel>(),
                FenyitesTipusaCimkeId = modalType == ModalTipusok.HatarozatMegszuntetese ? FenyitesTipusok.Megszuntetes.CastToInt() : (int?)null,
                MegszuntetesOkaCimkeId = modalType == ModalTipusok.HatarozatMegszuntetese ? MegszuntetesOkai.SikeresKozvetitoEljaras.CastToInt() : (int?)null,
                IsKarterites = fegyelmiUgy.KarteritesId != null
            };

            var fegyelmiVetsegTipusok = CimkeFunctions.GetFegyelmiVetsegTipusok();

            var vetsegRendeletSzerint = CimkeFunctions.GetFegyelmiVetsegRendeletSzerint();
            var fenyitesTipusok = CimkeFunctions.GetFegyelmiFenyitesTipusok().Where(w => w.Id != 351);
            var megszuntetesOkai = CimkeFunctions.GetFegyelmiFenyitesMegszuntetesOkai();

            model.FegyelmiVetsegTipusaOptions = fegyelmiVetsegTipusok.Select(x => new KSelect2ItemModel() { id = x.Id.ToString(), text = x.Nev }).ToList();
            model.VetsegRendeletSzerintOptions = vetsegRendeletSzerint.Select(x => new KSelect2ItemModel() { id = x.Id.ToString(), text = x.Nev }).ToList();
            model.FenyitesMegszuntetesOkaOptions = megszuntetesOkai.Where(w => w.Id != MegszuntetesOkai.UjEljarasLefolytatasa.CastToInt()).Select(x => new KSelect2ItemModel() { id = x.Id.ToString(), text = x.Nev }).ToList();
            model.FenyitesTipusaOptions = fenyitesTipusok.Select(x => new KSelect2ItemModel() { id = x.Id.ToString(), text = x.Nev }).ToList();
            if (!IsFogvatartottVegrehajtasiFokaElzaras(fegyelmiUgy.FogvatartottId))
                model.FenyitesTipusaOptions.RemoveAll(r => r.id == FenyitesTipusok.KimaradasMegvonas.CastToInt().ToString());

            var maganelzarasMaxTartama = GetMaxMaganelzarasTartama(fegyelmiUgy.FogvatartottId);
            var fenyitesHosszanakMennyisegiEgysegei = CimkeFunctions.GetFegyelmiFenyitesHosszanakMennyisegiEgysegei();
            var alkalom = fenyitesHosszanakMennyisegiEgysegei.Single(s => s.Id == MennyisegiEgysegek.Alkalom.CastToInt()).Nev.ToLower();
            var nap = fenyitesHosszanakMennyisegiEgysegei.Single(s => s.Id == MennyisegiEgysegek.Nap.CastToInt()).Nev.ToLower();
            var honap = fenyitesHosszanakMennyisegiEgysegei.Single(s => s.Id == MennyisegiEgysegek.Honap.CastToInt()).Nev.ToLower();

            Enumerable.Range(1, FenyitesTartama.MaxKietkezesCsokkentese).ToList().ForEach(f => model.FenyitesTartamaKietkezesCsokkentesOptions.Add(new KSelect2ItemModel() { id = $"{f} {honap}", text = $"{f} {honap}" }));
            Enumerable.Range(1, maganelzarasMaxTartama).ToList().ForEach(f => model.FenyitesTartamaMaganElzarasOptions.Add(new KSelect2ItemModel() { id = $"{f} {nap}", text = $"{f} {nap}" }));
            Enumerable.Range(1, FenyitesTartama.MaxMaganalTarthatoTargyakKorlatozasa).ToList().ForEach(f => model.FenyitesTartamaMaganalTarthatoTargyakKorlatozasaOptions.Add(new KSelect2ItemModel() { id = $"{f} {honap}", text = $"{f} {honap}" }));
            Enumerable.Range(1, FenyitesTartama.MaxProgaromokonValoResztvetelKorlatozasa).ToList().ForEach(f => model.FenyitesTartamaProgaromokonValoResztvetelKorlatozasaOptions.Add(new KSelect2ItemModel() { id = $"{f} {honap}", text = $"{f} {honap}" }));
            Enumerable.Range(1, FenyitesTartama.MaxProgaromokonValoResztvetelKorlatozasa).ToList().ForEach(f => model.FenyitesTartamaProgaromokonValoResztvetelKorlatozasaOptions.Add(new KSelect2ItemModel() { id = $"{f} {alkalom}", text = $"{f} {alkalom}" }));
            Enumerable.Range(1, FenyitesTartama.MaxProgaromokonValoResztvetelTiltasa).ToList().ForEach(f => model.FenyitesTartamaProgaromokonValoResztvetelTiltasaOptions.Add(new KSelect2ItemModel() { id = $"{f} {honap}", text = $"{f} {honap}" }));
            Enumerable.Range(1, FenyitesTartama.MaxTobbletSzolgaltatasokMegvonasa).ToList().ForEach(f => model.FenyitesTartamaTobbletSzolgaltatasokMegvonasaOptions.Add(new KSelect2ItemModel() { id = $"{f} {honap}", text = $"{f} {honap}" }));
            Enumerable.Range(1, FenyitesTartama.MaxKimaradasMegvonas).ToList().ForEach(f => model.FenyitesTartamaKimaradasMegvonasaOptions.Add(new KSelect2ItemModel() { id = $"{f} {alkalom}", text = $"{f} {alkalom}" }));

            if (naplobejegyzesId.HasValue)
            {
                var naplobejegyzes = KonasoftBVFonixContext.Naplo.Single(s => s.Id == naplobejegyzesId.Value);

                if (naplobejegyzes.MegszuntetesOkaCimkeId == MegszuntetesOkai.UjEljarasLefolytatasa.CastToInt())
                    throw new Exception("Új eljárás lefolytatása miatt nem lehet megszüntetni elsőfokú határozatot!");

                model.PanasszalElt = !naplobejegyzes.AlairtaFl.Value;
                model.Leiras = naplobejegyzes.JegyzokonyvTartalma;
                model.FenyitesTipusaCimkeId = naplobejegyzes.FenyitesTipusCimkeId;
                model.MegszuntetesOkaCimkeId = naplobejegyzes.MegszuntetesOkaCimkeId;
                model.FenyitesHosszaEsTipusa = $"{naplobejegyzes.FenyitesHossza} {fenyitesHosszanakMennyisegiEgysegei.SingleOrDefault(s => s.Id == naplobejegyzes.FenyitesHosszaMennyisegiEgysegCimkeId)?.Nev.ToLower()}";
                model.FegyelmiVetsegTipusaCimkeId = naplobejegyzes.FegyelmiVetsegTipusaCimkeId;
                model.VetsegRendeletSzerintCimkeId = naplobejegyzes.VetsegRendeletSzerintCimkeId;
                model.KietkezesCsokkentes = naplobejegyzes.KietkezesCsokkentes;
                model.NaplobejegyzesId = naplobejegyzesId;
            }
            return model;
        }

        private bool IsFogvatartottVegrehajtasiFokaElzaras(int fogvatartottId)
        {
            var fogvatartott = KonasoftBVFonixContext.Fogvatartottak.Single(s => s.Id == fogvatartottId);

            if (KodszotarEnums.Elazarasok.Contains(fogvatartott.VegrehajtasiFokKszId))
                return true;

            return false;
        }

        private int GetMaxMaganelzarasTartama(int fogvatartottId)
        {
            var now = DateTime.Now;

            var fogvatartott = KonasoftBVFonixContext.Fogvatartottak.Single(s => s.Id == fogvatartottId);
            //var munkaltatas = KonasoftBVFonixContext.Munkaltatasok.SingleOrDefault(s => s.FogvatartottId == fogvatartottId && s.Kezdete <= now && (s.Vege == null || s.Vege >= now));

            bool IsNemDolgozas = true;

            //if (munkaltatas != null)
            //{
            //    IsNemDolgozas = munkaltatas.IsNemDolgozas;
            //}

            int maganelzarasMaxTartama;
            if (fogvatartott.VegrehajtasiFokKszId == KodszotarEnums.VegrehajtasiFokok.FiatalkoruFoghaz.CastToInt() && IsNemDolgozas == false)
                maganelzarasMaxTartama = FenyitesTartama.MaxMaganelzarasFiatalkoruFoghazDolgozik;
            else if (fogvatartott.VegrehajtasiFokKszId == KodszotarEnums.VegrehajtasiFokok.FiatalkoruFoghaz.CastToInt() && IsNemDolgozas == true)
                maganelzarasMaxTartama = FenyitesTartama.MaxMaganelzarasFiatalkoruFoghazNemDolgozik;
            else if (fogvatartott.VegrehajtasiFokKszId == KodszotarEnums.VegrehajtasiFokok.FiatalkoruBorton.CastToInt() && IsNemDolgozas == false)
                maganelzarasMaxTartama = FenyitesTartama.MaxMaganelzarasFiatalkoruBortonDolgozik;
            else if (fogvatartott.VegrehajtasiFokKszId == KodszotarEnums.VegrehajtasiFokok.FiatalkoruBorton.CastToInt() && IsNemDolgozas == true)
                maganelzarasMaxTartama = FenyitesTartama.MaxMaganelzarasFiatalkoruBortonNemDolgozik;
            else if (fogvatartott.VegrehajtasiFokKszId == KodszotarEnums.VegrehajtasiFokok.Foghaz.CastToInt() && IsNemDolgozas == false)
                maganelzarasMaxTartama = FenyitesTartama.MaxMaganelzarasFoghazDolgozik;
            else if (fogvatartott.VegrehajtasiFokKszId == KodszotarEnums.VegrehajtasiFokok.Foghaz.CastToInt() && IsNemDolgozas == true)
                maganelzarasMaxTartama = FenyitesTartama.MaxMaganelzarasFoghazNemDolgozik;
            else if (fogvatartott.VegrehajtasiFokKszId == KodszotarEnums.VegrehajtasiFokok.Borton.CastToInt() && IsNemDolgozas == false)
                maganelzarasMaxTartama = FenyitesTartama.MaxMaganelzarasBortonDolgozik;
            else if (fogvatartott.VegrehajtasiFokKszId == KodszotarEnums.VegrehajtasiFokok.Borton.CastToInt() && IsNemDolgozas == true)
                maganelzarasMaxTartama = FenyitesTartama.MaxMaganelzarasBortonNemDolgozik;
            else if (fogvatartott.VegrehajtasiFokKszId == KodszotarEnums.VegrehajtasiFokok.Fegyhaz.CastToInt() && IsNemDolgozas == false)
                maganelzarasMaxTartama = FenyitesTartama.MaxMaganelzarasFegyhazDolgozik;
            else if (fogvatartott.VegrehajtasiFokKszId == KodszotarEnums.VegrehajtasiFokok.Fegyhaz.CastToInt() && IsNemDolgozas == true)
                maganelzarasMaxTartama = FenyitesTartama.MaxMaganelzarasFegyhazNemDolgozik;
            else if (fogvatartott.VegrehajtasiFokKszId == KodszotarEnums.VegrehajtasiFokok.KozerdekuMunkaAtvaltoztatasaFoghaz.CastToInt() && IsNemDolgozas == false)
                maganelzarasMaxTartama = FenyitesTartama.MaxMaganelzarasKozerdekuMunkaAtvaltoztatasaFoghazDolgozik;
            else if (fogvatartott.VegrehajtasiFokKszId == KodszotarEnums.VegrehajtasiFokok.KozerdekuMunkaAtvaltoztatasaFoghaz.CastToInt() && IsNemDolgozas == true)
                maganelzarasMaxTartama = FenyitesTartama.MaxMaganelzarasKozerdekuMunkaAtvaltoztatasaFoghazNemDolgozik;
            else if (fogvatartott.VegrehajtasiFokKszId == KodszotarEnums.VegrehajtasiFokok.PenzbuntHelySzabVesztesFoghaz.CastToInt() && IsNemDolgozas == false)
                maganelzarasMaxTartama = FenyitesTartama.MaxMaganelzarasPenzbuntHelySzabVesztesFoghazDolgozik;
            else if (fogvatartott.VegrehajtasiFokKszId == KodszotarEnums.VegrehajtasiFokok.PenzbuntHelySzabVesztesFoghaz.CastToInt() && IsNemDolgozas == true)
                maganelzarasMaxTartama = FenyitesTartama.MaxMaganelzarasPenzbuntHelySzabVesztesFoghazNemDolgozik;
            else if (KodszotarEnums.Elazarasok.Contains(fogvatartott.VegrehajtasiFokKszId) && IsNemDolgozas == false)
                maganelzarasMaxTartama = FenyitesTartama.MaxMaganelzarasElzarasDolgozik;
            else if (KodszotarEnums.Elazarasok.Contains(fogvatartott.VegrehajtasiFokKszId) && IsNemDolgozas == true)
                maganelzarasMaxTartama = FenyitesTartama.MaxMaganelzarasElzarasNemDolgozik;
            else if (KodszotarEnums.Letartoztatottak.Contains(fogvatartott.VegrehajtasiFokKszId) && IsNemDolgozas == false)
                maganelzarasMaxTartama = FenyitesTartama.MaxMaganelzarasLetartoztatottDolgozik;
            else if (KodszotarEnums.Letartoztatottak.Contains(fogvatartott.VegrehajtasiFokKszId) && IsNemDolgozas == true)
                maganelzarasMaxTartama = FenyitesTartama.MaxMaganelzarasLetartoztatottNemDolgozik;
            else
                maganelzarasMaxTartama = FenyitesTartama.MaxMaganelzarasNincs;
                // throw new Exception("Nem definiált magánelzárás tartama. A fogvatartott végrehajtási fokozata nincs lekezelve, vagy ilyen végrehajtási fokkal nem mehet magánelzárásba");
            return maganelzarasMaxTartama;
        }

        public void SaveFegyelmiUgyHatarozat(FegyelmiUgyHatarozatRogziteseModel model)
        {
            var aktIntezet = JogosultsagCacheFunctions.AktualisIntezet.Id;
            List<int> ujFegyelmi = new List<int>(0);
            List<int> megvaltozottFegyelmi = new List<int>(0);
            List<int> toroltFegyelmi = new List<int>(0);

            string szandekossagSzoveg = "";
            var fegyelmiUgy = FindById(model.FegyelmiUgyId);
            var fenyitesHosszanakMennyisegiEgysegei = CimkeFunctions.GetFegyelmiFenyitesHosszanakMennyisegiEgysegei();
            var fenyitesHosszTipusa = fenyitesHosszanakMennyisegiEgysegei.SingleOrDefault(s => s.Nev.ToLower() == model.FenyitesHosszaMennyisegiEgyseg?.ToLower())?.Id;
            using (var transaction = KonasoftBVFonixContext.Database.BeginTransaction())
            {
                try
                {
                    if (model.MegszuntetesOkaCimkeId == MegszuntetesOkai.UjEljarasLefolytatasa.CastToInt())
                        throw new Exception("Új eljárás lefolytatása miatt nem lehet megszüntetni elsőfokú határozatot!");

                    var naplok = NaploFunctions.GetNaplobejegyzesekByFegyelmiUgyId(model.FegyelmiUgyId);
                    var jegyzokonyvek = naplok.Where(x => x.TipusCimkeId == (int)CimkeEnums.FegyelmiNaploTipus.TargyalasiJegyzokonyv && x.AlairtaFl == true).ToList();
                    if ((jegyzokonyvek.Count() == 0 || jegyzokonyvek == null) && fegyelmiUgy.KozvetitoiSikeres == false)
                    {
                        throw new WarningException("A határozat csak a jegyzőkönyv rögzítése után rögzíthető.");
                    }

                    double hossz = 0;
                    DateTime fenyitesVegeDatum = DateTime.MinValue;
                    if (fenyitesHosszTipusa == (int)CimkeEnums.MennyisegiEgysegek.Honap)
                    {
                        fenyitesVegeDatum = DateTime.Now.AddMonths(model.FenyitesHossza.Value);
                        var kulonbseg = fenyitesVegeDatum - DateTime.Now;
                        hossz = kulonbseg.TotalDays;
                    }
                    if (fenyitesHosszTipusa == (int)CimkeEnums.MennyisegiEgysegek.Nap)
                    {
                        fenyitesVegeDatum = DateTime.Now.AddDays(model.FenyitesHossza.Value);
                        var kulonbseg = fenyitesVegeDatum - DateTime.Now;
                        hossz = kulonbseg.TotalDays;
                    }
                    if (fenyitesHosszTipusa == (int)CimkeEnums.MennyisegiEgysegek.Alkalom)
                    {
                        hossz = model.FenyitesHossza.Value;
                    }
                    if (model.IsVegleges)
                    {
                        //Ha az űrlapon nincs a panasznál pipa, akkor az státusz: "Fenyítés kiszabva" lesz, és az ügyben a HatarozatJogerosFL = 1,
                        //különben a státusz: "II. fokú tárgyalás", HatarozatJogerosFL = 0, és frissítjük az ügy fokát 2 - re.
                        if (model.FenyitesTipusaCimkeId == FenyitesTipusok.Megszuntetes.CastToInt())
                        {
                            fegyelmiUgy.StatuszCimkeId = (int)FegyelmiUgyStatusz.Megszuntetve;
                            fegyelmiUgy.MegszuntetesOkaCimkeId = model.MegszuntetesOkaCimkeId;
                            toroltFegyelmi = new List<int>();
                            toroltFegyelmi.Add(fegyelmiUgy.Id);
                        }
                        else if (!model.PanasszalElt && model.FenyitesTipusaCimkeId == FenyitesTipusok.Feddes.CastToInt())
                        {
                            fegyelmiUgy.StatuszCimkeId = (int)FegyelmiUgyStatusz.FenyitesVegrehajtva;
                            fegyelmiUgy.MegszuntetesOkaCimkeId = null;
                            model.MegszuntetesOkaCimkeId = null;
                            toroltFegyelmi = new List<int>();
                            toroltFegyelmi.Add(fegyelmiUgy.Id);
                        }
                        else
                        {
                            if (model.PanasszalElt)
                            {
                                fegyelmiUgy.StatuszCimkeId = (int)FegyelmiUgyStatusz.II_FokuTargyalas;
                                fegyelmiUgy.Lezarva = false;
                            }
                            else
                            {
                                fegyelmiUgy.StatuszCimkeId = (int)FegyelmiUgyStatusz.FenyitesKiszabva;
                                fegyelmiUgy.Lezarva = false;
                            }
                            fegyelmiUgy.MegszuntetesOkaCimkeId = null;
                            model.MegszuntetesOkaCimkeId = null;
                            megvaltozottFegyelmi = new List<int>();
                            megvaltozottFegyelmi.Add(fegyelmiUgy.Id);
                        }
                        Modify(fegyelmiUgy);

                        //15798-as backlog
                        fegyelmiUgy.Hatarido = GetFegyelmiHatarido(fegyelmiUgy.Id, false, fegyelmiUgy.StatuszCimkeId);

                        fegyelmiUgy.HatarozatJogerosFL = !model.PanasszalElt;
                        if (model.FenyitesTipusaCimkeId == FenyitesTipusok.Megszuntetes.CastToInt() ||
                           (!model.PanasszalElt && model.FenyitesTipusaCimkeId == FenyitesTipusok.Feddes.CastToInt()))
                        {
                            fegyelmiUgy.Lezarva = true;
                            fegyelmiUgy.Hatarido = null;
                            OsszevontFegyelmiUgyekLezarasaByFegyelmiUgyId(fegyelmiUgy.Id);
                        }
                        else
                        {
                            fegyelmiUgy.Lezarva = false;
                        }
                        fegyelmiUgy.FenyitesTipusCimkeId = model.FenyitesTipusaCimkeId;
                        fegyelmiUgy.KietkezesCsokkentes = model.KietkezesCsokkentes;
                        fegyelmiUgy.HatarozatIndoklasa = model.Leiras;
                        fegyelmiUgy.FegyelmiVetsegTipusaCimkeId = model.FegyelmiVetsegTipusaCimkeId;
                        fegyelmiUgy.VetsegRendeletSzerintCimkeId = model.VetsegRendeletSzerintCimkeId;
                        fegyelmiUgy.HatarozatotHozottSzemelyId = SzemelyzetFunctions.SzemelyzetLekeresVagyLetrehozas(WindowsIdentity.GetCurrent().User.Value, null, null).Id;
                        fegyelmiUgy.HatarozatDatuma = DateTime.Now;
                        fegyelmiUgy.FenyitesHossza = model.FenyitesHossza;
                        fegyelmiUgy.VegrehajtasHosszEddig = (float)hossz;
                        fegyelmiUgy.FenyitesHosszaMennyisegiEgysegCimkeId = fenyitesHosszTipusa;
                        if (model.PanasszalElt)
                        {
                            fegyelmiUgy.UgyFoka = 2;
                            if (model.FenyitesTipusaCimkeId == FenyitesTipusok.Maganelzaras.CastToInt())
                            {
                                fegyelmiUgy.HatarozatHozoJogkoreCimkeId = HatarozatHozoJogkore.BVBiro.CastToInt();
                            }
                            else
                            {
                                switch (fegyelmiUgy.HatarozatHozoJogkoreCimkeId)
                                {
                                    case (int)HatarozatHozoJogkore.FegyelmiJogkorGyakorloja:
                                        fegyelmiUgy.HatarozatHozoJogkoreCimkeId = HatarozatHozoJogkore.Parancsnok.CastToInt();
                                        break;
                                    case (int)HatarozatHozoJogkore.Parancsnok:
                                        fegyelmiUgy.HatarozatHozoJogkoreCimkeId = HatarozatHozoJogkore.BVBiro.CastToInt();
                                        break;
                                    default:
                                        throw new Exception("Nem definiált HatarozatHozoJogkore lépegetés");
                                }
                            }
                            fegyelmiUgy.StatuszCimkeId = (int)FegyelmiUgyStatusz.II_FokuTargyalas;
                        }
                        else
                        {
                            fegyelmiUgy.UgyFoka = 1;
                            fegyelmiUgy.HatarozatHozoJogkoreCimkeId = HatarozatHozoJogkore.FegyelmiJogkorGyakorloja.CastToInt();
                            if (model.FenyitesTipusaCimkeId != FenyitesTipusok.Feddes.CastToInt() && model.FenyitesTipusaCimkeId != FenyitesTipusok.Megszuntetes.CastToInt())
                            {
                                fegyelmiUgy.StatuszCimkeId = (int)FegyelmiUgyStatusz.FenyitesKiszabva;
                                fegyelmiUgy.Lezarva = false;
                            }
                            else
                            {
                                fegyelmiUgy.Lezarva = true;
                            }

                            if (model.FenyitesTipusaCimkeId != (int)FenyitesTipusok.Megszuntetes && fegyelmiUgy.KarteritesId != null) 
                            {
                                bool szandekosFl = model.IsSzandekosKarokozas ?? false;
                                // BvBankban szándékos károkozás esetén be kell billenteni egyre a fegyelmi_eljaras_szandekos_volt flag-et
                                //IKarteritesekFunctions karteritesekFunctions = new KarteritesekFunctions();
                                //karteritesekFunctions.SetKarteritesSzandekossag(fegyelmiUgy.KarteritesId.Value, szandekosFl);
                                //if (szandekosFl)
                                //    szandekossagSzoveg += "<p><i>Kártérítésre kötelezett.</i></p>";
                                //else
                                //    szandekossagSzoveg += "<p><i>Kártérítésre nem kötelezett.</i></p>";

                            }
                        }

                        Modify(fegyelmiUgy);
                      
                    }

                    if (model.NaplobejegyzesId.HasValue)
                    {
                        NaploFunctions.ModifyNaploBejegyzes(model);
                    }
                    else
                    {
                        var naploEnitity = new Naplo
                        {
                            FegyelmiUgyId = model.FegyelmiUgyId,
                            FogvatartottId = fegyelmiUgy.FogvatartottId,
                            AlairtaFl = !model.PanasszalElt,
                            Vegleges = model.IsVegleges,
                            JegyzokonyvTartalma = $"{model.Leiras}{szandekossagSzoveg}",
                            RogzitoIntezetId = aktIntezet,
                            TipusCimkeId = (int)FegyelmiNaploTipus.HatarozatRogzitese,
                            DonteshozoSzemelyId = SzemelyzetFunctions.SzemelyzetLekeresVagyLetrehozas(WindowsIdentity.GetCurrent().User.Value, null, null).Id,
                            FenyitesTipusCimkeId = model.FenyitesTipusaCimkeId,
                            FegyelmiVetsegTipusaCimkeId = model.FegyelmiVetsegTipusaCimkeId,
                            VetsegRendeletSzerintCimkeId = model.VetsegRendeletSzerintCimkeId,
                            KietkezesCsokkentes = model.KietkezesCsokkentes,
                            FenyitesHossza = model.FenyitesHossza,
                            FenyitesHosszaMennyisegiEgysegCimkeId = fenyitesHosszTipusa,
                            MegszuntetesOkaCimkeId = model.FenyitesTipusaCimkeId == FenyitesTipusok.Megszuntetes.CastToInt() ? model.MegszuntetesOkaCimkeId : null
                        };

                        KonasoftBVFonixContext.Naplo.Add(naploEnitity);
                        KonasoftBVFonixContext.SaveChanges();
                        model.NaplobejegyzesId = naploEnitity.Id;
                    }
                    transaction.Commit();

                }
                catch (Exception e)
                {
                    Log.Error("SaveFegyelmiUgyHatarozat hiba", e);
                    transaction.Rollback();
                    throw;
                }
                //emiatt nem szállhat el a fegyelmi
                var fogvatartottRezsimKszId = KonasoftBVFonixContext.Fogvatartottak.Single(s => s.Id == fegyelmiUgy.FogvatartottId).RezsimKszId;

                //if (model.PanasszalElt != true &&
                //    fogvatartottRezsimKszId != KodszotarEnums.RezsimTipusok.Szigorubb.CastToInt() &&
                //    (model.FenyitesTipusaCimkeId == FenyitesTipusok.Maganelzaras.CastToInt() ||
                //    (CimkeEnums.BfbElojegyzesFegyelmiVetsegTipusAlapjan.Contains(model.FegyelmiVetsegTipusaCimkeId.Value) && model.FenyitesTipusaCimkeId != FenyitesTipusok.Megszuntetes.CastToInt())))
                //{

                //    new F3VegrehajtasiListaFunctions().CreateBFBElojegyzes(
                //        AlkalmazasKontextusFunctions.Kontextus.SzemelyzetSid,
                //        AlkalmazasKontextusFunctions.Kontextus.RogzitoIntezetId,
                //        fegyelmiUgy.FogvatartottId);
                //}
            }

            OnFegyelmiUgyValtozas?.Invoke(ujFegyelmi, megvaltozottFegyelmi, toroltFegyelmi);
        }


        public List<int> SaveFegyelmiUgyTanuMeghallgatasiJegyzokonyv(FegyelmiUgyTanuMeghallgatasiJegyzokonyvModel model)
        {

            var aktIntezet = JogosultsagCacheFunctions.AktualisIntezet.Id;
            List<int> naploIds = new List<int>();

            List<int> ujFegyelmi = new List<int>();
            List<int> megvaltozottFegyelmi = new List<int>();
            List<int> toroltFegyelmi = new List<int>();
            using (DbContextTransaction transaction = KonasoftBVFonixContext.Database.BeginTransaction())
            {
                var currentId = 0;
                try
                {

                    if (!model.NaplobejegyzesIds.IsNullOrEmpty())
                    {
                        foreach (var naplobejegyzesId in model.NaplobejegyzesIds)
                        {
                            var naploBejegyzes = NaploFunctions.FindById(naplobejegyzesId);
                            currentId = naploBejegyzes.FegyelmiUgyId;
                            naploBejegyzes.JegyzokonyvTartalma = model.Leiras;
                            int? jegyzokonyvVezetoSid = null;
                            if (model.JegyzokonyvVezetoSid != null)
                            {
                                jegyzokonyvVezetoSid = SzemelyzetFunctions.SzemelyzetLekeresVagyLetrehozas(model.JegyzokonyvVezetoSid, null, null).Id;
                            }
                            naploBejegyzes.JegyzokonyvVezetoSzemelyId = jegyzokonyvVezetoSid;
                            naploBejegyzes.MeghallgatoSzemelyId = SzemelyzetFunctions.SzemelyzetLekeresVagyLetrehozas(model.MeghallgatoSid, null, null).Id;
                            naploBejegyzes.KihallgatasIntezetId = AlkalmazasKontextusFunctions.Kontextus.RogzitoIntezetId;
                            naploBejegyzes.TovabbiJelenlevok = model.TovabbiSzemelyekList == null ? null : string.Join(", ", model.TovabbiSzemelyekList);
                            naploBejegyzes.AlairtaFl = model.AlairtaFl;
                            naploBejegyzes.InkognitoFl = model.InkognitoFl;
                            NaploFunctions.Modify(naploBejegyzes);
                            KonasoftBVFonixContext.SaveChanges();
                            naploIds.Add(naploBejegyzes.Id);
                            megvaltozottFegyelmi.Add(naploBejegyzes.FegyelmiUgyId);
                        }
                    }
                    else
                    {

                        foreach (var fegyelmiUgyId in model.FegyelmiUgyIds)
                        {
                            var fegyelmiUgy = FindById(fegyelmiUgyId);
                            currentId = fegyelmiUgyId;
                            var tanu = KonasoftBVFonixContext.EsemenyResztvevok.Single(x => x.Id == model.TanuId);
                            var tipus = tanu.FogvatartottId == fegyelmiUgy.FogvatartottId ? (int)FegyelmiNaploTipus.EljarasAlaVontMeghallgatasa : (int)FegyelmiNaploTipus.TanuMeghallgatas;

                            int? jegyzokonyvVezetoSid = null;
                            if (model.JegyzokonyvVezetoSid != null)
                            {
                                jegyzokonyvVezetoSid = SzemelyzetFunctions.SzemelyzetLekeresVagyLetrehozas(model.JegyzokonyvVezetoSid, null, null).Id;
                            }
                            var entity = new Naplo()
                            {
                                FegyelmiUgyId = fegyelmiUgyId,
                                FogvatartottId = tanu.FogvatartottId,
                                JegyzokonyvTartalma = model.Leiras,
                                JegyzokonyvVezetoSzemelyId = jegyzokonyvVezetoSid,
                                MeghallgatoSzemelyId = SzemelyzetFunctions.SzemelyzetLekeresVagyLetrehozas(model.MeghallgatoSid, null, null).Id,
                                KihallgatasIntezetId = AlkalmazasKontextusFunctions.Kontextus.RogzitoIntezetId,
                                TipusCimkeId = tipus,
                                TovabbiJelenlevok = model.TovabbiSzemelyekList == null ? null : string.Join(", ", model.TovabbiSzemelyekList),
                                AlairtaFl = model.AlairtaFl,
                                InkognitoFl = model.InkognitoFl,
                            };
                            if (tanu.FogvatartottId == fegyelmiUgy.FogvatartottId)
                            {
                                fegyelmiUgy.EljarasAlaVontatMeghallgattukFL = true;
                            }
                            Modify(fegyelmiUgy);

                            KonasoftBVFonixContext.Naplo.Add(entity);
                            KonasoftBVFonixContext.SaveChanges();
                            naploIds.Add(entity.Id);
                            megvaltozottFegyelmi.Add(fegyelmiUgyId);

                        }
                    }
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    Log.Error($"Hiba a tanúmeghallgatási jegyzőkönyv mentés során (fegyelmiUgyId: {currentId})", e);
                    transaction.Rollback();
                    throw;
                }


            }

            OnFegyelmiUgyValtozas?.Invoke(ujFegyelmi, megvaltozottFegyelmi, toroltFegyelmi);
            return naploIds;
        }

        public List<int> SaveFegyelmiUgySzemelyiAllomanyiTanuMeghallgatasiJegyzokonyv(FegyelmiUgySzemelyiAllomanyiTanuMeghallgatasiJegyzokonyvModel model)
        {
            var aktIntezet = JogosultsagCacheFunctions.AktualisIntezet.Id;
            List<int> naploIds = new List<int>();

            List<int> ujFegyelmi = new List<int>();
            List<int> megvaltozottFegyelmi = new List<int>();
            List<int> toroltFegyelmi = new List<int>();
            using (DbContextTransaction transaction = KonasoftBVFonixContext.Database.BeginTransaction())
            {
                var currentId = 0;
                try
                {

                    if (!model.NaplobejegyzesIds.IsNullOrEmpty())
                    {
                        foreach (var naplobejegyzesId in model.NaplobejegyzesIds)
                        {
                            var naploBejegyzes = NaploFunctions.FindById(naplobejegyzesId);
                            currentId = naploBejegyzes.FegyelmiUgyId;
                            naploBejegyzes.JegyzokonyvTartalma = model.Leiras;
                            naploBejegyzes.JegyzokonyvVezetoSzemelyId = null;
                            naploBejegyzes.MeghallgatoSzemelyId = SzemelyzetFunctions.SzemelyzetLekeresVagyLetrehozas(model.MeghallgatoSid, null, null).Id;
                            naploBejegyzes.KihallgatasIntezetId = AlkalmazasKontextusFunctions.Kontextus.RogzitoIntezetId;
                            naploBejegyzes.TipusCimkeId = (int)FegyelmiNaploTipus.SzemelyiAllomanyiTanuMeghallgatasa;
                            naploBejegyzes.TovabbiJelenlevok = null;
                            naploBejegyzes.AlairtaFl = model.AlairtaFl;
                            naploBejegyzes.SzemelyiAllomanyiTanuSzemelyId = SzemelyzetFunctions.SzemelyzetLekeresVagyLetrehozas(model.TanuSid, null, null).Id;
                            NaploFunctions.Modify(naploBejegyzes);
                            KonasoftBVFonixContext.SaveChanges();
                            naploIds.Add(naploBejegyzes.Id);
                            megvaltozottFegyelmi.Add(naploBejegyzes.FegyelmiUgyId);
                        }
                    }
                    else
                    {
                        var tanuSzemely = SzemelyzetFunctions.SzemelyzetLekeresVagyLetrehozas(model.TanuSid, null, null);

                        var hianyzoEsemenyIds = (from esemeny in KonasoftBVFonixContext.Esemenyek
                                                 join ugy in KonasoftBVFonixContext.FegyelmiUgyek on esemeny.Id equals ugy.EsemenyId
                                                 join resztvevo in KonasoftBVFonixContext.EsemenyResztvevok on new { AllomanyiSzemelyId = (int?)tanuSzemely.Id, EsemenyId = esemeny.Id } equals new { resztvevo.AllomanyiSzemelyId, resztvevo.EsemenyId } into resztvevoL
                                                 from resztvevoLeft in resztvevoL.DefaultIfEmpty()
                                                 where model.FegyelmiUgyIds.Contains(ugy.Id) && resztvevoLeft == null
                                                 select esemeny.Id).ToList();

                        var hianyzoTanuk = new List<Szemelyzet>();

                        foreach (var esemenyId in hianyzoEsemenyIds)
                        {
                            EsemenyResztvevoFunctions.SaveEsemenyResztvevok(new List<int>() { tanuSzemely.Id }, (int)ErintettsegFoka.SzemelyiAllomanyiTanu, esemenyId);
                        }

                        foreach (var fegyelmiUgyId in model.FegyelmiUgyIds)
                        {
                            var fegyelmiUgy = FindById(fegyelmiUgyId);
                            currentId = fegyelmiUgyId;

                            var entity = new Naplo()
                            {
                                FegyelmiUgyId = fegyelmiUgyId,
                                FogvatartottId = null,
                                JegyzokonyvTartalma = model.Leiras,
                                JegyzokonyvVezetoSzemelyId = null,
                                MeghallgatoSzemelyId = SzemelyzetFunctions.SzemelyzetLekeresVagyLetrehozas(model.MeghallgatoSid, null, null).Id,
                                KihallgatasIntezetId = AlkalmazasKontextusFunctions.Kontextus.RogzitoIntezetId,
                                TipusCimkeId = (int)FegyelmiNaploTipus.SzemelyiAllomanyiTanuMeghallgatasa,
                                TovabbiJelenlevok = null,
                                InkognitoFl = true,
                                AlairtaFl = model.AlairtaFl,
                                SzemelyiAllomanyiTanuSzemelyId = tanuSzemely.Id

                            };
                            KonasoftBVFonixContext.Naplo.Add(entity);
                            KonasoftBVFonixContext.SaveChanges();
                            naploIds.Add(entity.Id);
                            megvaltozottFegyelmi.Add(fegyelmiUgyId);

                        }
                    }
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    Log.Error($"Hiba a személyi állomány tanúmeghallgatási jegyzőkönyv mentés során (fegyelmiUgyId: {currentId})", e);
                    transaction.Rollback();
                    throw;
                }

            }
            OnFegyelmiUgyValtozas?.Invoke(ujFegyelmi, megvaltozottFegyelmi, toroltFegyelmi);
            return naploIds;
        }
        public List<int> SaveFegyelmiUgyElsoFokuTargyalasiJegyzokonyv(FegyelmiUgyElsoFokuTargyalasiJegyzokonyvModel model)
        {
            var aktIntezet = JogosultsagCacheFunctions.AktualisIntezet.Id;
            List<int> naploIds = new List<int>();

            List<int> ujFegyelmi = new List<int>();
            List<int> megvaltozottFegyelmi = new List<int>();
            List<int> toroltFegyelmi = new List<int>();

            using (DbContextTransaction transaction = KonasoftBVFonixContext.Database.BeginTransaction())
            {
                var currentId = 0;
                try
                {

                    if (!model.NaplobejegyzesIds.IsNullOrEmpty())
                    {
                        foreach (var naplobejegyzesId in model.NaplobejegyzesIds)
                        {
                            var naploBejegyzes = NaploFunctions.FindById(naplobejegyzesId);
                            naploBejegyzes.JegyzokonyvTartalma = model.Leiras;
                            naploBejegyzes.JegyzokonyvVezetoSzemelyId = SzemelyzetFunctions.SzemelyzetLekeresVagyLetrehozas(model.JegyzokonyvVezetoSid, null, null).Id;
                            naploBejegyzes.DonteshozoSzemelyId = SzemelyzetFunctions.SzemelyzetLekeresVagyLetrehozas(model.FegyelmiJogkorGyakorlojaSid, null, null).Id;
                            naploBejegyzes.KihallgatasIntezetId = AlkalmazasKontextusFunctions.Kontextus.RogzitoIntezetId;
                            naploBejegyzes.TovabbiJelenlevok = model.TovabbiSzemelyekList == null ? null : string.Join(", ", model.TovabbiSzemelyekList);
                            naploBejegyzes.AlairtaFl = model.AlairtaFl;
                            NaploFunctions.Modify(naploBejegyzes);
                            KonasoftBVFonixContext.SaveChanges();
                            naploIds.Add(naploBejegyzes.Id);
                            megvaltozottFegyelmi.Add(naploBejegyzes.FegyelmiUgyId);
                        }
                    }
                    else
                    {
                        foreach (var fegyelmiUgyId in model.FegyelmiUgyIds)
                        {
                            var fegyelmiUgy = FindById(fegyelmiUgyId);
                            currentId = fegyelmiUgyId;

                            var entity = new Naplo()
                            {
                                FegyelmiUgyId = fegyelmiUgyId,
                                JegyzokonyvTartalma = model.Leiras,
                                JegyzokonyvVezetoSzemelyId = SzemelyzetFunctions.SzemelyzetLekeresVagyLetrehozas(model.JegyzokonyvVezetoSid, null, null).Id,
                                DonteshozoSzemelyId = SzemelyzetFunctions.SzemelyzetLekeresVagyLetrehozas(model.FegyelmiJogkorGyakorlojaSid, null, null).Id,
                                KihallgatasIntezetId = AlkalmazasKontextusFunctions.Kontextus.RogzitoIntezetId,
                                AlairtaFl = model.AlairtaFl,
                                TipusCimkeId = (int)FegyelmiNaploTipus.TargyalasiJegyzokonyv,
                                TovabbiJelenlevok = model.TovabbiSzemelyekList == null ? null : string.Join(", ", model.TovabbiSzemelyekList)
                            };
                            KonasoftBVFonixContext.Naplo.Add(entity);
                            KonasoftBVFonixContext.SaveChanges();
                            naploIds.Add(entity.Id);
                            megvaltozottFegyelmi.Add(fegyelmiUgyId);

                        }
                    }
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    Log.Error($"Hiba a elsőfokú tárgyalási jegyzőkönyv mentés során (fegyelmiUgyId: {currentId})", e);
                    transaction.Rollback();
                    throw;
                }


            }
            OnFegyelmiUgyValtozas?.Invoke(ujFegyelmi, megvaltozottFegyelmi, toroltFegyelmi);
            return naploIds;
        }
        public ReintegraciosTiszt GetReintegraciosTiszt(int fogvatartottId)
        {
            var fonixContext = new KonasoftBVFonixContext();
            var nevelesiCsoportId = fonixContext.Fogvatartottak.Where(w => w.Id == fogvatartottId).Select(s => s.NevelesiCsoportId).SingleOrDefault();
            NevelesiCsoport nevelesiCsoport;
            if (nevelesiCsoportId is null)
                nevelesiCsoport = fonixContext.NevelesiCsoportok.FirstOrDefault();
            else
                nevelesiCsoport = fonixContext.NevelesiCsoportok.FirstOrDefault(x => x.Id == nevelesiCsoportId);
            ReintegraciosTiszt reintTiszt = new ReintegraciosTiszt();
            if (nevelesiCsoportId != null)
            {
                var reintTisztSid = fonixContext.NevelesiCsoportok.Where(w => w.Id == nevelesiCsoportId).Select(s => s.NeveloSzemelySid).SingleOrDefault();
                reintTiszt = fonixContext.Szemelyzet.Where(w => w.AdSid == reintTisztSid && w.IntezetId == nevelesiCsoport.IntezetId).Select(s => new ReintegraciosTiszt
                {
                    //FanySzemelyzetId = reintTisztId,
                    Nev = s.Nev,
                    Rendfokozat = s.Rendfokozat == null ? null : s.Rendfokozat.Nev,
                    Id = s.Id,
                    Sid = s.AdSid
                }).FirstOrDefault();

                if (reintTiszt == null)
                {
                    reintTiszt = fonixContext.Szemelyzet.Where(w => w.AdSid == reintTisztSid).Select(s => new ReintegraciosTiszt
                    {
                        //FanySzemelyzetId = reintTisztId,
                        Nev = s.Nev,
                        Rendfokozat = s.Rendfokozat == null ? null : s.Rendfokozat.Nev,
                        Id = s.Id,
                        Sid = s.AdSid
                    }).FirstOrDefault();
                }
            }


            return reintTiszt;
        }

        public List<ReintegraciosTiszt> GetReintegraciosTisztek(int aktIntezetId)
        {
            var fonixContext = new KonasoftBVFonixContext();
            var reintegraicosTisztSids = fonixContext.NevelesiCsoportok.Where(w => w.IntezetId == aktIntezetId).Select(s => s.NeveloSzemelySid).ToList();
            var tisztek = fonixContext.Szemelyzet.Where(w => reintegraicosTisztSids.Contains(w.AdSid) && w.IntezetId == aktIntezetId).Select(s => new ReintegraciosTiszt
            {
                //FanySzemelyzetId = s.Id,
                Nev = s.Nev,
                Rendfokozat = s.Rendfokozat == null ? null : s.Rendfokozat.Nev,
                Sid = s.AdSid
            }).ToList();

            return tisztek;
        }

        public DateTime? GetFegyelmiHatarido(int fegyelmiUgyId, bool isHataridoModositas, int? leendoStatuszCimkeId = null, bool isKozvetitoi = false)
        {
            DateTime? hatarido = null;
            var fegyelmi = KonasoftBVFonixContext.FegyelmiUgyek.Single(x => x.Id == fegyelmiUgyId);
            if (leendoStatuszCimkeId == null)
                leendoStatuszCimkeId = fegyelmi.StatuszCimkeId;
            DateTime dontesDatum = fegyelmi.DontesDatuma.HasValue ? fegyelmi.DontesDatuma.Value : DateTime.Today;
            DateTime hatarozatDatum = DateTime.Today;
            if (fegyelmi.HatarozatDatuma.HasValue)
            {
                hatarozatDatum = fegyelmi.HatarozatDatuma.Value;
            }

            if (!isKozvetitoi)
            {
                var naploLista = KonasoftBVFonixContext.Naplo.Where(x => x.FegyelmiUgyId == fegyelmiUgyId).ToList();
                if (fegyelmi.KozvetitoiEljarasban == true)
                {
                    return fegyelmi.FelfuggesztesDatum.Value.AddDays(isHataridoModositas ? 26 : 18);
                }
                if (fegyelmi.DontesDatuma.HasValue)
                {
                    hatarido = dontesDatum.AddMonths(6);
                }
                switch (leendoStatuszCimkeId)
                {

                    case (int)FegyelmiUgyStatusz.KivizsgalasFolyamatban:
                        {

                            hatarido = dontesDatum.AddDays(isHataridoModositas ? 30 : 20);
                            if (fegyelmi.Felfuggesztve ?? false)
                            {
                                hatarido = dontesDatum.AddMonths(6);
                            }
                            break;
                        }
                    case (int)FegyelmiUgyStatusz.Kezdemenyezve:
                        {
                            hatarido = fegyelmi.LetrehozasDatuma.AddDays(3);
                            break;
                        }
                    case (int)FegyelmiUgyStatusz.II_FokuTargyalas:
                        {
                            hatarido = dontesDatum.AddMonths(6);
                            break;
                        }
                    case (int)FegyelmiUgyStatusz.ReintegraciosTisztiJogkorben:
                        {
                            hatarido = hatarozatDatum.AddDays(5);
                            if (fegyelmi.Felfuggesztve ?? false)
                            {
                                hatarido = dontesDatum.AddMonths(6);
                            }
                            break;
                        }
                    case (int)FegyelmiUgyStatusz.I_FokuTargyalas:
                        {
                            if (isHataridoModositas)
                            {
                                hatarido = dontesDatum.AddDays(30);
                            }
                            else
                            {
                                hatarido = dontesDatum.AddDays(20);
                            }
                            if (fegyelmi.Felfuggesztve ?? false)
                            {
                                hatarido = dontesDatum.AddMonths(6);
                            }
                            break;
                        }
                    //case (int)FegyelmiUgyStatusz.KozvetitoiEljarasAlatt:
                    //    {
                    //        hatarido = hatarozatDatum.AddDays(15);
                    //        break;
                    //    }
                    case (int)FegyelmiUgyStatusz.FenyitesKiszabva:
                    case (int)FegyelmiUgyStatusz.FenyitesVegrehajtasAlatt:
                    case (int)FegyelmiUgyStatusz.FenyitesVegrehajtasMegszakitva:
                        {
                            hatarido = hatarozatDatum.AddMonths(6);
                            break;
                        }
                }
                if (fegyelmi.HatarozatDatuma.HasValue)
                {
                    hatarido = fegyelmi.HatarozatDatuma.Value.AddMonths(6);
                }
                else
                {
                    FelfuggesztesFunctions FelfuggesztesFunc = new FelfuggesztesFunctions();
                    var felfuggesztettNapokSzama = FelfuggesztesFunc.GetFelfuggesztettNapokSzama(fegyelmiUgyId, fegyelmi.DontesDatuma);
                    hatarido = hatarido.Value.AddDays(felfuggesztettNapokSzama);
                    if ((hatarido == null || hatarido >= (fegyelmi.DontesDatuma.HasValue ? fegyelmi.DontesDatuma.Value.AddMonths(6) : DateTime.Now.AddMonths(6))))
                    {
                        hatarido = (fegyelmi.DontesDatuma.HasValue ? fegyelmi.DontesDatuma.Value.AddMonths(6) : DateTime.Now.AddMonths(6));
                    }
                }


                if (fegyelmi.Felfuggesztve ?? false)
                {
                    var felfuggesztes = KonasoftBVFonixContext.Felfuggesztesek.Where(w => w.FegyelmiUgyId == fegyelmi.Id && w.Vege == null).SingleOrDefault();
                    if (felfuggesztes != null && felfuggesztes.OkaCimkeId == (int)CimkeEnums.FelfuggesztesOka.JogellenesTavollet)
                    {
                        hatarido = new DateTime(2079, 06, 06);
                    }
                }
            }
            else
            {
                if (isHataridoModositas)
                {
                    hatarido = fegyelmi.FelfuggesztesDatum.HasValue ? fegyelmi.FelfuggesztesDatum.Value.AddDays(26) : DateTime.Now.AddDays(26);
                }
                else
                {
                    hatarido = fegyelmi.FelfuggesztesDatum.HasValue ? fegyelmi.FelfuggesztesDatum.Value.AddDays(18) : DateTime.Now.AddDays(18);
                }
            }

            return hatarido;
        }

        public ReintegraciosTisztDontesModel GetReintegraciosTisztDontesModel(List<int> fegyelmiUgyIds, List<int> naploIds)
        {
            ReintegraciosTisztDontesModel dontes = new ReintegraciosTisztDontesModel();
            var aktIntezet = JogosultsagCacheFunctions.AktualisIntezet.Id;

            int fegyelmiUgyId = fegyelmiUgyIds.First();

            dontes = (from fegyelmiUgy in KonasoftBVFonixContext.FegyelmiUgyek
                      where fegyelmiUgy.Id == fegyelmiUgyId
                      select new ReintegraciosTisztDontesModel
                      {
                          FegyelmiVetsegTipusaCimkeId = fegyelmiUgy.FegyelmiVetsegTipusaCimkeId.Value,
                          IsKarterites = fegyelmiUgy.KarteritesId != null
                      }).SingleOrDefault();
            dontes.FegyelmiUgyIds = fegyelmiUgyIds;
            var minimumDatumKivalasztas = KonasoftBVFonixContext.Naplo.Where(w => w.FegyelmiUgyId == fegyelmiUgyId &&
            w.TipusCimkeId == (int)FegyelmiUgyStatusz.ReintegraciosTisztiJogkorben).
                OrderByDescending(o => o.ErvenyessegKezdete).Select(s => s.ErvenyessegKezdete).FirstOrDefault();
            dontes.FenyitesKiszabasIdejeMinDate = minimumDatumKivalasztas.Date;

            dontes.FenyitesKiszabasIdeje = DateTime.Now;
            var reszlegek = KonasoftBVFonixContext.NevelesiCsoportok.Where(w => w.IntezetId == aktIntezet).OrderBy(o => o.Nev).ToList();
            dontes.ReintegraciosReszlegOptions = reszlegek.Select(x => new KSelect2ItemModel() { id = x.Id.ToString(), text = x.Nev }).ToList();
            var felhoId = 15;
            dontes.ReintegraciosReszlegId = reszlegek.Select(s => s.Id).FirstOrDefault();
            dontes.FegyelmiVetsegTipusaOptions = KonasoftBVFonixContext.Cimkek.Where(w => w.FelhoId == felhoId).Select(x => new KSelect2ItemModel() { id = x.Id.ToString(), text = x.Nev }).ToList();
            dontes.Indoklas = string.Empty;
            dontes.IsNemVetteTudomasul = false;
            dontes.NaplobejegyzesIds = new List<int>();
            if (naploIds == null || !naploIds.Any())
            {
                return dontes;
            }
            dontes.NaplobejegyzesIds.AddRange(naploIds);

            var naploId = naploIds.First();
            var naplo = KonasoftBVFonixContext.Naplo.Single(x => x.Id == naploId);
            dontes.ReintegraciosReszlegId = naplo.ReintegraciosReszlegId;
            dontes.FenyitesKiszabasIdeje = naplo?.FenyitesKiszabasDatuma ?? DateTime.Now;
            dontes.FegyelmiVetsegTipusaCimkeId = naplo.FegyelmiVetsegTipusaCimkeId;
            dontes.Indoklas = naplo.JegyzokonyvTartalma;
            dontes.IsNemVetteTudomasul = !naplo.IsFogvatartottElfogadta ?? false;


            return dontes;
        }
        public List<int> SaveReintegraciosTisztDontesKioktatasModel(ReintegraciosTisztDontesModel model)
        {
            //db-ben teszt fegyelmiUgyId = 12
            var aktIntezet = JogosultsagCacheFunctions.AktualisIntezet.Id;
            //var aktIntezet = 111;
            List<int> ujFegyelmi = new List<int>(0);
            List<int> megvaltozottFegyelmi = new List<int>();
            List<int> toroltFegyelmi = new List<int>(0);
            List<int> naploIds = new List<int>(0);
            using (DbContextTransaction transaction = KonasoftBVFonixContext.Database.BeginTransaction())
            {
                int currentId = 0;
                try
                {
                    if (model.NaplobejegyzesIds == null || !model.NaplobejegyzesIds.Any())
                    {
                        foreach (var fegyelmiUgyId in model.FegyelmiUgyIds)
                        {
                            var fegyelmiUgy = (FegyelmiUgyViewModel)Table.Single(x => x.Id == fegyelmiUgyId);
                            if (model.Alairta == true)
                            {
                                fegyelmiUgy.FegyelmiVetsegTipusaCimkeId = model.FegyelmiVetsegTipusaCimkeId; //eredeti 43
                                fegyelmiUgy.StatuszCimkeId = (int)FegyelmiUgyStatusz.FogvatartottKioktatva; // eredeti 89
                                fegyelmiUgy.Visszakuldve = false;
                                fegyelmiUgy.HatarozatJogerosFL = true;
                                fegyelmiUgy.Lezarva = true;
                                fegyelmiUgy.Hatarido = null;
                                fegyelmiUgy.FenyitesTipusCimkeId = null;
                                Modify(fegyelmiUgy);
                            }
                            KonasoftBVFonixContext.SaveChanges();
                            var naploEnitity = new Naplo
                            {
                                FegyelmiUgyId = fegyelmiUgyId,
                                FogvatartottId = fegyelmiUgy.FogvatartottId,
                                AlairtaFl = model.Alairta,
                                IsFogvatartottElfogadta = model.Alairta,
                                JegyzokonyvTartalma = model.Indoklas,
                                RogzitoIntezetId = aktIntezet,
                                TipusCimkeId = (int)FegyelmiNaploTipus.Kioktatas,
                                FenyitesKiszabasDatuma = model.FenyitesKiszabasIdeje,
                                FegyelmiVetsegTipusaCimkeId = model.FegyelmiVetsegTipusaCimkeId,
                                //ReintegraciosReszlegId = model.ReintegraciosReszlegId
                            };

                            KonasoftBVFonixContext.Naplo.Add(naploEnitity);
                            KonasoftBVFonixContext.SaveChanges();
                            naploIds.Add(naploEnitity.Id);
                        }
                    }
                    else
                    {
                        foreach (var naplobejegyzesId in model.NaplobejegyzesIds)
                        {
                            var naplobejegyzes = NaploFunctions.FindById(naplobejegyzesId);

                            var fegyelmiUgy = (FegyelmiUgyViewModel)Table.Single(x => x.Id == naplobejegyzes.FegyelmiUgyId);
                            if (model.Alairta == true)
                            {
                                fegyelmiUgy.FegyelmiVetsegTipusaCimkeId = model.FegyelmiVetsegTipusaCimkeId;
                                fegyelmiUgy.StatuszCimkeId = (int)FegyelmiUgyStatusz.FogvatartottKioktatva;
                                fegyelmiUgy.Hatarido = null;
                                fegyelmiUgy.Lezarva = true;
                                fegyelmiUgy.FenyitesTipusCimkeId = null;
                                fegyelmiUgy.HatarozatJogerosFL = !model.IsNemVetteTudomasul;
                                Modify(fegyelmiUgy);
                            }
                            KonasoftBVFonixContext.SaveChanges();

                            naplobejegyzes.AlairtaFl = model.Alairta;
                            naplobejegyzes.IsFogvatartottElfogadta = model.Alairta;
                            naplobejegyzes.JegyzokonyvTartalma = model.Indoklas;
                            naplobejegyzes.TipusCimkeId = (int)FegyelmiNaploTipus.Kioktatas;
                            naplobejegyzes.FenyitesKiszabasDatuma = model.FenyitesKiszabasIdeje;
                            naplobejegyzes.FegyelmiVetsegTipusaCimkeId = model.FegyelmiVetsegTipusaCimkeId;
                            //naplobejegyzes.ReintegraciosReszlegId = model.ReintegraciosReszlegId;
                            NaploFunctions.Modify(naplobejegyzes);

                            KonasoftBVFonixContext.SaveChanges();
                            naploIds.Add(naplobejegyzesId);
                        }
                    }
                    KonasoftBVFonixContext.SaveChanges();

                    if (model.Alairta == true) toroltFegyelmi.AddRange(model.FegyelmiUgyIds); // különben nem végleges, nem kell törölni

                    transaction.Commit();
                }
                catch (Exception e)
                {
                    Log.Error($"Hiba a reintegrációs tiszti döntés mentés során (fegyelmiUgyId: {currentId})", e);
                    transaction.Rollback();

                    throw;
                }
            }
            OnFegyelmiUgyValtozas?.Invoke(ujFegyelmi, megvaltozottFegyelmi, toroltFegyelmi);
            return naploIds;
        }
        public List<int> SaveReintegraciosTisztDontesFeddesModel(ReintegraciosTisztDontesModel model)
        {
            //db-ben teszt fegyelmiUgyId = 12
            var aktIntezet = JogosultsagCacheFunctions.AktualisIntezet.Id;
            //var aktIntezet = 111;
            var naploTipusaCimkeId = 0;
            var fenyitesTipusaCimkeId = 0;
            int fegyelmiUgyFenyitesTipusCimkeId = (int)FenyitesTipusok.Feddes;
            bool isAlairta = false;
            bool isFogvatartottElfogadta = false;
            string szandekossagSzoveg = "";
            List<int> ujFegyelmi = new List<int>(0);
            List<int> megvaltozottFegyelmi = new List<int>();
            List<int> toroltFegyelmi = new List<int>(0);
            List<int> naplobejegyzesIds = new List<int>();
            using (DbContextTransaction transaction = KonasoftBVFonixContext.Database.BeginTransaction())
            {
                int currentId = 0;
                try
                {
                    if (model.IsNemVetteTudomasul)
                    {
                        fenyitesTipusaCimkeId = (int)FegyelmiUgyStatusz.Kezdemenyezve;
                        naploTipusaCimkeId = (int)FegyelmiNaploTipus.Feddes;
                        isAlairta = false;
                        isFogvatartottElfogadta = false;
                    }
                    else
                    {
                        fenyitesTipusaCimkeId = (int)FegyelmiUgyStatusz.FenyitesVegrehajtva;
                        naploTipusaCimkeId = (int)FegyelmiNaploTipus.Feddes;
                        isAlairta = true;
                        isFogvatartottElfogadta = true;
                    }

                    if (model.NaplobejegyzesIds.IsNullOrEmpty())
                    {
                        foreach (var fegyelmiUgyId in model.FegyelmiUgyIds)
                        {
                            var fegyelmiUgy = (FegyelmiUgyViewModel)Table.Single(x => x.Id == fegyelmiUgyId);
                            if (model.Vegleges == true)
                            {
                                fegyelmiUgy.FegyelmiVetsegTipusaCimkeId = model.FegyelmiVetsegTipusaCimkeId; //eredeti 43
                                fegyelmiUgy.StatuszCimkeId = fenyitesTipusaCimkeId; // eredeti 89
                                fegyelmiUgy.FenyitesTipusCimkeId = fegyelmiUgyFenyitesTipusCimkeId;
                                fegyelmiUgy.HatarozatotHozottSzemelyId = AlkalmazasKontextusFunctions.Kontextus.SzemelyzetId;
                                fegyelmiUgy.Visszakuldve = model.IsNemVetteTudomasul;
                                fegyelmiUgy.HatarozatJogerosFL = !model.IsNemVetteTudomasul;
                                fegyelmiUgy.Lezarva = !model.IsNemVetteTudomasul;

                                if (!model.IsNemVetteTudomasul)
                                {
                                    fegyelmiUgy.Hatarido = null;

                                    if (fegyelmiUgy.KarteritesId != null)
                                    {
                                        bool szandekosFl = model.IsSzandekosKarokozas ?? false;
                                        // BvBankban szándékos károkozás esetén be kell billenteni egyre a fegyelmi_eljaras_szandekos_volt flag-et
                                        //IKarteritesekFunctions karteritesekFunctions = new KarteritesekFunctions();
                                        //karteritesekFunctions.SetKarteritesSzandekossag(fegyelmiUgy.KarteritesId.Value, szandekosFl);
                                        //if (szandekosFl)
                                        //    szandekossagSzoveg += "<p><i>Kártérítésre kötelezett.</i></p>";
                                        //else
                                        //    szandekossagSzoveg += "<p><i>Kártérítésre nem kötelezett.</i></p>";
                                    }
                                }

                            }

                            Modify(fegyelmiUgy);

                            var naploEnitity = new Naplo
                            {
                                FegyelmiUgyId = fegyelmiUgyId,
                                FogvatartottId = fegyelmiUgy.FogvatartottId,
                                AlairtaFl = isAlairta,
                                IsFogvatartottElfogadta = isFogvatartottElfogadta,
                                RogzitoIntezetId = aktIntezet,
                                TipusCimkeId = naploTipusaCimkeId,
                                JegyzokonyvTartalma = $"{model.Indoklas}{szandekossagSzoveg}",
                                FenyitesKiszabasDatuma = model.FenyitesKiszabasIdeje,
                                FenyitesTipusCimkeId = fegyelmiUgyFenyitesTipusCimkeId,
                                FegyelmiVetsegTipusaCimkeId = model.FegyelmiVetsegTipusaCimkeId,
                                Vegleges = model.Vegleges,
                                DonteshozoSzemelyId = AlkalmazasKontextusFunctions.Kontextus.SzemelyzetId
                            };

                            KonasoftBVFonixContext.Naplo.Add(naploEnitity);
                            KonasoftBVFonixContext.SaveChanges();
                            naplobejegyzesIds.Add(naploEnitity.Id);
                        }
                    }
                    else
                    {
                        foreach (var naplobejegyzesId in model.NaplobejegyzesIds)
                        {
                            var naplo = KonasoftBVFonixContext.Naplo.Single(x => x.Id == naplobejegyzesId);
                            var fegyelmiUgy = (FegyelmiUgyViewModel)Table.Single(x => x.Id == naplo.FegyelmiUgyId);
                            if (model.Vegleges == true)
                            {
                                fegyelmiUgy.FegyelmiVetsegTipusaCimkeId = model.FegyelmiVetsegTipusaCimkeId; //eredeti 43
                                fegyelmiUgy.StatuszCimkeId = fenyitesTipusaCimkeId; // eredeti 89
                                fegyelmiUgy.FenyitesTipusCimkeId = fegyelmiUgyFenyitesTipusCimkeId;
                                fegyelmiUgy.HatarozatotHozottSzemelyId = AlkalmazasKontextusFunctions.Kontextus.SzemelyzetId;
                                fegyelmiUgy.Visszakuldve = model.IsNemVetteTudomasul;
                                fegyelmiUgy.Lezarva = !model.IsNemVetteTudomasul;
                                fegyelmiUgy.HatarozatJogerosFL = !model.IsNemVetteTudomasul;

                                if (!model.IsNemVetteTudomasul)
                                {
                                    fegyelmiUgy.Hatarido = null;

                                    if (fegyelmiUgy.KarteritesId != null)
                                    {
                                        bool szandekosFl = model.IsSzandekosKarokozas ?? false;
                                        // BvBankban szándékos károkozás esetén be kell billenteni egyre a fegyelmi_eljaras_szandekos_volt flag-et
                                        //IKarteritesekFunctions karteritesekFunctions = new KarteritesekFunctions();
                                        //karteritesekFunctions.SetKarteritesSzandekossag(fegyelmiUgy.KarteritesId.Value, szandekosFl);
                                        //if (szandekosFl)
                                        //    szandekossagSzoveg += "<p><i>Kártérítésre kötelezett.</i></p>";
                                        //else
                                        //    szandekossagSzoveg += "<p><i>Kártérítésre nem kötelezett.</i></p>";
                                    }
                                }
                            }

                            Modify(fegyelmiUgy);

                            naplo.AlairtaFl = isAlairta;
                            naplo.IsFogvatartottElfogadta = isFogvatartottElfogadta;
                            naplo.RogzitoIntezetId = aktIntezet;
                            naplo.JegyzokonyvTartalma = $"{model.Indoklas}{szandekossagSzoveg}";
                            naplo.FenyitesKiszabasDatuma = model.FenyitesKiszabasIdeje;
                            naplo.FegyelmiVetsegTipusaCimkeId = model.FegyelmiVetsegTipusaCimkeId;
                            naplo.FenyitesTipusCimkeId = fegyelmiUgyFenyitesTipusCimkeId;
                            naplo.Vegleges = model.Vegleges;
                            naplo.DonteshozoSzemelyId = AlkalmazasKontextusFunctions.Kontextus.SzemelyzetId;
                            KonasoftBVFonixContext.SaveChanges();
                            naplobejegyzesIds.Add(naplo.Id);
                        }
                    }

                    if (model.Vegleges == true && !model.IsNemVetteTudomasul)
                    {
                        toroltFegyelmi.AddRange(model.FegyelmiUgyIds); // különben nem végleges, nem kell törölni
                    }
                    else
                    {
                        megvaltozottFegyelmi.AddRange(model.FegyelmiUgyIds);
                    }


                    transaction.Commit();
                }
                catch (Exception e)
                {
                    Log.Error($"Hiba a reintegrációs tiszti döntés feddés mentés során (fegyelmiUgyId: {currentId})", e);
                    transaction.Rollback();

                    throw;
                }
            }
            OnFegyelmiUgyValtozas?.Invoke(ujFegyelmi, megvaltozottFegyelmi, toroltFegyelmi);
            return naplobejegyzesIds;
        }
        public ReintegraciosTisztDontesModelVisszakuldes GetReintegraciosTisztDontesVisszakuldesModel(List<int> fegyelmiUgyIds)
        {
            int fegyelmiUgyId = fegyelmiUgyIds.First();
            var felhoId = (int)Felhok.VisszakuldesOka;
            var model = new ReintegraciosTisztDontesModelVisszakuldes();
            model.VisszakuldesOkaOptions = KonasoftBVFonixContext.Cimkek.Where(w => w.FelhoId == felhoId).Select(x => new KSelect2ItemModel() { id = x.Id.ToString(), text = x.Nev }).ToList();
            model.Indoklas = "";
            return model;
        }
        public void SaveReintegraciosTisztDontesVisszakuldesModel(ReintegraciosTisztDontesModelVisszakuldes model)
        {

            var aktIntezet = JogosultsagCacheFunctions.AktualisIntezet.Id;

            List<int> ujFegyelmi = new List<int>(0);
            List<int> megvaltozottFegyelmi = new List<int>();
            List<int> toroltFegyelmi = new List<int>(0);
            bool isNewTransaction = KonasoftBVFonixContext.Database.CurrentTransaction == null; // ha még nincs tranzakció folyamtban csak akkor csinálunk újat
            using (DbContextTransaction transaction = isNewTransaction ? KonasoftBVFonixContext.Database.BeginTransaction() : null)
            {
                int currentId = 0;
                try
                {
                    foreach (var fegyelmiUgyId in model.FegyelmiUgyIds)
                    {
                        var fegyelmiUgy = (FegyelmiUgyViewModel)Table.Single(x => x.Id == fegyelmiUgyId);
                        fegyelmiUgy.StatuszCimkeId = (int)FegyelmiUgyStatusz.Kezdemenyezve;
                        fegyelmiUgy.Hatarido = fegyelmiUgy.LetrehozasDatuma.AddDays(3);
                        fegyelmiUgy.Visszakuldve = true;
                        fegyelmiUgy.KivizsgaloSzemelyId = null;
                        Modify(fegyelmiUgy);

                        var naploEnitity = new Naplo
                        {
                            FegyelmiUgyId = fegyelmiUgyId,
                            FogvatartottId = fegyelmiUgy.FogvatartottId,
                            AlairtaFl = false,
                            IsFogvatartottElfogadta = true,
                            JegyzokonyvTartalma = model.Indoklas,
                            VisszakuldesOkaCimkeId = model.VisszakuldesOka.Value,
                            RogzitoIntezetId = aktIntezet,
                            TipusCimkeId = (int)FegyelmiNaploTipus.Visszakuldes,
                            DonteshozoSzemelyId = AlkalmazasKontextusFunctions.Kontextus.SzemelyzetId
                        };

                        KonasoftBVFonixContext.Naplo.Add(naploEnitity);

                    }
                    KonasoftBVFonixContext.SaveChanges();

                    megvaltozottFegyelmi.AddRange(model.FegyelmiUgyIds);

                    if (isNewTransaction)
                        transaction.Commit();
                }
                catch (Exception e)
                {
                    Log.Error($"Hiba a reintegrációs tiszti döntés visszaküldés mentés során (fegyelmiUgyId: {currentId})", e);
                    if (isNewTransaction)
                        transaction.Rollback();

                    throw;
                }
            }
            OnFegyelmiUgyValtozas?.Invoke(ujFegyelmi, megvaltozottFegyelmi, toroltFegyelmi);
        }

        public void SaveKarteritesIdFegyelmiUgybe(int fegyelmiUgyId, int karteritesId)
        {
            var fegyelmiUgy = Table.Single(x => x.Id == fegyelmiUgyId);
            fegyelmiUgy.KarteritesId = karteritesId;
            KonasoftBVFonixContext.SaveChanges();
        }
        public int FegyelmiUgyInditasa(int esemenyId, int fogvatartottId, Intezet intezet = null, bool? bvBankbol = null)
        {

            if (intezet == null) 
                intezet = KonasoftBVFonixContext.Intezet.Single(x => x.Id == AlkalmazasKontextusFunctions.Kontextus.RogzitoIntezetId);

            var esemeny = new EsemenyekFunctions().GetEsemenyById(esemenyId);

            var fegyelmiugyekSzama = KonasoftBVFonixContext.FegyelmiUgyek.Where(x => x.EsemenyId == esemenyId && x.FogvatartottId == fogvatartottId && x.Lezarva == false).Count();
            if (fegyelmiugyekSzama > 0)
            {
                throw new WarningException("Már van fegyelmi ügy indítva a fogvatartotthoz.");
            }

            var intezetiUtolsoFegyelmiUgy = KonasoftBVFonixContext.FegyelmiUgyek.Where(x => x.UgySorszamaEv == DateTime.Now.Year &&
             x.IntezetId == intezet.Id).OrderByDescending(x => x.UgySorszama).FirstOrDefault();

            var sorszam = 1;
            if (intezetiUtolsoFegyelmiUgy != null)
            {
                sorszam = intezetiUtolsoFegyelmiUgy.UgySorszama + 1;
            }

            //string ugyszam = "";

            bool isNewTransaction = KonasoftBVFonixContext.Database.CurrentTransaction == null; // ha még nincs tranzakció folyamtban csak akkor csinálunk újat
            using (DbContextTransaction transaction = isNewTransaction ? KonasoftBVFonixContext.Database.BeginTransaction() : null)
            {
                try
                {
                    var entity = new FegyelmiUgy()
                    {
                        FogvatartottId = fogvatartottId,
                        StatuszCimkeId = (int)CimkeEnums.FegyelmiUgyStatusz.Kezdemenyezve,
                        //15798-as backlog
                        Hatarido = DateTime.Today.AddDays(3),
                        EsemenyId = esemenyId,
                        UgySorszamaIntezetAzon = intezet.Azonosito,
                        IntezetId = intezet.Id,
                        UgySorszamaEv = DateTime.Now.Year,
                        UgySorszama = sorszam,
                    };

                    if (AlkalmazasKontextusFunctions.Kontextus.SzemelyzetSid == AdUserSid) // _BVOP-FONIXAPP hívás külsõ alkalmazásból
                    {
                        entity.KeziRogzitoAdatok = true;
                        entity.RogzitoIntezetId = esemeny.RogzitoIntezetId;
                        entity.RogzitoSzemelyId = esemeny.EszleloId;
                    }

                    KonasoftBVFonixContext.FegyelmiUgyek.Add(entity);
                    KonasoftBVFonixContext.SaveChanges();

                    var naplo = new Naplo()
                    {
                        FogvatartottId = fogvatartottId,
                        FegyelmiUgyId = entity.Id,
                        TipusCimkeId = (int)CimkeEnums.FegyelmiNaploTipus.UgyKezdemenyezese
                    };

                    if (AlkalmazasKontextusFunctions.Kontextus.SzemelyzetSid == AdUserSid) // _BVOP-FONIXAPP hívás külsõ alkalmazásból
                    {
                        naplo.KeziRogzitoAdatok = true;
                        naplo.RogzitoIntezetId = esemeny.RogzitoIntezetId;
                        naplo.RogzitoSzemelyId = esemeny.EszleloId;
                    }

                    KonasoftBVFonixContext.Naplo.Add(naplo);
                    KonasoftBVFonixContext.SaveChanges();

                    //ugyszam = entity.UgySorszamaIntezetAzon + "/" + entity.UgySorszamaEv + "/" + entity.UgySorszama;

                    if (isNewTransaction)
                        transaction.Commit();

                    //#region Kártérítés
                    ////98 Állami tulajdon rongálása, lopása
                    ////99 Állami tulajdon rongálása, lopása (bv - s mobiltelefon)
                    //if (!(bvBankbol ?? false))
                    //{
                    //    if (esemeny.JellegCimkeId == 99 || esemeny.JellegCimkeId == 98)
                    //    {
                    //        var rogzitoIntezetId = intezet.Id;
                    //        var KarteritesekFunctions = new KarteritesekFunctions();
                    //        var utalasiHely = UtalasiHelyekFunctions.ModelQuery
                    //            .Where(x => x.IntezetId == rogzitoIntezetId && x.TipusKszId == (int)KodszotarEnums.UtalasiHelyTipus.BvJavadalom)
                    //            .ToList();
                    //        if (utalasiHely.Count != 1)
                    //        {
                    //            //TODO rendes hibaüzenet a felhsználónak
                    //            throw new WarningException("Utalasi hely hiba! Kérem keresse fel a fejlesztőket!");
                    //        }
                    //        int tipusKszId = (int)KodszotarEnums.KarteritesTipus.NormalKarteritesiEljaras;
                    //        if (esemeny.JellegCimkeId == 99)
                    //            tipusKszId = (int)KodszotarEnums.KarteritesTipus.MobilKarteritesiEljaras;
                    //        KarteritesekViewModel karteritesiEljaras = new KarteritesekViewModel
                    //        {
                    //            KarteritesiAzonosito = KarteritesekFunctions.GetUjKarteritesAzonosito(intezet.Id, out sorszam),
                    //            FogvatartottId = fogvatartottId,
                    //            IntezetId = intezet.Id,
                    //            RogzitesDatuma = DateTime.Now.Date,
                    //            StatuszKszId = (int)KodszotarEnums.KarteritesStatusz.KarbejelentesAlatt,
                    //            KarteritesTipusKszId = tipusKszId,
                    //            UtalasiHelyId = utalasiHely[0].Id,
                    //            KarteritesSorszama = sorszam,
                    //            KaresetDatuma = esemeny.EsemenyDatuma,
                    //            FegyelmiUgyId = entity.Id,
                    //            KarteritesFegyelmibol = true,
                    //        };
                    //        var kartId = KarteritesekFunctions.SaveUjKarteritesiEljaras(karteritesiEljaras);
                    //        entity.KarteritesId = kartId;
                    //        KonasoftBVFonixContext.SaveChanges();
                    //    }
                    //}
                    //#endregion

                    return entity.Id;
                }
                catch (Exception e)
                {
                    if (isNewTransaction)
                        transaction.Rollback();
                    throw;
                }
            }
        }
        public List<int> FegyelmiUgyekInditasa(int esemenyId, List<int> fogvatartottIds, bool? bvbankbol = null, int? intezetId=null)
        {
            List<int> fegyelmiUgyek = new List<int>();
            if(intezetId==null)
            {
                intezetId = AlkalmazasKontextusFunctions.Kontextus.RogzitoIntezetId;
            }
            var intezet = KonasoftBVFonixContext.Intezet.Single(x => x.Id == intezetId);
            foreach (int fogvatartottId in fogvatartottIds)
            {
                int fegyelmiUgyId = FegyelmiUgyInditasa(esemenyId, fogvatartottId, intezet, bvbankbol);
                fegyelmiUgyek.Add(fegyelmiUgyId);
            };

            List<int> ujFegyelmi = new List<int>();
            List<int> megvaltozottFegyelmi = new List<int>(0);
            List<int> toroltFegyelmi = new List<int>(0);
            ujFegyelmi.AddRange(fegyelmiUgyek);

            //     OnFegyelmiUgyValtozas?.Invoke(ujFegyelmi, megvaltozottFegyelmi, toroltFegyelmi);

            return fegyelmiUgyek;
        }

        public EljarasLefolytatasanakMegtagadasModel GetEljarasLefolytatasanakMegtagadasa()
        {
            var fegyelmiUsers = SzemelyzetFunctions.GetFegyelmiAlkalmazottak();
            var isAtualisUserValaszthato = fegyelmiUsers.Any(a => a.Sid == WindowsIdentity.GetCurrent().User.Value);
            var currentEszlelo = AlkalmazasKontextusFunctions.Kontextus.SzemelyzetId;
            var szemelyzet = SzemelyzetFunctions.FindById(currentEszlelo);
            var eszlelo = SzemelyzetFunctions.GetAdFegyelmiUser(szemelyzet);
            List<AdFegyelmiUserItem> egyszemelyesNemModosithatoLista = new List<AdFegyelmiUserItem>();
            egyszemelyesNemModosithatoLista.Add(eszlelo);
            List<KSelect2ItemModel> szemelyzetSelectList2 = egyszemelyesNemModosithatoLista.Select(x => new KSelect2ItemModel() { id = x.Sid, text = x.Displayname + (x.Rendfokozat == null ? "" : (" " + x.Rendfokozat)) }).ToList();
            var fegyelmiUsersS2 = fegyelmiUsers.Select(x => new KSelect2ItemModel() { id = x.Sid, text = x.Displayname + (x.Rendfokozat == null ? "" : (" " + x.Rendfokozat)) }).ToList();
            var model = new EljarasLefolytatasanakMegtagadasModel()
            {
                DonteshozoSzemelySid = eszlelo.Sid,
                DonteshozoSzemelyek = szemelyzetSelectList2,
                Leiras = "",
            };
            return model;
        }
        public void EljarasLefolytatasanakMegtagadasa(EljarasLefolytatasanakMegtagadasModel model)
        {
            using (DbContextTransaction transaction = KonasoftBVFonixContext.Database.BeginTransaction())
            {
                int currentId = 0;
                try
                {
                    foreach (var fegyelmiUgyId in model.FegyelmiUgyIds)
                    {
                        currentId = fegyelmiUgyId;
                        var fegyelmiUgy = (FegyelmiUgyViewModel)Table.Single(x => x.Id == fegyelmiUgyId);

                        if (fegyelmiUgy.StatuszCimkeId != (int)FegyelmiUgyStatusz.Kezdemenyezve)
                        {
                            throw new WarningException("Csak kezdeményezve státuszban lévõ ügy lefolytatását lehet megtagadni");
                        }
                        fegyelmiUgy.StatuszCimkeId = (int)FegyelmiUgyStatusz.Megtagadva;
                        fegyelmiUgy.Hatarido = null;
                        fegyelmiUgy.Lezarva = true;
                        fegyelmiUgy.Visszakuldve = false;
                        fegyelmiUgy.ElrendeloSzemelyId = AlkalmazasKontextusFunctions.Kontextus.SzemelyzetId;
                        Modify(fegyelmiUgy);

                        var naploEnitity = new Naplo
                        {
                            FegyelmiUgyId = fegyelmiUgyId,
                            JegyzokonyvTartalma = model.Leiras,
                            DonteshozoSzemelyId = AlkalmazasKontextusFunctions.Kontextus.SzemelyzetId,
                            FogvatartottId = fegyelmiUgy.FogvatartottId,
                            TipusCimkeId = (int)FegyelmiNaploTipus.ÜgyMegtagadasa
                        };

                        KonasoftBVFonixContext.Naplo.Add(naploEnitity);
                    }
                    KonasoftBVFonixContext.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    Log.Error($"Hiba az eljárás lefolyásának megtagadása során (fegyelmiUgyId: {currentId})", e);
                    transaction.Rollback();

                    throw e;
                }
            }
            List<int> ujFegyelmi = new List<int>();
            List<int> megvaltozottFegyelmi = new List<int>();
            List<int> toroltFegyelmi = new List<int>();
            toroltFegyelmi.AddRange(model.FegyelmiUgyIds);

            OnFegyelmiUgyValtozas?.Invoke(ujFegyelmi, megvaltozottFegyelmi, toroltFegyelmi);

        }

        public void HataridoModositasiJavaslat(LeirasMegadasFormModel model)
        {
            using (DbContextTransaction transaction = KonasoftBVFonixContext.Database.BeginTransaction())
            {
                int currentId = 0;
                try
                {
                    foreach (var fegyelmiUgyId in model.FegyelmiUgyIds)
                    {
                        currentId = fegyelmiUgyId;
                        var fegyelmiUgy = (FegyelmiUgyViewModel)Table.Single(x => x.Id == fegyelmiUgyId);

                        if (fegyelmiUgy.StatuszCimkeId != (int)FegyelmiUgyStatusz.KivizsgalasFolyamatban && fegyelmiUgy.StatuszCimkeId != (int)FegyelmiUgyStatusz.KozvetitoiEljarasAlatt)
                        {
                            throw new WarningException("Ebben a státuszban a határidõ módosítás nem lehetséges");
                        }
                        fegyelmiUgy.HataridoModositasJavaslat = true;
                        Modify(fegyelmiUgy);

                        var naploEnitity = new Naplo
                        {
                            FegyelmiUgyId = fegyelmiUgyId,
                            JegyzokonyvTartalma = model.Leiras,
                            FogvatartottId = fegyelmiUgy.FogvatartottId,
                            TipusCimkeId = (int)FegyelmiNaploTipus.HataridoModositasiJavaslat
                        };

                        KonasoftBVFonixContext.Naplo.Add(naploEnitity);
                    }
                    KonasoftBVFonixContext.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    Log.Error($"HataridoModositasiJavaslat() hiba (fegyelmiUgyId: {currentId})", e);
                    transaction.Rollback();

                    throw e;
                }
            }
            List<int> ujFegyelmi = new List<int>();
            List<int> megvaltozottFegyelmi = new List<int>();
            List<int> toroltFegyelmi = new List<int>();
            megvaltozottFegyelmi.AddRange(model.FegyelmiUgyIds);

            OnFegyelmiUgyValtozas?.Invoke(ujFegyelmi, megvaltozottFegyelmi, toroltFegyelmi);
        }

        //JUNG
        public void UjSzabadszovegesFegyelmiNaplobejegyzesFelvitele(FegyelmiNaplobejegyzesFelviteleModel model)
        {
            using (DbContextTransaction transaction = KonasoftBVFonixContext.Database.BeginTransaction())
            {
                int currentId = 0;
                try
                {
                    foreach (var fegyelmiUgyId in model.FegyelmiUgyIds)
                    {
                        currentId = fegyelmiUgyId;

                        var naploEnitity = new Naplo
                        {
                            FegyelmiUgyId = fegyelmiUgyId,
                            JegyzokonyvTartalma = model.Leiras,
                            EgyebAdatokJson = AlkalmazasKontextusFunctions.Kontextus.SzemelyzetSid,
                            TipusCimkeId = (int)FegyelmiNaploTipus.SzabadszovegesNaplobejegyzes
                        };

                        KonasoftBVFonixContext.Naplo.Add(naploEnitity);
                    }
                    KonasoftBVFonixContext.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    Log.Error($"{nameof(UjSzabadszovegesFegyelmiNaplobejegyzesFelvitele)}() hiba (fegyelmiUgyId: {currentId})", e);
                    transaction.Rollback();

                    throw e;
                }
            }
            List<int> ujFegyelmi = new List<int>();
            List<int> megvaltozottFegyelmi = new List<int>();
            List<int> toroltFegyelmi = new List<int>();
            megvaltozottFegyelmi.AddRange(model.FegyelmiUgyIds);

            OnFegyelmiUgyValtozas?.Invoke(ujFegyelmi, megvaltozottFegyelmi, toroltFegyelmi);
        }
        public FegyelmiNaplobejegyzesFelviteleModel GetSzabadszovegesFegyelmiNaplobejegyzesSzerkesztese(List<int> naploIds)
        {
            var naploid = naploIds.First();
            var naplo = KonasoftBVFonixContext.Naplo.Single(n => n.Id == naploid);
            if (naplo.LetrehozasDatuma.Date != DateTime.Today)
            {
                throw new WarningException("A szabadszöveges naplóbejegyzés csak a készítés napján szerkeszthető");
            }
            if (naplo.RogzitoSzemelyId != AlkalmazasKontextusFunctions.Kontextus.SzemelyzetId)
            {
                throw new WarningException("A szabadszöveges naplóbejegyzést csak a készítője szerkesztheti");
            }
            FegyelmiNaplobejegyzesFelviteleModel result = new FegyelmiNaplobejegyzesFelviteleModel()
            {
                NaplobejegyzesIds = naploIds,
                Leiras = naplo.JegyzokonyvTartalma.Replace("<br />", "\n"),
                FegyelmiUgyIds = new List<int> { naplo.FegyelmiUgyId }
            };

            return result;
        }
        public void SzabadszovegesFegyelmiNaplobejegyzesSzerkesztese(int naploId, int fegyelmiUgyId, string jegyzokonyvTartalma)
        {
            using (DbContextTransaction transaction = KonasoftBVFonixContext.Database.BeginTransaction())
            {
                try
                {
                    var naplo = KonasoftBVFonixContext.Naplo.Single(n => n.Id == naploId);
                    naplo.JegyzokonyvTartalma = jegyzokonyvTartalma;
                    KonasoftBVFonixContext.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    Log.Error($"{nameof(SzabadszovegesFegyelmiNaplobejegyzesSzerkesztese)}() hiba (fegyelmiUgyId: {fegyelmiUgyId})", e);
                    transaction.Rollback();

                    throw e;
                }
            }
            List<int> ujFegyelmi = new List<int>();
            List<int> megvaltozottFegyelmi = new List<int>();
            List<int> toroltFegyelmi = new List<int>();
            megvaltozottFegyelmi.Add(fegyelmiUgyId);

            OnFegyelmiUgyValtozas?.Invoke(ujFegyelmi, megvaltozottFegyelmi, toroltFegyelmi);
        }

        //JUNG END

        public FelfuggesztesModel GetFelfuggesztesiJavaslat(List<int> fegyelmiUgyIds)
        {
            var model = new FelfuggesztesModel();
            var cimkek = KonasoftBVFonixContext.Cimkek
                .Where(w => w.FelhoId == (int)Felhok.FelfuggesztesOka)
                .Select(x => new KSelect2ItemModel() { id = x.Id.ToString(), text = x.Nev })
                .ToList();
            model.FelfuggesztesOkaOptions = cimkek;

            return model;
        }
        public void FelfuggesztesiJavaslat(FelfuggesztesModel model)
        {
            using (DbContextTransaction transaction = KonasoftBVFonixContext.Database.BeginTransaction())
            {
                int currentId = 0;
                try
                {
                    foreach (var fegyelmiUgyId in model.FegyelmiUgyIds)
                    {
                        currentId = fegyelmiUgyId;
                        var fegyelmiUgy = (FegyelmiUgyViewModel)Table.Single(x => x.Id == fegyelmiUgyId);

                        if (fegyelmiUgy.Felfuggesztve == true)
                        {
                            throw new WarningException("A fegyelmi ügy már fel van függesztve");
                        }
                        fegyelmiUgy.FelfuggesztesiJavaslat = true;
                        fegyelmiUgy.FelfuggesztesOkaCimkeId = model.FelfuggesztesOkaCimkeId;
                        Modify(fegyelmiUgy);

                        var naploEnitity = new Naplo
                        {
                            FegyelmiUgyId = fegyelmiUgyId,
                            FelfuggesztesOkaCimkeId = model.FelfuggesztesOkaCimkeId,
                            FogvatartottId = fegyelmiUgy.FogvatartottId,
                            TipusCimkeId = (int)FegyelmiNaploTipus.FelfuggesztesiJavaslat
                        };

                        KonasoftBVFonixContext.Naplo.Add(naploEnitity);
                    }
                    KonasoftBVFonixContext.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    Log.Error($"FelfuggesztesiJavaslat() hiba (fegyelmiUgyId: {currentId})", e);
                    transaction.Rollback();

                    throw e;
                }
            }
            List<int> ujFegyelmi = new List<int>();
            List<int> megvaltozottFegyelmi = new List<int>();
            List<int> toroltFegyelmi = new List<int>();
            megvaltozottFegyelmi.AddRange(model.FegyelmiUgyIds);

            OnFegyelmiUgyValtozas?.Invoke(ujFegyelmi, megvaltozottFegyelmi, toroltFegyelmi);
        }

        public int SaveFegyelmiUgyUjResztvevo(int fegyelmiUgyId, int fogvatartottId)
        {
            var fegyelmiUgy = KonasoftBVFonixContext.FegyelmiUgyek.Single(x => x.Id == fegyelmiUgyId);

            if (KonasoftBVFonixContext.EsemenyResztvevok.Any(x => x.FogvatartottId == fogvatartottId && x.EsemenyId == fegyelmiUgy.EsemenyId))
            {
                throw new WarningException("A fogvatartott már fel van véve résztvevőként a rendkívüli eseményhez");
            }

            var ujresztvevo = new EsemenyResztvevo()
            {
                ErintettsegFokaCimkeId = (int)CimkeEnums.ErintettsegFoka.Tanu,
                EsemenyId = fegyelmiUgy.EsemenyId,
                FogvatartottId = fogvatartottId,
            };

            KonasoftBVFonixContext.EsemenyResztvevok.Add(ujresztvevo);
            KonasoftBVFonixContext.SaveChanges();

            return ujresztvevo.Id;
        }

        public string SaveFegyelmiUgyUjAllomanyiResztvevo(List<int> fegyelmiUgyIds, string allomanyiSzemelySid)
        {
            // var fegyelmiUgy = KonasoftBVFonixContext.FegyelmiUgyek.Single(x => x.Id == fegyelmiUgyId);

            //if (KonasoftBVFonixContext.EsemenyResztvevok.Any(x => x.FogvatartottId == fogvatartottId && x.EsemenyId == fegyelmiUgy.EsemenyId))
            //{
            //    throw new WarningException("A fogvatartott már fel van véve résztvevőként a rendkívüli eseményhez");
            //}
            var tanuSzemely = SzemelyzetFunctions.SzemelyzetLekeresVagyLetrehozas(allomanyiSzemelySid, null, null);

            var hianyzoEsemenyIds = (from esemeny in KonasoftBVFonixContext.Esemenyek
                                     join ugy in KonasoftBVFonixContext.FegyelmiUgyek on esemeny.Id equals ugy.EsemenyId
                                     join resztvevo in KonasoftBVFonixContext.EsemenyResztvevok on new { AllomanyiSzemelyId = (int?)tanuSzemely.Id, EsemenyId = esemeny.Id } equals new { resztvevo.AllomanyiSzemelyId, resztvevo.EsemenyId } into resztvevoL
                                     from resztvevoLeft in resztvevoL.DefaultIfEmpty()
                                     where fegyelmiUgyIds.Contains(ugy.Id) && resztvevoLeft == null
                                     select esemeny.Id).ToList();

            var hianyzoTanuk = new List<Szemelyzet>();
            using (DbContextTransaction transaction = KonasoftBVFonixContext.Database.BeginTransaction())
            {
                try
                {
                    foreach (var esemenyId in hianyzoEsemenyIds)
                    {
                        EsemenyResztvevoFunctions.SaveEsemenyResztvevok(new List<int>() { tanuSzemely.Id }, (int)ErintettsegFoka.SzemelyiAllomanyiTanu, esemenyId);
                    }
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    Log.Error($"SaveFegyelmiUgyUjAllomanyiResztvevo ", e);
                    transaction.Rollback();

                    throw e;
                }


                return tanuSzemely.AdSid;
            }
        }

        public OsszefoglaloJelentesModel GetOsszefoglaloJelentesModalData(List<int> fegyelmiUgyIds, List<int> naplobejegyzesIds)
        {
            var model = new OsszefoglaloJelentesModel();
            if (!naplobejegyzesIds.IsNullOrEmpty())
            {
                var naplobejegyzesId = naplobejegyzesIds.First();
                var naploBejegyzes = NaploFunctions.FindById(naplobejegyzesId);
                model.NaplobejegyzesIds = naplobejegyzesIds;
                model.Leiras = naploBejegyzes.JegyzokonyvTartalma;

                if (model.Leiras.Trim() == "") model.Leiras = "<p></p>";
            }
            else
            {
                var fegyelmiUgyId = fegyelmiUgyIds.First();
                Dictionary<string, object> pars = new Dictionary<string, object>();

                pars.Add("@fegyelmiugyId", fegyelmiUgyId);

                var tortenetiadat = KonasoftBVFonixContext.RunStoredProcedureByNev<OsszefoglaloJelentesNyomtatasModel>("Fegyelmi.GetOsszefoglaloJelentesJutalmazasTobbletSzolgaltatasByFegyelmiugyId", pars).ToList();
                string leiras = $"<p><center><div style=\"font-size: 18px;\">Összefoglaló jelentés a kivizsgálás eredményéről</div></center></p><p><br /></p>";

                var cimkek = CimkeFunctions.GetCimkekByFelhoIds(new List<int>() {
                    //(int)Felhok.FegyelmiVetsegTipusa,
                    //(int)Felhok.FenyitesTipusa,
                    //(int)Felhok.FegyelmiUgyStatusza,
                    //(int)Felhok.VetsegRendeletSzerint,
                    //(int)Felhok.HatarozatHozoJogkore,
                    //(int)Felhok.EsemenyJellege,
                    (int)Felhok.EsemenyNapszaka,
                    (int)Felhok.EsemenyHelyszine
                }).ToDictionary(x => x.Id, y => y);

                var fegyelmiUgy = GetFegyelmiUgyEsemennyel(fegyelmiUgyId);

                var resztvevok = EsemenyResztvevoFunctions.GetEsemenyResztvevok(fegyelmiUgy.EsemenyId);

                // Fegyelmi lap tartalma
                leiras += $"<p><b><u>Fegyelmi lap tartalma:</u></b></p>";
                leiras += $"<p>{fegyelmiUgy.Esemeny.Leiras}</p><p><br /></p>";

                // A fegyelmi ügy alapjául szolgáló cselekmény elkövetésének helye, ideje
                leiras += $"<p><b><u>A fegyelmi ügy alapjául szolgáló cselekmény elkövetésének helye, ideje:</u></b></p>";
                leiras += $"<p>{cimkek[fegyelmiUgy.Esemeny.HelyCimkeId].Nev}, {cimkek[fegyelmiUgy.Esemeny.NapszakCimkeId].Nev}</p><p><br /></p>";

                // A fegyelmi ügyben résztvevők (terheltek) neve
                var elkovetok = (from resztvevo in KonasoftBVFonixContext.EsemenyResztvevok
                                 join fogv in KonasoftBVFonixContext.Fogvatartottak on resztvevo.FogvatartottId equals fogv.Id
                                 join szemely in KonasoftBVFonixContext.FogvatartottSzemelyesAdatai on fogv.Id equals szemely.FogvatartottId
                                 where resztvevo.EsemenyId == fegyelmiUgy.EsemenyId && (resztvevo.ErintettsegFokaCimkeId == (int)ErintettsegFoka.Elkoveto)
                                 select szemely.SzuletesiCsaladiNev_NE_HASZNALD + " " + szemely.SzuletesiUtonev_NE_HASZNALD + " - " + fogv.NyilvantartasiAzonosito).ToList();

                if (elkovetok.Count() > 0)
                {
                    leiras += $"<p><b><u>A fegyelmi ügyben résztvevők (terheltek) neve:</u></b></p>";
                    leiras += $"<p>{string.Join(", ", elkovetok)} " + (elkovetok.Count > 1 ? "fogvatartottak, mint terheltek" : "fogvatartott, mint terhelt") + "</p><p><br /></p>";
                }

                // A fegyelmi ügyben részvevők (fogvatartott tanú) neve
                var fogvatartottTanuk = (from resztvevo in KonasoftBVFonixContext.EsemenyResztvevok
                                         join fogv in KonasoftBVFonixContext.Fogvatartottak on resztvevo.FogvatartottId equals fogv.Id
                                         join szemely in KonasoftBVFonixContext.FogvatartottSzemelyesAdatai on fogv.Id equals szemely.FogvatartottId
                                         where resztvevo.EsemenyId == fegyelmiUgy.EsemenyId && (resztvevo.ErintettsegFokaCimkeId == (int)ErintettsegFoka.Tanu)
                                         select szemely.SzuletesiCsaladiNev_NE_HASZNALD + " " + szemely.SzuletesiUtonev_NE_HASZNALD + " - " + fogv.NyilvantartasiAzonosito).ToList();

                if (fogvatartottTanuk.Count() > 0)
                {
                    leiras += $"<p><b><u>A fegyelmi ügyben részvevők (fogvatartott tanú) neve:</u></b></p>";
                    leiras += $"<p>{string.Join(", ", fogvatartottTanuk)} " + (fogvatartottTanuk.Count > 1 ? "fogvatartottak, mint tanúk" : "fogvatartott, mint tanú") + "</p><p><br /></p>";
                }

                // A fegyelmi ügyben résztvevők (személyi állomány tanú) neve
                var szemelyiAllomanyiTanuk = (from esemeny in KonasoftBVFonixContext.Esemenyek
                                              join resztvevo in KonasoftBVFonixContext.EsemenyResztvevok on esemeny.Id equals resztvevo.EsemenyId
                                              join szemely in KonasoftBVFonixContext.Szemelyzet on resztvevo.AllomanyiSzemelyId equals szemely.Id
                                              join rendfokozat in KonasoftBVFonixContext.Kodszotar on szemely.RendfokozatKszId equals rendfokozat.Id
                                              where resztvevo.EsemenyId == fegyelmiUgy.EsemenyId && resztvevo.AllomanyiSzemelyId != null
                                              select szemely.Nev + " " + rendfokozat.Nev).Distinct().ToList();

                if (szemelyiAllomanyiTanuk.Count() > 0)
                {
                    leiras += $"<p><b><u>A fegyelmi ügyben résztvevők (személyi állomány tanú) neve:</u></b></p>";
                    leiras += $"<p>{string.Join(", ", szemelyiAllomanyiTanuk)}</p><p><br /></p>";
                }

                // A fegyelmi cselekmény észlelte
                if (fegyelmiUgy.Esemeny.Eszlelo != null)
                {
                    leiras += $"<p><b><u>A fegyelmi cselekményt észlelte:</u></b></p>";
                    leiras += $"<p>{fegyelmiUgy.Esemeny.Eszlelo.Nev} {fegyelmiUgy.Esemeny.Eszlelo.Rendfokozat.Nev}</p><p><br /></p>";
                }

                // Védő szereplése az ügyben
                leiras += $"<p><b><u>Védő szereplése az ügyben:</u></b></p>";
                leiras += "<p>" + (fegyelmiUgy.VanJogiKepviselet ? "Van jogi képviselet" : "Nincs kirendelve, sem megbízva") + "</p><p><br /></p>";

                // Tárgyi bizonyíték
                if (!String.IsNullOrWhiteSpace(fegyelmiUgy.Esemeny.Bizonyitek))
                {
                    leiras += $"<p><b><u>Tárgyi bizonyíték:</u></b></p>";
                    leiras += $"<p>{fegyelmiUgy.Esemeny.Bizonyitek}</p><p><br /></p>";
                }

                // Fegyelmi helyzete, jutalmazási helyzete, technikai eszközök
                foreach (var item in tortenetiadat)
                {
                    leiras += $"<p><u><b>{item.Fejlec}</b></u></p><p>{item.JegyzokonyvTartalma}</p><p><br /></p>"; //<strong> kiszedve fejléc mellől
                }

                // Többletszolgáltatások
                //leiras += $"<p><b><u>Többletszolgáltatások:</u></b></p>";
                //leiras += $"<p><br /></p>";

                // Foglalkoztatási adatok
                //KonasoftBVFonixContext.DisableToroltFlFilter(x => x.Munkahelyek);
                //var foglalkoztatasok = KonasoftBVFonixContext.Munkaltatasok.Where(w => w.FogvatartottId == fegyelmiUgy.FogvatartottId && w.IsNemDolgozas == false && w.MunkahelyId != null)
                //        .Include(x => x.Munkahely)
                //        .ToList()
                //        .Select(x => x.Kezdete.ToShortDateString() + " " + x.Munkahely.Nev).ToList();
                //KonasoftBVFonixContext.EnableToroltFlFilter(x => x.Munkahelyek);

                //if (foglalkoztatasok.Count() > 0)
                //{
                //    leiras += $"<p><b><u>Foglalkoztatási adatok:</u></b></p>";
                //    leiras += $"<p>{string.Join("<br/>", foglalkoztatasok)}</p><p><br /></p>";
                //}

                // Jelentések
                var osszefoglaloJelentesek = GetOsszefoglalojelentesNyomtatasAdat(fegyelmiUgyId); //kivéve összefoglaló jelentés
                if (osszefoglaloJelentesek.Count() > 0)
                {
                    leiras += $"<p><div style=\"font-size: 12px;\"><b><u>Bizonyítási eszközök:</u></b></div></p>";

                    string osszefoglaloJelentesekString = "";

                    foreach (var osszefoglaloJelentes in osszefoglaloJelentesek)
                    {
                        osszefoglaloJelentesekString += $"<p><b><u>{osszefoglaloJelentes.Fejlec}</u></b></p><p>{osszefoglaloJelentes.JegyzokonyvTartalma}</p><p><br /></p>";
                    }

                    model.Leiras = leiras + osszefoglaloJelentesekString;
                }

                model.Leiras += $"<p><b><u>Kivizsgáló összefoglaló jelentése:</u></b></p><p><br /></p>";
            }
            return model;
        }

        public HelysziniSzemleModel GetFegyelmiUgyDataHelysziniSzemleModel(List<int> fegyelmiUgyIds, List<int> naploIds)
        {
            HelysziniSzemleModel model = new HelysziniSzemleModel();
            model.FegyelmiUgyIds = fegyelmiUgyIds;

            if (naploIds.IsNullOrEmpty())
                return model;

            var naploId = naploIds.First();
            var naplo = KonasoftBVFonixContext.Naplo.Single(x => x.Id == naploId);
            model.NaplobejegyzesIds = naploIds;
            model.Leiras = naplo.JegyzokonyvTartalma;
            model.Datuma = naplo.JegyzokonyvLezarasDatuma;

            return model;
        }

        public List<int> FegyelmiUgyHelysziniSzemleMentes(HelysziniSzemleModel model)
        {
            int currentId = 0;
            List<int> ujFegyelmi = new List<int>(0);
            List<int> megvaltozottFegyelmi = new List<int>();
            List<int> toroltFegyelmi = new List<int>(0);
            List<int> naplobejegyzesIds = new List<int>();
            using (DbContextTransaction transaction = KonasoftBVFonixContext.Database.BeginTransaction())
            {
                try
                {
                    if (model.NaplobejegyzesIds.IsNullOrEmpty())
                    {
                        foreach (int fegyelmiUgyId in model.FegyelmiUgyIds)
                        {
                            var fegyelmiUgy = KonasoftBVFonixContext.FegyelmiUgyek.Single(x => x.Id == fegyelmiUgyId);

                            if (fegyelmiUgy.StatuszCimkeId != (int)CimkeEnums.FegyelmiUgyStatusz.KivizsgalasFolyamatban)
                            {
                                throw new WarningException("A helyszíni szemle nem módosítható, mert a fegyelmi ügy nem kivizsgálás folyamatban státuszú");
                            }

                            var naplo = new Naplo()
                            {
                                FegyelmiUgyId = fegyelmiUgyId,
                                JegyzokonyvLezarasDatuma = model.Datuma,
                                JegyzokonyvTartalma = model.Leiras,
                                JegyzokonyvVezetoSzemelyId = AlkalmazasKontextusFunctions.Kontextus.SzemelyzetId,
                                TipusCimkeId = (int)CimkeEnums.FegyelmiNaploTipus.HelysziniSzemle
                            };

                            KonasoftBVFonixContext.Naplo.Add(naplo);
                            KonasoftBVFonixContext.SaveChanges();
                            naplobejegyzesIds.Add(naplo.Id);
                            megvaltozottFegyelmi.Add(fegyelmiUgyId);
                        }
                    }

                    foreach (int naploId in model.NaplobejegyzesIds)
                    {
                        var naplo = KonasoftBVFonixContext.Naplo.Single(x => x.Id == naploId);
                        currentId = naplo.FegyelmiUgyId;
                        var fegyelmiUgy = KonasoftBVFonixContext.FegyelmiUgyek.Single(x => x.Id == naplo.FegyelmiUgyId);

                        naplo.JegyzokonyvTartalma = model.Leiras;
                        naplo.JegyzokonyvLezarasDatuma = model.Datuma;
                        KonasoftBVFonixContext.SaveChanges();
                        naplobejegyzesIds.Add(naplo.Id);
                        megvaltozottFegyelmi.Add(fegyelmiUgy.Id);
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    Log.Error($"Hiba a helyszíni szemle mentés során (fegyelmiUgyId: {currentId})", ex);
                    transaction.Rollback();

                    throw;
                }
            }
            OnFegyelmiUgyValtozas?.Invoke(ujFegyelmi, megvaltozottFegyelmi, toroltFegyelmi);
            return naplobejegyzesIds;
        }

        public SzakteruletiVelemenyModel GetFegyelmiUgyDataSzakteruletiVelemenyModelModel(List<int> fegyelmiUgyIds, List<int> naploIds)
        {
            SzakteruletiVelemenyModel model = new SzakteruletiVelemenyModel();
            model.FegyelmiUgyIds = fegyelmiUgyIds;
            model.NaplobejegyzesIds = new List<int>();

            model.UploadedFiles = new List<string>();

            if (naploIds.IsNullOrEmpty())
                return model;

            if (naploIds.Count > 0)
            {
                var fajlok = KonasoftBVFonixContext.Feltoltesek.Where(x => x.NaploId == naploIds.FirstOrDefault()).ToList().Select(x => (FeltoltesekViewModel)x).ToList();
                model.PrevUploadedFiles = fajlok;
            }

            var naploId = naploIds.First();
            var naplo = KonasoftBVFonixContext.Naplo.Single(x => x.Id == naploId);

            model.Leiras = naplo.JegyzokonyvTartalma;
            model.Datuma = naplo.JegyzokonyvLezarasDatuma;
            model.NaplobejegyzesIds = naploIds;

            return model;
        }

        public List<int> FegyelmiUgySzakteruletiVelemenyModelMentes(SzakteruletiVelemenyModel model)
        {
            int currentId = 0;
            List<string> fegyelmiUgyIds = new List<string>();
            List<int> ujFegyelmi = new List<int>(0);
            List<int> megvaltozottFegyelmi = new List<int>();
            List<int> toroltFegyelmi = new List<int>(0);
            List<int> naplobejegyzesIds = new List<int>();
            using (DbContextTransaction transaction = KonasoftBVFonixContext.Database.BeginTransaction())
            {
                try
                {
                    if (model.NaplobejegyzesIds.IsNullOrEmpty())
                    {
                        foreach (var fegyelmiUgyId in model.FegyelmiUgyIds)
                        {
                            currentId = fegyelmiUgyId;
                            fegyelmiUgyIds.Add(currentId.ToString());
                            var fegyelmiUgy = KonasoftBVFonixContext.FegyelmiUgyek.Single(x => x.Id == fegyelmiUgyId);
                            fegyelmiUgy.SzakteruletiVelemenyreVarFL = false;
                            KonasoftBVFonixContext.SaveChanges();

                            var naplo = new Naplo()
                            {
                                FegyelmiUgyId = fegyelmiUgyId,
                                JegyzokonyvLezarasDatuma = model.Datuma,
                                JegyzokonyvTartalma = model.Leiras,
                                JegyzokonyvVezetoSzemelyId = AlkalmazasKontextusFunctions.Kontextus.SzemelyzetId,
                                TipusCimkeId = (int)CimkeEnums.FegyelmiNaploTipus.SzakteruletiVelemeny
                            };

                            KonasoftBVFonixContext.Naplo.Add(naplo);
                            KonasoftBVFonixContext.SaveChanges();
                            naplobejegyzesIds.Add(naplo.Id);

                            if (model.UploadedFiles != null)
                            {
                                foreach (var url in model.UploadedFiles)
                                {
                                    KonasoftBVFonixContext.Feltoltesek.Add(new Feltoltesek
                                    {
                                        NaploId = naplo.Id,
                                        Url = url,
                                    });
                                }
                            }
                            KonasoftBVFonixContext.SaveChanges();
                            megvaltozottFegyelmi.Add(fegyelmiUgyId);
                        }
                    }
                    else
                    {
                        foreach (var naploId in model.NaplobejegyzesIds)
                        {
                            var naplo = KonasoftBVFonixContext.Naplo.Single(x => x.Id == naploId);
                            currentId = naplo.FegyelmiUgyId;
                            fegyelmiUgyIds.Add(currentId.ToString());

                            var fegyelmiUgy = KonasoftBVFonixContext.FegyelmiUgyek.Single(x => x.Id == currentId);
                            fegyelmiUgy.SzakteruletiVelemenyreVarFL = false;
                            KonasoftBVFonixContext.SaveChanges();

                            naplo.JegyzokonyvTartalma = model.Leiras;
                            naplo.JegyzokonyvLezarasDatuma = model.Datuma;
                            KonasoftBVFonixContext.SaveChanges();
                            naplobejegyzesIds.Add(naplo.Id);
                            megvaltozottFegyelmi.Add(naplo.FegyelmiUgyId);

                        }

                        if (model.UploadedFiles != null)
                        {
                            foreach (var url in model.UploadedFiles)
                            {
                                KonasoftBVFonixContext.Feltoltesek.Add(new Feltoltesek
                                {
                                    NaploId = model.NaplobejegyzesIds.FirstOrDefault(),
                                    Url = url,
                                });
                            }
                        }
                        KonasoftBVFonixContext.SaveChanges();
                    }

                    var datum = DateTime.Now;
                    List<SzakteruletiVelemenyKeresek> velemenyKeresek = new List<SzakteruletiVelemenyKeresek>();

                    foreach (var fegyelmiUgyId in fegyelmiUgyIds)
                    {
                        velemenyKeresek.AddRange(KonasoftBVFonixContext.SzakteruletiVelemenyKeresek.AsNoTracking()
                            .Where(w => w.FegyelmiUgyIdLista.Contains(fegyelmiUgyId)));
                    }

                    foreach (var velemenyKeres in velemenyKeresek)
                    {
                        velemenyKeres.Velemenyezve = datum;
                        var velemeny = SzakteruletiVelemenyKeresekFunctions.FindById(velemenyKeres.Id);
                        velemeny.Velemenyezve = datum;
                        SzakteruletiVelemenyKeresekFunctions.Modify(velemeny);
                        KonasoftBVFonixContext.SaveChanges();
                    }



                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    Log.Error($"Hiba a szakterületi vélemény mentés során (fegyelmiUgyId: {currentId})", ex);
                    transaction.Rollback();

                    throw;
                }
            }

            OnFegyelmiUgyValtozas?.Invoke(ujFegyelmi, megvaltozottFegyelmi, toroltFegyelmi);
            return naplobejegyzesIds;
        }

        public List<int> SaveOsszefoglaloJelentes(OsszefoglaloJelentesModel model)
        {

            List<int> naploIds = new List<int>();

            if (model.Leiras.StartsWith("<p><p>"))
            {
                model.Leiras = model.Leiras.Remove(model.Leiras.IndexOf("<p>"), "<p>".Length);
                model.Leiras = model.Leiras.Remove(model.Leiras.LastIndexOf("</p>"));
            }


            List<int> ujFegyelmi = new List<int>();
            List<int> megvaltozottFegyelmi = new List<int>();
            List<int> toroltFegyelmi = new List<int>();
            var aktIntezet = JogosultsagCacheFunctions.AktualisIntezet.Id;
            using (DbContextTransaction transaction = KonasoftBVFonixContext.Database.BeginTransaction())
            {
                var currentId = 0;
                try
                {
                    if (model.NaplobejegyzesIds.IsNullOrEmpty())
                    {
                        foreach (var fegyelmiUgyId in model.FegyelmiUgyIds)
                        {
                            currentId = fegyelmiUgyId;
                            var fegyelmiUgy = KonasoftBVFonixContext.FegyelmiUgyek.First(x => x.Id == fegyelmiUgyId);
                            if (fegyelmiUgy.StatuszCimkeId != (int)FegyelmiUgyStatusz.KivizsgalasFolyamatban)
                            {
                                throw new WarningException("Összefoglaló jelentést csak kivizsgálás folyamatban státusz alatt lehet készíteni.");
                            }
                            if (model.IsAlairta == true)
                            {
                                fegyelmiUgy.StatuszCimkeId = (int)FegyelmiUgyStatusz.I_FokuTargyalas;
                                //var hataridomodositas = false;
                                //if ((fegyelmiUgy.Hatarido - fegyelmiUgy.DontesDatuma).Value.TotalDays <= 20)
                                //{
                                //    hataridomodositas = false;
                                //}
                                //else
                                //{
                                //    hataridomodositas = true;
                                //}

                                //var hatarido = GetFegyelmiHatarido(fegyelmiUgyId, hataridomodositas, (int)FegyelmiUgyStatusz.I_FokuTargyalas) ?? DateTime.Now;
                                //fegyelmiUgy.Hatarido = hatarido;
                            }
                            KonasoftBVFonixContext.SaveChanges();
                            var naploEnitity = new Naplo
                            {
                                FegyelmiUgyId = fegyelmiUgyId,
                                FogvatartottId = fegyelmiUgy.FogvatartottId,
                                AlairtaFl = model.IsAlairta,
                                JegyzokonyvTartalma = model.Leiras,
                                RogzitoIntezetId = aktIntezet,
                                TipusCimkeId = (int)FegyelmiNaploTipus.OsszefoglaloJelentes
                            };
                            KonasoftBVFonixContext.Naplo.Add(naploEnitity);
                            KonasoftBVFonixContext.SaveChanges();
                            naploIds.Add(naploEnitity.Id);
                            megvaltozottFegyelmi.Add(fegyelmiUgyId);

                        }
                    }
                    else
                    {
                        foreach (var naplobejegyzesId in model.NaplobejegyzesIds)
                        {

                            var naplobejegyzes = KonasoftBVFonixContext.Naplo.Single(x => x.Id == naplobejegyzesId);
                            currentId = naplobejegyzes.FegyelmiUgyId;
                            var fegyelmiUgy = KonasoftBVFonixContext.FegyelmiUgyek.First(x => x.Id == naplobejegyzes.FegyelmiUgyId);
                            if (model.IsAlairta == true)
                            {
                                fegyelmiUgy.StatuszCimkeId = (int)FegyelmiUgyStatusz.I_FokuTargyalas;
                            }
                            KonasoftBVFonixContext.SaveChanges();

                            naplobejegyzes.JegyzokonyvTartalma = model.Leiras;
                            naplobejegyzes.AlairtaFl = model.IsAlairta;
                            KonasoftBVFonixContext.SaveChanges();
                            naploIds.Add(naplobejegyzesId);
                            megvaltozottFegyelmi.Add(naplobejegyzes.FegyelmiUgyId);
                        }
                    }

                    KonasoftBVFonixContext.SaveChanges();

                    transaction.Commit();

                }
                catch (Exception e)
                {
                    Log.Error($"Hiba történt az összefoglaló jelentés készítése során (fegyelmiUgyId: { currentId })", e);
                    transaction.Rollback();

                    throw new WarningException("Az összefoglaló jelentést nem sikerült menteni.");
                }
            }


            OnFegyelmiUgyValtozas?.Invoke(ujFegyelmi, megvaltozottFegyelmi, toroltFegyelmi);
            return naploIds;
        }

        public FegyelmiUgyOsszevonasModel GetFegyelmiUgyDataOsszevonashoz(int fegyelmiUgyId)
        {
            FegyelmiUgyOsszevonasModel model = new FegyelmiUgyOsszevonasModel();
            model.FegyelmiUgyId = fegyelmiUgyId;

            var fegyelmiUgy = KonasoftBVFonixContext.FegyelmiUgyek.Single(x => x.Id == fegyelmiUgyId);
            model.Ugyszam = fegyelmiUgy.UgySorszamaIntezetAzon + "/" + fegyelmiUgy.UgySorszamaEv + "/" + fegyelmiUgy.UgySorszama;

            var fogvSzemAdat = KonasoftBVFonixContext.FogvatartottSzemelyesAdatai.Include(x => x.Fogvatartott).Single(x => x.FogvatartottId == fegyelmiUgy.FogvatartottId);

            model.FogvatartottNev = fogvSzemAdat.SzuletesiCsaladiNev + " " + fogvSzemAdat.SzuletesiUtonev;
            model.FogvatartottNytsz = fogvSzemAdat.Fogvatartott.NyilvantartasiAzonosito;

            model.ValaszthatoFegyelmiUgyek = new List<FegyelmiUgyOsszevonasListItem>();

            if (KonasoftBVFonixContext.FegyelmiUgyek.Any(fegy => fegy.FogvatartottId == fegyelmiUgy.FogvatartottId
                               && fegy.StatuszCimkeId == (int)CimkeEnums.FegyelmiUgyStatusz.I_FokuTargyalas
                               && fegy.IntezetId == AlkalmazasKontextusFunctions.Kontextus.RogzitoIntezetId
                               && fegy.Id != fegyelmiUgyId && fegy.FoFegyelmiUgyId != null && fegy.FoFegyelmiUgyId != fegyelmiUgyId))
            {
                throw new WarningException("Másik fegyelmi ügyhöz már van ügy összevonva, nem lehet egyszerre két fő ügy elsőfokú tárgyalás státusszal");
            }

            var ugyek = (from fegy in KonasoftBVFonixContext.FegyelmiUgyek
                         join esemeny in KonasoftBVFonixContext.Esemenyek on fegy.EsemenyId equals esemeny.Id
                         join kiv in KonasoftBVFonixContext.Szemelyzet on fegy.KivizsgaloSzemelyId equals kiv.Id
                         join elr in KonasoftBVFonixContext.Szemelyzet on fegy.ElrendeloSzemelyId equals elr.Id
                         join jellegKsz in KonasoftBVFonixContext.Cimkek on esemeny.JellegCimkeId equals jellegKsz.Id
                         where fegy.FogvatartottId == fegyelmiUgy.FogvatartottId
                              && fegy.StatuszCimkeId == (int)CimkeEnums.FegyelmiUgyStatusz.I_FokuTargyalas
                              && fegy.IntezetId == AlkalmazasKontextusFunctions.Kontextus.RogzitoIntezetId
                              && fegy.Id != fegyelmiUgyId
                         select new FegyelmiUgyOsszevonasListItem()
                         {
                             ElrendeloSzemely = elr.Nev,
                             EsemenyJellege = jellegKsz.Nev,
                             FegyelmiUgyElrendelesenekDatuma = fegy.DontesDatuma.Value,
                             FegyelmiUgyId = fegy.Id,
                             KivizsgaloSzemely = kiv.Nev,
                             Ugyszam = fegy.UgySorszamaIntezetAzon + "/" + fegy.UgySorszamaEv + "/" + fegy.UgySorszama
                         }).ToList();

            model.ValaszthatoFegyelmiUgyek = ugyek;

            if (ugyek.Count == 0)
            {
                throw new WarningException("A fogvatartottnak nincs összevonható fegyelmi ügye");
            }

            return model;
        }

        public void FegyelmiUgyOsszevonasMentes(int fegyelmiUgyId, List<int> alUgyList)
        {

            var fegyelmiUgy = KonasoftBVFonixContext.FegyelmiUgyek
                                                .Include(x => x.Esemeny)
                                                .Include(x => x.Esemeny.Jelleg)
                                                .Include(x => x.KivizsgaloSzemely)
                                                .Include(x => x.KivizsgaloSzemely.Rendfokozat)
                                                .Single(x => x.Id == fegyelmiUgyId);

            if (fegyelmiUgy.StatuszCimkeId != (int)CimkeEnums.FegyelmiUgyStatusz.I_FokuTargyalas)
            {
                throw new WarningException("A fegyelmi ügy nem elsőfokú tárgyalás státuszban van.");
            }

            if (KonasoftBVFonixContext.FegyelmiUgyek.Any(fegy => fegy.FogvatartottId == fegyelmiUgy.FogvatartottId
                               && fegy.StatuszCimkeId == (int)CimkeEnums.FegyelmiUgyStatusz.I_FokuTargyalas
                               && fegy.IntezetId == AlkalmazasKontextusFunctions.Kontextus.RogzitoIntezetId
                               && fegy.Id != fegyelmiUgyId && fegy.FoFegyelmiUgyId != null && fegy.FoFegyelmiUgyId != fegyelmiUgyId))
            {
                throw new WarningException("Másik fegyelmi ügyhöz már van ügy összevonva, nem lehet egyszerre két fő ügy elsőfokú tárgyalás státusszal");
            }

            var egyebAdat = new FegyelmiUgyOsszevonasNaploEgyebAdatModel()
            {
                FoFegyelmiUgyId = fegyelmiUgyId,
                Ugyek = new List<FegyelmiUgyOsszevonasNaploEgyebAdatUgyItemModel>()
            };

            var alUgyek = KonasoftBVFonixContext.FegyelmiUgyek
                                                .Where(x => alUgyList.Contains(x.Id))
                                                .Include(x => x.Esemeny)
                                                .Include(x => x.Esemeny.Jelleg)
                                                .Include(x => x.KivizsgaloSzemely)
                                                .Include(x => x.KivizsgaloSzemely.Rendfokozat)
                                                .ToList();


            var valtozottList = new List<int>(alUgyList);


            using (DbContextTransaction transaction = KonasoftBVFonixContext.Database.BeginTransaction())
            {
                try
                {
                    FegyelmiUgyOsszevonasNaploEgyebAdatUgyItemModel naploUgyItem;
                    foreach (var ugy in alUgyek)
                    {
                        naploUgyItem = new FegyelmiUgyOsszevonasNaploEgyebAdatUgyItemModel();
                        naploUgyItem.ElrendelesDatuma = ugy.DontesDatuma.Value;
                        naploUgyItem.EsemenyJellege = ugy.Esemeny.Jelleg.Nev;
                        naploUgyItem.FegyelmiUgyId = ugy.Id;
                        naploUgyItem.Ugyszam = ugy.UgySorszama;
                        naploUgyItem.UgyszamEv = ugy.UgySorszamaEv;
                        naploUgyItem.UgyszamIntezetAzon = ugy.UgySorszamaIntezetAzon;
                        naploUgyItem.KivizsgaloSzemely = new SzemelyzetAdUser()
                        {
                            Id = ugy.KivizsgaloSzemely.Id,
                            Nev = ugy.KivizsgaloSzemely.Nev,
                            Sid = ugy.KivizsgaloSzemely.AdSid,
                            Rendfokozat = ugy.KivizsgaloSzemely?.RendfokozatKszId == 0 ? null : ugy.KivizsgaloSzemely?.Rendfokozat?.Nev
                        };
                        egyebAdat.Ugyek.Add(naploUgyItem);
                    }

                    naploUgyItem = new FegyelmiUgyOsszevonasNaploEgyebAdatUgyItemModel();
                    naploUgyItem.ElrendelesDatuma = fegyelmiUgy.DontesDatuma.Value;
                    naploUgyItem.EsemenyJellege = fegyelmiUgy.Esemeny.Jelleg.Nev;
                    naploUgyItem.FegyelmiUgyId = fegyelmiUgy.Id;
                    naploUgyItem.Ugyszam = fegyelmiUgy.UgySorszama;
                    naploUgyItem.UgyszamEv = fegyelmiUgy.UgySorszamaEv;
                    naploUgyItem.UgyszamIntezetAzon = fegyelmiUgy.UgySorszamaIntezetAzon;
                    naploUgyItem.KivizsgaloSzemely = new SzemelyzetAdUser()
                    {
                        Id = fegyelmiUgy.KivizsgaloSzemely.Id,
                        Nev = fegyelmiUgy.KivizsgaloSzemely.Nev,
                        Sid = fegyelmiUgy.KivizsgaloSzemely.AdSid,
                        Rendfokozat = fegyelmiUgy.KivizsgaloSzemely?.RendfokozatKszId == 0 ? null : fegyelmiUgy.KivizsgaloSzemely?.Rendfokozat?.Nev
                    };
                    egyebAdat.Ugyek.Add(naploUgyItem);

                    var egyebAdatJson = IktatottDokumentumokFunctions.SerializeIktatottNyomtatvanyAdat(egyebAdat);

                    Naplo naploEntity;
                    foreach (var ugy in alUgyek)
                    {

                        ugy.FoFegyelmiUgyId = fegyelmiUgyId;
                        ugy.StatuszCimkeId = (int)CimkeEnums.FegyelmiUgyStatusz.Osszevonva;
                        ugy.Hatarido = new DateTime(2079, 06, 06);
                        naploEntity = new Naplo
                        {
                            FegyelmiUgyId = ugy.Id,
                            FogvatartottId = ugy.FogvatartottId,
                            TipusCimkeId = (int)FegyelmiNaploTipus.UgyOsszevonasa,
                            EgyebAdatokJson = egyebAdatJson
                        };
                        KonasoftBVFonixContext.Naplo.Add(naploEntity);
                    }

                    naploEntity = new Naplo
                    {
                        FegyelmiUgyId = fegyelmiUgy.Id,
                        FogvatartottId = fegyelmiUgy.FogvatartottId,
                        TipusCimkeId = (int)FegyelmiNaploTipus.UgyOsszevonasa,
                        EgyebAdatokJson = egyebAdatJson
                    };
                    KonasoftBVFonixContext.Naplo.Add(naploEntity);

                    KonasoftBVFonixContext.SaveChanges();

                    transaction.Commit();


                    valtozottList.Add(fegyelmiUgyId);



                }
                catch (Exception e)
                {
                    Log.Error($"Hiba történt az ügy összevonás során (fegyelmiUgyId: {fegyelmiUgyId}, alügyid lista: {string.Join(",", alUgyList)})", e);
                    transaction.Rollback();

                    throw new WarningException("Az összefoglaló jelentést nem sikerült menteni.");
                }
            }
            OnFegyelmiUgyValtozas?.Invoke(new List<int>(), valtozottList, new List<int>());
        }
        public ElsoFokuFegyelmiTargyalasElokeszitesModel GetElsoFokuFegyelmiTargyalasElokeszitese(int fegyelmiUgyId)
        {
            var hatarozatHozoJogkoreDDL = (from cimkek in KonasoftBVFonixContext.Cimkek
                                           join felho in KonasoftBVFonixContext.Felhok on cimkek.FelhoId equals felho.Id
                                           where felho.Id == (int)Felhok.HatarozatHozoJogkore && cimkek.Id != (int)HatarozatHozoJogkore.BVBiro
                                           select new KSelect2ItemModel
                                           {
                                               id = cimkek.Id.ToString(),
                                               text = cimkek.Nev
                                           }).ToList();

            var hatarido = GetFegyelmiHatarido(fegyelmiUgyId, false, (int)FegyelmiUgyStatusz.I_FokuFegyelmiTargyalasElokeszitese) ?? DateTime.Now;

            ElsoFokuFegyelmiTargyalasElokeszitesModel result = new ElsoFokuFegyelmiTargyalasElokeszitesModel
            {
                FegyelmiUgyId = fegyelmiUgyId,
                HatarozatHozoSzemelyCimkeId = hatarozatHozoJogkoreDDL.Select(s => s.id).FirstOrDefault(),
                HatarozatHozoSzemelyOptions = hatarozatHozoJogkoreDDL,
                TargyalasIdopontja = DateTime.Now,
                TargyalasMaxIdopontja = hatarido
            };
            return result;
        }
        public ElsoFokuFegyelmiTargyalasElokeszitesModel GetMasodFokuFegyelmiTargyalasElokeszitese(int fegyelmiUgyId)
        {
            var fegyelmiUgy = FindById(fegyelmiUgyId);
            var minCimkeId = fegyelmiUgy.HatarozatHozoJogkoreCimkeId ?? 0;
            var hatarozatHozoJogkoreDDL = (from cimkek in KonasoftBVFonixContext.Cimkek
                                           where cimkek.Id == fegyelmiUgy.HatarozatHozoJogkoreCimkeId
                                           select new KSelect2ItemModel
                                           {
                                               id = cimkek.Id.ToString(),
                                               text = cimkek.Nev
                                           }).ToList();

            var hatarido = GetFegyelmiHatarido(fegyelmiUgyId, false, (int)FegyelmiUgyStatusz.II_FokuTargyalas) ?? DateTime.Now;

            ElsoFokuFegyelmiTargyalasElokeszitesModel result = new ElsoFokuFegyelmiTargyalasElokeszitesModel
            {
                FegyelmiUgyId = fegyelmiUgyId,
                HatarozatHozoSzemelyCimkeId = hatarozatHozoJogkoreDDL.OrderBy(o => o.id).Select(s => s.id).FirstOrDefault(),
                HatarozatHozoSzemelyOptions = hatarozatHozoJogkoreDDL,
                TargyalasIdopontja = DateTime.Now,
                TargyalasMaxIdopontja = hatarido
            };


            return result;
        }
        public void SaveElsoFokuTargyalasKituzese(ElsoFokuFegyelmiTargyalasElokeszitesModel model)
        {
            List<int> ujFegyelmi = new List<int>();
            List<int> megvaltozottFegyelmi = new List<int>();
            List<int> toroltFegyelmi = new List<int>();

            using (DbContextTransaction transaction = KonasoftBVFonixContext.Database.BeginTransaction())
            {
                try
                {
                    var fegyelmiUgy = FindById(model.FegyelmiUgyId);
                    fegyelmiUgy.ElsofokuTargyalasIdopontja = model.TargyalasIdopontja;
                    fegyelmiUgy.HatarozatHozoJogkoreCimkeId = Int32.Parse(model.HatarozatHozoSzemelyCimkeId);
                    fegyelmiUgy.UgyFoka = 1;
                    var aktIntezet = JogosultsagCacheFunctions.AktualisIntezet.Id;

                    Modify(fegyelmiUgy);

                    var naploEnitity = new Naplo
                    {
                        FegyelmiUgyId = model.FegyelmiUgyId,
                        FogvatartottId = fegyelmiUgy.FogvatartottId,
                        RogzitoIntezetId = aktIntezet,
                        ElsofokuTargyalasIdopontja = model.TargyalasIdopontja,
                        HatarozatHozoJogkoreCimkeId = Int32.Parse(model.HatarozatHozoSzemelyCimkeId),
                        TipusCimkeId = (int)FegyelmiNaploTipus.TargyalasElokeszitese,

                    };

                    KonasoftBVFonixContext.Naplo.Add(naploEnitity);
                    KonasoftBVFonixContext.SaveChanges();
                    transaction.Commit();

                }
                catch (Exception e)
                {

                    Log.Error($"Hiba az első fokú tárgyalás időpontjának kitűzés mentés során (fegyelmiUgyId: {model.FegyelmiUgyId})", e);
                    transaction.Rollback();
                    throw;
                }

                megvaltozottFegyelmi.Add(model.FegyelmiUgyId);
            }

            OnFegyelmiUgyValtozas?.Invoke(ujFegyelmi, megvaltozottFegyelmi, toroltFegyelmi);

        }

        public void SaveMasodFokuTargyalasKituzese(ElsoFokuFegyelmiTargyalasElokeszitesModel model)
        {
            List<int> ujFegyelmi = new List<int>();
            List<int> megvaltozottFegyelmi = new List<int>();
            List<int> toroltFegyelmi = new List<int>();

            using (DbContextTransaction transaction = KonasoftBVFonixContext.Database.BeginTransaction())
            {
                try
                {
                    var fegyelmiUgy = FindById(model.FegyelmiUgyId);
                    fegyelmiUgy.MasodfokuTargyalasIdopontja = model.TargyalasIdopontja;
                    var aktIntezet = JogosultsagCacheFunctions.AktualisIntezet.Id;

                    Modify(fegyelmiUgy);

                    var naploEnitity = new Naplo
                    {
                        FegyelmiUgyId = model.FegyelmiUgyId,
                        FogvatartottId = fegyelmiUgy.FogvatartottId,
                        RogzitoIntezetId = aktIntezet,
                        ElsofokuTargyalasIdopontja = model.TargyalasIdopontja,
                        HatarozatHozoJogkoreCimkeId = Int32.Parse(model.HatarozatHozoSzemelyCimkeId),
                        TipusCimkeId = (int)FegyelmiNaploTipus.MasodfokuTargyalasElokeszites,

                    };

                    KonasoftBVFonixContext.Naplo.Add(naploEnitity);
                    KonasoftBVFonixContext.SaveChanges();
                    transaction.Commit();

                }
                catch (Exception e)
                {

                    Log.Error($"Hiba a másod fokú tárgyalás időpontjának kitűzés mentés során (fegyelmiUgyId: {model.FegyelmiUgyId})", e);
                    transaction.Rollback();
                    throw new WarningException("Hiba a másod fokú tárgyalás időpontjának kitűzés mentés során");
                }

                megvaltozottFegyelmi.Add(model.FegyelmiUgyId);
            }
            OnFegyelmiUgyValtozas?.Invoke(ujFegyelmi, megvaltozottFegyelmi, toroltFegyelmi);

        }

        public KirendeltVedoKereseModel GetKirendeltVedoKereseModalData(List<int> fegyelmiUgyIds, List<int> naplobejegyzesIds)
        {
            var aktIntezet = JogosultsagCacheFunctions.AktualisIntezet.Id;
            //var torvenyszekek = TorvenyszekFunctions.Table.Where(w => w.FeluletenMegjelenitheto == true).ToList();
            //var userTorvenyszek = torvenyszekek.FirstOrDefault(f => f.IntezetId == aktIntezet);
            var model = new KirendeltVedoKereseModel();
            //model.TorvenyszekOptions = torvenyszekek.Select(s => new KSelect2ItemModel() { id = s.Id.ToString(), text = s.TorvenyszekNeve }).OrderBy(o => o.text).ToList();

            //if (naplobejegyzesIds.IsNullOrEmpty())
            //{
            //    model.TorvenyszekId = userTorvenyszek?.Id ?? torvenyszekek.First().Id;
            //    model.Leiras = "";
            //}
            //else
            //{
            //    var naploBejegyzesId = naplobejegyzesIds.First();
            //    var naploBejegyzes = NaploFunctions.FindById(naploBejegyzesId);
            //    model.TorvenyszekId = naploBejegyzes?.TorvenyszekId.Value ?? torvenyszekek.First().Id;
            //    model.Leiras = naploBejegyzes.JegyzokonyvTartalma;
            //}
            return model;
        }

        public MeghatalmazottVedoKereseModel GetMeghatalmazottVedoKereseModalData(List<int> fegyelmiUgyIds, List<int> naplobejegyzesIds)
        {
            var model = new MeghatalmazottVedoKereseModel();
            model.TitulusOptions = CimkeFunctions.GetCimkekByFelhoId((int)Felhok.Titulus)
                .ToList()
                .Select(s => new KSelect2ItemModel() { id = s.Id.ToString(), text = s.Nev })
                .ToList();
            if (naplobejegyzesIds.IsNullOrEmpty())
            {
                model.Leiras = "";
                model.Titulus = int.Parse(model.TitulusOptions.First().id);
                model.VedoElerhetosege = "";
                model.VedoNeve = "";
                model.VedoCime = "";
            }
            else
            {
                var naploBejegyzesId = naplobejegyzesIds.First();
                var naploBejegyzes = NaploFunctions.FindById(naploBejegyzesId);
                model.Leiras = naploBejegyzes.JegyzokonyvTartalma;
                model.Titulus = naploBejegyzes.TitulusCimkeId.Value;
                model.VedoElerhetosege = naploBejegyzes.VedoElerhetosege;
                model.VedoNeve = naploBejegyzes.VedoNeve;
                model.VedoCime = naploBejegyzes.VedoCime;
            }

            return model;
        }
        
        public void ThrowExceptionIfFegyelmiUgyNemModosithato(List<int> fegyelmiUgyIds)
        {
            if (fegyelmiUgyIds == null)
            {
                throw new WarningException("Nincs kiválasztva egy ügy sem.");
            }
            var fegyelmiUgyek = Table.Where(w => fegyelmiUgyIds.Contains(w.Id)).ToList();
            var oszevontfegyelmiUgyek = fegyelmiUgyek.Where(w => w.FoFegyelmiUgyId != null).ToList();
            if (oszevontfegyelmiUgyek.Count() > 0)
            {
                throw new WarningException("Összevont fegyelmi ügyek nem módosíthatóak.");
            }
        }

        public List<int> SaveKirendeltVedoKereseModalData(KirendeltVedoKereseModel model)
        {
            var aktIntezet = JogosultsagCacheFunctions.AktualisIntezet.Id;
            List<int> naploIds = new List<int>();
            List<int> ujFegyelmi = new List<int>();
            List<int> megvaltozottFegyelmi = new List<int>();
            List<int> toroltFegyelmi = new List<int>();

            using (DbContextTransaction transaction = KonasoftBVFonixContext.Database.BeginTransaction())
            {
                var currentId = 0;
                try
                {
                    if (model.NaplobejegyzesIds.IsNullOrEmpty())
                    {
                        foreach (var fegyelmiUgyId in model.FegyelmiUgyIds)
                        {
                            currentId = fegyelmiUgyId;
                            var fegyelmiUgy = Table.Single(x => x.Id == fegyelmiUgyId);                            
                            if (model.IsRogzites)
                            {
                                fegyelmiUgy.VanJogiKepviselet = true;
                                fegyelmiUgy.JogiKepviseletetKer = false;
                            }

                            var naploEnitity = new Naplo
                            {
                                FegyelmiUgyId = fegyelmiUgyId,
                                FogvatartottId = fegyelmiUgy.FogvatartottId,
                                Vegleges = model.IsRogzites,
                                JegyzokonyvTartalma = model.Leiras,
                                RogzitoIntezetId = aktIntezet,
                                TorvenyszekId = model.TorvenyszekId,
                                TipusCimkeId = (int)FegyelmiNaploTipus.KirendeltVedoKerese,

                            };

                            KonasoftBVFonixContext.Naplo.Add(naploEnitity);

                            KonasoftBVFonixContext.SaveChanges();

                            naploIds.Add(naploEnitity.Id);
                            megvaltozottFegyelmi.Add(fegyelmiUgyId);
                        }
                    }
                    else
                    {
                        foreach (var naploId in model.NaplobejegyzesIds)
                        {
                            var naplo = KonasoftBVFonixContext.Naplo.Single(x => x.Id == naploId);

                            var fegyelmiUgy = Table.Single(x => x.Id == naplo.FegyelmiUgyId);
                            currentId = naplo.FegyelmiUgyId;
                            if (model.IsRogzites)
                            {
                                fegyelmiUgy.VanJogiKepviselet = true;
                                fegyelmiUgy.JogiKepviseletetKer = false;
                            }

                            naplo.JegyzokonyvTartalma = model.Leiras;
                            naplo.RogzitoIntezetId = aktIntezet;
                            naplo.TorvenyszekId = model.TorvenyszekId;
                            naplo.Vegleges = naplo.Vegleges == true ? naplo.Vegleges : model.IsRogzites;

                            KonasoftBVFonixContext.SaveChanges();

                            naploIds.Add(naploId);
                            megvaltozottFegyelmi.Add(naplo.FegyelmiUgyId);
                        }
                    }
                    var fegyelmiUgyekReintegraciosTisztiJogkorben = Table.Where(w => model.FegyelmiUgyIds.Contains(w.Id)).ToList().Select(s => (FegyelmiUgyViewModel)s).ToList()
                       .Where(w => w.StatuszCimkeId == (int)FegyelmiUgyStatusz.ReintegraciosTisztiJogkorben).ToList();
                    if (fegyelmiUgyekReintegraciosTisztiJogkorben.Count > 0)
                    {
                        ReintegraciosTisztDontesModelVisszakuldes visszakuldesModel = new ReintegraciosTisztDontesModelVisszakuldes()
                        {
                            FegyelmiUgyIds = fegyelmiUgyekReintegraciosTisztiJogkorben.Select(s => s.Id).ToList(),
                            VisszakuldesOka = (int)VisszakuldesOka.JogiKepviseletetKer
                        };

                        SaveReintegraciosTisztDontesVisszakuldesModel(visszakuldesModel);
                    }


                    transaction.Commit();
                }
                catch (Exception e)
                {
                    Log.Error($"Hiba a kirendelt védő kérése mentés során (fegyelmiUgyId: {currentId})", e);
                    transaction.Rollback();

                    throw;
                }
            }


            OnFegyelmiUgyValtozas?.Invoke(ujFegyelmi, megvaltozottFegyelmi, toroltFegyelmi);
            return naploIds;
        }
        public List<int> SaveMeghatalmazottVedoKereseModalData(MeghatalmazottVedoKereseModel model)
        {

            var aktIntezet = JogosultsagCacheFunctions.AktualisIntezet.Id;
            List<int> naploIds = new List<int>();

            List<int> ujFegyelmi = new List<int>();
            List<int> megvaltozottFegyelmi = new List<int>();
            List<int> toroltFegyelmi = new List<int>();

            using (DbContextTransaction transaction = KonasoftBVFonixContext.Database.BeginTransaction())
            {
                var currentId = 0;
                try
                {
                    if (model.NaplobejegyzesIds.IsNullOrEmpty())
                    {
                        foreach (var fegyelmiUgyId in model.FegyelmiUgyIds)
                        {
                            currentId = fegyelmiUgyId;
                            var fegyelmiUgy = Table.Single(x => x.Id == fegyelmiUgyId);

                            if (model.IsRogzites)
                            {
                                fegyelmiUgy.VanJogiKepviselet = true;
                                fegyelmiUgy.JogiKepviseletetKer = false;
                            }

                            var naploEnitity = new Naplo
                            {
                                FegyelmiUgyId = fegyelmiUgyId,
                                FogvatartottId = fegyelmiUgy.FogvatartottId,
                                JegyzokonyvTartalma = model.Leiras,
                                RogzitoIntezetId = aktIntezet,
                                Vegleges = model.IsRogzites,
                                VedoCime = model.VedoCime,
                                VedoNeve = model.VedoNeve,
                                VedoElerhetosege = model.VedoElerhetosege,
                                TitulusCimkeId = model.Titulus,
                                TipusCimkeId = (int)FegyelmiNaploTipus.MeghatalmazottVedoKerese,
                            };

                            KonasoftBVFonixContext.Naplo.Add(naploEnitity);

                            KonasoftBVFonixContext.SaveChanges();

                            naploIds.Add(naploEnitity.Id);
                            megvaltozottFegyelmi.Add(fegyelmiUgyId);

                        }
                    }
                    else
                    {
                        foreach (var naploId in model.NaplobejegyzesIds)
                        {
                            var naplo = KonasoftBVFonixContext.Naplo.Single(x => x.Id == naploId);

                            var fegyelmiUgy = Table.Single(x => x.Id == naplo.FegyelmiUgyId);
                            currentId = naplo.FegyelmiUgyId;
                            if (model.IsRogzites)
                            {
                                fegyelmiUgy.VanJogiKepviselet = true;
                                fegyelmiUgy.JogiKepviseletetKer = false;
                            }

                            naplo.JegyzokonyvTartalma = model.Leiras;
                            naplo.RogzitoIntezetId = aktIntezet;
                            naplo.VedoCime = model.VedoCime;
                            naplo.VedoNeve = model.VedoNeve;
                            naplo.VedoElerhetosege = model.VedoElerhetosege;
                            naplo.TitulusCimkeId = model.Titulus;
                            naplo.Vegleges = naplo.Vegleges == true ? naplo.Vegleges : model.IsRogzites;

                            KonasoftBVFonixContext.SaveChanges();

                            naploIds.Add(naploId);
                            megvaltozottFegyelmi.Add(naplo.FegyelmiUgyId);
                        }
                    }
                    var fegyelmiUgyekReintegraciosTisztiJogkorben = Table.Where(w => model.FegyelmiUgyIds.Contains(w.Id)).ToList().Select(s => (FegyelmiUgyViewModel)s).ToList()
                        .Where(w => w.StatuszCimkeId == (int)FegyelmiUgyStatusz.ReintegraciosTisztiJogkorben).ToList();
                    if (fegyelmiUgyekReintegraciosTisztiJogkorben.Count > 0)
                    {
                        ReintegraciosTisztDontesModelVisszakuldes visszakuldesModel = new ReintegraciosTisztDontesModelVisszakuldes()
                        {
                            FegyelmiUgyIds = fegyelmiUgyekReintegraciosTisztiJogkorben.Select(s => s.Id).ToList(),
                            VisszakuldesOka = (int)VisszakuldesOka.JogiKepviseletetKer
                        };
                        SaveReintegraciosTisztDontesVisszakuldesModel(visszakuldesModel);
                    }


                    transaction.Commit();
                }
                catch (Exception e)
                {
                    Log.Error($"Hiba a meghatalmazott védő kérése mentés során (fegyelmiUgyId: {currentId})", e);
                    transaction.Rollback();

                    throw;
                }
            }

            OnFegyelmiUgyValtozas?.Invoke(ujFegyelmi, megvaltozottFegyelmi, toroltFegyelmi);
            return naploIds;
        }
        public List<FegyelmiUgyViewModel> GetFegyelmiUgyekByNaploTipus(List<int> fegyelmiUgyIds, int naploTipusCimkeId)
        {

            var fegyelmiUgyIdsNaploTipushoz = KonasoftBVFonixContext.Naplo
                .Where(w => w.TipusCimkeId == naploTipusCimkeId && fegyelmiUgyIds.Contains(w.FegyelmiUgyId))
                .Select(s => s.FegyelmiUgyId)
                .ToList();
            var fegyelmiUgyek = Table.Where(w => fegyelmiUgyIdsNaploTipushoz.Contains(w.Id)).ToList().Select(s => (FegyelmiUgyViewModel)s).ToList();
            return fegyelmiUgyek;
        }
        public HataridoModositasModel GetHataridoModositasModalData(List<int> fegyelmiUgyIds)
        {
            var fegyelmiUgyek = Table.Where(w => fegyelmiUgyIds.Contains(w.Id)).ToList();
            DateTime datum = DateTime.MaxValue;
            foreach (var fegyelmiUgy in fegyelmiUgyek)
            {
                var hatarIdo = GetFegyelmiHatarido(fegyelmiUgy.Id, true);
                datum = hatarIdo.Value.AddDays(-1);
                if (datum < DateTime.Today)
                {
                    datum = hatarIdo.Value;
                }
            }
            var model = new HataridoModositasModel()
            {
                Datum = datum,
                Leiras = "",
                MaxDatum = datum,
            };
            return model;
        }
        public bool SaveHataridoModositasModalData(HataridoModositasModel model)
        {
            var fegyelmiUgyek = Table.Where(w => model.FegyelmiUgyIds.Contains(w.Id)).ToList();
            var osszesKivizsgalasFolyamatban = fegyelmiUgyek.All(a => a.StatuszCimkeId == (int)FegyelmiUgyStatusz.KivizsgalasFolyamatban);
            var osszesIFokuTargyalasban = fegyelmiUgyek.All(a => a.StatuszCimkeId == (int)FegyelmiUgyStatusz.I_FokuTargyalas);
            List<int> ujFegyelmi = new List<int>();
            List<int> megvaltozottFegyelmi = new List<int>();
            List<int> toroltFegyelmi = new List<int>();
            if (osszesKivizsgalasFolyamatban || osszesIFokuTargyalasban)
            {
                using (DbContextTransaction transaction = KonasoftBVFonixContext.Database.BeginTransaction())
                {
                    var currentId = 0;
                    try
                    {

                        foreach (var fegyelmiUgyId in model.FegyelmiUgyIds)
                        {
                            currentId = fegyelmiUgyId;
                            var fegyelmiUgy = (FegyelmiUgyViewModel)Table.Single(x => x.Id == fegyelmiUgyId);

                            var hatarIdo = GetFegyelmiHatarido(fegyelmiUgy.Id, true);
                            fegyelmiUgy.Hatarido = hatarIdo;
                            fegyelmiUgy.KivizsgalasiHatarido = hatarIdo < model.Datum ? hatarIdo : model.Datum;
                            fegyelmiUgy.HataridoModositasJavaslat = false;
                            fegyelmiUgy.ElsofokuTargyalasIdopontja = null;
                            if (fegyelmiUgy.KozvetitoiEljarasban != true)
                            {
                                fegyelmiUgy.StatuszCimkeId = (int)FegyelmiUgyStatusz.KivizsgalasFolyamatban;
                            }
                            Modify(fegyelmiUgy);
                            KonasoftBVFonixContext.SaveChanges();


                            var naploEnitity = new Naplo
                            {
                                FegyelmiUgyId = fegyelmiUgyId,
                                FogvatartottId = fegyelmiUgy.FogvatartottId,
                                JegyzokonyvTartalma = model.Leiras,
                                Hatarido = model.Datum,
                                TipusCimkeId = (int)FegyelmiNaploTipus.HataridoModositas,

                            };

                            KonasoftBVFonixContext.Naplo.Add(naploEnitity);
                            KonasoftBVFonixContext.SaveChanges();

                            megvaltozottFegyelmi.Add(fegyelmiUgyId);

                        }



                        transaction.Commit();
                    }
                    catch (Exception e)
                    {
                        Log.Error($"Hiba a határidő módosíás mentés során (fegyelmiUgyId: {currentId})", e);
                        transaction.Rollback();

                        throw;
                    }
                }
            }
            else
            {
                throw new WarningException("A határidő nem módosítható");
            }
            OnFegyelmiUgyValtozas?.Invoke(ujFegyelmi, megvaltozottFegyelmi, toroltFegyelmi);
            return true;
        }
        public bool SaveFelfuggesztesMegszuntetes(FelfuggesztesMegszunteteseViewModel model)
        {
            List<int> ujFegyelmi = new List<int>();
            List<int> megvaltozottFegyelmi = new List<int>();
            List<int> toroltFegyelmi = new List<int>();

            using (DbContextTransaction transaction = KonasoftBVFonixContext.Database.BeginTransaction())
            {
                try
                {
                    var timeStamp = DateTime.Now;

                    var fegyelmiUgy = FindById(model.FegyelmiUgyIds.FirstOrDefault());
                    fegyelmiUgy.Felfuggesztve = false;
                    fegyelmiUgy.FelfuggesztesDatum = null;
                    KonasoftBVFonixContext.SaveChanges();
                    var tanuk = KonasoftBVFonixContext.EsemenyResztvevok
                            .Include(i => i.AllomanyiSzemely)
                            .Where(x => x.EsemenyId == fegyelmiUgy.EsemenyId && x.ErintettsegFokaCimkeId == (int)CimkeEnums.ErintettsegFoka.SzemelyiAllomanyiTanu)
                            .Select(x => x.AllomanyiSzemely)
                            .ToList();
                    foreach (var tanu in tanuk)
                    {
                        if (tanu.AdSid == model.KivizsgaloSzemelySid)
                        {
                            throw new WarningException("A tanú nem vizsgálhatja ki az ügyet");
                        }
                    }

                    // Ha GetFegyelmiHatarido() == null akkor exception
                    var torvenyiHatarido = GetFegyelmiHatarido(fegyelmiUgy.Id, false, (int)fegyelmiUgy.StatuszCimkeId).Value;
                    fegyelmiUgy.Hatarido = torvenyiHatarido;

                    if (fegyelmiUgy.StatuszCimkeId == (int)CimkeEnums.FegyelmiUgyStatusz.KivizsgalasFolyamatban)
                    {
                        fegyelmiUgy.KivizsgalasiHatarido = model.KivizsgalasiHatarido;
                    }

                    fegyelmiUgy.KivizsgaloSzemelyId = SzemelyzetFunctions.SzemelyzetLekeresVagyLetrehozas(model.KivizsgaloSzemelySid, null, null).Id;
                    Modify(fegyelmiUgy);

                    // JUNG: miért OrDefault??
                    var felfuggesztes = KonasoftBVFonixContext.Felfuggesztesek.Where(w => w.FegyelmiUgyId == model.FegyelmiUgyIds.FirstOrDefault() && w.Vege == null).SingleOrDefault();

                    felfuggesztes.Vege = timeStamp;

                    FelfuggesztesFunctions.Modify((FelfuggesztesViewModel)felfuggesztes);

                    model.KivizsgalasiHatarido = model.KivizsgalasiHatarido > torvenyiHatarido ? torvenyiHatarido : model.KivizsgalasiHatarido;


                    var naploEnitity = new Naplo
                    {
                        FegyelmiUgyId = fegyelmiUgy.Id,
                        FogvatartottId = fegyelmiUgy.FogvatartottId,
                        TipusCimkeId = (int)FegyelmiNaploTipus.FelfuggesztesMegszuntetese,
                        KivizsgaloSzemelyId = SzemelyzetFunctions.SzemelyzetLekeresVagyLetrehozas(model.KivizsgaloSzemelySid, null, null).Id,
                        Hatarido = model.KivizsgalasiHatarido,
                    };

                    KonasoftBVFonixContext.Naplo.Add(naploEnitity);
                    KonasoftBVFonixContext.SaveChanges();
                    transaction.Commit();

                    megvaltozottFegyelmi.Add(fegyelmiUgy.Id);

                }
                catch (Exception e)
                {
                    Log.Error($"Hiba a felfüggesztés megszüntetése során (fegyelmiUgyId: {model.FegyelmiUgyIds.FirstOrDefault()})", e);
                    transaction.Rollback();
                    throw;
                }
            }

            OnFegyelmiUgyValtozas?.Invoke(ujFegyelmi, megvaltozottFegyelmi, toroltFegyelmi);
            return true;
        }
        public FelfuggesztesModel GetFelfuggesztesModalData(List<int> fegyelmiUgyIds)
        {
            var cimkek = KonasoftBVFonixContext.Cimkek
                .Where(w => w.FelhoId == (int)Felhok.FelfuggesztesOka)
                .Select(x => new KSelect2ItemModel() { id = x.Id.ToString(), text = x.Nev })
                .ToList();
            var model = new FelfuggesztesModel() { FelfuggesztesOkaOptions = cimkek };

            var fegyelmiUgyId = fegyelmiUgyIds.First();
            var fegyelmiUgy = (FegyelmiUgyViewModel)Table.Single(x => x.Id == fegyelmiUgyId);
            model.FelfuggesztesOkaCimkeId = fegyelmiUgy.FelfuggesztesOkaCimkeId;
            return model;
        }

        public bool SaveFelfuggesztesModalData(FelfuggesztesModel model)
        {
            var fegyelmiUgyek = Table.Where(w => model.FegyelmiUgyIds.Contains(w.Id)).ToList();

            if (fegyelmiUgyek.Any(a => a.Felfuggesztve == true))
            {
                throw new WarningException("Az ügy már fel van függesztve");
            }
            //if (fegyelmiUgyek.Any(a => a.StatuszCimkeId == (int)FegyelmiUgyStatusz.Kezdemenyezve))
            //{
            //    throw new WarningException("Az ügyet kezdeményezve státuszban nem lehet felfüggeszteni");
            //}

            List<int> ujFegyelmi = new List<int>();
            List<int> megvaltozottFegyelmi = new List<int>();
            List<int> toroltFegyelmi = new List<int>();

            using (DbContextTransaction transaction = KonasoftBVFonixContext.Database.BeginTransaction())
            {
                var currentId = 0;
                try
                {
                    var kezdete = DateTime.Now;

                    foreach (var fegyelmiUgyId in model.FegyelmiUgyIds)
                    {
                        currentId = fegyelmiUgyId;
                        var fegyelmiUgy = (FegyelmiUgyViewModel)Table.Single(x => x.Id == fegyelmiUgyId);

                        fegyelmiUgy.Felfuggesztve = true;
                        fegyelmiUgy.FelfuggesztesiJavaslat = false;
                        fegyelmiUgy.FelfuggesztesDatum = kezdete;
                        fegyelmiUgy.FelfuggesztesOkaCimkeId = model.FelfuggesztesOkaCimkeId;

                        Modify(fegyelmiUgy);

                        FelfuggesztesFunctions.CreateFelfuggesztes(kezdete, model.FelfuggesztesOkaCimkeId.Value, fegyelmiUgyId);

                        var torvenyiHatarido = GetFegyelmiHatarido(fegyelmiUgy.Id, false);
                        fegyelmiUgy.Hatarido = torvenyiHatarido;

                        if (fegyelmiUgy.StatuszCimkeId == (int)CimkeEnums.FegyelmiUgyStatusz.KivizsgalasFolyamatban ||
                            fegyelmiUgy.StatuszCimkeId == (int)CimkeEnums.FegyelmiUgyStatusz.ReintegraciosTisztiJogkorben)
                        {
                            fegyelmiUgy.KivizsgalasiHatarido = torvenyiHatarido;
                        }

                        Modify(fegyelmiUgy);

                        var naploEnitity = new Naplo
                        {
                            FegyelmiUgyId = fegyelmiUgyId,
                            FogvatartottId = fegyelmiUgy.FogvatartottId,
                            FelfuggesztesOkaCimkeId = model.FelfuggesztesOkaCimkeId,
                            TipusCimkeId = (int)FegyelmiNaploTipus.Felfuggesztes,

                        };

                        KonasoftBVFonixContext.Naplo.Add(naploEnitity);
                        KonasoftBVFonixContext.SaveChanges();

                        megvaltozottFegyelmi.Add(fegyelmiUgyId);

                    }

                    transaction.Commit();
                }
                catch (Exception e)
                {
                    Log.Error($"Hiba a felfüggesztés mentés során (fegyelmiUgyId: {currentId})", e);
                    transaction.Rollback();

                    throw;
                }
            }

            OnFegyelmiUgyValtozas?.Invoke(ujFegyelmi, megvaltozottFegyelmi, toroltFegyelmi);
            return true;
        }

        public VedoTelefonosTajekoztatasaModel GetVedoTelefonosTajekoztatasaModalData(List<int> fegyelmiUgyIds, List<int> naplobejegyzesIds)
        {
            var model = new VedoTelefonosTajekoztatasaModel();
            var fegyelmiJogkorGyakorlojaUsers = SzemelyzetFunctions.GetFegyelmiJogkorGyakorlojaAlkalmazottak();
            var reintUsers = SzemelyzetFunctions.GetFegyelmiReintegraciosTisztiAlkalmazottak();

            var meghallgato = new List<AdFegyelmiUserItem>();
            meghallgato.AddRange(reintUsers);
            meghallgato.AddRange(fegyelmiJogkorGyakorlojaUsers);
            meghallgato = meghallgato.GroupBy(x => x.Sid).Select(x => x.First()).ToList();

            var tajekoztatastNyujtok = meghallgato.ToList().Select(x => new KSelect2ItemModel() { id = x.Sid, text = x.Displayname + (x.Rendfokozat == null ? "" : (" " + x.Rendfokozat)) }).ToList();
            var userSid = AlkalmazasKontextusFunctions.Kontextus.SzemelyzetSid;
            var isUserInList = tajekoztatastNyujtok.Any(a => a.id == userSid);

            if (naplobejegyzesIds != null && naplobejegyzesIds.Count > 0)
            {
                var temp = GetVedoTelefonosTajekoztatasaNyomtatvanyokByNaplok(naplobejegyzesIds);
                var naplo = NaploFunctions.FindById(naplobejegyzesIds.First());
                var szemelyzetSid = SzemelyzetFunctions.Table.AsNoTracking().SingleOrDefault(x => x.Id == naplo.TajekoztatastNyujtoId.Value).AdSid;
                var tempModel = temp.FirstOrDefault();
                model = new VedoTelefonosTajekoztatasaModel()
                {
                    TajekoztatastNyujtoOptions = tajekoztatastNyujtok,
                    Leiras = tempModel.TajekoztatoTartalma,
                    TajekoztatasSikertelensegenekOka = tempModel.SikertelenText,
                    TajekoztatasIdeje = naplo.TajekoztatasIdeje.Value,
                    TajekoztatastNyujto = szemelyzetSid,
                    Tajekoztatott = tempModel.TajekoztatottSzemelyNev,
                    Telefonszam = tempModel.TajekoztatottSzemelyTel,
                    FegyelmiUgyIds = fegyelmiUgyIds,
                    NaplobejegyzesIds = naplobejegyzesIds,
                };
                return model;
            }
            else
            {

                model = new VedoTelefonosTajekoztatasaModel()
                {
                    TajekoztatastNyujtoOptions = tajekoztatastNyujtok,
                    Leiras = "",
                    TajekoztatasSikertelensegenekOka = "",
                    TajekoztatasIdeje = DateTime.Now,
                    TajekoztatastNyujto = isUserInList ? userSid : null,
                    Tajekoztatott = "",
                    Telefonszam = "",
                };
            }

            return model;
        }
        public List<int> SaveVedoTelefonosTajekoztatasaModalData(VedoTelefonosTajekoztatasaModel model)
        {
            List<int> naploIds = new List<int>();
            var fegyelmiUgyek = Table.Where(w => model.FegyelmiUgyIds.Contains(w.Id)).ToList();
            //var elsofokuTargyalason = fegyelmiUgyek.Any(a => a.StatuszCimkeId == (int)FegyelmiUgyStatusz.I_FokuTargyalas && a.ElsofokuTargyalasIdopontja != null);
            //var masodfokuTargyalason = fegyelmiUgyek.Any(a => a.StatuszCimkeId == (int)FegyelmiUgyStatusz.II_FokuTargyalas && a.MasodfokuTargyalasIdopontja != null);
            //if (!elsofokuTargyalason && !masodfokuTargyalason)
            //{
            //    throw new WarningException("Az ügy nincs tárgyalás alatt");
            //}

            List<int> ujFegyelmi = new List<int>();
            List<int> megvaltozottFegyelmi = new List<int>();
            List<int> toroltFegyelmi = new List<int>();

            using (DbContextTransaction transaction = KonasoftBVFonixContext.Database.BeginTransaction())
            {
                var currentId = 0;
                try
                {
                    int? tajekoztatastNyujtoId = null;
                    if (model.TajekoztatastNyujto != null)
                    {
                        tajekoztatastNyujtoId = SzemelyzetFunctions.SzemelyzetLekeresVagyLetrehozas(model.TajekoztatastNyujto, null, null).Id;
                    }

                    if (model.NaplobejegyzesIds.IsNullOrEmpty())
                    {
                        foreach (var fegyelmiUgyId in model.FegyelmiUgyIds)
                        {
                            currentId = fegyelmiUgyId;
                            var fegyelmiUgy = (FegyelmiUgyViewModel)Table.Single(x => x.Id == fegyelmiUgyId);

                            var naploEnitity = new Naplo
                            {
                                FegyelmiUgyId = fegyelmiUgyId,
                                JegyzokonyvTartalma = model.Leiras,
                                FogvatartottId = fegyelmiUgy.FogvatartottId,
                                TajekoztatastNyujtoId = tajekoztatastNyujtoId,
                                Tajekoztatott = model.Tajekoztatott,
                                Telefonszam = model.Telefonszam,
                                TajekoztatasIdeje = model.TajekoztatasIdeje,
                                AlairtaFl = model.IsRogzites,
                                TajekoztatasSikertelensegenekOka = model.TajekoztatasSikertelensegenekOka,
                                TipusCimkeId = (int)FegyelmiNaploTipus.VedoTelefonosTajekoztatasa,

                            };

                            KonasoftBVFonixContext.Naplo.Add(naploEnitity);
                            KonasoftBVFonixContext.SaveChanges();


                            naploIds.Add(naploEnitity.Id);
                            megvaltozottFegyelmi.Add(fegyelmiUgyId);

                        }
                    }
                    else
                    {
                        foreach (var naploId in model.NaplobejegyzesIds)
                        {
                            var naplo = KonasoftBVFonixContext.Naplo.Single(x => x.Id == naploId);
                            var fegyelmiUgy = (FegyelmiUgyViewModel)Table.Single(x => x.Id == naplo.FegyelmiUgyId);
                            currentId = naplo.FegyelmiUgyId;

                            //FegyelmiUgyId = fegyelmiUgyId,
                            naplo.JegyzokonyvTartalma = model.Leiras;
                            //FogvatartottId = fegyelmiUgy.FogvatartottId,
                            naplo.TajekoztatastNyujtoId = tajekoztatastNyujtoId;
                            naplo.Tajekoztatott = model.Tajekoztatott;
                            naplo.Telefonszam = model.Telefonszam;
                            naplo.TajekoztatasIdeje = model.TajekoztatasIdeje;
                            naplo.AlairtaFl = model.IsRogzites;
                            naplo.TajekoztatasSikertelensegenekOka = model.TajekoztatasSikertelensegenekOka;
                            //naplo.TipusCimkeId = (int)FegyelmiNaploTipus.VedoTelefonosTajekoztatasa;

                            KonasoftBVFonixContext.SaveChanges();
                            naploIds.Add(naploId);


                        }
                    }


                    transaction.Commit();
                }
                catch (Exception e)
                {
                    Log.Error($"Hiba a védő telefonos tájékoztatása mentés során (fegyelmiUgyId: {currentId})", e);
                    transaction.Rollback();

                    throw;
                }
            }

            OnFegyelmiUgyValtozas?.Invoke(ujFegyelmi, megvaltozottFegyelmi, toroltFegyelmi);
            return naploIds;
        }

        public FegyelmiUgyMasodFokuTargyalasiJegyzokonyvModel GetMasodFokuTargyalasiJegyzokonyv(List<int> fegyelmiUgyIds, List<int> naplobejegyzesIds)
        {
            FegyelmiUgyMasodFokuTargyalasiJegyzokonyvModel model;
            var fegyelmiUgyId = fegyelmiUgyIds.First();
            var fegyelmiUgy = FindById(fegyelmiUgyId);
            model = new FegyelmiUgyMasodFokuTargyalasiJegyzokonyvModel()
            {
                FegyelmiUgyIds = fegyelmiUgyIds,
                JegyzokonyvVezetoSid = WindowsIdentity.GetCurrent().User.Value,
                HatarozatHozoJogkoreCimkeId = fegyelmiUgy.HatarozatHozoJogkoreCimkeId
            };

            //if (fegyelmiUgy.ElrendeloSzemelyId.HasValue)
            //    model.FegyelmiJogkorGyakorlojaSid = SzemelyzetFunctions.FindById(fegyelmiUgy.ElrendeloSzemelyId.Value).AdSid;

            var fegyelmiJogkorGyakorlojaUsers = SzemelyzetFunctions.GetFegyelmiJogkorGyakorlojaAlkalmazottak();
            var reintUsers = SzemelyzetFunctions.GetFegyelmiReintegraciosTisztiAlkalmazottak();
            var jogkorGyakorloUsers = SzemelyzetFunctions.GetFegyelmiJogkorGyakorlojaAlkalmazottak();

            var reintJogkorGyakorloUsers = new List<AdFegyelmiUserItem>();
            reintJogkorGyakorloUsers.AddRange(reintUsers);
            reintJogkorGyakorloUsers.AddRange(jogkorGyakorloUsers);
            reintJogkorGyakorloUsers = reintJogkorGyakorloUsers.GroupBy(x => x.Sid).Select(x => x.First()).ToList();


            model.TovabbiSzemelyekOptions = reintJogkorGyakorloUsers.Select(x => new KSelect2ItemModel() { id = x.Displayname + (x.Rendfokozat == null ? "" : (" " + x.Rendfokozat)), text = x.Displayname + (x.Rendfokozat == null ? "" : (" " + x.Rendfokozat)) }).ToList();
            model.FegyelmiJogkorGyakorlojaOptions = fegyelmiJogkorGyakorlojaUsers.Select(x => new KSelect2ItemModel() { id = x.Sid, text = x.Displayname + (x.Rendfokozat == null ? "" : (" " + x.Rendfokozat)) }).ToList();
            model.JegyzokonyvVezetoOptions = reintJogkorGyakorloUsers.Select(x => new KSelect2ItemModel() { id = x.Sid, text = x.Displayname + (x.Rendfokozat == null ? "" : (" " + x.Rendfokozat)) }).ToList();

            if (!naplobejegyzesIds.IsNullOrEmpty())
            {
                var naplobejegyzesId = naplobejegyzesIds.First();
                var naploBejegyzes = NaploFunctions.FindById(naplobejegyzesId);

                if (naploBejegyzes.JegyzokonyvVezetoSzemelyId.HasValue)
                    model.JegyzokonyvVezetoSid = SzemelyzetFunctions.FindById(naploBejegyzes.JegyzokonyvVezetoSzemelyId.Value).AdSid;

                if (naploBejegyzes.DonteshozoSzemelyId.HasValue)
                    model.FegyelmiJogkorGyakorlojaSid = SzemelyzetFunctions.FindById(naploBejegyzes.DonteshozoSzemelyId.Value).AdSid;

                model.NaplobejegyzesIds = naplobejegyzesIds;
                model.Leiras = naploBejegyzes.JegyzokonyvTartalma;

                if (!string.IsNullOrWhiteSpace(naploBejegyzes.TovabbiJelenlevok))
                {
                    model.TovabbiSzemelyekList = naploBejegyzes.TovabbiJelenlevok.Split(new string[] { ", " }, StringSplitOptions.None).ToList();
                    var nemLetezoTovabbiSzemelyek = model.TovabbiSzemelyekList.Except(model.TovabbiSzemelyekOptions.Select(s => s.text));
                    var nemLetezoTovabbiSzemelyekSelect2ItemModel = nemLetezoTovabbiSzemelyek.Select(x => new KSelect2ItemModel() { id = x, text = x }).ToList();
                    model.TovabbiSzemelyekOptions.AddRange(nemLetezoTovabbiSzemelyekSelect2ItemModel);
                }
            }

            return model;
        }

        public List<int> SaveMasodFokuTargyalasiJegyzokonyv(FegyelmiUgyMasodFokuTargyalasiJegyzokonyvModel model)
        {
            var aktIntezet = JogosultsagCacheFunctions.AktualisIntezet.Id;
            List<int> naploIds = new List<int>();

            List<int> ujFegyelmi = new List<int>();
            List<int> megvaltozottFegyelmi = new List<int>();
            List<int> toroltFegyelmi = new List<int>();

            using (DbContextTransaction transaction = KonasoftBVFonixContext.Database.BeginTransaction())
            {
                var currentId = 0;
                try
                {

                    if (!model.NaplobejegyzesIds.IsNullOrEmpty())
                    {
                        foreach (var naplobejegyzesId in model.NaplobejegyzesIds)
                        {
                            var naploBejegyzes = NaploFunctions.FindById(naplobejegyzesId);
                            naploBejegyzes.JegyzokonyvTartalma = model.Leiras;
                            naploBejegyzes.JegyzokonyvVezetoSzemelyId = SzemelyzetFunctions.SzemelyzetLekeresVagyLetrehozas(model.JegyzokonyvVezetoSid, null, null).Id;
                            naploBejegyzes.DonteshozoSzemelyId = SzemelyzetFunctions.SzemelyzetLekeresVagyLetrehozas(model.FegyelmiJogkorGyakorlojaSid, null, null).Id;
                            naploBejegyzes.KihallgatasIntezetId = AlkalmazasKontextusFunctions.Kontextus.RogzitoIntezetId;
                            naploBejegyzes.TovabbiJelenlevok = model.TovabbiSzemelyekList == null ? null : string.Join(", ", model.TovabbiSzemelyekList);
                            NaploFunctions.Modify(naploBejegyzes);

                            var fegyelmiUgy = FindById(naploBejegyzes.FegyelmiUgyId);
                            fegyelmiUgy.HatarozatotHozottSzemelyId = naploBejegyzes.DonteshozoSzemelyId;
                            Modify(fegyelmiUgy);

                            KonasoftBVFonixContext.SaveChanges();
                            naploIds.Add(naploBejegyzes.Id);
                            megvaltozottFegyelmi.Add(naploBejegyzes.FegyelmiUgyId);
                        }
                    }
                    else
                    {
                        foreach (var fegyelmiUgyId in model.FegyelmiUgyIds)
                        {
                            var entity = new Naplo()
                            {
                                FegyelmiUgyId = fegyelmiUgyId,
                                JegyzokonyvTartalma = model.Leiras,
                                JegyzokonyvVezetoSzemelyId = SzemelyzetFunctions.SzemelyzetLekeresVagyLetrehozas(model.JegyzokonyvVezetoSid, null, null).Id,
                                DonteshozoSzemelyId = SzemelyzetFunctions.SzemelyzetLekeresVagyLetrehozas(model.FegyelmiJogkorGyakorlojaSid, null, null).Id,
                                KihallgatasIntezetId = AlkalmazasKontextusFunctions.Kontextus.RogzitoIntezetId,
                                TipusCimkeId = (int)FegyelmiNaploTipus.II_fokuTargyalasiJegyzokonyv,
                                TovabbiJelenlevok = model.TovabbiSzemelyekList == null ? null : string.Join(", ", model.TovabbiSzemelyekList)
                            };
                            KonasoftBVFonixContext.Naplo.Add(entity);
                            KonasoftBVFonixContext.SaveChanges();

                            var fegyelmiUgy = FindById(fegyelmiUgyId);
                            fegyelmiUgy.HatarozatotHozottSzemelyId = entity.DonteshozoSzemelyId;
                            Modify(fegyelmiUgy);

                            naploIds.Add(entity.Id);
                            megvaltozottFegyelmi.Add(fegyelmiUgyId);

                        }
                    }
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    Log.Error($"Hiba a másodfokú tárgyalási jegyzőkönyv mentés során (fegyelmiUgyId: {currentId})", e);
                    transaction.Rollback();
                    throw;
                }

            }
            OnFegyelmiUgyValtozas?.Invoke(ujFegyelmi, megvaltozottFegyelmi, toroltFegyelmi);
            return naploIds;
        }


        public SzakteruletiVelemenyFelkeresModel GetSzakteruletiVelemenyKereseModalData(List<int> fegyelmiUgyIds, List<int> naplobejegyzesIds)
        {
            SzakteruletiVelemenyFelkeresModel model = new SzakteruletiVelemenyFelkeresModel();
            model.FegyelmiUgyIds = fegyelmiUgyIds;
            model.NaplobejegyzesIds = naplobejegyzesIds;
            model.MaxHatarido = DateTime.Now.AddDays(3);
            model.Hatarido = DateTime.Now.AddDays(3);
            int esemenyId = 0;
            foreach (var ugyId in fegyelmiUgyIds)
            {
                var fegyelmiUgy = (FegyelmiUgyViewModel)Table.Single(x => x.Id == ugyId);
                if (esemenyId != 0 && fegyelmiUgy.EsemenyId != esemenyId)
                {
                    throw new WarningException("Az összevont véleménykérésnél az összes ügynek azonos esememényhez kell tartoznia.");
                }
                esemenyId = fegyelmiUgy.EsemenyId;

                var hatarido = GetFegyelmiHatarido(ugyId, false);
                if (model.MaxHatarido > hatarido)
                    model.MaxHatarido = hatarido ?? model.MaxHatarido;
            }
            if (model.MaxHatarido < model.Hatarido)
                model.Hatarido = model.MaxHatarido;

            model.CimzettSzakteruletiVezetok = new List<string>();
            model.SzakteruletiVezetokdefaultValue = new List<KSelect2ItemModel>();


            return model;
        }

        public List<int> SaveSzakteruletiVelemenyKereseModalData(SzakteruletiVelemenyFelkeresModel model)
        {
            var aktIntezet = JogosultsagCacheFunctions.AktualisIntezet.Id;
            List<int> naploIds = new List<int>();

            List<int> ujFegyelmi = new List<int>();
            List<int> megvaltozottFegyelmi = new List<int>();
            List<int> toroltFegyelmi = new List<int>();

            Dictionary<string, string> parameterek = new Dictionary<string, string>();

            using (DbContextTransaction transaction = KonasoftBVFonixContext.Database.BeginTransaction())
            {
                var currentId = 0;
                try
                {
                    if (model.NaplobejegyzesIds.IsNullOrEmpty())
                    {
                        var cimzettLista = SzemelyzetFunctions.GetFegyelmiSzakteruletiVezetok("").Where(x => model.CimzettSzakteruletiVezetok.Contains(x.Sid));
                        List<string> cimzettekSzovegesenLista = new List<string>();
                        List<string> cimzettekBeosztassalLista = new List<string>();
                        foreach (var item in cimzettLista)
                        {
                            cimzettekSzovegesenLista.Add($"{item.Displayname.ToTitleCase()}<{item.Email}>");
                            cimzettekBeosztassalLista.Add($"{item.Displayname.ToTitleCase()} {item.Position}".TrimEnd());
                        }
                        var cimzettekSzovegesen = string.Join(";", cimzettekSzovegesenLista);
                        var cimzettekBeosztassal = string.Join(", ", cimzettekBeosztassalLista);

                        var generaltLink = "";

                        SzakteruletiVelemenyKeresekViewModel keres = new SzakteruletiVelemenyKeresekViewModel()
                        {
                            CimzettLista = cimzettekSzovegesen,
                            FegyelmiUgyIdLista = string.Join(",", model.FegyelmiUgyIds),
                            HatarIdo = model.Hatarido,
                            Tema = model.Leiras,
                            SzakvelemenyKeroje = AlkalmazasKontextusFunctions.Kontextus.SzemelyzetSid,
                            GeneraltLink = generaltLink
                        };

                        int felkeresId = SzakteruletiVelemenyKeresekFunctions.Create(keres);

                        keres = SzakteruletiVelemenyKeresekFunctions.FindById(felkeresId);

                        var alkalmazasUrl = ConfigurationManager.AppSettings["AlkalmazasUrl"];

                        keres.GeneraltLink = alkalmazasUrl + $"/#/FegyelmiUgyek/?velemenykeresid={felkeresId}";

                        SzakteruletiVelemenyKeresekFunctions.Modify(keres);

                        int esemenyId = 0;
                        List<int> kivizsgaloSzemelyLista = new List<int>();
                        List<int> eljarasAlaVontLista = new List<int>();
                        List<string> fegyelmiugyszamokLista = new List<string>();
                        foreach (var fegyelmiUgyId in model.FegyelmiUgyIds)
                        {
                            currentId = fegyelmiUgyId;
                            var fegyelmiUgy = (FegyelmiUgyViewModel)Table.Single(x => x.Id == fegyelmiUgyId);
                            if (esemenyId != 0 && fegyelmiUgy.EsemenyId != esemenyId)
                            {
                                throw new WarningException("Az összevont véleménykérésnél az összes ügynek azonos esememényhez kell tartoznia.");
                            }
                            fegyelmiUgy.SzakteruletiVelemenyreVarFL = true;
                            Modify(fegyelmiUgy);
                            esemenyId = fegyelmiUgy.EsemenyId;
                            if (fegyelmiUgy.KivizsgaloSzemelyId != null && !kivizsgaloSzemelyLista.Contains(fegyelmiUgy.KivizsgaloSzemelyId ?? 0))
                                kivizsgaloSzemelyLista.Add(fegyelmiUgy.KivizsgaloSzemelyId ?? 0);
                            if (!eljarasAlaVontLista.Contains(fegyelmiUgy.FogvatartottId))
                                eljarasAlaVontLista.Add(fegyelmiUgy.FogvatartottId);
                            fegyelmiugyszamokLista.Add($"{fegyelmiUgy.UgySorszamaIntezetAzon}/{fegyelmiUgy.UgySorszamaEv}/{fegyelmiUgy.UgySorszama}");

                            var naploEnitity = new Naplo
                            {
                                FegyelmiUgyId = fegyelmiUgyId,
                                FogvatartottId = fegyelmiUgy.FogvatartottId,
                                JegyzokonyvTartalma = model.Leiras,
                                RogzitoIntezetId = aktIntezet,
                                TipusCimkeId = (int)FegyelmiNaploTipus.SzakteruletiVelemenyKerese,
                                TovabbiJelenlevok = cimzettekBeosztassal,
                                Hatarido = model.Hatarido
                            };

                            KonasoftBVFonixContext.Naplo.Add(naploEnitity);

                            KonasoftBVFonixContext.SaveChanges();

                            naploIds.Add(naploEnitity.Id);
                            megvaltozottFegyelmi.Add(fegyelmiUgyId);

                        }

                        List<string> kivizsgaloSzemelyek = new List<string>();
                        foreach (var item in kivizsgaloSzemelyLista)
                        {
                            var szemely = KonasoftBVFonixContext.Szemelyzet.FirstOrDefault(x => x.Id == item);
                            if (szemely != null)
                            {
                                kivizsgaloSzemelyek.Add(szemely.Nev.ToTitleCase() + (szemely.RendfokozatKszId == 0 ? "" : " " + szemely.Rendfokozat));
                            }
                        }
                        parameterek.Add("@kivizsgalo", string.Join(", ", kivizsgaloSzemelyek));

                        List<string> eljarasAlaVontak = new List<string>();
                        foreach (var item in eljarasAlaVontLista)
                        {
                            var szemely = KonasoftBVFonixContext.FogvatartottakNezet.FirstOrDefault(x => x.Id == item);
                            if (szemely != null)
                            {
                                eljarasAlaVontak.Add(szemely.NyilvantartasiAzonosito + " " + szemely.TeljesNev.ToTitleCase());
                            }
                        }
                        parameterek.Add("@eljarasAlaVont", string.Join(", ", eljarasAlaVontak));

                        var esemeny = KonasoftBVFonixContext.Esemenyek.FirstOrDefault(x => x.Id == esemenyId);
                        parameterek.Add("@esemenyIdeje", esemeny.EsemenyDatuma.ToString("yyyy. MM. dd. HH:mm"));
                        parameterek.Add("@esemenyLeirasa", esemeny.Leiras);

                        parameterek.Add("@fegyelmiUgySzam", string.Join(", ", fegyelmiugyszamokLista));
                        string vetseg = KonasoftBVFonixContext.Cimkek.FirstOrDefault(x => x.Id == esemeny.JellegCimkeId)?.Nev;
                        parameterek.Add("@fegyelmiVetseg", string.Join(", ", vetseg));

                        parameterek.Add("@Tema", model.Leiras);
                        parameterek.Add("@hatarido", model.Hatarido.ToString("yyyy. MM. dd."));
                        parameterek.Add("@BSRLink", keres.GeneraltLink);
                        parameterek.Add("@fegyelmiLink", alkalmazasUrl + "/#/");

                        //FegyelmiEmailFunctions.FelkeresSzakteruletiVelemenyreEmail(parameterek, "zszeli@konasoft.hu");
                        //FegyelmiEmailFunctions.FelkeresSzakteruletiVelemenyreEmail(parameterek, keres.CimzettLista);
                    }
                    else
                    {
                        throw new WarningException("A szakerületi vélemény kérése nem módosítható");
                    }

                    transaction.Commit();
                }
                catch (Exception e)
                {
                    Log.Error($"Hiba a szakterületi vélemény kérése mentés során (fegyelmiUgyId: {currentId})", e);
                    transaction.Rollback();

                    throw;
                }
            }



            OnFegyelmiUgyValtozas?.Invoke(ujFegyelmi, megvaltozottFegyelmi, toroltFegyelmi);
            return naploIds;
        }

        public SzembesitesiJegyzokonyvModel GetSzembesitesiJegyzokonyv(List<int> fegyelmiUgyIds, List<int> naplobejegyzesIds)
        {
            var fegyelmiUgyId = fegyelmiUgyIds.First();

            SzembesitesiJegyzokonyvModel model;
            var fegyelmiUgy = FindById(fegyelmiUgyId);
            model = new SzembesitesiJegyzokonyvModel()
            {
                FegyelmiUgyIds = fegyelmiUgyIds,
                JegyzokonyvVezetoSid = null,
                MeghallgatoSid = WindowsIdentity.GetCurrent().User.Value
            };
            model.AlairtaFl = false;
            // selectek feltöltése
            model.SzembesitettFogvatartottOptions = (from resztvevo in KonasoftBVFonixContext.EsemenyResztvevok
                                                     join foka in KonasoftBVFonixContext.Cimkek on resztvevo.ErintettsegFokaCimkeId equals foka.Id
                                                     join fogv in KonasoftBVFonixContext.Fogvatartottak on resztvevo.FogvatartottId equals fogv.Id
                                                     join szemely in KonasoftBVFonixContext.FogvatartottSzemelyesAdatai on fogv.Id equals szemely.FogvatartottId
                                                     join fegyelmi in KonasoftBVFonixContext.FegyelmiUgyek on
                                                     new { resztvevo.EsemenyId, FogvatartottId = (int)resztvevo.FogvatartottId } equals new { fegyelmi.EsemenyId, fegyelmi.FogvatartottId } into fegyelmiL
                                                     from fegyelmiLeft in fegyelmiL.DefaultIfEmpty()
                                                     where resztvevo.EsemenyId == fegyelmiUgy.EsemenyId
                                                     select new KSelect2ItemModel()
                                                     {
                                                         id = resztvevo.Id.ToString(),
                                                         text = (resztvevo.FogvatartottId == fegyelmiUgy.FogvatartottId ? "Eljárás alá vont" : "Tanú") + " – " + fogv.NyilvantartasiAzonosito + " " + szemely.SzuletesiCsaladiNev_NE_HASZNALD + " " + szemely.SzuletesiUtonev_NE_HASZNALD
                                                     }).DistinctBy(d => d.id).ToList();

            if (model.SzembesitettFogvatartottOptions.Count < 2)
            {
                throw new WarningException("Szembesítéshez legalább egy tanú vagy egy sértett szükséges!");
            }
            var intezetiUsers = SzemelyzetFunctions.GetIntezetiAlkalmazottak();
            var fegyelmiUsers = SzemelyzetFunctions.GetFegyelmiAlkalmazottak();

            model.TovabbiSzemelyekOptions = intezetiUsers.Select(x => new KSelect2ItemModel() { id = x.Displayname + (x.Rendfokozat == null ? "" : (" " + x.Rendfokozat)), text = x.Displayname + (x.Rendfokozat == null ? "" : (" " + x.Rendfokozat)) }).ToList();
            model.MeghallgatoOptions = fegyelmiUsers.Select(x => new KSelect2ItemModel() { id = x.Sid, text = x.Displayname + (x.Rendfokozat == null ? "" : (" " + x.Rendfokozat)) }).ToList();
            model.JegyzokonyvVezetoOptions = fegyelmiUsers.Select(x => new KSelect2ItemModel() { id = x.Sid, text = x.Displayname + (x.Rendfokozat == null ? "" : (" " + x.Rendfokozat)) }).ToList();
            if (!naplobejegyzesIds.IsNullOrEmpty())
            {
                var naplobejegyzesId = naplobejegyzesIds.First();
                var naploBejegyzes = NaploFunctions.FindById(naplobejegyzesId);
                var szembesitettFogvatartott1EsemenyResztvevoId = KonasoftBVFonixContext.EsemenyResztvevok.Where(w => w.FogvatartottId == naploBejegyzes.Szembesitett1FogvatartottId && w.EsemenyId == fegyelmiUgy.EsemenyId).Select(s => s.Id).FirstOrDefault();
                var szembesitettFogvatartott2EsemenyResztvevoId = KonasoftBVFonixContext.EsemenyResztvevok.Where(w => w.FogvatartottId == naploBejegyzes.Szembesitett2FogvatartottId && w.EsemenyId == fegyelmiUgy.EsemenyId).Select(s => s.Id).FirstOrDefault();
                List<int> szembesitettFogvatartottakEsemenyResztvevoIds = new List<int>();
                szembesitettFogvatartottakEsemenyResztvevoIds.Add(szembesitettFogvatartott1EsemenyResztvevoId);
                szembesitettFogvatartottakEsemenyResztvevoIds.Add(szembesitettFogvatartott2EsemenyResztvevoId);
                if (naploBejegyzes.JegyzokonyvVezetoSzemelyId.HasValue)
                {
                    var jegyzokonyvVezeto = SzemelyzetFunctions.FindById(naploBejegyzes.JegyzokonyvVezetoSzemelyId.Value);
                    model.JegyzokonyvVezetoSid = jegyzokonyvVezeto.AdSid;
                }
                if (naploBejegyzes.MeghallgatoSzemelyId.HasValue)
                {
                    var meghallgatoSzemely = SzemelyzetFunctions.FindById(naploBejegyzes.MeghallgatoSzemelyId.Value);
                    model.MeghallgatoSid = meghallgatoSzemely.AdSid;
                }
                model.NaplobejegyzesIds = naplobejegyzesIds;
                model.SzembesitettFogvatartottIds = szembesitettFogvatartottakEsemenyResztvevoIds;
                model.Leiras = naploBejegyzes.JegyzokonyvTartalma;

                if (string.IsNullOrWhiteSpace(naploBejegyzes.TovabbiJelenlevok))
                    model.TovabbiSzemelyekList = new List<string>();
                else
                    model.TovabbiSzemelyekList = naploBejegyzes.TovabbiJelenlevok.Split(new string[] { ", " }, StringSplitOptions.None).Where(w => !string.IsNullOrWhiteSpace(w)).ToList();

                var hianyzoSzemelyek = model.TovabbiSzemelyekList.Where(w => !model.TovabbiSzemelyekOptions.Any(a => a.id == w)).ToList();
                model.TovabbiSzemelyekOptions.AddRange(hianyzoSzemelyek.Select(x => new KSelect2ItemModel() { id = x, text = x }));
            }

            return model;
        }

        public List<int> SaveSzembesitesiJegyzokonyv(SzembesitesiJegyzokonyvModel model)
        {
            var aktIntezet = JogosultsagCacheFunctions.AktualisIntezet.Id;
            List<int> naploIds = new List<int>();

            List<int> ujFegyelmi = new List<int>();
            List<int> megvaltozottFegyelmi = new List<int>();
            List<int> toroltFegyelmi = new List<int>();
            using (DbContextTransaction transaction = KonasoftBVFonixContext.Database.BeginTransaction())
            {
                var currentId = 0;
                try
                {

                    if (!model.NaplobejegyzesIds.IsNullOrEmpty())
                    {
                        foreach (var naplobejegyzesId in model.NaplobejegyzesIds)
                        {
                            var naploBejegyzes = NaploFunctions.FindById(naplobejegyzesId);
                            currentId = naploBejegyzes.FegyelmiUgyId;
                            naploBejegyzes.JegyzokonyvTartalma = model.Leiras;
                            int? jegyzokonyvVezetoSid = null;
                            var esemenyResztvevoId1 = model.SzembesitettFogvatartottIds[0];
                            var esemenyResztvevoId2 = model.SzembesitettFogvatartottIds[1];
                            var szembesitettFogvatarott1 = KonasoftBVFonixContext.EsemenyResztvevok.Single(x => x.Id == esemenyResztvevoId1).FogvatartottId;
                            var szembesitettFogvatarott2 = KonasoftBVFonixContext.EsemenyResztvevok.Single(x => x.Id == esemenyResztvevoId2).FogvatartottId;
                            if (model.JegyzokonyvVezetoSid != null)
                            {
                                jegyzokonyvVezetoSid = SzemelyzetFunctions.SzemelyzetLekeresVagyLetrehozas(model.JegyzokonyvVezetoSid, null, null).Id;
                            }
                            naploBejegyzes.Szembesitett1FogvatartottId = szembesitettFogvatarott1;
                            naploBejegyzes.Szembesitett2FogvatartottId = szembesitettFogvatarott2;
                            naploBejegyzes.JegyzokonyvVezetoSzemelyId = jegyzokonyvVezetoSid;
                            naploBejegyzes.MeghallgatoSzemelyId = SzemelyzetFunctions.SzemelyzetLekeresVagyLetrehozas(model.MeghallgatoSid, null, null).Id;
                            naploBejegyzes.KihallgatasIntezetId = AlkalmazasKontextusFunctions.Kontextus.RogzitoIntezetId;
                            naploBejegyzes.TovabbiJelenlevok = model.TovabbiSzemelyekList == null ? null : string.Join(", ", model.TovabbiSzemelyekList);
                            naploBejegyzes.AlairtaFl = model.AlairtaFl;
                            NaploFunctions.Modify(naploBejegyzes);
                            KonasoftBVFonixContext.SaveChanges();
                            naploIds.Add(naploBejegyzes.Id);
                            megvaltozottFegyelmi.Add(naploBejegyzes.FegyelmiUgyId);
                        }
                    }
                    else
                    {
                        foreach (var fegyelmiUgyId in model.FegyelmiUgyIds)
                        {
                            var fegyelmiUgy = FindById(fegyelmiUgyId);
                            currentId = fegyelmiUgyId;
                            var esemenyResztvevoId1 = model.SzembesitettFogvatartottIds[0];
                            var esemenyResztvevoId2 = model.SzembesitettFogvatartottIds[1];
                            var szembesitettFogvatarott1 = KonasoftBVFonixContext.EsemenyResztvevok.Single(x => x.Id == esemenyResztvevoId1).FogvatartottId;
                            var szembesitettFogvatarott2 = KonasoftBVFonixContext.EsemenyResztvevok.Single(x => x.Id == esemenyResztvevoId2).FogvatartottId;
                            var tipus = (int)FegyelmiNaploTipus.SzembesitesiJegyzokonyv;

                            int? jegyzokonyvVezetoSid = null;
                            if (model.JegyzokonyvVezetoSid != null)
                            {
                                jegyzokonyvVezetoSid = SzemelyzetFunctions.SzemelyzetLekeresVagyLetrehozas(model.JegyzokonyvVezetoSid, null, null).Id;
                            }
                            var entity = new Naplo()
                            {
                                FegyelmiUgyId = fegyelmiUgyId,
                                FogvatartottId = fegyelmiUgy.FogvatartottId,
                                JegyzokonyvTartalma = model.Leiras,
                                JegyzokonyvVezetoSzemelyId = jegyzokonyvVezetoSid,
                                MeghallgatoSzemelyId = SzemelyzetFunctions.SzemelyzetLekeresVagyLetrehozas(model.MeghallgatoSid, null, null).Id,
                                KihallgatasIntezetId = AlkalmazasKontextusFunctions.Kontextus.RogzitoIntezetId,
                                TipusCimkeId = tipus,
                                TovabbiJelenlevok = model.TovabbiSzemelyekList == null ? null : string.Join(", ", model.TovabbiSzemelyekList),
                                AlairtaFl = model.AlairtaFl,
                                Szembesitett1FogvatartottId = szembesitettFogvatarott1.Value,
                                Szembesitett2FogvatartottId = szembesitettFogvatarott2.Value,
                            };
                            KonasoftBVFonixContext.Naplo.Add(entity);
                            KonasoftBVFonixContext.SaveChanges();
                            naploIds.Add(entity.Id);


                            megvaltozottFegyelmi.Add(fegyelmiUgyId);

                        }
                    }
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    Log.Error($"Hiba a szembesítési jegyzőkönyv mentés során (fegyelmiUgyId: {currentId})", e);
                    transaction.Rollback();
                    throw;
                }


            }
            OnFegyelmiUgyValtozas?.Invoke(ujFegyelmi, megvaltozottFegyelmi, toroltFegyelmi);
            return naploIds;
        }


        public void JogiKepviseletVisszavonasa(List<int> fegyelmiUgyIds)
        {
            var aktIntezet = JogosultsagCacheFunctions.AktualisIntezet.Id;
            List<int> ujFegyelmi = new List<int>(0);
            List<int> megvaltozottFegyelmi = new List<int>();
            List<int> toroltFegyelmi = new List<int>(0);
            using (DbContextTransaction transaction = KonasoftBVFonixContext.Database.BeginTransaction())
            {
                int currentId = 0;
                try
                {
                    foreach (var fegyelmiUgyId in fegyelmiUgyIds)
                    {
                        currentId = fegyelmiUgyId;
                        var fegyelmiUgy = (FegyelmiUgyViewModel)Table.Single(x => x.Id == fegyelmiUgyId);
                        fegyelmiUgy.VanJogiKepviselet = false;
                        fegyelmiUgy.JogiKepviseletetKer = false;
                        Modify(fegyelmiUgy);

                        var naploEnitity = new Naplo
                        {
                            FegyelmiUgyId = fegyelmiUgyId,
                            FogvatartottId = fegyelmiUgy.FogvatartottId,
                            RogzitoIntezetId = aktIntezet,
                            TipusCimkeId = (int)FegyelmiNaploTipus.JogiKepviseletVisszavonasa,
                        };
                        KonasoftBVFonixContext.Naplo.Add(naploEnitity);

                    }
                    KonasoftBVFonixContext.SaveChanges();

                    transaction.Commit();


                    megvaltozottFegyelmi.AddRange(fegyelmiUgyIds);
                }
                catch (Exception e)
                {
                    Log.Error($"Hiba a JogiKepviseletVisszavonasa mentés során (fegyelmiUgyId: {currentId})", e);
                    transaction.Rollback();
                    throw;
                }
            }

            OnFegyelmiUgyValtozas?.Invoke(ujFegyelmi, megvaltozottFegyelmi, toroltFegyelmi);
        }


        public void UpdateFenyitesVegrahajtvaTipusuUgyek(object o)
        {
            try
            {
                KonasoftBVFonixContext.Database.CommandTimeout = int.MaxValue;
                KonasoftBVFonixContext.Database.SqlQuery<int>
                         ("Fegyelmi.FenyitesStatuszUpdate").SingleOrDefault();
            }
            catch (Exception e)
            {
                Log.Error($"Hiba a(z) FenyitesKiszabvaTipusuUgyekVegrehajtasa függvény hívásakor", e);
                throw;
            }
        }


        public void UpdateSzabadultFogvatartottUgyek(object o)
        {
            KonasoftBVFonixContext.Configuration.LazyLoadingEnabled = false;

            List<int> nemIntezetebenNyilvantartasiStatuszIdk = new List<int>{ 2777, 2797, 2798, 2799, 2800, 2801, 2802, 2803, 2804, 2805, 2806, 2807, 2808, 2809, 2810, 2811, 2812, 2813, 2814, 2815, 2816, 2817, 2818,
            2819, 2820, 2821, 2822, 2823, 2824, 2825, 2826, 2827, 2828, 2829, 2830, 2831, 2832, 2833, 2834, 2835, 2836, 2837, 2838, 3186, 4823, 1002635, 1002652};

            List<int> ujFegyelmi = new List<int>(0);
            List<int> megvaltozottFegyelmi = new List<int>();
            List<int> toroltFegyelmi = new List<int>(0);
            try
            {
                KonasoftBVFonixContext.Database.CommandTimeout = int.MaxValue;
                // összeszedjük a fegyelmiugyek emberkéit
                var fegyelmiUgyek = KonasoftBVFonixContext
                    .FegyelmiUgyek
                    .Include(i => i.FogvatartottFonix)
                    .Where(x => nemIntezetebenNyilvantartasiStatuszIdk.Contains(x.FogvatartottFonix.NyilvantartasiStatuszKszId) && x.Lezarva != true).ToList();
                List<int> fegyelmiUgyIds = fegyelmiUgyek.Select(x => x.Id).ToList();
                megvaltozottFegyelmi.AddRange(fegyelmiUgyIds);
                foreach (var fegyelmiUgy in fegyelmiUgyek)
                {
                    if (fegyelmiUgy.ElkulonitveFl == true)
                    {
                        fegyelmiUgy.ElkulonitveFl = false;

                        // Főnix3 elkülönítés megszüntetés
                        //new F3ElkulonitesekFunctions().ElkulonitesMegszuntetesByFegyelmiId(fegyelmiUgy.Id);
                    }

                    fegyelmiUgy.Lezarva = true;
                    fegyelmiUgy.Hatarido = null;
                    fegyelmiUgy.StatuszCimkeId = (int)FegyelmiUgyStatusz.AutomatikusanLezart;

                    OsszevontFegyelmiUgyekLezarasaByFegyelmiUgyId(fegyelmiUgy.Id);

                    var naploEnitity = new Naplo
                    {
                        FegyelmiUgyId = fegyelmiUgy.Id,
                        FogvatartottId = fegyelmiUgy.FogvatartottId,
                        TipusCimkeId = (int)FegyelmiNaploTipus.AutomatikusLezaras,
                        AutomatikusFl = true
                    };

                    KonasoftBVFonixContext.Naplo.Add(naploEnitity);
                }

                KonasoftBVFonixContext.SaveChanges();
            }
            catch (Exception e)
            {
                Log.Error($"Hiba a(z) UpdateSzabadultFogvatartottUgyek függvény hívásakor", e);
                throw;
            }
            OnFegyelmiUgyValtozas?.Invoke(ujFegyelmi, megvaltozottFegyelmi, toroltFegyelmi);
        }

        public FegyelmiUgyHatarozatRogziteseMasodfokonModel GetHatarozatRogziteseMasodfokon(List<int> fegyelmiUgyIds, List<int> naplobejegyzesIds)
        {
            FegyelmiUgyHatarozatRogziteseMasodfokonModel model;
            var fegyelmiUgyId = fegyelmiUgyIds.First();
            var fegyelmiUgy = FindById(fegyelmiUgyId);
            var masodfokonHatarozatotHozoParancsnok = SzemelyzetFunctions.SzemelyzetLekeresVagyLetrehozas(WindowsIdentity.GetCurrent().User.Value, null, null).Nev;
            NaploViewModel naploBejegyzes = new NaploViewModel();

            if (!naplobejegyzesIds.IsNullOrEmpty())
            {
                var naplobejegyzesId = naplobejegyzesIds.First();
                naploBejegyzes = NaploFunctions.FindById(naplobejegyzesId);
            }


            model = new FegyelmiUgyHatarozatRogziteseMasodfokonModel()
            {
                FegyelmiUgyIds = fegyelmiUgyIds,
                NaplobejegyzesIds = naplobejegyzesIds,
                HatarozatHozoJogkoreCimkeId = fegyelmiUgy.HatarozatHozoJogkoreCimkeId,
                MasodfokonHatarozatotHozoParancsnok = masodfokonHatarozatotHozoParancsnok,
                HatarozatDatuma = DateTime.Now,
                FenyitesTartamaKietkezesCsokkentesOptions = new List<KSelect2ItemModel>(),
                FenyitesTartamaMaganElzarasOptions = new List<KSelect2ItemModel>(),
                FenyitesTartamaMaganalTarthatoTargyakKorlatozasaOptions = new List<KSelect2ItemModel>(),
                FenyitesTartamaProgaromokonValoResztvetelKorlatozasaOptions = new List<KSelect2ItemModel>(),
                FenyitesTartamaProgaromokonValoResztvetelTiltasaOptions = new List<KSelect2ItemModel>(),
                FenyitesTartamaTobbletSzolgaltatasokMegvonasaOptions = new List<KSelect2ItemModel>(),
                FenyitesTartamaKimaradasMegvonasaOptions = new List<KSelect2ItemModel>(),
                TorvenyszekOptions = new List<KSelect2ItemModel>(),
                ElsofokuTargyalasIdopontja = fegyelmiUgy.ElsofokuTargyalasIdopontja,
                IsKarterites = fegyelmiUgy.KarteritesId != null
            };

            var fegyelmiVetsegTipusok = CimkeFunctions.GetFegyelmiVetsegTipusok();

            var vetsegRendeletSzerint = CimkeFunctions.GetFegyelmiVetsegRendeletSzerint();
            var fenyitesTipusok = CimkeFunctions.GetFegyelmiFenyitesTipusok().Where(w => w.Id != 298).ToList();
            if (fegyelmiUgy.FenyitesTipusCimkeId != FenyitesTipusok.Maganelzaras.CastToInt())
                fenyitesTipusok.RemoveAll(r => r.Id == FenyitesTipusok.Maganelzaras.CastToInt());
            var hatalyonKivulHelyezesTipusai = CimkeFunctions.GetFegyelmiFenyitesHatalyonKivulHelyezesOkai();

            model.FegyelmiVetsegTipusaOptions = fegyelmiVetsegTipusok.Select(x => new KSelect2ItemModel() { id = x.Id.ToString(), text = x.Nev }).ToList();
            model.VetsegRendeletSzerintOptions = vetsegRendeletSzerint.Select(x => new KSelect2ItemModel() { id = x.Id.ToString(), text = x.Nev }).ToList();
            model.FenyitesHatalyonKivulHelyezesOkaOptions = hatalyonKivulHelyezesTipusai.Select(x => new KSelect2ItemModel() { id = x.Id.ToString(), text = x.Nev }).ToList();
            model.FenyitesTipusaOptions = fenyitesTipusok.Select(x => new KSelect2ItemModel() { id = x.Id.ToString(), text = x.Nev }).ToList();
            if (!IsFogvatartottVegrehajtasiFokaElzaras(fegyelmiUgy.FogvatartottId))
                model.FenyitesTipusaOptions.RemoveAll(r => r.id == FenyitesTipusok.KimaradasMegvonas.CastToInt().ToString());

            var elsofokonKivalasztottMaganelzarasTartama = Int32.MaxValue;
            if (fegyelmiUgy.FenyitesTipusCimkeId == FenyitesTipusok.Maganelzaras.CastToInt() && fegyelmiUgy.FenyitesHossza.HasValue)
                elsofokonKivalasztottMaganelzarasTartama = fegyelmiUgy.FenyitesHossza.Value;
            var maganelzarasMaxTartama = Math.Min(GetMaxMaganelzarasTartama(fegyelmiUgy.FogvatartottId), elsofokonKivalasztottMaganelzarasTartama);

            var fenyitesHosszanakMennyisegiEgysegei = CimkeFunctions.GetFegyelmiFenyitesHosszanakMennyisegiEgysegei();
            var alkalom = fenyitesHosszanakMennyisegiEgysegei.Single(s => s.Id == MennyisegiEgysegek.Alkalom.CastToInt()).Nev.ToLower();
            var nap = fenyitesHosszanakMennyisegiEgysegei.Single(s => s.Id == MennyisegiEgysegek.Nap.CastToInt()).Nev.ToLower();
            var honap = fenyitesHosszanakMennyisegiEgysegei.Single(s => s.Id == MennyisegiEgysegek.Honap.CastToInt()).Nev.ToLower();

            Enumerable.Range(1, FenyitesTartama.MaxKietkezesCsokkentese).ToList().ForEach(f => model.FenyitesTartamaKietkezesCsokkentesOptions.Add(new KSelect2ItemModel() { id = $"{f} {honap}", text = $"{f} {honap}" }));
            Enumerable.Range(1, maganelzarasMaxTartama).ToList().ForEach(f => model.FenyitesTartamaMaganElzarasOptions.Add(new KSelect2ItemModel() { id = $"{f} {nap}", text = $"{f} {nap}" }));
            Enumerable.Range(1, FenyitesTartama.MaxMaganalTarthatoTargyakKorlatozasa).ToList().ForEach(f => model.FenyitesTartamaMaganalTarthatoTargyakKorlatozasaOptions.Add(new KSelect2ItemModel() { id = $"{f} {honap}", text = $"{f} {honap}" }));
            Enumerable.Range(1, FenyitesTartama.MaxProgaromokonValoResztvetelKorlatozasa).ToList().ForEach(f => model.FenyitesTartamaProgaromokonValoResztvetelKorlatozasaOptions.Add(new KSelect2ItemModel() { id = $"{f} {honap}", text = $"{f} {honap}" }));
            Enumerable.Range(1, FenyitesTartama.MaxProgaromokonValoResztvetelKorlatozasa).ToList().ForEach(f => model.FenyitesTartamaProgaromokonValoResztvetelKorlatozasaOptions.Add(new KSelect2ItemModel() { id = $"{f} {alkalom}", text = $"{f} {alkalom}" }));
            Enumerable.Range(1, FenyitesTartama.MaxProgaromokonValoResztvetelTiltasa).ToList().ForEach(f => model.FenyitesTartamaProgaromokonValoResztvetelTiltasaOptions.Add(new KSelect2ItemModel() { id = $"{f} {honap}", text = $"{f} {honap}" }));
            Enumerable.Range(1, FenyitesTartama.MaxTobbletSzolgaltatasokMegvonasa).ToList().ForEach(f => model.FenyitesTartamaTobbletSzolgaltatasokMegvonasaOptions.Add(new KSelect2ItemModel() { id = $"{f} {honap}", text = $"{f} {honap}" }));
            Enumerable.Range(1, FenyitesTartama.MaxKimaradasMegvonas).ToList().ForEach(f => model.FenyitesTartamaKimaradasMegvonasaOptions.Add(new KSelect2ItemModel() { id = $"{f} {alkalom}", text = $"{f} {alkalom}" }));

            //var torvenyszekek = KonasoftBVFonixContext.Torvenyszekek.Where(t => t.FeluletenMegjelenitheto).ToList();
            model.Leiras = naploBejegyzes.JegyzokonyvTartalma;
            //torvenyszekek.ForEach(f => model.TorvenyszekOptions.Add(new KSelect2ItemModel() { id = f.Id.ToString(), text = f.TorvenyszekNeve }));
            model.FenyitesTipusaCimkeId = fegyelmiUgy.FenyitesTipusCimkeId.Value;
            model.FenyitesHosszaEsTipusa = $"{fegyelmiUgy.FenyitesHossza} {fenyitesHosszanakMennyisegiEgysegei.SingleOrDefault(s => s.Id == fegyelmiUgy.FenyitesHosszaMennyisegiEgysegCimkeId)?.Nev.ToLower()}";
            model.FegyelmiVetsegTipusaCimkeId = fegyelmiUgy.FegyelmiVetsegTipusaCimkeId;
            model.VetsegRendeletSzerintCimkeId = fegyelmiUgy.VetsegRendeletSzerintCimkeId;
            model.KietkezesCsokkentes = fegyelmiUgy.KietkezesCsokkentes;

            return model;
        }

        public List<KSelect2ItemModel> FindSzakteruletiVezetokForSelect(string term)
        {
            return SzemelyzetFunctions.GetFegyelmiSzakteruletiVezetok(term).Select(x => new KSelect2ItemModel() { id = x.Sid, text = x.Displayname + " " + x.Rendfokozat }).ToList();
        }

        public void SaveMasodFokuHatarozat(FegyelmiUgyHatarozatRogziteseMasodfokonModel model)
        {
            var aktIntezet = JogosultsagCacheFunctions.AktualisIntezet.Id;
            List<int> ujFegyelmi = new List<int>(0);
            List<int> valtozottFegyelmi = new List<int>(0);
            List<int> toroltFegyelmi = new List<int>(0);
            string szandekossagSzoveg = "";

            var fegyelmiUgyId = model.FegyelmiUgyIds.First();
            var fegyelmiUgy = Table.Single(x => x.Id == fegyelmiUgyId);
            var fenyitesHosszanakMennyisegiEgysegei = CimkeFunctions.GetFegyelmiFenyitesHosszanakMennyisegiEgysegei();
            var fenyitesHosszTipusa = fenyitesHosszanakMennyisegiEgysegei.SingleOrDefault(s => s.Nev.ToLower() == model.FenyitesHosszaMennyisegiEgyseg?.ToLower())?.Id;
            using (var transaction = KonasoftBVFonixContext.Database.BeginTransaction())
            {
                try
                {
                    if (model.IsVegleges)
                    {
                        if (model.FenyitesTipusaCimkeId == FenyitesTipusok.Feddes.CastToInt() && model.IsHelybenhagyas)
                        {
                            fegyelmiUgy.StatuszCimkeId = (int)FegyelmiUgyStatusz.FenyitesVegrehajtva;
                            fegyelmiUgy.Lezarva = true;
                            fegyelmiUgy.Hatarido = null;
                            fegyelmiUgy.HatarozatIndoklasa = model.Leiras;
                            fegyelmiUgy.HatarozatJogerosFL = true;
                            fegyelmiUgy.HatarozatotHozottSzemelyId = model.TorvenyszekId.HasValue ? (int?)null : SzemelyzetFunctions.SzemelyzetLekeresVagyLetrehozas(WindowsIdentity.GetCurrent().User.Value, null, null).Id;
                            fegyelmiUgy.HatarozatotHozoTorvenyszekId = model.TorvenyszekId;
                            fegyelmiUgy.HatarozatDatuma = model.HatarozatDatuma;
                            toroltFegyelmi = new List<int>();
                            toroltFegyelmi.Add(fegyelmiUgyId);
                        }
                        else if (model.FenyitesTipusaCimkeId == FenyitesTipusok.Feddes.CastToInt() && !model.IsHelybenhagyas)
                        {
                            fegyelmiUgy.StatuszCimkeId = (int)FegyelmiUgyStatusz.FenyitesVegrehajtva;
                            fegyelmiUgy.Lezarva = true;
                            fegyelmiUgy.Hatarido = null;
                            fegyelmiUgy.FenyitesTipusCimkeId = model.FenyitesTipusaCimkeId > 0 ? model.FenyitesTipusaCimkeId : null;
                            fegyelmiUgy.FenyitesHossza = model.FenyitesHossza;
                            fegyelmiUgy.HatarozatIndoklasa = model.Leiras;
                            fegyelmiUgy.HatarozatJogerosFL = true;
                            fegyelmiUgy.FegyelmiVetsegTipusaCimkeId = model.FegyelmiVetsegTipusaCimkeId;
                            fegyelmiUgy.VetsegRendeletSzerintCimkeId = model.VetsegRendeletSzerintCimkeId;
                            fegyelmiUgy.HatarozatotHozottSzemelyId = model.TorvenyszekId.HasValue ? (int?)null : SzemelyzetFunctions.SzemelyzetLekeresVagyLetrehozas(WindowsIdentity.GetCurrent().User.Value, null, null).Id;
                            fegyelmiUgy.HatarozatotHozoTorvenyszekId = model.TorvenyszekId;
                            fegyelmiUgy.HatarozatDatuma = model.HatarozatDatuma;
                            toroltFegyelmi = new List<int>();
                            toroltFegyelmi.Add(fegyelmiUgyId);
                        }
                        else if (model.FenyitesTipusaCimkeId != FenyitesTipusok.HatalyonKivulHelyezes.CastToInt() && model.IsHelybenhagyas)
                        {
                            fegyelmiUgy.StatuszCimkeId = (int)FegyelmiUgyStatusz.FenyitesKiszabva;
                            fegyelmiUgy.HatarozatIndoklasa = model.Leiras;
                            fegyelmiUgy.HatarozatJogerosFL = true;
                            fegyelmiUgy.HatarozatotHozottSzemelyId = model.TorvenyszekId.HasValue ? (int?)null : SzemelyzetFunctions.SzemelyzetLekeresVagyLetrehozas(WindowsIdentity.GetCurrent().User.Value, null, null).Id;
                            fegyelmiUgy.HatarozatotHozoTorvenyszekId = model.TorvenyszekId;
                            fegyelmiUgy.HatarozatDatuma = model.HatarozatDatuma;
                            fegyelmiUgy.Lezarva = false;
                            fegyelmiUgy.Hatarido = GetFegyelmiHatarido(fegyelmiUgy.Id, false, fegyelmiUgy.StatuszCimkeId);
                            valtozottFegyelmi = new List<int>();
                            valtozottFegyelmi.Add(fegyelmiUgyId);
                        }
                        else if (model.FenyitesTipusaCimkeId != FenyitesTipusok.HatalyonKivulHelyezes.CastToInt() && !model.IsHelybenhagyas)
                        {
                            fegyelmiUgy.FenyitesTipusCimkeId = model.FenyitesTipusaCimkeId > 0 ? model.FenyitesTipusaCimkeId : null;
                            fegyelmiUgy.FenyitesHossza = model.FenyitesHossza;
                            fegyelmiUgy.FenyitesHosszaMennyisegiEgysegCimkeId = fenyitesHosszTipusa;
                            fegyelmiUgy.KietkezesCsokkentes = model.KietkezesCsokkentes;
                            fegyelmiUgy.HatarozatIndoklasa = model.Leiras;
                            fegyelmiUgy.HatarozatJogerosFL = true;
                            fegyelmiUgy.FegyelmiVetsegTipusaCimkeId = model.FegyelmiVetsegTipusaCimkeId;
                            fegyelmiUgy.VetsegRendeletSzerintCimkeId = model.VetsegRendeletSzerintCimkeId;
                            fegyelmiUgy.HatarozatotHozottSzemelyId = model.TorvenyszekId.HasValue ? (int?)null : SzemelyzetFunctions.SzemelyzetLekeresVagyLetrehozas(WindowsIdentity.GetCurrent().User.Value, null, null).Id;
                            fegyelmiUgy.HatarozatotHozoTorvenyszekId = model.TorvenyszekId;
                            fegyelmiUgy.HatarozatDatuma = model.HatarozatDatuma;
                            fegyelmiUgy.Lezarva = false;
                            fegyelmiUgy.Hatarido = GetFegyelmiHatarido(fegyelmiUgy.Id, false, fegyelmiUgy.StatuszCimkeId);
                            fegyelmiUgy.StatuszCimkeId = (int)FegyelmiUgyStatusz.FenyitesKiszabva;
                            valtozottFegyelmi = new List<int>();
                            valtozottFegyelmi.Add(fegyelmiUgyId);
                        }
                        else if (model.FenyitesTipusaCimkeId == (int)FenyitesTipusok.HatalyonKivulHelyezes)
                        {
                            fegyelmiUgy.Lezarva = true;
                            fegyelmiUgy.Hatarido = null;
                            fegyelmiUgy.MegszuntetesOkaCimkeId = model.HatalyonKivulHelyezesOkaCimkeId;
                            fegyelmiUgy.FenyitesTipusCimkeId = null;
                            fegyelmiUgy.FenyitesHossza = null;
                            fegyelmiUgy.FenyitesHosszaMennyisegiEgysegCimkeId = null;
                            fegyelmiUgy.KietkezesCsokkentes = null;
                            fegyelmiUgy.HatarozatIndoklasa = model.Leiras;
                            fegyelmiUgy.HatarozatJogerosFL = true;
                            fegyelmiUgy.FegyelmiVetsegTipusaCimkeId = model.FegyelmiVetsegTipusaCimkeId;
                            fegyelmiUgy.VetsegRendeletSzerintCimkeId = model.VetsegRendeletSzerintCimkeId;
                            fegyelmiUgy.HatarozatotHozottSzemelyId = model.TorvenyszekId.HasValue ? (int?)null : SzemelyzetFunctions.SzemelyzetLekeresVagyLetrehozas(WindowsIdentity.GetCurrent().User.Value, null, null).Id;
                            fegyelmiUgy.HatarozatotHozoTorvenyszekId = model.TorvenyszekId;
                            fegyelmiUgy.HatarozatDatuma = model.HatarozatDatuma;
                            fegyelmiUgy.StatuszCimkeId = (int)FegyelmiUgyStatusz.HatalyonKivulHelyezve;
                            toroltFegyelmi = new List<int>();
                            toroltFegyelmi.Add(fegyelmiUgyId);
                        }
                        else
                        {
                            throw new Exception("Nem definiált másodfokú határozat mentés");
                        }

                        if (model.FenyitesTipusaCimkeId != (int)FenyitesTipusok.HatalyonKivulHelyezes && fegyelmiUgy.KarteritesId != null)
                        {
                            bool szandekosFl = model.IsSzandekosKarokozas ?? false;
                            // BvBankban szándékos károkozás esetén be kell billenteni egyre a fegyelmi_eljaras_szandekos_volt flag-et
                            //IKarteritesekFunctions karteritesekFunctions = new KarteritesekFunctions();
                            //karteritesekFunctions.SetKarteritesSzandekossag(fegyelmiUgy.KarteritesId.Value, szandekosFl);
                            //if (szandekosFl)
                            //    szandekossagSzoveg += "<p><i>Kártérítésre kötelezett.</i></p>";
                            //else
                            //    szandekossagSzoveg += "<p><i>Kártérítésre nem kötelezett.</i></p>";
                        }
                    }

                    if (!model.NaplobejegyzesIds.IsNullOrEmpty())
                    {
                        NaploFunctions.ModifyNaploBejegyzes(model);
                    }
                    else
                    {
                        var naploEnitity = new Naplo
                        {
                            FegyelmiUgyId = fegyelmiUgyId,
                            FogvatartottId = fegyelmiUgy.FogvatartottId,
                            Vegleges = model.FenyitesTipusaCimkeId == (int)FenyitesTipusok.HatalyonKivulHelyezes ? true : false,
                            JegyzokonyvTartalma = $"{model.Leiras}{szandekossagSzoveg}",
                            RogzitoIntezetId = aktIntezet,
                            TipusCimkeId = (int)FegyelmiNaploTipus.HatarozatMasodfokon,
                            DonteshozoSzemelyId = model.TorvenyszekId.HasValue ? (int?)null : SzemelyzetFunctions.SzemelyzetLekeresVagyLetrehozas(WindowsIdentity.GetCurrent().User.Value, null, null).Id,
                            TorvenyszekId = model.TorvenyszekId,
                            FenyitesTipusCimkeId = model.FenyitesTipusaCimkeId > 0 ? model.FenyitesTipusaCimkeId : null,
                            FegyelmiVetsegTipusaCimkeId = model.FegyelmiVetsegTipusaCimkeId,
                            VetsegRendeletSzerintCimkeId = model.VetsegRendeletSzerintCimkeId,
                            KietkezesCsokkentes = model.KietkezesCsokkentes,
                            FenyitesHossza = model.FenyitesTipusaCimkeId == FenyitesTipusok.Feddes.CastToInt() ? null : model.FenyitesHossza,
                            FenyitesHosszaMennyisegiEgysegCimkeId = model.FenyitesTipusaCimkeId == FenyitesTipusok.Feddes.CastToInt() ? null : fenyitesHosszTipusa,
                            MegszuntetesOkaCimkeId = model.HatalyonKivulHelyezesOkaCimkeId,
                            IsHelybenhagyasFl = model.IsHelybenhagyas,
                            AlairtaFl = model.IsVegleges,
                            Hatarido = model.HatarozatDatuma
                        };

                        KonasoftBVFonixContext.Naplo.Add(naploEnitity);
                        KonasoftBVFonixContext.SaveChanges();
                        model.NaplobejegyzesIds = new List<int>();
                        model.NaplobejegyzesIds.Add(naploEnitity.Id);
                    }

                    if (model.IsVegleges && model.HatalyonKivulHelyezesOkaCimkeId == (int)HatalyonKivulHelyezesTipusa.UjEljarasInditasa)
                    {
                        var ujFegyelmiUgyId = FegyelmiUgyInditasa(fegyelmiUgy.EsemenyId, fegyelmiUgy.FogvatartottId);
                        ujFegyelmi = new List<int>();
                        ujFegyelmi.Add(ujFegyelmiUgyId);
                        var ujNaploEnitity = new Naplo
                        {
                            FegyelmiUgyId = ujFegyelmiUgyId,
                            FogvatartottId = fegyelmiUgy.FogvatartottId,
                            Vegleges = false,
                            RogzitoIntezetId = aktIntezet,
                            TipusCimkeId = (int)FegyelmiNaploTipus.UjEljarasLefolytatasa,
                            DonteshozoSzemelyId = SzemelyzetFunctions.SzemelyzetLekeresVagyLetrehozas(WindowsIdentity.GetCurrent().User.Value, null, null).Id,
                            ElozmenyUgyAzonosito = $"{fegyelmiUgy.UgySorszamaIntezetAzon}/{fegyelmiUgy.UgySorszamaEv}/{fegyelmiUgy.UgySorszama}"
                        };

                        KonasoftBVFonixContext.Naplo.Add(ujNaploEnitity);
                    }
                    KonasoftBVFonixContext.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw;
                }
                //emiatt nem szállhat el a fegyelmi
                var fogvatartottRezsimKszId = KonasoftBVFonixContext.Fogvatartottak.Single(s => s.Id == fegyelmiUgy.FogvatartottId).RezsimKszId;

                //if (fogvatartottRezsimKszId != KodszotarEnums.RezsimTipusok.Szigorubb.CastToInt() &&
                //    (model.FenyitesTipusaCimkeId == FenyitesTipusok.Maganelzaras.CastToInt()) ||
                //    (CimkeEnums.BfbElojegyzesFegyelmiVetsegTipusAlapjan.Contains(model.FegyelmiVetsegTipusaCimkeId.Value) && model.FenyitesTipusaCimkeId != FenyitesTipusok.Megszuntetes.CastToInt()))
                //{
                //    // Főnix3 BFB előjegyzés
                //    new F3VegrehajtasiListaFunctions().CreateBFBElojegyzes(
                //        AlkalmazasKontextusFunctions.Kontextus.SzemelyzetSid,
                //        AlkalmazasKontextusFunctions.Kontextus.RogzitoIntezetId,
                //        fegyelmiUgy.FogvatartottId);
                //}
            }

            OnFegyelmiUgyValtozas?.Invoke(ujFegyelmi, valtozottFegyelmi, toroltFegyelmi);
        }

        public FegyelmiNaplobejegyzesFelviteleModel GetUgyesziHatalyonKivulHelyezes(List<int> fegyelmiIds)
        {
            FegyelmiNaplobejegyzesFelviteleModel result = new FegyelmiNaplobejegyzesFelviteleModel()
            {
                NaplobejegyzesIds = new List<int>(),
                Leiras = "",
                FegyelmiUgyIds = fegyelmiIds
            };

            return result;
        }

        public List<int> SaveHatalyonKivulHelyezes(FegyelmiNaplobejegyzesFelviteleModel model)
        {
            List<int> ujFegyelmi = new List<int>();
            List<int> megvaltozottFegyelmi = new List<int>();
            List<int> toroltFegyelmi = new List<int>();
            List<int> naplobejegyzesIds = new List<int>();

            using (DbContextTransaction transaction = KonasoftBVFonixContext.Database.BeginTransaction())
            {
                try
                {
                    foreach (int fegyelmiUgyId in model.FegyelmiUgyIds.Where(id => id > 0))
                    {
                        var fegyelmiUgy = Table.Single(x => x.Id == fegyelmiUgyId);

                        if (fegyelmiUgy.HatarozatJogerosFL == true &&
                            (fegyelmiUgy.StatuszCimkeId == (int)FegyelmiUgyStatusz.FenyitesKiszabva
                            || fegyelmiUgy.StatuszCimkeId == (int)FegyelmiUgyStatusz.FenyitesVegrehajtasMegszakitva
                            || fegyelmiUgy.StatuszCimkeId == (int)FegyelmiUgyStatusz.FenyitesVegrehajtva
                            || fegyelmiUgy.StatuszCimkeId == (int)FegyelmiUgyStatusz.NemHajthatoVegre))
                        {
                            fegyelmiUgy.StatuszCimkeId = (int)FegyelmiUgyStatusz.HatalyonKivulHelyezve;
                            fegyelmiUgy.Lezarva = true;
                            fegyelmiUgy.Hatarido = null;
                            fegyelmiUgy.HatarozatJogerosFL = true;
                            toroltFegyelmi.Add(fegyelmiUgyId);

                            var naploEnitity = new Naplo
                            {
                                FegyelmiUgyId = fegyelmiUgyId,
                                JegyzokonyvTartalma = $"<b>Ügyészi hatályon kívül helyezés:</b><br/>{model.Leiras}",
                                EgyebAdatokJson = AlkalmazasKontextusFunctions.Kontextus.SzemelyzetSid,
                                TipusCimkeId = (int)FegyelmiNaploTipus.SzabadszovegesNaplobejegyzes
                            };
                            KonasoftBVFonixContext.Naplo.Add(naploEnitity);
                            KonasoftBVFonixContext.SaveChanges();
                            naplobejegyzesIds.Add(naploEnitity.Id);
                        }
                    }
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    Log.Error($"{nameof(SaveHatalyonKivulHelyezes)}() hiba", e);
                    transaction.Rollback();

                    throw e;
                }
            }

            OnFegyelmiUgyValtozas?.Invoke(ujFegyelmi, megvaltozottFegyelmi, toroltFegyelmi);

            return naplobejegyzesIds;
        }

        public List<AktivitasFolyamModel> GetAktivitasFolyamList(int? intezetId, List<int> fegyelmiUgyIds = null)
        {
            KonasoftBVFonixContext.Configuration.LazyLoadingEnabled = false;
            KonasoftBVFonixContext.Database.CommandTimeout = int.MaxValue;

            SqlParameter intezetIdParameter;

            if (intezetId == null)
            {
                intezetIdParameter = new SqlParameter("@IntezetId", SqlInt32.Null);
            }
            else
            {
                intezetIdParameter = new SqlParameter("@IntezetId", intezetId);
            }

            DataTable dataTable = new DataTable("IdList");
            dataTable.Columns.Add("Id", typeof(int));
            if (fegyelmiUgyIds != null)
                fegyelmiUgyIds.ForEach(f => dataTable.Rows.Add(f));

            SqlParameter fegyelmiUgyParameter = new SqlParameter();
            fegyelmiUgyParameter.ParameterName = "@fegyelmiUgyIds";
            fegyelmiUgyParameter.SqlDbType = SqlDbType.Structured;
            fegyelmiUgyParameter.Value = dataTable;
            fegyelmiUgyParameter.TypeName = "dbo.IdList";


            return KonasoftBVFonixContext.Database.SqlQuery<AktivitasFolyamModel>
                      ("Fegyelmi.GetAktivitasFolyamList @IntezetId, @fegyelmiUgyIds",
                       intezetIdParameter, fegyelmiUgyParameter
                      ).ToList();
        }

        public MaganelzarasMegkezdesenekRogziteseModel GetMaganelzarasMegkezdesenekRogziteseModalData(List<int> fegyelmiUgyIds, List<int> naplobejegyzesIds)
        {
            if (fegyelmiUgyIds.Count > 1)
                throw new WarningException("Ez a modal nem nyitható meg többes szerkesztésre.");
            if (naplobejegyzesIds != null && naplobejegyzesIds.Count > 1)
                throw new WarningException("Ez a modal nem nyitható meg többes szerkesztésre.");

            MaganelzarasMegkezdesenekRogziteseModel model = new MaganelzarasMegkezdesenekRogziteseModel();

            IEnumerable<IdLabelWithChildren> objektumok = GetElhelyezesek(AlkalmazasKontextusFunctions.Kontextus.RogzitoIntezetId, true, true);
            model.ZarkabaHelyezesOptions = objektumok.ToList();
            model.fegyelmiUgyIds = fegyelmiUgyIds;
            model.naplobejegyzesIds = naplobejegyzesIds;

            if (model.naplobejegyzesIds != null)
            {
                var naplobejegyzesId = model.naplobejegyzesIds.First();
                var naplo = KonasoftBVFonixContext.Naplo.FirstOrDefault(x => x.Id == naplobejegyzesId);
                if (naplo.AlairtaFl ?? false)
                {
                    throw new WarningException("Ez a naplóbejegyzés lezárva, már nem nyitható meg szerkesztésre.");
                }
                model.ZarkabaHelyezes = naplo.EgyebAdatokJson;
                model.BehelyezesTenylegesIdeje = naplo.Hatarido.Value;
            }
            else
            {
                var naploEnitity = KonasoftBVFonixContext.Naplo.OrderByDescending(x => x.LetrehozasDatuma).FirstOrDefault(x => x.FegyelmiUgyId == fegyelmiUgyIds.FirstOrDefault() && !(x.AlairtaFl ?? false));
                if (naploEnitity != null && naploEnitity.TipusCimkeId == (int)(int)FegyelmiNaploTipus.MaganelzarasMegkezdese)
                {
                    model.ZarkabaHelyezes = naploEnitity.EgyebAdatokJson;
                    model.BehelyezesTenylegesIdeje = naploEnitity.Hatarido.Value;
                }
                else
                {
                    model.BehelyezesTenylegesIdeje = DateTime.Now;
                }
            }
            return model;
        }

        private IEnumerable<IdLabelWithChildren> GetElhelyezesek(int intezetId, bool csakUresZarkak, bool csakFegyelmiZarkak = false, int? fogvatartottId = null)
        {

            List<SzabadZarkaModel> list = IntezetiZarkalekerdezes(intezetId, csakUresZarkak, csakFegyelmiZarkak);
            if (fogvatartottId != null)
            {
                //var fogvatartottElhelyezesAdatok = KonasoftBVFonixContext.Fogvatartottak.Where(f => f.Id == fogvatartottId).ToList().Select(s => $"{s.IntezetiObjektumId}_{s.KorletId}_{s.ZarkaId}_{s.ZarkaAgy}").SingleOrDefault();
                var fogvatartott = KonasoftBVFonixContext.Fogvatartottak
                    .Include(i => i.AktualisIntezet)
                    .Include(i => i.IntezetiObjektum)
                    .Include(i => i.Korlet)
                    .Include(i => i.Zarka)
                    .Where(f => f.Id == fogvatartottId && f.IntezetiObjektumId != null && f.KorletId != null && f.ZarkaId != null && f.ZarkaAgy != null).SingleOrDefault();
                if (fogvatartott != null)
                {
                    var zarka = new SzabadZarkaModel()
                    {
                        Agy = fogvatartott.ZarkaAgy.Value,
                        Intezet = fogvatartott.AktualisIntezet.Nev,
                        IntezetId = fogvatartott.AktualisIntezetId,
                        Objektum = fogvatartott.IntezetiObjektum.Nev,
                        ObjektumId = fogvatartott.IntezetiObjektumId.Value,
                        Reszleg = fogvatartott.Korlet.Nev,
                        ReszlegId = fogvatartott.KorletId.Value,
                        Zarka = fogvatartott.Zarka.Azonosito,
                        ZarkaId = fogvatartott.ZarkaId.Value
                    };

                    var isInList = list.Where(a => a.ZarkaId == zarka.ZarkaId).Any(s => s.Agy == zarka.Agy);
                    if (!isInList)
                    {
                        list.Add(zarka);
                    }
                }
            }
            var objektumok =
                list.GroupBy(c => c.ObjektumId)
                .Select(o => new IdLabelWithChildren()
                {
                    id = $"{o.Key}",
                    label = o.Select(n => n.Objektum).First(),
                    children = o.GroupBy(c => c.ReszlegId)
                              .Select(r => new IdLabelWithChildren()
                              {
                                  id = $"{o.Key}_{r.Key}",
                                  label = r.Select(n => n.Reszleg).First(),
                                  children = r.GroupBy(c => c.ZarkaId)
                                  .Select(z => new IdLabelWithChildren()
                                  {
                                      id = $"{o.Key}_{r.Key}_{z.Key}",
                                      label = z.Select(n => n.Zarka).First(),
                                      children = z
                                      .Select(a => new IdLabelWithChildren()
                                      {
                                          id = $"{o.Key}_{r.Key}_{z.Key}_{a.Agy}",
                                          label = $"{o.Select(n => n.Objektum).First()}/{r.Select(n => n.Reszleg).First()}/{z.Select(n => n.Zarka).First()}/{a.Agy}",
                                      }).ToList()
                                  })
                                  .ToList()
                              }).ToList()
                });
            return objektumok;
        }

        private List<SzabadZarkaModel> IntezetiZarkalekerdezes(int intezetId, bool csakUresZarkak, bool csakFegyelmiZarkak)
        {
            Dictionary<string, object> par = new Dictionary<string, object>();
            par.Add("@intezetId", intezetId);
            par.Add("@csakUresZarkak", csakUresZarkak);
            par.Add("@csakFegyelmiZarkak", csakFegyelmiZarkak);
            var list = KonasoftBVFonixContext.RunStoredProcedureByNev<SzabadZarkaModel>("[Fegyelmi].[GetSzabadZarkaAgyak]", par)
                .OrderBy(x => x.Intezet)
                .ThenBy(x => x.Objektum)
                .ThenBy(x => x.Reszleg)
                .ThenBy(x => x.Zarka)
                .ThenBy(x => x.Agy)
                .ToList();
            return list;
        }

        public string GetElhelyezesNevByString(string val)
        {
            string[] elhelyezes = val.Split('_');
            int objektumId = int.Parse(elhelyezes[0]);
            int reszlegId = int.Parse(elhelyezes[1]);
            int zarkaId = int.Parse(elhelyezes[2]);
            int agyId = int.Parse(elhelyezes[3]);
            var zarka = KonasoftBVFonixContext.Zarka
                .Include(x => x.IntezetiObjektum)
                .Include(x => x.Korlet)
                .FirstOrDefault(x => x.Id == zarkaId);
            return $"{zarka.IntezetiObjektum?.Nev}/{zarka.Korlet?.Nev}/{zarka.Azonosito}/{agyId}";
        }

        public List<int> SaveMaganelzarasMegkezdesenekRogziteseModalData(MaganelzarasMegkezdesenekRogziteseModel model)
        {
            var aktIntezet = JogosultsagCacheFunctions.AktualisIntezet.Id;
            List<int> naploIds = new List<int>();
            int fId = 0;

            List<int> ujFegyelmi = new List<int>();
            List<int> megvaltozottFegyelmi = new List<int>();
            List<int> toroltFegyelmi = new List<int>();
            string[] elhelyezes = model.ZarkabaHelyezes.Split('_');

            if (model.fegyelmiUgyIds.Count > 1)
                throw new WarningException("Ez a modal nem nyitható meg többes szerkesztésre");
            if (model.naplobejegyzesIds != null && model.naplobejegyzesIds.Count > 1)
                throw new WarningException("Ez a modal nem nyitható meg többes szerkesztésre");
            if (elhelyezes.Length != 4)
            {
                throw new WarningException("Az elhelyezés azonosítója nem megfelelő.");
            }
            int zarkaId = int.Parse(elhelyezes[2]);
            int agyId = int.Parse(elhelyezes[3]);

            var elhelyezesStr = GetElhelyezesNevByString(model.ZarkabaHelyezes);

            Dictionary<string, string> parameterek = new Dictionary<string, string>();

            using (DbContextTransaction transaction = KonasoftBVFonixContext.Database.BeginTransaction())
            {
                var currentId = 0;
                try
                {
                    var fegyelmiUgyId = model.fegyelmiUgyIds.First();
                    var fegyelmiUgy = (FegyelmiUgyViewModel)Table.Single(x => x.Id == fegyelmiUgyId);

                    if (fegyelmiUgy.StatuszCimkeId != (int)CimkeEnums.FegyelmiUgyStatusz.FenyitesKiszabva && fegyelmiUgy.StatuszCimkeId != (int)CimkeEnums.FegyelmiUgyStatusz.FenyitesVegrehajtasMegszakitva)
                    {
                        Log.Error($"Hiba a magánelzárás megkezdésekor, fenyítés végrehajtása alatt nem kezdeményezhet magánelzárást (fegyelmiUgyId: {currentId})");
                        throw new WarningException("Fenyítés végrehajtása alatt nem kezdeményezhet magánelzárást! Kérjük, csukja be a részletek ablakot és nyissa újra.");
                    }

                    var hatarozatDatuma = fegyelmiUgy.HatarozatDatuma;
                    if (hatarozatDatuma > model.BehelyezesTenylegesIdeje)
                    {
                        Log.Error($"Hiba a magánelzárás megkezdésekor, a magánelzárás megkezdésének időpontja nem lehet korábbi, mint a határozat időpontja (fegyelmiUgyId: {currentId})");
                        throw new WarningException("A magánelzárás megkezdésének időpontja nem lehet korábbi, mint a határozat időpontja.");
                    }

                    fId = fegyelmiUgy.FogvatartottId;
                    var naploEnitity = new Naplo
                    {
                        FegyelmiUgyId = fegyelmiUgyId,
                        FogvatartottId = fegyelmiUgy.FogvatartottId,
                        JegyzokonyvTartalma = elhelyezesStr,
                        EgyebAdatokJson = model.ZarkabaHelyezes,
                        RogzitoIntezetId = aktIntezet,
                        TipusCimkeId = (int)FegyelmiNaploTipus.MaganelzarasMegkezdese,
                        Hatarido = model.BehelyezesTenylegesIdeje,
                        MaganelzarasVegeDatum = model.MaganelzarasVegeDatum,
                    };
                    KonasoftBVFonixContext.Naplo.Add(naploEnitity);
                    naploIds.Add(naploEnitity.Id);

                    var vegrehajtas = new FenyitesVegrehajtasok()
                    {
                        FegyelmiUgyId = fegyelmiUgy.Id,
                        Hossz = 0,
                        MennyisegiEgyegCimkeId = fegyelmiUgy.FenyitesHosszaMennyisegiEgysegCimkeId,
                        KezdeteIdo = model.BehelyezesTenylegesIdeje
                    };
                    KonasoftBVFonixContext.FenyitesVegrehajtasok.Add(vegrehajtas);

                    fegyelmiUgy.MaganelzarasVegeDatum = model.MaganelzarasVegeDatum;
                    fegyelmiUgy.VegrehajtasKezdeteIdo = model.BehelyezesTenylegesIdeje;
                    fegyelmiUgy.StatuszCimkeId = (int)CimkeEnums.FegyelmiUgyStatusz.FenyitesVegrehajtasAlatt;
                    Modify(fegyelmiUgy);

                    KonasoftBVFonixContext.SaveChanges();
                    megvaltozottFegyelmi.Add(fegyelmiUgyId);

                    try
                    {
                        //try
                        //{
                        //    new FegyelmiSzervizClient().VegrehajtMaganelzaras(
                        //        AlkalmazasKontextusFunctions.Kontextus.SzemelyzetSid,
                        //        AlkalmazasKontextusFunctions.Kontextus.RogzitoIntezetId,
                        //        fId,
                        //        zarkaId,
                        //        agyId,
                        //        model.BehelyezesTenylegesIdeje);
                        //}
                        //catch(Exception ex)
                        //{
                        //    Log.Error("FegyelmiSzervizclient().VegrehajtMaganelzaras hívás hiba: ", ex);
                        //}

                        // Főnix3 zárkába helyezés
                        //F3ZarkabaHelyezesFunctions.ZarkabaHelyezes((int)Modul.Fenyites, zarkaId, fegyelmiUgy.FogvatartottId, agyId, model.BehelyezesTenylegesIdeje, fegyelmiUgyId, 42040, model.NocheckVegrehajtasiFok);
                    }
                    //catch (F3ZarkabaHelyezesFunctions.VegrehajtasiFokException)
                    //{
                    //    throw;
                    //}
                    catch (Exception exc)
                    {
                        Log.Error("Zárkábahelyezés Főnix3 hívás", exc);
                        throw new WarningException($"A behelyezés elküldése a Főnix3 rendszerbe sikertelen volt, kérjük próbálja újra. ({exc.Message})");
                    }
                    transaction.Commit();
                }
                //catch (F3ZarkabaHelyezesFunctions.VegrehajtasiFokException)
                //{
                //    transaction.Rollback();
                //    throw;
                //}
                catch (Exception e)
                {
                    Log.Error($"Hiba a behelyezés mentés során (fegyelmiUgyId: {currentId})", e);
                    transaction.Rollback();

                    throw;
                }
            }

            OnFegyelmiUgyValtozas?.Invoke(ujFegyelmi, megvaltozottFegyelmi, toroltFegyelmi);

            return naploIds;
        }

        public Tuple<int?, List<KSelect2ItemModel>> GetSertettOptionsByErintettsegFoka(int esemenyId, List<ErintettsegFoka> erintettsegFokai, ErintettsegFoka defaultOptionErintettsegFoka = 0, bool erintettsegFokaSelectben = false)
        {
            var erintettsegFokaiInt = erintettsegFokai.Select(s => (int)s).ToList();
            var result = (from resztvevo in KonasoftBVFonixContext.EsemenyResztvevok
                          join foka in KonasoftBVFonixContext.Cimkek on resztvevo.ErintettsegFokaCimkeId equals foka.Id
                          join fogv in KonasoftBVFonixContext.Fogvatartottak on resztvevo.FogvatartottId equals fogv.Id
                          join szemely in KonasoftBVFonixContext.FogvatartottSzemelyesAdatai on fogv.Id equals szemely.FogvatartottId
                          join fegyelmi in KonasoftBVFonixContext.FegyelmiUgyek on
                                             new { resztvevo.EsemenyId, FogvatartottId = (int)resztvevo.FogvatartottId } equals new { fegyelmi.EsemenyId, fegyelmi.FogvatartottId } into fegyelmiL
                          from fegyelmiLeft in fegyelmiL.DefaultIfEmpty()
                          where resztvevo.EsemenyId == esemenyId && erintettsegFokaiInt.Contains(resztvevo.ErintettsegFokaCimkeId)
                          select new
                          {
                              ResztvevoId = resztvevo.Id,
                              ErintettsegFoka = foka.Nev,
                              ErintettsegFokaId = foka.Id,
                              FogvNyilvantartasiAzonosito = fogv.NyilvantartasiAzonosito,
                              FogvNev = szemely.SzuletesiCsaladiNev_NE_HASZNALD + " " + szemely.SzuletesiUtonev_NE_HASZNALD
                          }
                          ).ToList();
            var selectOptions =
                          result.Select(s => new KSelect2ItemModel()
                          {
                              id = s.ResztvevoId.ToString(),
                              text = (erintettsegFokaSelectben ? (s.ErintettsegFoka + " – ") : "") + s.FogvNyilvantartasiAzonosito + " " + s.FogvNev
                          }).ToList();
            var defaultOption = result.FirstOrDefault(f => f.ErintettsegFokaId == (int)defaultOptionErintettsegFoka)?.ResztvevoId ?? null;
            return new Tuple<int?, List<KSelect2ItemModel>>(defaultOption, selectOptions);
        }

        public KozvetitoiEljarasKezdemenyezeseModel GetKozvetitoiEljarasKezdemenyezeseModalData(List<int> fegyelmiUgyIds, List<int> naplobejegyzesIds)
        {
            var model = new KozvetitoiEljarasKezdemenyezeseModel();
            var fegyelmiUgy = FindById(fegyelmiUgyIds.First());
            var sertettekTouple = GetSertettOptionsByErintettsegFoka(fegyelmiUgy.EsemenyId, new List<ErintettsegFoka>() { ErintettsegFoka.Sertett, ErintettsegFoka.Tanu }, ErintettsegFoka.Sertett);
            model.SertettOptions = sertettekTouple.Item2;
            model.NaplobejegyzesIds = naplobejegyzesIds;


            if (naplobejegyzesIds.IsNullOrEmpty())
            {
                model.Leiras = "";

                model.SertettId = sertettekTouple.Item1;
                model.SertettKepviseloje = "";
                model.EljarasAlaVontKepviseloje = "";
            }
            else
            {
                var naploBejegyzesId = naplobejegyzesIds.First();
                var naploBejegyzes = NaploFunctions.FindById(naploBejegyzesId);
                model.Leiras = naploBejegyzes.JegyzokonyvTartalma;
                model.NaplobejegyzesIds = naplobejegyzesIds;
                model.SertettId = naploBejegyzes.SertettId;
                model.SertettKepviseloje = naploBejegyzes.SertettKepviseloje;
                model.EljarasAlaVontKepviseloje = naploBejegyzes.EljarasAlaVontKepviseloje;
            }
            return model;
        }

        public List<int> SaveKozvetitoiEljarasKezdemenyezeseModalData(KozvetitoiEljarasKezdemenyezeseModel model)
        {
            var aktIntezet = JogosultsagCacheFunctions.AktualisIntezet.Id;
            List<int> ujFegyelmi = new List<int>();
            List<int> megvaltozottFegyelmi = new List<int>();
            List<int> toroltFegyelmi = new List<int>();
            List<int> naploIds = new List<int>();
            using (var transaction = KonasoftBVFonixContext.Database.BeginTransaction())
            {
                var currentId = 0;
                try
                {

                    if (!model.NaplobejegyzesIds.IsNullOrEmpty())
                    {
                        foreach (var naplobejegyzesId in model.NaplobejegyzesIds)
                        {
                            var naploBejegyzes = NaploFunctions.FindById(naplobejegyzesId);
                            var fegyelmiUgy = (FegyelmiUgyViewModel)Table.Single(x => x.Id == naploBejegyzes.FegyelmiUgyId);
                            currentId = fegyelmiUgy.Id;
                            if (model.IsRogzites)
                            {
                                fegyelmiUgy.KozvetitoiEljarasKezdemenyezve = true;

                                Modify(fegyelmiUgy);
                                KonasoftBVFonixContext.SaveChanges();
                            }

                            naploBejegyzes.JegyzokonyvTartalma = model.Leiras;
                            naploBejegyzes.SertettId = model.SertettId;
                            naploBejegyzes.SertettKepviseloje = model.SertettKepviseloje;
                            naploBejegyzes.EljarasAlaVontKepviseloje = model.EljarasAlaVontKepviseloje;
                            naploBejegyzes.AlairtaFl = naploBejegyzes.AlairtaFl == true ? naploBejegyzes.AlairtaFl : model.IsRogzites;

                            NaploFunctions.Modify(naploBejegyzes);
                            KonasoftBVFonixContext.SaveChanges();

                            naploIds.Add(naploBejegyzes.Id);
                            megvaltozottFegyelmi.Add(naploBejegyzes.FegyelmiUgyId);
                        }
                    }
                    else
                    {
                        foreach (var fegyelmiUgyId in model.FegyelmiUgyIds)
                        {
                            currentId = fegyelmiUgyId;
                            var fegyelmiUgy = (FegyelmiUgyViewModel)Table.Single(x => x.Id == fegyelmiUgyId);
                            if (model.IsRogzites)
                            {
                                fegyelmiUgy.KozvetitoiEljarasKezdemenyezve = true;

                                Modify(fegyelmiUgy);
                                KonasoftBVFonixContext.SaveChanges();
                            }

                            var entity = new Naplo()
                            {
                                FegyelmiUgyId = fegyelmiUgyId,
                                FogvatartottId = fegyelmiUgy.FogvatartottId,
                                TipusCimkeId = (int)FegyelmiNaploTipus.KozvetitoiEljarasKezdemenyezese,
                                AlairtaFl = model.IsRogzites,
                                JegyzokonyvTartalma = model.Leiras,
                                SertettId = model.SertettId,
                                SertettKepviseloje = model.SertettKepviseloje,
                                EljarasAlaVontKepviseloje = model.EljarasAlaVontKepviseloje,
                            };
                            KonasoftBVFonixContext.Naplo.Add(entity);
                            KonasoftBVFonixContext.SaveChanges();

                            naploIds.Add(entity.Id);
                            megvaltozottFegyelmi.Add(fegyelmiUgyId);

                        }
                    }
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    Log.Error($"Hiba a közvetitöi eljárás kezdeményezése során (fegyelmiUgyId: {currentId})", e);
                    transaction.Rollback();
                    throw;
                }
            }

            OnFegyelmiUgyValtozas?.Invoke(ujFegyelmi, megvaltozottFegyelmi, toroltFegyelmi);
            return naploIds;
        }

        public List<int> GetArchivEvek(int intezetId)
        {
            var ugyek = (from fegyelmi in Table
                         join fogv in KonasoftBVFonixContext.Fogvatartottak on fegyelmi.FogvatartottId equals fogv.Id
                         where fegyelmi.TOROLT_FL != true && fegyelmi.Lezarva == true && (fegyelmi.RogzitoIntezetId == intezetId || fogv.AktualisIntezetId == intezetId || intezetId == (int)BvIntezet.Bvop)
                         select fegyelmi.UgySorszamaEv).Distinct().OrderByDescending(o => o).ToList();

            return ugyek;
        }

        public MaganelzarasVegrehajtvaModel GetMaganelzarasVegrehajtvaModalData(List<int> fegyelmiUgyIds, List<int> naplobejegyzesIds)
        {
            if (fegyelmiUgyIds.Count > 1)
                throw new WarningException("Ez a modal nem nyitható meg többes szerkesztésre.");
            if (naplobejegyzesIds != null && naplobejegyzesIds.Count > 1)
                throw new WarningException("Ez a modal nem nyitható meg többes szerkesztésre.");

            MaganelzarasVegrehajtvaModel model = new MaganelzarasVegrehajtvaModel();

            IEnumerable<IdLabelWithChildren> objektumok = GetElhelyezesek(AlkalmazasKontextusFunctions.Kontextus.RogzitoIntezetId, false);
            model.ZarkabaHelyezesOptions = objektumok.ToList();
            model.FegyelmiUgyIds = fegyelmiUgyIds;
            model.NaplobejegyzesIds = naplobejegyzesIds;

            if (model.NaplobejegyzesIds != null)
            {
                var naplobejegyzesId = model.NaplobejegyzesIds.First();
                var naplo = KonasoftBVFonixContext.Naplo.FirstOrDefault(x => x.Id == naplobejegyzesId);
                if (naplo.AlairtaFl ?? false)
                {
                    throw new WarningException("Ez a naplóbejegyzés lezárva, már nem nyitható meg szerkesztésre.");
                }
                model.ZarkabaHelyezes = naplo.EgyebAdatokJson;
                model.KihelyezesTenylegesIdeje = naplo.Hatarido.Value;
            }
            else
            {
                var naploEnitity = KonasoftBVFonixContext.Naplo.OrderByDescending(x => x.LetrehozasDatuma).FirstOrDefault(x => x.FegyelmiUgyId == fegyelmiUgyIds.FirstOrDefault() && !(x.AlairtaFl ?? false));
                if (naploEnitity != null && naploEnitity.TipusCimkeId == (int)(int)FegyelmiNaploTipus.MaganelzarasMegkezdese)
                {
                    model.ZarkabaHelyezes = naploEnitity.EgyebAdatokJson;
                    model.KihelyezesTenylegesIdeje = naploEnitity.MaganelzarasVegeDatum.HasValue ? naploEnitity.MaganelzarasVegeDatum.Value : DateTime.Now;
                }
                else
                {
                    model.KihelyezesTenylegesIdeje = DateTime.Now;
                }
            }

            return model;
        }

        public List<int> SaveMaganelzarasVegrehajtvaModalData(MaganelzarasVegrehajtvaModel model)
        {
            var aktIntezet = JogosultsagCacheFunctions.AktualisIntezet.Id;
            List<int> naploIds = new List<int>();
            int fId = 0;

            var fegyelmiUgyId = model.FegyelmiUgyIds.First();
            var fegyelmiUgy = Table.Single(x => x.Id == fegyelmiUgyId);
            List<int> ujFegyelmi = new List<int>();
            List<int> megvaltozottFegyelmi = new List<int>();
            List<int> toroltFegyelmi = new List<int>();
            string[] elhelyezes = model.ZarkabaHelyezes?.Split('_');

            if (model.FegyelmiUgyIds.Count > 1)
                throw new WarningException("Ez a modal nem nyitható meg többes szerkesztésre");
            if (model.NaplobejegyzesIds != null && model.NaplobejegyzesIds.Count > 1)
                throw new WarningException("Ez a modal nem nyitható meg többes szerkesztésre");
            if (fegyelmiUgy.FenyitesTipusCimkeId != FenyitesTipusok.Maganelzaras.CastToInt())
                throw new WarningException("A fegyelmi ügy típusa nem magánelzárás");
            if (!fegyelmiUgy.FenyitesHossza.HasValue)
                throw new WarningException("Fenyítés hossza nincs megadva, ami nem lehetséges");

            var vegrehajtasok = KonasoftBVFonixContext.FenyitesVegrehajtasok.Where(w => w.FegyelmiUgyId == fegyelmiUgyId).ToList();
            var vegrehajtottFenyitesIdeje = vegrehajtasok.Sum(s => ((TimeSpan)((s.VegeIdo ?? DateTime.Now) - s.KezdeteIdo)).TotalDays);
            var datumigVegrehajtott = vegrehajtasok.Sum(s => ((TimeSpan)((s.VegeIdo ?? model.KihelyezesTenylegesIdeje) - s.KezdeteIdo)).TotalMinutes);

            var maganelzarasKiszamoltVege = fegyelmiUgy.MaganelzarasVegeDatum.Value;
            var maradek = ((TimeSpan)(maganelzarasKiszamoltVege - DateTime.Now)).TotalMinutes.ToString("# ##0.##");
            if (DateTime.Now < maganelzarasKiszamoltVege)
                throw new WarningException($"A magánelzárás ideje még nem telt le. Még {maradek} perc van hátra a végrehajtásból");

            if (fegyelmiUgy.StatuszCimkeId != (int)CimkeEnums.FegyelmiUgyStatusz.FenyitesVegrehajtasAlatt)
            {
                Log.Error($"Hiba a magánelzárás megkezdésekor, csak fenyítés végrehajtása alatt kezdeményezhet magánelzárás végrehajtást (fegyelmiUgyId: {fegyelmiUgy})");
                throw new WarningException("Csak fenyítés végrehajtása alatt kezdeményezhet magánelzárás végrehajtást! Kérjük, csukja be a részletek ablakot és nyissa újra.");
            }
            var vegrehajtandoPercben = fegyelmiUgy.FenyitesHossza * 1440;
            if (datumigVegrehajtott < vegrehajtandoPercben)
            {
                Log.Error($"Hiba a magánelzárás megkezdésekor, csak fenyítés végrehajtása alatt kezdeményezhet magánelzárás végrehajtást (fegyelmiUgyId: {fegyelmiUgy})");
                throw new WarningException($"A magánelzárás ideje nem telt le a megadott dátumig.");
            }

            string elhelyezesStr = string.Empty;
            if (!string.IsNullOrWhiteSpace(model.ZarkabaHelyezes))
                elhelyezesStr = GetElhelyezesNevByString(model.ZarkabaHelyezes);

            using (DbContextTransaction transaction = KonasoftBVFonixContext.Database.BeginTransaction())
            {
                try
                {
                    OsszevontFegyelmiUgyekLezarasaByFegyelmiUgyId(fegyelmiUgy.Id);

                    fegyelmiUgy.StatuszCimkeId = (int)FegyelmiUgyStatusz.FenyitesVegrehajtva;
                    fegyelmiUgy.Lezarva = true;
                    fegyelmiUgy.Hatarido = null;
                    fegyelmiUgy.VegrehajtasVegeIdo = model.KihelyezesTenylegesIdeje;
                    fegyelmiUgy.VegrehajtasHosszEddig = vegrehajtottFenyitesIdeje;

                    fId = fegyelmiUgy.FogvatartottId;
                    if (model.NaplobejegyzesIds.IsNullOrEmpty())
                    {

                        var naploEnitity = new Naplo
                        {
                            FegyelmiUgyId = fegyelmiUgyId,
                            FogvatartottId = fegyelmiUgy.FogvatartottId,
                            JegyzokonyvTartalma = elhelyezesStr,
                            EgyebAdatokJson = model.ZarkabaHelyezes,
                            RogzitoIntezetId = aktIntezet,
                            TipusCimkeId = (int)FegyelmiNaploTipus.MaganelzarasVegrehajtva,
                            Hatarido = model.KihelyezesTenylegesIdeje,
                            Vegleges = true,
                            AlairtaFl = true
                        };
                        KonasoftBVFonixContext.Naplo.Add(naploEnitity);

                        var fenyitesVegrehajtasModel = KonasoftBVFonixContext.FenyitesVegrehajtasok
                                                        .Where(x => x.FegyelmiUgyId == fegyelmiUgyId && x.TOROLT_FL == false && x.VegeIdo == null)
                                                        .Single();

                        fenyitesVegrehajtasModel.VegeIdo = model.KihelyezesTenylegesIdeje;


                        KonasoftBVFonixContext.SaveChanges();
                        naploIds.Add(naploEnitity.Id);
                    }
                    else
                    {
                        throw new Exception("Magánelzárás végrehajtva form nem szerkeszthető");
                    }
                    toroltFegyelmi.Add(fegyelmiUgyId);

                    try
                    {
                        if (elhelyezes?.Length == 4)
                        {
                            int zarkaId = int.Parse(elhelyezes[2]);
                            int agyId = int.Parse(elhelyezes[3]);

                            //try
                            //{
                            //    new FegyelmiSzervizClient().MegszuntetMaganelzarasEsZarkabaBehelyez(
                            //        AlkalmazasKontextusFunctions.Kontextus.SzemelyzetSid,
                            //        AlkalmazasKontextusFunctions.Kontextus.RogzitoIntezetId,
                            //        fId,
                            //        zarkaId,
                            //        agyId,
                            //        model.KihelyezesTenylegesIdeje);
                            //}
                            //catch(Exception ex)
                            //{
                            //    Log.Error("FegyelmiSzervizclient().MegszuntetMaganelzarasEsZarkabaBehelyez hívás hiba: ", ex);
                            //}

                            // Főnix3 zárkába helyezés
                            //F3ZarkabaHelyezesFunctions.ZarkabaHelyezes((int)Modul.Fenyites, zarkaId, fId, agyId, model.KihelyezesTenylegesIdeje, fegyelmiUgyId, 42082, model.NocheckVegrehajtasiFok);
                        }
                        else
                        {
                            //try
                            //{
                            //    new BFBSzervizClient().ZarkabolKihelyezes(
                            //        AlkalmazasKontextusFunctions.Kontextus.SzemelyzetSid,
                            //        AlkalmazasKontextusFunctions.Kontextus.RogzitoIntezetId,
                            //        fId,
                            //        model.KihelyezesTenylegesIdeje);
                            //}
                            //catch(Exception ex)
                            //{
                            //    Log.Error("BFBSzervizclient().ZarkabolKihelyezes hívás hiba: ", ex);
                            //}

                            // Főnix3 zárkából kihelyezés
                            //F3ZarkabaHelyezesFunctions.ZarkabolKihelyezesFegyelmi((int)Modul.Fenyites, fId, model.KihelyezesTenylegesIdeje, model.NocheckVegrehajtasiFok);
                        }
                    }
                    //catch (F3ZarkabaHelyezesFunctions.VegrehajtasiFokException)
                    //{
                    //    throw;
                    //}
                    catch (Exception exc)
                    {
                        Log.Error("Zárkából kihelyezés Főnix3 hívás", exc);
                        try
                        {
                            //try
                            //{
                            //    new BFBSzervizClient().ZarkabolKihelyezes(
                            //        AlkalmazasKontextusFunctions.Kontextus.SzemelyzetSid,
                            //        AlkalmazasKontextusFunctions.Kontextus.RogzitoIntezetId,
                            //        fId,
                            //        model.KihelyezesTenylegesIdeje);
                            //}
                            //catch(Exception ex)
                            //{
                            //    Log.Error("BFBSzervizclient().ZarkabolKihelyezes hívás hiba: ", ex);
                            //}

                            //F3ZarkabaHelyezesFunctions.ZarkabolKihelyezesFegyelmi((int)Modul.Fenyites, fId, model.KihelyezesTenylegesIdeje, model.NocheckVegrehajtasiFok);
                        }
                        //catch (F3ZarkabaHelyezesFunctions.VegrehajtasiFokException)
                        //{
                        //    throw;
                        //}
                        catch (Exception e)
                        {
                            Log.Error($"Hiba a kihelyezés mentés során (fegyelmiUgyId: {model.FegyelmiUgyIds?.FirstOrDefault()})", e);
                            throw new WarningException($"A kihelyezés elküldése a Főnix3 rendszerbe sikertelen volt, kérjük próbálja újra. ({exc.Message})");
                        }
                    }

                    transaction.Commit();
                }
                //catch (F3ZarkabaHelyezesFunctions.VegrehajtasiFokException)
                //{
                //    transaction.Rollback();
                //    throw;
                //}
                catch (Exception e)
                {
                    Log.Error($"Hiba a behelyezés mentés során (fegyelmiUgyId: {model.FegyelmiUgyIds?.FirstOrDefault()})", e);
                    transaction.Rollback();

                    throw;
                }
            }

            OnFegyelmiUgyValtozas?.Invoke(ujFegyelmi, megvaltozottFegyelmi, toroltFegyelmi);

            return naploIds;
        }

        public FenyitesNemVegrehajthatoModel GetFenyitesNemVegrehajthatoModalData(List<int> fegyelmiUgyIds, List<int> naplobejegyzesIds)
        {
            if (fegyelmiUgyIds.Count > 1)
                throw new WarningException("Ez a modal nem nyitható meg többes szerkesztésre.");
            if (naplobejegyzesIds != null && naplobejegyzesIds.Count > 1)
                throw new WarningException("Ez a modal nem nyitható meg többes szerkesztésre.");

            FenyitesNemVegrehajthatoModel model = new FenyitesNemVegrehajthatoModel()
            {
                fegyelmiUgyIds = fegyelmiUgyIds,
                naplobejegyzesIds = naplobejegyzesIds
            };

            return model;
        }

        public List<int> SaveFenyitesNemVegrehajthatoModalData(FenyitesNemVegrehajthatoModel model)
        {
            var aktIntezet = JogosultsagCacheFunctions.AktualisIntezet.Id;
            List<int> naploIds = new List<int>();

            List<int> ujFegyelmi = new List<int>();
            List<int> megvaltozottFegyelmi = new List<int>();
            List<int> toroltFegyelmi = new List<int>();

            if (model.fegyelmiUgyIds.Count > 1)
                throw new WarningException("Ez a modal nem nyitható meg többes szerkesztésre");
            if (model.naplobejegyzesIds != null && model.naplobejegyzesIds.Count > 1)
                throw new WarningException("Ez a modal nem nyitható meg többes szerkesztésre");


            using (DbContextTransaction transaction = KonasoftBVFonixContext.Database.BeginTransaction())
            {
                try
                {
                    var fegyelmiUgyId = model.fegyelmiUgyIds.First();
                    var fegyelmiUgy = Table.Single(x => x.Id == fegyelmiUgyId);

                    if (fegyelmiUgy.StatuszCimkeId == (int)CimkeEnums.FegyelmiUgyStatusz.FenyitesVegrehajtasAlatt && fegyelmiUgy.FenyitesTipusCimkeId == (int)CimkeEnums.FenyitesTipusok.Maganelzaras)
                    {
                        Log.Error($"Hiba a fenyítés végrehajthatatlanná tétele során, magánelzárás alatt a fenyítés nem tehető végrehajthatatlan státuszba.");
                        throw new WarningException("Magánelzárás alatt a fenyítés nem tehető végrehajthatatlan státuszba.");
                    }

                    Naplo naploEnitity = new Naplo
                    {
                        FegyelmiUgyId = fegyelmiUgyId,
                        FogvatartottId = fegyelmiUgy.FogvatartottId,
                        JegyzokonyvTartalma = model.Leiras,
                        RogzitoIntezetId = aktIntezet,
                        Vegleges = true,
                        TipusCimkeId = (int)FegyelmiNaploTipus.FenyitesNemVegrehajthato,
                    };
                    KonasoftBVFonixContext.Naplo.Add(naploEnitity);
                    KonasoftBVFonixContext.SaveChanges();

                    fegyelmiUgy.Lezarva = true;
                    fegyelmiUgy.Hatarido = null;
                    fegyelmiUgy.StatuszCimkeId = (int)FegyelmiUgyStatusz.NemHajthatoVegre;

                    OsszevontFegyelmiUgyekLezarasaByFegyelmiUgyId(fegyelmiUgy.Id);

                    KonasoftBVFonixContext.SaveChanges();

                    toroltFegyelmi.Add(fegyelmiUgyId);


                    transaction.Commit();
                }
                catch (Exception e)
                {
                    Log.Error($"Hiba a nem végrehajthatóság mentése során (fegyelmiUgyId: {model.fegyelmiUgyIds.First()})", e);
                    transaction.Rollback();

                    throw;
                }
            }

            OnFegyelmiUgyValtozas?.Invoke(ujFegyelmi, megvaltozottFegyelmi, toroltFegyelmi);

            return naploIds;
        }

        public void OsszevontFegyelmiUgyekLezarasaByFegyelmiUgyId(int fegyelmiUgyId)
        {
            var alFegyelmiUgyek = KonasoftBVFonixContext.FegyelmiUgyek.Where(w => w.FoFegyelmiUgyId == fegyelmiUgyId).ToList();

            foreach (var alFegyelmiUgy in alFegyelmiUgyek)
            {
                if (alFegyelmiUgy.ElkulonitveFl == true)
                {
                    alFegyelmiUgy.ElkulonitveFl = false;

                    // Főnix3 elkülönítés megszüntetés
                    //new F3ElkulonitesekFunctions().ElkulonitesMegszuntetesByFegyelmiId(alFegyelmiUgy.Id);
                }

                alFegyelmiUgy.Lezarva = true;
                alFegyelmiUgy.Hatarido = null;
            }
        }

        public MaganelzarasEllenjavallataModel GetMaganelzarasEllenjavallataModalData(List<int> fegyelmiUgyIds, List<int> naplobejegyzesIds)
        {
            if (fegyelmiUgyIds.Count > 1)
                throw new WarningException("Ez a modal nem nyitható meg többes szerkesztésre.");
            if (naplobejegyzesIds != null && naplobejegyzesIds.Count > 1)
                throw new WarningException("Ez a modal nem nyitható meg többes szerkesztésre.");

            MaganelzarasEllenjavallataModel model = new MaganelzarasEllenjavallataModel()
            {
                fegyelmiUgyIds = fegyelmiUgyIds,
                naplobejegyzesIds = naplobejegyzesIds
            };

            return model;
        }

        public List<int> SaveMaganelzarasEllenjavallataModalData(MaganelzarasEllenjavallataModel model)
        {
            var aktIntezet = JogosultsagCacheFunctions.AktualisIntezet.Id;
            List<int> naploIds = new List<int>();

            List<int> ujFegyelmi = new List<int>();
            List<int> megvaltozottFegyelmi = new List<int>();
            List<int> toroltFegyelmi = new List<int>();

            if (model.fegyelmiUgyIds.Count > 1)
                throw new WarningException("Ez a modal nem nyitható meg többes szerkesztésre");
            if (model.naplobejegyzesIds != null && model.naplobejegyzesIds.Count > 1)
                throw new WarningException("Ez a modal nem nyitható meg többes szerkesztésre");


            using (DbContextTransaction transaction = KonasoftBVFonixContext.Database.BeginTransaction())
            {
                try
                {
                    var fegyelmiUgyId = model.fegyelmiUgyIds.First();
                    var fegyelmiUgy = Table.Single(x => x.Id == fegyelmiUgyId);

                    if (fegyelmiUgy.StatuszCimkeId == (int)CimkeEnums.FegyelmiUgyStatusz.FenyitesVegrehajtasAlatt && fegyelmiUgy.FenyitesTipusCimkeId == (int)CimkeEnums.FenyitesTipusok.Maganelzaras)
                    {
                        Log.Error($"Hiba az ideiglenes ellenjavallat rögzítése során, magánelzárás alatt az ellenjavallat rögzítéséhez előbb fel kell függeszteni a magánelzárást.");
                        throw new WarningException("Magánelzárás alatt a fenem tehető ideiglenes ellenjavallat.");
                    }

                    Naplo naploEnitity = new Naplo
                    {
                        FegyelmiUgyId = fegyelmiUgyId,
                        FogvatartottId = fegyelmiUgy.FogvatartottId,
                        JegyzokonyvTartalma = model.Leiras,
                        RogzitoIntezetId = aktIntezet,
                        Vegleges = true,
                        TipusCimkeId = (int)FegyelmiNaploTipus.MaganelzarasIdeiglenesenEllenjavallt,
                        Hatarido = model.Hatarido
                    };
                    KonasoftBVFonixContext.Naplo.Add(naploEnitity);
                    KonasoftBVFonixContext.SaveChanges();

                    //fegyelmiUgy.Lezarva = true;
                    //fegyelmiUgy.Hatarido = null;
                    //fegyelmiUgy.StatuszCimkeId = (int)FegyelmiUgyStatusz.NemHajthatoVegre;
                    fegyelmiUgy.MaganelzarasEllenjavaltHatarido = model.Hatarido;
                    KonasoftBVFonixContext.SaveChanges();

                    megvaltozottFegyelmi.Add(fegyelmiUgyId);


                    transaction.Commit();
                }
                catch (Exception e)
                {
                    Log.Error($"Hiba az ideiglenes ellenjavallat mentése során (fegyelmiUgyId: {model.fegyelmiUgyIds.First()})", e);
                    transaction.Rollback();

                    throw;
                }
            }

            OnFegyelmiUgyValtozas?.Invoke(ujFegyelmi, megvaltozottFegyelmi, toroltFegyelmi);

            return naploIds;
        }


        public DontesKozvetitoiEljarasrolModel GetDontesKozvetitoiEljarasrolModalData(List<int> fegyelmiUgyIds, List<int> naplobejegyzesIds)
        {
            var result = new DontesKozvetitoiEljarasrolModel();

            var fegyelmiJogkorGyakorlojaUsers = SzemelyzetFunctions.GetFegyelmiJogkorGyakorlojaAlkalmazottak();
            var reintUsers = SzemelyzetFunctions.GetFegyelmiReintegraciosTisztiAlkalmazottak();
            var kozvetito = new List<AdFegyelmiUserItem>();
            kozvetito.AddRange(reintUsers);
            kozvetito.AddRange(fegyelmiJogkorGyakorlojaUsers);
            kozvetito = kozvetito.GroupBy(x => x.Sid).Select(x => x.First()).ToList();
            result.KozvetitoOptions = kozvetito.Select(s => new KSelect2ItemModel() { id = s.Sid, text = s.Displayname + " " + s.Rendfokozat }).ToList();

            // A modal nem módosítható
            var fegyelmiUgyId = fegyelmiUgyIds.First();
            var hatarIdo = GetFegyelmiHatarido(fegyelmiUgyId, false, null, true);

            if (!hatarIdo.HasValue)
            {
                Log.Error($"Hiba a határidő kérése során (GetDontesKozvetitoiEljarasrolModalData, fegyelmiUgyId: {fegyelmiUgyId})");
                throw new WarningException("Nem állapítható meg határidő");
            }

            result.Datum = hatarIdo.Value;
            result.MaxDatum = hatarIdo.Value;

            return result;
        }

        public List<int> SaveDontesKozvetitoiEljarasrolModalData(DontesKozvetitoiEljarasrolModel model)
        {
            var aktIntezet = JogosultsagCacheFunctions.AktualisIntezet.Id;
            List<int> ujFegyelmi = new List<int>();
            List<int> megvaltozottFegyelmi = new List<int>();
            List<int> toroltFegyelmi = new List<int>();
            List<int> naploIds = new List<int>();
            using (var transaction = KonasoftBVFonixContext.Database.BeginTransaction())
            {
                var currentId = 0;
                try
                {
                    int? kozvetitoId = null;
                    if (!string.IsNullOrWhiteSpace(model.KozvetitoId))
                    {
                        kozvetitoId = SzemelyzetFunctions.SzemelyzetLekeresVagyLetrehozas(model.KozvetitoId, null, null).Id;
                    }
                    if (!model.NaplobejegyzesIds.IsNullOrEmpty())
                    {
                        foreach (var naplobejegyzesId in model.NaplobejegyzesIds)
                        {
                            var naploBejegyzes = NaploFunctions.FindById(naplobejegyzesId);
                            var fegyelmiUgy = (FegyelmiUgyViewModel)Table.Single(x => x.Id == naploBejegyzes.FegyelmiUgyId);
                            currentId = fegyelmiUgy.Id;

                            fegyelmiUgy.KozvetitoiEljarasKezdemenyezve = false;
                            if (!model.IsVisszautasitas)
                            {

                                fegyelmiUgy.KozvetitoiEljarasban = true;
                                fegyelmiUgy.KozvetitoSzemelyId = kozvetitoId;

                                fegyelmiUgy.Felfuggesztve = true;
                                fegyelmiUgy.FelfuggesztesDatum = DateTime.Now;
                                fegyelmiUgy.FelfuggesztesiJavaslat = false;
                                fegyelmiUgy.Hatarido = model.Datum;
                                FelfuggesztesFunctions.CreateFelfuggesztes(DateTime.Now, (int)FelfuggesztesOka.KozvetítoiEljarasraUtalas, fegyelmiUgy.Id);

                            }
                            else
                            {
                                fegyelmiUgy.KozvetitoiEljarasban = false;
                                fegyelmiUgy.Felfuggesztve = false;
                                fegyelmiUgy.FelfuggesztesDatum = null;
                                fegyelmiUgy.KozvetitoSzemelyId = kozvetitoId;
                            }

                            Modify(fegyelmiUgy);
                            KonasoftBVFonixContext.SaveChanges();


                            naploBejegyzes.Visszautasitva = naploBejegyzes.Visszautasitva ?? false;
                            naploBejegyzes.JegyzokonyvVezetoSzemelyId = kozvetitoId;

                            naploBejegyzes.Hatarido = model.IsVisszautasitas ? (DateTime?)null : model.Datum;


                            NaploFunctions.Modify(naploBejegyzes);
                            KonasoftBVFonixContext.SaveChanges();

                            naploIds.Add(naploBejegyzes.Id);
                            megvaltozottFegyelmi.Add(naploBejegyzes.FegyelmiUgyId);
                        }
                    }
                    else
                    {
                        foreach (var fegyelmiUgyId in model.FegyelmiUgyIds)
                        {
                            currentId = fegyelmiUgyId;
                            var fegyelmiUgy = (FegyelmiUgyViewModel)Table.Single(x => x.Id == fegyelmiUgyId);

                            fegyelmiUgy.KozvetitoiEljarasKezdemenyezve = false;
                            if (!model.IsVisszautasitas)
                            {

                                fegyelmiUgy.KozvetitoiEljarasban = true;
                                fegyelmiUgy.KozvetitoSzemelyId = kozvetitoId;
                                fegyelmiUgy.Felfuggesztve = true;
                                fegyelmiUgy.FelfuggesztesDatum = DateTime.Now;
                                fegyelmiUgy.FelfuggesztesiJavaslat = false;
                                fegyelmiUgy.Hatarido = model.Datum;
                                FelfuggesztesFunctions.CreateFelfuggesztes(DateTime.Now, (int)FelfuggesztesOka.KozvetítoiEljarasraUtalas, fegyelmiUgyId);

                            }
                            else
                            {
                                fegyelmiUgy.KozvetitoiEljarasban = false;
                                fegyelmiUgy.Felfuggesztve = false;
                                fegyelmiUgy.FelfuggesztesDatum = null;
                                fegyelmiUgy.KozvetitoSzemelyId = kozvetitoId;
                            }

                            Modify(fegyelmiUgy);
                            KonasoftBVFonixContext.SaveChanges();

                            var entity = new Naplo()
                            {
                                FegyelmiUgyId = fegyelmiUgyId,
                                FogvatartottId = fegyelmiUgy.FogvatartottId,
                                TipusCimkeId = (int)FegyelmiNaploTipus.KozvetitoiEljarasElrendeles,

                                Visszautasitva = model.IsVisszautasitas,
                                Hatarido = model.IsVisszautasitas ? (DateTime?)null : model.Datum,
                                JegyzokonyvVezetoSzemelyId = kozvetitoId

                            };


                            KonasoftBVFonixContext.Naplo.Add(entity);
                            KonasoftBVFonixContext.SaveChanges();

                            naploIds.Add(entity.Id);
                            megvaltozottFegyelmi.Add(fegyelmiUgyId);

                        }
                    }
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    Log.Error($"Hiba a közvetitöi eljárás kezdeményezése során (fegyelmiUgyId: {currentId})", e);
                    transaction.Rollback();
                    throw;
                }
            }

            OnFegyelmiUgyValtozas?.Invoke(ujFegyelmi, megvaltozottFegyelmi, toroltFegyelmi);
            return naploIds;
        }
        public IndoklassalMegszuntetesModel GetIndoklassalMegszuntetesModalData(List<int> fegyelmiUgyIds, List<int> naplobejegyzesIds)
        {
            var cimkek = CimkeFunctions.GetCimkekByFelhoId((int)Felhok.MegszunesOka).Select(s => new KSelect2ItemModel() { id = s.Id.ToString(), text = s.Nev }).ToList();
            var fegyelmiUgyId = fegyelmiUgyIds.First();
            var fegyelmiUgyStatusz = GetFegyelmiUgyById(fegyelmiUgyId).StatuszCimkeId;
            var model = new IndoklassalMegszuntetesModel()
            {
                OkaOptions = cimkek,
                OkaId = null,
                Indoklas = "",
                FegyelmiUgyStatusz = fegyelmiUgyStatusz,
            };


            return model;
        }

        public List<int> SaveIndoklassalMegszuntetesModalData(IndoklassalMegszuntetesModel model)
        {
            var aktIntezet = JogosultsagCacheFunctions.AktualisIntezet.Id;
            List<int> ujFegyelmi = new List<int>();
            List<int> megvaltozottFegyelmi = new List<int>();
            List<int> toroltFegyelmi = new List<int>();
            List<int> naploIds = new List<int>();
            using (var transaction = KonasoftBVFonixContext.Database.BeginTransaction())
            {
                var currentId = 0;
                try
                {

                    if (!model.NaplobejegyzesIds.IsNullOrEmpty())
                    {
                        foreach (var naplobejegyzesId in model.NaplobejegyzesIds)
                        {
                            var naploBejegyzes = NaploFunctions.FindById(naplobejegyzesId);
                            var fegyelmiUgy = (FegyelmiUgyViewModel)Table.Single(x => x.Id == naploBejegyzes.FegyelmiUgyId);
                            currentId = fegyelmiUgy.Id;

                            fegyelmiUgy.KozvetitoiEljarasban = false;
                            fegyelmiUgy.KozvetitoiSikeres = false;

                            Modify(fegyelmiUgy);
                            KonasoftBVFonixContext.SaveChanges();

                            if (fegyelmiUgy.StatuszCimkeId != (int)FegyelmiUgyStatusz.KivizsgalasFolyamatban)
                            {
                                FelfuggesztesMegszunteteseConfrimModal(new List<int>() { fegyelmiUgy.Id });
                            }

                            naploBejegyzes.MegszuntetesOkaCimkeId = model.OkaId;
                            naploBejegyzes.JegyzokonyvTartalma = model.Indoklas;

                            NaploFunctions.Modify(naploBejegyzes);
                            KonasoftBVFonixContext.SaveChanges();

                            naploIds.Add(naploBejegyzes.Id);
                            megvaltozottFegyelmi.Add(naploBejegyzes.FegyelmiUgyId);
                        }
                    }
                    else
                    {
                        foreach (var fegyelmiUgyId in model.FegyelmiUgyIds)
                        {
                            currentId = fegyelmiUgyId;
                            var fegyelmiUgy = (FegyelmiUgyViewModel)Table.Single(x => x.Id == fegyelmiUgyId);

                            fegyelmiUgy.KozvetitoiEljarasban = false;
                            fegyelmiUgy.KozvetitoiSikeres = false;

                            Modify(fegyelmiUgy);
                            KonasoftBVFonixContext.SaveChanges();

                            if (fegyelmiUgy.StatuszCimkeId != (int)FegyelmiUgyStatusz.KivizsgalasFolyamatban)
                            {
                                FelfuggesztesMegszunteteseConfrimModal(new List<int>() { fegyelmiUgy.Id });
                            }

                            var entity = new Naplo()
                            {
                                FegyelmiUgyId = fegyelmiUgyId,
                                FogvatartottId = fegyelmiUgy.FogvatartottId,
                                TipusCimkeId = (int)FegyelmiNaploTipus.KozvetitoiEljarasIndoklassalMegszuntetes,
                                MegszuntetesOkaCimkeId = model.OkaId,
                                JegyzokonyvTartalma = model.Indoklas
                            };
                            KonasoftBVFonixContext.Naplo.Add(entity);
                            KonasoftBVFonixContext.SaveChanges();

                            naploIds.Add(entity.Id);
                            megvaltozottFegyelmi.Add(fegyelmiUgyId);

                        }
                    }
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    Log.Error($"Hiba a közvetitöi eljárás döntése során (fegyelmiUgyId: {currentId})", e);
                    transaction.Rollback();
                    throw;
                }
            }

            OnFegyelmiUgyValtozas?.Invoke(ujFegyelmi, megvaltozottFegyelmi, toroltFegyelmi);
            return naploIds;
        }


        public MaganelzarasMegszakitasanakRogziteseModel GetMaganelzarasMegszakitasanakRogziteseModalData(List<int> fegyelmiUgyIds, List<int> naplobejegyzesIds)
        {
            if (fegyelmiUgyIds.Count > 1)
                throw new WarningException("Ez a modal nem nyitható meg többes szerkesztésre.");
            if (naplobejegyzesIds != null && naplobejegyzesIds.Count > 1)
                throw new WarningException("Ez a modal nem nyitható meg többes szerkesztésre.");

            MaganelzarasMegszakitasanakRogziteseModel model = new MaganelzarasMegszakitasanakRogziteseModel();

            IEnumerable<IdLabelWithChildren> objektumok = GetElhelyezesek(AlkalmazasKontextusFunctions.Kontextus.RogzitoIntezetId, false);
            model.VisszahelyezesZarkabaOptions = objektumok.ToList();
            model.fegyelmiUgyIds = fegyelmiUgyIds;
            model.naplobejegyzesIds = naplobejegyzesIds;

            if (model.naplobejegyzesIds != null)
            {
                var naplobejegyzesId = model.naplobejegyzesIds.First();
                var naplo = KonasoftBVFonixContext.Naplo.FirstOrDefault(x => x.Id == naplobejegyzesId);
                if (naplo.AlairtaFl ?? false)
                {
                    throw new WarningException("Ez a naplóbejegyzés lezárva, már nem nyitható meg szerkesztésre.");
                }
                model.VisszahelyezesZarkaba = naplo.EgyebAdatokJson;
                model.MaganzarkabolKihelyezesTenylegesIdeje = naplo.Hatarido.Value;
                model.Indoklas = naplo.Indoklas;
            }
            else
            {
                var naploEnitity = KonasoftBVFonixContext.Naplo.OrderByDescending(x => x.LetrehozasDatuma).FirstOrDefault(x => x.FegyelmiUgyId == fegyelmiUgyIds.FirstOrDefault() && !(x.AlairtaFl ?? false));
                if (naploEnitity != null && naploEnitity.TipusCimkeId == (int)FegyelmiNaploTipus.MaganelzarasMegszakitasa)
                {
                    model.VisszahelyezesZarkaba = naploEnitity.EgyebAdatokJson;
                    model.MaganzarkabolKihelyezesTenylegesIdeje = naploEnitity.Hatarido.Value;
                }
                else
                {
                    model.MaganzarkabolKihelyezesTenylegesIdeje = DateTime.Now;
                }
            }

            return model;
        }

        public List<int> SaveMaganelzarasMegszakitasanakRogziteseModalData(MaganelzarasMegszakitasanakRogziteseModel model)
        {
            var aktIntezet = JogosultsagCacheFunctions.AktualisIntezet.Id;
            List<int> naploIds = new List<int>();
            int fId = 0;

            List<int> ujFegyelmi = new List<int>();
            List<int> megvaltozottFegyelmi = new List<int>();
            List<int> toroltFegyelmi = new List<int>();
            string[] elhelyezes = model.VisszahelyezesZarkaba != null ? model.VisszahelyezesZarkaba.Split('_') : null;

            if (model.fegyelmiUgyIds.Count > 1)
                throw new WarningException("Ez a modal nem nyitható meg többes szerkesztésre");
            if (model.naplobejegyzesIds != null && model.naplobejegyzesIds.Count > 1)
                throw new WarningException("Ez a modal nem nyitható meg többes szerkesztésre");
            if (elhelyezes != null && elhelyezes.Length != 4)
            {
                throw new WarningException("Az elhelyezés azonosítója nem megfelelő.");
            }


            var elhelyezesStr = model.VisszahelyezesZarkaba != null ? GetElhelyezesNevByString(model.VisszahelyezesZarkaba) : null;

            Dictionary<string, string> parameterek = new Dictionary<string, string>();

            using (DbContextTransaction transaction = KonasoftBVFonixContext.Database.BeginTransaction())
            {
                var currentId = 0;
                try
                {
                    var fegyelmiUgyId = model.fegyelmiUgyIds.First();
                    var fegyelmiUgy = (FegyelmiUgyViewModel)Table.Single(x => x.Id == fegyelmiUgyId);

                    fId = fegyelmiUgy.FogvatartottId;


                    if (fegyelmiUgy.StatuszCimkeId != (int)CimkeEnums.FegyelmiUgyStatusz.FenyitesVegrehajtasAlatt)
                    {
                        Log.Error($"Hiba a magánelzárás megszakításakor, csak magánelzárás végrehajtása alatt kezdeményezhet magánelzárás megszakítást.");
                        throw new WarningException("Csak magánelzárás végrehajtása alatt kezdeményezhet magánelzárás megszakítást! Kérjük, csukja be a részletek ablakot és nyissa újra.");
                    }

                    var fenyitesVegrehajtasModel = KonasoftBVFonixContext.FenyitesVegrehajtasok
                                                                .Where(x => x.FegyelmiUgyId == fegyelmiUgyId && x.TOROLT_FL == false && x.VegeIdo == null)
                                                                .Single();

                    var behelyezesIdeje = fenyitesVegrehajtasModel.KezdeteIdo;
                    if (behelyezesIdeje > model.MaganzarkabolKihelyezesTenylegesIdeje)
                    {
                        Log.Error($"Hiba a magánelzárás megszakításakor, a magánelzárást csak a végrehajtás megekezdését követően lehet megszakítani.");
                        throw new WarningException("A magánelzárást csak a végrehajtás megekezdését követően lehet megszakítani.");
                    }

                    fegyelmiUgy.StatuszCimkeId = (int)CimkeEnums.FegyelmiUgyStatusz.FenyitesVegrehajtasMegszakitva;
                    Modify(fegyelmiUgy);
                    KonasoftBVFonixContext.SaveChanges();

                    if (model.naplobejegyzesIds.IsNullOrEmpty())
                    {
                        var naploEnitity = new Naplo()
                        {
                            FegyelmiUgyId = fegyelmiUgyId,
                            FogvatartottId = fegyelmiUgy.FogvatartottId,
                            JegyzokonyvTartalma = elhelyezesStr,
                            EgyebAdatokJson = model.VisszahelyezesZarkaba,
                            RogzitoIntezetId = aktIntezet,
                            TipusCimkeId = (int)FegyelmiNaploTipus.MaganelzarasMegszakitasa,
                            Hatarido = model.MaganzarkabolKihelyezesTenylegesIdeje,
                            Indoklas = model.Indoklas

                        };
                        KonasoftBVFonixContext.Naplo.Add(naploEnitity);

                        KonasoftBVFonixContext.SaveChanges();
                        naploIds.Add(naploEnitity.Id);
                    }
                    else
                    {
                        var naploEntity = KonasoftBVFonixContext.Naplo.FirstOrDefault(x => x.Id == model.naplobejegyzesIds.FirstOrDefault());
                        var legfrisebbNaplo = KonasoftBVFonixContext.Naplo.OrderByDescending(x => x.LetrehozasDatuma).FirstOrDefault(x => x.FegyelmiUgyId == fegyelmiUgy.Id);
                        if (naploEntity.AlairtaFl ?? false)
                        {
                            throw new WarningException("Ez a naplóbejegyzés lezárva, már nem nyitható meg szerkesztésre.");
                        }

                        naploEntity.Hatarido = model.MaganzarkabolKihelyezesTenylegesIdeje;
                        naploEntity.EgyebAdatokJson = model.VisszahelyezesZarkaba;
                        naploEntity.JegyzokonyvTartalma = elhelyezesStr;
                        naploEntity.Indoklas = model.Indoklas;
                        KonasoftBVFonixContext.SaveChanges();
                        naploIds.Add(naploEntity.Id);
                    }
                    fenyitesVegrehajtasModel.VegeIdo = model.MaganzarkabolKihelyezesTenylegesIdeje;

                    KonasoftBVFonixContext.SaveChanges();
                    megvaltozottFegyelmi.Add(fegyelmiUgyId);

                    try
                    {
                        if (elhelyezes?.Length == 4)
                        {
                            int zarkaId = int.Parse(elhelyezes[2]);
                            int agyId = int.Parse(elhelyezes[3]);

                            //try
                            //{
                            //    new FegyelmiSzervizClient().MegszuntetMaganelzarasEsZarkabaBehelyez(
                            //        AlkalmazasKontextusFunctions.Kontextus.SzemelyzetSid,
                            //        AlkalmazasKontextusFunctions.Kontextus.RogzitoIntezetId,
                            //        fId,
                            //        zarkaId,
                            //        agyId,
                            //        model.MaganzarkabolKihelyezesTenylegesIdeje);
                            //}
                            //catch (Exception ex)
                            //{
                            //    Log.Error("FegyelmiSzervizclient().MegszuntetMaganelzarasEsZarkabaBehelyez hívás hiba: ", ex);
                            //}

                            // Főnix3 zárkába helyezés
                            //F3ZarkabaHelyezesFunctions.ZarkabaHelyezes((int)Modul.Fenyites, zarkaId, fId, agyId, model.MaganzarkabolKihelyezesTenylegesIdeje, fegyelmiUgyId, 42082, model.NocheckVegrehajtasiFok);
                        }
                        else
                        {
                            //try
                            //{
                            //    new BFBSzervizClient().ZarkabolKihelyezes(
                            //        AlkalmazasKontextusFunctions.Kontextus.SzemelyzetSid,
                            //        AlkalmazasKontextusFunctions.Kontextus.RogzitoIntezetId,
                            //        fId,
                            //        model.MaganzarkabolKihelyezesTenylegesIdeje);
                            //}
                            //catch(Exception ex)
                            //{
                            //    Log.Error("BFBSzervizclient().ZarkabolKihelyezes hívás hiba: ", ex);
                            //}

                            // Főnix3 zárkából kihelyezés
                            //F3ZarkabaHelyezesFunctions.ZarkabolKihelyezesFegyelmi((int)Modul.Fenyites, fId, model.MaganzarkabolKihelyezesTenylegesIdeje, model.NocheckVegrehajtasiFok);

                        }
                    }
                    //catch (F3ZarkabaHelyezesFunctions.VegrehajtasiFokException)
                    //{
                    //    throw;
                    //}
                    catch (Exception exc)
                    {
                        Log.Error("Zárkábahelyezés Főnix3 hívás", exc);
                        throw new WarningException($"A behelyezés elküldése a Főnix3 rendszerbe sikertelen volt, kérjük próbálja újra. ({exc.Message})");
                    }

                    transaction.Commit();
                }
                //catch (F3ZarkabaHelyezesFunctions.VegrehajtasiFokException)
                //{
                //    transaction.Rollback();
                //    throw;
                //}
                catch (Exception e)
                {
                    Log.Error($"Hiba a magánelzárás megszakítása során (fegyelmiUgyId: {currentId})", e);
                    transaction.Rollback();

                    throw;
                }
            }


            OnFegyelmiUgyValtozas?.Invoke(ujFegyelmi, megvaltozottFegyelmi, toroltFegyelmi);

            return naploIds;
        }

        public KozvetitoiEljarasGaranciaTeljesulesenekRogziteseModel GetKozvetitoiEljarasGaranciaTeljesulesenekRogziteseModalData(List<int> fegyelmiUgyIds, List<int> naplobejegyzesIds)
        {
            if (fegyelmiUgyIds.Count > 1)
                throw new WarningException("Ez a modal nem nyitható meg többes szerkesztésre.");
            if (naplobejegyzesIds != null && naplobejegyzesIds.Count > 1)
                throw new WarningException("Ez a modal nem nyitható meg többes szerkesztésre.");

            KozvetitoiEljarasGaranciaTeljesulesenekRogziteseModel model = new KozvetitoiEljarasGaranciaTeljesulesenekRogziteseModel()
            {
                fegyelmiUgyIds = fegyelmiUgyIds,
                naplobejegyzesIds = naplobejegyzesIds,
                IsTeljesult = true
            };

            return model;
        }

        public List<int> SaveKozvetitoiEljarasGaranciaTeljesulesenekRogziteseModalData(KozvetitoiEljarasGaranciaTeljesulesenekRogziteseModel model)
        {
            var aktIntezet = JogosultsagCacheFunctions.AktualisIntezet.Id;
            List<int> naploIds = new List<int>();

            List<int> ujFegyelmi = new List<int>();
            List<int> megvaltozottFegyelmi = new List<int>();
            List<int> toroltFegyelmi = new List<int>();

            if (model.fegyelmiUgyIds.Count > 1)
                throw new WarningException("Ez a modal nem nyitható meg többes szerkesztésre");
            if (model.naplobejegyzesIds != null && model.naplobejegyzesIds.Count > 1)
                throw new WarningException("Ez a modal nem nyitható meg többes szerkesztésre");

            using (DbContextTransaction transaction = KonasoftBVFonixContext.Database.BeginTransaction())
            {
                try
                {
                    var fegyelmiUgyId = model.fegyelmiUgyIds.First();
                    var fegyelmiUgy = Table.Single(x => x.Id == fegyelmiUgyId);

                    Naplo naploEnitity = new Naplo
                    {
                        FegyelmiUgyId = fegyelmiUgyId,
                        FogvatartottId = fegyelmiUgy.FogvatartottId,
                        GaranciaTeljesultFl = model.IsTeljesult,
                        RogzitoIntezetId = aktIntezet,
                        TipusCimkeId = (int)FegyelmiNaploTipus.GaranciaTeljesulesenekRogzitese,
                    };
                    KonasoftBVFonixContext.Naplo.Add(naploEnitity);
                    KonasoftBVFonixContext.SaveChanges();

                    fegyelmiUgy.GaranciaTeljesultFl = model.IsTeljesult;
                    KonasoftBVFonixContext.SaveChanges();

                    megvaltozottFegyelmi.Add(fegyelmiUgyId);


                    transaction.Commit();
                }
                catch (Exception e)
                {
                    Log.Error($"Hiba a közvetítői eljárás gerencia teljesülésének mentése során (fegyelmiUgyId: {model.fegyelmiUgyIds.First()})", e);
                    transaction.Rollback();

                    throw;
                }
            }

            OnFegyelmiUgyValtozas?.Invoke(ujFegyelmi, megvaltozottFegyelmi, toroltFegyelmi);

            return naploIds;
        }

        public FeljegyzesEsMegallapodasModel GetFeljegyzesEsMegallapodasModalData(List<int> fegyelmiUgyIds, List<int> naplobejegyzesIds)
        {
            var result = new FeljegyzesEsMegallapodasModel();

            var fegyelmiJogkorGyakorlojaUsers = SzemelyzetFunctions.GetFegyelmiJogkorGyakorlojaAlkalmazottak();
            var reintUsers = SzemelyzetFunctions.GetFegyelmiReintegraciosTisztiAlkalmazottak();
            var kozvetito = new List<AdFegyelmiUserItem>();
            kozvetito.AddRange(reintUsers);
            kozvetito.AddRange(fegyelmiJogkorGyakorlojaUsers);
            kozvetito = kozvetito.GroupBy(x => x.Sid).Select(x => x.First()).ToList();
            result.ReintegraciosTisztOptions = kozvetito.Select(s => new KSelect2ItemModel() { id = s.Sid, text = s.Displayname }).ToList();

            if (fegyelmiUgyIds != null && fegyelmiUgyIds.Count > 1)
            {
                throw new WarningException("Ez a modal nem nyitható meg többes szerkesztésre.");
            }


            var fegyelmiUgyId = fegyelmiUgyIds.First();
            var hatarIdo = GetFegyelmiHatarido(fegyelmiUgyId, false, null, true);

            if (!hatarIdo.HasValue)
            {
                Log.Error($"Hiba a határidő kérése során (GetDontesKozvetitoiEljarasrolModalData, fegyelmiUgyId: {fegyelmiUgyId})");
                throw new WarningException("Nem állapítható meg határidő");
            }

            result.Hatarido = null;

            if (!naplobejegyzesIds.IsNullOrEmpty())
            {
                if (naplobejegyzesIds.Count > 1)
                    throw new WarningException("Ez a modal nem nyitható meg többes szerkesztésre.");

                var naploId = naplobejegyzesIds.First();
                var naplo = KonasoftBVFonixContext.Naplo.FirstOrDefault(x => x.Id == naploId);

                result.Megallapodas = naplo.JegyzokonyvTartalma;
                result.FeljegyzesMegbeszelesrol = naplo.VedoElerhetosege;
                result.EljarasAlaVontKepviseloje = naplo.EljarasAlaVontKepviseloje;
                result.SertettKepviseloje = naplo.SertettKepviseloje;
                if (!string.IsNullOrWhiteSpace(naplo.VedoCime))
                    result.EljarasAlaVontatErintoKoltsegek = int.Parse(naplo.VedoCime);
                if (!string.IsNullOrWhiteSpace(naplo.VedoNeve))
                    result.SertettetErintoKoltsegek = int.Parse(naplo.VedoNeve);
                result.ReintegraciosTisztSid = naplo.TovabbiJelenlevok;
                result.Hatarido = naplo.Hatarido;
            }

            var fegyelmiUgy = (FegyelmiUgyViewModel)Table.Single(x => x.Id == fegyelmiUgyId);
            result.MaxDatum = hatarIdo.Value;
            result.MinDatum = fegyelmiUgy.FelfuggesztesDatum.Value;
            return result;
        }

        public List<int> SaveFeljegyzesEsMegallapodasModalData(FeljegyzesEsMegallapodasModel model)
        {
            var aktIntezet = JogosultsagCacheFunctions.AktualisIntezet.Id;
            List<int> ujFegyelmi = new List<int>();
            List<int> megvaltozottFegyelmi = new List<int>();
            List<int> toroltFegyelmi = new List<int>();
            List<int> naploIds = new List<int>();
            using (var transaction = KonasoftBVFonixContext.Database.BeginTransaction())
            {
                var currentId = 0;
                try
                {

                    if (!model.NaplobejegyzesIds.IsNullOrEmpty())
                    {
                        var naplobejegyzesId = model.NaplobejegyzesIds.First();
                        var naploBejegyzes = NaploFunctions.FindById(naplobejegyzesId);
                        var fegyelmiUgy = (FegyelmiUgyViewModel)Table.Single(x => x.Id == naploBejegyzes.FegyelmiUgyId);
                        currentId = fegyelmiUgy.Id;

                        if (model.Vegleges)
                        {
                            fegyelmiUgy.KozvetitoiGaranciaHatarido = model.Hatarido;
                            Modify(fegyelmiUgy);
                            KonasoftBVFonixContext.SaveChanges();
                            naploBejegyzes.AlairtaFl = true;
                        }

                        naploBejegyzes.JegyzokonyvTartalma = model.Megallapodas;
                        naploBejegyzes.Hatarido = model.Hatarido;
                        naploBejegyzes.VedoElerhetosege = model.FeljegyzesMegbeszelesrol;
                        naploBejegyzes.EljarasAlaVontKepviseloje = model.EljarasAlaVontKepviseloje;
                        naploBejegyzes.SertettKepviseloje = model.SertettKepviseloje;
                        naploBejegyzes.VedoCime = model.EljarasAlaVontatErintoKoltsegek == null ? null : model.EljarasAlaVontatErintoKoltsegek.ToString();
                        naploBejegyzes.VedoNeve = model.SertettetErintoKoltsegek == null ? null : model.SertettetErintoKoltsegek.ToString();
                        naploBejegyzes.TovabbiJelenlevok = model.ReintegraciosTisztSid;

                        NaploFunctions.Modify(naploBejegyzes);
                        KonasoftBVFonixContext.SaveChanges();

                        naploIds.Add(naploBejegyzes.Id);
                        megvaltozottFegyelmi.Add(naploBejegyzes.FegyelmiUgyId);
                    }
                    else
                    {

                        currentId = model.FegyelmiUgyIds.First();
                        var fegyelmiUgy = (FegyelmiUgyViewModel)Table.Single(x => x.Id == currentId);

                        var entity = new Naplo()
                        {
                            FegyelmiUgyId = currentId,
                            FogvatartottId = fegyelmiUgy.FogvatartottId,
                            TipusCimkeId = (int)FegyelmiNaploTipus.FeljegyzesMegbeszelesrolMegallapodas,

                            JegyzokonyvTartalma = model.Megallapodas,
                            Hatarido = model.Hatarido,
                            VedoElerhetosege = model.FeljegyzesMegbeszelesrol,
                            EljarasAlaVontKepviseloje = model.EljarasAlaVontKepviseloje,
                            SertettKepviseloje = model.SertettKepviseloje,
                            VedoCime = model.EljarasAlaVontatErintoKoltsegek == null ? null : model.EljarasAlaVontatErintoKoltsegek.ToString(),
                            VedoNeve = model.SertettetErintoKoltsegek == null ? null : model.SertettetErintoKoltsegek.ToString(),
                            TovabbiJelenlevok = model.ReintegraciosTisztSid
                        };

                        if (model.Vegleges)
                        {
                            fegyelmiUgy.KozvetitoiGaranciaHatarido = model.Hatarido;
                            Modify(fegyelmiUgy);
                            entity.AlairtaFl = true;
                        }

                        KonasoftBVFonixContext.Naplo.Add(entity);
                        KonasoftBVFonixContext.SaveChanges();
                        naploIds.Add(entity.Id);
                        megvaltozottFegyelmi.Add(currentId);


                    }
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    Log.Error($"Hiba a megbeszélés, megállapodás rögzítésekor (fegyelmiUgyId: {currentId})", e);
                    transaction.Rollback();
                    throw;
                }
            }

            OnFegyelmiUgyValtozas?.Invoke(ujFegyelmi, megvaltozottFegyelmi, toroltFegyelmi);
            return naploIds;
        }

        public KozvetitoiEljarasHataridoModositasKereseModel GetKozvetitoiEljarasHataridoModositasKereseModalData(List<int> fegyelmiUgyIds, List<int> naplobejegyzesIds)
        {
            var model = new KozvetitoiEljarasHataridoModositasKereseModel();
            var fegyelmiUgy = FindById(fegyelmiUgyIds.First());
            model.NaplobejegyzesIds = naplobejegyzesIds;

            if (naplobejegyzesIds.IsNullOrEmpty())
            {
                model.Leiras = "";
            }
            else
            {
                var naploBejegyzesId = naplobejegyzesIds.First();
                var naploBejegyzes = NaploFunctions.FindById(naploBejegyzesId);
                model.Leiras = naploBejegyzes.JegyzokonyvTartalma;
            }
            return model;
        }

        public List<int> SaveKozvetitoiEljarasHataridoModositasKereseModalData(KozvetitoiEljarasHataridoModositasKereseModel model)
        {
            var aktIntezet = JogosultsagCacheFunctions.AktualisIntezet.Id;
            List<int> ujFegyelmi = new List<int>();
            List<int> megvaltozottFegyelmi = new List<int>();
            List<int> toroltFegyelmi = new List<int>();
            List<int> naploIds = new List<int>();
            using (var transaction = KonasoftBVFonixContext.Database.BeginTransaction())
            {
                var currentId = 0;
                try
                {
                    if (!model.NaplobejegyzesIds.IsNullOrEmpty())
                    {
                        foreach (var naplobejegyzesId in model.NaplobejegyzesIds)
                        {
                            var naploBejegyzes = NaploFunctions.FindById(naplobejegyzesId);
                            var fegyelmiUgy = (FegyelmiUgyViewModel)Table.Single(x => x.Id == naploBejegyzes.FegyelmiUgyId);
                            currentId = fegyelmiUgy.Id;
                            fegyelmiUgy.HataridoModositasJavaslat = true;

                            naploBejegyzes.JegyzokonyvTartalma = model.Leiras;
                            NaploFunctions.Modify(naploBejegyzes);
                            KonasoftBVFonixContext.SaveChanges();

                            naploIds.Add(naploBejegyzes.Id);
                            megvaltozottFegyelmi.Add(naploBejegyzes.FegyelmiUgyId);
                        }
                    }
                    else
                    {
                        foreach (var fegyelmiUgyId in model.FegyelmiUgyIds)
                        {
                            currentId = fegyelmiUgyId;
                            var fegyelmiUgy = (FegyelmiUgyViewModel)Table.Single(x => x.Id == fegyelmiUgyId);

                            var entity = new Naplo()
                            {
                                FegyelmiUgyId = fegyelmiUgyId,
                                FogvatartottId = fegyelmiUgy.FogvatartottId,
                                TipusCimkeId = (int)FegyelmiNaploTipus.KozvetitoiEljarasHataridoModositasKerese,
                                JegyzokonyvTartalma = model.Leiras,
                            };

                            fegyelmiUgy.HataridoModositasJavaslat = true;

                            naploIds.Add(entity.Id);
                            KonasoftBVFonixContext.Naplo.Add(entity);
                            KonasoftBVFonixContext.SaveChanges();

                            megvaltozottFegyelmi.Add(fegyelmiUgyId);

                        }
                    }
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    Log.Error($"Hiba a közvetitöi eljárás határidő módosítás kérése során (fegyelmiUgyId: {currentId})", e);
                    transaction.Rollback();
                    throw;
                }
            }

            OnFegyelmiUgyValtozas?.Invoke(ujFegyelmi, megvaltozottFegyelmi, toroltFegyelmi);
            return naploIds;
        }

        public KozvetitoiEljarasGaranciaHataridoModositasaModel GetKozvetitoiEljarasHataridoModositasModalData(List<int> fegyelmiUgyIds, List<int> naplobejegyzesIds)
        {
            if (fegyelmiUgyIds.Count > 1)
                throw new WarningException("Ez a modal nem nyitható meg többes szerkesztésre.");
            if (naplobejegyzesIds != null && naplobejegyzesIds.Count > 1)
                throw new WarningException("Ez a modal nem nyitható meg többes szerkesztésre.");
            //if (GetFegyelmiHatarido(fegyelmiUgyIds[0],true) < DateTime.Now.Date)
            //    throw new WarningException("A meghosszabbított határidővel is lejárt lenne a határidő, ezért nem hosszabbítható meg.");
            var datum = DateTime.Now;
            var fegyelmiUgyId = fegyelmiUgyIds.First();
            var hatarIdo = GetFegyelmiHatarido(fegyelmiUgyId, true);
            datum = hatarIdo.Value.AddDays(-1);
            if (datum < DateTime.Today)
            {
                datum = hatarIdo.Value;
            }

            var fegyelmiUgy = (FegyelmiUgyViewModel)Table.Single(x => x.Id == fegyelmiUgyId);

            KozvetitoiEljarasGaranciaHataridoModositasaModel model = new KozvetitoiEljarasGaranciaHataridoModositasaModel()
            {
                fegyelmiUgyIds = fegyelmiUgyIds,
                naplobejegyzesIds = naplobejegyzesIds,
                MaxDatum = datum,
                MinDatum = fegyelmiUgy.FelfuggesztesDatum.Value,
                Datum = datum
            };

            return model;
        }

        public List<int> SaveKozvetitoiEljarasHataridoModositasModalData(KozvetitoiEljarasGaranciaHataridoModositasaModel model)
        {
            var aktIntezet = JogosultsagCacheFunctions.AktualisIntezet.Id;
            List<int> naploIds = new List<int>();

            List<int> ujFegyelmi = new List<int>();
            List<int> megvaltozottFegyelmi = new List<int>();
            List<int> toroltFegyelmi = new List<int>();

            if (model.fegyelmiUgyIds.Count > 1)
                throw new WarningException("Ez a modal nem nyitható meg többes szerkesztésre");
            if (model.naplobejegyzesIds != null && model.naplobejegyzesIds.Count > 1)
                throw new WarningException("Ez a modal nem nyitható meg többes szerkesztésre");

            using (DbContextTransaction transaction = KonasoftBVFonixContext.Database.BeginTransaction())
            {
                try
                {
                    var fegyelmiUgyId = model.fegyelmiUgyIds.First();
                    var fegyelmiUgy = Table.Single(x => x.Id == fegyelmiUgyId);

                    Naplo naploEnitity = new Naplo
                    {
                        FegyelmiUgyId = fegyelmiUgyId,
                        FogvatartottId = fegyelmiUgy.FogvatartottId,
                        Hatarido = model.Datum,
                        JegyzokonyvTartalma = model.Leiras,
                        RogzitoIntezetId = aktIntezet,
                        TipusCimkeId = (int)FegyelmiNaploTipus.KozvetitoiEljarasHataridoModositas,
                    };
                    KonasoftBVFonixContext.Naplo.Add(naploEnitity);
                    KonasoftBVFonixContext.SaveChanges();

                    fegyelmiUgy.Hatarido = model.Datum;
                    fegyelmiUgy.HataridoModositasJavaslat = false;
                    KonasoftBVFonixContext.SaveChanges();

                    megvaltozottFegyelmi.Add(fegyelmiUgyId);


                    transaction.Commit();
                }
                catch (Exception e)
                {
                    Log.Error($"Hiba a közvetítői eljárás határidő módosítása során (fegyelmiUgyId: {model.fegyelmiUgyIds.First()})", e);
                    transaction.Rollback();

                    throw;
                }
            }

            OnFegyelmiUgyValtozas?.Invoke(ujFegyelmi, megvaltozottFegyelmi, toroltFegyelmi);

            return naploIds;
        }

        public KozvetitoiEljarasLezarasaModel GetKozvetitoiEljarasLezarasaModalData(List<int> fegyelmiUgyIds, List<int> naplobejegyzesIds)
        {
            if (fegyelmiUgyIds.Count > 1)
                throw new WarningException("Ez a modal nem nyitható meg többes szerkesztésre.");
            if (naplobejegyzesIds != null && naplobejegyzesIds.Count > 1)
                throw new WarningException("Ez a modal nem nyitható meg többes szerkesztésre.");

            var fegyelmiUgyId = fegyelmiUgyIds.First();
            var fegyelmiUgy = Table.Single(x => x.Id == fegyelmiUgyId);

            KozvetitoiEljarasLezarasaModel model = new KozvetitoiEljarasLezarasaModel()
            {
                FegyelmiUgyIds = fegyelmiUgyIds,
                NaplobejegyzesIds = naplobejegyzesIds,
                FegyelmiUgyStatusz = fegyelmiUgy.StatuszCimkeId,
            };
            if (!fegyelmiUgy.GaranciaTeljesultFl.HasValue)
                throw new Exception("A GaranciaTeljesult mező nincs kitöltve! KozvetitoiEljarasLezarasa folyamat előfeltétele az oszlop kitöltöttsége");

            if (fegyelmiUgy.GaranciaTeljesultFl.Value)
                model.EljarasEredmenyeOptions = Enum.GetValues(typeof(FegyelmiKozvetitoEljarasGarancialisEredmenyei)).Cast<FegyelmiKozvetitoEljarasGarancialisEredmenyei>().ToList().Select(s => new KSelect2ItemModel() { id = s.CastToBool().ToString(), text = s.ToDescriptionString() }).ToList();
            else
                model.EljarasEredmenyeOptions = Enum.GetValues(typeof(FegyelmiKozvetitoEljarasGaranciaNemTeljesultEredmenyei)).Cast<FegyelmiKozvetitoEljarasGaranciaNemTeljesultEredmenyei>().ToList().Select(s => new KSelect2ItemModel() { id = s.CastToBool().ToString(), text = s.ToDescriptionString() }).ToList();

            return model;
        }

        public List<int> SaveKozvetitoiEljarasLezarasaModalData(KozvetitoiEljarasLezarasaModel model)
        {
            var aktIntezet = JogosultsagCacheFunctions.AktualisIntezet.Id;
            List<int> ujFegyelmi = new List<int>();
            List<int> megvaltozottFegyelmi = new List<int>();
            List<int> toroltFegyelmi = new List<int>();
            List<int> naploIds = new List<int>();

            if (model.FegyelmiUgyIds.Count > 1)
                throw new WarningException("Ez a modal nem menthető többes szerkesztésből.");
            if (model.NaplobejegyzesIds != null && model.NaplobejegyzesIds.Count > 1)
                throw new WarningException("Ez a modal nem menthető többes szerkesztésből.");
            if (!model.EljarasEredmenye.HasValue)
                throw new WarningException("Kötelező az eljárás eredményét megadni");

            using (var transaction = KonasoftBVFonixContext.Database.BeginTransaction())
            {
                var currentId = 0;
                try
                {
                    currentId = model.FegyelmiUgyIds.First();
                    var fegyelmiUgy = Table.Single(x => x.Id == currentId);
                    fegyelmiUgy.KozvetitoiSikeres = model.EljarasEredmenye;
                    fegyelmiUgy.KozvetitoiEljarasban = false;
                    if (model.EljarasEredmenye == true)
                    {
                        fegyelmiUgy.Felfuggesztve = false;
                        fegyelmiUgy.HatarozatDatuma = null;
                        fegyelmiUgy.FelfuggesztesiJavaslat = false;
                        fegyelmiUgy.HataridoModositasJavaslat = false;

                        var felfuggesztes = KonasoftBVFonixContext.Felfuggesztesek.SingleOrDefault(s => s.FegyelmiUgyId == currentId && s.Vege == null);
                        felfuggesztes.Vege = DateTime.Now;
                    }
                    var entity = new Naplo()
                    {
                        FegyelmiUgyId = currentId,
                        FogvatartottId = fegyelmiUgy.FogvatartottId,
                        TipusCimkeId = (int)FegyelmiNaploTipus.KozvetitoiEljarasLezarasa,
                        AlairtaFl = true,
                        KozvetitoiSikeresFL = model.EljarasEredmenye,
                        GaranciaTeljesultFl = fegyelmiUgy.GaranciaTeljesultFl
                    };

                    if (model.EljarasEredmenye == false && fegyelmiUgy.StatuszCimkeId != (int)FegyelmiUgyStatusz.KivizsgalasFolyamatban)
                    {
                        FelfuggesztesMegszunteteseConfrimModal(model.FegyelmiUgyIds);
                    }

                    KonasoftBVFonixContext.Naplo.Add(entity);
                    KonasoftBVFonixContext.SaveChanges();
                    naploIds.Add(entity.Id);
                    megvaltozottFegyelmi.Add(currentId);

                    transaction.Commit();
                }
                catch (Exception e)
                {
                    Log.Error($"Hiba a közvetitöi eljárás lezárása során (fegyelmiUgyId: {currentId})", e);
                    transaction.Rollback();
                    throw;
                }
            }

            OnFegyelmiUgyValtozas?.Invoke(ujFegyelmi, megvaltozottFegyelmi, toroltFegyelmi);
            return naploIds;
        }
        public void FelfuggesztesMegszunteteseConfrimModal(List<int> fegyelmiUgyIds)
        {
            var aktIntezet = JogosultsagCacheFunctions.AktualisIntezet.Id;
            List<int> ujFegyelmi = new List<int>(0);
            List<int> megvaltozottFegyelmi = new List<int>();
            List<int> toroltFegyelmi = new List<int>(0);
            bool isNewTransaction = KonasoftBVFonixContext.Database.CurrentTransaction == null; // ha még nincs tranzakció folyamtban csak akkor csinálunk újat
            using (DbContextTransaction transaction = isNewTransaction ? KonasoftBVFonixContext.Database.BeginTransaction() : null)
            {
                int currentId = 0;
                try
                {
                    var timeStamp = DateTime.Now;
                    foreach (var fegyelmiUgyId in fegyelmiUgyIds)
                    {
                        currentId = fegyelmiUgyId;
                        var fegyelmiUgy = (FegyelmiUgyViewModel)Table.Single(x => x.Id == fegyelmiUgyId);
                        fegyelmiUgy.Felfuggesztve = false;
                        fegyelmiUgy.FelfuggesztesDatum = null;

                        var torvenyiHatarido = GetFegyelmiHatarido(fegyelmiUgyId, false);
                        fegyelmiUgy.Hatarido = torvenyiHatarido;

                        if (fegyelmiUgy.StatuszCimkeId == (int)CimkeEnums.FegyelmiUgyStatusz.ReintegraciosTisztiJogkorben)
                        {
                            fegyelmiUgy.KivizsgalasiHatarido = torvenyiHatarido;
                        }

                        Modify(fegyelmiUgy);

                        var naploEnitity = new Naplo
                        {
                            FegyelmiUgyId = fegyelmiUgyId,
                            FogvatartottId = fegyelmiUgy.FogvatartottId,
                            RogzitoIntezetId = aktIntezet,
                            Hatarido = torvenyiHatarido,
                            TipusCimkeId = (int)FegyelmiNaploTipus.FelfuggesztesMegszuntetese,
                        };
                        KonasoftBVFonixContext.Naplo.Add(naploEnitity);
                        var felfuggesztes = KonasoftBVFonixContext.Felfuggesztesek.Where(w => w.FegyelmiUgyId == fegyelmiUgyId && w.Vege == null).SingleOrDefault();
                        felfuggesztes.Vege = timeStamp;
                        FelfuggesztesFunctions.Modify((FelfuggesztesViewModel)felfuggesztes);

                    }
                    KonasoftBVFonixContext.SaveChanges();
                    if (isNewTransaction)
                        transaction.Commit();

                    megvaltozottFegyelmi.AddRange(fegyelmiUgyIds);
                }
                catch (Exception e)
                {
                    Log.Error($"Hiba a FelfuggesztesMegszunteteseConfrimModal mentés során (fegyelmiUgyId: {currentId})", e);
                    if (isNewTransaction)
                        transaction.Rollback();
                    throw;
                }
            }

            if (isNewTransaction)
                OnFegyelmiUgyValtozas?.Invoke(ujFegyelmi, megvaltozottFegyelmi, toroltFegyelmi);
        }

        public List<FelfuggesztesEmailData> UpdateAutomatikusFelfuggesztesTipusuUgyek(object o)
        {
            Log.Debug("UpdateAutomatikusFelfuggesztes függvény start");
            List<int> ujFegyelmi = new List<int>(0);
            List<int> megvaltozottFegyelmi = new List<int>();
            List<int> toroltFegyelmi = new List<int>(0);

            List<int> felfuggesztettUgyek = new List<int>();
            List<int> megszakitottFelfuggesztettUgyek = new List<int>();

            try
            {
                KonasoftBVFonixContext.Configuration.LazyLoadingEnabled = false;
                var osszesFegyelmiUgy = KonasoftBVFonixContext.FegyelmiUgyek
                    .Include(x => x.Fogvatartott)
                    .Where(x => CimkeEnums.AutomatikusFelfuggesztesStatuszLista.Contains(x.StatuszCimkeId)
                         && x.KozvetitoiEljarasban != true
                         && x.Lezarva != true
                         &&
                             ((x.Felfuggesztve != true
                             && x.IntezetId != x.Fogvatartott.IntezetId)
                         ||
                             (x.Felfuggesztve == true
                             && x.IntezetId == x.Fogvatartott.IntezetId
                             && x.FelfuggesztesOkaCimkeId == (int)CimkeEnums.FelfuggesztesOka.Tavollet)))
                    .ToList();

                foreach (var item in osszesFegyelmiUgy)
                {
                    using (var transaction = KonasoftBVFonixContext.Database.BeginTransaction())
                    {
                        var fegyelmiUgy = KonasoftBVFonixContext.FegyelmiUgyek.Single(x => x.Id == item.Id);
                        if ((item.Felfuggesztve != true)
                            && item.IntezetId != item.Fogvatartott.IntezetId)
                        {
                            var kezdete = DateTime.Now;
                            fegyelmiUgy.Felfuggesztve = true;
                            fegyelmiUgy.FelfuggesztesiJavaslat = false;
                            fegyelmiUgy.FelfuggesztesDatum = kezdete;
                            fegyelmiUgy.FelfuggesztesOkaCimkeId = (int)CimkeEnums.FelfuggesztesOka.Tavollet;
                            KonasoftBVFonixContext.SaveChanges();
                            fegyelmiUgy.Hatarido = GetFegyelmiHatarido(fegyelmiUgy.Id, false);
                            if (KonasoftBVFonixContext.Felfuggesztesek.Any(x => x.FegyelmiUgyId == fegyelmiUgy.Id && x.Vege == null))
                            {
                                Log.Error($"Hiba a(z) UpdateAutomatikusFelfuggesztesTipusuUgyek függvény hívásakor, már van felfüggesztése: {fegyelmiUgy.Id}");
                            }
                            else
                            {
                                KonasoftBVFonixContext.Felfuggesztesek.Add(new Felfuggesztes()
                                {
                                    Kezdete = kezdete,
                                    OkaCimkeId = (int)CimkeEnums.FelfuggesztesOka.Tavollet,
                                    FegyelmiUgyId = fegyelmiUgy.Id
                                });

                                var naploEnitity = new Naplo
                                {
                                    FegyelmiUgyId = fegyelmiUgy.Id,
                                    FogvatartottId = fegyelmiUgy.FogvatartottId,
                                    FelfuggesztesOkaCimkeId = (int)CimkeEnums.FelfuggesztesOka.Tavollet,
                                    TipusCimkeId = (int)FegyelmiNaploTipus.Felfuggesztes,
                                    AutomatikusFl = true
                                };

                                KonasoftBVFonixContext.Naplo.Add(naploEnitity);
                                felfuggesztettUgyek.Add(item.Id);
                                megvaltozottFegyelmi.Add(item.Id);
                            }
                        }
                        else if (item.Felfuggesztve == true
                            && item.IntezetId == item.Fogvatartott.IntezetId
                            && item.FelfuggesztesOkaCimkeId == (int)CimkeEnums.FelfuggesztesOka.Tavollet
                            && item.Fogvatartott.ZarkaId != null)
                        {
                            fegyelmiUgy.Felfuggesztve = false;
                            fegyelmiUgy.FelfuggesztesDatum = null;
                            KonasoftBVFonixContext.SaveChanges();
                            NaploFunctions naploFunctions = new NaploFunctions();
                            List<NaploListaViewmodel> naplok = new List<NaploListaViewmodel>();
                            var modositasFl = false;
                            try
                            {
                                naplok = naploFunctions.GetNaplobejegyzesekByFegyelmiUgyId(fegyelmiUgy.Id);
                                if (naplok.Any(x => x.TipusCimkeId == (int)FegyelmiNaploTipus.HataridoModositas))
                                {
                                    modositasFl = true;
                                }
                            }
                            catch (Exception e)
                            {
                                Log.Error($"Hiba a(z) UpdateAutomatikusFelfuggesztesTipusuUgyek belül naploFunctions.GetNaplobejegyzesekByFegyelmiUgyId függvény hívásakor", e);
                            }
                            var hatarido = GetFegyelmiHatarido(fegyelmiUgy.Id, modositasFl);

                            var felfuggesztes = KonasoftBVFonixContext.Felfuggesztesek.Where(w => w.FegyelmiUgyId == fegyelmiUgy.Id && w.Vege == null).SingleOrDefault();
                            if (felfuggesztes != null)
                            {
                                felfuggesztes.Vege = DateTime.Now;
                            }

                            var naploEnitity = new Naplo
                            {
                                FegyelmiUgyId = fegyelmiUgy.Id,
                                FogvatartottId = fegyelmiUgy.FogvatartottId,
                                TipusCimkeId = (int)FegyelmiNaploTipus.FelfuggesztesMegszuntetese,
                                KivizsgaloSzemelyId = fegyelmiUgy.KivizsgaloSzemelyId,
                                Hatarido = hatarido,
                                AutomatikusFl = true
                            };

                            KonasoftBVFonixContext.Naplo.Add(naploEnitity);
                            megszakitottFelfuggesztettUgyek.Add(item.Id);
                            megvaltozottFegyelmi.Add(item.Id);
                        }
                        KonasoftBVFonixContext.SaveChanges();
                        transaction.Commit();
                    }
                }

            }
            catch (Exception e)
            {
                Log.Error($"Hiba a(z) UpdateAutomatikusFelfuggesztesTipusuUgyek függvény hívásakor", e);
                //transaction.Rollback();
                throw;
            }

            OnFegyelmiUgyValtozas?.Invoke(ujFegyelmi, megvaltozottFegyelmi, toroltFegyelmi);
            Log.Debug("UpdateAutomatikusFelfuggesztes függvény end");
            return FegyelmiUgyValtozasEmailAdatok(felfuggesztettUgyek, megszakitottFelfuggesztettUgyek);
        }
        public FegyelmiElkulonitesElrendeleseFelulvizsgalataMegszunteteseModel GetFegyelmiElkulonitesElrendelesFelulvizsgataMegszuntetese(List<int> fegyelmiUgyIds, List<int> naploIds)
        {
            var aktIntezet = JogosultsagCacheFunctions.AktualisIntezet.Id;
            var fegyelmiUgyId = fegyelmiUgyIds.First();
            FegyelmiElkulonitesElrendeleseFelulvizsgalataMegszunteteseModel model;

            model = new FegyelmiElkulonitesElrendeleseFelulvizsgalataMegszunteteseModel()
            {
                FegyelmiUgyIds = fegyelmiUgyIds,
                ElrendeloId = WindowsIdentity.GetCurrent().User.Value,
            };


            var isElkulonitve = KonasoftBVFonixContext.Naplo.Where(w => w.FegyelmiUgyId == fegyelmiUgyId && w.TipusCimkeId == (int)FegyelmiNaploTipus.Elkulonites).OrderByDescending(o => o.ErvenyessegKezdete).FirstOrDefault();
            var currentEszleloId = AlkalmazasKontextusFunctions.Kontextus.SzemelyzetId;
            var jogkorGyakorloUsers = SzemelyzetFunctions.GetFegyelmiJogkorGyakorlojaAlkalmazottak();
            var jogkorGyakorlokUsersList = new List<AdFegyelmiUserItem>();
            model.ElrendeloOptions = jogkorGyakorloUsers.Select(x => new KSelect2ItemModel() { id = x.Sid, text = x.Displayname + (x.Rendfokozat == null ? "" : (" " + x.Rendfokozat)) }).ToList();
            var fegyelmiUgy = FindById(fegyelmiUgyId);
            IEnumerable<IdLabelWithChildren> objektumok = GetElhelyezesek(AlkalmazasKontextusFunctions.Kontextus.RogzitoIntezetId, false, false, fegyelmiUgy.FogvatartottId);

            //megnézzük van-e már a elkülönítés elrendelve a naplóból
            if (isElkulonitve != null)
            {
                model.ElkulonitesOka = isElkulonitve.ElkulonitesOka;
                model.Rendelkezesek = isElkulonitve.ElkulonitesRendelkezesek;
                model.ElrendelesIdeje = isElkulonitve.ElkulonitesElrendelesIdeje;
                model.ZarkabaHelyezes = isElkulonitve.EgyebAdatokJson;
                model.ElrendeloId = SzemelyzetFunctions.FindById(isElkulonitve.DonteshozoSzemelyId.Value).AdSid;

                if (!string.IsNullOrEmpty(isElkulonitve.EgyebAdatokJson))
                {

                    string[] elhelyezes = isElkulonitve.EgyebAdatokJson.Split('_');
                    int zarkaId = int.Parse(elhelyezes[2]);
                    var defaultZarkaOptions = GetObejktumRelszlegZarkaByZarkaId(zarkaId);
                    model.ZarkabaHelyezesObjektumNev = defaultZarkaOptions.Objektum;
                    model.ZarkabaHelyezesReszlegNev = defaultZarkaOptions.Reszleg;
                    model.ZarkabaHelyezesZarkaNev = defaultZarkaOptions.Zarka;

                }
                model.ZarkabaHelyezesOptions = objektumok.ToList();
                model.IsFelulvizsgalva = isElkulonitve.ElkulonitesFelulvizsgalvaFL.Value;
                model.MegszuntetesIdeje = isElkulonitve.ElkulonitesMegszuntetesenekIdeje;

                //model.MegszuntetesIdeje = isElkulonitve.ElkulonitesElrendelesIdeje;
            }
            else
            {

                var fogvatartottZarka = GetFogvatartottZarka(fegyelmiUgy.FogvatartottId);
                if (fogvatartottZarka != null)
                {
                    //csak akkor tötljük ki az elhelyzését, ha zárkába van jelenleg helyezve
                    model.ZarkabaHelyezes = fogvatartottZarka;
                    string[] elhelyezes = fogvatartottZarka.Split('_');
                    int zarkaId = int.Parse(elhelyezes[2]);
                    var defaultZarkaOptions = GetObejktumRelszlegZarkaByZarkaId(zarkaId);
                    model.ZarkabaHelyezesObjektumNev = defaultZarkaOptions.Objektum;
                    model.ZarkabaHelyezesReszlegNev = defaultZarkaOptions.Reszleg;
                    model.ZarkabaHelyezesZarkaNev = defaultZarkaOptions.Zarka;
                }
                model.ElrendelesIdeje = DateTime.Now;

                model.ZarkabaHelyezesOptions = objektumok.ToList();
                model.IsFelulvizsgalva = false;
            }

            return model;
        }
        public List<int> SaveElkulonitesElrendelesVagyElkulonitesMegszuntetes(FegyelmiElkulonitesElrendeleseFelulvizsgalataMegszunteteseModel model)
        {

            if (model.FegyelmiUgyIds == null || model.FegyelmiUgyIds.Count > 1)
            {
                throw new WarningException("A tömeges elkülönítés nem lehetséges!");
            }
            if (model.MegszuntetesIdeje != null && model.ZarkabaHelyezes == null)
            {
                throw new WarningException("Kérem helyezze zárkába a fogvatartottat! Elkülönítés megszüntetése után melyik zárkába kerül a fogvatartott?");
            }
            var fegyelmiUgyId = model.FegyelmiUgyIds.First();
            var aktIntezet = JogosultsagCacheFunctions.AktualisIntezet.Id;
            var isElkulonitve = KonasoftBVFonixContext.Naplo.Where(w => w.FegyelmiUgyId == fegyelmiUgyId && w.TipusCimkeId == (int)FegyelmiNaploTipus.Elkulonites).OrderByDescending(o => o.ErvenyessegKezdete).FirstOrDefault();
            var fegyelmiUgy = FindById(fegyelmiUgyId);
            if (isElkulonitve != null && fegyelmiUgy.ElkulonitveFl == false)
            {
                throw new WarningException("A fogvatarottnak már volt elkülönítése ami meg lett szüntetve! Új létrehozása vagy módosítása nem lehetséges");
            }
            var elhelyezesStr = string.Empty;
            int zarkaId = 0;
            int agyId = 0;
            if (!string.IsNullOrWhiteSpace(model.ZarkabaHelyezes))
            {
                try
                {
                    string[] elhelyezes = model.ZarkabaHelyezes.Split('_');
                    zarkaId = int.Parse(elhelyezes[2]);
                    agyId = int.Parse(elhelyezes[3]);
                    elhelyezesStr = GetElhelyezesNevByString(model.ZarkabaHelyezes);
                }
                catch (Exception e)
                {
                    Log.Error("A model.ZarkabaHelyezes feldolgozása közben hiba történt!", e);
                    throw;
                }

            }
            List<int> naploIds = new List<int>();
            List<int> ujFegyelmi = new List<int>(0);
            List<int> megvaltozottFegyelmi = new List<int>();
            List<int> toroltFegyelmi = new List<int>(0);

            using (var transaction = KonasoftBVFonixContext.Database.BeginTransaction())
            {
                try
                {

                    if (isElkulonitve == null)
                    {
                        //ha nincs még neki elkülönítése

                        fegyelmiUgy.ElkulonitveFl = true;
                        Modify(fegyelmiUgy);

                        var naploEntity = new Naplo()
                        {
                            FegyelmiUgyId = fegyelmiUgyId,
                            RogzitoIntezetId = aktIntezet,
                            TipusCimkeId = (int)FegyelmiNaploTipus.Elkulonites,
                            EgyebAdatokJson = model.ZarkabaHelyezes,
                            ElkulonitesOka = model.ElkulonitesOka,
                            ElkulonitesRendelkezesek = model.Rendelkezesek,
                            ElkulonitesElrendelesIdeje = model.ElrendelesIdeje,
                            ElkulonitesFelulvizsgalvaFL = model.IsFelulvizsgalva,
                            JegyzokonyvTartalma = elhelyezesStr,
                            FogvatartottId = fegyelmiUgy.FogvatartottId,
                            DonteshozoSzemelyId = SzemelyzetFunctions.SzemelyzetLekeresVagyLetrehozas(model.ElrendeloId, null, null).Id,
                        };

                        if (!string.IsNullOrWhiteSpace(model.ZarkabaHelyezes))
                        {
                            var fogvatartottZarka = GetFogvatartottZarka(fegyelmiUgy.FogvatartottId);
                            if (model.ZarkabaHelyezes == fogvatartottZarka)
                            {
                                //ha ugyan abba a zárkába különítik el, ahol alapból van

                                try
                                {
                                    //try
                                    //{
                                    //    new FegyelmiSzervizClient().FegyelmiElkulonitesZarkabaHelyezes(
                                    //       AlkalmazasKontextusFunctions.Kontextus.SzemelyzetSid,
                                    //       AlkalmazasKontextusFunctions.Kontextus.RogzitoIntezetId,
                                    //       fegyelmiUgy.FogvatartottId,
                                    //       zarkaId,
                                    //       agyId,
                                    //       model.ElrendelesIdeje.Value);
                                    //}
                                    //catch (Exception ex)
                                    //{
                                    //    Log.Error("FegyelmiSzervizclient().FegyelmiElkulonitesZarkabaHelyezes hívás hiba: ", ex);
                                    //}

                                    // Főnix3 zárkába helyezés
                                    //F3ZarkabaHelyezesFunctions.ZarkabaHelyezes((int)Modul.Fenyites, zarkaId, fegyelmiUgy.FogvatartottId, agyId, DateTime.Now, fegyelmiUgyId, 42039, model.NocheckVegrehajtasiFok);
                                }
                                //catch (F3ZarkabaHelyezesFunctions.VegrehajtasiFokException)
                                //{
                                //    throw;
                                //}
                                catch (Exception e)
                                {
                                    Log.Error($"Hiba az elkülönítés elrendelése szerzíz hívás során (fegyelmiUgyId: {fegyelmiUgy.Id})", e);
                                    throw new WarningException($"Hiba az elkülönítés elrendelése során! ({e.Message})");
                                }

                            }
                            else
                            {
                                //ha másik zárkát állítanak be neki, mint ami alapból van

                                try
                                {
                                    //try
                                    //{
                                    //    new FegyelmiSzervizClient().FegyelmiElkulonitesZarkabaHelyezes(
                                    //        AlkalmazasKontextusFunctions.Kontextus.SzemelyzetSid,
                                    //        AlkalmazasKontextusFunctions.Kontextus.RogzitoIntezetId,
                                    //        fegyelmiUgy.FogvatartottId,
                                    //        zarkaId,
                                    //        agyId,
                                    //        model.ElrendelesIdeje.Value);
                                    //}
                                    //catch(Exception ex)
                                    //{
                                    //    Log.Error("FegyelmiSzervizclient().FegyelmiElkulonitesZarkabaHelyezes hívás hiba: ", ex);
                                    //}

                                    // Főnix3 zárkába helyezés
                                    //F3ZarkabaHelyezesFunctions.ZarkabaHelyezes((int)Modul.Fenyites, zarkaId, fegyelmiUgy.FogvatartottId, agyId, DateTime.Now, fegyelmiUgyId, 42039, model.NocheckVegrehajtasiFok);
                                }
                                //catch(F3ZarkabaHelyezesFunctions.VegrehajtasiFokException)
                                //{
                                //    throw;
                                //}
                                catch (Exception e)
                                {
                                    Log.Error($"Hiba az elkülönítés elrendelése - Zárkába helyezés - szerzíz hívás során (fegyelmiUgyId: {fegyelmiUgy.Id})", e);
                                    throw new WarningException($"Hiba az elkülönítés módosítása során! ({e.Message})");

                                }
                            }

                        }

                        KonasoftBVFonixContext.Naplo.Add(naploEntity);
                        KonasoftBVFonixContext.SaveChanges();
                        naploIds.Add(naploEntity.Id);


                        transaction.Commit();
                    }
                    else
                    {
                        bool isElkulonitesMegszuntetes = false;

                        //ha megszüntetjük vagy felülbíráljuk az elkülönítést
                        if (model.MegszuntetesIdeje != null || model.MegszuntetesIdeje.HasValue)
                        {
                            fegyelmiUgy.ElkulonitveFl = false;
                            Modify(fegyelmiUgy);
                            isElkulonitesMegszuntetes = true;
                        }
                        //megnézzük, hogy változott-e a zárka
                        if (elhelyezesStr != isElkulonitve.JegyzokonyvTartalma)
                        {
                            //ha igen, akkor naplóba mentjük és meghívjuk a zárka szervízt
                            var naploEntity = new Naplo()
                            {
                                FegyelmiUgyId = fegyelmiUgyId,
                                RogzitoIntezetId = aktIntezet,
                                TipusCimkeId = (int)FegyelmiNaploTipus.Elkulonites,
                                ElkulonitesFelulvizsgalvaFL = model.IsFelulvizsgalva,
                                JegyzokonyvTartalma = elhelyezesStr,
                                EgyebAdatokJson = model.ZarkabaHelyezes,
                                ElkulonitesOka = model.ElkulonitesOka,
                                ElkulonitesRendelkezesek = model.Rendelkezesek,
                                ElkulonitesElrendelesIdeje = model.ElrendelesIdeje,
                                ElkulonitesMegszuntetesenekIdeje = model.MegszuntetesIdeje,
                                FogvatartottId = fegyelmiUgy.FogvatartottId,
                                IsFogvatartottElfogadta = true, // ezzel a flaggel jelzem, hogy módosította a fegyelmi ügyet
                                DonteshozoSzemelyId = SzemelyzetFunctions.SzemelyzetLekeresVagyLetrehozas(model.ElrendeloId, null, null).Id,

                            };
                            if (!string.IsNullOrWhiteSpace(model.ZarkabaHelyezes))
                            {
                                var fogvatartottZarka = GetFogvatartottZarka(fegyelmiUgy.FogvatartottId);
                                if (model.ZarkabaHelyezes == fogvatartottZarka)
                                {
                                    //ha ugyan abba a zárkába különítik el, ahol alapból van

                                    try
                                    {
                                        //try
                                        //{
                                        //    new FegyelmiSzervizClient().FegyelmiElkulonitesZarkabaHelyezes(
                                        //       AlkalmazasKontextusFunctions.Kontextus.SzemelyzetSid,
                                        //       AlkalmazasKontextusFunctions.Kontextus.RogzitoIntezetId,
                                        //       fegyelmiUgy.FogvatartottId,
                                        //       zarkaId,
                                        //       agyId,
                                        //       DateTime.Now);
                                        //}
                                        //catch(Exception ex)
                                        //{
                                        //    Log.Error("FegyelmiSzervizclient().FegyelmiElkulonitesZarkabaHelyezes hívás hiba: ", ex);
                                        //}

                                        // Főnix3 zárkába helyezés
                                        //F3ZarkabaHelyezesFunctions.ZarkabaHelyezes((int)Modul.Fenyites, zarkaId, fegyelmiUgy.FogvatartottId, agyId, DateTime.Now, fegyelmiUgyId, 42039, model.NocheckVegrehajtasiFok);
                                    }
                                    //catch (F3ZarkabaHelyezesFunctions.VegrehajtasiFokException)
                                    //{
                                    //    throw;
                                    //}
                                    catch (Exception e)
                                    {
                                        Log.Error($"Hiba az elkülönítés elrendelése szerzíz hívás során (fegyelmiUgyId: {fegyelmiUgy.Id})", e);
                                        throw new WarningException($"Hiba az elkülönítés elrendelése során! ({e.Message})");
                                    }

                                }

                                try
                                {
                                    if (isElkulonitesMegszuntetes)
                                    {
                                        //megszütetik az elkülönítést úgy, hogy közben másik zárkába helyezik

                                        //try
                                        //{
                                        //    new FegyelmiSzervizClient().MegszuntetElkulonitesEsZarkabaBehelyez(
                                        //    AlkalmazasKontextusFunctions.Kontextus.SzemelyzetSid,
                                        //    AlkalmazasKontextusFunctions.Kontextus.RogzitoIntezetId,
                                        //    fegyelmiUgy.FogvatartottId,
                                        //    zarkaId,
                                        //    agyId,
                                        //    model.MegszuntetesIdeje.HasValue ? model.MegszuntetesIdeje.Value : DateTime.Now);
                                        //}
                                        //catch(Exception ex)
                                        //{
                                        //    Log.Error("FegyelmiSzervizclient().MegszuntetElkulonitesEsZarkabaBehelyez hívás hiba: ", ex);
                                        //}

                                        // Főnix3 zárkába helyezés
                                        //F3ZarkabaHelyezesFunctions.ZarkabaHelyezes((int)Modul.Fenyites, zarkaId, fegyelmiUgy.FogvatartottId, agyId, DateTime.Now, fegyelmiUgyId, 42082, model.NocheckVegrehajtasiFok);
                                    }
                                    else
                                    {
                                        //módosítják elhelyezés közben a zárkát

                                        //try
                                        //{
                                        //    new FegyelmiSzervizClient().FegyelmiElkulonitesZarkabaHelyezes(
                                        //    AlkalmazasKontextusFunctions.Kontextus.SzemelyzetSid,
                                        //    AlkalmazasKontextusFunctions.Kontextus.RogzitoIntezetId,
                                        //    fegyelmiUgy.FogvatartottId,
                                        //    zarkaId,
                                        //    agyId,
                                        //    DateTime.Now);
                                        //}
                                        //catch(Exception ex)
                                        //{
                                        //    Log.Error("FegyelmiSzervizclient().FegyelmiElkulonitesZarkabaHelyezes hívás hiba: ", ex);
                                        //}

                                        // Főnix3 tárkába helyezés
                                        //F3ZarkabaHelyezesFunctions.ZarkabaHelyezes((int)Modul.Fenyites, zarkaId, fegyelmiUgy.FogvatartottId, agyId, DateTime.Now, fegyelmiUgyId, 42039, model.NocheckVegrehajtasiFok);
                                    }

                                }
                                //catch (F3ZarkabaHelyezesFunctions.VegrehajtasiFokException)
                                //{
                                //    throw;
                                //}
                                catch (Exception e)
                                {
                                    Log.Error($"Hiba az elkülönítés megszüntetése vagy felülbírálása - Zárkából kihelyezés - szerzíz hívás során (fegyelmiUgyId: {fegyelmiUgy.Id})", e);
                                    throw new WarningException($"Hiba az elkülönítés során! ({e.Message})");
                                }
                            }
                            KonasoftBVFonixContext.Naplo.Add(naploEntity);
                            KonasoftBVFonixContext.SaveChanges();
                            naploIds.Add(naploEntity.Id);
                            transaction.Commit();
                        }
                        else
                        {
                            //zárka -elhelyezés- nem változott
                            if (isElkulonitesMegszuntetes)
                            {
                                //megszüntetik az elkülönítést
                                if (!string.IsNullOrWhiteSpace(model.ZarkabaHelyezes))
                                {
                                    //ha volt neki beállítva zárka

                                    try
                                    {
                                        //try
                                        //{
                                        //    new FegyelmiSzervizClient().MegszuntetElkulonitesEsZarkabaBehelyez(
                                        //       AlkalmazasKontextusFunctions.Kontextus.SzemelyzetSid,
                                        //       AlkalmazasKontextusFunctions.Kontextus.RogzitoIntezetId,
                                        //       fegyelmiUgy.FogvatartottId,
                                        //       zarkaId,
                                        //       agyId,
                                        //       model.MegszuntetesIdeje.Value);
                                        //}
                                        //catch(Exception ex)
                                        //{
                                        //    Log.Error("FegyelmiSzervizclient().MegszuntetElkulonitesEsZarkabaBehelyez hívás hiba: ", ex);
                                        //}

                                        // Főnix3 zárkába helyezés
                                        //F3ZarkabaHelyezesFunctions.ZarkabaHelyezes((int)Modul.Fenyites, zarkaId, fegyelmiUgy.FogvatartottId, agyId, DateTime.Now, fegyelmiUgyId, 42082, model.NocheckVegrehajtasiFok);
                                    }
                                    //catch (F3ZarkabaHelyezesFunctions.VegrehajtasiFokException)
                                    //{
                                    //    throw;
                                    //}
                                    catch (Exception e)
                                    {
                                        Log.Error($"Hiba az elkülönítés elrendelése szerzíz hívás során (fegyelmiUgyId: {fegyelmiUgy.Id})", e);
                                        throw new WarningException($"Hiba az elkülönítés megszüntetés során! ({e.Message})");

                                    }
                                }
                            }

                            var naploEntity = new Naplo()
                            {
                                FegyelmiUgyId = fegyelmiUgyId,
                                RogzitoIntezetId = aktIntezet,
                                TipusCimkeId = (int)FegyelmiNaploTipus.Elkulonites,
                                ElkulonitesFelulvizsgalvaFL = model.IsFelulvizsgalva,
                                ElkulonitesOka = model.ElkulonitesOka,
                                ElkulonitesRendelkezesek = model.Rendelkezesek,
                                ElkulonitesElrendelesIdeje = model.ElrendelesIdeje,
                                JegyzokonyvTartalma = elhelyezesStr,
                                EgyebAdatokJson = model.ZarkabaHelyezes,
                                ElkulonitesMegszuntetesenekIdeje = model.MegszuntetesIdeje,
                                IsFogvatartottElfogadta = true, // ezzel a flaggel jelzem, hogy módosította a fegyelmi ügyet
                                DonteshozoSzemelyId = SzemelyzetFunctions.SzemelyzetLekeresVagyLetrehozas(model.ElrendeloId, null, null).Id,
                            };
                            KonasoftBVFonixContext.Naplo.Add(naploEntity);
                            KonasoftBVFonixContext.SaveChanges();
                            naploIds.Add(naploEntity.Id);

                            //new F3ElkulonitesekFunctions().ModifyElkulonites(model);

                            // teszt
                            //new F3VegrehajtasiListaFunctions().BefogadoBizottsagiUlesElojegyzes(
                            //        AlkalmazasKontextusFunctions.Kontextus.SzemelyzetSid,
                            //        AlkalmazasKontextusFunctions.Kontextus.RogzitoIntezetId,
                            //        fegyelmiUgy.FogvatartottId,
                            //        DateTime.Now,
                            //        fegyelmiUgyId);

                            transaction.Commit();
                        }

                        //new F3ElkulonitesekFunctions().ModifyElkulonites(model);
                    }
                }
                //catch (F3ZarkabaHelyezesFunctions.VegrehajtasiFokException)
                //{
                //    transaction.Rollback();
                //    throw;
                //}
                catch (Exception e)
                {
                    Log.Error($"Hiba az SaveElkulonitesElrendelesVagyElkulonitesMegszuntetes függvény során, a (fegyelmiUgyId: {fegyelmiUgy.Id})", e);
                    transaction.Rollback();
                    throw new WarningException(e.Message);
                }
            }

            megvaltozottFegyelmi.Add(fegyelmiUgyId);
            OnFegyelmiUgyValtozas?.Invoke(ujFegyelmi, megvaltozottFegyelmi, toroltFegyelmi);
            return naploIds;
        }
        private string GetFogvatartottZarka(int fogvatartottId)
        {
            var returnString = "";
            var fogvatartot = KonasoftBVFonixContext.Fogvatartottak.FirstOrDefault(f => f.Id == fogvatartottId);
            if (fogvatartot.ZarkaId.HasValue && fogvatartot.ZarkaAgy.HasValue)
            {
                returnString = $"{fogvatartot.IntezetiObjektumId}_{fogvatartot.KorletId}_{fogvatartot.ZarkaId ?? 0}_{fogvatartot.ZarkaAgy ?? 0}";
                return returnString;
            }
            else
            {
                return null;
            }
        }
        private SzabadZarkaModel GetObejktumRelszlegZarkaByZarkaId(int zarkaId)
        {
            var model = (from zarka in KonasoftBVFonixContext.Zarka
                         where zarka.Id == zarkaId
                         select new SzabadZarkaModel
                         {
                             Objektum = zarka.IntezetiObjektum.Nev,
                             Reszleg = zarka.Korlet.Nev,
                             Zarka = zarka.Azonosito,

                         }).SingleOrDefault();
            return model;
        }

        public FeljelentesRogziteseModel GetBuntetoFeljelentesRogziteseModalData(List<int> fegyelmiUgyIds, List<int> naplobejegyzesIds)
        {
            if (fegyelmiUgyIds.Count > 1)
                throw new WarningException("Ez a modal nem nyitható meg többes szerkesztésre.");
            if (naplobejegyzesIds != null && naplobejegyzesIds.Count > 1)
                throw new WarningException("Ez a modal nem nyitható meg többes szerkesztésre.");

            var fegyelmiUgyId = fegyelmiUgyIds.First();
            var fegyelmiUgy = Table.Single(x => x.Id == fegyelmiUgyId);

            FeljelentesRogziteseModel model = new FeljelentesRogziteseModel()
            {
                FegyelmiUgyIds = fegyelmiUgyIds,
                NaplobejegyzesIds = naplobejegyzesIds
            };
            model.ElintezesModjaOptions = CimkeFunctions.GetCimkekByFelhoId((int)Felhok.FeljelentesElintezesModja)
                .ToList()
                .Select(s => new KSelect2ItemModel() { id = s.Id.ToString(), text = s.Nev })
                .ToList();
            model.FeljelentesMinositeseOptions = CimkeFunctions.GetCimkekByFelhoId((int)Felhok.FeljelentesMinositese)
               .ToList()
               .Select(s => new KSelect2ItemModel() { id = s.Id.ToString(), text = s.Nev })
               .ToList();
            model.FenyitesKiszabasIdeje = DateTime.Now;
            var naplo = KonasoftBVFonixContext.Naplo.FirstOrDefault(n => n.FegyelmiUgyId == fegyelmiUgyId && n.TipusCimkeId == (int)CimkeEnums.FegyelmiNaploTipus.BuntetoFeljelentesRogzitese);
            if (naplo != null)
            {
                model.FenyitesKiszabasIdeje = naplo.Hatarido.Value;
                model.FeljelentesMinositeseId = naplo.TitulusCimkeId ?? 0;
                model.ElintezesModjaId = naplo.TorvenyszekId ?? 0;
                model.Leiras = naplo.JegyzokonyvTartalma ?? "";
            }

            if (model.Leiras == null || model.Leiras.Trim() == "") model.Leiras = "<p></p>";

            return model;
        }

        public List<int> SaveBuntetoFeljelentesRogziteseModalData(FeljelentesRogziteseModel model)
        {

            if (model.Leiras.StartsWith("<p><p>"))
            {
                model.Leiras = model.Leiras.Remove(model.Leiras.IndexOf("<p>"), "<p>".Length);
                model.Leiras = model.Leiras.Remove(model.Leiras.LastIndexOf("</p>"));
            }

            var aktIntezet = JogosultsagCacheFunctions.AktualisIntezet.Id;
            List<int> ujFegyelmi = new List<int>();
            List<int> megvaltozottFegyelmi = new List<int>();
            List<int> toroltFegyelmi = new List<int>();
            List<int> naploIds = new List<int>();

            if (model.FegyelmiUgyIds.Count > 1)
                throw new WarningException("Ez a modal nem menthető többes szerkesztésből.");
            if (model.NaplobejegyzesIds != null && model.NaplobejegyzesIds.Count > 1)
                throw new WarningException("Ez a modal nem menthető többes szerkesztésből.");

            using (var transaction = KonasoftBVFonixContext.Database.BeginTransaction())
            {
                var currentId = 0;
                try
                {
                    currentId = model.FegyelmiUgyIds.First();
                    var fegyelmiUgy = KonasoftBVFonixContext.FegyelmiUgyek.Single(x => x.Id == currentId);

                    var entity = KonasoftBVFonixContext.Naplo.FirstOrDefault(n => n.FegyelmiUgyId == currentId && n.TipusCimkeId == (int)CimkeEnums.FegyelmiNaploTipus.BuntetoFeljelentesRogzitese);
                    if (entity == null)
                    {
                        entity = new Naplo()
                        {
                            FegyelmiUgyId = currentId,
                            FogvatartottId = fegyelmiUgy.FogvatartottId,
                            TipusCimkeId = (int)FegyelmiNaploTipus.BuntetoFeljelentesRogzitese
                        };
                        KonasoftBVFonixContext.Naplo.Add(entity);
                        KonasoftBVFonixContext.SaveChanges();
                    }
                    entity.Hatarido = model.FenyitesKiszabasIdeje;
                    entity.TitulusCimkeId = model.FeljelentesMinositeseId;
                    entity.TorvenyszekId = model.ElintezesModjaId;
                    var feljelentesminositesText = CimkeFunctions.FindById(model.FeljelentesMinositeseId)?.Nev;
                    var elintezesModjaText = CimkeFunctions.FindById(model.ElintezesModjaId)?.Nev;
                    entity.VedoCime = feljelentesminositesText;
                    entity.VedoElerhetosege = elintezesModjaText;
                    entity.JegyzokonyvTartalma = model.Leiras;
                    fegyelmiUgy.FeljelentesFl = true;

                    KonasoftBVFonixContext.SaveChanges();

                    naploIds.Add(entity.Id);
                    megvaltozottFegyelmi.Add(currentId);

                    transaction.Commit();
                }
                catch (Exception e)
                {
                    Log.Error($"Hiba a feljelentés rögzítése során (fegyelmiUgyId: {currentId})", e);
                    transaction.Rollback();
                    throw;
                }
            }

            OnFegyelmiUgyValtozas?.Invoke(ujFegyelmi, megvaltozottFegyelmi, toroltFegyelmi);
            return naploIds;
        }


        public List<FelfuggesztesEmailData> FegyelmiUgyValtozasEmailAdatok(List<int> felfuggesztettUgyek, List<int> aktivraAllitottUgyek)
        {
            Log.Debug("FegyelmiUgyValtozasEmailAdatok függvény start");
            HashSet<int> felfuggesztettUgyekHash = new HashSet<int>(felfuggesztettUgyek);
            HashSet<int> aktivraAllitottUgyekHash = new HashSet<int>(aktivraAllitottUgyek);
            var emailList = new List<FelfuggesztesEmailData>();

            try
            {
                var valtozottUgyek = (from ugy in KonasoftBVFonixContext.FegyelmiUgyek
                                      join esemeny in KonasoftBVFonixContext.Esemenyek on ugy.EsemenyId equals esemeny.Id
                                      join fogv in KonasoftBVFonixContext.Fogvatartottak on ugy.FogvatartottId equals fogv.Id
                                      join fogvSzamAdat in KonasoftBVFonixContext.FogvatartottSzemelyesAdatai on fogv.Id equals fogvSzamAdat.FogvatartottId
                                      join kivSzemely in KonasoftBVFonixContext.Szemelyzet on ugy.KivizsgaloSzemelyId equals kivSzemely.Id
                                      join elrSzemely in KonasoftBVFonixContext.Szemelyzet on ugy.ElrendeloSzemelyId equals elrSzemely.Id
                                      join kivBeosztasKsz in KonasoftBVFonixContext.Kodszotar on kivSzemely.BeosztasKszId equals kivBeosztasKsz.Id into kivBeosztasKszL
                                      from kivBeosztasKszLeft in kivBeosztasKszL.DefaultIfEmpty()
                                      join elrBeosztasKsz in KonasoftBVFonixContext.Kodszotar on elrSzemely.BeosztasKszId equals elrBeosztasKsz.Id into elrBeosztasKszL
                                      from elrBeosztasKszLeft in elrBeosztasKszL.DefaultIfEmpty()

                                      where felfuggesztettUgyek.Contains(ugy.Id) || aktivraAllitottUgyek.Contains(ugy.Id)
                                      select new FegyelmiUgyItem
                                      {
                                          FegyelmiUgyId = ugy.Id,
                                          // A fegyelmi alkalmazás nem foglalkozik védett fogvatartottakkal
                                          FogvatartottNev = fogvSzamAdat.SzuletesiCsaladiNev_NE_HASZNALD + " " + fogvSzamAdat.SzuletesiUtonev_NE_HASZNALD,
                                          FogvatartottNyilvantartasiSzam = fogv.NyilvantartasiAzonosito,
                                          Ugyszam = ugy.UgySorszamaIntezetAzon + "/" + ugy.UgySorszamaEv + "/" + ugy.UgySorszama,
                                          EsemenyDatuma = esemeny.EsemenyDatuma,
                                          KivizsgaloSid = kivSzemely.AdSid,
                                          KivizsgaloNev = kivSzemely.Nev,
                                          ElrendeloSid = elrSzemely.AdSid,
                                          ElrendeloNev = elrSzemely.Nev,
                                          ElrendeloBeosztas = elrBeosztasKszLeft == null || elrSzemely.BeosztasKszId == 0 ? "" : elrBeosztasKszLeft.Nev,
                                          KivizsgaloBeosztas = kivBeosztasKszLeft == null || kivSzemely.BeosztasKszId == 0 ? "" : kivBeosztasKszLeft.Nev,
                                          FegyelmiUgyIntezetId = ugy.IntezetId,
                                      }
                                     ).ToList();

                if (valtozottUgyek != null)
                {
                    Dictionary<int, FegyelmiUgyItem> valtozottUgyekDict = valtozottUgyek.ToDictionary(x => x.FegyelmiUgyId);

                    Dictionary<string, ErtesitendoSzemely> ertesitendoSzemelyek = new Dictionary<string, ErtesitendoSzemely>();
                    Dictionary<string, ErtesitendoSzemely> ertesitendoSzemelyekFegyJogkorGyak = new Dictionary<string, ErtesitendoSzemely>();

                    // emaillel rendelkező személyek kigyűjtése
                    foreach (var ugy in valtozottUgyek)
                    {
                        var intezetAdat = new IntezetFunctions().FindById(ugy.FegyelmiUgyIntezetId);
                        var groupName = intezetAdat.Azonosito2 + "-" + "FN" + "-" + "Fegyelmi-jogkor-gyakorloja";
                        var jogkorgyakorlok = new ActiveDirectoryKezeloFunctions().KeresGroupFelhasznalok(groupName);

                        if (!ertesitendoSzemelyek.ContainsKey(ugy.ElrendeloSid))
                        {
                            var email = new ActiveDirectoryKezeloFunctions().KeresEmailcim(ugy.ElrendeloSid);
                            ertesitendoSzemelyek.Add(ugy.ElrendeloSid, new ErtesitendoSzemely()
                            {
                                Email = email,
                                SzemelySid = ugy.ElrendeloSid,
                                SzemelyNev = ugy.ElrendeloNev,
                                SzemelyBeosztas = ugy.ElrendeloBeosztas
                            });
                        }
                        if (!ertesitendoSzemelyek.ContainsKey(ugy.KivizsgaloSid))
                        {
                            var email = new ActiveDirectoryKezeloFunctions().KeresEmailcim(ugy.KivizsgaloSid);
                            ertesitendoSzemelyek.Add(ugy.KivizsgaloSid, new ErtesitendoSzemely()
                            {
                                Email = email,
                                SzemelySid = ugy.KivizsgaloSid,
                                SzemelyNev = ugy.KivizsgaloNev,
                                SzemelyBeosztas = ugy.KivizsgaloBeosztas
                            });
                        }

                        foreach (var jogkorgyakorlo in jogkorgyakorlok)
                        {
                            if (!ertesitendoSzemelyek.ContainsKey(jogkorgyakorlo.Sid.ToString()))
                            {
                                var email = new ActiveDirectoryKezeloFunctions().KeresEmailcim(jogkorgyakorlo.Sid.ToString());
                                var jogkorgyakorloBesztas = new ActiveDirectoryKezeloFunctions().KeresBeosztas(jogkorgyakorlo.Sid.ToString());

                                ertesitendoSzemelyek.Add(jogkorgyakorlo.Sid.ToString(), new ErtesitendoSzemely()
                                {
                                    Email = email,
                                    SzemelySid = jogkorgyakorlo.Sid.ToString(),
                                    SzemelyNev = jogkorgyakorlo.Name.ToString(),
                                    SzemelyBeosztas = jogkorgyakorloBesztas == null || jogkorgyakorloBesztas == "" ? "" : jogkorgyakorloBesztas
                                });
                            }
                        }
                    }
                    // nullok törlése
                    ertesitendoSzemelyek = ertesitendoSzemelyek.Where(x => x.Value.Email != null).ToDictionary(x => x.Key, x => x.Value);

                    // ügyek kigyűjtése személyekhez
                    foreach (var ugy in valtozottUgyek)
                    {
                        var intezetAdat = new IntezetFunctions().FindById(ugy.FegyelmiUgyIntezetId);
                        var groupName = intezetAdat.Azonosito2 + "-" + "FN" + "-" + "Fegyelmi-jogkor-gyakorloja";
                        var jogkorgyakorlok = new ActiveDirectoryKezeloFunctions().KeresGroupFelhasznalok(groupName);
                        if (ertesitendoSzemelyek.ContainsKey(ugy.ElrendeloSid))
                        {
                            ertesitendoSzemelyek[ugy.ElrendeloSid].FegyelmiIdList.Add(ugy.FegyelmiUgyId);
                        }

                        if (ertesitendoSzemelyek.ContainsKey(ugy.KivizsgaloSid))
                        {
                            ertesitendoSzemelyek[ugy.KivizsgaloSid].FegyelmiIdList.Add(ugy.FegyelmiUgyId);
                        }

                        if (aktivraAllitottUgyek.Contains(ugy.FegyelmiUgyId))
                        {
                            foreach (var jogkorgyakorlo in jogkorgyakorlok)
                            {
                                if (ertesitendoSzemelyek.ContainsKey(jogkorgyakorlo.Sid.ToString())
                                    && !jogkorgyakorlo.Sid.ToString().Equals(ugy.KivizsgaloSid)
                                    && !jogkorgyakorlo.Sid.ToString().Equals(ugy.ElrendeloSid))
                                {
                                    ertesitendoSzemelyek[jogkorgyakorlo.Sid.ToString()].FegyelmiIdList.Add(ugy.FegyelmiUgyId);
                                }
                            }
                        }
                    }
                    // ügy lista kulcs előállítás
                    foreach (var ertesites in ertesitendoSzemelyek)
                    {
                        ertesites.Value.FegyelmiKey = string.Join(",", ertesites.Value.FegyelmiIdList.OrderBy(x => x));
                    }

                    // azonos kiküldendő tartalomhoz emailek egyesítése
                    foreach (var item in ertesitendoSzemelyek.GroupBy(x => x.Value.FegyelmiKey))
                    {
                        if (item.Any(x => x.Value.FegyelmiKey != ""))
                        {
                            var email = new FelfuggesztesEmailItem();
                            foreach (var szemely in item.ToList())
                            {
                                email.ErtesitendoSzemelyek.Add(szemely.Value);
                            }
                            foreach (var ugyId in item.First().Value.FegyelmiIdList)
                            {
                                if (felfuggesztettUgyekHash.Contains(ugyId))
                                {
                                    email.FelfuggesztettFegyelmiList.Add(valtozottUgyekDict[ugyId]);
                                }
                                else if (aktivraAllitottUgyekHash.Contains(ugyId))
                                {
                                    email.AktivraAllitottFegyelmiList.Add(valtozottUgyekDict[ugyId]);
                                }
                            }
                            emailList.Add(email.GetEmailData());
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error("Az e-mail kilküldéséhez szükséges adatok összeállításánál hiba lépett fel: " + e);
                throw new WarningException("Az e-mail kilküldéséhez szükséges adatok összeállításánál hiba lépett fel: " + e);
            }
            Log.Debug("FegyelmiUgyValtozasEmailAdatok függvény end");

            return emailList;
        }

        public List<int> SaveMaganelzarasElrendeleseModalData(MaganelzarasElrendeleseModel model)
        {
            var aktIntezet = JogosultsagCacheFunctions.AktualisIntezet.Id;
            List<int> ujFegyelmi = new List<int>();
            List<int> megvaltozottFegyelmi = new List<int>();
            List<int> toroltFegyelmi = new List<int>();
            List<int> naploIds = new List<int>();
            List<int> felugyeloIds = new List<int>();

            if (model.Fofelugyelok != null)
            {
                foreach (var felugyelo in model.Fofelugyelok)
                {
                    var szemely = SzemelyzetFunctions.FindBySid(felugyelo, null);
                    if (szemely != null)
                    {
                        felugyeloIds.Add(szemely.Id);
                    }
                }
            }

            using (var transaction = KonasoftBVFonixContext.Database.BeginTransaction())
            {
                var currentId = 0;
                try
                {
                    if (!model.FegyelmiUgyIds.IsNullOrEmpty())
                    {
                        foreach (var fegyelmiUgyId in model.FegyelmiUgyIds)
                        {
                            currentId = fegyelmiUgyId;
                            var fegyelmiUgy = (FegyelmiUgyViewModel)Table.Single(x => x.Id == fegyelmiUgyId);

                            if (model.TervezettDatum < DateTime.Today || model.TervezettDatum > fegyelmiUgy.HatarozatDatuma.Value.AddMonths(6))
                                throw new WarningException("A kiválasztott tervezett dátum nem megfelelő.");

                            if (fegyelmiUgy.StatuszCimkeId != (int)CimkeEnums.FegyelmiUgyStatusz.FenyitesKiszabva && fegyelmiUgy.StatuszCimkeId != (int)CimkeEnums.FegyelmiUgyStatusz.FenyitesVegrehajtasMegszakitva)
                            {
                                Log.Error($"Hiba a magánelzárás elrendelésekor, fenyítés végrehajtása alatt nem rendelhet el magánelzárást (fegyelmiUgyId: {currentId})");
                                throw new WarningException("Fenyítés végrehajtása alatt nem rendelhet el magánelzárást! Kérjük, csukja be a részletek ablakot és nyissa újra.");
                            }

                            var entity = new Naplo()
                            {
                                FegyelmiUgyId = fegyelmiUgy.Id,
                                FogvatartottId = fegyelmiUgy.FogvatartottId,
                                TipusCimkeId = (int)FegyelmiNaploTipus.MaganelzarasElrendelese,
                                ElkulonitesElrendelesIdeje = model.TervezettDatum,
                                DonteshozoSzemelyId = AlkalmazasKontextusFunctions.Kontextus.SzemelyzetId
                            };

                            fegyelmiUgy.MaganelzarasElrendelesDatum = model.TervezettDatum;
                            Modify(fegyelmiUgy);

                            KonasoftBVFonixContext.Naplo.Add(entity);
                            KonasoftBVFonixContext.SaveChanges();
                            naploIds.Add(entity.Id);
                            if (felugyeloIds != null)
                            {
                                MaganelzarasFofelugyelokFunctions.SaveMaganelzarasFofelugyelok(felugyeloIds, fegyelmiUgy.Id, entity.Id);
                            }
                            megvaltozottFegyelmi.Add(currentId);
                        }
                    }
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    Log.Error($"Hiba a magánelzárás elrendelésekor (fegyelmiUgyId: {currentId})", e);
                    transaction?.Rollback();
                    throw;
                }
            }

            OnFegyelmiUgyValtozas?.Invoke(ujFegyelmi, megvaltozottFegyelmi, toroltFegyelmi);
            return naploIds;
        }

        public List<ElkulonitesEmailData> FegyelmiUgyElkulonitesErtesitoAdatok()
        {
            Log.Debug("FegyelmiUgyElkulonitesErtesitoAdatok start");
            var today = DateTime.Today;
            var maxDatum = DateTime.Today.AddDays(-18);
            // lekerjuk az összes elkülönítést
            var elkulonitesek = (from ugy in KonasoftBVFonixContext.FegyelmiUgyek
                                 join naplo in KonasoftBVFonixContext.Naplo on ugy.Id equals naplo.FegyelmiUgyId
                                 where naplo.TipusCimkeId == (int)FegyelmiNaploTipus.Elkulonites &&
                                    naplo.ElkulonitesElrendelesIdeje < maxDatum && ugy.ElkulonitveFl == true && ugy.Lezarva == false
                                 select new
                                 {
                                     naplo.ErvenyessegKezdete,
                                     naplo.ElkulonitesElrendelesIdeje,
                                     naplo.ElkulonitesMegszuntetesenekIdeje,
                                     naplo.FegyelmiUgyId,
                                 }).AsNoTracking().ToList();

            // lekérjük az összes "Távollét" típusu felfüggesztést
            var felfuggesztesek = (from felfg in KonasoftBVFonixContext.Felfuggesztesek
                                   where felfg.OkaCimkeId == (int)FelfuggesztesOka.Tavollet
                                   select felfg).AsNoTracking().ToList();

            var fegyelmiIdList = new List<int>();

            foreach (var elkulonites in elkulonitesek)
            {
                try
                {
                    var daysInElkulonites = (today - elkulonites.ElkulonitesElrendelesIdeje).Value.Days;
                    var felfuggOsszevontLista = new List<Felfuggesztes>();
                    var felfuggLista = felfuggesztesek.Where(f => f.FegyelmiUgyId == elkulonites.FegyelmiUgyId).OrderBy(f => f.Kezdete);

                    // Ha felfüggesztés alatt különítenek el akkor a felfüggesztést kezdetét beállítjuk a elkülönítés kezdetére a számoláshoz
                    foreach (var felfuggesztes in felfuggLista)
                    {
                        if (felfuggesztes.Kezdete.Date < elkulonites.ElkulonitesElrendelesIdeje.Value.Date)
                        {
                            felfuggesztes.Kezdete = elkulonites.ElkulonitesElrendelesIdeje.Value.Date;
                        }
                    }

                    // Összevonjunk azokat a felfüggesztéseket ahol A felfüggesztés vege = B felfuggesztés kezdetével
                    foreach (var felfuggesztes in felfuggLista)
                    {
                        var vane = felfuggOsszevontLista.SingleOrDefault(f => (f.Vege.HasValue ? f.Vege.Value.Date : today.Date) == felfuggesztes.Kezdete.Date);
                        if (vane != null)
                        {
                            vane.Vege = felfuggesztes.Vege;
                        }
                        else
                        {
                            felfuggOsszevontLista.Add(felfuggesztes);
                        }
                    }
                    var daysInFelfuggesztes = felfuggOsszevontLista.Sum(f => ((f.Vege.HasValue ? f.Vege.Value.Date : today.Date) - f.Kezdete.Date).Days + 1);

                    //Ha a felfüggesztésben töltött időt kivonjuk az elkülönítésben töltött időve és még így is meghaladja a 19 et akkor küldünk mailt.
                    if (daysInElkulonites - daysInFelfuggesztes >= 19)
                    {
                        fegyelmiIdList.Add(elkulonites.FegyelmiUgyId);
                    }
                }
                catch (Exception e)
                {
                    Log.Error($"Hiba a FegyelmiUgyElkulonitesErtesitoAdatok elkulonitesek meghívásakor (fegyelmiUgyId: {elkulonites.FegyelmiUgyId})", e);
                    throw;
                }
            }

            var ugyek = (from ugy in KonasoftBVFonixContext.FegyelmiUgyek
                         join esemeny in KonasoftBVFonixContext.Esemenyek on ugy.EsemenyId equals esemeny.Id
                         join fogv in KonasoftBVFonixContext.Fogvatartottak on ugy.FogvatartottId equals fogv.Id
                         join fogvSzemAdat in KonasoftBVFonixContext.FogvatartottSzemelyesAdatai on fogv.Id equals fogvSzemAdat.FogvatartottId
                         join elrSzemely in KonasoftBVFonixContext.Szemelyzet on ugy.ElrendeloSzemelyId equals elrSzemely.Id
                         join vetseg in KonasoftBVFonixContext.Cimkek on esemeny.JellegCimkeId equals vetseg.Id
                         join elrBeosztasKsz in KonasoftBVFonixContext.Kodszotar on elrSzemely.BeosztasKszId equals elrBeosztasKsz.Id into elrBeosztasKszL
                         from elrBeosztasKszLeft in elrBeosztasKszL.DefaultIfEmpty()
                         where fegyelmiIdList.Contains(ugy.Id)
                         select new FegyelmiUgyItem
                         {
                             FegyelmiUgyId = ugy.Id,
                             // A fegyelmi alkalmazás nem foglalkozik védett fogvatartottakkal
                             FogvatartottNev = fogvSzemAdat.SzuletesiCsaladiNev_NE_HASZNALD + " " + fogvSzemAdat.SzuletesiUtonev_NE_HASZNALD,
                             FogvatartottNyilvantartasiSzam = fogv.NyilvantartasiAzonosito,
                             Ugyszam = ugy.UgySorszamaIntezetAzon + "/" + ugy.UgySorszamaEv + "/" + ugy.UgySorszama,
                             EsemenyDatuma = esemeny.EsemenyDatuma,
                             EsemenyLeirasa = esemeny.Leiras,
                             FegyelmiVetseg = vetseg.Nev,
                             ElrendeloSid = elrSzemely.AdSid,
                             ElrendeloNev = elrSzemely.Nev,
                             ElrendeloBeosztas = elrBeosztasKszLeft == null ? "" : elrBeosztasKszLeft.Nev,
                         }).ToList();

            var emailList = new List<ElkulonitesEmailData>();

            foreach (var ugy in ugyek)
            {
                try
                {
                    var email = new ActiveDirectoryKezeloFunctions().KeresEmailcim(ugy.ElrendeloSid);

                    var emailData = new ElkulonitesEmailData()
                    {
                        CimzettekTitle = ugy.ElrendeloNev + " " + ugy.ElrendeloBeosztas,
                        Datum = DateTime.Today,
                        EmailAddresses = email,
                        Fegyelmi = ugy
                    };

                    var elrendelo = (from naplo in KonasoftBVFonixContext.Naplo
                                     join elr in KonasoftBVFonixContext.Szemelyzet on naplo.RogzitoSzemelyId equals elr.Id
                                     join elrBeosztasKsz in KonasoftBVFonixContext.Kodszotar on elr.BeosztasKszId equals elrBeosztasKsz.Id into elrBeosztasKszL
                                     from elrBeosztasKszLeft in elrBeosztasKszL.DefaultIfEmpty()
                                     where naplo.TipusCimkeId == (int)FegyelmiNaploTipus.Elkulonites && naplo.FegyelmiUgyId == ugy.FegyelmiUgyId
                                     select new
                                     {
                                         naplo.ErvenyessegKezdete,
                                         elr.AdSid,
                                         elr.Nev,
                                         Beosztas = elrBeosztasKszLeft == null ? "" : elrBeosztasKszLeft.Nev
                                     })
                                    .OrderBy(x => x.ErvenyessegKezdete).First();

                    if (elrendelo.AdSid != ugy.ElrendeloSid)
                    {
                        email = new ActiveDirectoryKezeloFunctions().KeresEmailcim(elrendelo.AdSid);
                        if (email != null)
                        {
                            if (string.IsNullOrEmpty(emailData.EmailAddresses))
                            {
                                emailData.EmailAddresses = email;
                                emailData.CimzettekTitle = elrendelo.Nev + " " + elrendelo.Beosztas;
                            }
                            else
                            {
                                emailData.EmailAddresses += ";" + email;
                                emailData.CimzettekTitle = "Címzettek";
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(emailData.EmailAddresses))
                    {
                        emailList.Add(emailData);
                    }

                }
                catch (Exception e)
                {
                    Log.Error($"Hiba a FegyelmiUgyElkulonitesErtesitoAdatok ugyek meghívásakor (fegyelmiUgyId: {ugy.FegyelmiUgyId})", e);
                    throw;
                }
            }
            Log.Debug("FegyelmiUgyElkulonitesErtesitoAdatok end");
            return emailList;
        }

        public List<RendezvenyErtesitesEmailData> FegyelmiUgyRendezvenyErtesitesEmailAdatok()
        {
            Log.Debug("FegyelmiUgyRendezvenyErtesitesEmailAdatok start");
            var szakteruletek = KonasoftBVFonixContext.VegrehajtoSzakteruletek.ToList().ToDictionary(x => x.IntezetId);

            var minDate = DateTime.Today;
            var maxDate = minDate.AddDays(1);

            var fenyitesek = (from ugy in KonasoftBVFonixContext.FegyelmiUgyek
                              join esemeny in KonasoftBVFonixContext.Esemenyek on ugy.EsemenyId equals esemeny.Id
                              join fogv in KonasoftBVFonixContext.Fogvatartottak on ugy.FogvatartottId equals fogv.Id
                              join fogvSzamAdat in KonasoftBVFonixContext.FogvatartottSzemelyesAdatai on fogv.Id equals fogvSzamAdat.FogvatartottId
                              join fenyTipus in KonasoftBVFonixContext.Cimkek on ugy.FenyitesTipusCimkeId equals fenyTipus.Id
                              join vetseg in KonasoftBVFonixContext.Cimkek on ugy.FegyelmiVetsegTipusaCimkeId equals vetseg.Id
                              join egyseg in KonasoftBVFonixContext.Cimkek on ugy.FenyitesHosszaMennyisegiEgysegCimkeId equals egyseg.Id

                              where (ugy.FenyitesTipusCimkeId == (int)CimkeEnums.FenyitesTipusok.BvIntProgramokonValoReszvetelKorlatozasa ||
                                     ugy.FenyitesTipusCimkeId == (int)CimkeEnums.FenyitesTipusok.BvIntProgramokonValoReszvetelTiltasa)
                                     && ((minDate < ugy.VegrehajtasKezdeteIdo && ugy.VegrehajtasKezdeteIdo < maxDate) ||
                                     (minDate < ugy.VegrehajtasVegeIdo && ugy.VegrehajtasVegeIdo < maxDate && ugy.StatuszCimkeId == (int)CimkeEnums.FegyelmiUgyStatusz.FenyitesVegrehajtva))
                              select new FegyelmiUgyItem
                              {
                                  FegyelmiUgyId = ugy.Id,
                                  // A fegyelmi alkalmazás nem foglalkozik védett fogvatartottakkal
                                  FogvatartottNev = fogvSzamAdat.SzuletesiCsaladiNev_NE_HASZNALD + " " + fogvSzamAdat.SzuletesiUtonev_NE_HASZNALD,
                                  FogvatartottNyilvantartasiSzam = fogv.NyilvantartasiAzonosito,
                                  FogvatartottId = fogv.Id,
                                  FegyelmiUgyIntezetId = ugy.IntezetId,
                                  FogvatartottAktualisIntezetId = fogv.AktualisIntezetId,
                                  Ugyszam = ugy.UgySorszamaIntezetAzon + "/" + ugy.UgySorszamaEv + "/" + ugy.UgySorszama,
                                  EsemenyDatuma = esemeny.EsemenyDatuma,
                                  NevelesiCsoportId = fogv.NevelesiCsoportId,
                                  FegyelmiUgyStatuszCimkeId = ugy.StatuszCimkeId,
                                  FenyitesTartam = ugy.FenyitesHossza + " " + egyseg.Nev,
                                  FegyelmiVetseg = vetseg.Nev
                              }
                                 ).ToList();


            var dicFenyitesek = fenyitesek.ToDictionary(x => x.FegyelmiUgyId);

            Dictionary<int, ErtesitendoSzemely> ertesitendoRendezvenyszervezok = new Dictionary<int, ErtesitendoSzemely>();
            Dictionary<string, ErtesitendoSzemely> ertesitendoSzemelyek = new Dictionary<string, ErtesitendoSzemely>();

            var konaContext = new KonasoftBVFonixContext();

            // emaillel rendelkező személyek kigyűjtése
            foreach (var ugy in fenyitesek)
            {
                if (szakteruletek.ContainsKey(ugy.FegyelmiUgyIntezetId))
                {
                    if (!string.IsNullOrEmpty(szakteruletek[ugy.FegyelmiUgyIntezetId].RendezvenySzervezoCimzettLista))
                    {
                        if (!ertesitendoRendezvenyszervezok.ContainsKey(ugy.FegyelmiUgyIntezetId))
                        {

                            ertesitendoRendezvenyszervezok.Add(ugy.FegyelmiUgyIntezetId, new ErtesitendoSzemely()
                            {
                                Email = szakteruletek[ugy.FegyelmiUgyIntezetId].RendezvenySzervezoCimzettLista
                            });
                        }
                        ertesitendoRendezvenyszervezok[ugy.FegyelmiUgyIntezetId].FegyelmiIdList.Add(ugy.FegyelmiUgyId);
                    }
                }
                if (ugy.FogvatartottAktualisIntezetId != ugy.FegyelmiUgyIntezetId)
                    if (szakteruletek.ContainsKey(ugy.FogvatartottAktualisIntezetId))
                    {
                        if (!string.IsNullOrEmpty(szakteruletek[ugy.FogvatartottAktualisIntezetId].RendezvenySzervezoCimzettLista))
                        {
                            if (!ertesitendoRendezvenyszervezok.ContainsKey(ugy.FogvatartottAktualisIntezetId))
                            {
                                ertesitendoRendezvenyszervezok.Add(ugy.FogvatartottAktualisIntezetId, new ErtesitendoSzemely()
                                {
                                    Email = szakteruletek[ugy.FogvatartottAktualisIntezetId].RendezvenySzervezoCimzettLista
                                });
                            }
                            ertesitendoRendezvenyszervezok[ugy.FogvatartottAktualisIntezetId].FegyelmiIdList.Add(ugy.FegyelmiUgyId);
                        }
                    }
                if (ugy.NevelesiCsoportId != null)
                {
                    var csoport = konaContext.NevelesiCsoportok.SingleOrDefault(x => x.Id == ugy.NevelesiCsoportId);
                    if (csoport != null)
                    {
                        var szemely = konaContext.Szemelyzet.FirstOrDefault(x => x.AdSid == csoport.NeveloSzemelySid && x.IntezetId == csoport.IntezetId);
                        if (szemely == null) szemely = konaContext.Szemelyzet.FirstOrDefault(x => x.AdSid == csoport.NeveloSzemelySid);
                        if (szemely != null)
                        {
                            var email = new ActiveDirectoryKezeloFunctions().KeresEmailcim(szemely.AdSid);
                            if (!string.IsNullOrEmpty(email))
                            {
                                var beosztas = konaContext.Kodszotar.SingleOrDefault(x => x.Id == szemely.BeosztasKszId);
                                if (!ertesitendoSzemelyek.ContainsKey(szemely.AdSid))
                                    ertesitendoSzemelyek.Add(szemely.AdSid, new ErtesitendoSzemely()
                                    {
                                        Email = email,
                                        SzemelySid = szemely.AdSid,
                                        SzemelyBeosztas = beosztas.Nev,
                                        SzemelyNev = szemely.Nev
                                    });
                                ertesitendoSzemelyek[szemely.AdSid].FegyelmiIdList.Add(ugy.FegyelmiUgyId);
                            }
                        }
                    }
                }
            }

            // ügy lista kulcs előállítás
            foreach (var ertesites in ertesitendoSzemelyek)
            {
                ertesites.Value.FegyelmiKey = string.Join(",", ertesites.Value.FegyelmiIdList.OrderBy(x => x));
            }
            foreach (var ertesites in ertesitendoRendezvenyszervezok)
            {
                ertesites.Value.FegyelmiKey = string.Join(",", ertesites.Value.FegyelmiIdList.OrderBy(x => x));
            }

            var emailList = new List<RendezvenyErtesitesEmailData>();
            // azonos kiküldendő tartalomhoz emailek egyesítése
            foreach (var item in ertesitendoRendezvenyszervezok.GroupBy(x => x.Value.FegyelmiKey))
            {
                var email = new RendezvenyErtesitesEmailItem();
                foreach (var szemely in item.ToList())
                {
                    email.ErtesitendoSzemelyek.Add(szemely.Value);
                }
                foreach (var ugyId in item.First().Value.FegyelmiIdList)
                {
                    if (dicFenyitesek[ugyId].FegyelmiUgyStatuszCimkeId == (int)CimkeEnums.FegyelmiUgyStatusz.FenyitesVegrehajtva)
                    {
                        email.EngedelyezettFegyelmiList.Add(dicFenyitesek[ugyId]);
                    }
                    else
                    {
                        email.KorlatozottFegyelmiList.Add(dicFenyitesek[ugyId]);
                    }
                }
                emailList.Add(email.GetEmailData());
            }

            foreach (var item in ertesitendoSzemelyek.GroupBy(x => x.Value.FegyelmiKey))
            {
                var email = new RendezvenyErtesitesEmailItem();
                foreach (var szemely in item.ToList())
                {
                    email.ErtesitendoSzemelyek.Add(szemely.Value);
                }
                foreach (var ugyId in item.First().Value.FegyelmiIdList)
                {
                    if (dicFenyitesek[ugyId].FegyelmiUgyStatuszCimkeId == (int)CimkeEnums.FegyelmiUgyStatusz.FenyitesVegrehajtva)
                    {
                        email.EngedelyezettFegyelmiList.Add(dicFenyitesek[ugyId]);
                    }
                    else
                    {
                        email.KorlatozottFegyelmiList.Add(dicFenyitesek[ugyId]);
                    }
                }
                emailList.Add(email.GetEmailData());
            }
            Log.Debug("FegyelmiUgyRendezvenyErtesitesEmailAdatok end");
            return emailList;
        }

        public List<TargyiErtesitesEmailData> FegyelmiUgyTargyiErtesitesEmailAdatok()
        {
            Log.Debug("FegyelmiUgyTargyiErtesitesEmailAdatok start");
            var szakteruletek = KonasoftBVFonixContext.VegrehajtoSzakteruletek.ToList().ToDictionary(x => x.IntezetId);

            var minDate = DateTime.Today;
            var maxDate = minDate.AddDays(1);

            var fenyitesek = (from ugy in KonasoftBVFonixContext.FegyelmiUgyek
                              join esemeny in KonasoftBVFonixContext.Esemenyek on ugy.EsemenyId equals esemeny.Id
                              join fogv in KonasoftBVFonixContext.Fogvatartottak on ugy.FogvatartottId equals fogv.Id
                              join fogvSzamAdat in KonasoftBVFonixContext.FogvatartottSzemelyesAdatai on fogv.Id equals fogvSzamAdat.FogvatartottId
                              join fenyTipus in KonasoftBVFonixContext.Cimkek on ugy.FenyitesTipusCimkeId equals fenyTipus.Id
                              join vetseg in KonasoftBVFonixContext.Cimkek on ugy.FegyelmiVetsegTipusaCimkeId equals vetseg.Id
                              join egyseg in KonasoftBVFonixContext.Cimkek on ugy.FenyitesHosszaMennyisegiEgysegCimkeId equals egyseg.Id

                              where (ugy.FenyitesTipusCimkeId == (int)CimkeEnums.FenyitesTipusok.MaganalTarthatoTargyakKorenekKorlatozasa)
                                     && ((minDate < ugy.VegrehajtasKezdeteIdo && ugy.VegrehajtasKezdeteIdo < maxDate) ||
                                     (minDate < ugy.VegrehajtasVegeIdo && ugy.VegrehajtasVegeIdo < maxDate && ugy.StatuszCimkeId == (int)CimkeEnums.FegyelmiUgyStatusz.FenyitesVegrehajtva))
                              select new FegyelmiUgyItem
                              {
                                  FegyelmiUgyId = ugy.Id,
                                  // A fegyelmi alkalmazás nem foglalkozik védett fogvatartottakkal
                                  FogvatartottNev = fogvSzamAdat.SzuletesiCsaladiNev_NE_HASZNALD + " " + fogvSzamAdat.SzuletesiUtonev_NE_HASZNALD,
                                  FogvatartottNyilvantartasiSzam = fogv.NyilvantartasiAzonosito,
                                  FogvatartottId = fogv.Id,
                                  FegyelmiUgyIntezetId = ugy.IntezetId,
                                  FogvatartottAktualisIntezetId = fogv.AktualisIntezetId,
                                  Ugyszam = ugy.UgySorszamaIntezetAzon + "/" + ugy.UgySorszamaEv + "/" + ugy.UgySorszama,
                                  EsemenyDatuma = esemeny.EsemenyDatuma,
                                  NevelesiCsoportId = fogv.NevelesiCsoportId,
                                  FegyelmiUgyStatuszCimkeId = ugy.StatuszCimkeId,
                                  FenyitesTartam = ugy.FenyitesHossza + " " + egyseg.Nev,
                                  HatarozatSzovege = ugy.HatarozatIndoklasa,

                              }
                                 ).ToList();


            var dicFenyitesek = fenyitesek.ToDictionary(x => x.FegyelmiUgyId);

            Dictionary<int, ErtesitendoSzemely> ertesitendoRendezvenyszervezok = new Dictionary<int, ErtesitendoSzemely>();
            Dictionary<string, ErtesitendoSzemely> ertesitendoSzemelyek = new Dictionary<string, ErtesitendoSzemely>();

            var konaContext = new KonasoftBVFonixContext();

            // emaillel rendelkező személyek kigyűjtése
            foreach (var ugy in fenyitesek)
            {
                if (szakteruletek.ContainsKey(ugy.FegyelmiUgyIntezetId))
                {
                    if (!string.IsNullOrEmpty(szakteruletek[ugy.FegyelmiUgyIntezetId].LetetesCimzettLista))
                    {
                        if (!ertesitendoRendezvenyszervezok.ContainsKey(ugy.FegyelmiUgyIntezetId))
                        {

                            ertesitendoRendezvenyszervezok.Add(ugy.FegyelmiUgyIntezetId, new ErtesitendoSzemely()
                            {
                                Email = szakteruletek[ugy.FegyelmiUgyIntezetId].LetetesCimzettLista
                            });
                        }
                        ertesitendoRendezvenyszervezok[ugy.FegyelmiUgyIntezetId].FegyelmiIdList.Add(ugy.FegyelmiUgyId);
                    }
                }
                if (ugy.FogvatartottAktualisIntezetId != ugy.FegyelmiUgyIntezetId)
                    if (szakteruletek.ContainsKey(ugy.FogvatartottAktualisIntezetId))
                    {
                        if (!string.IsNullOrEmpty(szakteruletek[ugy.FogvatartottAktualisIntezetId].LetetesCimzettLista))
                        {
                            if (!ertesitendoRendezvenyszervezok.ContainsKey(ugy.FogvatartottAktualisIntezetId))
                            {

                                ertesitendoRendezvenyszervezok.Add(ugy.FogvatartottAktualisIntezetId, new ErtesitendoSzemely()
                                {
                                    Email = szakteruletek[ugy.FogvatartottAktualisIntezetId].LetetesCimzettLista
                                });
                            }
                            ertesitendoRendezvenyszervezok[ugy.FogvatartottAktualisIntezetId].FegyelmiIdList.Add(ugy.FegyelmiUgyId);
                        }
                    }
                if (ugy.NevelesiCsoportId != null)
                {
                    var csoport = konaContext.NevelesiCsoportok.SingleOrDefault(x => x.Id == ugy.NevelesiCsoportId);
                    if (csoport != null)
                    {
                        var szemely = konaContext.Szemelyzet.FirstOrDefault(x => x.AdSid == csoport.NeveloSzemelySid && x.IntezetId == csoport.IntezetId);
                        if (szemely == null) szemely = konaContext.Szemelyzet.FirstOrDefault(x => x.AdSid == csoport.NeveloSzemelySid);
                        if (szemely != null)
                        {
                            var email = new ActiveDirectoryKezeloFunctions().KeresEmailcim(szemely.AdSid);
                            if (!string.IsNullOrEmpty(email))
                            {
                                var beosztas = konaContext.Kodszotar.SingleOrDefault(x => x.Id == szemely.BeosztasKszId);
                                if (!ertesitendoSzemelyek.ContainsKey(szemely.AdSid))
                                    ertesitendoSzemelyek.Add(szemely.AdSid, new ErtesitendoSzemely()
                                    {
                                        Email = email,
                                        SzemelySid = szemely.AdSid,
                                        SzemelyBeosztas = beosztas.Nev,
                                        SzemelyNev = szemely.Nev
                                    });
                                ertesitendoSzemelyek[szemely.AdSid].FegyelmiIdList.Add(ugy.FegyelmiUgyId);
                            }
                        }
                    }
                }
            }

            // ügy lista kulcs előállítás
            foreach (var ertesites in ertesitendoSzemelyek)
            {
                ertesites.Value.FegyelmiKey = string.Join(",", ertesites.Value.FegyelmiIdList.OrderBy(x => x));
            }
            foreach (var ertesites in ertesitendoRendezvenyszervezok)
            {
                ertesites.Value.FegyelmiKey = string.Join(",", ertesites.Value.FegyelmiIdList.OrderBy(x => x));
            }

            var emailList = new List<TargyiErtesitesEmailData>();
            // azonos kiküldendő tartalomhoz emailek egyesítése
            foreach (var item in ertesitendoRendezvenyszervezok.GroupBy(x => x.Value.FegyelmiKey))
            {
                var email = new TargyiErtesitesEmailItem();
                foreach (var szemely in item.ToList())
                {
                    email.ErtesitendoSzemelyek.Add(szemely.Value);
                }
                foreach (var ugyId in item.First().Value.FegyelmiIdList)
                {
                    if (dicFenyitesek[ugyId].FegyelmiUgyStatuszCimkeId == (int)CimkeEnums.FegyelmiUgyStatusz.FenyitesVegrehajtva)
                    {
                        email.EngedelyezettFegyelmiList.Add(dicFenyitesek[ugyId]);
                    }
                    else
                    {
                        email.KorlatozottFegyelmiList.Add(dicFenyitesek[ugyId]);
                    }
                }
                emailList.Add(email.GetEmailData());
            }

            foreach (var item in ertesitendoSzemelyek.GroupBy(x => x.Value.FegyelmiKey))
            {
                var email = new TargyiErtesitesEmailItem();
                foreach (var szemely in item.ToList())
                {
                    email.ErtesitendoSzemelyek.Add(szemely.Value);
                }
                foreach (var ugyId in item.First().Value.FegyelmiIdList)
                {
                    if (dicFenyitesek[ugyId].FegyelmiUgyStatuszCimkeId == (int)CimkeEnums.FegyelmiUgyStatusz.FenyitesVegrehajtva)
                    {
                        email.EngedelyezettFegyelmiList.Add(dicFenyitesek[ugyId]);
                    }
                    else
                    {
                        email.KorlatozottFegyelmiList.Add(dicFenyitesek[ugyId]);
                    }
                }
                emailList.Add(email.GetEmailData());
            }
            Log.Debug("FegyelmiUgyTargyiErtesitesEmailAdatok end");
            return emailList;
        }

        public List<TargyiErtesitesEmailData> FegyelmiUgyTobbletszolgaltatasEmailAdatok()
        {
            Log.Debug("FegyelmiUgyTobbletszolgaltatasEmailAdatok start");
            var szakteruletek = KonasoftBVFonixContext.VegrehajtoSzakteruletek.ToList().ToDictionary(x => x.IntezetId);

            var minDate = DateTime.Today;
            var maxDate = minDate.AddDays(1);

            var fenyitesek = (from ugy in KonasoftBVFonixContext.FegyelmiUgyek
                              join esemeny in KonasoftBVFonixContext.Esemenyek on ugy.EsemenyId equals esemeny.Id
                              join fogv in KonasoftBVFonixContext.Fogvatartottak on ugy.FogvatartottId equals fogv.Id
                              join fogvSzamAdat in KonasoftBVFonixContext.FogvatartottSzemelyesAdatai on fogv.Id equals fogvSzamAdat.FogvatartottId
                              join fenyTipus in KonasoftBVFonixContext.Cimkek on ugy.FenyitesTipusCimkeId equals fenyTipus.Id
                              join vetseg in KonasoftBVFonixContext.Cimkek on ugy.FegyelmiVetsegTipusaCimkeId equals vetseg.Id
                              join egyseg in KonasoftBVFonixContext.Cimkek on ugy.FenyitesHosszaMennyisegiEgysegCimkeId equals egyseg.Id

                              where (ugy.FenyitesTipusCimkeId == (int)CimkeEnums.FenyitesTipusok.TobbletszolgaltatasokMegvonasa)
                                     && ((minDate < ugy.VegrehajtasKezdeteIdo && ugy.VegrehajtasKezdeteIdo < maxDate) ||
                                     (minDate < ugy.VegrehajtasVegeIdo && ugy.VegrehajtasVegeIdo < maxDate && ugy.StatuszCimkeId == (int)CimkeEnums.FegyelmiUgyStatusz.FenyitesVegrehajtva))
                              select new FegyelmiUgyItem
                              {
                                  FegyelmiUgyId = ugy.Id,
                                  // A fegyelmi alkalmazás nem foglalkozik védett fogvatartottakkal
                                  FogvatartottNev = fogvSzamAdat.SzuletesiCsaladiNev_NE_HASZNALD + " " + fogvSzamAdat.SzuletesiUtonev_NE_HASZNALD,
                                  FogvatartottNyilvantartasiSzam = fogv.NyilvantartasiAzonosito,
                                  FogvatartottId = fogv.Id,
                                  FegyelmiUgyIntezetId = ugy.IntezetId,
                                  FogvatartottAktualisIntezetId = fogv.AktualisIntezetId,
                                  Ugyszam = ugy.UgySorszamaIntezetAzon + "/" + ugy.UgySorszamaEv + "/" + ugy.UgySorszama,
                                  EsemenyDatuma = esemeny.EsemenyDatuma,
                                  NevelesiCsoportId = fogv.NevelesiCsoportId,
                                  FegyelmiUgyStatuszCimkeId = ugy.StatuszCimkeId,
                                  FenyitesTartam = ugy.FenyitesHossza + " " + egyseg.Nev,
                                  HatarozatSzovege = ugy.HatarozatIndoklasa,

                              }
                                 ).ToList();


            var dicFenyitesek = fenyitesek.ToDictionary(x => x.FegyelmiUgyId);

            Dictionary<int, ErtesitendoSzemely> ertesitendoRendezvenyszervezok = new Dictionary<int, ErtesitendoSzemely>();
            Dictionary<string, ErtesitendoSzemely> ertesitendoSzemelyek = new Dictionary<string, ErtesitendoSzemely>();

            var konaContext = new KonasoftBVFonixContext();

            // emaillel rendelkező személyek kigyűjtése
            foreach (var ugy in fenyitesek)
            {
                if (szakteruletek.ContainsKey(ugy.FegyelmiUgyIntezetId))
                {
                    if (!string.IsNullOrEmpty(szakteruletek[ugy.FegyelmiUgyIntezetId].LetetesCimzettLista))
                    {
                        if (!ertesitendoRendezvenyszervezok.ContainsKey(ugy.FegyelmiUgyIntezetId))
                        {

                            ertesitendoRendezvenyszervezok.Add(ugy.FegyelmiUgyIntezetId, new ErtesitendoSzemely()
                            {
                                Email = szakteruletek[ugy.FegyelmiUgyIntezetId].LetetesCimzettLista
                            });
                        }
                        ertesitendoRendezvenyszervezok[ugy.FegyelmiUgyIntezetId].FegyelmiIdList.Add(ugy.FegyelmiUgyId);
                    }
                }
                if (ugy.FogvatartottAktualisIntezetId != ugy.FegyelmiUgyIntezetId)
                    if (szakteruletek.ContainsKey(ugy.FogvatartottAktualisIntezetId))
                    {
                        if (!string.IsNullOrEmpty(szakteruletek[ugy.FogvatartottAktualisIntezetId].LetetesCimzettLista))
                        {
                            if (!ertesitendoRendezvenyszervezok.ContainsKey(ugy.FogvatartottAktualisIntezetId))
                            {

                                ertesitendoRendezvenyszervezok.Add(ugy.FogvatartottAktualisIntezetId, new ErtesitendoSzemely()
                                {
                                    Email = szakteruletek[ugy.FogvatartottAktualisIntezetId].LetetesCimzettLista
                                });
                            }
                            ertesitendoRendezvenyszervezok[ugy.FogvatartottAktualisIntezetId].FegyelmiIdList.Add(ugy.FegyelmiUgyId);
                        }
                    }
                if (ugy.NevelesiCsoportId != null)
                {
                    var csoport = konaContext.NevelesiCsoportok.SingleOrDefault(x => x.Id == ugy.NevelesiCsoportId);
                    if (csoport != null)
                    {
                        var szemely = konaContext.Szemelyzet.FirstOrDefault(x => x.AdSid == csoport.NeveloSzemelySid && x.IntezetId == csoport.IntezetId);
                        if (szemely == null) szemely = konaContext.Szemelyzet.FirstOrDefault(x => x.AdSid == csoport.NeveloSzemelySid);
                        if (szemely != null)
                        {
                            var email = new ActiveDirectoryKezeloFunctions().KeresEmailcim(szemely.AdSid);
                            if (!string.IsNullOrEmpty(email))
                            {
                                var beosztas = konaContext.Kodszotar.SingleOrDefault(x => x.Id == szemely.BeosztasKszId);
                                if (!ertesitendoSzemelyek.ContainsKey(szemely.AdSid))
                                    ertesitendoSzemelyek.Add(szemely.AdSid, new ErtesitendoSzemely()
                                    {
                                        Email = email,
                                        SzemelySid = szemely.AdSid,
                                        SzemelyBeosztas = beosztas.Nev,
                                        SzemelyNev = szemely.Nev
                                    });
                                ertesitendoSzemelyek[szemely.AdSid].FegyelmiIdList.Add(ugy.FegyelmiUgyId);
                            }
                        }
                    }
                }
            }

            // ügy lista kulcs előállítás
            foreach (var ertesites in ertesitendoSzemelyek)
            {
                ertesites.Value.FegyelmiKey = string.Join(",", ertesites.Value.FegyelmiIdList.OrderBy(x => x));
            }
            foreach (var ertesites in ertesitendoRendezvenyszervezok)
            {
                ertesites.Value.FegyelmiKey = string.Join(",", ertesites.Value.FegyelmiIdList.OrderBy(x => x));
            }

            var emailList = new List<TargyiErtesitesEmailData>();
            // azonos kiküldendő tartalomhoz emailek egyesítése
            foreach (var item in ertesitendoRendezvenyszervezok.GroupBy(x => x.Value.FegyelmiKey))
            {
                var email = new TargyiErtesitesEmailItem();
                foreach (var szemely in item.ToList())
                {
                    email.ErtesitendoSzemelyek.Add(szemely.Value);
                }
                foreach (var ugyId in item.First().Value.FegyelmiIdList)
                {
                    if (dicFenyitesek[ugyId].FegyelmiUgyStatuszCimkeId == (int)CimkeEnums.FegyelmiUgyStatusz.FenyitesVegrehajtva)
                    {
                        email.EngedelyezettFegyelmiList.Add(dicFenyitesek[ugyId]);
                    }
                    else
                    {
                        email.KorlatozottFegyelmiList.Add(dicFenyitesek[ugyId]);
                    }
                }
                emailList.Add(email.GetEmailData());
            }

            foreach (var item in ertesitendoSzemelyek.GroupBy(x => x.Value.FegyelmiKey))
            {
                var email = new TargyiErtesitesEmailItem();
                foreach (var szemely in item.ToList())
                {
                    email.ErtesitendoSzemelyek.Add(szemely.Value);
                }
                foreach (var ugyId in item.First().Value.FegyelmiIdList)
                {
                    if (dicFenyitesek[ugyId].FegyelmiUgyStatuszCimkeId == (int)CimkeEnums.FegyelmiUgyStatusz.FenyitesVegrehajtva)
                    {
                        email.EngedelyezettFegyelmiList.Add(dicFenyitesek[ugyId]);
                    }
                    else
                    {
                        email.KorlatozottFegyelmiList.Add(dicFenyitesek[ugyId]);
                    }
                }
                emailList.Add(email.GetEmailData());
            }
            Log.Debug("FegyelmiUgyTobbletszolgaltatasEmailAdatok end");
            return emailList;
        }

        public List<int> FenyitesVegrahajtasaKesz(List<int> fegyelmiUgyIds)
        {
            var aktIntezet = JogosultsagCacheFunctions.AktualisIntezet.Id;
            List<int> ujFegyelmi = new List<int>();
            List<int> megvaltozottFegyelmi = new List<int>();
            List<int> toroltFegyelmi = new List<int>();
            List<int> naploIds = new List<int>();

            using (var transaction = KonasoftBVFonixContext.Database.BeginTransaction())
            {
                var currentId = 0;
                try
                {

                    if (!fegyelmiUgyIds.IsNullOrEmpty())
                    {
                        foreach (var fegyelmiUgyId in fegyelmiUgyIds)
                        {
                            currentId = fegyelmiUgyId;
                            var fegyelmiUgy = (FegyelmiUgyViewModel)Table.Single(x => x.Id == fegyelmiUgyId);


                            fegyelmiUgy.StatuszCimkeId = (int)FegyelmiUgyStatusz.FenyitesVegrehajtva;
                            fegyelmiUgy.Lezarva = true;
                            fegyelmiUgy.Hatarido = null;
                            Modify(fegyelmiUgy);
                            KonasoftBVFonixContext.SaveChanges();

                            var entity = new Naplo()
                            {
                                FegyelmiUgyId = fegyelmiUgy.Id,
                                FogvatartottId = fegyelmiUgy.FogvatartottId,
                                TipusCimkeId = (int)FegyelmiNaploTipus.FenyitesVegrehajtasaKesz,
                            };

                            KonasoftBVFonixContext.Naplo.Add(entity);
                            KonasoftBVFonixContext.SaveChanges();
                            naploIds.Add(entity.Id);
                            toroltFegyelmi.Add(currentId);
                        }
                    }
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    Log.Error($"Hiba a fenyítés végrehajtása kész meghívásakor (fegyelmiUgyId: {currentId})", e);
                    transaction?.Rollback();
                    throw;
                }
            }

            OnFegyelmiUgyValtozas?.Invoke(ujFegyelmi, megvaltozottFegyelmi, toroltFegyelmi);
            return naploIds;
        }

        public Tuple<List<FogvatartottFegyelmiUgyAdatokModel>, List<FogvatartottFegyelmiUgyAdatokModel>> GetOsszesFegyelmiUgyByFegyelmiUgyId(int fegyelmiUgyId)
        {
            EsemenyekFunctions esemenyekFunctions = new EsemenyekFunctions();
            var jelenlegiUgy = GetFegyelmiUgyById(fegyelmiUgyId);
            var jelenlegiEsemeny = esemenyekFunctions.GetEsemenyById(jelenlegiUgy.EsemenyId);
            var fogvatartottFegyelmiUgyei = Table
                                            .Include(x => x.Intezet)
                                            .Where(x => x.FogvatartottId == jelenlegiUgy.FogvatartottId)
                                            .ToList();

            List<FogvatartottFegyelmiUgyAdatokModel> zartUgyek = new List<FogvatartottFegyelmiUgyAdatokModel>();
            List<FogvatartottFegyelmiUgyAdatokModel> nyitottUgyek = new List<FogvatartottFegyelmiUgyAdatokModel>();
            if (fogvatartottFegyelmiUgyei != null)
            {
                foreach (var fegyelmiUgy in fogvatartottFegyelmiUgyei)
                {
                    if (fegyelmiUgy.Id != jelenlegiUgy.Id)
                    {
                        var esemeny = esemenyekFunctions.GetEsemenyById(fegyelmiUgy.EsemenyId);
                        var egyEv = DateTime.Now.AddYears(-1);
                        if (esemeny.EsemenyDatuma > egyEv && jelenlegiEsemeny.JellegCimkeId == esemeny.JellegCimkeId && fegyelmiUgy.Lezarva == true)
                        {
                            FogvatartottFegyelmiUgyAdatokModel adatokModel = new FogvatartottFegyelmiUgyAdatokModel()
                            {
                                EsemenyDatuma = esemeny.EsemenyDatuma,
                                IntezetNev = fegyelmiUgy.Intezet?.KozepesNev,
                                UgySzama = fegyelmiUgy.UgySorszamaIntezetAzon + '/' + fegyelmiUgy.UgySorszamaEv.ToString() + '/' + fegyelmiUgy.UgySorszama.ToString(),
                                VetsegTipusa = esemeny?.Jelleg
                            };
                            zartUgyek.Add(adatokModel);
                        }
                        if (fegyelmiUgy.Lezarva != true)
                        {
                            FogvatartottFegyelmiUgyAdatokModel adatokModel = new FogvatartottFegyelmiUgyAdatokModel()
                            {
                                EsemenyDatuma = esemeny.EsemenyDatuma,
                                IntezetNev = fegyelmiUgy.Intezet?.KozepesNev,
                                UgySzama = fegyelmiUgy.UgySorszamaIntezetAzon + '/' + fegyelmiUgy.UgySorszamaEv.ToString() + '/' + fegyelmiUgy.UgySorszama.ToString(),
                                VetsegTipusa = esemeny?.Jelleg
                            };
                            nyitottUgyek.Add(adatokModel);
                        }
                    }
                }
            }
            return new Tuple<List<FogvatartottFegyelmiUgyAdatokModel>, List<FogvatartottFegyelmiUgyAdatokModel>>(nyitottUgyek, zartUgyek);
        }

        public List<MaganelzarasFofelugyeloEmailData> MaganelzarasFofelugyelokEmailAdatok()
        {
            Log.Debug("MaganelzarasFofelugyelokEmailAdatok start");
            var holnap = DateTime.Today.AddDays(1);
            var naploList = (from fofelugy in KonasoftBVFonixContext.MaganelzarasFofelugyelok
                             join naplo in KonasoftBVFonixContext.Naplo on fofelugy.NaploId equals naplo.Id
                             where naplo.ElkulonitesElrendelesIdeje >= DateTime.Today
                             && naplo.ElkulonitesElrendelesIdeje < holnap
                             && naplo.TipusCimkeId == (int)CimkeEnums.FegyelmiNaploTipus.MaganelzarasElrendelese
                             select new
                             {
                                 naplo.ErvenyessegKezdete,
                                 naplo.ElkulonitesElrendelesIdeje,
                                 naplo.ElkulonitesMegszuntetesenekIdeje,
                                 naplo.FegyelmiUgyId
                             }).ToList();

            List<int> fegyelmiIdList = new List<int>();

            foreach (var naplok in naploList.GroupBy(x => x.FegyelmiUgyId))
            {
                var list = naplok.OrderByDescending(x => x.ErvenyessegKezdete).ToList();
                if (list.First().ElkulonitesMegszuntetesenekIdeje == null)
                {
                    fegyelmiIdList.Add(list.First().FegyelmiUgyId);
                }
            }

            var ugyek = (from naplo in KonasoftBVFonixContext.Naplo
                         join maganFelugy in KonasoftBVFonixContext.MaganelzarasFofelugyelok on naplo.Id equals maganFelugy.NaploId
                         join ugy in KonasoftBVFonixContext.FegyelmiUgyek on naplo.FegyelmiUgyId equals ugy.Id
                         join esemeny in KonasoftBVFonixContext.Esemenyek on ugy.EsemenyId equals esemeny.Id
                         join fogv in KonasoftBVFonixContext.Fogvatartottak on ugy.FogvatartottId equals fogv.Id
                         join felugySzemely in KonasoftBVFonixContext.Szemelyzet on maganFelugy.FofelugyeloId equals felugySzemely.Id
                         join fogvSzemAdat in KonasoftBVFonixContext.FogvatartottSzemelyesAdatai on fogv.Id equals fogvSzemAdat.FogvatartottId
                         join elrSzemely in KonasoftBVFonixContext.Szemelyzet on ugy.ElrendeloSzemelyId equals elrSzemely.Id
                         join vetseg in KonasoftBVFonixContext.Cimkek on esemeny.JellegCimkeId equals vetseg.Id
                         join elrBeosztasKsz in KonasoftBVFonixContext.Kodszotar on elrSzemely.BeosztasKszId equals elrBeosztasKsz.Id into elrBeosztasKszL
                         from elrBeosztasKszLeft in elrBeosztasKszL.DefaultIfEmpty()
                         where fegyelmiIdList.Contains(ugy.Id)
                            && naplo.ElkulonitesElrendelesIdeje >= DateTime.Today
                            && naplo.ElkulonitesElrendelesIdeje < holnap
                         select new FegyelmiUgyItem
                         {
                             FegyelmiUgyId = ugy.Id,
                             // A fegyelmi alkalmazás nem foglalkozik védett fogvatartottakkal
                             FogvatartottNev = fogvSzemAdat.SzuletesiCsaladiNev_NE_HASZNALD + " " + fogvSzemAdat.SzuletesiUtonev_NE_HASZNALD,
                             FogvatartottNyilvantartasiSzam = fogv.NyilvantartasiAzonosito,
                             Ugyszam = ugy.UgySorszamaIntezetAzon + "/" + ugy.UgySorszamaEv + "/" + ugy.UgySorszama,
                             EsemenyDatuma = esemeny.EsemenyDatuma,
                             EsemenyLeirasa = esemeny.Leiras,
                             FegyelmiVetseg = vetseg.Nev,
                             ElrendeloSid = elrSzemely.AdSid,
                             ElrendeloNev = elrSzemely.Nev,
                             ElrendeloBeosztas = elrBeosztasKszLeft == null ? "" : elrBeosztasKszLeft.Nev,
                             FofelugyeloNev = felugySzemely.Nev,
                             FofelugyeloSid = felugySzemely.AdSid,
                             ElrendelesRogzitesIdeje = naplo.ErvenyessegKezdete,
                             NaploId = naplo.Id
                         }).ToList();


            var emailList = new List<MaganelzarasFofelugyeloEmailData>();
            var ugyekDistinct = ugyek.GroupBy(x => x.FegyelmiUgyId);

            foreach (var ugyDistinct in ugyekDistinct)
            {
                var ugy = ugyek.Where(x => x.FegyelmiUgyId == ugyDistinct.Key).OrderByDescending(x => x.ElrendelesRogzitesIdeje).First();

                var felugyelok = (from felugyMagan in KonasoftBVFonixContext.MaganelzarasFofelugyelok
                                  join naplo in KonasoftBVFonixContext.Naplo on felugyMagan.NaploId equals naplo.Id
                                  join felugy in KonasoftBVFonixContext.Szemelyzet on felugyMagan.FofelugyeloId equals felugy.Id
                                  where felugyMagan.NaploId == ugy.NaploId
                                     && naplo.TipusCimkeId == (int)CimkeEnums.FegyelmiNaploTipus.MaganelzarasElrendelese
                                  select new
                                  {
                                      felugy.AdSid,
                                      felugy.Nev
                                  })
                                .ToList();

                var emailData = new MaganelzarasFofelugyeloEmailData()
                {
                    CimzettekTitle = "",
                    Datum = DateTime.Today.ToShortDateString(),
                    EmailAddresses = "",
                    Fegyelmi = ugy
                };

                foreach (var felugyelo in felugyelok)
                {
                    var email = new ActiveDirectoryKezeloFunctions().KeresEmailcim(felugyelo.AdSid);

                    if (email != null)
                    {
                        if (string.IsNullOrEmpty(emailData.EmailAddresses))
                        {
                            emailData.EmailAddresses = email;
                            emailData.CimzettekTitle = felugyelo.Nev;
                        }
                        else
                        {
                            emailData.EmailAddresses += ";" + email;
                            emailData.CimzettekTitle = "Címzettek";
                        }
                    }
                }
                if (!string.IsNullOrEmpty(emailData.EmailAddresses))
                {
                    emailList.Add(emailData);
                }
            }
            Log.Debug("MaganelzarasFofelugyelokEmailAdatok end");
            return emailList;
        }

        public FegyelmiUgyKivizsgaloModositasaViewModel GetFegyelmiUgyDataKivizsgaloModositashoz(List<int> fegyelmiUgyIds)
        {
            List<AdFegyelmiUserItem> fegyelmiUsers = SzemelyzetFunctions.GetFegyelmiAlkalmazottak();
            var fegyelmiUgy = FindById(fegyelmiUgyIds.First());
            SzemelyzetModel kivizsgalo = null;
            if (fegyelmiUgy.KivizsgaloSzemelyId != null)
            {
                kivizsgalo = SzemelyzetFunctions.FindById(fegyelmiUgy.KivizsgaloSzemelyId ?? 0);
            }
            List<KSelect2ItemModel> szemelyzetSelectList = fegyelmiUsers.Select(x => new KSelect2ItemModel() { id = x.Sid, text = x.Displayname + (x.Rendfokozat == null ? "" : (" " + x.Rendfokozat)) }).ToList();

            FegyelmiUgyKivizsgaloModositasaViewModel result = new FegyelmiUgyKivizsgaloModositasaViewModel()
            {
                FegyelmiUgyIds = fegyelmiUgyIds,
                KivizsgalasiHatarido = fegyelmiUgy.KivizsgalasiHatarido ?? DateTime.Now,
                KivizsgaloSzemelyOptions = szemelyzetSelectList,
                KivizsgaloSzemelySid = kivizsgalo?.AdSid,
            };
            return result;
        }

        public string SaveFegyelmiUgyKivizsgaloModositasa(FegyelmiUgyKivizsgaloModositasaViewModel model)
        {
            List<int> ujFegyelmi = new List<int>();
            List<int> megvaltozottFegyelmi = new List<int>();
            List<int> toroltFegyelmi = new List<int>();
            string nev = string.Empty;

            using (DbContextTransaction transaction = KonasoftBVFonixContext.Database.BeginTransaction())
            {
                try
                {
                    var fegyelmiUgy = FindById(model.FegyelmiUgyIds.FirstOrDefault());

                    var szemelyAd = SzemelyzetFunctions.SzemelyzetLekeresVagyLetrehozas(model.KivizsgaloSzemelySid, null, fegyelmiUgy.IntezetId);

                    if (szemelyAd.Id == fegyelmiUgy.ElrendeloSzemelyId)
                    {
                        throw new WarningException("A kivizsgáló személy nem lehet az elrendelő.");
                    }

                    var esemeny = KonasoftBVFonixContext.Esemenyek.AsNoTracking().FirstOrDefault(x => x.Id == fegyelmiUgy.EsemenyId);

                    if (esemeny.EszleloId == szemelyAd.Id)
                    {
                        throw new WarningException("Az esemény észlelője nem lehet kivizsgáló.");
                    }

                    fegyelmiUgy.KivizsgaloSzemelyId = szemelyAd.Id;
                    Modify(fegyelmiUgy);

                    var naploEnitity = new Naplo
                    {
                        FegyelmiUgyId = fegyelmiUgy.Id,
                        FogvatartottId = fegyelmiUgy.FogvatartottId,
                        TipusCimkeId = (int)FegyelmiNaploTipus.KivizsgaloModositasa,
                        KivizsgaloSzemelyId = szemelyAd.Id
                    };

                    var szemely = SzemelyzetFunctions.GetFegyelmiAlkalmazottak().FirstOrDefault(x => x.Sid == model.KivizsgaloSzemelySid);
                    nev = szemely.Displayname + (szemely.Rendfokozat == null ? "" : (" " + szemely.Rendfokozat));

                    KonasoftBVFonixContext.Naplo.Add(naploEnitity);
                    KonasoftBVFonixContext.SaveChanges();
                    transaction.Commit();

                    megvaltozottFegyelmi.Add(fegyelmiUgy.Id);

                }
                catch (Exception e)
                {
                    Log.Error($"Hiba a kivizsgáló személy módosítása során (fegyelmiUgyId: {model.FegyelmiUgyIds.FirstOrDefault()})", e);
                    transaction.Rollback();
                    throw;
                }
            }

            OnFegyelmiUgyValtozas?.Invoke(ujFegyelmi, megvaltozottFegyelmi, toroltFegyelmi);
            return nev;
        }

        public List<FogvatartottFegyelmiUgyAdatokModel> GetFolyamatbanEsKiszabvaFegyelmiUgyekByFegyelmiUgyId(int fegyelmiUgyId)
        {
            List<FogvatartottFegyelmiUgyAdatokModel> lista = new List<FogvatartottFegyelmiUgyAdatokModel>();
            EsemenyekFunctions esemenyekFunctions = new EsemenyekFunctions();
            var jelenlegiUgy = GetFegyelmiUgyById(fegyelmiUgyId);
            var jelenlegiEsemeny = esemenyekFunctions.GetEsemenyById(jelenlegiUgy.EsemenyId);
            var fogvatartottFegyelmiUgyei = Table
                                            .Include(x => x.Intezet)
                                            .Include(x => x.StatuszCimke)
                                            .Include(x => x.FenyitesTipusa)
                                            .Include(x => x.FenyitesHosszaMennyisegiEgyseg)
                                            .Where(x => x.FogvatartottId == jelenlegiUgy.FogvatartottId
                                                && x.Lezarva == false &&
                                                (x.StatuszCimkeId == (int)CimkeEnums.FegyelmiUgyStatusz.FenyitesVegrehajtasAlatt
                                                ||
                                                x.StatuszCimkeId == (int)CimkeEnums.FegyelmiUgyStatusz.FenyitesKiszabva))
                                            .ToList();

            foreach (var fegyelmiUgy in fogvatartottFegyelmiUgyei)
            {
                if (fegyelmiUgy.Id != jelenlegiUgy.Id)
                {
                    DateTime tervezettVeg = new DateTime();
                    var esemeny = esemenyekFunctions.GetEsemenyById(fegyelmiUgy.EsemenyId);
                    if (fegyelmiUgy.StatuszCimkeId == (int)CimkeEnums.FegyelmiUgyStatusz.FenyitesVegrehajtasAlatt
                        && fegyelmiUgy.VegrehajtasKezdeteIdo.HasValue)
                    {
                        var vegAtszamolasNapokba =
                            fegyelmiUgy.FenyitesHosszaMennyisegiEgysegCimkeId == (int)CimkeEnums.MennyisegiEgysegek.Honap
                            ? fegyelmiUgy.VegrehajtasKezdeteIdo.Value.AddMonths((int)fegyelmiUgy.FenyitesHossza)
                            : fegyelmiUgy.VegrehajtasKezdeteIdo.Value.AddDays((int)fegyelmiUgy.FenyitesHossza);

                        tervezettVeg = fegyelmiUgy.FenyitesTipusCimkeId == (int)CimkeEnums.FenyitesTipusok.Maganelzaras ? (DateTime)fegyelmiUgy.MaganelzarasVegeDatum : vegAtszamolasNapokba;
                    }

                    FogvatartottFegyelmiUgyAdatokModel adatokModel = new FogvatartottFegyelmiUgyAdatokModel()
                    {
                        EsemenyDatuma = esemeny.EsemenyDatuma,
                        IntezetNev = fegyelmiUgy.Intezet?.KozepesNev,
                        UgySzama = fegyelmiUgy.UgySorszamaIntezetAzon + '/' + fegyelmiUgy.UgySorszamaEv.ToString() + '/' + fegyelmiUgy.UgySorszama.ToString(),
                        UgyStatusz = fegyelmiUgy.StatuszCimkeId == (int)CimkeEnums.FegyelmiUgyStatusz.FenyitesVegrehajtasAlatt
                        ? fegyelmiUgy.StatuszCimke.Nev + " " + tervezettVeg.ToString("yyyy. MM. dd.") + "-ig"
                        : fegyelmiUgy.StatuszCimke.Nev,
                        FenyitesTipusaEsHossza = fegyelmiUgy.FenyitesHossza + " " + fegyelmiUgy.FenyitesHosszaMennyisegiEgyseg.Nev.ToLower() + " " + fegyelmiUgy.FenyitesTipusa?.Nev
                    };
                    lista.Add(adatokModel);
                }
            }
            return lista;
        }
        public List<KSelect2ItemModel> GetJelenlevoIntezetiFogvatartottak(int intezetId, string queryString)
        {
            KonasoftBVFonixContext.Configuration.LazyLoadingEnabled = false;
            var jelenlevoFogvatartottak =
                (from fogv in FogvatartottFunctions.Table
                 where (
                 fogv.AktualisIntezetId == intezetId || fogv.NyilvantartoIntezetId == intezetId || intezetId == (int)BvIntezet.Bvop)
                 //jelenlevoStatuszok.Contains(fogv.NyilvantartasiStatuszKszId) 
                 && fogv.Vedett != true && fogv.FogvSzemAdatok.Any(s => (s.SzuletesiCsaladiNev_NE_HASZNALD + " " + s.SzuletesiUtonev_NE_HASZNALD + " - " + fogv.NyilvantartasiAzonosito).Contains(queryString))
                 group fogv by fogv.Id into fogvGroup
                 select fogvGroup.FirstOrDefault()).Include(i => i.FogvSzemAdatok).ToList();

            return jelenlevoFogvatartottak.Where(f => f.FogvSzemAdatok.Count > 0)
                .Select(s => new KSelect2ItemModel() { id = s.Id.ToString(), text = $"{s.FogvSzemAdatok.First().SzuletesiCsaladiNev.ToTitleCase()} {s.FogvSzemAdatok.First().SzuletesiUtonev.ToTitleCase()} – {s.NyilvantartasiAzonosito}" })
                .OrderBy(o => o.text)
                .ToList();
        }

       
      
        public ZarkaViewModel GetZarkaById(int id)
        {
            var zarka = KonasoftBVFonixContext.Zarka.Include(x => x.Korlet)
                .Include(x => x.IntezetiObjektum)
                .Include(x => x.Intezet)
                //.Include(x => x.NevelesiCsoport)
                .Include(x => x.Neme)
                //.Include(x => x.ZarkaTipus)
                //.Include(x => x.ZarkaJelleg)
                //.Include(x => x.VegrehajtasiFokok)
                .FirstOrDefault(x => x.Id == id);
            if (zarka != null)
            {
                return (ZarkaViewModel)zarka;
            }
            return null;

        }

        public FogvatartottNezet GetFogvatartottZarkahoz(int fogvatartottId)
        {
            var fogvatartott = KonasoftBVFonixContext.FogvatartottakNezet
                .Include(x => x.FogvSzemAdatok)
                //.Include(x => x.FogvatartottFenykepek)
                //.Include(x => x.VegrehajtasiFok)
                //.Include(x => x.IntezetiObjektum)
                //.Include(x => x.Korlet)
                //.Include(x => x.Zarka)
                //.Include(x => x.Rezsim)
                .FirstOrDefault(x => x.Id == fogvatartottId);
            return fogvatartott;
        }

        //public List<EsemenyJelentoFogvatartottFegyelmiUgySzamModel> GetFegyelmiUgyekForEsemenyJelentoByFogvatarottIds(List<int> fogvatartottIds)
        //{
        //    var fogvatartott = KonasoftBVFonixContext.Fogvatartottak.Where(w => fogvatartottIds.Contains(w.Id)).ToList();
        //    var fegyelmiUgyek = KonasoftBVFonixContext.FegyelmiUgyek.Where(w => fogvatartottIds.Contains(w.FogvatartottId) && w.Lezarva == false).ToList();            

        //    var result = (from f in fogvatartott
        //                  join fu in fegyelmiUgyek on f.Id equals fu.FogvatartottId
        //                  select new EsemenyJelentoFogvatartottFegyelmiUgySzamModel
        //                  {
        //                      Azonosito = f.AktualisAzonosito,
        //                      FogvtartotottId = f.Id,
        //                      Ugyszam = fu.Ugyszam,
        //                      FegyelmiUgyId = fu.Id
        //                  }).ToList();

        //    return result;
        //}

        //public List<EsemenyJelentoFogvatartottFegyelmiUgySzamModel> GetFegyelmiUgyekForEsemenyJelentoByFegyelmiUgyIds(List<int> fegyelmiUgyIds)
        //{
            
        //    var fegyelmiUgyek = KonasoftBVFonixContext.FegyelmiUgyek.Where(w => fegyelmiUgyIds.Contains(w.Id)).ToList();
            
        //    var fogvatartottIds = fegyelmiUgyek.Select(s => s.FogvatartottId).ToList();
        //    var fogvatartott = KonasoftBVFonixContext.Fogvatartottak.Where(w => fogvatartottIds.Contains(w.Id)).ToList();

        //    var result = (from f in fogvatartott
        //                  join fu in fegyelmiUgyek on f.Id equals fu.FogvatartottId
        //                  select new EsemenyJelentoFogvatartottFegyelmiUgySzamModel
        //                  {
        //                      Azonosito = f.AktualisAzonosito,
        //                      FogvtartotottId = f.Id,
        //                      Ugyszam = fu.Ugyszam,
        //                      FegyelmiUgyId = fu.Id
        //                  }).ToList();

        //    return result;
        //}
    }
}
