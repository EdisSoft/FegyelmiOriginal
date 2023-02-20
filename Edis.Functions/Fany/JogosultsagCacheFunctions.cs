using Edis.Entities.Fany;
using Edis.Functions.Base;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Web;

namespace Edis.Functions.Fany
{
    public class JogosultsagCacheFunctions : /*FunctionsBase<object,object>,*/ IJogosultsagCacheFunctions
    {
          #region mezők
        private IDictionary<JogosultsagInformaciosKulcs, JogosultsagInformacio> _jogosultsagCache;
        #endregion mezők

        public float Guid { get; set; }
        //-private LogGenerator LogGenerator { get; set; }

        public WindowsIdentity AdUserIdentity { get; set; }
        public List<Intezet> JogosultIntezetek
        {
            get
            {
                List<Intezet> kontextus = (List<Intezet>)HttpContext.Current.Session["JogosultIntezetek"];
                return kontextus;
            }
            set { SetSessionValue("JogosultIntezetek", value); }
        }
        public Intezet AktualisIntezet
        {
            get
            {
                Intezet kontextus = (Intezet)HttpContext.Current.Session["AktualisIntezet"];
                return kontextus;
            }
            set { SetSessionValue("AktualisIntezet", value); }
        }

        public Dictionary<string, HashSet<int>> UserJogosultsagok
        {
            get
            {
                Dictionary<string, HashSet<int>> kontextus = (Dictionary<string, HashSet<int>>)HttpContext.Current.Session["UserJogosultsagok"];
                return kontextus;
            }
            set { SetSessionValue("UserJogosultsagok", value); }
        }
        public Dictionary<string, int> EngedelyezettIntezetek
        {
            get
            {
                Dictionary<string, int> kontextus = (Dictionary<string, int>)HttpContext.Current.Session["EngedelyezettIntezetek"];
                return kontextus;
            }
            set { SetSessionValue("EngedelyezettIntezetek", value); }
        }

        public Dictionary<string, HashSet<string>> Szerepkorok
        {
            get
            {
                var kontextus = (Dictionary<string, HashSet<string>>)HttpContext.Current.Session["Szerepkorok"];
                return kontextus;
            }
            set { SetSessionValue("Szerepkorok", value); }
        }

        #region konstruktor
        public JogosultsagCacheFunctions(/*LogGenerator logGenerator*/)
        {
            //-LogGenerator = logGenerator;
            Random random = new Random();
            Guid = random.Next(0, 10000);
            Torol();
        }
        #endregion konstruktor

        #region cache eljárások

        void SetSessionValue(string key, object val)
        {
            if (HttpContext.Current.Session != null)
                HttpContext.Current.Session[key] = val;
        }
        public void Torol()
        {
            _jogosultsagCache = new Dictionary<JogosultsagInformaciosKulcs, JogosultsagInformacio>();
        }

        public bool Inicializalatlan()
        {
            return _jogosultsagCache == null || _jogosultsagCache.Keys.Count == 0;
        }

        // TODO: Definiálni, hogy mikor lehet törölni a cache-ből, és ehhez milyen eljárások szükségesek.
        public void HozzaadJogosultsagInformacioCachehez(JogosultsagInformacio jogosultsagInformacio)
        {
            var jogosultsagInformaciosKulcs = new JogosultsagInformaciosKulcs(
                jogosultsagInformacio.SzemelySid, jogosultsagInformacio.JogosultsagAzonosito, jogosultsagInformacio.BvIntezetId);
            if (_jogosultsagCache.ContainsKey(jogosultsagInformaciosKulcs)) 
            {
                _jogosultsagCache[jogosultsagInformaciosKulcs] = jogosultsagInformacio;
            } 
            else
            {
                _jogosultsagCache.Add(jogosultsagInformaciosKulcs, jogosultsagInformacio);
            } 
        }

        public JogosultsagInformacio KeresesJogosultsagInformacioCacheben(string sid, string jogosultsagAzonosito, int? muveletTargyBvIntId)
        {
            var globalisJogosultsagKulcs = new JogosultsagInformaciosKulcs(sid, jogosultsagAzonosito, null);
            if (_jogosultsagCache.ContainsKey(globalisJogosultsagKulcs))
            {
                return _jogosultsagCache[globalisJogosultsagKulcs];
            }

            var jogosultsagKulcs = new JogosultsagInformaciosKulcs(sid, jogosultsagAzonosito, muveletTargyBvIntId);
            if (_jogosultsagCache.ContainsKey(jogosultsagKulcs))
            {
                return _jogosultsagCache[jogosultsagKulcs];
            }

            return null;
        }

        public IDictionary<JogosultsagInformaciosKulcs, JogosultsagInformacio> JogosultsagInformacioCacheLekerese()
        {
            return _jogosultsagCache;
        }
        #endregion cache eljárások
    }
}
