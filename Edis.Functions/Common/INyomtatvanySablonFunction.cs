using Edis.ViewModels.Common;
using Novacode;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Edis.Functions.Common
{
    public interface INyomtatvanySablonFunction : INyomtatvanySablonFunctionBase
    {
       
        #region Fegyelmi fenyítés

        //List<NyomtatvanySablonModel> GetFegyelmiNyomtatvanyokList();
        //DocX EljarasAlaVontMeghallgatasiJegyzokonyvFegyelmiUgyKivizsgalasahoz(int fegyelmiUgyId);
        //DocX TanuMeghallgatasiJegyzokonyvFegyelmiUgyKivizsgalasahoz(int fegyelmiUgyId);
        //DocX JegyzokonyvTanuSzemelyiAllomanyiTagMeghallgatasarol(int fegyelmiUgyId);
        //DocX FegyelmiTargyalasiJegyzokonyv(int fegyelmiUgyId);
        //DocX JegyzokonyvFogvatartottakSzembesiteserol(int fegyelmiUgyId);
        //DocX JegyzokonyvAlairasMegtagadasarol(int fegyelmiUgyId);
        //DocX FanyErtesitoLap(int fegyelmiUgyId);
        //DocX FegyelmiElkulonitesiLap(int fegyelmiUgyId);
        //DocX TajekoztatoAFegyelmiEljarassalOsszefuggoJogokrolKotelezettsegekrol(int fegyelmiUgyId);
        //DocX VegrehajtasiLap(int fegyelmiUgyId);
        //DocX NyilatkozatKozvetitoiEljarasonValoReszvetelrolEljarasAlaVontFogvatartott(int fegyelmiUgyId);
        //DocX NyilatkozatKozvetitoiEljarasonValoReszvetelrolSertett(int fegyelmiUgyId);
        //DocX FeljegyzesKozvetitoiMegbeszelesrol(int fegyelmiUgyId);
        //DocX MegallapodasKozvetitoiEljarasEredmenyerol(int fegyelmiUgyId);
        //DocX NyilatkozatVedoErtesitesehez(int fegyelmiUgyId);
        //DocX NyilatkozatVedoKirendelesehez(int fegyelmiUgyId);
        //DocX VedoKiertesitese(int fegyelmiUgyId);
        //DocX VedoKirendelese(int fegyelmiUgyId);
        //DocX VedoTelefononTortenoTajekoztatasa(int fegyelmiUgyId);
        //DocX VedoKirendeleseNyomtatvany(int? fegyelmiUgyId, int? iktatasId);
        //DocX MeghatalmazottVedoKirendeleseNyomtatvany(int? fegyelmiUgyId, int? iktatasId);


        //DocX ErtesitoLap(int fegyelmiUgyId, int? iktatasId = null);
        //DocX ElkulonitesiLap(int? fegyelmiUgyId, int? iktatasId = null);
        //DocX JegyzokonyvAlairasMegtagadasarolFegyelmi(int? fegyelmiUgyId, int? iktatasId);
        //DocX FegyemiVedoTelefononTortenoTajekoztatasa(int? naplobejegyzesId, int? iktatasId);
        //DocX FegyelmiEljarasKezdemenyezoLap(int fegyelmiUgyId, int? iktatasId = null);
        //DocX LefoglalasiJegyzokonyv(int fegyelmiUgyId, int? iktatasId = null);
        //DocX IratosszesitoFegyelmiUgyben(int fegyelmiUgyId, int? iktatasId = null);
        //DocX OsszefoglaloJelentesDocxNyomtatvany(int? naplobejegyzesId, int? iktatasId);
        //DocX BuntetoFeljelentesDocxNyomtatvany(int? naplobejegyzesId, int? iktatasId);
        ////DocX OtosSzamuMellekletDocxNyomtatvany(int fegyelmiUgyId);
        ////DocX KarteritesFeljelentoDocxNyomtatvany(int fegyelmiUgyId);
        //DocX KarjelentoLapDocxNyomtatvany(int? esemenyId, int? iktatasId, int? fegyelmiUgyId = null);
        //DocX KarjelentoLapErtesitoLapDocxNyomtatvany(int esemenyId);
        //DocX ErtesitoLapByEsemenyIdDocxNyomtatvany(int esemenyId);
        //DocX TeritesmentesTelefonalasNyomtatvany(int jutalmiUgyId);
        #endregion

    }
}