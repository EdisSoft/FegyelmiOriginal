using Edis.Entities.Enums;
using Edis.Entities.Fany;
using Edis.Functions.Base;
using Edis.Functions.Common;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Functions.Fany
{
    using Edis.ViewModels.Fany;

    public interface IJogosultsagKezeloFunctions
    {
        // átrakni a base interfészbe ha lesz
        IAlkalmazasKontextusFunctions AlkalmazasKontextus { get; set; }

        #region jellemzők
        IActiveDirectoryFunctions ADTarolo { get; }
        ISzemelyzetFunctions SzemelyzetTarolo { get; }
        //ITorzsadatLekerdezo TorzsadatLekerdezo { get; }
        SzemelyzetSzerepkor AktualisSzemelyzetSzerepkor { get; set; }
        SzemelyzetCsoport AktualisSzemelyzetCsoport { get; set; }
     

        void Belepes(WindowsIdentity sid, string clientHostName);
        void BelepesCsakActiveDirectoryOlvasassal(WindowsIdentity sid, string clientHostName, int intezetId);
        object AktualisJogosultsagFa { get; set; }
        SzerepkorModositasTipus AktualisSzerepkorModositasTipus { get; set; }

        #endregion

        #region eljárások
        bool IsDebugPermissions(string sid);
        void LehetsegesIntezetekBeallitasa(WindowsIdentity sid);
        void LehetsegesIntezetekBeallitasaCsakActiveDirectoryOlvasassal(WindowsIdentity sid);
        void LehetsegesIntezetekBeallitasa(WindowsIdentity sid, string kapottAzonosito);
        bool VanJogosultsaga(Jogosultsagok jogosultsagAzonosito);
        [Obsolete("Az egy paraméteres a használandó. A sid a WindowsIdentity.Current() alapján, a BvIntId az Alkalmazás kontextuból kitalálható")]
        // TODO: A muveletTargyBvIntId helyett lehetne egy IMuveletTargy IF-propertyInfo implementáló objektum paraméter is.
        bool VanJogosultsaga(string jogosultsagAzonosito);
        [Obsolete("Az egy paraméteres a használandó. A sid a WindowsIdentity.Current() alapján, a BvIntId az Alkalmazás kontextuból kitalálható")]
        bool VanJogosultsaga(WindowsIdentity sid, string jogosultsagAzonosito, int? muveletTargyBvIntId);
        bool VanJogosultsaga(Jogosultsagok jogosultsagAzonosito, int bvIntId);
        bool VanJogosultsaga(string sid, Jogosultsagok jogosultsagAzonosito, int? bvIntId = null);
        List<Intezet> IntezetListaLegyujtes(string sid);
        //void SetLehetsegesRaktarak(List<int> intezetIds);
        void LehetsegesIntezetekBeallitasaAdCsoportbol(UserPrincipal user);

        void BelepesActiveDirectoryOlvasassal(UserPrincipal user, string clientHostName, int intezetId);
        void JogosultsagNelkuliIntezetBeallitas(WindowsIdentity sid);


        #region ActiveDirectory szolgáltatások
        UserPrincipal BetoltesUserPrincipal(string sid);
        List<GroupPrincipal> ListazasGroupPrincipal();
        List<GroupPrincipal> FelhasznaloAdGroupLista(string sid);
        #endregion

        #region FANY adatbázis szolgáltatások
        //void TranzakcioInditas();
        //void TranzakcioMentes();


        //Szemelyzet LekerAktualisFelhasznalo();
        //Intezet LekerAktualisIntezet();

        ////IList<SzemelyzetCsoport> ListazasIntezetSzemelyzetCsoportjai(int intezetId);
        //IList<SzemelyzetSzerepkor> ListazasIntezetSzerepkorei(int intezetId);
        //IList<SzemelyzetSzerepkor> ListazasCsoportSzerepkorei(string sid);
        //IList<SzemelyzetJogosultsag> ListazasSzerepkorJogosultsagai(int szerepkorId);
        //IList<JogosultsagInformacio> ListazasJogosultsagInformacioAdatbazisbol(WindowsIdentity sid);
        #endregion

        #region Szemelyzet eljárások

        //List<string> SzemelyzetSzerepkorei(string sid);
        //void JogosultsagBeallitasokTorlese();
        //  Szemelyzet SzemelyzetLekeresVagyLetrehozas(string sidStr, string userName);
        // Szemelyzet SzemelyzetLekeresVagyLetrehozas(string sidStr, string userName, bool frissitesAdAlapjan);
        //IList<SzemelyzetCsoport> SzemelyzetCsoportListazas();
        //SzemelyzetCsoport SzemelyzetCsoportKereses(int id);
        //void SzemelyzetCsoportLetrehozas(SzemelyzetCsoport szemelyzetCsoport);
        //void SzemelyzetCsoportModositas(SzemelyzetCsoport szemelyzetCsoport);
        //SzemelyzetCsoport UjSzemelyzetCsoportPeldanyKeres();
        //void SzemelyzetCsoportTorles(SzemelyzetCsoport szemelyzetCsoport);
        //void SzemelyzetCsoportTorlesVisszavonas(SzemelyzetCsoport szemelyzetCsoport);

        //void SzemelyzetCsoportSzerepkorLetrehozas(SzemelyzetCsoportSzerepkor szemelyzetCsoportSzerepkor);
        //void SzemelyzetCsoportSzerepkorModositas(SzemelyzetCsoportSzerepkor szemelyzetCsoportSzerepkor);
        //void SzemelyzetCsoportSzerepkorTorles(SzemelyzetCsoportSzerepkor szemelyzetCsoportSzerepkor);
        //void SzemelyzetCsoportSzerepkorTorlesVisszavonas(SzemelyzetCsoportSzerepkor szemelyzetCsoportSzerepkor);
        //SzemelyzetCsoportSzerepkor UjSzemelyzetCsoportSzerepkorPeldanyKeres();


        //IList<SzemelyzetSzerepkor> SzemelyzetSzerepkorListazas();
        //SzemelyzetSzerepkor SzemelyzetSzerepkorKereses(int id);
        //void SzemelyzetSzerepkorLetrehozas(SzemelyzetSzerepkor szemelyzetSzerepkor);
        //void SzemelyzetSzerepkorModositas(SzemelyzetSzerepkor szemelyzetSzerepkor);
        //SzemelyzetSzerepkor UjSzemelyzetSzerepkorPeldanyKeres();
        //void SzemelyzetSzerepkorTorles(SzemelyzetSzerepkor szemelyzetSzerepkor);
        //void SzemelyzetSzerepkorTorlesVisszavonas(SzemelyzetSzerepkor szemelyzetSzerepkor);

        //void SzemelyzetSzerepkorJogosultsagLetrehozas(SzemelyzetSzerepkorJogosultsag szemelyzetSzerepkorJogosultsag);
        //void SzemelyzetSzerepkorJogosultsagModositas(SzemelyzetSzerepkorJogosultsag szemelyzetSzerepkorJogosultsag);
        //void SzemelyzetSzerepkorJogosultsagTorles(SzemelyzetSzerepkorJogosultsag szemelyzetSzerepkorJogosultsag);
        //void SzemelyzetSzerepkorJogosultsagTorlesVisszavonas(SzemelyzetSzerepkorJogosultsag szemelyzetSzerepkorJogosultsag);
        //SzemelyzetSzerepkorJogosultsag UjSzemelyzetSzerepkorJogosultsagPeldanyKeres();

        //IList<SzemelyzetJogosultsag> SzemelyzetJogosultsagListazas();

        //IList<SzemelyzetJogosultsagCsoportok> SzemelyzetJogosultsagCsoportokListazas();

        #endregion Szemelyzet eljárások


        #endregion
    }
}
