// DB-ből generált - SZG

const JutalmazasStatusz = Object.freeze({
  /** 5*/
  Elutasitva: 5,
  /** 2*/
  Engedelyezve: 2,
  /** 6*/
  Kihirdetve: 6,
  /** 3*/
  OsztalyvezetonekElkuldve: 3,
  /** 4*/
  ParancsnoknakElkuldve: 4,
  /** 1*/
  ReintegaciosTisztnekElkuldve: 1,
});

const BVNKivul = Object.freeze({
  /** 8*/
  BuntetesFelbeszakitas: 8,
  /** 7*/
  Eloallitas: 7,
  /** 9*/
  Eltavozas: 9,
  /** 10*/
  NyomozasraKiadas: 10,
});

const IntezetiEgyeb = Object.freeze({
  /** 17*/
  BFBMegjelenes: 17,
  /** 12*/
  FegyelmiElkulonites: 12,
  /** 11*/
  FegyelmiUgyTargyalas: 11,
  /** 24*/
  Foglalkoztatas: 24,
  /** 18*/
  Latogatas: 18,
  /** 13*/
  Maganelzaras: 13,
  /** 14*/
  Szallitas: 14,
  /** 16*/
  ZarkabaHelyezes: 16,
});

const IntezetiFix = Object.freeze({
  /** 19*/
  Szabaditas: 19,
  /** 20*/
  Tavtargyalas: 20,
});

const JutalmazasJogkor = Object.freeze({
  /** 21*/
  ReintegraciosTiszt: 21,
  /** 22*/
  Osztalyvezeto: 22,
  /** 23*/
  Parancsnok: 23,
});

const ElkovetesEszkoze = Object.freeze({
  /** 25*/
  Borotvapenge: 25,
  /** 28*/
  ButorAlkatresz: 28,
  /** 26*/
  Furesz: 26,
  /** 27*/
  Kes: 27,
  /** 30*/
  NincsMeghatarozva: 30,
  /** 29*/
  ZarkaFelszereles: 29,
});

const EsemenyJellege = Object.freeze({
  /** 38*/
  EgyebVetseg: 38,
  /** 34*/
  FogvatartottakKozottiVetseg: 34,
  /** 31*/
  MunkavegzesselKapcsolatosVetsegek: 31,
  /** 35*/
  OnEgeszsegkarositoVetseg: 35,
  /** 37*/
  RendkivuliEsemenyek: 37,
  /** 33*/
  RendszabalyokMegsertese: 33,
  /** 32*/
  SzemelyzettelKapcsolatosVetsegek: 32,
  /** 36*/
  VisszateresselOsszefuggoVetseg: 36,
});

const EsemenyModja = Object.freeze({
  /** 39*/
  Falbontas: 39,
  /** 44*/
  NincsMeghatarozva: 44,
  /** 42*/
  Orvtamadas: 42,
  /** 43*/
  Oncsonkitas: 43,
  /** 40*/
  Racsfureszeles: 40,
  /** 41*/
  ZarkaEltorlaszolas: 41,
});

const EsemenyHelyszine = Object.freeze({
  /** 62*/
  BirosagUgyeszseg: 62,
  /** 57*/
  EgeszsegugyiReszleg: 57,
  /** 51*/
  EloallitasHelye: 51,
  /** 55*/
  FoglalkOktHelyiseg: 55,
  /** 64*/
  Folyoso: 64,
  /** 54*/
  IntKivuliTerulet: 54,
  /** 52*/
  IrodaIrodaepulet: 52,
  /** 61*/
  KFTIMhIntKivul: 61,
  /** 60*/
  KFTIMhIntBelul: 60,
  /** 49*/
  Korhaz: 49,
  /** 46*/
  Korlet: 46,
  /** 58*/
  KvIMhIntBelul: 58,
  /** 59*/
  KvIMhIntKivul: 59,
  /** 63*/
  LatogatasiHelyiseg: 63,
  /** 47*/
  Munkahely: 47,
  /** 53*/
  Orhely: 53,
  /** 48*/
  Setaudvar: 48,
  /** 56*/
  Szallitas: 56,
  /** 50*/
  Targyalas: 50,
  /** 45*/
  Zarka: 45,
});

const ErintettsegFoka = Object.freeze({
  /** 65*/
  Elkoveto: 65,
  /** 68*/
  Eszlelo: 68,
  /** 67*/
  Segito: 67,
  /** 66*/
  Sertett: 66,
  /** 187*/
  Tanu: 187,
});

const EsemenyNapszaka = Object.freeze({
  /** 70*/
  EbresztoNyitasKozott: 70,
  /** 76*/
  Folytatolagosan: 76,
  /** 74*/
  HivataliIdoUtanZarasKozott: 74,
  /** 72*/
  HivataliIdoben: 72,
  /** 73*/
  NapkozbenMunkaszunetiNapon: 73,
  /** 77*/
  NemVoltMegallapithato: 77,
  /** 71*/
  NyitasMunkakezdesKozott: 71,
  /** 69*/
  ZarasEbresztoKozott: 69,
  /** 75*/
  ZarasTakarodoKozott: 75,
});

const FegyelmiUgyStatusza = Object.freeze({
  /** 141*/
  FenyitesKiszabva: 141,
  /** 149*/
  FenyitesVegrehajtasAlatt: 149,
  /** 156*/
  FenyitesVegrehajtasMegszakitva: 156,
  /** 150*/
  FenyitesVegrehajtva: 150,
  /** 152*/
  FogvatartottKioktatva: 152,
  /** 142*/
  IFokuTargyalas: 142,
  /** 157*/
  IIFokuTargyalas: 157,
  /** 86*/
  Kezdemenyezve: 86,
  /** 87*/
  KivizsgalasFolyamatban: 87,
  /** 177*/
  KozvetitoiEljarasAlatt: 177,
  /** 161*/
  Megszuntetve: 161,
  /** 145*/
  MegszuntetveUjEljarassal: 145,
  /** 88*/
  Megtagadva: 88,
  /** 175*/
  NemHajthatoVegre: 175,
  /** 148*/
  Osszevonva: 148,
  /** 89*/
  ReintegraciosTisztiJogkorben: 89,
  /** 174*/
  VegrehajthatosagaElevult: 174,
  /**  219*/
  IFokuFegyelmiTargyalasElokeszitese: 219,
  /** 304*/
  MaganelzarasMegszakitasanakRogzitese: 304,
  /** 343 */
  HatalyonKivulHelyezve: 343,
});

const FenyitesTipusa = Object.freeze({
  /** 94*/
  BvIntProgramokonValoReszvetelKorlatozasa: 94,
  /** 95*/
  BvIntProgramokonValoReszvetelTiltasa: 95,
  /** 90*/
  Feddes: 90,
  /** 91*/
  KietkezesCsokkentes: 91,
  /** 97*/
  KimaradasMegvonas: 97,
  /** 93*/
  MaganalTarthatoTargyakKorenekKorlatozasa: 93,
  /** 92*/
  Maganelzaras: 92,
  /** 96*/
  TobbletszolgaltatasokMegvonasa: 96,
  /** 298 */
  Megszuntetes: 298,
  /** 350 */
  HatalyonKivulHelyezes: 351,
});

const FegyelmiVetsegTipusa = Object.freeze({
  /** 115*/
  AllamiVMagantulajdonRongalasaLopasa: 115,
  /** 135*/
  BfRolKeses: 135,
  /** 136*/
  BfRolVisszateresElmulasztasa: 136,
  /** 137*/
  EgyebVetseg: 137,
  /** 134*/
  EltavozasrolVisszateresElmulasztasa: 134,
  /** 128*/
  EtkezesMegtagadas: 128,
  /** 117*/
  FajtalansagraKenyszerites: 117,
  /** 105*/
  FelugyeletMegteveszteseFelrevezetese: 105,
  /** 109*/
  Fogolyszokes: 109,
  /** 100*/
  FogolyszokesKiserleteElokeszulete: 100,
  /** 101*/
  FogolyszokeshezSegitsegNyujtasa: 101,
  /** 112*/
  Fogolyzendules: 112,
  /** 113*/
  FogvatartottTarsBantalmazasa8NaponBelulGyogyulo: 113,
  /** 114 */
  FogvatartottTarsBantalmazasa8NaponTulGyogyulo: 114,
  /** 120*/
  FogvatartottTarsKihasznalasaZsarolasa: 120,
  /** 123*/
  FogvatartottTarsSanyargatasa: 123,
  /** 121*/
  FogvatartottTarsSertegetese: 121,
  /** 122*/
  FogvatartottTarsTulajdonanakRongalasaLopasa: 122,
  /** 126*/
  GyogykezelesMegtagadasa: 126,
  /** 116*/
  KapcsolattartasSzabalyainakMegsertese: 116,
  /** 129*/
  KimaradasrolKeses: 129,
  /** 130*/
  KimaradasrolVisszateresElmulasztasa: 130,
  /** 124*/
  KozremukodesSzandekosEgeszsegkarositasban: 124,
  /** 110*/
  MeghatarozottFeladatElvegzeseAloliKibuvas: 110,
  /** 107*/
  MeghatarozottFeladatElvegzesenekMegtagadasa: 107,
  /** 108*/
  MeghatarozottFeladatHanyagVegzese: 108,
  /** 133*/
  Megszuntetve: 133,
  /** 109*/
  MunkahelyiRendMegsertese: 109,
  /** 132*/
  RovidtartamuEltavRolVisszateresElmulasztasa: 132,
  /** 131*/
  RovidtartamuEltavozasrolKeses: 131,
  /** 112*/
  SzabadlevegonTartozkodasMegzavarasa: 112,
  /** 125*/
  SzandekosEgeszsegkarositas: 125,
  /** 102*/
  SzemelyzetTagjaElleniEroszak: 102,
  /** 103*/
  SzemelyzetTagjanakMegsertese: 103,
  /** 104*/
  SzemelyzetUtasitasaVegrehajtasanakMegtagadasa: 104,
  /** 127*/
  SzeszesitalBoditoszerFogyasztasaHasznalata: 127,
  /** 113*/
  TiltottSzerencsejatek: 113,
  /** 114*/
  TiltottTargyKesziteseTartasaCsereje: 114,
  /** 106*/
  TiszteletlenMagatartas: 106,
  /** 146*/
  Verekedes: 146,
  /** 177*/
  Vesztegetes: 177,
  /** 111*/
  ZarkaKorletrendMegsertese: 111,
});

const NaploTipus = Object.freeze({
  /** 184*/
  ElrendelemAzEljarasLefolytatasat: 184,
  /** 188*/
  Feddes: 188,
  /** 186*/
  FogvatartottJogiKepviseletetKer: 186,
  /** 190*/
  Kioktatas: 190,
  /** 185*/
  ReintegraciosTisztiJogkorbeUtalom: 185,
  /** 183*/
  TanuMeghallgatas: 183,
  /** 192*/
  UgyKezdemenyezese: 192,
  /** 191*/
  UgyMegtagadasa: 191,
  /** 189*/
  Visszakuldes: 189,
  /** 202*/
  FelfuggesztesiJavaslat: 202,
  /** 203*/
  HataridoModositasiJavaslat: 203,
  /** 210*/
  EljarasAlaVontMeghallgatasa: 210,
  /** 215*/
  SzemelyiAllomanyiTanuMeghallgatas: 215,
  /** 216*/
  OsszefoglaloJelentes: 216,
  /** 217*/
  HelysziniSzemle: 217,
  /** 218*/
  SzakteruletiVelemeny: 218,
  /** 220*/
  I_fokuTargyalasKituzese: 220,
  /** 223*/
  I_fokuTargyalasiJegyzokonyv: 223,
  /** 224*/
  UgyOsszevonasa: 224,
  /** 229*/
  TagyalasElokeszitese: 229,
  /** 252*/
  MasodfokuTargyalasElokeszites: 252,
  /** 248*/
  HatarozatRogzitese: 248,
  /** 264*/
  KirendeltVedoKerese: 264,
  /** 265*/
  MeghatalmazottVedoKerese: 265,
  /** 268*/
  HataridoModositas: 268,
  /** 277*/
  Felfuggesztes: 277,
  /** 278*/
  VedoTelefonosTajekoztatasa: 278,
  /** 286*/
  MasodfokuTargyalasiJegyzokonyv: 286,
  /** 279*/
  FelfuggesztesMegszuntetese: 279,
  /** 297*/
  HatarozatRogziteseMasodFokon: 297,
  /** 300*/
  UjEljarasLefolytatasa: 300,
  /** 291*/
  JogiKepviseletVisszavonasa: 291,
  /** 292*/
  SzakteruletiVelemenyKerese: 292,
  /** 293*/
  SzembesitesiJegyzokonyv: 293,
  /** 291*/
  JogiKepviseletVisszavonasa: 291,
  /** 302 TODO: backend*/
  MaganelzarasMegkezdesenekRogzitese: 302,
  /** 306*/
  FenyitesVegrehajthatatlannaTetele: 306,
  /** 305*/
  MaganelzarasVegrehajtva: 305,
  /** 1007 TODO: backend*/
  //FenyitesVegrehajthatatlannaTetele: 1007,
  /** 313 TODO: backend*/
  KozvetitoiEljarasHataridoModositasKerese: 313,
  /** 304 TODO: backend*/
  MaganelzarasMegszakitasa: 304,
  /** 301*/
  KozvetitoiEljarasKezdemenyezese: 301,
  /** 307 TODO: backend*/
  KozvetitoiEljarasElrendeles: 307,
  /** 1012 TODO: backend*/
  KozvetitoiEljarasMegtagadas: 1012,
  /** 1013 TODO: backend*/
  KozvetitoiEljarasFeljegyzes: 1013,

  KozvetitoiEljarasMegallapodas: 309,
  /** 1015 TODO: backend*/
  KozvetitoiEljarasMegallapodasTeljesult: 308,
  /** 1016 TODO: backend*/
  KozvetitoiEljarasMegallapodasNemTeljesult: 1016,
  /** 314*/
  KozvetitoiEljarasLezaras: 314,
  /** 311 TODO: backend*/
  KozvetitoiEljarasIndoklassalMegszuntetes: 311,
  /** 316 */
  FegyelmiElkulonitesElrendelese: 316,
  /** 1022 TODO: backend*/
  FegyelmiElkulonitesVegrehajtva: 1022,
  /** 1023 TODO: backend*/
  HataridoTullepesMiattiMegszuntetesNaplobejegyzes: 1023,
  /** 325*/
  BuntetoFeljelentesRogziteseNaploBejegyzes: 325,
  /** 313 */
  KozvetitoiEljarasHataridoModositasKereseNaplobejegyzes: 313,
  /** 312 */
  KozvetitoiEljarasHataridoModositasNaplobejegyzes: 312,
  /** 327 */
  MaganelzarasElrendelese: 327,
  FenyitesVegrehajtasaKesz: 328,
  VegrehajthatosagElevultNaplobejegyzes: 329,
  AutomatikusLezaras: 332,
  AutomatikusVegrehajtasMegkezdese: 336,
  AutomatikusVegrehajtasBefejezese: 337,
  KivizsgaloModositasa: 338,
  SzabadszovegesNaplobejegyzes: 339,
  MaganelzarasIdeiglenesenEllenjavallt: 344,
});

const VisszakuldesOka = Object.freeze({
  /** 228*/
  EgyebOkMiatt: 228,
  /** 226*/
  FogvatartottNemIsmeriEl: 226,
  /** 227*/
  SulyosabbAzUgy: 227,
  /** 269*/
  JogiKepviseletetKer: 269,
});

const IktatottDokumentumTipus = Object.freeze({
  /** 211*/
  EljarasAlaVontMeghallgatasa: 211,
  /** 204*/
  ElkulonitesiLap: 204,
  /** 201*/
  ErtesitoLap: 201,
  /** 249*/
  FegyelmiHatarozat: 249,
  /** 250*/
  FegyelmiHatarozatMegszuntetese: 250,
  /** 207*/
  FegyelmiLap: 207,
  /** 253*/
  ReintegraciosTisztiHatarozat: 253,
  /** 214*/
  SzemelyiAllomanyiTanuMeghallgatasa: 214,
  /** 209*/
  TajekoztatasKerNyomtatvanyt: 209,
  /** 208*/
  TajekoztatasNemKerNyomtatvanyt: 208,
  /** 212*/
  TanuMeghallgatasa: 212,
  /** 225*/
  TargyalasiJegyzokonyv: 225,
  /** 280*/
  ReintegraciosTisztiKioktatas: 280,
  /** 281*/
  KirendeltVedoKereseNyilatkozat: 281,
  /** 282*/
  KirendeltVedoKerese: 282,
  /** 283*/
  MeghatalmazottVedoKereseNyilatkozat: 283,
  /** 284*/
  MeghatalmazottVedoKerese: 284,
  /** 285*/
  VedoTelefonosTajekoztatasa: 285,
  /** 287*/
  MasodfokuTargyalasiJegyzokonyv: 287,
  /** 294*/
  MasodfokuHatarozat: 294,
  /** 295*/
  MasodfokuHatarozatMegszuntetese: 295,
  /** 294*/
  SzembesitesiJegyzokonyv: 294,
  /** 296*/
  SzembesitesiJegyzokonyvNyomtatvany: 296,
  /** 303*/
  MaganelzarasMegkezdesenekRogziteseNyomtatvany: 303,
  /** 326*/
  KozvetitoiEljarasKezdemenyezese: 326,
  /** 330*/
  AlairasMegtagadasa: 330,
  /** 331*/
  OsszefoglaloJelentes: 331,
  /** 340*/
  OsszefoglaloJelentesDocx: 340,
  /** 345*/
  KarjelentoLap: 345,
  /** 347*/
  BvBankKarjelentoLap: 347,
  /** 348*/
  BuntetoFeljelentes: 348,
});
const HatarozatHozoJogkore = Object.freeze({
  /** 221*/
  FegyelmiJogkorGyakorloja: 221,
  /** 222*/
  Parancsnok: 222,
  /** 251*/
  BVBiro: 251,
});

const FelfuggesztesOkai = Object.freeze({
  /** 270*/
  KozvetitoiEljarasraUtalas: 270,
  /** 271*/
  FogvatartottVedoKirendelesetKeri: 271,
  /** 272*/
  AFogvatartottEgeszsegiAllapotaTavolleteEseten: 272,
  /** 273*/
  SzakertoiVizsgalatSzukseges: 273,
  /** 274*/
  JogellenesTavollet: 274,
  /** 275*/
  MasikIntezetMegkereseseOkan: 275,
  /** 276*/
  BirosagVagyMasHatosagDonteseig: 276,
});

const HatalyonKivulHelyezesTipusai = Object.freeze({
  /** 349 */
  UjEljarasInditasa: 349,
  /** 350 */
  NemAkarokUjEljarast: 350,
});

const MennyisegiEgyseg = Object.freeze({
  /** 288*/
  Alkalom: 288,
  /** 289*/
  Honap: 289,
  /** 290*/
  Nap: 290,
});

const JutalmazasNaploTipus = Object.freeze({
  Eloterjesztes: 42151,
  Javaslat: 42155,
  Dontes: 42156,
  Velemeny: 42157,
  Osszevonas: 42165,
  VegrehajtasAlatt: 42417,
  Vegrehajtva: 42419,
  AutomatikusLezaras: 42415,
  Kihirdetve: 42174,
});

const JutalomTipusok = Object.freeze({
  KondiDijmHaszn: 4,
  SzemSzuksFordOsszegNoveles: 5,
  FenyitesTorles: 8,
  Elutasitas: 12,
});
const Szakterulet = Object.freeze({
  ReintegraciosTiszt: 20019,
  Osztalyvezeto: 20020,
  Parancsnok: 20021,
});
const JutalomStatusz = Object.freeze({
  Osszevonva: 42164,
  ReintegaciosTisztnekElkuldve: 42175,
  Engedelyezve: 42176,
  OsztalyvezetonekElkuldve: 42177,
  ParancsnoknakElkuldve: 42178,
  Elutasitva: 42179,
  Kihirdetve: 42180,
});

const Cimkek = {
  JutalmazasStatusz,
  BVNKivul,
  IntezetiEgyeb,
  IntezetiFix,
  JutalmazasJogkor,
  ElkovetesEszkoze,
  EsemenyJellege,
  EsemenyModja,
  EsemenyHelyszine,
  ErintettsegFoka,
  EsemenyNapszaka,
  FegyelmiUgyStatusza,
  FenyitesTipusa,
  FegyelmiVetsegTipusa,
  NaploTipus,
  VisszakuldesOka,
  IktatottDokumentumTipus,
  FelfuggesztesOkai,
  HatarozatHozoJogkore,
  HatalyonKivulHelyezesTipusai,
  MennyisegiEgyseg,
  JutalmazasNaploTipus,
  JutalomTipusok,
  Szakterulet,
  JutalomStatusz,
};

export default Cimkek;
