using Edis.Entities.Fany;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Functions.Fany
{
    public interface IJogosultsagCacheFunctions 
    {

        WindowsIdentity AdUserIdentity { get; set; }
        List<Intezet> JogosultIntezetek { get; set; }
        //List<KeszletRaktar> JogosultRaktarak { get; set; }
        Intezet AktualisIntezet { get; set; }
       

        Dictionary<string, HashSet<int>> UserJogosultsagok { get; set; }
        Dictionary<string, int> EngedelyezettIntezetek { get; set; }
        Dictionary<string, HashSet<string>> Szerepkorok { get; set; }
        

        #region eljárások
        void Torol();
        bool Inicializalatlan();
        void HozzaadJogosultsagInformacioCachehez(JogosultsagInformacio jogosultsagInformacio);
        JogosultsagInformacio KeresesJogosultsagInformacioCacheben(string sid, string jogosultsagAzonosito, int? muveletTargyBvIntId);
        IDictionary<JogosultsagInformaciosKulcs, JogosultsagInformacio> JogosultsagInformacioCacheLekerese();

        #endregion eljárások
    }
}
