using Edis.Entities.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Entities.Enums
{
    public enum KodszotarCsoportok
    {
        ///<summary>
        /// 001
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "001")]
        Aktivitasafoglalkozason,
        
        // RIN
        [KodszotarCsoportAzonosito(Azonosito = "RIN1")]
        Latogatas = 1,

        // RIN
        [KodszotarCsoportAzonosito(Azonosito = "RIN2")]
        SzolgaltatasUtemezes = 2,

        // RIN
        [KodszotarCsoportAzonosito(Azonosito = "RIN3")]
        LezarasStatusz = 3,

        // RIN
        [KodszotarCsoportAzonosito(Azonosito = "RIN5")]
        UgyfelTipus = 5,

        // RIN
        [KodszotarCsoportAzonosito(Azonosito = "RIN6")]
        FelulvizsgalatOka = 6,

        [KodszotarCsoportAzonosito(Azonosito = "RIN7")]
        HozzatartozoSzolgaltatasIdoszak = 7,

        [KodszotarCsoportAzonosito(Azonosito = "RIN8")]
        FogvatartottiSzolgaltatasIdoszak = 8,

        [KodszotarCsoportAzonosito(Azonosito = "RIN9")]
        EgeszsegugyiAllapotErtekek = 9,

        [KodszotarCsoportAzonosito(Azonosito = "RIN10")]
        EgeszsegugyiAllapotTipusok = 10,

        [KodszotarCsoportAzonosito(Azonosito = "RIN11")]
        IntezmenyTipusok = 11,

        [KodszotarCsoportAzonosito(Azonosito = "RIN13")]
        KozossegiFoglalkozatoCelcsoport = 13,
        [KodszotarCsoportAzonosito(Azonosito = "RIN14")]
        KozossegiFoglalkozatoKilepesOkai = 14,
        [KodszotarCsoportAzonosito(Azonosito = "RIN15")]
        KozossegiFoglalkozatoKilepesEgyebOkai = 15,

        [KodszotarCsoportAzonosito(Azonosito = "RIN18")]
        MunkaeroPiaciStatusz = 18,

        [KodszotarCsoportAzonosito(Azonosito = "RIN19")]
        UgyfelElerhetosege = 19,

        ///<summary>
        /// 002
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "002")]
        Allampolgarsag,

        ///<summary>
        /// 003
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "003")]
        Allomanybolvalotorlesoka,

        ///<summary>
        /// 004
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "004")]
        BefogadoBizeleallitasoka,

        ///<summary>
        /// 005
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "005")]
        Befogadasmodja,

        ///<summary>
        /// 006
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "006")]
        Befogadoszervezet,

        ///<summary>
        /// 007
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "007")]
        Bfengedelyezeshataskore,

        ///<summary>
        /// 008
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "008")]
        Bfoka,

        ///<summary>
        /// 009
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "009")]
        Btkkulonosreszfejezetei,

        ///<summary>
        /// 010
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "010")]
        Buncselekmenyjellege,

        ///<summary>
        /// 011
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "011")]
        Bvintezetjellege,

        ///<summary>
        /// 012
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "012")]
        Bvmunkakorkodja,

        ///<summary>
        /// 013
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "013")]
        Bvugystatusza,

        ///<summary>
        /// 014
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "014")]
        Csaladiallapot = 135,

        ///<summary>
        /// 015
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "015")]
        Cimzettszakterulet,

        ///<summary>
        /// 016
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "016")]
        Bvugytipusa,

        ///<summary>
        /// 017
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "017")]
        Cselekmenyhelye,

        ///<summary>
        /// 018
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "018")]
        Csomagminosege,

        ///<summary>
        /// 019
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "019")]
        Csomagstatusza,

        ///<summary>
        /// 020
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "020")]
        Gyogydrogprelterelesoka,

        ///<summary>
        /// 021
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "021")]
        Egeszsegiallapot,

        ///<summary>
        /// 022
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "022")]
        Fegyelmieljaraselintmodja,

        ///<summary>
        /// 023
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "023")]
        Elkovetokodja,

        ///<summary>
        /// 024
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "024")]
        Csomagvisszakuldesoka,

        ///<summary>
        /// 025
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "025")]
        Eloallitasjellege,

        ///<summary>
        /// 026
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "026")]
        Eloallitasmodja,

        ///<summary>
        /// 027
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "027")]
        Eloallitasoka,

        ///<summary>
        /// 028
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "028")]
        Eloallitottvissza,

        ///<summary>
        /// 029
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "029")]
        Elozetesmegszunesoka,

        ///<summary>
        /// 030
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "030")]
        Eltavozasoka,

        ///<summary>
        /// 031
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "031")]
        Elelmezesinorma,

        ///<summary>
        /// 032
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "032")]
        Engedellyelszallithato,

        ///<summary>
        /// 033
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "033")]
        Enyhebbvegrehajtasiszabaly,

        ///<summary>
        /// 034
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "034")]
        Fegyelmifenyiteskodja,

        ///<summary>
        /// 035
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "035")]
        Ertesitestipusa,

        ///<summary>
        /// 036
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "036")]
        Elzarasihatarozatstatusza,

        ///<summary>
        /// 037
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "037")]
        Fegyelmifenyitesstatusza,

        ///<summary>
        /// 038
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "038")]
        Fegyelmivetsegkodja,

        ///<summary>
        /// 039
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "039")]
        Fellebbezokodja,

        ///<summary>
        /// 040
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "040")]
        Kedvezmenymertekefeltszab,

        ///<summary>
        /// 041
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "041")]
        Feljelenteselintezesmodja,

        ///<summary>
        /// 042
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "042")]
        Fellebbezeseredmenye,

        ///<summary>
        /// 043
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "043")]
        Fizetettszabadsagterhere,

        ///<summary>
        /// 044
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "044")]
        Foglalkozastemaja,

        ///<summary>
        /// 045
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "045")]
        Foglalkozastipusa,

        ///<summary>
        /// 046
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "046")]
        Fogvatartottatadva,

        ///<summary>
        /// 047
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "047")]
        Tisztsegekfogvatartott,

        ///<summary>
        /// 048
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "048")]
        Folymunkavegzesmegszunesok,

        ///<summary>
        /// 049
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "049")]
        Fuggobenlevougyek,

        ///<summary>
        /// 050
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "050")]
        Kerelempanaszugybendontes,

        ///<summary>
        /// 051
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "051")]
        Iskolaivegzettseg = 211,

        ///<summary>
        /// 052
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "052")]
        Fogvatartottszallitasa,

        ///<summary>
        /// 053
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "053")]
        Latogatashelyisege,

        ///<summary>
        /// 054
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "054")]
        Elelmiszerfogyasztasa,

        ///<summary>
        /// 055
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "055")]
        Ugytipusa,

        ///<summary>
        /// 056
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "056")]
        Kapcsolattartasjogcime = 221,

        ///<summary>
        /// 057
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "057")]
        Idezesstatusza,

        ///<summary>
        /// 058
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "058")]
        Intezetbentartozkodas,

        ///<summary>
        /// 059
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "059")]
        Intezkrendelktipusa,

        ///<summary>
        /// 060
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "060")]
        Intezkedesrendelkezes,

        ///<summary>
        /// 061
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "061")]
        Atmeneticsoportbahelyezes,

        ///<summary>
        /// 062
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "062")]
        Iskolabolkimaradasoka,

        ///<summary>
        /// 063
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "063")]
        Iteletjelleg,

        ///<summary>
        /// 064
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "064")]
        Iteletkodja,

        ///<summary>
        /// 065
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "065")]
        Szvjegyzektipusa,

        ///<summary>
        /// 066
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "066")]
        Jutalomkodja,

        ///<summary>
        /// 067
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "067")]
        Jutalmazasoka,

        ///<summary>
        /// 068
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "068")]
        Karokozastelismeriteriti,

        ///<summary>
        /// 069
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "069")]
        Karokozastipusa,

        ///<summary>
        /// 070
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "070")]
        Ellenorzohataskor,

        ///<summary>
        /// 071
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "071")]
        Kerelempanasztipusa,

        ///<summary>
        /// 072
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "072")]
        Kilepeshova,

        ///<summary>
        /// 073
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "073")]
        Korlatozaskodja,

        ///<summary>
        /// 074
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "074")]
        Korlettipusa,

        ///<summary>
        /// 075
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "075")]
        Onkentjelentkszemazonosit,

        ///<summary>
        /// 076
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "076")]
        Kulsomunkaltengedelyezett,

        ///<summary>
        /// 077
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "077")]
        Latogatasminosege,

        ///<summary>
        /// 078
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "078")]
        Megye,

        ///<summary>
        /// 079
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "079")]
        Letszamstatisztikajellege,

        ///<summary>
        /// 080
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "080")]
        Megjelent,

        ///<summary>
        /// 081
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "081")]
        Megszakitasoka,

        ///<summary>
        /// 082
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "082")]
        Megorzesideiglbefmegszunt,

        ///<summary>
        /// 083
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "083")]
        Latogatasstatusza,

        ///<summary>
        /// 084
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "084")]
        TAJszamtipusa,

        ///<summary>
        /// 085
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "085")]
        Iteletstatusza,

        ///<summary>
        /// 086
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "086")]
        Mellekbuntetes,

        ///<summary>
        /// 087
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "087")]
        Mellekbuntetesmertekegysege,

        ///<summary>
        /// 088
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "088")]
        Minosites,

        ///<summary>
        /// 089
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "089")]
        Munkahelyorzesbiztkategoria,

        ///<summary>
        /// 090
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "090")]
        Neme,

        ///<summary>
        /// 091
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "091")]
        Munkavegzestipusa,

        ///<summary>
        /// 092
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "092")]
        Munkabaallitando,

        ///<summary>
        /// 093
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "093")]
        Partnerjelleg,

        ///<summary>
        /// 094
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "094")]
        Munkavegzokepesseg,

        ///<summary>
        /// 095
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "095")]
        Munkahelytipusa,

        ///<summary>
        /// 096
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "096")]
        Biztonsagicsoport,

        ///<summary>
        /// 097
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "097")]
        Velemenyfeljegyzesstatusza,

        ///<summary>
        /// 098
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "098")]
        Intezetbekerulesorvetmod,

        ///<summary>
        /// 099
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "099")]
        Munkabaallitastakadalyozook = 265,
        

        ///<summary>
        /// 100
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "100")]
        Okmanyjellege,

        ///<summary>
        /// 101
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "101")]
        Oktatastipusa,

        ///<summary>
        /// 102
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "102")]
        Orszag = 293,

        ///<summary>
        /// 103
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "103")]
        Beszamitastipusa,

        ///<summary>
        /// 104
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "104")]
        KNyvisszaigazolasjelzo,

        ///<summary>
        /// 105
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "105")]
        Velemenyfeljegyzestipusa,

        ///<summary>
        /// 106
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "106")]
        Orizetmegszuntetesoka,

        ///<summary>
        /// 107
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "107")]
        Technikaieszkozok,

        ///<summary>
        /// 108
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "108")]
        Elzarasihatarozattipusa,

        ///<summary>
        /// 109
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "109")]
        Rendelesenmegjelent,

        ///<summary>
        /// 110
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "110")]
        Resztvevok,

        ///<summary>
        /// 111
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "111")]
        Sajatszamitas,

        ///<summary>
        /// 112
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "112")]
        Szabadsagkiadasjellege,

        ///<summary>
        /// 113
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "113")]
        Veszelyessegminositese,

        ///<summary>
        /// 114
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "114")]
        Vegrehajtasifokozat,

        ///<summary>
        /// 115
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "115")]
        Szallitasoka,

        ///<summary>
        /// 116
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "116")]
        Visszaesesfoka,

        ///<summary>
        /// 117
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "117")]
        Zarkatipusa,

        ///<summary>
        /// 118
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "118")]
        Szokeshonnan,

        ///<summary>
        /// 119
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "119")]
        Tanulmanyieredmeny,

        ///<summary>
        /// 120
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "120")]
        Tartamegysege,

        ///<summary>
        /// 121
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "121")]
        Tartozkodasihely,

        ///<summary>
        /// 122
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "122")]
        Tavolletokaft,

        ///<summary>
        /// 123
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "123")]
        Nyilvantartottstatusz,

        ///<summary>
        /// 124
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "124")]
        Ujeljaras,

        ///<summary>
        /// 125
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "125")]
        Bvugyeredmenye,

        ///<summary>
        /// 126
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "126")]
        Elszamolas,

        ///<summary>
        /// 127
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "127")]
        Szabadulasmodja,

        ///<summary>
        /// 128
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "128")]
        VelemenyezoFeljkeszitoszt,

        ///<summary>
        /// 129
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "129")]
        Szakkepesites,

        ///<summary>
        /// 130
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "130")]
        Vegrehajtaselmaradasoka,

        ///<summary>
        /// 131
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "131")]
        Szallitasravonatkozorendelk,

        ///<summary>
        /// 132
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "132")]
        Szolgalattipusa,

        ///<summary>
        /// 133
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "133")]
        Zarkabahelyezesstatusza,

        ///<summary>
        /// 134
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "134")]
        FMeszkozfunkcio,

        ///<summary>
        /// 135
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "135")]
        Kenyszeszkalkalmazascelja,

        ///<summary>
        /// 136
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "136")]
        Kenyszeszkalkalmazasoka,

        ///<summary>
        /// 137
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "137")]
        Kenyszeritoeszkozok,

        ///<summary>
        /// 138
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "138")]
        Keszletvaltozasoka,

        ///<summary>
        /// 139
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "139")]
        Nyomtatvanytipusa,

        ///<summary>
        /// 140
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "140")]
        Logyakorlateredmenye,

        ///<summary>
        /// 141
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "141")]
        Reszveteljellege,

        ///<summary>
        /// 142
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "142")]
        Tavolletoka,

        ///<summary>
        /// 143
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "143")]
        Elkovetesmodja,

        ///<summary>
        /// 144
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "144")]
        Elkoveteseszkoze,

        ///<summary>
        /// 145
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "145")]
        Esemenyhelye,

        ///<summary>
        /// 146
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "146")]
        MozgasjellegeFtszallitasa,

        ///<summary>
        /// 147
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "147")]
        Leveltipusa,

        ///<summary>
        /// 148
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "148")]
        Levelelintezesimodja,

        ///<summary>
        /// 149
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "149")]
        Beosztas,

        ///<summary>
        /// 150
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "150")]
        Rendfokozat,

        ///<summary>
        /// 151
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "151")]
        Cikkcsoport,

        ///<summary>
        /// 152
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "152")]
        Cikkjelleg,

        ///<summary>
        /// 153
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "153")]
        Mennyisegiegyseg,

        ///<summary>
        /// 154
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "154")]
        Csokkmunkaidosfogloka,

        ///<summary>
        /// 155
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "155")]
        Fogvatartasjellege,

        ///<summary>
        /// 156
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "156")]
        Allapotjelzo,

        ///<summary>
        /// 157
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "157")]
        Atmcsoportbolkihelyezesoka,

        ///<summary>
        /// 158
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "158")]
        Gyogydrogprelterelesvege,

        ///<summary>
        /// 159
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "159")]
        Elintezendovelemenyezendo,

        ///<summary>
        /// 160
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "160")]
        EsemenyJellege,

        ///<summary>
        /// 161
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "161")]
        Kerelempanaszelintmodja,

        ///<summary>
        /// 162
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "162")]
        Jutalomelintezesimodja,

        ///<summary>
        /// 163
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "163")]
        Vetkessegmegallapitasa,

        ///<summary>
        /// 164
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "164")]
        Hatarozatfellebbezesjogkore,

        ///<summary>
        /// 165
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "165")]
        Szvjegyzekenszereples,

        ///<summary>
        /// 166
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "166")]
        Ujsagkodja,

        ///<summary>
        /// 167
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "167")]
        Foglalkozasstatusza,

        ///<summary>
        /// 168
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "168")]
        Kuldottadatpostastatusza,

        ///<summary>
        /// 169
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "169")]
        Adatpostacsomagtipusa,

        ///<summary>
        /// 170
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "170")]
        Adatpostahordozo,

        ///<summary>
        /// 171
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "171")]
        Cselekmenyelkovnapszaka,

        ///<summary>
        /// 172
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "172")]
        Fogadottadatpostastatusza,

        ///<summary>
        /// 173
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "173")]
        Foganatbaveteloka,

        ///<summary>
        /// 174
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "174")]
        Adatkuldesfogadastipusa,

        ///<summary>
        /// 175
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "175")]
        Fegyfenyitettekkapcstartas,

        ///<summary>
        /// 176
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "176")]
        Letszamstatisztikak,

        ///<summary>
        /// 177
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "177")]
        Adatvaltozasjelentesallapot,

        ///<summary>
        /// 178
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "178")]
        Adatvaltozasfogadasallapota,

        ///<summary>
        /// 179
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "179")]
        Itelettartamcsoportok1,

        ///<summary>
        /// 180
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "180")]
        Eletkorcsoportok,

        ///<summary>
        /// 181
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "181")]
        Fenykepkeszultsegfok,

        ///<summary>
        /// 182
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "182")]
        Fenykepezeselojegyzesoka,

        ///<summary>
        /// 183
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "183")]
        Munkaltatasstatusza,

        ///<summary>
        /// 184
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "184")]
        Targyalasonvaloresztvetel,

        ///<summary>
        /// 185
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "185")]
        Adatkereselintezesimodja,

        ///<summary>
        /// 186
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "186")]
        Adatkerescelja,

        ///<summary>
        /// 187
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "187")]
        Velhetolegazonosakjelzese,

        ///<summary>
        /// 188
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "188")]
        Tisztsegmegszunesenekoka,

        ///<summary>
        /// 189
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "189")]
        Tobbletinformaciooka,

        ///<summary>
        /// 190
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "190")]
        Adatszolgaltataskerestipusa,

        ///<summary>
        /// 191
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "191")]
        Valtjelentescsomagstatusza,

        ///<summary>
        /// 192
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "192")]
        Verzioallapotastatusza,

        ///<summary>
        /// 193
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "193")]
        Idegenrorizetmegszunesoka,

        ///<summary>
        /// 194
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "194")]
        Orizetbeveteltipusa,

        ///<summary>
        /// 195
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "195")]
        Zarkajellege,

        ///<summary>
        /// 196
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "196")]
        Elkulonitestipusa,

        ///<summary>
        /// 197
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "197")]
        Szallitastipusa,

        ///<summary>
        /// 198
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "198")]
        Igennem,

        ///<summary>
        /// 199
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "199")]
        Vannincs,

        ///<summary>
        /// 284
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "284")]
        FogvatartottJelenlegiTartozkodasNyilvantartottStatusz,

        ///<summary>
        /// 301
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "301")]
        Verziominositese,

        ///<summary>
        /// 302
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "302")]
        Verzioelerhetosege,

        ///<summary>
        /// 303
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "303")]
        KNORadattablaktipusa,

        ///<summary>
        /// 304
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "304")]
        Velemenytipusok,

        ///<summary>
        /// 305
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "305")]
        Eletfogytfelulvizsgalatesed,

        ///<summary>
        /// 306
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "306")]
        Elozetesletartoztatastartam,

        ///<summary>
        /// 307
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "307")]
        Allampolgarsagcsoport,

        ///<summary>
        /// 308
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "308")]
        Itelettartamcsoportok2,

        ///<summary>
        /// 309
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "309")]
        Szallitasstatusza,

        ///<summary>
        /// 310
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "310")]
        EletkorcsoportokEUTanacs,

        ///<summary>
        /// 311
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "311")]
        Verziofeldolgozottsaga,

        ///<summary>
        /// 312
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "312")]
        ItelettartamcsoportokEU,

        ///<summary>
        /// 313
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "313")]
        Velhetolegazonoscsopstat,

        ///<summary>
        /// 314
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "314")]
        Tisztalkodasicikkfelszerel,

        ///<summary>
        /// 315
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "315")]
        Talalkozastipusa,

        ///<summary>
        /// 316
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "316")]
        Buncselekmenycsoportok,

        ///<summary>
        /// 317
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "317")]
        Hetiletszamadatainakmegnev,

        ///<summary>
        /// 318
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "318")]
        Fegyfenystatisztikatipusok,

        ///<summary>
        /// 319
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "319")]
        Eustatisztikaktipusai,

        ///<summary>
        /// 320
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "320")]
        EustatBNOszerintmegnev,

        ///<summary>
        /// 321
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "321")]
        Eufogesemenyekbolkigymegn,

        ///<summary>
        /// 322
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "322")]
        Kiadasoka,

        ///<summary>
        /// 323
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "323")]
        Biztonsagicsopfelulvizsgok,

        ///<summary>
        /// 324
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "324")]
        Ugyeszsegtoladatkeresoka,

        ///<summary>
        /// 325
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "325")]
        Kapcsolattartokorcsoportja,

        ///<summary>
        /// 326
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "326")]
        Ugyeszsegnelfolyamatosugy,

        ///<summary>
        /// 327
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "327")]
        Ugyeszsegiadatkerestipusa,

        ///<summary>
        /// 328
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "328")]
        Kapcsolattartasmegszunesoka,

        ///<summary>
        /// 329
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "329")]
        Listazandokerelmekkore,

        ///<summary>
        /// 330
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "330")]
        Kapcsolattartasmodja,

        ///<summary>
        /// 331
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "331")]
        Cimjellege,

        ///<summary>
        /// 332
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "332")]
        Eljarasfokaazitelkezesben,

        ///<summary>
        /// 333
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "333")]
        Elozetesletrendlejaratoka,

        ///<summary>
        /// 334
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "334")]
        Nevjellege,

        ///<summary>
        /// 335
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "335")]
        Ellatasgyakorisagatcikk,

        ///<summary>
        /// 336
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "336")]
        Magyarorszagregioi,

        ///<summary>
        /// 337
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "337")]
        Adatszolgaltatastipusa,

        ///<summary>
        /// 338
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "338")]
        Belegteteleinekforrasa,

        ///<summary>
        /// 339
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "339")]
        Magyarorszagmegyei2jegyu,

        ///<summary>
        /// 340
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "340")]
        Folyuggyelkapcsesemeny,

        ///<summary>
        /// 341
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "341")]
        Folyugyekstatusza,

        ///<summary>
        /// 342
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "342")]
        Nyomozasicselekmenyhelye,

        ///<summary>
        /// 343
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "343")]
        Szallitasiutvonaltipusa,

        ///<summary>
        /// 344
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "344")]
        Szallitasiutirany,

        ///<summary>
        /// 345
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "345")]
        Specialisetkezesiszokas,

        ///<summary>
        /// 346
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "346")]
        Szemelykituntetocime,

        ///<summary>
        /// 347
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "347")]
        Fegyelmivetseg111996IM,

        ///<summary>
        /// 348
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "348")]
        Bvugyeloterjmellozesoka,

        ///<summary>
        /// 349
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "349")]
        Gondnoksagtipusa,

        ///<summary>
        /// 350
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "350")]
        Gondnoksaghatalya,

        ///<summary>
        /// 351
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "351")]
        Gondnokkategoria,

        ///<summary>
        /// 352
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "352")]
        TavozasTipusok,

        ///<summary>
        /// 353
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "353")]
        KekkhAdatAllapota,

        ///<summary>
        /// 354
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "354")]
        BiztonsagiKockazat,

        ///<summary>
        /// 355
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "355")]
        RezsimKategoria,

        ///<summary>
        /// 400
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "400")]
        Foglalkoztatasiesemenyek,

        ///<summary>
        /// 401
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "401")]
        Oktatasiintezmenyjellege,

        ///<summary>
        /// 402
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "402")]
        Munkajellege,

        ///<summary>
        /// 403
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "403")]
        Munkahelyibeosztas,

        ///<summary>
        /// 404
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "404")]
        Munkahelyibesorolas,

        ///<summary>
        /// 405
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "405")]
        Munkavegzesszunetelesoka,

        ///<summary>
        /// 406
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "406")]
        Naptarinaptipusa,

        ///<summary>
        /// 407
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "407")]
        Munkaraalkalmassag,

        ///<summary>
        /// 408
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "408")]
        Napitevekenysegtipusa,

        ///<summary>
        /// 409
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "409")]
        Munkahelymuszakszama,

        ///<summary>
        /// 410
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "410")]
        Oktatasbefejezesmodja,

        ///<summary>
        /// 411
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "411")]
        Oktataseredmeny,

        ///<summary>
        /// 412
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "412")]
        Oktatasrolhianyzasoka,

        ///<summary>
        /// 413
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "413")]
        Munkaraelojegyzesstatusza,

        ///<summary>
        /// 414
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "414")]
        Munkakorok,

        ///<summary>
        /// 415
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "415")]
        MunkaidokedvtipusaNEMKELL,

        ///<summary>
        /// 416
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "416")]
        Leallasoka,

        ///<summary>
        /// 417
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "417")]
        Vizsgatipusa,

        ///<summary>
        /// 418
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "418")]
        Oktataspillanatnyistatusza,

        ///<summary>
        /// 419
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "419")]
        Munkaltatasfajtaja,

        ///<summary>
        /// 420
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "420")]
        Szabadsagjellege,

        ///<summary>
        /// 421
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "421")]
        Iskolaitevekenysegtipusa,

        ///<summary>
        /// 422
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "422")]
        Tervezettvizsgastatusza,

        ///<summary>
        /// 423
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "423")]
        Munkabaallitastipusa,

        ///<summary>
        /// 424
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "424")]
        Muszakbeosztas,

        ///<summary>
        /// 425
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "425")]
        Elojegyzestipusa,

        ///<summary>
        /// 500
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "500")]
        Ugyeszsegnelutolsoeljmozz,

        ///<summary>
        /// 501
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "501")]
        Ugyeszsegenugyallasa,

        ///<summary>
        /// 502
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "502")]
        Ugyeszsegenbuntetoeljpoz,

        ///<summary>
        /// 600
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "600")]
        FogvatartottElelmezesiNorma,

        ///<summary>
        /// 602
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "602")]
        OrvosiEllatasTipus,


        ///<summary>
        /// 603
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "603")]
        FogvatartottEUEngedely,

        ///<summary>
        /// 603
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "605")]
        MunkaraAlkalmassag,

        ///<summary>
        /// 700
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "700")]
        SzabadSzovegSablonKategoria,

        ///<summary>
        /// 900
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "900")]
        Tesztelesstatusza,

        ///<summary>
        /// 901
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "901")]
        Teszteleseredmenye,

        ///<summary>
        /// L00
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "L00")]
        Raktarihelytipusa=1000,

        ///<summary>
        /// L01
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "L01")]
        Letetszelvenytipusa,

        ///<summary>
        /// L02
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "L02")]
        Okmanycikktipusa,

        ///<summary>
        /// L03
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "L03")]
        Ertekcikktipusa,

        ///<summary>
        /// L04
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "L04")]
        Targycikktipusa,

        ///<summary>
        /// L05
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "L05")]
        Letetszelvenystatusza,

        ///<summary>
        /// L06
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "L06")]
        Valutanem,

        ///<summary>
        /// M05
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "M05")]
        MunkahelyNev,

        ///<summary>
        /// M06
        ///</summary>
        [KodszotarCsoportAzonosito(Azonosito = "M06")]
        MunkakorNev,

        //[KodszotarCsoportAzonosito(Azonosito = "1038")]
        //MennyisegiEgyseg = 1038,

        [KodszotarCsoportAzonosito(Azonosito = "1618")]
        CsomagVisszavetelStatusz = 1618,

        [KodszotarCsoportAzonosito(Azonosito = "1617")]
        WebshopMennyisegiEgyseg = 1617,

        [KodszotarCsoportAzonosito(Azonosito = "1657")]
        KeszletNyilvantartoIdoIntervallumok = 1657,

        [KodszotarCsoportAzonosito(Azonosito = "1611")]
        CsomagStatuszok = 1611,

        [KodszotarCsoportAzonosito(Azonosito = "1613")]
        TisztasagiFelszerelesek = 1613,

        [KodszotarCsoportAzonosito(Azonosito = "1614")]
        ElelmiszerekEsEtkezesiEszkozok = 1614,

        [KodszotarCsoportAzonosito(Azonosito = "1620")]
        Papir = 1620,

        [KodszotarCsoportAzonosito(Azonosito = "1619")]
        Dohany = 1619,

        [KodszotarCsoportAzonosito(Azonosito = "1615")]
        EgyebCikk = 1615,

        [KodszotarCsoportAzonosito(Azonosito = "1616")]
        NemTeljesulesOk = 1616,

        [KodszotarCsoportAzonosito(Azonosito = "1636")]
        KeszletCsokkenesStatusz = 1636,

        [KodszotarCsoportAzonosito(Azonosito = "1637")]
        RaktarkoziMozgasStatusz = 1637,

        [KodszotarCsoportAzonosito(Azonosito = "1639")]
        EgyebBoltiTevekenyseg = 1639,

        [KodszotarCsoportAzonosito(Azonosito = "1645")]
        RuhazatRaktariTetelKategoria = 1645,

        [KodszotarCsoportAzonosito(Azonosito = "1646")]
        RuhazatiTetelMozgasTipus = 1646,

        [KodszotarCsoportAzonosito(Azonosito = "1109")]
        ZoldsegGyumolcsCsoport = 1109
    }
}
