//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Edis.ViewModels.JFK.FENY
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Edis.Utilities;
    using Edis.ViewModels.Base;
    using System.ComponentModel.DataAnnotations;
    using Edis.Entities.JFK.FENY;
    using System.Web.Mvc;
    using System.Web.Helpers;
    using Newtonsoft.Json.Linq;
    using Edis.ViewModels.Common;

    public class FegyelmiUgyViewModel : BaseViewModel
    {
        public DateTime LetrehozasDatuma { get; set; }
        public int EsemenyId { get; set; }

        public int FogvatartottId { get; set; }

        public string UgySorszamaIntezetAzon { get; set; }

        public int IntezetId { get; set; }

        public int UgySorszamaEv { get; set; }

        public int UgySorszama { get; set; }

        public int StatuszCimkeId { get; set; }

        public DateTime? DontesDatuma { get; set; }

        public int? ElrendeloSzemelyId { get; set; }

        public int? KivizsgaloSzemelyId { get; set; }

        public bool VanJogiKepviselet { get; set; }

        public DateTime? HatarozatDatuma { get; set; }

        public DateTime? MasodfokuHatarozatDatuma { get; set; }

        public DateTime? TorvenyszekiHatarozatDatuma { get; set; }

        public DateTime? FeljelentesKezdemenyezesenekDatuma { get; set; }

        public bool? ElsofokuHatarozatotTudomasulVette { get; set; }

        public int? FegyelmiVetsegTipusaCimkeId { get; set; }

        public int? FeljelentesKezdemenyezesenekTipusaCimkeId { get; set; }

        public int? FeljelentesStatuszaCimkeId { get; set; }

        public int? HatarozatVagyFellebbezesJogkoreCimkeId { get; set; }

        public int? FoFegyelmiUgyId { get; set; }

        public byte? UgyFoka { get; set; }

        public int? FenyitesTipusCimkeId { get; set; }

        public bool? Lezarva { get; set; }

        public bool? FelfuggesztesiJavaslat { get; set; }

        public bool? Felfuggesztve { get; set; }

        public bool? HataridoModositasJavaslat { get; set; }

        public DateTime? Hatarido { get; set; }

        public DateTime? ElsofokuTargyalasIdopontja { get; set; }
        public DateTime? MasodfokuTargyalasIdopontja { get; set; }

        public int? HatarozatHozoJogkoreCimkeId { get; set; }
        public bool? Visszakuldve { get; set; }

        public string HatarozatIndoklasa { get; set; }

        public bool? HatarozatJogerosFL { get; set; }

        public int? VetsegRendeletSzerintCimkeId { get; set; }

        public int? HatarozatotHozottSzemelyId { get; set; }

        public DateTime? KozvetitoiEljarasbaUtalvaDatum { get; set; }
        public bool? KozvetitoiEljarasKezdemenyezve { get; set; }
        public bool? KozvetitoiEljarasban { get; set; }
        public DateTime? FelfuggesztesDatum { get; set; }
        public bool? JogiKepviseletetKer { get; set; }


        public int? FenyitesHossza { get; set; }

        public int? FenyitesHosszaMennyisegiEgysegCimkeId { get; set; }

        public DateTime? VegrehajtasKezdeteIdo { get; set; }

        public DateTime? VegrehajtasVegeIdo { get; set; }

        public double? VegrehajtasHosszEddig { get; set; }

        public int? FenyitesMennyEgysCimkeId { get; set; }

        public byte? KietkezesCsokkentes { get; set; }

        public int HatarozatotHozoTorvenyszekId { get; set; }

        public int? MegszuntetesOkaCimkeId { get; set; }

        public bool? SzakteruletiVelemenyreVarFL { get; set; }
        public int? FelfuggesztesOkaCimkeId { get; set; }
        public bool? KozvetitoiSikeres { get; set; }
        public DateTime? KozvetitoiGaranciaHatarido { get; set; }
        public int? KozvetitoSzemelyId { get; set; }
        public bool? GaranciaTeljesultFl { get; set; }
        public bool? EljarasAlaVontatMeghallgattukFL { get; set; }
        public bool? FeljelentesFl { get; set; }
        public bool? ElkulonitveFl { get; set; }
        public DateTime? KivizsgalasiHatarido { get; set; }
        public DateTime? MaganelzarasVegeDatum { get; set; }
        public DateTime? MaganelzarasElrendelesDatum { get; set; }
        public int? KarteritesId { get; set; }
       

        public static explicit operator FegyelmiUgy(FegyelmiUgyViewModel model)
        {
            FegyelmiUgy entity = new FegyelmiUgy();
            entity = ValueInjecterUtilities.InjectViewModel<FegyelmiUgyViewModel, FegyelmiUgy>(model);

            return entity;
        }

        public static explicit operator FegyelmiUgyViewModel(FegyelmiUgy model)
        {
            FegyelmiUgyViewModel entity = new FegyelmiUgyViewModel();
            entity = ValueInjecterUtilities.InjectViewModel<FegyelmiUgy, FegyelmiUgyViewModel>(model);

            return entity;
        }
    }
}
