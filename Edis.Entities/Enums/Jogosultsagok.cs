using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Entities.Enums
{
    public enum Jogosultsagok
    {
        #region AlapNyilvantarto

        #region általános jogosultságok
        /// <summary>Index</summary>        
        [Description("Index")]
        ARIndex,

        #endregion általános jogosultságok

        #region funkció jogosultságok

        #region Bűnügyi nyilvántartás

        #region Befogadások

        #region Befogadás
        /// <summary>Befogadás</summary>
        [Description("Befogadás")]
        AFBeFe,

        /// <summary>Ideiglenes befogadás - Bűnügyi nyilvántartó</summary>
        [Description("Ideiglenes befogadás - Bűnügyi nyilvántartó")]
        AFBeSzVeL1,

        /// <summary>Ideiglenes befogadás - Biztonság</summary>
        [Description("Ideiglenes befogadás - Biztonság")]
        AFBeSzVeL2,

        /// <summary>Befogadásra váró személyek</summary>
        [Description("Befogadásra váró személyek")]
        AFBeSzVeL3,

        /// <summary>Befogadás - Fogvatartott részletes adatai</summary>
        [Description("Befogadás - Fogvatartott részletes adatai")]
        AFBeFoAdRe,

        #endregion Befogadás

        #region Átfogadás ideiglenesből
        /// <summary>Átfogadás Ideiglenesből - Fogvatartott Átfogadás</summary>
        [Description("Átfogadás Ideiglenesből - Fogvatartott Átfogadás")]
        AFAtIdFoAt,

        /// <summary>Átfogadás Ideiglenesből - Fogvatartott Lista</summary>
        [Description("Átfogadás Ideiglenesből - Fogvatartott Lista")]
        AFAtIdFoLi,

        /// <summary>Átfogadás Ideiglenesből - Fogvatartott részletes adatai</summary>
        [Description("Átfogadás Ideiglenesből - Fogvatartott részletes adatai")]
        AFAtIdFoRe,
        #endregion  Átfogadás ideiglenesből

        #region Átfogadás elzárásból előzetesbe/elítéltnek

        [Description("Átfogadás elzárásból előzetesbe/elítéltnek - Fogvatartott lista")]
        AFAtfElzEloFtLi,

        [Description("Átfogadás elzárásból előzetesbe/elítéltnek - Felvitel")]
        AFAtfElzEloFe,

        [Description("Átfogadás elzárásból előzetesbe/elítéltnek - Fogvatartott részletek")]
        AFAtfElzEloFtRe,

        #endregion Átfogadás elzárásból előzetesbe/elítéltnek

        #region Átfogadás előzetesből/szabadságvesztésből elzárásba

        [Description("Átfogadás előzetesből/szabadságvesztésből elzárásba - Fogvatartott lista")]
        AFAtfEloElzFtLi,

        [Description("Átfogadás előzetesből/szabadságvesztésből elzárásba - Felvitel")]
        AFAtfEloElzFe,

        [Description("Átfogadás előzetesből/szabadságvesztésből elzárásba - Fogvatartott részletek")]
        AFAtfEloElzFtRe,

        #endregion Átfogadás előzetesből/szabadságvesztésből elzárásba

        #region Átfogadás megőrzésből

        [Description("Átfogadás megőrzésből - Fogvatartott lista")]
        AFAtfMegOrzFtLi,

        [Description("Átfogadás megőrzésből - Felvitel")]
        AFAtfMegOrzFe,

        [Description("Átfogadás megőrzésből - Fogvatartott részletek")]
        AFAtfMegOrzFtRe,

        #endregion Átfogadás megőrzésből

        #region Ideiglenes befogadás távollétből

        [Description("Ideiglenes befogadás távollétből - Fogvatartott lista")]
        AFBefIdeTavFtLi,

        [Description("Ideiglenes befogadás távollétből - Felvitel")]
        AFBefIdeTavFe,

        [Description("Ideiglenes befogadás távollétből - Fogvatartott részletek")]
        AFBefIdeTavFtRe,

        #endregion Ideiglenes befogadás távollétből

        #endregion Befogadások

        #region Irategyesítés

        /// <summary>
        /// Irategyesítés - Fogvatartott személy lista
        /// </summary>
        [Description("Irategyesítés - Fogvatartott személy lista")]
        AFIratFoSzLi,

        /// <summary>
        /// Irategyesítés - Fogvatartott lista
        /// </summary>
        [Description("Irategyesítés - Fogvatartott lista")]
        AFIratFtLi,

        /// <summary>
        /// Irategyesítés - Intézet saját fogvatartott lista
        /// </summary>
        [Description("Irategyesítés - Intézet saját fogvatartott lista")]
        AFIratSaFtLi,

        /// <summary>
        /// Irategyesítés - Intézet saját fogvatartott lista
        /// </summary>
        [Description("Irategyesítés - Előzetes letartoztatás lista")]
        AFIratElLeLi,

        /// <summary>
        /// Irategyesítés - Fogvatartott lista
        /// </summary>
        [Description("Irategyesítés - Ítélet lista")]
        AFIratItLi,

        /// <summary>
        /// Irategyesítés - Befogadás új azonosítóval
        /// </summary>
        [Description("Irategyesítés - Befogadás új azonosítóval")]
        AFIratBeUjAz,

        /// <summary>
        /// Irategyesítés - Iratesgyesítés régi azonosítón
        /// </summary>
        [Description("Irategyesítés - Iratesgyesítés régi azonosítón")]
        AFIratRegiAz,

        /// <summary>
        /// Irategyesítés - Irategyesítés az eddigi saját azonosítón
        /// </summary>
        [Description("Irategyesítés - Irategyesítés az eddigi saját azonosítón")]
        AFIratSajaAz,

        /// <summary>
        /// Irategyesítés - Fogvatartott részletes adatai
        /// </summary>
        [Description("Irategyesítés - Fogvatartott részletes adatai")]
        AFIratFtRe,

        #endregion Irategyesítés

        #region Szv. Jegyzék
        /// <summary>Szabadságvesztési jegyzék felvitele</summary>
        [Description("Szabadságvesztési jegyzék felvitele")]
        AFSzJeFe,

        /// <summary>Szabadságvesztési jegyzék lista</summary>
        [Description("Szabadságvesztési jegyzék lista")]
        AFSzJeLi,

        /// <summary>Szabadságvesztési jegyzék módosítása</summary>
        [Description("Szabadságvesztési jegyzék módosítása")]
        AFSzJeMo,

        /// <summary>Szabadságvesztési jegyzék törlése</summary>
        [Description("Szabadságvesztési jegyzék törlése")]
        AFSzJeT,

        /// <summary>Szabadságvesztési jegyzék részletes adatai</summary>
        [Description("Szabadságvesztési jegyzék részletes adatai")]
        AFSzJeRe,

        /// <summary>Jegyzéken szereplő személy felvitel</summary>
        [Description("Jegyzéken szereplő személy felvitel")]
        AFSzJeSzFe,

        /// <summary>Jegyzéken szereplő személyek</summary>
        [Description("Jegyzéken szereplő személyek")]
        AFSzJeSzLi,

        /// <summary>Szabadságvesztési jegyzéken szereplő személy karbantartása</summary>
        [Description("Szabadságvesztési jegyzéken szereplő személy karbantartása")]
        AFSzJeSLBV,

        /// <summary>Jegyzéken szereplő személy módosítása</summary>
        [Description("Jegyzéken szereplő személy módosítása")]
        AFSzJeSzMo,

        /// <summary>Jegyzéken szereplő személy törlése</summary>
        [Description("Jegyzéken szereplő személy törlése")]
        AFSzJeSzT,

        /// <summary>Jegyzéken szereplő személy részletes adatai</summary>
        [Description("Jegyzéken szereplő személy részletes adatai")]
        AFSzJeSzRe,
        #endregion Szv. Jegyzék

        #region Távozások előkészítése

        #region Szabadítás előjegyzése

        #region Ideiglenesen befogadott
        /// <summary>Ideiglenesen befogadott szabadításának előjegyzése - Fogvatartott lista</summary>
        [Description("Ideiglenesen befogadott szabadításának előjegyzése - Fogvatartott lista")]
        AFSzabEiFtLi,

        /// <summary>Ideiglenesen befogadott szabadításának előjegyzése - Részletek</summary>
        [Description("Ideiglenesen befogadott szabadításának előjegyzése - Részletek")]
        AFSzabEiRe,

        /// <summary>Ideiglenesen befogadott szabadításának előjegyzése - Felvitel</summary>
        [Description("Ideiglenesen befogadott szabadításának előjegyzése - Felvitel")]
        AFSzabEiFe,

        /// <summary>Ideiglenesen befogadott szabadításának előjegyzése - Módositas</summary>
        [Description("Ideiglenesen befogadott szabadításának előjegyzése - Módositas")]
        AFSzabEiM,
        #endregion Ideiglenesen befogadott

        #region Előzetes letartóztatott
        /// <summary>Előzetes letartóztatott szabadításának előjegyzése - Fogvatartott lista</summary>
        [Description("Előzetes letartóztatott szabadításának előjegyzése - Fogvatartott lista")]
        AFSzabEEloFtLi,

        /// <summary>Előzetes letartóztatott szabadításának előjegyzése - Részletek</summary>
        [Description("Előzetes letartóztatott szabadításának előjegyzése - Részletek")]
        AFSzabEEloRe,

        /// <summary>Előzetes letartóztatott szabadításának előjegyzése - Felvitel</summary>
        [Description("Előzetes letartóztatott szabadításának előjegyzése - Felvitel")]
        AFSzabEEloFe,

        /// <summary>Előzetes letartóztatott szabadításának előjegyzése - Módositas</summary>
        [Description("Előzetes letartóztatott szabadításának előjegyzése - Módositas")]
        AFSzabEEloM,
        #endregion Előzetes letartóztatott

        #region Elítélt
        /// <summary>Elítélt szabadításának előjegyzése - Fogvatartott lista</summary>
        [Description("Elítélt szabadításának előjegyzése - Fogvatartott lista")]
        AFSzabEEliFtLi,

        /// <summary>Elítélt szabadításának előjegyzése - Részletek</summary>
        [Description("Elítélt szabadításának előjegyzése - Részletek")]
        AFSzabEEliRe,

        /// <summary>Elítélt szabadításának előjegyzése - Felvitel</summary>
        [Description("Elítélt szabadításának előjegyzése - Felvitel")]
        AFSzabEEliFe,

        /// <summary>Elítélt szabadításának előjegyzése - Módositas</summary>
        [Description("Elítélt szabadításának előjegyzése - Módositas")]
        AFSzabEEliM,
        #endregion Elítélt

        #region Elzárásos
        /// <summary>Elzárásos fogvatartott szabadításának előjegyzése - Fogvatartott lista</summary>
        [Description("Elzárásos fogvatartott szabadításának előjegyzése - Fogvatartott lista")]
        AFSzabEElzFtLi,

        /// <summary>Elzárásos fogvatartott szabadításának előjegyzése - Részletek</summary>
        [Description("Elzárásos fogvatartott szabadításának előjegyzése - Részletek")]
        AFSzabEElzRe,

        /// <summary>Elzárásos fogvatartott szabadításának előjegyzése - Felvitel</summary>
        [Description("Elzárásos fogvatartott szabadításának előjegyzése - Felvitel")]
        AFSzabEElzFe,

        /// <summary>Elzárásos fogvatartott szabadításának előjegyzése - Módositas</summary>
        [Description("Elzárásos fogvatartott szabadításának előjegyzése - Módositas")]
        AFSzabEElzM,
        #endregion Elzárásos
        #endregion Szabadítás előjegyzése

        #region Idézések
        /// <summary>Idézés - Fogvatartott lista</summary>
        [Description("Idézés - Fogvatartott lista")]
        AFIdKaFtLi,

        /// <summary>Idézés - Fogvatartott idézés lista</summary>
        [Description("Idézés - Fogvatartott idézés lista")]
        AFIdKaIdLi,

        /// <summary>Idézés részletek</summary>
        [Description("Idézés részletek")]
        AFIdKaRe,

        /// <summary>Idézés felvitel</summary>
        [Description("Idézés felvitel")]
        AFIdKaFe,

        /// <summary>Idézés módosítás</summary>
        [Description("Idézés módosítás")]
        AFIdKaMo,

        /// <summary>Idézés törlés</summary>
        [Description("Idézés törlés")]
        AFIdKaTo,

        /// <summary>Idézés nyomtatás</summary>
        [Description("Idézés nyomtatás")]
        AFIdKaNyo,
        #endregion Idézések

        #region Büntetés-félbeszakítás előjegyzés
        /// <summary>Büntetés-félbeszakítás előjegyzés - Fogvatartott lista</summary>
        [Description("Büntetés-félbeszakítás előjegyzés - Fogvatartott lista")]
        AFBuFelFoLi,
        /// <summary>Büntetés-félbeszakítás előjegyzés - Lista</summary>
        [Description("Büntetés-félbeszakítás előjegyzés - Lista")]
        AFBuFelLi,
        /// <summary>Büntetés-félbeszakítás előjegyzés - Részletek</summary>
        [Description("Büntetés-félbeszakítás előjegyzés - Részletek")]
        AFBuFelRe,
        /// <summary>Büntetés-félbeszakítás előjegyzés - Felvitel</summary>
        [Description("Büntetés-félbeszakítás előjegyzés - Felvitel")]
        AFBuFelFe,
        /// <summary>Büntetés-félbeszakítás előjegyzés - Módositas</summary>
        [Description("Büntetés-félbeszakítás előjegyzés - Módositas")]
        AFBuFelM,
        /// <summary>Büntetés-félbeszakítás előjegyzés - Törlés</summary>
        [Description("Büntetés-félbeszakítás előjegyzés - Törlés")]
        AFBuFelT,
        #endregion Büntetés-félbeszakítás előjegyzés

        #region Büntetés-félbeszakítás hosszabbítás
        /// <summary>Büntetés-félbeszakítás hosszabbítás - Fogvatartott lista</summary>
        [Description("Büntetés-félbeszakítás hosszabbítás - Fogvatartott lista")]
        AFBuFelHosszFoLi,
        /// <summary>Büntetés-félbeszakítás hosszabbítás - Módosítás</summary>
        [Description("Büntetés-félbeszakítás hosszabbítás - Módosítás")]
        AFBuFelHosszM,
        #endregion

        #region Eltávozás előjegyzés
        /// <summary>Eltávozás előjegyzés - Fogvatartott lista</summary>
        [Description("Eltávozás előjegyzés - Fogvatartott lista")]
        AFEltEloFtLi,

        /// <summary>Eltávozás előjegyzés - Kiválasztott kapcsolattartó tételei</summary>
        [Description("Eltávozás előjegyzés - Kiválasztott fogvatartott tételei")]
        AFEltEloEltLi,

        /// <summary>Eltávozás előjegyzés - Kiválasztott tétel részletei</summary>
        [Description("Eltávozás előjegyzés- Kiválasztott tétel részletei")]
        AFEltEloRe,

        /// <summary>Eltávozás előjegyzés - Felvitel</summary>
        [Description("Eltávozás előjegyzés - Felvitel")]
        AFEltEloFe,

        /// <summary>Eltávozás előjegyzés - Módosítás</summary>
        [Description("Eltávozás előjegyzés - Módosítás")]
        AFEltEloMo,
        #endregion Eltávozás előjegyzés

        #region Szabadságvesztés végrehajtásának átengedése kérelemre

        /// <summary>
        /// Szabadságvesztés végrehajtásának átengedése kérelemre - Fogvatartott lista
        /// </summary>
        [Description("Szabadságvesztés végrehajtásának átengedése kérelemre - Fogvatartott lista")]
        AFSzvVhAtKeFtLi,

        /// <summary>
        /// Szabadságvesztés végrehajtásának átengedése kérelemre - Kérelem részletek
        /// </summary>
        [Description("Szabadságvesztés végrehajtásának átengedése kérelemre - Kérelem részletek")]
        AFSzvVhAtKeRe,

        /// <summary>
        /// Szabadságvesztés végrehajtásának átengedése kérelemre - Felvitel
        /// </summary>
        [Description("Szabadságvesztés végrehajtásának átengedése kérelemre - Felvitel")]
        AFSzvVhAtKeFe,

        /// <summary>
        /// Szabadságvesztés végrehajtásának átengedése kérelemre - Módosítás
        /// </summary>
        [Description("Szabadságvesztés végrehajtásának átengedése kérelemre - Módosítás")]
        AFSzvVhAtKeMo,

        /// <summary>
        /// Szabadságvesztés végrehajtásának átengedése kérelemre - Törlés
        /// </summary>
        [Description("Szabadságvesztés végrehajtásának átengedése kérelemre - Törlés")]
        AFSzvVhAtKeTo,

        #endregion  Szabadságvesztés végrehajtásának átengedése kérelemre

        #endregion Távozások előkészítése

        #region Fogvatartott adatainak karbantartása

        #region Fogv. személyi adatai
        /// <summary>Fogvatartott adat karbantartás lista</summary>
        [Description("Fogvatartott adat karbantartás lista")]
        AFFoAdKaLi,

        /// <summary>Fogvatartott adat módosítása</summary>
        [Description("Fogvatartott adat módosítása")]
        AFFoAdKaMo,


        /// <summary>Fogvatartott részletes adatai</summary>
        [Description("Fogvatartott részletes adatai")]
        AFFoAdKaRe,
        #endregion Fogv. személyi adatai

        #region Fényképezés előjegyzés
        [Description("Fényképezés előjegyzés - Fogvatartott lista")]
        AFFenykepKaFtLi,

        [Description("Fényképezés előjegyzés - Fogvatartott fényképezés előjegyzései")]
        AFFenykepKaFeLi,

        [Description("Fényképezés előjegyzés - Felvitel")]
        AFFenykepKaFe,

        [Description("Fényképezés előjegyzés - Módosítás")]
        AFFenykepKaMo,

        [Description("Fényképezés előjegyzés - Törlés")]
        AFFenykepKaTo,

        [Description("Fényképezés előjegyzés - Részletek")]
        AFFenykepKaRe,
        #endregion Fényképezés előjegyzés

        #region Gondnokság alá helyezés
        [Description("Gondnokság alá helyezés - Fogvatartott lista")]
        AFGondAlaHelyKaFtLi,

        [Description("Gondnokság alá helyezés - Fogvatartott gondnokság alá helyezései")]
        AFGondAlaHelyKaGoLi,

        [Description("Gondnokság alá helyezés - Felvitel")]
        AFGondAlaHelyKaFe,

        [Description("Gondnokság alá helyezés - Módosítás")]
        AFGondAlaHelyKaMo,

        [Description("Gondnokság alá helyezés - Törlés")]
        AFGondAlaHelyKaTo,

        [Description("Gondnokság alá helyezés - Részletek")]
        AFGondAlaHelyKaRe,

        #endregion Gondnokság alá helyezés

        #region Bűntársi csoport karbantartás
        /// <summary>
        /// Bűntársi csoport adatok karbantartása - Bűntársi csoport felvitel
        /// </summary>
        [Description("Bűntársi csoport adatok karbantartása - Bűntársi csoport felvitel")]
        AFBCsF,

        /// <summary>
        /// Bűntársi csoport adatok karbantartása - Bűntársi csoport lista
        /// </summary>
        [Description("Bűntársi csoport adatok karbantartása - Bűntársi csoport lista")]
        AFBCsL,

        /// <summary>
        /// Bűntársi csoport adatok karbantartása - Bűntársi csoport részletek
        /// </summary>
        [Description("Bűntársi csoport adatok karbantartása - Bűntársi csoport részletek")]
        AFBCsR,

        /// <summary>
        /// Bűntársi csoport adatok karbantartása - Bűntársi csoport módosítás
        /// </summary>
        [Description("Bűntársi csoport adatok karbantartása - Bűntársi csoport módosítás")]
        AFBCsM,

        /// <summary>
        /// Bűntársi csoport adatok karbantartása - Bűntársi csoport tag felvitel másik intézetből
        /// </summary>
        [Description("Bűntársi csoport adatok karbantartása - Bűntársi csoport tag felvitel másik intézetből")]
        AFBCsTFMI,

        /// <summary>
        /// Bűntársi csoport adatok karbantartása - Bűntársi csoport tag felvitel saját intézetből
        /// </summary>
        [Description("Bűntársi csoport adatok karbantartása - Bűntársi csoport tag felvitel saját intézetből")]
        AFBCsTFSI,

        /// <summary>
        /// Bűntársi csoport adatok karbantartása - Bűntársi csoport tag lista
        /// </summary>
        [Description("Bűntársi csoport adatok karbantartása - Bűntársi csoport tag lista")]
        AFBCsTL,
        #endregion Bűntársi csoport karbantartás

        #region Általános korlátozások
        /// <summary>Általános korlátozások - Fogvatartott lista</summary>
        [Description("Általános korlátozások - Fogvatartott lista")]
        AFAltKorlFL,

        /// <summary>Általános korlátozások - Kiválasztott fogvatartott tételei</summary>
        [Description("Általános korlátozások - Kiválasztott fogvatartott tételei")]
        AFAltKorlKL,

        /// <summary>Általános korlátozások - Kiválasztott tétel részletei</summary>
        [Description("Általános korlátozások - Kiválasztott tétel részletei")]
        AFAltKorlR,

        /// <summary>Általános korlátozások - Felvitel</summary>
        [Description("Általános korlátozások - Felvitel")]
        AFAltKorlF,

        /// <summary>Általános korlátozások - Módosítás</summary>
        [Description("Általános korlátozások - Módosítás")]
        AFAltKorlM,
        #endregion Általános korlátozások

        #region Büntetés-végrehajtási ügy

        #region Büntetés-végrehajtási ügy indítása
        /// <summary>Büntetés végrehajtási ügy indítás - Fogvatartott lista</summary>
        [Description("Büntetés végrehajtási ügy indítás - Fogvatartott lista")]
        AFBVUgyIndFL,

        /// <summary>Büntetés végrehajtási ügy indítás - Kiválasztott fogvatartott tételei</summary>
        [Description("Büntetés végrehajtási ügy indítás - Kiválasztott fogvatartott tételei")]
        AFBVUgyIndBVUL,

        /// <summary>Büntetés végrehajtási ügy indítás - Kiválasztott tétel részletei</summary>
        [Description("Büntetés végrehajtási ügy indítás - Kiválasztott tétel részletei")]
        AFBVUgyIndBVUR,

        /// <summary>Büntetés végrehajtási ügy indítás - Felvitel</summary>
        [Description("Büntetés végrehajtási ügy indítás - Felvitel")]
        AFBVUgyIndBVUF,

        /// <summary>Büntetés végrehajtási ügy indítás - Módosítás</summary>
        [Description("Büntetés végrehajtási ügy indítás - Módosítás")]
        AFBVUgyIndBVUM,
        #endregion Büntetés-végrehajtási ügy indítása

        #region Büntetés-végrehajtási ügy előterjesztése
        /// <summary>Büntetés végrehajtási ügy előterjesztése - Ügy lista</summary>
        [Description("Büntetés végrehajtási ügy előterjesztése - Ügy lista")]
        AFBVUgyEloBVUL,

        /// <summary>Büntetés végrehajtási ügy előterjesztése - Módosítás</summary>
        [Description("Büntetés végrehajtási ügy előterjesztése - Módosítás")]
        AFBVUgyEloBVUM,
        #endregion Büntetés-végrehajtási ügy előterjesztése

        #region Büntetés-végrehajtási ügy lezárása
        /// <summary>Büntetés végrehajtási ügy lezárás - Ügy lista</summary>
        [Description("Büntetés végrehajtási ügy lezárás - Ügy lista")]
        AFBVUgyLezBVUL,

        /// <summary>Büntetés végrehajtási ügy lezárás - Kiválasztott tétel részletei</summary>
        [Description("Büntetés végrehajtási ügy lezárás - Kiválasztott tétel részletei")]
        AFBVUgyLezBVUR,

        /// <summary>Büntetés végrehajtási ügy lezárás - Módosítás</summary>
        [Description("Büntetés végrehajtási ügy lezárás - Módosítás")]
        AFBVUgyModBVUM,
        #endregion Büntetés-végrehajtási ügy lezárása

        #region Büntetés-végrehajtási ügy visszavonása
        /// <summary>Büntetés végrehajtási ügy visszavonása - Fogvatartott lista</summary>
        [Description("Büntetés végrehajtási ügy visszavonása - Fogvatartott lista")]
        AFBVUgyViszFL,

        /// <summary>Büntetés végrehajtási ügy visszavonása - Kiválasztott fogvatartott tételei</summary>
        [Description("Büntetés végrehajtási ügy visszavonása - Kiválasztott fogvatartott tételei")]
        AFBVUgyViszBVUL,

        /// <summary>Büntetés végrehajtási ügy visszavonása - Módosítás</summary>
        [Description("Büntetés végrehajtási ügy visszavonása - Módosítás")]
        AFBVUgyViszBVUM,
        #endregion Büntetés-végrehajtási ügy visszavonása

        #region EVSZ parancsnoki felfüggesztése

        /// <summary>
        /// EVSZ parancsnoki felfüggesztése - fogvatartott lista
        /// </summary>
        [Description("EVSZ parancsnoki felfüggesztése - fogvatartott lista")]
        AFEvszpfFl,

        /// <summary>
        /// EVSZ parancsnoki felfüggesztése - felfüggesztés
        /// </summary>
        [Description("EVSZ parancsnoki felfüggesztése - felfüggesztés")]
        AFEvszpfF,

        #endregion // EVSZ parancsnoki felfüggesztése

        #region Téves EVSZ felfüggesztés javítása

        /// <summary>
        /// Téves EVSZ felfüggesztés javítása - fogvatartott lista
        /// </summary>
        [Description("Téves EVSZ felfüggesztés javítása - fogvatartott lista")]
        AFTevszfjFl,

        /// <summary>
        /// Téves EVSZ felfüggesztés javítása - felfüggesztés megszüntetése
        /// </summary>
        [Description("Téves EVSZ felfüggesztés javítása - felfüggesztés megszüntetése")]
        AFTevszfjFm,

        #endregion  Téves EVSZ felfüggesztés javítása

        #region Szabadlábon intézett bv. ügyhöz "befogadás"

        /// <summary>
        /// Szabadlábon intézett bv. ügyhöz "befogadás" - fogvatartott lista
        /// </summary>
        [Description("Szabadlábon intézett bv. ügyhöz \"befogadás\" - fogvatartott lista")]
        AFSzibubFl,

        #endregion  Szabadlábon intézett bv. ügyhöz "befogadás"

        #region Szabadlábon intézett bv. ügy

        /// <summary>
        /// Szabadlábon intézett bv. ügy - fogvatartott lista
        /// </summary>
        [Description("Szabadlábon intézett bv. ügy - fogvatartott lista")]
        AFSzibuFl,

        /// <summary>
        /// Szabadlábon intézett bv. ügy - számított szabadulás rögzítése
        /// </summary>
        [Description("Szabadlábon intézett bv. ügy - számított szabadulás rögzítése")]
        AFSzibuSzszr,

        /// <summary>
        /// Szabadlábon intézett bv. ügy - szabadlábon bv. ügy lezárása
        /// </summary>
        [Description("Szabadlábon intézett bv. ügy - szabadlábon bv. ügy lezárása")]
        AFSzibuSzbuf,

        /// <summary>
        /// Szabadlábon intézett bv. ügy - szabadlábon bv. ügy visszavonása
        /// </summary>
        [Description("Szabadlábon intézett bv. ügy - szabadlábon bv. ügy visszavonása")]
        AFSzibuSzbuv,

        #endregion Szabadlábon intézett bv. ügy

        #endregion Büntetés-végrehajtási ügy

        #region Fogvatartott további nevei

        /// <summary>Fogvatartott további nevei - Fogvatartott lista</summary>
        [Description("Fogvatartott további nevei - Fogvatartott lista")]
        AFTovabbiNevFL,

        /// <summary>Fogvatartott további nevei - Kiválasztott fogvatartott tételei</summary>
        [Description("Fogvatartott további nevei - Kiválasztott fogvatartott tételei")]
        AFTovabbiNevNL,

        /// <summary>Fogvatartott további nevei - Kiválasztott tétel részletei</summary>
        [Description("Fogvatartott további nevei - Kiválasztott tétel részletei")]
        AFTovabbiNevR,

        /// <summary>Fogvatartott további nevei - Felvitel</summary>
        [Description("Fogvatartott további nevei - Felvitel")]
        AFTovabbiNevF,

        /// <summary>Fogvatartott további nevei - Módosítás</summary>
        [Description("Fogvatartott további nevei - Módosítás")]
        AFTovabbiNevM,

        /// <summary>Fogvatartott további nevei - Törlés</summary>
        [Description("Fogvatartott további nevei - Törlés")]
        AFTovabbiNevT,
        #endregion Fogvatartott további nevei

        #region Előzetes letartóztatás karbantartása

        /// <summary>Előzetes letartóztatás - Fogvatartott Lista</summary>
        [Description("Előzetes letartóztatás - Fogvatartott Lista")]
        AFElLetFoLi,

        /// <summary>Előzetes letartóztatás - Lista</summary>
        [Description("Előzetes letartóztatás - Lista")]
        AFElLetLi,
        /// <summary>Előzetes letartóztatás - Részletek</summary>
        [Description("Előzetes letartóztatás - Részletek")]
        AFElLetRe,

        /// <summary>Előzetes letartóztatás - Felvitel</summary>
        [Description("Előzetes letartóztatás - Felvitel")]
        AFElLetFe,

        /// <summary>Előzetes letartóztatás - Módosítás</summary>
        [Description("Előzetes letartóztatás - Módosítás")]
        AFElLetMo,

        /// <summary>Előzetes letartóztatás - Törlés</summary>
        [Description("Előzetes letartóztatás - Törlés")]
        AElLetT,

        #endregion Előzetes letartóztatás karbantartása

        #region Ítélet
        /// <summary>Itélet adatok karbantartása - Fogvatartott lista</summary>
        [Description("Ítélet adatok karbantartása - Fogvatartott lista")]
        AFItKaFtLi,

        /// <summary>Itélet adatok karbantartása - Ítélet lista</summary>
        [Description("Ítélet adatok karbantartása - Ítélet lista")]
        AFItKaItLi,

        /// <summary>Itélet adatok karbantartása - Ítélet részletek</summary>
        [Description("Ítélet adatok karbantartása - Ítélet részletek")]
        AFItKaRe,

        /// <summary>Itélet adatok karbantartása - Ítélet felvitel</summary>
        [Description("Ítélet adatok karbantartása - Ítélet felvitel")]
        AFItKaFe,

        /// <summary>Itélet adatok karbantartása - Ítélet módosítás</summary>
        [Description("Ítélet adatok karbantartása - Ítélet módosítás")]
        AFItKaMo,

        /// <summary>Itélet adatok karbantartása - Itéletek töltési sorrendjének módosítása</summary>
        [Description("Ítélet adatok karbantartása - Itéletek töltési sorrendjének módosítása")]
        AFItKaSoMo,

        /// <summary>Itélet adatok karbantartása - Ítélet törlés</summary>
        [Description("Ítélet adatok karbantartása - Ítélet törlés")]
        AFItKaTo,

        /// <summary>Itélet adatok karbantartása - Ítélet nyomtatás</summary>
        [Description("Ítélet adatok karbantartása - Ítélet nyomtatás")]
        AFItKaNyo,
        #endregion Itélet

        #region Ítélet beszámítás
        /// <summary>Ítélet beszámítás adatok karbantartása - Kiválasztott ítélet mellékbüntetései lista</summary>
        [Description("Ítélet beszámítás adatok karbantartása - Kiválasztott ítélet beszámításai lista")]
        AFItBeKaLi,

        /// <summary>Ítélet beszámítás adatok karbantartása - Felvitel</summary>
        [Description("Ítélet beszámítás adatok karbantartása - Felvitel")]
        AFItBeKaFe,

        /// <summary>Ítélet beszámítás adatok karbantartása - Felvitel</summary>
        [Description("Ítélet beszámítás adatok karbantartása - Módosítás")]
        AFItBeKaMo,

        /// <summary>Ítélet beszámítás adatok karbantartása - Felvitel</summary>
        [Description("Ítélet beszámítás adatok karbantartása - Törlés")]
        AFItBeKaTo,
        #endregion Ítélet beszámítás

        #region Ítélet megszakítás
        /// <summary>Ítélet megszakítás adatok karbantartása - Kiválasztott ítélet megszakításai lista</summary>
        [Description("Ítélet megszakítás adatok karbantartása - Kiválasztott ítélet mellékbüntetései lista")]
        AFItMegKaLi,

        /// <summary>Ítélet megszakítás adatok karbantartása - Felvitel</summary>
        [Description("Ítélet megszakítás adatok karbantartása - Felvitel")]
        AFItMegKaFe,

        /// <summary>Ítélet megszakítás adatok karbantartása - Felvitel</summary>
        [Description("Ítélet megszakítás adatok karbantartása - Törlés")]
        AFItMegKaTo,
        #endregion Ítélet megszakítás

        #region Intézkedés, rendelkezés
        /// <summary>Intézkedés, rendelkezés adatok karbantartása - Kiválasztott ítélet intézkedései, rendelkezései lista</summary>
        [Description("Intézkedés, rendelkezés adatok karbantartása - Kiválasztott ítélet intézkedései, rendelkezései lista")]
        AFIntRenKaLi,

        /// <summary>Intézkedés, rendelkezés adatok karbantartása - Felvitel</summary>
        [Description("Intézkedés, rendelkezés adatok karbantartása - Felvitel")]
        AFIntRenKaFe,

        /// <summary>Intézkedés, rendelkezés adatok karbantartása - Felvitel</summary>
        [Description("Intézkedés, rendelkezés adatok karbantartása - Módosítás")]
        AFIntRenKaMo,

        /// <summary>Intézkedés, rendelkezés adatok karbantartása - Felvitel</summary>
        [Description("Intézkedés, rendelkezés adatok karbantartása - Törlés")]
        AFIntRenKaTo,
        #endregion Intézkedés, rendelkezés

        #region Mellékbüntetés
        /// <summary>Mellékbüntetés adatok karbantartása - Kiválasztott ítélet mellékbüntetései lista</summary>
        [Description("Mellékbüntetés adatok karbantartása - Kiválasztott ítélet mellékbüntetései lista")]
        AFMelBunKaLi,

        /// <summary>Mellékbüntetés adatok karbantartása - Felvitel</summary>
        [Description("Mellékbüntetés adatok karbantartása - Felvitel")]
        AFMelBunKaFe,

        /// <summary>Mellékbüntetés adatok karbantartása - Felvitel</summary>
        [Description("Mellékbüntetés adatok karbantartása - Módosítás")]
        AFMelBunKaMo,

        /// <summary>Mellékbüntetés adatok karbantartása - Felvitel</summary>
        [Description("Mellékbüntetés adatok karbantartása - Törlés")]
        AFMelBunKaTo,
        #endregion Mellékbüntetés

        #region Elkövetett bűncselelkmények

        [Description("Elkövetett bűncselelkmények - Fogvatartott elkövetett bűnycselekményei")]
        AFElkovBuncsKaEBLi,

        [Description("Elkövetett bűncselekmény - Felvitel")]
        AFElkovBuncsKaFe,

        [Description("Elkövetett bűncselekmény - Módosítás")]
        AFElkovBuncsKaMo,

        [Description("Elkövetett bűncselekmény - Törlés")]
        AFElkovBuncsKaTo,

        [Description("Elkövetett bűncselekmény - Részletek")]
        AFElkovBuncsKaRe,

        #endregion Elkövetett bűncselelkmények

        #region Foganatba vétel
        /// <summary>Foganatba vétel - Fogvatartott lista</summary>
        [Description("Foganatba vétel - Fogvatartott lista")]
        AFFogaFtLi,

        /// <summary>Foganatba vétel - Ítélet lista</summary>
        [Description("Foganatba vétel - Ítélet lista")]
        AFFogaItLi,

        /// <summary>Foganatba vétel - Foganatba vétel történet lista</summary>
        [Description("Foganatba vétel - Foganatba vétel történet lista")]
        AFFogaFoToLi,

        /// <summary>Foganatba vétel részletek</summary>
        [Description("Foganatba vétel részletek")]
        AFFoga1Re,

        /// <summary>Foganatba vétel részletek - Tényleges életfogytiglan</summary>
        [Description("Foganatba vétel részletek - Tényleges életfogytiglan")]
        AFFoga2Re,

        /// <summary>Foganatba vétel részletek - Életfogytiglan és KGyK</summary>
        [Description("Foganatba vétel részletek - Életfogytiglan és KGyK")]
        AFFoga3Re,

        /// <summary>Foganatba vétel részletek - Péntbüntetést helyettesítő</summary>
        [Description("Foganatba vétel részletek - Péntbüntetést helyettesítő")]
        AFFoga4Re,

        /// <summary>Foganatba vétel részletek - Kegyelem</summary>
        [Description("Foganatba vétel részletek - Kegyelem")]
        AFFoga5Re,

        /// <summary>Foganatba vétel részletek - Felt. szab. öszb. fogl.</summary>
        [Description("Foganatba vétel részletek - Felt. szab. öszb. fogl.")]
        AFFoga6Re,

        /// <summary>Foganatba vétel felvitel</summary>
        [Description("Foganatba vétel felvitel")]
        AFFoga1Fe,

        /// <summary>Foganatba vétel felvitel - Tényleges életfogytiglan</summary>
        [Description("Foganatba vétel felvitel - Tényleges életfogytiglan")]
        AFFoga2Fe,

        /// <summary>Foganatba vétel felvitel - Életfogytiglan és KGyK</summary>
        [Description("Foganatba vétel felvitel - Életfogytiglan és KGyK")]
        AFFoga3Fe,

        /// <summary>Foganatba vétel felvitel - Péntbüntetést helyettesítő</summary>
        [Description("Foganatba vétel felvitel - Péntbüntetést helyettesítő")]
        AFFoga4Fe,

        /// <summary>Foganatba vétel felvitel - Kegyelem</summary>
        [Description("Foganatba vétel felvitel - Kegyelem")]
        AFFoga5Fe,

        /// <summary>Foganatba vétel felvitel - Felt. szab. öszb. fogl.</summary>
        [Description("Foganatba vétel felvitel - Felt. szab. öszb. fogl.")]
        AFFoga6Fe,

        /// <summary>Foganatba vétel törlés</summary>
        [Description("Foganatba vétel törlés")]
        AFFoga1To,

        /// <summary>Foganatba vétel törlés - Tényleges életfogytiglan</summary>
        [Description("Foganatba vétel törlés - Tényleges életfogytiglan")]
        AFFoga2To,

        /// <summary>Foganatba vétel törlés - Életfogytiglan és KGyK</summary>
        [Description("Foganatba vétel törlés - Életfogytiglan és KGyK")]
        AFFoga3To,

        /// <summary>Foganatba vétel törlés - Péntbüntetést helyettesítő</summary>
        [Description("Foganatba vétel törlés - Péntbüntetést helyettesítő")]
        AFFoga4To,

        /// <summary>Foganatba vétel törlés - Kegyelem</summary>
        [Description("Foganatba vétel törlés - Kegyelem")]
        AFFoga5To,

        /// <summary>Foganatba vétel törlés - Felt. szab. öszb. fogl</summary>
        [Description("Foganatba vétel törlés - Felt. szab. öszb. fogl")]
        AFFoga6To,
        #endregion Foganatba vétel

        #region Foganatba vétel ellenőrzése
        /// <summary>Foganatba vétel ellenőrzés - Fogvatartott lista</summary>
        [Description("Foganatba vétel ellenőrzés - Fogvatartott lista")]
        AFFogaEllFtLi,

        /// <summary>Foganatba vétel ellenőrzés - Ítélet lista</summary>
        [Description("Foganatba vétel ellenőrzés - Ítélet lista")]
        AFFogaEllItLi,

        /// <summary>Foganatba vétel ellenőrzése</summary>
        [Description("Foganatba vétel ellenőrzés")]
        AFFogaEllEll,

        /// <summary>Foganatba vétel ellenőrzés</summary>
        [Description("Foganatba vétel ellenőrzés")]
        AFFoga1Ell,

        /// <summary>Foganatba vétel ellenőrzés - Tényleges életfogytiglan</summary>
        [Description("Foganatba vétel ellenőrzés - Tényleges életfogytiglan")]
        AFFoga2Ell,

        /// <summary>Foganatba vétel ellenőrzés - Életfogytiglan és KGyK</summary>
        [Description("Foganatba vétel ellenőrzés - Életfogytiglan és KGyK")]
        AFFoga3Ell,

        /// <summary>Foganatba vétel ellenőrzés - Péntbüntetést helyettesítő</summary>
        [Description("Foganatba vétel ellenőrzés - Péntbüntetést helyettesítő")]
        AFFoga4Ell,

        /// <summary>Foganatba vétel ellenőrzés - Kegyelem</summary>
        [Description("Foganatba vétel ellenőrzés - Kegyelem")]
        AFFoga5Ell,

        /// <summary>Foganatba vétel ellenőrzés - Felt. szab. öszb. fogl</summary>
        [Description("Foganatba vétel ellenőrzés - Felt. szab. öszb. fogl")]
        AFFoga6Ell,
        #endregion Foganatba vétel ellenőrzése

        #region Elzárási határozat karbantartás
        /// <summary>
        /// Elzárási határozatok karbantartása - Fogvatartott lista
        /// </summary>
        [Description("Elzárási határozatok karbantartása - Fogvatartott lista")]
        AFEHFL,

        /// <summary>
        /// Elzárási határozatok karbantartása -  Elzárási határozat lista
        /// </summary>
        [Description("Elzárási határozatok karbantartása - Elzárási határozat lista")]
        AFEHEHL,

        /// <summary>
        /// Elzárási határozatok karbantartása - Elzárási határozat részletek
        /// </summary>
        [Description("Elzárási határozatok karbantartása - Elzárási határozat részletek")]
        AFEHEHR,

        /// <summary>
        /// Elzárási határozatok karbantartása - Elzárási határozat felvitel
        /// </summary>
        [Description("Elzárási határozatok karbantartása - Elzárási határozat felvitel")]
        AFEHEHF,

        /// <summary>
        /// Elzárási határozatok karbantartása - Elzárási határozat módosítás
        /// </summary>
        [Description("Elzárási határozatok karbantartása - Elzárási határozat módosítás")]
        AFEHEHM,
        #endregion Elzárási határozat karbantartás

        #region Őrizetbevétel adatok karbantartása
        /// <summary>Őrizetbevétel adatok karbantartása - Fogvatartott lista</summary>
        [Description("Őrizetbevétel adatok karbantartása - Fogvatartott lista")]
        AFOrizAdKaFtLi,

        /// <summary>Őrizetbevétel adatok karbantartása - Őriz.vétel lista</summary>
        [Description("Őrizetbevétel adatok karbantartása - Őriz.vétel lista")]
        AFOrizAdKaOvLi,

        /// <summary>Őrizetbevétel adatok karbantartása - Felvitel</summary>
        [Description("Őrizetbevétel adatok karbantartása - Felvitel")]
        AFOrizAdKaFe,

        /// <summary>Őrizetbevétel adatok karbantartása - Módosítás</summary>
        [Description("Őrizetbevétel adatok karbantartása - Módosítás")]
        AFOrizAdKaMo,

        /// <summary>Őrizetbevétel adatok karbantartása - Törlés</summary>
        [Description("Őrizetbevétel adatok karbantartása - Törlés")]
        AFOrizAdKaTo,

        /// <summary>Őrizetbevétel adatok karbantartása - Részletek</summary>
        [Description("Őrizetbevétel adatok karbantartása - Részletek")]
        AFOrizAdKaRe,
        #endregion

        #region Fogvatartott személyek összevonása

        /// <summary>
        /// Fogvatartott személyek összevonása - személy csoport lista
        /// </summary>
        [Description("Fogvatartott személyek összevonása - személy csoport lista")]
        AFFszoSzcsl,

        /// <summary>
        /// Fogvatartott személyek összevonása - csoport minősítés
        /// </summary>
        [Description("Fogvatartott személyek összevonása - csoport minősítés")]
        AFFszoCsm,

        /// <summary>
        /// Fogvatartott személyek összevonása - személy csoport tag lista
        /// </summary>
        [Description("Fogvatartott személyek összevonása - személy csoport tag lista")]
        AFFszoSzcstl,

        /// <summary>
        /// Fogvatartott személyek összevonása - személy fogvatartási lista
        /// </summary>
        [Description("Fogvatartott személyek összevonása - személy fogvatartási lista")]
        AFFszoSzfl,

        #endregion Fogvatartott személyek összevonása

        #region Folyamatban lévő egyéb ügyek adatainak karbantartása

        /// <summary>
        /// Folyamatban lévő egyéb ügyek adatainak karbantartása - Fogvatartott lista
        /// </summary>
        [Description("Folyamatban lévő egyéb ügyek adatainak karbantartása - Fogvatartott lista")]
        AFEgyUgyAdKaFtLi,

        /// <summary>
        /// Folyamatban lévő egyéb ügyek adatainak karbantartása - Egyéb ügy lista
        /// </summary>
        [Description("Folyamatban lévő egyéb ügyek adatainak karbantartása - Egyéb ügy lista")]
        AFEgyUgyAdKaEgyUgyLi,

        /// <summary>
        /// Folyamatban lévő egyéb ügyek adatainak karbantartása - Részletek
        /// </summary>
        [Description("Folyamatban lévő egyéb ügyek adatainak karbantartása - Részletek")]
        AFEgyUgyAdKaRe,

        /// <summary>
        /// Folyamatban lévő egyéb ügyek adatainak karbantartása - Felvitel
        /// </summary>
        [Description("Folyamatban lévő egyéb ügyek adatainak karbantartása - Felvitel")]
        AFEgyUgyAdKaFe,

        /// <summary>
        /// Folyamatban lévő egyéb ügyek adatainak karbantartása - Módosítás
        /// </summary>
        [Description("Folyamatban lévő egyéb ügyek adatainak karbantartása - Módosítás")]
        AFEgyUgyAdKaMo,

        /// <summary>
        /// Folyamatban lévő egyéb ügyek adatainak karbantartása - Törlés
        /// </summary>
        [Description("Folyamatban lévő egyéb ügyek adatainak karbantartása - Törlés")]
        AFEgyUgyAdKaTo,

        #endregion Folyamatban lévő egyéb ügyek adatainak karbantartása

        #endregion Fogvatartott adatainak karbantartása

        #region többletinformáció ellenőrzése

        /// <summary>Többletinformációk listája</summary>
        [Description("Többletinformációk listája")]
        AFFoToElLi,

        /// <summary>Többletinformáció ellenőrzés módosítás</summary>
        [Description("Többletinformáció ellenőrzés módosítás")]
        AFFoToElMo,

        /// <summary>Többletinformáció ellenőrzés részletek</summary>
        [Description("Többletinformáció ellenőrzés részletek")]
        AFFoToElRe,

        /// <summary>Többletinformáció ellenőrzés törlés</summary>
        [Description("Többletinformáció ellenőrzés törlés")]
        AFFoToElTo,

        #endregion többletinformáció ellenőrzése

        #region Szállítás

        #region Szállításjelentés készítés
        /// <summary>
        /// Szállítási jelentés készítés - Fogvatartott lista
        /// </summary>
        [Description("Szállítási jelentés készítés - Fogvatartott lista")]
        AFSzJelKarbFtLi,
        /// <summary>
        /// Szállítási jelentés készítés - Felvitel
        /// </summary>
        [Description("Szállítási jelentés készítés - Felvitel")]
        AFSzJelKarbFe,
        /// <summary>
        /// Szállítási jelentés készítés - Módosítás
        /// </summary>
        [Description("Szállítási jelentés készítés - Módosítás")]
        AFSzJelKarbMo,
        /// <summary>
        /// Szállítási jelentés készítés - Részletek
        /// </summary>
        [Description("Szállítási jelentés készítés - Részletek")]
        AFSzJelKarbRe,
        /// <summary>
        /// Szállítási jelentés készítés - Törlés
        /// </summary>
        [Description("Szállítási jelentés készítés - Törlés")]
        AFSzJelKarbTo,
        #endregion Szállításjelentés készítés

        #region Szállításjelentés küldése
        /// <summary>
        /// Szállítási jelentés küldése - Fogvatartott lista
        /// </summary>
        [Description("Szállítási jelentés küldése - Fogvatartott lista")]
        AFSzJelKulFtLi,
        /// <summary>
        /// Szállítási jelentés küldése
        /// </summary>
        [Description("Szállítási jelentés küldése")]
        AFSzJelKulFe,
        #endregion Szállításjelentés küldése

        #region Szállítás előjegyzése
        /// <summary>Előjegyzés szállításra - Fogvatartott lista</summary>
        [Description("Előjegyzés szállításra - Fogvatartott lista")]
        AFSzEFtLi,

        /// <summary>Előjegyzés szállításra - Szállítás lista fogvartatotthoz</summary>
        [Description("Előjegyzés szállításra - Szállítás lista fogvartatotthoz")]
        AFSzESzaLi,

        /// <summary>Előjegyzés szállításra - Szállítás részletek</summary>
        [Description("Előjegyzés szállításra - Szállítás részletek")]
        AFSzESzRe,

        /// <summary>Előjegyzés szállításra - Szállítás módosítás</summary>
        [Description("Előjegyzés szállításra - Szállítás módosítás")]
        AFSzESzM,

        /// <summary>Előjegyzés szállításra - Szállítás létrehozás</summary>
        [Description("Előjegyzés szállításra - Szállítás létrehozás")]
        AFSzESzL,
        #endregion Szállítás előjegyzése

        #region Szállítás előjegyzése több fogvatartottnak

        #region Előjegyzés teljes névsorból

        /// <summary>Szállítási előjegyzés rögzítése több fogvatartottnak - Fogvatartott lista</summary>
        [Description("Szállítási előjegyzés rögzítése több fogvatartottnak - Fogvatartott lista")]
        AFSzERTFFl,

        /// <summary>Szállítási előjegyzés rögzítése több fogvatartottnak</summary>
        [Description("Szállítási előjegyzés rögzítése több fogvatartottnak")]
        AFSzERTF0,

        /// <summary>Szállítási előjegyzés rögzítése több fogvatartottnak - Szállítás felvitel</summary>
        [Description("Szállítási előjegyzés rögzítése több fogvatartottnak - Szállítás felvitel")]
        AFSzERTFSzF,
        #endregion Előjegyzés teljes névsorból

        #region Megőrzésről visszaszállítás előjegyzése
        /// <summary>Szállítási előjegyzés rögzítése több fogvatartottnak - Megőrzésből visszaszállítás</summary>
        [Description("Szállítási előjegyzés rögzítése több fogvatartottnak - Megőrzésből visszaszállítás")]
        AFSzERTF1,
        #endregion Megőrzésről visszaszállítás előjegyzése

        #region Megőrzésről továbbszállítás előjegyzése
        /// <summary>Szállítási előjegyzés rögzítése több fogvatartottnak - Megőrzésről továbbszállítás</summary>
        [Description("Szállítási előjegyzés rögzítése több fogvatartottnak - Megőrzésről továbbszállítás")]
        AFSzERTF2,
        #endregion Megőrzésről továbbszállítás előjegyzése

        #region Idegen fokozatba kerülés miatt előjegyzés
        /// <summary>Szállítási előjegyzés rögzítése több fogvatartottnak - Idegen fokozatba kerülés miatt</summary>
        [Description("Szállítási előjegyzés rögzítése több fogvatartottnak - Idegen fokozatba kerülés miatt")]
        AFSzERTF3,
        #endregion Idegen fokozatba kerülés miatt előjegyzés

        #region Nyomozásra kiadás miatt előjegyzés
        /// <summary>Szállítási előjegyzés rögzítése több fogvatartottnak - Nyomozás miatt</summary>
        [Description("Szállítási előjegyzés rögzítése több fogvatartottnak - Nyomozás miatt")]
        AFSzERTF4,
        #endregion Nyomozásra kiadás miatt előjegyzés

        #region Távoli előállítás miatt előjegyzés
        /// <summary>Szállítási előjegyzés rögzítése több fogvatartottnak - Előállítás tárgyalásra</summary>
        [Description("Szállítási előjegyzés rögzítése több fogvatartottnak - Előállítás tárgyalásra")]
        AFSzERTF5,
        #endregion Távoli előállítás miatt előjegyzés

        #region Átszállítási kérelem miatt előjegyzés
        /// <summary>Szállítási előjegyzés rögzítése több fogvatartottnak - Átszállítási kérelem miatt</summary>
        [Description("Szállítási előjegyzés rögzítése több fogvatartottnak - Átszállítási kérelem miatt")]
        AFSzERTF6,
        #endregion Átszállítási kérelem miatt előjegyzés
        #region Egészségügyi esemény miatt előjegyzés
        /// <summary>Szállítási előjegyzés rögzítése több fogvatartottnak - Egészségügyi esemény miatt előjegyzés</summary>
        [Description("Szállítási előjegyzés rögzítése több fogvatartottnak - Egészségügyi esemény miatt előjegyzés")]
        AFSzERTF7,
        #endregion Átszállítási kérelem miatt előjegyzés
        #endregion Szállítás előjegyzése több fogvatartottnak

        #region Szállítás útbaindítása
        /// <summary>Szállításra átadás rögzítése - Fogvatartott lista</summary>
        [Description("Szállításra átadás rögzítése - Fogvatartott lista")]
        AFSzARFl,
        #endregion Szállítás útbaindítása

        #region Szállítás fogadása

        [Description("Befogadás szállításból - Fogvatartott lista")]
        AFBefSzaFtLi,

        [Description("Befogadás szállításból - Felvitel")]
        AFBefSzaFe,

        #endregion Szállítás fogadása

        #region Beosztás szállító járműre

        /// <summary>
        /// Beosztás szállító járműre - Szállító jármű lista
        /// </summary>
        [Description("Beosztás szállító járműre - Szállító jármű lista")]
        AFBeoSzaJarSzaJaLi,

        /// <summary>
        /// Beosztás szállító járműre - Szállításra előjegyzettek
        /// </summary>
        [Description("Beosztás szállító járműre - Szállításra előjegyzettek")]
        AFBeoSzaJarSzaEloLi,

        /// <summary>
        /// Beosztás szállító járműre - Beosztottak névsora
        /// </summary>
        [Description("Beosztás szállító járműre - Beosztottak névsora")]
        AFBeoSzaJarBeoNevLi,

        /// <summary>
        /// Beosztás szállító járműre - Törlés a beosztottak névsorából
        /// </summary>
        [Description("Beosztás szállító járműre - Törlés a beosztottak névsorából")]
        AFBeoSzaJarBeoNevTo,

        /// <summary>
        /// Beosztás szállító járműre - Szállítási jegyzék nyomtatása
        /// </summary>
        [Description("Beosztás szállító járműre - Szállítási jegyzék nyomtatása")]
        AFBeoSzaJarBeoNevNyo,

        /// <summary>
        /// Beosztás szállító járműre - Érték és letétcsomagok száma
        /// </summary>
        [Description("Beosztás szállító járműre - Érték és letétcsomagok száma")]
        AFBeoSzaJarErtLetCsF,

        #endregion Beosztás szállító járműre

        #region Útbaindítás visszavonása

        /// <summary>
        /// Útbaindítás visszavonása - Fogvatartott lista
        /// </summary>
        [Description("Útbaindítás visszavonása - Fogvatartott lista")]
        AFUtIndViFtLi,

        #endregion Útbaindítás visszavonása

        #endregion Szállítás

        #region Nyomtatások

        #region 8-as számú adatlap - Ítéletek
        /// <summary>
        /// 8-as számú adatlap - Ítéletek
        /// </summary>
        [Description("8-as számú adatlap - Ítéletek")]
        AF8asNyomIt,
        #endregion 8-as számú adatlap - Ítéletek

        #region Szabadulási igazolás
        /// <summary>
        /// Szabadulási igazolás nyomtatása
        /// </summary>
        [Description("Szabadulási igazolás nyomtatása")]
        AFSzabIgNyom,
        #endregion Szabadulási igazolás

        #region Szabadulási igazolás ítéletről
        /// <summary>
        /// Szabadulási igazolás nyomtatása - Ítéletek
        /// </summary>
        [Description("Szabadulási igazolás nyomtatása - Ítéletek")]
        AFSzabIgNyomIt,
        #endregion Szabadulási igazolás ítéletről

        #region Szabadulási igazolás előzetes letartóztatásról
        /// <summary>
        /// Szabadulási igazolás nyomtatása - Előzetes letartóztatások
        /// </summary>
        [Description("Szabadulási igazolás nyomtatása - Előzetes letartóztatások")]
        AFSzabIgNyomElozLet,
        #endregion Szabadulási igazolás előzetes letartóztatásról

        #region Szabadulási igazolás elzárásról
        /// <summary>
        /// Szabadulási igazolás nyomtatása - Elzárás határozatok
        /// </summary>
        [Description("Szabadulási igazolás nyomtatása - Elzárás határozatok")]
        AFSzabIgNyomElzHat,
        #endregion Szabadulási igazolás elzárásról

        #region Szabadulási igazolás bf/elz-félbeszakításról
        /// <summary>
        /// Szabadulási igazolás nyomtatása - Büntetés-félbeszakítások
        /// </summary>
        [Description("Szabadulási igazolás nyomtatása - Büntetés-félbeszakítások")]
        AFSzabIgNyomBuntFelb,
        #endregion Szabadulási igazolás bf/elz-félbeszakításról

        #region Értesítők

        /// <summary>
        /// Értesítések karbantartása - Fogvatartott lista
        /// </summary>
        [Description("Értesítések - Fogvatartott lista - kpi.nyt.")]
        AFEFLKNy,

        /// <summary>
        /// Értesítések karbantartása - Fogvatartott lista
        /// </summary>
        [Description("Értesítések - Fogvatartott lista")]
        AFEkFL,

        /// <summary>
        /// Értesítések karbantartása - Fogvatartott értesítéseinek listája
        /// </summary>
        [Description("Értesítések - Fogvatartott értesítéseinek listája")]
        AFEkFEL,

        /// <summary>
        /// Értesítések karbantartása - Értesítés részletek
        /// </summary>
        [Description("Értesítések - Értesítés részletek")]
        AFEkER,

        /// <summary>
        /// Értesítések karbantartása - Értesítés felvitel
        /// </summary>
        [Description("Értesítések - Értesítés felvitel")]
        AFEkEF,

        /// <summary>
        /// Értesítések karbantartása - Értesítés módosítás
        /// </summary>
        [Description("Értesítések - Értesítés módosítás")]
        AFEkEM,

        #endregion Értesítők

        #region Összbüntetésbe foglalási javaslat

        /// <summary>
        /// Összbüntetésbe foglalási javaslat - Feljegyzés
        /// </summary>
        [Description("Összbüntetésbe foglalási javaslat - Feljegyzés")]
        AFOfjF,

        /// <summary>
        /// Összbüntetésbe foglalási javaslat - Feljegyzés készítése"
        /// </summary>
        [Description("Összbüntetésbe foglalási javaslat - Feljegyzés készítése")]
        AFOfjFk,

        /// <summary>
        /// Összbüntetésbe foglalási javaslat - Fogvatartott lista
        /// </summary>
        [Description("Összbüntetésbe foglalási javaslat - Fogvatartott lista")]
        AFOfjFl,

        /// <summary>
        /// Összbüntetésbe foglalási javaslat - Ítélet lista
        /// </summary>
        [Description("Összbüntetésbe foglalási javaslat - Ítélet lista")]
        AFOfjIl,

        #endregion Összbüntetésbe foglalási javaslat

        #region Belég nyomtatása

        /// <summary>
        /// Belég nyomtatása - Fogvatartott lista
        /// </summary>
        [Description("Belég nyomtatása - Fogvatartott lista")]
        AFBelNyoFtLi,

        /// <summary>
        /// Belég nyomtatása - Nyomtatás
        /// </summary>
        [Description("Belég nyomtatása - Nyomtatás")]
        AFBelNyoNyo,

        #endregion Belég nyomtatása

        #region Mutatókarton

        /// <summary>Mutatókarton - Fogvatartott lista</summary>
        [Description("Mutatókarton - Fogvatartott lista")]
        ALMuKaFtLi,

        /// <summary>
        /// Mutatókarton
        /// </summary>
        [Description("Mutatókarton")]
        AFMuKaRe,

        #endregion Mutatókarton

        #endregion Nyomtatások

        #endregion Bűnügyi nyilvántartás

        #region Nevelés

        #region Kapcsolattartás

        #region Kapcsolattartó adatok karbantartása
        /// <summary>Kapcsolattartó adatok - Reintegrációs részleg lista</summary>
        [Description("Kapcsolattartó adatok - Reintegrációs részleg lista")]
        AFKapcsKarbNCSL,

        /// <summary>Kapcsolattartó adatok - Fogvatartott lista</summary>
        [Description("Kapcsolattartó adatok - Fogvatartott lista")]
        AFKapcsKarbFL,

        /// <summary>Kapcsolattartó adatok - Kiválasztott fogvatartott tételei</summary>
        [Description("Kapcsolattartó adatok - Kiválasztott fogvatartott tételei")]
        AFKapcsKarbKtL,

        /// <summary>Kapcsolattartó adatok - Kiválasztott tétel részletei</summary>
        [Description("Kapcsolattartó adatok - Kiválasztott tétel részletei")]
        AFKapcsKarbR,

        /// <summary>Kapcsolattartó adatok - Felvitel</summary>
        [Description("Kapcsolattartó adatok - Felvitel")]
        AFKapcsKarbF,

        /// <summary>Kapcsolattartó adatok - Módosítás</summary>
        [Description("Kapcsolattartó adatok - Módosítás")]
        AFKapcsKarbM,

        #endregion Kapcsolattartó adatok karbantartása

        #region Kapcsolattartási módok

        /// <summary>Kapcsolattartási mód adatok - Kiválasztott kapcsolattartó tételei</summary>
        [Description("Kapcsolattartási mód adatok - Kiválasztott kapcsolattartó tételei")]
        AFKapcsModKarbKtML,

        /// <summary>Kapcsolattartási mód adatok - Kiválasztott tétel részletei</summary>
        [Description("Kapcsolattartási mód adatok - Kiválasztott tétel részletei")]
        AFKapcsModKarbR,

        /// <summary>Kapcsolattartási mód adatok - Felvitel</summary>
        [Description("Kapcsolattartási mód adatok - Felvitel")]
        AFKapcsModKarbF,

        /// <summary>Kapcsolattartási mód adatok - Módosítás</summary>
        [Description("Kapcsolattartási mód adatok - Módosítás")]
        AFKapcsModKarbM,

        #endregion Kapcsolattartási módok

        #region Korlátozások


        /// <summary>Korlátozás adatok - Kiválasztott kapcsolattartó tételei</summary>
        [Description("Korlátozás adatok - Kiválasztott kapcsolattartó tételei")]
        AFKorlKarbKL,

        /// <summary>Korlátozás adatok - Kiválasztott tétel részletei</summary>
        [Description("Korlátozás adatok - Kiválasztott tétel részletei")]
        AFKorlKarbR,

        /// <summary>Korlátozás adatok - Felvitel</summary>
        [Description("Korlátozás adatok - Felvitel")]
        AFKorlKarbF,

        /// <summary>Korlátozás adatok - Módosítás</summary>
        [Description("Korlátozás adatok - Módosítás")]
        AFKorlKarbM,

        #endregion Korlátozások

        #region Csomag

        #region Csomagküldési engedély felvitele több fogvatartottnak

        /// <summary>
        /// Csomagküldési engedély csoportos felvitele - Fogvatartott lista
        /// </summary>
        [Description("Csomagküldési engedély csoportos felvitele - Fogvatartott lista")]
        AFCsEngCsFeFtLi,

        /// <summary>
        /// Csomagküldési engedély csoportos felvitele - Felvitel
        /// </summary>
        [Description("Csomagküldési engedély csoportos felvitele - Felvitel")]
        AFCsEngCsFeFe,

        #endregion Csomagküldési engedély felvitele több fogvatartottnak

        #region Csomagküldési engedély lezárása több fogvatartottnak

        /// <summary>
        /// Csomagküldési engedélyek csoportos lezárása - Csomagküldési engedély lista
        /// </summary>
        [Description("Csomagküldési engedélyek csoportos lezárása - Csomagküldési engedély lista")]
        AFCsEngCsLeCsEngLi,

        /// <summary>
        /// Csomagküldési engedélyek csoportos lezárása - Lezárás
        /// </summary>
        [Description("Csomagküldési engedélyek csoportos lezárása - Lezárás")]
        AFCsEngCsLeFe,

        #endregion Csomagküldési engedély lezárása több fogvatartottnak

        #region Csomagküldési engedély, szelvény nyomtatása

        /// <summary>
        /// Csomagküldési engedély karbantartása - Fogvatartott lista
        /// </summary>
        [Description("Csomagküldési engedély karbantartása - Fogvatartott lista")]
        AFCsEngKarFtLi,

        /// <summary>
        /// Csomagküldési engedély karbantartása - Csomagküldési engedély lista
        /// </summary>
        [Description("Csomagküldési engedély karbantartása - Csomagküldési engedély lista")]
        AFCsEngKarCsEngLi,

        /// <summary>
        /// Csomagküldési engedély karbantartása - Részletek
        /// </summary>
        [Description("Csomagküldési engedély karbantartása - Részletek")]
        AFCsEngKarRe,

        /// <summary>
        /// Csomagküldési engedély karbantartása - Felvitel
        /// </summary>
        [Description("Csomagküldési engedély karbantartása - Felvitel")]
        AFCsEngKarFe,

        /// <summary>
        /// Csomagküldési engedély karbantartása - Módosítás
        /// </summary>
        [Description("Csomagküldési engedély karbantartása - Módosítás")]
        AFCsEngKarMo,

        /// <summary>
        /// Csomagküldési engedély karbantartása - Törlés
        /// </summary>
        [Description("Csomagküldési engedély karbantartása - Törlés")]
        AFCsEngKarTo,

        /// <summary>
        /// Csomagküldési engedély karbantartása - Szelvény nyomtatás
        /// </summary>
        [Description("Csomagküldési engedély karbantartása - Szelvény nyomtatás")]
        AFCsEngKarNy,

        #endregion Csomagküldési engedély, szelvény nyomtatása

        #endregion Csomag

        #region Látogatási engedély, engedély nyomtatás

        /// <summary>Látogatási engedély - Reintegrációs részleg lista</summary>
        [Description("Látogatási engedély - Reintegrációs részleg lista")]
        AFLatEngKarbNCsL,

        /// <summary>Látogatási engedély - Fogvatartott lista</summary>
        [Description("Látogatási engedély - Fogvatartott lista")]
        AFLatEngKarbFL,

        /// <summary>Látogatási engedély - Kiválasztott fogvatartott tételei</summary>
        [Description("Látogatási engedély - Kiválasztott fogvatartott tételei")]
        AFLatEngKarbLiEL,

        /// <summary>Látogatási engedély - Kiválasztott tétel részletei</summary>
        [Description("Látogatási engedély - Kiválasztott tétel részletei")]
        AFLatEngKarbLiER,

        /// <summary>Látogatási engedély - Felvitel</summary>
        [Description("Látogatási engedély - Felvitel")]
        AFLatEngKarbLiEF,

        /// <summary>Látogatási engedély - Módosítás</summary>
        [Description("Látogatási engedély - Módosítás")]
        AFLatEngKarbLiEM,

        /// <summary>Látogatási engedély - Kiválasztott engedélyhez tartozó látogatók</summary>
        [Description("Látogatási engedély - Kiválasztott engedélyhez tartozó látogatók")]
        AFLatEngKarbLoEL,

        /// <summary>Látogatási engedély - Kiválasztott engedélyhez tartozó látogató részletek</summary>
        [Description("Látogatási engedély - Kiválasztott engedélyhez tartozó látogató részletek")]
        AFLatEngKarbLoER,

        /// <summary>Látogatási engedély - Kiválasztott fogvatartott tételei</summary>
        [Description("Látogatási engedély - Kiválasztott fogvatartott korlátozásai")]
        AFLatEngKarbKorlL,

        /// <summary>Látogatási engedély - Kiválasztott fogvatartott tételei</summary>
        [Description("Látogatási engedély - Kiválasztott fogvatartott előállításai")]
        AFLatEngKarbEloL,

        /// <summary>Látogatási engedély - Kiválasztott fogvatartott tételei</summary>
        [Description("Látogatási engedély - Kiválasztott fogvatartott szállításai")]
        AFLatEngKarbSzallL,

        /// <summary>Látogatási engedély - Kiválasztott fogvatartott tételei</summary>
        [Description("Látogatási engedély - Kiválasztott fogvatartott idézései")]
        AFLatEngKarbIdezL,

        #endregion Látogatási engedély, engedély nyomtatás

        #endregion Kapcsolattartás

        #region Kérelem/panasz kezelése

        #region Kérelem, panasz előterjesztése
        /// <summary>Kérelem/panasz adatok karbantartása</summary>
        [Description("Kérelem/panasz adatok karbantartása")]
        AFKpAKPv,

        /// <summary>Kérelem/panasz adatok karbantartása - Reintegrációs részleg lista</summary>
        [Description("Kérelem/panasz adatok karbantartása - Reintegrációs részleg lista")]
        AFKpAKNCsL,

        /// <summary>Kérelem/panasz adatok karbantartása - Fogvatartott lista</summary>
        [Description("Kérelem/panasz adatok karbantartása - Fogvatartott lista")]
        AFKpPAKFL,

        /// <summary>Kérelem/panasz adatok karbantartása - Fogvatartott kérelemeinek, panaszainak listája</summary>
        [Description("Kérelem/panasz adatok karbantartása - Fogvatartott kérelemeinek, panaszainak listája")]
        AFKPAKKpL,

        /// <summary>Kérelem/panasz adatok karbantartása - Kérelem/panasz részletek</summary>
        [Description("Kérelem/panasz adatok karbantartása - Kérelem/panasz részletek")]
        AFKPAKKpR,

        /// <summary>Kérelem/panasz adatok karbantartása - Kérelem/panasz módosítás</summary>
        [Description("Kérelem/panasz adatok karbantartása - Kérelem/panasz módosítás")]
        AFKPAKKpM,

        /// <summary>Kérelem/panasz adatok karbantartása - Kérelem/panasz felvitel</summary>
        [Description("Kérelem/panasz adatok karbantartása - Kérelem/panasz felvitel")]
        AFKPAKKpF,
        #endregion Kérelem, panasz előterjesztése

        #region Kérelem, panasz kihirdetése

        /// <summary>
        /// Engedélyezett, elutasított kérelem/panasz kihirdetése - Kérelem/panasz lista
        /// </summary>
        [Description("Engedélyezett, elutasított kérelem/panasz kihirdetése - Kérelem/panasz lista")]
        AFKPKKPL,

        /// <summary>
        /// Engedélyezett, elutasított kérelem/panasz kihirdetése - Kérelem/panasz részletek
        /// </summary>
        [Description("Engedélyezett, elutasított kérelem/panasz kihirdetése - Kérelem/panasz részletek")]
        AFKPKKPR,

        /// <summary>
        /// Engedélyezett, elutasított kérelem/panasz kihirdetése - Kérelem/panasz kihirdetése
        /// </summary>
        [Description("Engedélyezett, elutasított kérelem/panasz kihirdetése - Kérelem/panasz kihirdetése")]
        AFKPKKPK,

        /// <summary>
        /// Engedélyezett, elutasított kérelem/panasz kihirdetése - Kérelem/panasz visszaküldése
        /// </summary>
        [Description("Engedélyezett, elutasított kérelem/panasz kihirdetése - Kérelem/panasz visszaküldése")]
        AFKPKKPV,

        #endregion Kérelem, panasz kihirdetése

        #region Kérelem, panasz visszavonása

        /// <summary>
        /// Folyamatban lévő kérelem/panasz visszavonása - Kérelem/panasz lista
        /// </summary>
        [Description("Folyamatban lévő kérelem/panasz visszavonása - Kérelem/panasz lista")]
        AFKPVKPL,

        /// <summary>
        /// Folyamatban lévő kérelem/panasz visszavonása - Kérelem/panasz módosítása
        /// </summary>
        [Description("Folyamatban lévő kérelem/panasz visszavonása - Kérelem/panasz módosítása")]
        AFKPVKPM,

        #endregion Kérelem, panasz visszavonása

        #region Panasz előterjesztése elutasított kérelem miatt

        /// <summary>
        /// Panasz előterjesztése elutasított kérelem miatt - Reintegrációs részleg lista
        /// </summary>
        [Description("Panasz előterjesztése elutasított kérelem miatt - Reintegrációs részleg lista")]
        AFKPPeNCSL,

        /// <summary>
        /// Panasz előterjesztése elutasított kérelem miatt - Fogvatartott lista
        /// </summary>
        [Description("Panasz előterjesztése elutasított kérelem miatt - Fogvatartott lista")]
        AFKPPeFL,

        /// <summary>
        /// Panasz előterjesztése elutasított kérelem miatt - Panaszolt kérelem részletek
        /// </summary>
        [Description("Panasz előterjesztése elutasított kérelem miatt - Panaszolt kérelem részletek")]
        AFKPPeKPR,

        /// <summary>
        /// Panasz előterjesztése elutasított kérelem miatt - Panasz rögzítése
        /// </summary>
        [Description("Panasz előterjesztése elutasított kérelem miatt - Panasz rögzítése")]
        AFKPPeKPPA,

        /// <summary>
        /// Panasz előterjesztése elutasított kérelem miatt - Kérelem/panasz lista
        /// </summary>
        [Description("Panasz előterjesztése elutasított kérelem miatt - Kérelem/panasz lista")]
        AFKPPeKPL,

        /// <summary>
        /// Panasz előterjesztése elutasított kérelem miatt - Kérelem/panasz módosítása
        /// </summary>
        [Description("Panasz előterjesztése elutasított kérelem miatt - Kérelem/panasz módosítása")]
        AFKPPeKPF,

        #endregion Panasz előterjesztése elutasított kérelem miatt

        #region Lezárt kérelem, panasz javítása

        /// <summary>
        /// Lezárt kérelem/panasz javítása - Kérelem panasz lista
        /// </summary>
        [Description("Lezárt kérelem/panasz javítása - Kérelem panasz lista")]
        AFKPJFL,

        /// <summary>
        /// Lezárt kérelem/panasz javítása - Kérelem panasz módosítása
        /// </summary>
        [Description("Lezárt kérelem/panasz javítása - Kérelem panasz módosítása")]
        AFKPJKPM,

        #endregion Lezárt kérelem, panasz javítása

        #endregion Kérelem/panasz kezelése

        #region Nevelői/Pszichológusi feljegyzés/ vélemény
        /// <summary>Feljegyzések, vélemények karbantartása - Fogvatartott lista</summary>
        [Description("Feljegyzések, vélemények karbantartása - Fogvatartott lista")]
        AFVelemenyKaFtLi,

        /// <summary>Feljegyzések, vélemények karbantartása - Vélemény lista</summary>
        [Description("Feljegyzések, vélemények karbantartása - Vélemény lista")]
        AFVelemenyKaVeLi,

        /// <summary>Feljegyzések, vélemények karbantartása - Felvitel</summary>
        [Description("Feljegyzések, vélemények karbantartása - Felvitel")]
        AFVelemenyKaFe,

        /// <summary>Feljegyzések, vélemények karbantartása - Módosítás</summary>
        [Description("Feljegyzések, vélemények karbantartása - Módosítás")]
        AFVelemenyKaMo,

        /// <summary>Feljegyzések, vélemények karbantartása - Törlés</summary>
        [Description("Feljegyzések, vélemények karbantartása - Törlés")]
        AFVelemenyKaTo,

        /// <summary>Feljegyzések, vélemények karbantartása - Részletek</summary>
        [Description("Feljegyzések, vélemények karbantartása - Részletek")]
        AFVelemenyKaRe,

        #endregion Nevelői/Pszichológusi feljegyzés/ vélemény

        #region Dohányzási szokás

        /// <summary>Dohányzási adatok - Fogvatartott Lista</summary>
        [Description("Dohányzási adatok - Fogvatartott Lista")]
        AFDohKarbFoLi,

        /// <summary>Dohányzási adatok - Lista</summary>
        [Description("Dohányzási adatok - Lista")]
        AFDohKarbLi,

        /// <summary>Dohányzási adatok - Részletek</summary>
        [Description("Dohányzási adatok - Részletek")]
        AFDohKarbRe,

        /// <summary>Dohányzási adatok - Felvitel</summary>
        [Description("Dohányzási adatok - Felvitel")]
        AFDohKarbFe,

        /// <summary>Dohányzási adatok - Módosítás</summary>
        [Description("Dohányzási adatok - Módosítás")]
        AFDohKarbMo,

        /// <summary>Dohányzási adatok - Törlés</summary>
        [Description("Dohányzási adatok - Törlés")]
        AFDohKarbT,

        #endregion Dohányzási szokás

        #region Jutalmazás nevelői jogkörben

        /// <summary>
        /// Jutalmazás nevelői jogkörben - Jutalmazás lista
        /// </summary>
        [Description("Jutalmazás nevelői jogkörben - Jutalmazás lista")]
        AFJutNevJogJutLi,

        /// <summary>
        /// Jutalmazás nevelői jogkörben - Fogvatartott lista
        /// </summary>
        [Description("Jutalmazás nevelői jogkörben - Fogvatartott lista")]
        AFJutNevJogFtLi,

        /// <summary>
        /// Jutalmazás nevelői jogkörben - Felvitel
        /// </summary>
        [Description("Jutalmazás nevelői jogkörben - Felvitel")]
        AFJutNevJogFe,

        /// <summary>
        /// Jutalmazás nevelői jogkörben - Részletek
        /// </summary>
        [Description("Jutalmazás nevelői jogkörben - Részletek")]
        AFJutNevJogRe,

        /// <summary>
        /// Jutalmazás nevelői jogkörben - Véleményezés, továbbítás, döntés
        /// </summary>
        [Description("Jutalmazás nevelői jogkörben - Véleményezés, továbbítás, döntés")]
        AFJutNevJogMo,

        /// <summary>
        /// Jutalmazás nevelői jogkörben - Törlés
        /// </summary>
        [Description("Jutalmazás nevelői jogkörben - Törlés")]
        AFJutNevJogTo,

        /// <summary>
        /// Jutalmazás nevelői jogkörben - Csoportos véleményezés
        /// </summary>
        [Description("Jutalmazás nevelői jogkörben - Csoportos véleményezés")]
        AFJutNevJogCsVe,

        /// <summary>
        /// Jutalmazás nevelői jogkörben -  Csoportosan véleményezhető jutalmak listája
        /// </summary>
        [Description("Jutalmazás nevelői jogkörben - Csoportosan véleményezhető jutalmak listája")]
        AFJutNevJogCsVeJutLi,

        /// <summary>
        /// Jutalmazás nevelői jogkörben - Összevonható jutalmak listája
        /// </summary>
        [Description("Jutalmazás nevelői jogkörben - Összevonható jutalmak listája")]
        AFJutNevJogOvJutLi,

        /// <summary>
        /// Jutalmazás nevelői jogkörben - Elkülönítés
        /// </summary>
        [Description("Jutalmazás nevelői jogkörben - Elkülönítés")]
        AFJutNevJogElk,

        #endregion Jutalmazás nevelői jogkörben

        #region Fegyelmi ügy intézés nevelői jogkörben

        /// <summary>
        /// Fenyítés kiszabása nevelői jogkörben - fogvatartott lista
        /// </summary>
        [Description("Fenyítés kiszabása nevelői jogkörben - fogvatartott lista")]
        AFFknjFl,

        /// <summary>
        /// Fenyítés kiszabása nevelői jogkörben - fegyelmi lista
        /// </summary>
        [Description("Fenyítés kiszabása nevelői jogkörben - fegyelmi lista")]
        AFFknjFul,

        /// <summary>
        /// Fenyítés kiszabása nevelői jogkörben - Feddés
        /// </summary>
        [Description("Fenyítés kiszabása nevelői jogkörben - Feddés")]
        AFFknjF,

        /// <summary>
        /// Fenyítés kiszabása nevelői jogkörben - Visszaküldés
        /// </summary>
        [Description("Fenyítés kiszabása nevelői jogkörben - Visszaküldés")]
        AFFknjV,

        /// <summary>
        /// Fenyítés kiszabása nevelői jogkörben - Kioktatás
        /// </summary>
        [Description("Fenyítés kiszabása nevelői jogkörben - Kioktatás")]
        AFFknjK,

        /// <summary>
        /// Fenyítés kiszabása nevelői jogkörben - fenyítés végrehajtása
        /// </summary>
        [Description("Fenyítés kiszabása nevelői jogkörben - fenyítés végrehajtása")]
        AFFknjFv,

        /// <summary>
        /// Fenyítés kiszabása nevelői jogkörben - Fegyelmi ügy részletei
        /// </summary>
        [Description("Fenyítés kiszabása nevelői jogkörben - Fegyelmi ügy részletei")]
        AFFknjR,

        #endregion Fegyelmi ügy intézés nevelői jogkörben

        #region Fogvatartott tisztségei

        /// <summary>
        /// Fogvatartott tisztségeinek karbantartása - Fogvatartott lista
        /// </summary>
        [Description("Fogvatartott tisztségeinek karbantartása - Fogvatartott lista")]
        AFFtTiKaFtLi,

        /// <summary>
        /// Fogvatartott tisztségeinek karbantartása - Fogvatartott tisztség lista
        /// </summary>
        [Description("Fogvatartott tisztségeinek karbantartása - Fogvatartott tisztség lista")]
        AFFtTiKaFtTiLi,

        /// <summary>
        /// Fogvatartott tisztségeinek karbantartása - Részletek
        /// </summary>
        [Description("Fogvatartott tisztségeinek karbantartása - Részletek")]
        AFFtTiKaRe,

        /// <summary>
        /// Fogvatartott tisztségeinek karbantartása - Felvitel
        /// </summary>
        [Description("Fogvatartott tisztségeinek karbantartása - Felvitel")]
        AFFtTiKaFe,

        /// <summary>
        /// Fogvatartott tisztségeinek karbantartása - Módosítás
        /// </summary>
        [Description("Fogvatartott tisztségeinek karbantartása - Módosítás")]
        AFFtTiKaMo,

        /// <summary>
        /// Fogvatartott tisztségeinek karbantartása - Törlés
        /// </summary>
        [Description("Fogvatartott tisztségeinek karbantartása - Törlés")]
        AFFtTiKaTo,

        /// <summary>
        /// Fogvatartott tisztségeinek karbantartása - Megbízás nyomtatása
        /// </summary>
        [Description("Fogvatartott tisztségeinek karbantartása - Megbízás nyomtatása")]
        AFFtTiKaMbNy,

        #endregion Fogvatartott tisztségei

        #region Oktatási formák adatai
        [Description("Oktatások, oktatási adatok karbantartása - Oktatási lista")]
        AFOktatAdatokKaOkLi,
        [Description("Oktatások, oktatási adatok karbantartása - Részletek")]
        AFOktatAdatokKaRe,
        [Description("Oktatások, oktatási adatok karbantartása - Felvitel")]
        AFOktatAdatokKaFe,
        [Description("Oktatások, oktatási adatok karbantartása - Módosítás")]
        AFOktatAdatokKaMo,
        [Description("Oktatások, oktatási adatok karbantartása - Törlés")]
        AFOktatAdatokKaTo,
        #endregion Oktatási formák adatai

        #region Fogvatartott oktatási adatainak karbantartása
        [Description("Fogvatartott oktatási adatainak karbantartása - Fogvatartott lista")]
        AFFtOktAdatokKaFtLi,
        [Description("Fogvatartott oktatási adatainak karbantartása - Oktatás résztvevő lista")]
        AFFtOktAdatokKaOkLi,
        [Description("Fogvatartott oktatási adatainak karbantartása - Részletek")]
        AFFtOktAdatokKaRe,
        [Description("Fogvatartott oktatási adatainak karbantartása - Felvitel")]
        AFFtOktAdatokKaFe,
        [Description("Fogvatartott oktatási adatainak karbantartása - Módosítás")]
        AFFtOktAdatokKaMo,
        [Description("Fogvatartott oktatási adatainak karbantartása - Törlés")]
        AFFtOktAdatokKaTo,
        #endregion Fogvatartott oktatási adatainak karbantartása

        #region Beiskolázási adatok csoportos felvitele
        [Description("Beiskolázási adatok csoportos felvitele - Oktatási formák lista")]
        AFBeiskAdCsopKaOkLi,
        [Description("Beiskolázási adatok csoportos felvitele - Oktatásban nem résztvevő lista")]
        AFBeiskAdCsopKaFtLi,
        [Description("Beiskolázási adatok csoportos felvitele - Oktatás résztvevő lista")]
        AFBeiskAdCsopKaOkReL,
        [Description("Beiskolázási adatok csoportos felvitele - Felvitel")]
        AFBeiskAdCsopKaFe,
        #endregion Beiskolázási adatok csoportos felvitele

        #region Biztonsági csoportba sorolás felülvizsgálatáról feljegyzés
        /// <summary>Biztonsági csoportba sorolás - Fogvatartott lista</summary>
        [Description("Biztonsági csoportba sorolás felülvizsgálatához vélemény - Fogvatartott lista")]
        AFBizCsopSorFtLi,
        #endregion Biztonsági csoportba sorolás felülvizsgálatáról feljegyzés

        #endregion Nevelés

        #region Biztonság

        #region Visszafogadás bf-ről, jogellenes távollétből

        /// <summary>Visszafogadás bf.-ről, jogellenes távollétből- Fogvatartott lista</summary>
        [Description("Visszafogadás bf.-ről, jogellenes távollétből- Fogvatartott lista")]
        AFVisszafogTavolFtLi,

        /// <summary>Visszafogadás bf.-ről, jogellenes távollétből- Visszafogadás</summary>
        [Description("Visszafogadás bf.-ről, jogellenes távollétből- Visszafogadás")]
        AFVisszafogTavolVf,

        /// <summary>Visszafogadás bf.-ről, jogellenes távollétből- Visszavonás</summary>
        [Description("Visszafogadás bf.-ről, jogellenes távollétből- Visszavonás")]
        AFVisszafogTavolVfv,

        #endregion Visszafogadás bf-ről, jogellenes távollétből

        #region Visszafogadás más távollétből

        /// <summary>Visszafogadás távollétből több fogvatartottnak - Fogvatartott lista</summary>
        [Description("Visszafogadás távollétből több fogvatartottnak - Fogvatartott lista")]
        AFVisszaTavolTobbFtL,

        /// <summary>Visszafogadás távollétből több fogvatartottnak - Felvitel</summary>
        [Description("Visszafogadás távollétből több fogvatartottnak - Felvitel")]
        AFVisszaTavolTobbFe,

        #endregion Visszafogadás más távollétből

        #region Elkülönítés

        #region Elkülönítés elrendelése



        /// <summary>
        /// Biztonsági elkülönítés elrendelése - Fogvatartott lista
        /// </summary>
        [Description("Biztonsági elkülönítés elrendelése - Fogvatartott lista")]
        AFElkElrZFtLi,

        /// <summary>
        /// Elkülönítés elrendelése - Fegyelmi ügy lista
        /// </summary>
        [Description("Elkülönítés elrendelése - Fegyelmi eseményen résztvevők listája")]
        AFElkElrFuLi,

        /// <summary>
        /// Elkülönítés elrendelése - Elkülönítés lista
        /// </summary>
        [Description("Elkülönítés elrendelése - Elkülönítés lista")]
        AFElkElrElkLi,

        /// <summary>
        /// Elkülönítés elrendelése - Elkülönítés részletek
        /// </summary>
        [Description("Elkülönítés elrendelése - Elkülönítés részletek")]
        AFElkElrRe,

        /// <summary>
        /// Elkülönítés elrendelése - Elkülönítés részletek
        /// </summary>
        [Description("Elkülönítés elrendelése - Elkülönítés felvitel")]
        AFElkElrFe,

        /// <summary>
        /// Elkülönítés elrendelése - Elkülönítés módosítás
        /// </summary>
        [Description("Elkülönítés elrendelése - Elkülönítés módosítás")]
        AFElkElrMo,

        /// <summary>
        /// Elkülönítés elrendelése - Elkülönítés törlés
        /// </summary>
        [Description("Elkülönítés elrendelése - Elkülönítés törlés")]
        AFElkElrTo,

        #endregion Elkülönítés elrendelése

        #region Elkülönítés végrehajtása

        /// <summary>
        /// Biztonsági elkülönítés végrehajtása - Fogvatartott lista
        /// </summary>
        [Description("Biztonsági elkülönítés végrehajtása - Fogvatartott lista")]
        AFElkVegZFtLi,

        /// <summary>
        /// Elkülönítés végrehajtása - Elkülönítés lista
        /// </summary>
        [Description("Elkülönítés végrehajtása - Elkülönítés lista")]
        AFElkVegElkLi,

        /// <summary>
        /// Elkülönítés végrehajtása - Elkülönítés részletek
        /// </summary>
        [Description("Elkülönítés végrehajtása - Elkülönítés részletek")]
        AFElkVegElkRe,

        /// <summary>
        /// Elkülönítés végrehajtása - Zárkába helyezés részletek
        /// </summary>
        [Description("Elkülönítés végrehajtása - Zárkába helyezés részletek")]
        AFElkVegZaHeRe,

        /// <summary>
        /// Elkülönítés végrehajtása - Behelyezés
        /// </summary>
        [Description("Elkülönítés végrehajtása - Behelyezés")]
        AFElkVegBehe,

        /// <summary>
        /// Elkülönítés végrehajtása - Előjegyzés
        /// </summary>
        [Description("Elkülönítés végrehajtása - Előjegyzés")]
        AFElkVegElo,

        /// <summary>
        /// Elkülönítés végrehajtása - Behelyezés előjegyzés visszavonás
        /// </summary>
        [Description("Elkülönítés végrehajtása - Behelyezés előjegyzés visszavonás")]
        AFElkVegEloVivo,

        /// <summary>
        /// Elkülönítés végrehajtása - Kihelyezés
        /// </summary>
        [Description("Elkülönítés végrehajtása - Kihelyezés")]
        AFElkVegKihe,

        #endregion Elkülönítés végrehajtása

        #region Elkülönítés megszüntetés elrendelése

        /// <summary>
        /// Biztonsági elkülönítés megszüntetése - Fogvatartott lista
        /// </summary>
        [Description("Biztonsági elkülönítés megszüntetése - Fogvatartott lista")]
        AFElkMegZFtLi,

        /// <summary>
        /// Elkülönítés megszüntetése - Megszüntetés elrendelése
        /// </summary>
        [Description("Elkülönítés megszüntetése - Megszüntetés elrendelése")]
        AFElkMegMeg,

        #endregion Elkülönítés megszüntetés elrendelése

        #region Elkülönítés felülvizsgálata

        /// <summary>
        /// Biztonsági elkülönítés felülvizsgálata - Fogvatartott lista
        /// </summary>
        [Description("Biztonsági elkülönítés felülvizsgálata - Fogvatartott lista")]
        AFElkFelZFtLi,

        /// <summary>
        /// Elkülönítés felülvizsgálata - Felülvizsgálat
        /// </summary>
        [Description("Elkülönítés felülvizsgálata - Felülvizsgálat")]
        AFElkFelFel,

        #endregion Elkülönítés felülvizsgálata

        #endregion Elkülönítés

        #region Eltávozás nyugtázása

        /// <summary>Eltavozás távozás rögzítés - Eltávozás lista</summary>
        [Description("Eltavozás távozás rögzítés - Eltávozás lista")]
        AFEltTavRoEltLi,

        /// <summary>Eltavozás távozás rögzítés - Eltávozás lista</summary>
        [Description("Eltavozás távozás rögzítés - Távozás rögzítése több fogvatartottnak")]
        AFEltTavRoEltMo,

        #endregion Eltávozás nyugtázása

        #region Bf. nyugtázása
        /// <summary>Büntetés-félbeszakítás távozás rögzítés több fogvatartottnak - Bf lista</summary>
        [Description("Büntetés-félbeszakítás távozás rögzítés több fogvatartottnak - Bf lista")]
        AFBuFTavToLi,
        /// <summary>Büntetés-félbeszakítás távozás rögzítés több fogvatartottnak - Felvitel</summary>
        [Description("Büntetés-félbeszakítás távozás rögzítés több fogvatartottnak - Felvitel")]
        AFBuFTavTF,
        #endregion Bf. nyugtázása

        #region Szabadítás nyugtázása
        /// <summary>Szabadítás rögzítése - Fogvatartott lista</summary>
        [Description("Szabadítás rögzítése - Fogvatartott lista")]
        AFSzabRFL,
        #endregion Szabadítás nyugtázása

        #region Engedélyhez nem kötött látogatás rögzítése

        /// <summary>Hivatalos látogatás - Fogvatartott lista</summary>
        [Description("Hivatalos látogatás - Fogvatartott lista")]
        AFHivLatKarbFL,

        /// <summary>Hivatalos látogatás - Kiválasztott kapcsolattartó tételei</summary>
        [Description("Hivatalos látogatás - Kiválasztott fogvatartott tételei")]
        AFHivLatKarbLL,

        /// <summary>Hivatalos látogatás - Kiválasztott tétel részletei</summary>
        [Description("Hivatalos látogatás- Kiválasztott tétel részletei")]
        AFHivLatKarbR,

        /// <summary>Hivatalos látogatás - Felvitel</summary>
        [Description("Hivatalos látogatás - Felvitel")]
        AFHivLatKarbF,

        /// <summary>Hivatalos látogatás - Módosítás</summary>
        [Description("Hivatalos látogatás - Módosítás")]
        AFHivLatKarbM,

        #endregion Engedélyhez nem kötött látogatás rögzítése

        #region Engedélyhez kötött látogatás rögzítése

        /// <summary>Engedélyezett látogatások - Látogatás lista</summary>
        [Description("Engedélyezett látogatások - Látogatás lista")]
        AFEngLatKarbLaLi,

        /// <summary>Engedélyezett látogatások - Látogatás részletei</summary>
        [Description("Engedélyezett látogatások - Látogatás részletei")]
        AFEngLatKarbLaRe,

        /// <summary>Engedélyezett látogatások - Látogatás megtörtént</summary>
        [Description("Engedélyezett látogatások - Látogatás megtörtént")]
        AFEngLatKarbLaMe,

        /// <summary>Engedélyezett látogatások - Látogatók listája</summary>
        [Description("Engedélyezett látogatások - Látogatók listája")]
        AFEngLatKarbLoLi,

        #endregion Engedélyhez kötött látogatás rögzítése

        #region Nyomozásra kiadás rögzítése
        /// <summary>Nyomozásra kiadás rögz. több fogv. - Pótnyomozas lista</summary>
        [Description("Nyomozásra kiadás rögz. több fogv. - Pótnyomozas lista")]
        AFNyoKiRTFoFtLi,

        /// <summary>Nyomozásra kiadás rögzítése - időpont megadás</summary>
        [Description("Nyomozásra kiadás rögzítése - időpont megadás")]
        AFNyoKiRTFel,
        #endregion Nyomozásra kiadás rögzítése

        #region Külső előállítás rögzítése több fogvatartottnak


        /// <summary>Külső előállítás - Lista</summary>
        [Description("Külső előállítás - Lista")]
        AFKuElLi,

        /// <summary>Külső előállítás - Előállítva</summary>
        [Description("Külső előállítás - Előállítva")]
        AFKuElMo,

        #endregion Külső előállítás rögzítése több fogvatartottnak

        #region Kényszerítő eszköz alkalmazás

        #region Kényszerítő eszköz alkalmazása - karbantartás

        /// <summary>
        /// Kényszerítő eszköz használat karbantartása - Fogvatartott lista
        /// </summary>
        [Description("Kényszerítő eszköz használat karbantartása - Fogvatartott lista")]
        AFKenyszEszkKaFtLi,

        /// <summary>
        /// Kényszerítő eszköz használat karbantartása - Egyéb ügy lista
        /// </summary>
        [Description("Kényszerítő eszköz használat karbantartása - Eszköz használat lista")]
        AFKenyszEszkKenyszLi,

        /// <summary>
        /// Kényszerítő eszköz használat karbantartása - Részletek
        /// </summary>
        [Description("Kényszerítő eszköz használat karbantartása - Részletek")]
        AFKenyszEszkKaRe,

        /// <summary>
        /// Kényszerítő eszköz használat karbantartása - Felvitel
        /// </summary>
        [Description("Kényszerítő eszköz használat karbantartása - Felvitel")]
        AFKenyszEszkKaFe,

        /// <summary>
        /// Kényszerítő eszköz használat karbantartása - Módosítás
        /// </summary>
        [Description("Kényszerítő eszköz használat karbantartása - Módosítás")]
        AFKenyszEszkKaMo,

        /// <summary>
        /// Kényszerítő eszköz használat karbantartása - Törlés
        /// </summary>
        [Description("Kényszerítő eszköz használat karbantartása - Törlés")]
        AFKenyszEszkKaTo,

        #endregion Kényszerítő eszköz alkalmazása - karbantartás

        #region Kényszerítő eszköz alkalmazása - bizt. ov. vélemény

        /// <summary>
        /// Kényszerítő eszköz alkalmazása – biztonsági osztályvezető véleménye - fogvatartott lista
        /// </summary>
        [Description("Kényszerítő eszköz alkalmazása – biztonsági osztályvezető véleménye - fogvatartott lista")]
        AFKeabovFl,

        /// <summary>
        /// Kényszerítő eszköz alkalmazása – biztonsági osztályvezető véleménye - kényszerítő eszköz lista
        /// </summary>
        [Description("Kényszerítő eszköz alkalmazása – biztonsági osztályvezető véleménye - kényszerítő eszköz lista")]
        AFKeabovKehl,

        /// <summary>
        /// Kényszerítő eszköz alkalmazása – biztonsági osztályvezető véleménye - részletek
        /// </summary>
        [Description("Kényszerítő eszköz alkalmazása – biztonsági osztályvezető véleménye - részletek")]
        AFKeabovR,

        /// <summary>
        /// Kényszerítő eszköz alkalmazása – biztonsági osztályvezető véleménye - vélemény rögzítése
        /// </summary>
        [Description("Kényszerítő eszköz alkalmazása – biztonsági osztályvezető véleménye - vélemény rögzítése")]
        AFKeabovVr,

        #endregion Kényszerítő eszköz alkalmazása - bizt. ov. vélemény


        #endregion Kényszerítő eszköz alkalmazás

        #endregion Biztonság

        #region Bef. Biz. feladatok

        #region Bef.Biz.elé állítás bizt.csop.felülvizsg.miatt
        /// <summary>Befogadási Bizottsági megjelenés előjegyzése több fogvatartottnak</summary>
        [Description("Befogadási Bizottsági megjelenés előjegyzése több fogvatartottnak")]
        AFBefBizMElToFogv,

        /// <summary>Biztonsági csoportba sorolás - Lista</summary>
        [Description("Biztonsági csoportba sorolás - Lista")]
        AFBizCsopSorLi,
        /// <summary>Biztonsági csoportba sorolás - Részletek</summary>
        [Description("Biztonsági csoportba sorolás - Részletek")]
        AFBizCsopSorRe,

        /// <summary>Biztonsági csoportba sorolás - Felvitel</summary>
        [Description("Biztonsági csoportba sorolás - Felvitel")]
        AFBizCsopSorFe,

        /// <summary>Biztonsági csoportba sorolás - Módosítás</summary>
        [Description("Biztonsági csoportba sorolás - Módosítás")]
        AFBizCsopSorM,

        /// <summary>Biztonsági csoportba sorolás - Törlés</summary>
        [Description("Biztonsági csoportba sorolás - Törlés")]
        AFBizCsopSorT,

        #endregion Bef.Biz.elé állítás bizt.csop.felülvizsg.miatt

        #region Rezsim kategória
        /// <summary>Rezsim kategóriába sorolás felülvizsgálatához vélemény - Fogvatartott lista</summary>
        [Description("Rezsim kategóriába sorolás felülvizsgálatához vélemény - Fogvatartott lista")]
        AFRezsKatSorFtLi,

        /// <summary>Rezsim kategóriába sorolás - Lista</summary>
        [Description("Rezsim kategóriába sorolás - Lista")]
        AFRezsKatSorLi,

        /// <summary>Rezsim kategóriába sorolás - Részletek</summary>
        [Description("Rezsim kategóriába sorolás - Részletek")]
        AFRezsKatSorRe,

        /// <summary>Rezsim kategóriába sorolás - Felvitel</summary>
        [Description("Rezsim kategóriába sorolás - Felvitel")]
        AFRezsKatSorFe,

        /// <summary>Rezsim kategóriába sorolás - Módosítás</summary>
        [Description("Rezsim kategóriába sorolás - Módosítás")]
        AFRezsKatSorM,

        /// <summary>Rezsim kategóriába sorolás - Törlés</summary>
        [Description("Rezsim kategóriába sorolás - Törlés")]
        AFRezsKatSorT,

        #endregion Rezsim kategória

        #region Biztonsági kockázat
        /// <summary>Biztonsági kockázatba sorolás felülvizsgálatához vélemény - Fogvatartott lista</summary>
        [Description("Biztonsági kockázatba sorolás felülvizsgálatához vélemény - Fogvatartott lista")]
        AFBiztKockSorFtLi,

        /// <summary>Biztonsági kockázatba sorolás - Lista</summary>
        [Description("Biztonsági kockázatba sorolás - Lista")]
        AFBiztKockSorLi,

        /// <summary>Biztonsági kockázatba sorolás - Részletek</summary>
        [Description("Biztonsági kockázatba sorolás - Részletek")]
        AFBiztKockSorRe,

        /// <summary>Biztonsági kockázatba sorolás - Felvitel</summary>
        [Description("Biztonsági kockázatba sorolás - Felvitel")]
        AFBiztKockSorFe,

        /// <summary>Biztonsági kockázatba sorolás - Módosítás</summary>
        [Description("Biztonsági kockázatba sorolás - Módosítás")]
        AFBiztKockSorM,

        /// <summary>Biztonsági kockázatba sorolás - Törlés</summary>
        [Description("Biztonsági kockázatba sorolás - Törlés")]
        AFBiztKockSorT,

        #endregion Biztonsági kockázat

        #region Befogadási Biztottsági ülés
        /// <summary>Befogadó bizottsági ülés - Fogvatartott lista</summary>
        [Description("Befogadó bizottsági ülés - Fogvatartott lista")]
        AFBeBiUFLi,

        /// <summary>Befogadó bizottsági ülés - Fogvatartott részletek</summary>
        [Description("Befogadó bizottsági ülés - Fogvatartott részletek")]
        AFBeBiUFRe,

        /// <summary>Befogadási bizottsági ülésen megjelenés oka - lista</summary>
        [Description("Befogadási bizottsági ülésen megjelenés oka - lista")]
        AFBeBiUlLi,

        /// <summary>Befogadó bizottsági megjelenés módosítása</summary>
        [Description("Befogadó bizottsági megjelenés módosítása")]
        AFBeBiUlMo,

        /// <summary>Befogadási bizottsági ülésen megjelenés részletek</summary>
        [Description("Befogadási bizottsági ülésen megjelenés részletek")]
        AFBeBiUlRe,
        #endregion Befogadási Biztottsági ülés

        #region Előjegyzés befogadási bizottsági megjelenésre

        /// <summary>
        /// Előjegyzés befogadási bizottsági megjelenésre - Fogvatartott lista
        /// </summary>
        [Description("Előjegyzés befogadási bizottsági megjelenésre - Fogvatartott lista")]
        AFEloBefBizMegFtLi,

        /// <summary>
        /// Előjegyzés befogadási bizottsági megjelenésre - Befogadó bizottsági megjelenés lista
        /// </summary>
        [Description("Előjegyzés befogadási bizottsági megjelenésre - Befogadó bizottsági megjelenés lista")]
        AFEloBefBizMegBBMLi,

        /// <summary>
        /// Előjegyzés befogadási bizottsági megjelenésre - Felvitel
        /// </summary>
        [Description("Előjegyzés befogadási bizottsági megjelenésre - Felvitel")]
        AFEloBefBizMegFe,

        /// <summary>
        /// Előjegyzés befogadási bizottsági megjelenésre - Felvitel több fogvatartottnak
        /// </summary>
        [Description("Előjegyzés befogadási bizottsági megjelenésre - Felvitel több fogvatartottnak")]
        AFEloBefBizMegTFtFe,

        /// <summary>
        /// Előjegyzés befogadási bizottsági megjelenésre - Részletek
        /// </summary>
        [Description("Előjegyzés befogadási bizottsági megjelenésre - Részletek")]
        AFEloBefBizMegRe,

        /// <summary>
        /// Előjegyzés befogadási bizottsági megjelenésre - Módosítás
        /// </summary>
        [Description("Előjegyzés befogadási bizottsági megjelenésre - Módosítás")]
        AFEloBefBizMegMo,

        /// <summary>
        /// Előjegyzés befogadási bizottsági megjelenésre - Törlés
        /// </summary>
        [Description("Előjegyzés befogadási bizottsági megjelenésre - Törlés")]
        AFEloBefBizMegTo,

        #endregion Előjegyzés befogadási bizottsági megjelenésre


        #region Törlés befogadási bizottsági ülésre kijelöltek névsorából

        /// <summary>
        /// Törlés befogadási bizottsági ülésre kijelöltek névsorából - Fogvatartott lista
        /// </summary>
        [Description("Törlés befogadási bizottsági ülésre kijelöltek névsorából - Fogvatartott lista")]
        AFTorlBefBizUlKiFtLi,

        #endregion Törlés befogadási bizottsági ülésre kijelöltek névsorából

        #region Gyógyító terápiás részleg adatok karbantartása
        [Description("Gyógyító terápiás részleg adatok karbantartása - Fogvatartott lista")]
        GyogyCsopHelyKaFtLi,
        [Description("Gyógyító terápiás részleg adatok karbantartása - Gyógyító terápiás részleg lista")]
        GyogyCsopHelyKaOkLi,
        [Description("Gyógyító terápiás részleg adatok karbantartása - Részletek")]
        GyogyCsopHelyKaRe,
        [Description("Gyógyító terápiás részleg adatok karbantartása - Felvitel")]
        GyogyCsopHelyKaFe,
        [Description("Gyógyító terápiás részleg adatok karbantartása - Módosítás")]
        GyogyCsopHelyKaMo,
        [Description("Gyógyító terápiás részleg adatok karbantartása - Törlés")]
        GyogyCsopHelyKaTo,
        #endregion Gyógyító terápiás részleg adatok karbantartása

        #region Drog prevenciós adatok karbantartása
        [Description("Drog-prevenciós részleg adatok karbantartása - Fogvatartott lista")]
        DrogCsopHelyKaFtLi,
        [Description("Drog-prevenciós részleg adatok karbantartása - Drog-prevenciós részleg lista")]
        DrogCsopHelyKaOkLi,
        [Description("Drog-prevenciós részleg adatok karbantartása - Részletek")]
        DrogCsopHelyKaRe,
        [Description("Drog-prevenciós részleg adatok karbantartása - Felvitel")]
        DrogCsopHelyKaFe,
        [Description("Drog-prevenciós részleg adatok karbantartása - Módosítás")]
        DrogCsopHelyKaMo,
        [Description("Drog-prevenciós részleg adatok karbantartása - Törlés")]
        DrogCsopHelyKaTo,

        #endregion Drog prevenciós adatok karbantartása

        #region Pszicho-szociális adatok karbantartása
        [Description("Pszicho-szociális részleg adatok karbantartása - Fogvatartott lista")]
        PsziCsopHelyKaFtLi,
        [Description("Pszicho-szociális részleg adatok karbantartása - Pszicho-szociális részleg lista")]
        PsziCsopHelyKaOkLi,
        [Description("Pszicho-szociális részleg adatok karbantartása - Részletek")]
        PsziCsopHelyKaRe,
        [Description("Pszicho-szociális részleg adatok karbantartása - Felvitel")]
        PsziCsopHelyKaFe,
        [Description("Pszicho-szociális részleg adatok karbantartása - Módosítás")]
        PsziCsopHelyKaMo,
        [Description("Pszicho-szociális részleg adatok karbantartása - Törlés")]
        PsziCsopHelyKaTo,

        #endregion Pszicho-szociális adatok karbantartása

        #endregion Bef. Biz. feladatok

        #region Elhelyezés

        #region Zárkába helyezés előjegyzés, behelyezés, kihelyezés
        /// <summary>Zárkába Helyezés - Behelyezés</summary>
        [Description("Zárkába Helyezés - Behelyezés")]
        AFZaHeBe,

        /// <summary>Zárkába helyezés - Fogvatartottak</summary>
        [Description("Zárkába helyezés - Fogvatartottak")]
        AFZaHeFoLi,

        /// <summary>Zárkába helyezés - Ágy csere másik fogvatartottal</summary>
        [Description("Zárkába helyezés - Ágy csere másik fogvatartottal")]
        AFZaHeCseFoLi,

        /// <summary>Zárkába Helyezés - Fogvatartott részletes adatai</summary>
        [Description("Zárkába Helyezés - Fogvatartott részletes adatai")]
        AFZaHeFoRe,

        /// <summary>Zárkába Helyezés - Intézeti objektumok</summary>
        [Description("Zárkába Helyezés - Intézeti objektumok")]
        AFZaHeIOLi,

        /// <summary>Zárkába helyezés - Körletek</summary>
        [Description("Zárkába helyezés - Körletek")]
        AFZaHeKoLi,

        /// <summary>Zárkába helyezések</summary>
        [Description("Zárkába helyezések")]
        AFZaHeLi,

        /// <summary>Zárkába helyezés részletek</summary>
        [Description("Zárkába helyezés részletek")]
        AFZaHeRe,

        /// <summary>Zárka helyezés - Fogvatarttottak ágyai</summary>
        [Description("Zárka helyezés - Fogvatarttottak ágyai")]
        AFZaHZAgLi,

        /// <summary>Zárkába helyezés - Zárkák</summary>
        [Description("Zárkába helyezés - Zárkák")]
        AFZaHeZaLi,
        #endregion Zárkába helyezés előjegyzés, behelyezés, kihelyezés

        #region Reintegrációs részlegbe helyezés
        /// <summary>Reintegrációs részlegbe helyezés - Fogvatartottak</summary>
        [Description("Reintegrációs részlegbe helyezés - Fogvatartottak")]
        AFNevCsopHeFoLi,

        /// <summary>Reintegrációs részlegbe helyezés - Lista</summary>
        [Description("Reintegrációs részlegbe helyezés - Lista")]
        AFNevCsopHeLi,

        /// <summary>Reintegrációs részlegbe helyezés - Részletek</summary>
        [Description("Reintegrációs részlegbe helyezés - Részletek")]
        AFNevCsopHeRe,

        /// <summary>Reintegrációs részlegbe helyezés - Behelyezés</summary>
        [Description("Reintegrációs részlegbe helyezés - Behelyezés")]
        AFNevCsopHeBe,
        #endregion Reintegrációs részlegbe helyezés

        #region Behelyezés nyugtázás, előjegyzés törlés (csop)
        /// <summary>Zárkába áthelyezés jóváhagyása - Fogvatartottak</summary>
        [Description("Zárkába áthelyezés jóváhagyása  - Fogvatartottak")]
        AFZarAthJovFoLi,

        /// <summary>Zárkába áthelyezés jóváhagyása - Részletek</summary>
        [Description("Zárkába áthelyezés jóváhagyása - Részletek")]
        AFZarAthJovRe,

        /// <summary>Zárkába áthelyezés jóváhagyása - Felvitel</summary>
        [Description("Zárkába áthelyezés jóváhagyása - Felvitel")]
        AFZarAthJovFe,
        #endregion Behelyezés nyugtázás, előjegyzés törlés (csop)

        #region Különleges bizt. elhelyezés felülvizsgálata

        /// <summary>Különleges biztonságú elhelyezés karbantartása - Fogvatartott lista</summary>
        [Description("Különleges biztonságú elhelyezés karbantartása - Fogvatartott lista")]
        AFKulBiztElhKaFtLi,

        /// <summary>Különleges biztonságú elhelyezés karbantartása - Különleges bizt. elh. lista</summary>
        [Description("Különleges biztonságú elhelyezés karbantartása - Különleges bizt. elh. lista")]
        AFKulBiztElhKaKbLi,

        /// <summary>Különleges biztonságú elhelyezés karbantartása - Felvitel</summary>
        [Description("Különleges biztonságú elhelyezés karbantartása - Felvitel")]
        AFKulBiztElhKaFe,

        /// <summary>Különleges biztonságú elhelyezés karbantartása - Módosítás</summary>
        [Description("Különleges biztonságú elhelyezés karbantartása - Módosítás")]
        AFKulBiztElhKaMo,

        /// <summary>Különleges biztonságú elhelyezés karbantartása - Törlés</summary>
        [Description("Különleges biztonságú elhelyezés karbantartása - Törlés")]
        AFKulBiztElhKaTo,

        /// <summary>Különleges biztonságú elhelyezés karbantartása - Részletek</summary>
        [Description("Különleges biztonságú elhelyezés karbantartása - Részletek")]
        AFKulBiztElhKaRe,

        #endregion Különleges bizt. elhelyezés felülvizsgálata

        #endregion Elhelyezés

        #region Egyéb szakter. feladatok

        #region Bv. ügy véleményezése
        /// <summary>Büntetés végrehajtási ügy véleményezése - Ügy lista</summary>
        [Description("Büntetés végrehajtási ügy véleményezése - Ügy lista")]
        AFBVUgyVelBVUL,

        /// <summary>Büntetés végrehajtási ügy véleményezése - Módosítás</summary>
        [Description("Büntetés végrehajtási ügy véleményezése - Módosítás")]
        AFBVUgyVelBVUM,
        #endregion Bv. ügy véleményezése

        #region Előállítás

        #region Előállítás előjegyzése
        /// <summary>Előállítás előjegyzés - Fogvatartott lista</summary>
        [Description("Előállítás előjegyzés - Fogvatartott lista")]
        AFElElFtLi,

        /// <summary>Előállítás előjegyzés - Fogvatartott előállításai lista</summary>
        [Description("Előállítás előjegyzés - Fogvatartott előállításai lista")]
        AFElElElLi,

        /// <summary>Előállítás előjegyzés részletek</summary>
        [Description("Előállítás előjegyzés részletek")]
        AFElElRe,

        /// <summary>Előállítás előjegyzés felvitel</summary>
        [Description("Előállítás előjegyzés felvitel")]
        AFElElFe,

        /// <summary>Előállítás előjegyzés módosítás</summary>
        [Description("Előállítás előjegyzés módosítás")]
        AFElElMo,

        /// <summary>Előállítás előjegyzés törlés</summary>
        [Description("Előállítás előjegyzés törlés")]
        AFElElTo,
        #endregion Előállítás előjegyzése



        #endregion Előállítás

        #region Orvosi rendelésre jelentkezés/visszarendelés

        /// <summary>Fogvatartott orvosi rendelései - Fogvatartott lista</summary>
        [Description("Fogvatartott orvosi rendelései - Fogvatartott lista")]
        AFOrvRendFL,

        /// <summary>Fogvatartott orvosi rendelései- Kiválasztott fogvatartott tételei</summary>
        [Description("Fogvatartott orvosi rendelései - Kiválasztott fogvatartott tételei")]
        AFOrvRendNL,

        /// <summary>Fogvatartott orvosi rendelései - Kiválasztott tétel részletei</summary>
        [Description("Fogvatartott orvosi rendelései - Kiválasztott tétel részletei")]
        AFOrvRendR,

        /// <summary>Fogvatartott orvosi rendelései - Felvitel</summary>
        [Description("Fogvatartott orvosi rendelései - Felvitel")]
        AFOrvRendF,

        /// <summary>Fogvatartott orvosi rendelései - Módosítás</summary>
        [Description("Fogvatartott orvosi rendelései - Módosítás")]
        AFOrvRendM,

        /// <summary>Fogvatartott orvosi rendelései - Törlés</summary>
        [Description("Fogvatartott orvosi rendelései - Törlés")]
        AFOrvRendT,
        #endregion Orvosi rendelésre jelentkezés/visszarendelés

        #region Kérelem, panasz szakterületi intézése

        #region Kérelem, panasz véleményezése, döntés

        /// <summary>
        /// Kérelem panasz intézése - Fogvatartott lista
        /// </summary>
        [Description("Kérelem panasz intézése - Fogvatartott lista")]
        AFKPIFL,

        /// <summary>
        /// Kérelem panasz intézése - Kérelem panasz lista
        /// </summary>
        [Description("Kérelem panasz intézése - Kérelem panasz lista")]
        AFKPIKPL,

        /// <summary>
        /// Kérelem panasz intézése - Kérelem panasz részletek
        /// </summary>
        [Description("Kérelem panasz intézése - Kérelem panasz részletek")]
        AFKPIKPR,

        /// <summary>
        /// Kérelem panasz intézése - Kérelem panasz módosítása
        /// </summary>
        [Description("Kérelem panasz intézése - Kérelem panasz módosítás")]
        AFKPIKPM,
        /// <summary>
        /// Kérelem panasz intézése - Kérelem panasz módosítása
        /// </summary>
        [Description("Kérelem panasz intézése - Kérelem panasz véleményezés/döntés")]
        AFKPIKPD,
        /// <summary>
        /// Kérelem panasz intézése - Kérelem panasz módosítása
        /// </summary>
        [Description("Kérelem panasz intézése - Kérelem panasz Kihirdetése")]
        AFKPIKPK,
        #endregion Kérelem, panasz véleményezése, döntés

        #endregion Kérelem, panasz szakterületi intézése

        #region Egyéb távozások

        #region Szökés
        [Description("Szökések rögzítése - Fogvatartott lista")]
        AFSzokesFtLi,
        [Description("Szökések rögzítése - Felvitel")]
        AFSzokesFe,
        [Description("Szökések rögzítése - Módosítás")]
        AFSzokesMo,
        [Description("Szökések rögzítése - Törlés")]
        AFSzokesTo,

        #endregion Szökés

        #region Kiadás nyomozás céljából

        /// <summary>Kiadás nyomozás céljából - Fogvatartott lista</summary>
        [Description("Kiadás nyomozás céljából - Fogvatartott lista")]
        AFNyoKiElKaFtLi,

        /// <summary>Kiadás nyomozás céljából - Előjegyzés lista</summary>
        [Description("Kiadás nyomozás céljából - Előjegyzés lista")]
        AFNyoKiElKaNyoLi,

        /// <summary>Kiadás nyomozás céljából - Felvitel</summary>
        [Description("Kiadás nyomozás céljából - Felvitel")]
        AFNyoKiElKaFe,

        /// <summary>Nyomozásra kiadás előjegyzés - Módosítás</summary>
        [Description("Kiadás nyomozás céljából - Módosítás")]
        AFNyoKiElKaMo,

        /// <summary>Nyomozásra kiadás előjegyzés - Törlés</summary>
        [Description("Kiadás nyomozás céljából - Törlés")]
        AFNyoKiElKaTo,

        /// <summary>Kiadás nyomozás céljából - Részletek</summary>
        [Description("Kiadás nyomozás céljából - Részletek")]
        AFNyoKiElKaRe,

        #endregion Kiadás nyomozás céljából

        #region Jogellenes távollétbe helyezés

        /// <summary>Jogellenes távollétbe helyezés több fogvatartottnak - Fogvatartott lista</summary>
        [Description("Jogellenes távollétbe helyezés több fogvatartottnak - Fogvatartott lista")]
        AFJogellTavTobbFtLi,

        /// <summary>Jogellenes távollétbe helyezés több fogvatartottnak - Jogellenes távollétbe helyezés</summary>
        [Description("Jogellenes távollétbe helyezés több fogvatartottnak - Jogellenes távollétbe helyezés")]
        AFJogellTavTobbTavHe,

        /// <summary>Jogellenes távollétbe helyezés több fogvatartottnak - Jogellenes távollétbe helyezés visszavonása</summary>
        [Description("Jogellenes távollétbe helyezés több fogvatartottnak - Jogellenes távollétbe helyezés visszavonása")]
        AFJogellTavTobbTavVi,

        #endregion Jogellenes távollétbe helyezés

        #region Egyéb távozások
        /// <summary>Egyéb távozások rögzítése - Fogvatartott lista</summary>
        [Description("Egyéb távozások rögzítése - Fogvatartott lista")]
        AFEgyTavKaFtLi,

        /// <summary>Egyéb távozások rögzítése - Felvitel</summary>
        [Description("Egyéb távozások rögzítése - Felvitel")]
        AFEgyTavKaFe,

        /// <summary>Egyéb távozások rögzítése - Módosítás</summary>
        [Description("Egyéb távozások rögzítése - Módosítás")]
        AFEgyTavKaMo,

        /// <summary>Egyéb távozások rögzítése - Törlés</summary>
        [Description("Egyéb távozások rögzítése - Törlés")]
        AFEgyTavKaTo,

        /// <summary>Egyéb távozások rögzítése - Részletek</summary>
        [Description("Egyéb távozások rögzítése - Részletek")]
        AFEgyTavKaRe,
        #endregion Egyéb távozások

        #endregion Egyéb távozások

        #region Jutalmazás

        #region Jutalmazási javaslat készítése

        /// <summary>
        /// Jutalmazási javaslat készítése - Fogvatartott lista
        /// </summary>
        [Description("Jutalmazási javaslat készítése - Fogvatartott lista")]
        AFJutJavKeFtLi,

        /// <summary>
        /// Jutalmazási javaslat készítése - Jutalmazás lista
        /// </summary>
        [Description("Jutalmazási javaslat készítése - Jutalmazás lista")]
        AFJutJavKeJutLi,

        /// <summary>
        /// Jutalmazási javaslat készítése - Felvitel
        /// </summary>
        [Description("Jutalmazási javaslat készítése - Felvitel")]
        AFJutJavKeFe,

        /// <summary>
        /// Jutalmazási javaslat készítése - Módosítás
        /// </summary>
        [Description("Jutalmazási javaslat készítése - Módosítás")]
        AFJutJavKeMo,

        /// <summary>
        /// Jutalmazási javaslat készítése - Törlés
        /// </summary>
        [Description("Jutalmazási javaslat készítése - Törlés")]
        AFJutJavKeTo,

        /// <summary>
        /// Jutalmazási javaslat készítése - Részletek
        /// </summary>
        [Description("Jutalmazási javaslat készítése - Részletek")]
        AFJutJavKeRe,

        /// <summary>
        /// Jutalmazási javaslat készítése - Elkülönítés
        /// </summary>
        [Description("Jutalmazási javaslat készítése - Elkülönítés")]
        AFJutJavKeElk,

        #endregion Jutalmazási javaslat készítése

        #region Jutalmazási javaslat több fogvatartottnak

        /// <summary>
        /// Jutalmazási javaslat készítése több fogvatartottnak - Fogvatartott lista
        /// </summary>
        [Description("Jutalmazási javaslat készítése több fogvatartottnak - Fogvatartott lista")]
        AFJutJavKeToFtFtLi,

        /// <summary>
        /// Jutalmazási javaslat készítése több fogvatartottnak - Felvitel
        /// </summary>
        [Description("Jutalmazási javaslat készítése több fogvatartottnak - Felvitel")]
        AFJutJavKeToFtFe,

        #endregion Jutalmazási javaslat több fogvatartottnak

        #region Jutalmazási javaslat véleményezése, elbírálása

        /// <summary>
        /// Jutalmazás intézése - Fogvatartott lista
        /// </summary>
        [Description("Jutalmazás intézése - Fogvatartott lista")]
        AFJutIntFtLi,

        /// <summary>
        /// Jutalmazás intézése - Jutalmazás lista
        /// </summary>
        [Description("Jutalmazás intézése - Jutalmazás lista")]
        AFJutIntJutLi,

        /// <summary>
        /// Jutalmazás intézése - Felvitel
        /// </summary>
        [Description("Jutalmazás intézése - Felvitel")]
        AFJutIntFe,

        /// <summary>
        /// Jutalmazás intézése - Részletek
        /// </summary>
        [Description("Jutalmazás intézése - Részletek")]
        AFJutIntRe,

        /// <summary>
        /// Jutalmazás intézése - Véleményezés, döntés
        /// </summary>
        [Description("Jutalmazás intézése - Véleményezés, döntés")]
        AFJutIntMo,

        /// <summary>
        /// Jutalmazás intézése - Törlés
        /// </summary>
        [Description("Jutalmazás intézése - Törlés")]
        AFJutIntTo,

        /// <summary>
        /// Jutalmazás intézése - Összevonás
        /// </summary>
        [Description("Jutalmazás intézése - Összevonás")]
        AFJutIntOssVon,

        /// <summary>
        /// Jutalmazás intézése - Fenyítés törlése
        /// </summary>
        [Description("Jutalmazás intézése - Fenyítés törlése")]
        AFJutIntFeTo,

        /// <summary>
        /// Jutalmazás intézése - Belevont jutalmak
        /// </summary>
        [Description("Jutalmazás intézése - Belevont jutalmak")]
        AFJutIntBeVoJutLi,

        #endregion Jutalmazási javaslat véleményezése, elbírálása

        #region Szakterületek értesítése jutalmazásról

        /// <summary>
        /// Szakterületek értesítése jutalmazásról - Jutalmazás lista
        /// </summary>
        [Description("Szakterületek értesítése jutalmazásról - Jutalmazás lista")]
        AFSztErtJutJutLi,

        /// <summary>
        /// Szakterületek értesítése jutalmazásról - Részletek
        /// </summary>
        [Description("Szakterületek értesítése jutalmazásról - Részletek")]
        AFSztErtJutRe,

        /// <summary>
        /// Szakterületek értesítése jutalmazásról - Módosítás
        /// </summary>
        [Description("Szakterületek értesítése jutalmazásról - Módosítás")]
        AFSztErtJutMo,

        /// <summary>
        /// Szakterületek értesítése jutalmazásról - Összevont jutalmak lista
        /// </summary>
        [Description("Szakterületek értesítése jutalmazásról - Összevont jutalmak lista")]
        AFSztErtJutOssVonJut,

        /// <summary>
        /// Szakterületek értesítése jutalmazásról - További kiétkezés növelés jutalmak megjelenítése
        /// </summary>
        [Description("Szakterületek értesítése jutalmazásról - További kiétkezés növelés jutalmak megjelenítése")]
        AFSztErtJutEtkNovJut,

        /// <summary>
        /// Szakterületek értesítése jutalmazásról - Kiétkezés csökkentés fenyítések megjelenítése
        /// </summary>
        [Description("Szakterületek értesítése jutalmazásról - Kiétkezés csökkentés fenyítések megjelenítése")]
        AFSztErtJutEtkCsoFe,

        #endregion Szakterületek értesítése jutalmazásról

        #region Engedélyezett, elutasított jutalmak javítása

        /// <summary>
        /// Engedélyezett, elutasított jutalom javítása - Jutalmazás lista
        /// </summary>
        [Description("Engedélyezett, elutasított jutalom javítása - Jutalmazás lista")]
        AFEngEluJutJavJutLi,

        /// <summary>
        /// Engedélyezett, elutasított jutalom javítása - Részletek
        /// </summary>
        [Description("Engedélyezett, elutasított jutalom javítása - Részletek")]
        AFEngEluJutJavRe,

        /// <summary>
        /// Engedélyezett, elutasított jutalom javítása - Módosítás
        /// </summary>
        [Description("Engedélyezett, elutasított jutalom javítása - Módosítás")]
        AFEngEluJutJavMo,

        #endregion Engedélyezett, elutasított jutalmak javítása

        #region Engedélyezett jutalmak kihirdetése

        /// <summary>
        /// Engedélyezett jutalmak kihirdetése - Jutalmazás lista
        /// </summary>
        [Description("Engedélyezett jutalmak kihirdetése - Jutalmazás lista")]
        AFEngJutKihJutLi,

        /// <summary>
        /// Engedélyezett jutalmak kihirdetése - Kihirdetés dátumának rögzítése
        /// </summary>
        [Description("Engedélyezett jutalmak kihirdetése - Kihirdetés dátumának rögzítése")]
        AFEngJutKihRo,

        #endregion Engedélyezett jutalmak kihirdetése

        #endregion Jutalmazás

        #region Fegyelmi eljárás

        #region Fegyelmi esemény, résztvevői, eljárás indítása

        /// <summary>
        /// Fegyelmi események karbantartása - Fegyelmi események listája
        /// </summary>
        [Description("Fegyelmi események karbantartása - Fegyelmi események listája")]
        AFRekReL,

        /// <summary>
        /// Fegyelmi események karbantartása - Fegyelmi esemény felvitel
        /// </summary>
        [Description("Fegyelmi események karbantartása - Fegyelmi esemény felvitel")]
        AFRekReF,

        /// <summary>
        /// Fegyelmi események karbantartása - Fegyelmi esemény részletek
        /// </summary>
        [Description("Fegyelmi események karbantartása - Fegyelmi esemény részletek")]
        AFRekReR,

        /// <summary>
        /// Fegyelmi események karbantartása - Fegyelmi esemény módosítás
        /// </summary>
        [Description("Fegyelmi események karbantartása - Fegyelmi esemény módosítás")]
        AFRekReM,

        /// <summary>
        /// Fegyelmi események karbantartása - Fegyelmi eseményen résztvevők listája
        /// </summary>
        [Description("Fegyelmi események karbantartása - Fegyelmi eseményen résztvevők listája")]
        AFRekRerL,

        /// <summary>
        /// Fegyelmi események karbantartása - Fegyelmi eseményen résztvevő hozzáadása
        /// </summary>
        [Description("Fegyelmi események karbantartása - Fegyelmi eseményen résztvevő hozzáadása")]
        AFRekRerF,

        /// <summary>
        /// Fegyelmi események karbantartása - Fegyelmi eseményen résztvevő módosítás
        /// </summary>
        [Description("Fegyelmi események karbantartása - Fegyelmi eseményen résztvevő módosítás")]
        AFRekRerM,

        /// <summary>
        /// Rendkívüli események karbantartása - Fogvatartott lista
        /// </summary>
        [Description("Fegyelmi események karbantartása - Fogvatartott lista")]
        AFRekFL,

        #endregion Fegyelmi esemény, résztvevői, eljárás indítása

        #region Fegyelmi elkülönítés

        #region Fegyelmi elkülönítés elrendelése
        /// <summary>
        /// Fegyelmi elkülönítés elrendelése - Fogvatartott lista
        /// </summary>
        [Description("Fegyelmi elkülönítés elrendelése - Fogvatartott lista")]
        AFElkElrEFtLi,
        #endregion Fegyelmi elkülönítés elrendelése

        #region Fegyelmi elkülönítés végrehajtása
        /// <summary>
        /// Fegyelmi elkülönítés végrehajtása - Fogvatartott lista
        /// </summary>
        [Description("Fegyelmi elkülönítés végrehajtása - Fogvatartott lista")]
        AFElkVegEFtLi,
        #endregion Fegyelmi elkülönítés végrehajtása

        #region Fegyelmi elkülön. megszüntetés elrend.
        /// <summary>
        /// Fegyelmi elkülönítés megszüntetése - Fogvatartott lista
        /// </summary>
        [Description("Fegyelmi elkülönítés megszüntetése - Fogvatartott lista")]
        AFElkMegEFtLi,
        #endregion Fegyelmi elkülön. megszüntetés elrend.

        #region Fegyelmi elkülönítés felülvizsgálata
        /// <summary>
        /// Fegyelmi elkülönítés felülvizsgálata - Fogvatartott lista
        /// </summary>
        [Description("Fegyelmi elkülönítés felülvizsgálata - Fogvatartott lista")]
        AFElkFelEFtLi,
        #endregion Fegyelmi elkülönítés felülvizsgálata

        #endregion Fegyelmi elkülönítés

        #region Döntés fegyelmi ügyről

        /// <summary>
        /// Döntés a fegyelmi ügyről - Fegyelmi ügy lista
        /// </summary>
        [Description("Döntés a fegyelmi ügyről - Fegyelmi ügy lista")]
        AFDFuFuL,

        /// <summary>
        /// Döntés a fegyelmi ügyről - Fegyelmi ügy módosítása
        /// </summary>
        [Description("Döntés a fegyelmi ügyről - Fegyelmi ügy módosítása")]
        AFDFuFuM,

        #endregion Döntés fegyelmi ügyről

        #region Jogi képviseletre vonatkozó adat megadása

        /// <summary>
        /// Jogi képviselet megadása - Fogvatartott lista
        /// </summary>
        [Description("Jogi képviselet megadása - Fogvatartott lista")]
        AFJkmFL,

        /// <summary>
        /// Jogi képviselet megadása - Fegyelmi ügy lista
        /// </summary>
        [Description("Jogi képviselet megadása - Fegyelmi ügy lista")]
        AFJkmFuL,

        /// <summary>
        /// Jogi képviselet megadása - Fegyelmi ügy módosítás
        /// </summary>
        [Description("Jogi képviselet megadása - Fegyelmi ügy módosítás")]
        AFJkmFuM,

        #endregion Jogi képviseletre vonatkozó adat megadása

        #region Fegyelmi tárgyalás előkészítése

        /// <summary>
        /// Fegyelmi tárgyalás előkészítése - Fogvatartott lista
        /// </summary>
        [Description("Fegyelmi tárgyalás előkészítése - Fogvatartott lista")]
        AFFteFl,

        /// <summary>
        /// Fegyelmi tárgyalás előkészítése - Fegyelmi ügy lista
        /// </summary>
        [Description("Fegyelmi tárgyalás előkészítése - Fegyelmi ügy lista")]
        AFFteFul,

        /// <summary>
        /// Fegyelmi tárgyalás előkészítése - Tárgyalás időpontjának kitűzése
        /// </summary>
        [Description("Fegyelmi tárgyalás előkészítése - Tárgyalás időpontjának kitűzése")]
        AFFteTik,

        /// <summary>
        /// Fegyelmi tárgyalás előkészítése - Eljárás megszüntetése
        /// </summary>
        [Description("Fegyelmi tárgyalás előkészítése - Eljárás megszüntetése")]
        AFFteEm,

        /// <summary>
        /// Fegyelmi tárgyalás előkészítése - Eljárás megszüntetése
        /// </summary>
        [Description("Fegyelmi tárgyalás előkészítése - Eljárás felfüggesztése")]
        AFFteEF,
        /// <summary>
        /// Fegyelmi tárgyalás előkészítése - Javaslat elbíráslása
        /// </summary>
        [Description("Fegyelmi tárgyalás előkészítése - Javaslat elbíráslása")]
        AFFteJe,
        /// <summary>
        /// Fegyelmi tárgyalás előkészítése - Fegyelmi ügy részletek
        /// </summary>
        [Description("Fegyelmi tárgyalás előkészítése - Fegyelmi ügy részletek")]
        AFFteRe,
        /// <summary>
        /// Fegyelmi tárgyalás előkészítése - Új határidő kitűzése
        /// </summary>
        [Description("Fegyelmi tárgyalás előkészítése - Új határidő kitűzése")]
        AFFteUhk,

        /// <summary>
        /// Fegyelmi tárgyalás előkészítése - Felfüggesztett eljárás folytatása
        /// </summary>
        [Description("Fegyelmi tárgyalás előkészítése - Felfüggesztett eljárás folytatása")]
        AFFteFef,
        /// <summary>
        /// Fegyelmi tárgyalás előkészítése - Rendkivüli esemény részletek
        /// </summary>
        [Description("Fegyelmi tárgyalás előkészítése - Rendkivüli esemény részletek")]
        AFFteReRe,
        #endregion Fegyelmi tárgyalás előkészítése

        #region Fegyelmi ügy kivizsgálása

        /// <summary>
        /// Fegyelmi ügyek kivizsgálása - Fogvatartott lista
        /// </summary>
        [Description("Fegyelmi ügyek kivizsgálása - Fogvatartott lista")]
        AFFukFL,

        /// <summary>
        /// Fegyelmi ügyek kivizsgálása - Fegyelmi ügy lista
        /// </summary>
        [Description("Fegyelmi ügyek kivizsgálása - Fegyelmi ügy lista")]
        AFFukFuL,

        /// <summary>
        /// Fegyelmi ügyek kivizsgálása - Fegyelmi ügy részletek
        /// </summary>
        [Description("Fegyelmi ügyek kivizsgálása - Fegyelmi ügy részletek")]
        AFFukFuR,

        /// <summary>
        /// Fegyelmi ügyek kivizsgálása - Javaslat új határidő, felfüggesztésre
        /// </summary>
        [Description("Fegyelmi ügyek kivizsgálása - Javaslat új határidő, felfüggesztésre")]
        AFFukJuhF,

        /// <summary>
        /// Fegyelmi ügyek kivizsgálása - Kivizsgálás rögzítése
        /// </summary>
        [Description("Fegyelmi ügyek kivizsgálása - Kivizsgálás rögzítése")]
        AFfukKr,

        /// <summary>
        /// Fegyelmi ügyek kivizsgálása - Jegyzőkönyv nyomtatása
        /// </summary>
        [Description("Fegyelmi ügyek kivizsgálása - Jegyzőkönyv nyomtatása")]
        AFfukJNy,

        #endregion Fegyelmi ügy kivizsgálása

        #region Fegyelmi ügy szakterületi véleményezése

        /// <summary>
        /// Fegyelmi ügy szakterületi véleményezése - fogvatartott lista
        /// </summary>
        [Description("Fegyelmi ügy szakterületi véleményezése - fogvatartott lista")]
        AFFuszvFl,

        /// <summary>
        /// Fegyelmi ügy szakterületi véleményezése - fegyelmi ügy lista
        /// </summary>
        [Description("Fegyelmi ügy szakterületi véleményezése - fegyelmi ügy lista")]
        AFFuszvFul,

        /// <summary>
        /// Fegyelmi ügy szakterületi véleményezése - véleményezés
        /// </summary>
        [Description("Fegyelmi ügy szakterületi véleményezése - véleményezés")]
        AFFuszvV,

        #endregion Fegyelmi ügy szakterületi véleményezése

        #region Fegyelmi tárgyalás I. fokon
        /// <summary>
        /// Fegyelmi eljárás elsőfokon - Fogvatartott lista
        /// </summary>
        [Description("Fegyelmi eljárás elsőfokon - Fogvatartott lista")]
        AFFeeFl,

        /// <summary>
        /// Fegyelmi eljárás elsőfokon - Fegyelmi ügy lista
        /// </summary>
        [Description("Fegyelmi eljárás elsőfokon - Fegyelmi ügy lista")]
        AFFeeFuL,

        /// <summary>
        /// Fegyelmi eljárás elsőfokon - Eljárás megszüntetése
        /// </summary>
        [Description("Fegyelmi eljárás elsőfokon - Eljárás megszüntetése")]
        AFFeeEM,

        /// <summary>
        /// Fegyelmi eljárás elsőfokon - Feddés
        /// </summary>
        [Description("Fegyelmi eljárás elsőfokon - Feddés")]
        AFFeeF,

        /// <summary>
        /// Fegyelmi eljárás elsőfokon - Kiétkezés csökkentése
        /// </summary>
        [Description("Fegyelmi eljárás elsőfokon - Kiétkezés csökkentése")]
        AFFeeKCs,

        /// <summary>
        /// Fegyelmi eljárás elsőfokon - Magánelzárás
        /// </summary>
        [Description("Fegyelmi eljárás elsőfokon - Magánelzárás")]
        AFFeeM,

        /// <summary>
        /// Fegyelmi eljárás elsőfokon - Összevonás
        /// </summary>
        [Description("Fegyelmi eljárás elsőfokon - Összevonás")]
        AFFeeO,

        /// <summary>
        /// Fegyelmi eljárás elsőfokon - Fegyelmi ügy részletek
        /// </summary>
        [Description("Fegyelmi eljárás elsőfokon - Fegyelmi ügy részletek")]
        AFFeeR,

        /// <summary>
        /// Fegyelmi eljárás elsőfokon - Rendkivüli esemény részletek
        /// </summary>
        [Description("Fegyelmi eljárás elsőfokon - Rendkivüli esemény részletek")]
        AFFeeReR,
        #endregion Fegyelmi tárgyalás I. fokon

        #region Fegyelmi tárgyalás II. fokon

        /// <summary>
        /// Fegyelmi eljárás másodfokon - Fogvatartott lista
        /// </summary>
        [Description("Fegyelmi eljárás másodfokon - Fogvatartott lista")]
        AFFemFL,

        /// <summary>
        /// Fegyelmi eljárás másodfokon - Fegyelmi ügy lista
        /// </summary>
        [Description("Fegyelmi eljárás másodfokon - Fegyelmi ügy lista")]
        AFFemFuL,

        /// <summary>
        /// Fegyelmi eljárás másodfokon - Eljárás megszüntetése
        /// </summary>
        [Description("Fegyelmi eljárás másodfokon - Eljárás megszüntetése")]
        AFFemEm,

        /// <summary>
        /// Fegyelmi eljárás másodfokon - Eljárás megszüntetése, új eljárás indítása
        /// </summary>
        [Description("Fegyelmi eljárás másodfokon - Eljárás megszüntetése, új eljárás indítása")]
        AFFemEmUe,

        /// <summary>
        /// Fegyelmi eljárás másodfokon - Elsőfokú döntés helybenhagyása
        /// </summary>
        [Description("Fegyelmi eljárás másodfokon - Elsőfokú döntés helybenhagyása")]
        AFFemEdH,

        /// <summary>
        /// Fegyelmi eljárás másodfokon - Elsőfokú döntés megváltoztatása
        /// </summary>
        [Description("Fegyelmi eljárás másodfokon - Elsőfokú döntés megváltoztatása")]
        AFFemEdM,

        /// <summary>
        /// Fegyelmi eljárás másodfokon - Fegyelmi ügy részletek
        /// </summary>
        [Description("Fegyelmi eljárás másodfokon - Fegyelmi ügy részletek")]
        AFFemEdR,

        /// <summary>
        /// Fegyelmi eljárás másodfokon - Rendkivüli esemény részletek
        /// </summary>
        [Description("Fegyelmi eljárás másodfokon - Rendkivüli esemény részletek")]
        AFFemEdReR,
        #endregion Fegyelmi tárgyalás II. fokon

        #region Fenyítés végrehajtása

        #region Fenyítés elévülése

        /// <summary>
        /// Fenyítés elévülése - Fegyelmi lista
        /// </summary>
        [Description("Fenyítés elévülése - Fegyelmi lista")]
        AFFeFl,

        /// <summary>
        /// Fenyítés elévülése - Elévültnek nyilvánítás
        /// </summary>
        [Description("Fenyítés elévülése - Elévültnek nyilvánítás")]
        AFFeEny,

        /// <summary>
        /// Fenyítés elévülése - Fenyítés részletek
        /// </summary>
        [Description("Fenyítés elévülése - Fenyítés részletek")]
        AFFeEFeR,

        #endregion Fenyítés elévülése

        #region Kiétkezés csökkentés végrehajtása

        /// <summary>
        /// Fenyítés - kiétkeztetés csökkentés - Fegyelmi lista
        /// </summary>
        [Description("Fenyítés - kiétkeztetés csökkentés - fegyelmi lista")]
        AFFKcsFfl,

        /// <summary>
        /// Fenyítés - kiétkeztetés csökkentés - Fenyítés részletek
        /// </summary>
        [Description("Fenyítés - kiétkeztetés csökkentés - Fenyítés részletek")]
        AFFKcsFfR,

        /// <summary>
        /// Fenyítés - kiétkeztetés csökkentés - Fenyítés végrehajtása/megszüntetése
        /// </summary>
        [Description("Fenyítés - kiétkeztetés csökkentés - Fenyítés végrehajtása/megszüntetése")]
        AFFKcsFvm,

        /// <summary>
        /// Fenyítés - kiétkeztetés csökkentés - Értesítés fenyítés végrehajtásáról
        /// </summary>
        [Description("Fenyítés - kiétkeztetés csökkentés - Értesítés fenyítés végrehajtásáról")]
        AFFKcsEfv,

        /// <summary>
        /// Fenyítés - kiétkeztetés csökkentés - Megszakítás
        /// </summary>
        [Description("Fenyítés - kiétkeztetés csökkentés - Megszakítás")]
        AFFKcsM,

        /// <summary>
        /// Fenyítés - kiétkeztetés csökkentés - megszakítás lezárás
        /// </summary>
        [Description("Fenyítés - kiétkeztetés csökkentés - megszakítás lezárás")]
        AFFKcsMl,

        /// <summary>
        /// Fenyítés - kiétkeztetés csökkentés - Megszakítás törlés
        /// </summary>
        [Description("Fenyítés - kiétkeztetés csökkentés- Megszakítás törlés")]
        AFFKcsMt,

        #endregion Kiétkezés csökkentés végrehajtása

        #region Magánelzárás végrehajtása

        /// <summary>
        /// Fenyítés - magánelzárás - fegyelmi lista
        /// </summary>
        [Description("Fenyítés - magánelzárás - fegyelmi lista")]
        AFFMeFFl,

        /// <summary>
        /// Fenyítés - magánelzárás - Fenyítés részletek
        /// </summary>
        [Description("Fenyítés - magánelzáráss - Fenyítés részletek")]
        AFFMeFfR,

        /// <summary>
        /// Fenyítés - magánelzárás - fenyítés végrehajtása (magánelzárásba helyezés)
        /// </summary>
        [Description("Fenyítés - magánelzárás - fenyítés végrehajtása (magánelzárásba helyezés)")]
        AFFMeFvme,

        /// <summary>
        /// Fenyítés - magánelzárás - áthelyezés másik zárkába
        /// </summary>
        [Description("Fenyítés - magánelzárás - áthelyezés másik zárkába")]
        AFFMeAmz,

        /// <summary>
        /// Fenyítés - magánelzárás - fenyítés megszüntetése (magánelzárásból kihelyezés)
        /// </summary>
        [Description("Fenyítés - magánelzárás - fenyítés megszüntetése (magánelzárásból kihelyezés)")]
        AFFMeFmmk,

        /// <summary>
        /// Fenyítés - magánelzárás - fenyítés megszakítása
        /// </summary>
        [Description("Fenyítés - magánelzárás - fenyítés megszakítása")]
        AFFMeFm,

        /// <summary>
        /// Fenyítés - magánelzárás - fenyítés folytatása
        /// </summary>
        [Description("Fenyítés - magánelzárás - fenyítés folytatása")]
        AFFMeFl,

        #endregion Magánelzárás végrehajtása

        #region Fenyítés végrehajtása

        /// <summary>
        /// Fenyítés - végrehajtás - Fenyítés lista
        /// </summary>
        [Description("Fenyítés - végrehajtás - Fenyítés lista")]
        AFFVegrLi,

        /// <summary>
        /// Fenyítés - végrehajtás - Fenyítés részletek
        /// </summary>
        [Description("Fenyítés - végrehajtás - Fenyítés részletek")]
        AFFVegrRe,

        /// <summary>
        /// Fenyítés - végrehajtás - Fenyítés karbantartó
        /// </summary>
        [Description("Fenyítés - végrehajtás - Fenyítés karbantartó")]
        AFFVegrMo,

        /// <summary>
        /// Fenyítés - végrehajtás - Fenyítés törlése
        /// </summary>
        [Description("Fenyítés - végrehajtás - Fenyítés törlése")]
        AFFVegrTo,

        #endregion Fenyítés végrehajtása

        #endregion Fenyítés végrehajtása

        #region Törvényszék döntése fegyelmi ügyben

        /// <summary>
        /// Törvényszék döntése fegyelmi ügyben - fogvatartott lista
        /// </summary>
        [Description("Törvényszék döntése fegyelmi ügyben - fogvatartott lista")]
        AFTdfuFl,

        /// <summary>
        /// Törvényszék döntése fegyelmi ügyben - fegyelmi lista
        /// </summary>
        [Description("Törvényszék döntése fegyelmi ügyben - fegyelmi lista")]
        AFTdfuFul,

        /// <summary>
        /// Törvényszék döntése fegyelmi ügyben - II. fokú döntés helybenhagyása
        /// </summary>
        [Description("Törvényszék döntése fegyelmi ügyben - II. fokú döntés helybenhagyása")]
        AFTdfuDh,

        /// <summary>
        /// Törvényszék döntése fegyelmi ügyben - II. fokú döntés megváltoztatása
        /// </summary>
        [Description("Törvényszék döntése fegyelmi ügyben - II. fokú döntés megváltoztatása")]
        AFTdfuDm,

        /// <summary>
        /// Törvényszék döntése fegyelmi ügyben - Eljárás megszüntetése
        /// </summary>
        [Description("Törvényszék döntése fegyelmi ügyben - Eljárás megszüntetése")]
        AFTdfuEm,
        /// <summary>
        /// Törvényszék döntése fegyelmi ügyben - Fegyelmi ügy részletek
        /// </summary>
        [Description("Törvényszék döntése fegyelmi ügyben - Fegyelmi ügy részletek")]
        AFTdfuR,

        /// <summary>
        /// Törvényszék döntése fegyelmi ügyben - Rendkivüli esemény részletek
        /// </summary>
        [Description("Törvényszék döntése fegyelmi ügyben - Rendkivüli esemény részletek")]
        AFTdfuReR,
        #endregion Törvényszék döntése fegyelmi ügyben

        #region Feljelentés kezdeményezése

        /// <summary>
        /// Feljelentés kezdeményezése - Fogvatartott lista
        /// </summary>
        [Description("Feljelentés kezdeményezése - Fogvatartott lista")]
        AFFkFL,

        /// <summary>
        /// Feljelentés kezdeményezése - Fegyelmi ügy lista
        /// </summary>
        [Description("Feljelentés kezdeményezése - Fegyelmi ügy lista")]
        AFFkFuL,

        /// <summary>
        /// Feljelentés kezdeményezése - Fegyelmi ügy módosítása
        /// </summary>
        [Description("Feljelentés kezdeményezése - Fegyelmi ügy módosítása")]
        AFFkFuM,

        /// <summary>
        /// Feljelentés kezdeményezése - Fegyelmi ügy részletek
        /// </summary>
        [Description("Feljelentés kezdeményezése - Fegyelmi ügy részletek")]
        AFFkFuR,

        /// <summary>
        /// Feljelentés kezdeményezése - Rendkívüli esemény részletek
        /// </summary>
        [Description("Feljelentés kezdeményezése - Rendkívüli esemény részletek")]
        AFFkReReR,

        #endregion Feljelentés kezdeményezése

        #endregion Fegyelmi eljárás

        #region Átmeneti részleg (be-kihelyezés)

        #region Javaslat behelyezésre

        [Description("Átmeneti részlegbe helyezési javaslat - Fogvatartott lista")]
        AFAtCsopHeJaKaFtLi,
        [Description("Átmeneti részlegbe helyezési javaslat - Felvitel")]
        AFAtCsopHeJaKaFe,
        [Description("Átmeneti részlegbe helyezési javaslat - Módosítás")]
        AFAtCsopHeJaKaMo,
        [Description("Átmeneti részlegbe helyezési javaslat - Törlés")]
        AFAtCsopHeJaKaTo,
        [Description("Átmeneti részlegbe helyezési javaslat - Részletek")]
        AFAtCsopHeJaKaRe,

        #endregion Javaslat behelyezésre

        #region Átmenti csoportba helyezés véleményezése
        [Description("Átmeneti részlegbe helyezés véleményezése - Fogvatartott lista")]
        AFAtCsopHeVeKaFtLi,
        [Description("Átmeneti részlegbe helyezés véleményezése - Módosítás")]
        AFAtCsopHeVeKaMo,
        [Description("Átmeneti részlegbe helyezés véleményezése - Részletek")]
        AFAtCsopHeVeKaRe,
        #endregion Átmenti csoportba helyezés véleményezése

        #region Átmeneti részlegbe helyezés
        [Description("Átmeneti részlegbe helyezés - Fogvatartott lista")]
        AFAtCsopHelyKaFtLi,
        [Description("Átmeneti részlegbe helyezés")]
        AFAtCsopHelyKaMo,
        [Description("Átmeneti részlegbe helyezést nem engedélyez")]
        AFAtCsopHelyKaTo,
        [Description("Átmeneti részlegbe helyezés - Részletek")]
        AFAtCsopHelyKaRe,
        #endregion Átmeneti részlegbe helyezés

        #region Javaslat kihelyezésre

        [Description("Átmeneti részlegből kihelyezési javaslat " +
                     "- Fogvatartott lista")]
        AFAtCsopKiHeKaFtLi,
        [Description("Átmeneti részlegből kihelyezési javaslat - Módosítás")]
        AFAtCsopKiHeKaMo,

        [Description("Átmeneti részlegből kihelyezési javaslat - Részletek")]
        AFAtCsopKiHeKaRe,

        #endregion Javaslat kihelyezésre

        #region Átmeneti részlegből kihelyezés véleményezése

        [Description("Átmeneti részlegből kihelyezés véleményezése - Fogvatartott lista")]
        AFAtCsopKiVeKaFtLi,

        [Description("Átmeneti részlegből kihelyezés véleményezése - Vélemény írás")]
        AFAtCsopKiVeKaMo,

        [Description("Átmeneti részlegből kihelyezés véleményezése - Részletek")]
        AFAtCsopKiVeKaRe,

        #endregion Átmeneti részlegből kihelyezés véleményezése

        #region Átmeneti részlegből kihelyezés

        [Description("Átmeneti részlegből kihelyezés - Fogvatartott lista")]
        AFAtCsopKiHelyKaFtLi,
        [Description("Átmeneti részlegből kihelyezés - Módosítás")]
        AFAtCsopKiHelyKaMo,
        [Description("Átmeneti részlegből kihelyezés - Törlés")]
        AFAtCsopKiHelyKaTo,
        [Description("Átmeneti részlegből kihelyezés - Részletek")]
        AFAtCsopKiHelyKaRe,

        #endregion Átmeneti részlegből kihelyezés


        #endregion Átmeneti részleg (be-kihelyezés)

        #region Többletinformáció

        /// <summary>Fogvatartott többletinformáció felvitel</summary>
        [Description("Fogvatartott többletinformáció felvitel")]
        AFFoToInFe,

        /// <summary>Többletinformációval rendelkező fogvatartottak listája</summary>
        [Description("Többletinformációval rendelkező fogvatartottak listája")]
        AFFoToIFLi,

        /// <summary>Fogvatartott többletinformációi</summary>
        [Description("Fogvatartott többletinformációi")]
        AFFoToInLi,

        /// <summary>Fogvatartott többletinformáció módosítás</summary>
        [Description("Fogvatartott többletinformáció módosítás")]
        AFFoToInMo,

        /// <summary>Fogvatartott többletinformáció részletek</summary>
        [Description("Fogvatartott többletinformáció részletek")]
        AFFoToInRe,

        #endregion Többletinformáció

        #region Szállítás jóváhagyása
        /// <summary>Szállítási határidő jóváhagyása - Szállítás lista</summary>
        [Description("Szállítási határidő jóváhagyása - Szállítás lista")]
        AFSzaHijSzl,
        #endregion Szállítás jóváhagyása

        #region Szállítás jóváhagyás visszavonása
        /// <summary>Szállítási határidő jóváhagyása - Szállítás lista</summary>
        [Description("Szállítási határidő jóváhagyás visszavonása - Szállítás lista")]
        AFSzaHijViSzl,
        #endregion Szállítás jóváhagyása

        #region Szállításjelentés lekérdezése
        /// <summary>Szállításjelentés lekérdezése</summary>
        [Description("Szállításjelentés lekérdezése")]
        ALSzJeLe,
        #endregion Szállításjelentés lekérdezése

        #region Eltávozás jóváhagyása

        /// <summary>Eltavozás távozás rögzítés - Eltávozás lista</summary>
        [Description("Eltávozás jóváhagyása - Eltávozás lista")]
        AFEltJovEltLi,

        /// <summary>Eltávozás jóváhagyása</summary>
        [Description("Eltávozás jóváhagyása")]
        AFEltJovEltMo,

        #endregion Eltávozás jóváhagyása

        #region Bf. jóváhagyása
        /// <summary>Büntetés-félbeszakítás jóváhagyása több fogvatartottnak - Bf lista</summary>
        [Description("Büntetés-félbeszakítás jóváhagyása több fogvatartottnak - Bf lista")]
        AFBuFJovToLi,
        /// <summary>Büntetés-félbeszakítás jóváhagyása több fogvatartottnak - Felvitel</summary>
        [Description("Büntetés-félbeszakítás jóváhagyása több fogvatartottnak - Felvitel")]
        AFBuFJovTF,
        #endregion Bf. jóváhagyása

        #region Technikai eszközök adatainak karbantartása

        /// <summary>
        /// Technikai eszközök adatainak karbantartása - fogvatartott lista
        /// </summary>
        [Description("Technikai eszközök adatainak karbantartása - fogvatartott lista")]
        AFTeakFl,

        /// <summary>
        /// Technikai eszközök adatainak karbantartása - technikai eszköz tartás lista
        /// </summary>
        [Description("Technikai eszközök adatainak karbantartása - technikai eszköz tartás lista")]
        AFTeakTetl,

        /// <summary>
        /// Technikai eszközök adatainak karbantartása - technikai eszköz tartás részletek
        /// </summary>
        [Description("Technikai eszközök adatainak karbantartása - technikai eszköz tartás részletek")]
        AFTeakTetR,

        /// <summary>
        /// Technikai eszközök adatainak karbantartása - technikai eszköz tartás felvitel
        /// </summary>
        [Description("Technikai eszközök adatainak karbantartása - technikai eszköz tartás felvitel")]
        AFTeakTetF,

        /// <summary>
        /// Technikai eszközök adatainak karbantartása - technikai eszköz tartás módosítás
        /// </summary>
        [Description("Technikai eszközök adatainak karbantartása - technikai eszköz tartás módosítás")]
        AFTeakTetM,

        /// <summary>
        /// Technikai eszközök adatainak karbantartása - technikai eszköz tartás törlés
        /// </summary>
        [Description("Technikai eszközök adatainak karbantartása - technikai eszköz tartás törlés")]
        AFTeakTetT,

        #endregion // Technikai eszközök adatainak karbantartása

        #region Kényszerítő eszköz alkalmazása – orvosi vélemény

        /// <summary>
        /// Kényszerítő eszköz alkalmazása –  orvosi vélemény - fogvatartott lista
        /// </summary>
        [Description("Kényszerítő eszköz alkalmazása –  orvosi vélemény - fogvatartott lista")]
        AFKeaovFl,

        /// <summary>
        /// Kényszerítő eszköz alkalmazása –  orvosi vélemény - kényszerítő eszköz lista
        /// </summary>
        [Description("Kényszerítő eszköz alkalmazása –  orvosi vélemény - kényszerítő eszköz lista")]
        AFkeaovKehl,

        /// <summary>
        /// Kényszerítő eszköz alkalmazása – elbírálás - kényszerítő eszközz reszletek
        /// </summary>
        [Description("Kényszerítő eszköz alkalmazása –  orvosi vélemény - kényszerítő eszköz reszletek")]
        AFkeaovKehR,

        /// <summary>
        /// Kényszerítő eszköz alkalmazása – elbírálás - vélemény rögzítése
        /// </summary>
        [Description("Kényszerítő eszköz alkalmazása –  orvosi vélemény - vélemény rögzítése")]
        AFkeaovKehVr,

        #endregion // Kényszerítő eszköz alkalmazása – orvosi vélemény

        #region Kényszerítő eszköz alkalmazása – elbírálás

        /// <summary>
        /// Kényszerítő eszköz alkalmazása – elbírálás - fogvatartott lista
        /// </summary>
        [Description("Kényszerítő eszköz alkalmazása – elbírálás - fogvatartott lista")]
        AFKeaeFl,

        /// <summary>
        /// Kényszerítő eszköz alkalmazása – elbírálás - kényszerítő eszköz használata lista
        /// </summary>
        [Description("Kényszerítő eszköz alkalmazása – elbírálás - fogvatartott lista")]
        AFKeaKehl,

        /// <summary>
        /// Kényszerítő eszköz alkalmazása – elbírálás - elbírálás
        /// 
        /// </summary>
        [Description("Kényszerítő eszköz alkalmazása – elbírálás - elbírálás")]
        AFkeaE,

        #endregion // Kényszerítő eszköz alkalmazása – elbírálás

        #region Foglalkozások karbantartása

        /// <summary>
        /// Foglalkozások karbantartása - foglalkozás lista
        /// </summary>
        [Description("Foglalkozások karbantartása - foglalkozás lista")]
        AFFgkFl,

        /// <summary>
        /// Foglalkozások karbantartása - foglalkozás felvitel
        /// </summary>
        [Description("Foglalkozások karbantartása - foglalkozás felvitel")]
        AFFgkF,

        /// <summary>
        /// Foglalkozások karbantartása - foglalkozás módosítás
        /// </summary>
        [Description("Foglalkozások karbantartása - foglalkozás módosítás")]
        AFFgkM,

        /// <summary>
        /// Foglalkozások karbantartása - foglalkozás törlése
        /// </summary>
        [Description("Foglalkozások karbantartása - foglalkozás törlése")]
        AFFgkT,

        /// <summary>
        /// Foglalkozások karbantartása - foglalkozás részletek
        /// </summary>
        [Description("Foglalkozások karbantartása - foglalkozás részletek")]
        AFFgkR,

        /// <summary>
        /// Foglalkozások karbantartása - foglalkozáson részvevők listája
        /// </summary>
        [Description("Foglalkozások karbantartása - foglalkozáson részvevők listája")]
        AFFgkFrl,

        /// <summary>
        /// Foglalkozások karbantartása - foglalkozás résztvevő felvitele
        /// </summary>
        [Description("Foglalkozások karbantartása - foglalkozás résztvevő felvitele")]
        AFFgkFrF,

        /// <summary>
        /// Foglalkozások karbantartása - aktivitás rögzítése
        /// </summary>
        [Description("Foglalkozások karbantartása - aktivitás rögzítése")]
        AFFgkFrAr,

        /// <summary>
        /// Foglalkozások karbantartása - foglalkozáson résztvevő törlése
        /// </summary>
        [Description("Foglalkozások karbantartása - foglalkozáson résztvevő törlése")]
        AFFgkFrT,

        #endregion // Foglalkozások karbantartása

        #region Könyvkölcsönzés adatok
        /// <summary>Könyvkölcsönzés adatok - Reintegrációs részleg lista</summary>
        [Description("Könyvkölcsönzés adatok - Reintegrációs részleg lista")]
        AFKonyvkolcsKarbNCSL,

        /// <summary>Könyvkölcsönzés adatok - Fogvatartott lista</summary>
        [Description("Könyvkölcsönzés adatok - Fogvatartott lista")]
        AFKonyvkolcsKarbFL,

        /// <summary>Könyvkölcsönzés adatok - Kiválasztott fogvatartott tételei</summary>
        [Description("Könyvkölcsönzés adatok - Kiválasztott fogvatartott tételei")]
        AFKonyvkolcsKarbKtL,

        /// <summary>Könyvkölcsönzés adatok - Kiválasztott tétel részletei</summary>
        [Description("Könyvkölcsönzés adatok - Kiválasztott tétel részletei")]
        AFKonyvkolcsKarbR,

        /// <summary>Könyvkölcsönzés adatok - Felvitel</summary>
        [Description("Könyvkölcsönzés adatok - Felvitel")]
        AFKonyvkolcsKarbF,

        /// <summary>Könyvkölcsönzés adatok - Módosítás</summary>
        [Description("Könyvkölcsönzés adatok - Módosítás")]
        AFKonyvkolcsKarbM,

        /// <summary>Könyvkölcsönzés adatok - Törlés</summary>
        [Description("Könyvkölcsönzés adatok - Törlés")]
        AFKonyvkolcsKarbTo,

        #endregion Könyvkölcsönzés adatok

        #region Újságrendelés adatok
        /// <summary>Újságrendelés adatok - Reintegrációs részleg lista</summary>
        [Description("Újságrendelés adatok - Reintegrációs részleg lista")]
        AFUjsrendKarbNCSL,

        /// <summary>Újságrendelés adatok - Fogvatartott lista</summary>
        [Description("Újságrendelés adatok - Fogvatartott lista")]
        AFUjsrendKarbFL,

        /// <summary>Újságrendelés adatok - Kiválasztott fogvatartott tételei</summary>
        [Description("Újságrendelés adatok - Kiválasztott fogvatartott tételei")]
        AFUjsrendKarbKtL,

        /// <summary>Újságrendelés adatok - Kiválasztott tétel részletei</summary>
        [Description("Újságrendelés adatok - Kiválasztott tétel részletei")]
        AFUjsrendKarbR,

        /// <summary>Újságrendelés adatok - Felvitel</summary>
        [Description("Újságrendelés adatok - Felvitel")]
        AFUjsrendKarbF,

        /// <summary>Újságrendelés adatok - Módosítás</summary>
        [Description("Újságrendelés adatok - Módosítás")]
        AFUjsrendKarbM,

        /// <summary>Újságrendelés adatok - Törlés</summary>
        [Description("Újságrendelés adatok - Törlés")]
        AFUjsrendKarbTo,

        #endregion Újságrendelés adatok

        #region Tisztálkodási cikkek igénylése, kiadása

        #region Jogosultság térítésmentes tisztálkodási cikkre

        /// <summary>
        /// Jogosultság térítésmentes tisztálkodási cikkekre - Fogvatartott lista
        /// </summary>
        [Description("Jogosultság térítésmentes tisztálkodási cikkekre - Fogvatartott lista")]
        AFJogTmTiCikkFtLi,

        /// <summary>
        /// Jogosultság térítésmentes tisztálkodási cikkekre - Beállítás
        /// </summary>
        [Description("Jogosultság térítésmentes tisztálkodási cikkekre - Beállítás")]
        AFJogTmTiCikkBe,

        #endregion Jogosultság térítésmentes tisztálkodási cikkre

        #region Tisztálkodási cikk igénylések csoportos lezárása

        /// <summary>
        /// Tisztálkodási cikkek igénylésének csoportos lezárása - Fogvatartott lista
        /// </summary>
        [Description("Tisztálkodási cikkek igénylésének csoportos lezárása - Fogvatartott lista")]
        AFTiCikkIgCsoLezFtLi,

        /// <summary>
        /// Tisztálkodási cikkek igénylésének csoportos lezárása - Lezárás
        /// </summary>
        [Description("Tisztálkodási cikkek igénylésének csoportos lezárása - Lezárás")]
        AFTiCikkIgCsoLezFe,

        #endregion Tisztálkodási cikk igénylések csoportos lezárása

        #region Tisztálkodási cikk csoportos igénylése

        /// <summary>
        /// Tisztálkodási cikkek csoportos igénylése - Fogvatartott lista
        /// </summary>
        [Description("Tisztálkodási cikkek csoportos igénylése - Fogvatartott lista")]
        AFTiCikkCsoIgFtLi,

        /// <summary>
        /// Tisztálkodási cikkek csoportos igénylése - Felvitel
        /// </summary>
        [Description("Tisztálkodási cikkek csoportos igénylése - Felvitel")]
        AFTiCikkCsoIgFe,

        #endregion Tisztálkodási cikk csoportos igénylése

        #region Tisztálkodási cikk igénylése
        /// <summary>
        /// Tisztálkodási cikkek igénylése - Fogvatartott lista
        /// </summary>
        [Description("Tisztálkodási cikkek igénylése - Fogvatartott lista")]
        AFTiCikkIgFtLi,

        /// <summary>
        /// Tisztálkodási cikkek igénylése - Tisztálkodási cikk kérelem lista
        /// </summary>
        [Description("Tisztálkodási cikkek igénylése - Tisztálkodási cikk kérelem lista")]
        AFTiCikkIgTiCikkKeLi,

        /// <summary>
        /// Tisztálkodási cikkek igénylése - Részletek
        /// </summary>
        [Description("Tisztálkodási cikkek igénylése - Részletek")]
        AFTiCikkIgRe,

        /// <summary>
        /// Tisztálkodási cikkek igénylése - Felvitel
        /// </summary>
        [Description("Tisztálkodási cikkek igénylése - Felvitel")]
        AFTiCikkIgFe,

        /// <summary>
        /// Tisztálkodási cikkek igénylése - Módosítás
        /// </summary>
        [Description("Tisztálkodási cikkek igénylése - Módosítás")]
        AFTiCikkIgMo,

        /// <summary>
        /// Tisztálkodási cikkek igénylése - Törlés
        /// </summary>
        [Description("Tisztálkodási cikkek igénylése - Törlés")]
        AFTiCikkIgTo,

        #endregion Tisztálkodási cikk igénylése

        #region Tisztálkodási cikk igénylések csoportos kiadása

        /// <summary>
        /// Tisztálkodási cikkek csoportos kiadása - Fogvatartott lista
        /// </summary>
        [Description("Tisztálkodási cikkek csoportos kiadása - Fogvatartott lista")]
        AFTiCikkCsoKiFtLi,

        /// <summary>
        /// Tisztálkodási cikkek csoportos kiadása - Felvitel
        /// </summary>
        [Description("Tisztálkodási cikkek csoportos kiadása - Felvitel")]
        AFTiCikkCsoKiFe,

        #endregion Tisztálkodási cikk igénylések csoportos kiadása

        #region Tisztálkodási cikk kiadása

        /// <summary>
        /// Tisztálkodási cikkek kiadása - Fogvatartott lista
        /// </summary>
        [Description("Tisztálkodási cikkek kiadása - Fogvatartott lista")]
        AFTiCikkKiFtLi,

        /// <summary>
        /// Tisztálkodási cikkek kiadása - Tisztálkodási cikk kiadás lista
        /// </summary>
        [Description("Tisztálkodási cikkek kiadása - Tisztálkodási cikk kiadás lista")]
        AFTiCikkKiTiCikkKiLi,

        /// <summary>
        /// Tisztálkodási cikkek kiadása - Részletek
        /// </summary>
        [Description("Tisztálkodási cikkek kiadása - Részletek")]
        AFTiCikkKiRe,

        /// <summary>
        /// Tisztálkodási cikkek kiadása - Felvitel
        /// </summary>
        [Description("Tisztálkodási cikkek kiadása - Felvitel")]
        AFTiCikkKiFe,

        /// <summary>
        /// Tisztálkodási cikkek kiadása - Módosítás
        /// </summary>
        [Description("Tisztálkodási cikkek kiadása - Módosítás")]
        AFTiCikkKiMo,

        /// <summary>
        /// Tisztálkodási cikkek kiadása - Törlés
        /// </summary>
        [Description("Tisztálkodási cikkek kiadása - Törlés")]
        AFTiCikkKiTo,

        #endregion Tisztálkodási cikk kiadása

        #endregion Tisztálkodási cikkek igénylése, kiadása

        #region Csomag érkeztetés

        /// <summary>
        /// Csomag érkeztetés - Fogvatartott lista
        /// </summary>
        [Description("Csomag érkeztetés - Fogvatartott lista")]
        AFCsErkFtLi,

        /// <summary>
        /// Csomag érkeztetés - Szelvény nélkül érkezett csomagok listája
        /// </summary>
        [Description("Csomag érkeztetés - Szelvény nélkül érkezett csomagok listája")]
        AFCsErkSzNeCsLi,

        /// <summary>
        /// Csomag érkeztetés - Szelvény nélküli csomag részletek
        /// </summary>
        [Description("Csomag érkeztetés - Szelvény nélküli csomag részletek")]
        AFCsErkSzNeCsRe,

        /// <summary>
        /// Csomag érkeztetés - Szelvény nélküli csomag felvitel
        /// </summary>
        [Description("Csomag érkeztetés - Szelvény nélküli csomag felvitel")]
        AFCsErkSzNeCsFe,

        /// <summary>
        /// Csomag érkeztetés - Szelvény nélküli csomag módosítás
        /// </summary>
        [Description("Csomag érkeztetés - Szelvény nélküli csomag módosítás")]
        AFCsErkSzNeCsMo,

        /// <summary>
        /// Csomag érkeztetés - Szelvény nélküli csomag törlés
        /// </summary>
        [Description("Csomag érkeztetés - Szelvény nélküli csomag törlés")]
        AFCsErkSzNeCsTo,

        /// <summary>
        /// Csomag érkeztetés - Csomagküldési engedély lista
        /// </summary>
        [Description("Csomag érkeztetés - Csomagküldési engedély lista")]
        AFCsErkCsKuEngLi,

        /// <summary>
        /// Csomag érkeztetés - Szelvénnyel érkezett csomagok listája
        /// </summary>
        [Description("Csomag érkeztetés - Szelvénnyel érkezett csomagok listája")]
        AFCsErkSzErkCsLi,

        /// <summary>
        /// Csomag érkeztetés - Szelvénnyel érkezett csomag részletek
        /// </summary>
        [Description("Csomag érkeztetés - Szelvénnyel érkezett csomag részletek")]
        AFCsErkSzErkCsRe,

        /// <summary>
        /// Csomag érkeztetés - Szelvénnyel érkezett csomag felvitel
        /// </summary>
        [Description("Csomag érkeztetés - Szelvénnyel érkezett csomag felvitel")]
        AFCsErkSzErkCsFe,

        /// <summary>
        /// Csomag érkeztetés - Szelvénnyel érkezett csomag módosítás
        /// </summary>
        [Description("Csomag érkeztetés - Szelvénnyel érkezett csomag módosítás")]
        AFCsErkSzErkCsMo,

        /// <summary>
        /// Csomag érkeztetés - Szelvénnyel érkezett csomag törlés
        /// </summary>
        [Description("Csomag érkeztetés - Szelvénnyel érkezett csomag törlés")]
        AFCsErkSzErkCsTo,

        #endregion Csomag érkeztetés

        #region Munkáltatás adatok karbantartása
        /// <summary>Munkáltatás - Fogvatartott lista</summary>
        [Description("Munkáltatás - Fogvatartott lista")]
        AFMunkaltatFL,

        /// <summary>Munkáltatás- Munkáltatás/Nem dolgozás lista</summary>
        [Description("Munkáltatás- Munkáltatás/Nem dolgozás lista")]
        AFMunkaltatMNDL,

        /// <summary>Munkáltatás- Munkáltatás/Nem dolgozás részletekk</summary>
        [Description("Munkáltatás- Munkáltatás/Nem dolgozás részletek")]
        AFMunkaltatMNDR,

        /// <summary>Munkáltatás- Új munkára előjegyzés</summary>
        [Description("Munkáltatás- Új munkára előjegyzés")]
        AFMunkaltatME,

        /// <summary>Munkáltatás- Dolgozóvá nyilvánítás/áthelyezés</summary>
        [Description("Munkáltatás- Dolgozóvá nyilvánítás/áthelyezés")]
        AFMunkaltatDN,

        /// <summary>Munkáltatás - Nem dolgozóvá nyilvánítás</summary>
        [Description("Munkáltatás- Nem dolgozóvá nyilvánítás")]
        AFMunkaltatNDNY,

        /// <summary>Munkáltatás - Nem dolgozás felülvizsgálata</summary>
        [Description("Munkáltatás- Nem dolgozás felülvizsgálata")]
        AFMunkaltatNDF,

        /// <summary>Munkáltatás- Módosítás</summary>
        [Description("Munkáltatás- Módosítás")]
        AFMunkaltatM,

        /// <summary>Munkáltatás- Törlés</summary>
        [Description("Munkáltatás- Törlés")]
        AFMunkaltatT,

        /// <summary>ÍMunkáltatás - Nem dolgozás felülvizsgálata több fogvatartottnak</summary>
        [Description("Munkáltatás - Nem dolgozás felülvizsgálata több fogvatartottnak")]
        AFMunkaltatNDFTF,


        #endregion Munkáltatás adatok karbantartása

        #region Élelmezési norma adatok karbantartása
        /// <summary>Élelmezési norma - Fogvatartott lista</summary>
        [Description("Élelmezési norma - Fogvatartott lista")]
        AFElNoAdFtLi,

        /// <summary>Élelmezési norma - Élelmezési normák lista</summary>
        [Description("Élelmezési norma - Élelmezési normák lista")]
        AFElNoAdEnLi,

        /// <summary>Élelmezési norma - Részletek</summary>
        [Description("Élelmezési norma - Részletek")]
        AFElNoAdRe,

        /// <summary>Élelmezési norma - Felvitel</summary>
        [Description("Élelmezési norma - Felvitel")]
        AFElNoAdFe,

        /// <summary>Élelmezési norma - Módosítás</summary>
        [Description("Élelmezési norma - Módosítás")]
        AFElNoAdMo,

        /// <summary>Élelmezési norma - Törlés</summary>
        [Description("Élelmezési norma - Törlés")]
        AFElNoAdTo,

        #endregion Élelmezési norma adatok karbantartása

        #region EU engedély adatok karbantartása
        /// <summary>Élelmezési norma - Fogvatartott lista</summary>
        [Description("EU engedély - Fogvatartott lista")]
        AFEuEngFtLi,

        /// <summary>EU engedély - Élelmezési normák lista</summary>
        [Description("EU engedély - EU engedélyek lista")]
        AFEuEngELi,

        /// <summary>EU engedély - Részletek</summary>
        [Description("EU engedély - Részletek")]
        AFEuEngRe,

        /// <summary>EU engedély - Felvitel</summary>
        [Description("EU engedély - Felvitel")]
        AFEuEngFe,

        /// <summary>EU engedély - Módosítás</summary>
        [Description("EU engedély - Módosítás")]
        AFEuEngMo,

        /// <summary>EU engedély - Törlés</summary>
        [Description("EU engedély - Törlés")]
        AFEuEngTo,

        #endregion EU engedély adatok karbantartása

        #endregion Egyéb szakter. feladatok

        #endregion funkció jogosultságok

        #region lekérdezés jogosultságok

        #region Bűnügyi nyilvántartás
        #region Fogvatartott adatainak karbantartása
        #region Büntetés-végrehajtási ügy

        #region Bf-re engedett fogvatartottak
        #endregion Bf-re engedett fogvatartottak

        #endregion Büntetés-végrehajtási ügy

        #endregion Fogvatartott adatainak karbantartása

        #region Hibás adatú fogvatartottak
        /// <summary>Lekérdezés - Hibás adatú fogvatartottak</summary>
        [Description("Lekérdezés - Hibás adatú fogvatartottak")]
        ALHiAdFo,
        #endregion Hibás adatú fogvatartottak

        #region Körözési lista
        /// <summary>Lekérdezés - Körözési lista</summary>
        [Description("Lekérdezés - Körözési lista")]
        ALKoLiFo,
        #endregion Körözési lista

        #region Vélhetőleg azonos személyek lekérdezése
        /// <summary>Lekérdezés - Vélhetőleg azonos személyek - Vélhetőleg azonos személy csoport lista</summary>
        [Description("Lekérdezés - Vélhetőleg azonos személyek - Vélhetőleg azonos személy csoport lista")]
        ALVeAzSzVeAzSzCs,

        /// <summary>Lekérdezés - Vélhetőleg azonos személyek - Vélhetőleg azonos személy csoport tag lista</summary>
        [Description("Lekérdezés - Vélhetőleg azonos személyek - Vélhetőleg azonos személy csoport tag lista")]
        ALVeAzSzVeAzSzCsTa,
        #endregion Vélhetőleg azonos személyek lekérdezése

        #region Szállítás

        #region Szállítási jegyzék nyomtatása
        /// <summary>Lekérdezés - Szállítási jegyzék nyomtatása</summary>
        [Description("Lekérdezés - Szállítási jegyzék nyomtatása")]
        ALSzJeNy,
        #endregion Szállítási jegyzék nyomtatása

        #region Átadás-átvételi elismervény nyomtatása
        /// <summary>Lekérdezés - Átadás átvételi elismervény nyomtatása - Bv intézet Lista</summary>
        [Description("Lekérdezés - Átadás átvételi elismervény nyomtatása - Bv intézet Lista")]
        ALAtAtElNyBvInLi,

        /// <summary>Lekérdezés - Átadás átvételi elismervény nyomtatása - Szállítás  Lista</summary>
        [Description("Lekérdezés - Átadás átvételi elismervény nyomtatása - Szállítás Lista")]
        ALAtAtElNySzLi,
        #endregion Átadás-átvételi elismervény nyomtatása

        #region Szállítással érkezők
        /// <summary>Lekérdezés - Szállítással érkezők lista</summary>
        [Description("Lekérdezés - Szállítással érkezők lista")]
        ALSzErFo,
        #endregion Szállítással érkezők

        #endregion Szállítás

        #region Nyomtatások

        #region Nyilvántartó lap
        /// <summary>Lekérdezés - Nyilvántartó lap nyomtatása</summary>
        [Description("Lekérdezés - Nyilvántartó lap nyomtatása")]
        ALNyLaNy,
        #endregion Nyilvántartó lap

        #region Címke
        /// <summary>Lekérdezés - Címke nyomtatása</summary>
        [Description("Lekérdezés - Címke nyomtatása")]
        ALCiNy,
        #endregion Címke

        #region 8-as számú adatlap
        /// <summary>Lekérdezés - 8-as sz. adatlap nyomtatása</summary>
        [Description("Lekérdezés - 8-as sz. adatlap nyomtatása")]
        ALNySzAdNy,
        #endregion 8-as számú adatlap

        #region Általános igazolás nyomtatása
        /// <summary>Lekérdezés - Általános igazolás nyomtatása</summary>
        [Description("Lekérdezés - Általános igazolás nyomtatása")]
        ALAlIgNyFo,
        #endregion Általános igazolás nyomtatása

        #endregion Nyomtatások

        #endregion Bűnügyi nyilvántartás

        #region Biztonság

        #region Elrendelt biztonsági elkülönítés
        /// <summary>Lekérdezés - Elrendelt biztonsági elkülönítések</summary>
        [Description("Lekérdezés - Elrendelt biztonsági elkülönítések ")]
        ALElBiElFo,
        #endregion Elrendelt biztonsági elkülönítés

        #region Elrendelt fegyelmi elkülönítés
        /// <summary>Lekérdezés - Elrendelt fegyelmi elkülönítések - Fogvatartott lista</summary>
        [Description("Lekérdezés - Elrendelt fegyelmi elkülönítések - Fogvatartott lista")]
        ALElFeElFo,

        /// <summary>Lekérdezés - Elrendelt fegyelmi elkülönítések - Fegyelmi ügy lista</summary>
        [Description("Lekérdezés - Elrendelt fegyelmi elkülönítések - Fegyelmi ügy lista")]
        ALElFeElFeUg,
        #endregion Elrendelt fegyelmi elkülönítés

        #endregion Biztonság

        #region Bef. Biz. feladatok

        #region Befogadási Bizottsági ülésen történt változások
        /// <summary>Lekérdezés - Befogadási Bizottsági ülésen történt változások - Fogvatartott lista</summary>
        [Description("Lekérdezés - Befogadási Bizottsági ülésen történt változások - Fogvatartott lista")]
        ALBeBiUlToVaFo,

        /// <summary>Lekérdezés - Befogadási Bizottsági ülésen történt változások - Kiválasztott fogvatartott befogadási bizottság elé állításai lista</summary>
        [Description("Lekérdezés - Befogadási Bizottsági ülésen történt változások - Kiválasztott fogvatartott befogadási bizottság elé állításai lista")]
        ALBeBiUlToVaKiFo,
        #endregion Befogadási Bizottsági ülésen történt változások

        #region Biztonsági csoportba sorolás lekérdezése
        /// <summary>Lekérdezés - Biztonsági csoportba sorolás - Fogvatartott lista</summary>
        [Description("Lekérdezés - Biztonsági csoportba sorolás - Fogvatartott lista")]
        ALBiCsSo,
        #endregion Biztonsági csoportba sorolás lekérdezése

        #region Lekérdezés - Rezs. kat. sorolás lekérdezése
        /// <summary>Lekérdezés - Rezs. kat., bizt. kock. sorolás - Fogvatartott lista</summary>
        [Description("Lekérdezés - Rezs. kat. sorolás - Fogvatartott lista")]
        ALReSo,
        #endregion Lekérdezés - Rezs. kat. sorolás lekérdezése

        #endregion Bef. Biz. feladatok

        #region Egyéb szakter. feladatok

        #region Előállítás

        #region Előállítási utasítás nyomtatása
        /// <summary>Lekérdezés - Előállítandók</summary>
        [Description("Lekérdezés - Előállítandók")]
        ALElAl,
        #endregion Előállítási utasítás nyomtatása

        #endregion Előállítás

        #region Kérelem, panasz szakterületi intézése

        #region Kérelem, panasz – határozat nyomtatása
        /// <summary>Lekérdezés - Kérelmek, panaszok - intézeti hatáskörben hozott határozat nyomtatása</summary>
        [Description("Lekérdezés - Kérelmek, panaszok - intézeti hatáskörben hozott határozat nyomtatása")]
        ALKePaNy,
        #endregion Kérelem, panasz – határozat nyomtatása

        #endregion Kérelem, panasz szakterületi intézése

        #region Fegyelmi eljárás

        #region Fegyelmi ügy és nyomtatványai
        /// <summary>Lekérdezés - Fegyelmi lap nyomtatása- Fogvatartott lista</summary>
        [Description("Lekérdezés - Fegyelmi lap nyomtatása - Fogvatartott lista")]
        ALFeLaNyFo,

        /// <summary>Lekérdezés - Fegyelmi lap nyomtatása- Fegyelmi ügy lista</summary>
        [Description("Lekérdezés - Fegyelmi lap nyomtatása - Fegyelmi ügy lista")]
        ALFeLaNyFeUg,

        /// <summary>Fegyelmi ügyhöz kapcsolódó nyomtatványok lista</summary>
        [Description("Fegyelmi ügyhöz kapcsolódó nyomtatványok lista")]
        ALFeUgyKaNyL,
        #endregion Fegyelmi ügy és nyomtatványai

        #endregion Fegyelmi eljárás

        #endregion Egyéb szakter. feladatok

        #region Egy fogvatartottra vonatkozó adatok

        #region Jelenlévők és korábbi fogvatartásaik

        /// <summary>Lekérdezés - Jelenlévők és korábbi fogvatartásaik</summary>
        [Description("Lekérdezés - Jelenlévők és korábbi fogvatartásaik")]
        ALJeFo,

        /// <summary>Lekérdezés - Személy összes fogvatartása lista</summary>
        [Description("Lekérdezés - Személy összes fogvatartása lista")]
        ALEgSzOsFo,

        #endregion Jelenlévők és korábbi fogvatartásaik

        #region Végleg távozott ft. adatai

        /// <summary>Lekérdezés - Végleg távozott fogvatartott lista</summary>
        [Description("Lekérdezés - Végleg távozott fogvatartott lista")]
        ALVeTaFo,

        #endregion Végleg távozott ft. adatai

        #region Fogvatartott adatai idősorban

        /// <summary>Lekérdezés - Fogvatartott adatai idősorban - Fogvatartott lista</summary>
        [Description("Lekérdezés - Fogvatartott adatai idősorban - Fogvatartott lista")]
        ALFoAdIdFo,

        /// <summary>Lekérdezés - Fogvatartott adatai idősorban - Idősoros lista</summary>
        [Description("Lekérdezés - Fogvatartott adatai idősorban - Idősoros lista")]
        ALFoAdIdId,

        #endregion Fogvatartott adatai idősorban

        #region Fogvatartott további (más) nevei

        /// <summary>Lekérdezés - További nevekkel rendelkező fogvatartottak</summary>
        [Description("Lekérdezés - További nevekkel rendelkező fogvatartottak")]
        ALToNeReFo,

        /// <summary>Lekérdezés - Fogvatartott további nevei</summary>
        [Description("Lekérdezés - Fogvatartott további nevei")]
        ALFoToNe,

        #endregion Fogvatartott további (más) nevei

        #region Fogvatartott töltött ítélete

        /// <summary>Lekérdezés - Fogvatartott töltött ítéletei</summary>
        [Description("Lekérdezés - Fogvatartott töltött ítéletei")]
        ALFoToIt,

        #endregion Fogvatartott töltött ítélete

        #region Fogvatartott bűntársai

        /// <summary>Lekérdezés - Bűntársi csoport tag fogvatartottak</summary>
        [Description("Lekérdezés - Bűntársi csoport tag fogvatartottak")]
        ALBuCsTaFo,

        #endregion Fogvatartott bűntársai

        #region Fogvatartott munkára való alkalmasságának lekérdezése - EÜ modultól

        /// <summary>Lekérdezés - Fogvatartott munkavégzőképessége - eü modultól</summary>
        [Description("Lekérdezés - Fogvatartott munkavégzőképessége - eü modultól")]
        ALFoEuAl,

        #endregion Fogvatartott munkára való alkalmasságának lekérdezése - EÜ modultól

        #region Egészségügyi állapot adatok lekérdezése -Eü.modultól
        #endregion Egészségügyi állapot adatok lekérdezése -Eü.modultól

        #region Fogvatartott kérelmeinek lekérdezése

        /// <summary>Lekérdezés - Kérelmek</summary>
        [Description("Lekérdezés - Kérelmek")]
        ALFoKe,

        #endregion Fogvatartott kérelmeinek lekérdezése

        #region Fogvatartott technikai eszköz adatainak megjelenítése

        /// <summary>Lekérdezés - Fogvatartott technikai eszközeinek lekérdezése</summary>
        [Description("Lekérdezés - Fogvatartott technikai eszközeinek lekérdezése")]
        ALFoTeEs,

        #endregion Fogvatartott technikai eszköz adatainak megjelenítése

        #region Fogvatartottról készült vélemények

        /// <summary>Lekérdezés - Vélemények lekérdezése - Fogvatartott lista</summary>
        [Description("Lekérdezés - Vélemények lekérdezése - Fogvatartott lista")]
        ALVelFog,

        /// <summary>Lekérdezés - Vélemények lekérdezése - Vélemények lista</summary>
        [Description("Lekérdezés - Vélemények lekérdezése - Vélemények lista")]
        ALVeVe,

        #endregion Fogvatartottról készült vélemények


        #region Fogvatartott átmeneti részleg adatai

        /// <summary>Fogvatartott átmeneti részleg adatának lekérdezése - Fogvatartott lista</summary>
        [Description("Fogvatartott átmeneti részleg adatának lekérdezése - Fogvatartott lista")]
        ALFoAtCsAdFo,

        /// <summary>Fogvatartott átmeneti részleg adatának lekérdezése - Átmeneti részleg adatok lista</summary>
        [Description("Fogvatartott átmeneti részleg adatának lekérdezése - Átmeneti részleg adatok lista")]
        ALFoAtCsAdAtCsAd,

        #endregion Fogvatartott átmeneti részleg adatai

        #region Fogvatartott jutalmainak lekérdezése

        /// <summary>Lekérdezés - Fogvatartott jutalmai - Fogvatartott lista</summary>
        [Description("Lekérdezés - Fogvatartott jutalmai - Fogvatartott lista")]
        ALFoJuFo,

        /// <summary>Lekérdezés - Fogvatartott jutalmai - Fogvatartott jutalmai</summary>
        [Description("Lekérdezés - Fogvatartott jutalmai - Fogvatartott jutalmai")]
        ALFoJuFoJu,

        /// <summary>Lekérdezés - Fogvatartott jutalmai - Összevont jutalmak</summary>
        [Description("Lekérdezés - Fogvatartott jutalmai - Összevont jutalmak")]
        ALFoJuOsJu,

        #endregion Fogvatartott jutalmainak lekérdezése

        #endregion Egy fogvatartottra vonatkozó adatok

        #region Fogvatartott csoportra vonatkozó adatok

        #region Adott időszakban munkáltatott fogvatartottak

        /// <summary>Lekérdezés - Adott időszakban munkáltatott fogvatartottak</summary>
        [Description("Lekérdezés - Adott időszakban munkáltatott fogvatartottak")]
        ALMuFoAdId,

        #endregion Adott időszakban munkáltatott fogvatartottak

        #region Szabálysértők névsora, jelentés az elzárási hat-ról

        /// <summary>Lekérdezés - Szabálysértők</summary>
        [Description("Lekérdezés - Szabálysértők")]
        ALSzSe,

        #endregion Szabálysértők névsora, jelentés az elzárási hat-ról

        #region Bűntársi csoportok lekérdezése

        /// <summary>Lekérdezés - Bűntársi csoportok</summary>
        [Description("Lekérdezés - Bűntársi csoportok")]
        ALBuCs,

        /// <summary>Lekérdezés - Bűntársi csoport tagok</summary>
        [Description("Lekérdezés - Bűntársi csoport tagok")]
        ALBuCsTa,

        #endregion Bűntársi csoportok lekérdezése

        #region Többletinformációval rendelkező ftt

        /// <summary>Lekérdezés - Többletinformációval rendelkező fogvatartottak</summary>
        [Description("Lekérdezés - Többletinformációval rendelkező fogvatartottak")]
        ALToInReFo,

        #endregion Többletinformációval rendelkező ftt

        #region Végrehajtási fokozat változás lekérdezése

        /// <summary>Lekérdezés - Végrehajtási fokozatok - Végrehajtási fokozat változások</summary>
        [Description("Lekérdezés - Végrehajtási fokozatok - Végrehajtási fokozat változások")]
        ALVeFoVa,

        /// <summary>Lekérdezés - Végrehajtási fokozatok - Végrehajtási fokozatok</summary>
        [Description("Lekérdezés - Végrehajtási fokozatok - Végrehajtási fokozatok")]
        ALVeFo,

        #endregion Végrehajtási fokozat változás lekérdezése

        #region Végrehajtási fokozat lekérdezése

        /// <summary>Lekérdezés - Végrehajtási fokozatok - Fogvatartottak</summary>
        [Description("Lekérdezés - Végrehajtási fokozatok - Fogvatartottak")]
        ALVeFoFo,

        #endregion Végrehajtási fokozat lekérdezése

        #region Távollevők névsora

        /// <summary>Lekérdezés - Távollévők - Távollétek</summary>
        [Description("Lekérdezés - Távollévők - Távollétek")]
        ALTavTavolletek,

        /// <summary>Lekérdezés - Távollévők - Távollévők</summary>
        [Description("Lekérdezés - Távollévők - Távollévők")]
        ALTavTavollevok,

        #endregion Távollevők névsora

        #region Jogellenesen távollévő fogvatartottak

        /// <summary>Lekérdezés - Jogellenesen távollévők</summary>
        [Description("Lekérdezés - Jogellenesen távollévők")]
        ALJoTa,

        #endregion Jogellenesen távollévő fogvatartottak

        #region Gondnokság alá helyezett fogvatartottak
        /// <summary>Lekérdezés - Gondnokság alá helyezettek - Fogvatartott lista</summary>
        [Description("Lekérdezés - Gondnokság alá helyezettek - Fogvatartott lista")]
        ALGoAlHeFo,

        /// <summary>Lekérdezés - Gondnokság alá helyezettek - Gondnokság alá helyezés lista</summary>
        [Description("Lekérdezés - Gondnokság alá helyezettek - Gondnokság alá helyezés lista")]
        ALGoAlHeGoAlHe,
        #endregion Gondnokság alá helyezett fogvatartottak

        #region Szv jegyzéken szereplők

        /// <summary>Lekérdezés - Szv. jegyzéken szereplők lekérdezése</summary>
        [Description("Lekérdezés - Szv. jegyzéken szereplők lekérdezése")]
        ALSzJeSz,

        #endregion Szv jegyzéken szereplők

        #region Behívásra nem jelentkezők
        /// <summary>Lekérdezés - Behívásra nem jelentkezők</summary>
        [Description("Lekérdezés - Behívásra nem jelentkezők")]
        ALBeNeJeSzSz,
        #endregion Behívásra nem jelentkezők

        #region Átmeneti részlegbe helyezettek
        /// <summary>Lekérdezés - Átmeneti részlegbe helyezettek</summary>
        [Description("Lekérdezés - Átmeneti részlegbe helyezettek")]
        ALAtCsHeFo,
        #endregion Átmeneti részlegbe helyezettek

        #region Kényszerítő eszköz alkalmazás - ft. névsor
        /// <summary>Lekérdezés - Kényszerítő eszköz alkalmazása - Fogvatartott lista</summary>
        [Description("Lekérdezés - Kényszerítő eszköz alkalmazása - Fogvatartott lista")]
        ALKeEsAlFo,

        /// <summary>Lekérdezés - Kényszerítő eszköz alkalmazása - Kényszerítő eszköz használati lista</summary>
        [Description("Lekérdezés - Kényszerítő eszköz alkalmazása - Kényszerítő eszköz használati lista")]
        ALKeEsAlKeEsAl,

        #endregion Kényszerítő eszköz alkalmazás - ft. névsor

        #endregion Fogvatartott csoportra vonatkozó adatok

        #region Fogvatartott csoportra vonatkozó nevelési adatok

        #region Kérelmek/panaszok listája

        /// <summary>Lekérdezés - Kérelmek, panaszok</summary>
        [Description("Lekérdezés - Kérelmek, panaszok")]
        ALKePa,

        #endregion Kérelmek/panaszok listája

        #region Lejárt határidejű kérelmek/panaszok

        /// <summary>Lekérdezés - Lejárt határidejű kérelmek, panaszok</summary>
        [Description("Lekérdezés - Lejárt határidejű kérelmek, panaszok")]
        ALLeHaKP,

        #endregion Lejárt határidejű kérelmek/panaszok

        #region Fogvatartottak kapcsolattartói

        /// <summary>Lekérdezés - Fogvatartott kapcsolattartói - Fogvatartott lista"</summary>
        [Description("Lekérdezés - Fogvatartott kapcsolattartói - Fogvatartott lista")]
        ALFoKaFo,

        /// <summary>Lekérdezés - Fogvatartott kapcsolattartói - Kiválasztott fogvatartott kapcsolattartói</summary>
        [Description("Lekérdezés - Fogvatartott kapcsolattartói - Kiválasztott fogvatartott kapcsolattartói")]
        ALFoKaKiFoKa,

        /// <summary>Lekérdezés - Fogvatartott kapcsolattartói - Kiválasztott kapcsolattartó kapcsolattartási módjai</summary>
        [Description("Lekérdezés - Fogvatartott kapcsolattartói - Kiválasztott kapcsolattartó kapcsolattartási módjai")]
        ALFoKaKiKaKaMo,

        /// <summary>Lekérdezés - Fogvatartott kapcsolattartói - Kiválasztott kapcsolattartó korlátozásai</summary>
        [Description("Lekérdezés - Fogvatartott kapcsolattartói - Kiválasztott kapcsolattartó korlátozásai")]
        ALFoKaKiKaKo,

        #endregion Fogvatartottak kapcsolattartói

        #region Adott időszakban csomagot kaptak

        /// <summary>Lekérdezés - Adott időszakban kaptak csomagot</summary>
        [Description("Lekérdezés - Adott időszakban kaptak csomagot")]
        ALAdIdKaCsFo,

        #endregion Adott időszakban csomagot kaptak

        #region Adott időszakban van visszaküldött csomagja

        /// <summary>Lekérdezés - Adott időszakban van visszaküldött csomagja</summary>
        [Description("Lekérdezés - Adott időszakban van visszaküldött csomagja")]
        ALAdIdVaViCsFo,

        #endregion Adott időszakban van visszaküldött csomagja

        #region Adott időszakban nem kaptak csomagot

        /// <summary>Lekérdezés - Adott időszakban nem kaptak csomagot</summary>
        [Description("Lekérdezés - Adott időszakban nem kaptak csomagot")]
        ALAdIdNeKaCsFo,

        #endregion Adott időszakban nem kaptak csomagot

        #region Megtörtént látogatások intézeten belül

        /// <summary>Lekérdezés - Megtörtént látogatások intézeten belül - Látogatás lista</summary>
        [Description("Lekérdezés - Megtörtént látogatások intézeten belül - Látogatás lista")]
        ALMeLaInBeLa,

        /// <summary>Lekérdezés - Megtörtént látogatások intézeten belül - Látogatók lista</summary>
        [Description("Lekérdezés - Megtörtént látogatások intézeten belül - Látogatók lista")]
        ALMeLaInBeLak,

        #endregion Megtörtént látogatások intézeten belül

        #region Fogvatartottak tisztségei

        /// <summary>Lekérdezés - Fogvatartott tisztségeinek lekérdezése - Fogvatartott tisztségei lista</summary>
        [Description("Lekérdezés - Fogvatartott tisztségeinek lekérdezése - Fogvatartott tisztségei lista")]
        ALFoTiFoTi,

        #endregion Fogvatartottak tisztségei

        #region Foglalkozások listája

        /// <summary>Lekérdezés - Foglalkozások listája - Foglalkozás lista</summary>
        [Description("Lekérdezés - Foglalkozások listája - Foglalkozás lista")]
        ALFoFo,

        /// <summary>Lekérdezés - Foglalkozások listája - Foglalkozás résztvevőinek listája</summary>
        [Description("Lekérdezés - Foglalkozások listája - Foglalkozás résztvevőinek listája")]
        ALFoFoRe,

        #endregion Foglalkozások listája

        #region Gyógyító terápiás részlegba helyezettek

        /// <summary>Lekérdezés - Gyógyító terápiás részlegba helyezettek lista</summary>
        [Description("Lekérdezés - Gyógyító terápiás részlegba helyezettek lista")]
        ALGyCsHe,

        #endregion Gyógyító terápiás részlegba helyezettek

        #region Drog prev. csoportba helyezettek

        /// <summary>Lekérdezés - Drog-prevenciós részlegbe helyezettek</summary>
        [Description("Lekérdezés - Drog-prevenciós részlegbe helyezettek")]
        ALDrPrCsHeFo,

        #endregion Drog prev. csoportba helyezettek

        #region Pszicho-szociális részlegbe helyezettek

        /// <summary>Lekérdezés - Pszicho-szociális részlegbe helyezettek</summary>
        [Description("Lekérdezés - Pszicho-szociális részlegbe helyezettek")]
        ALPszSzRHeFo,

        #endregion Pszicho-szociális részlegbe helyezettek

        #region Újságrendelések

        /// <summary>Lekérdezés - Újságrendelések</summary>
        [Description("Lekérdezés - Újságrendelések")]
        ALUjRe,

        #endregion Újságrendelések

        #region Technikai eszközök adatainak megjelenítése
        /// <summary>Lekérdezés - Technikai eszközök adatainak megjelenítése - Technikai eszköz tartás lista</summary>
        [Description("Lekérdezés - Technikai eszközök adatainak megjelenítése - Technikai eszköz tartás lista")]
        ALTeEsAdMeTeEsTa,

        /// <summary>Technikai eszközök tartásának listája részletek</summary>
        [Description("Technikai eszközök tartásának listája részletek")]
        AFFATeEsTaR,
        #endregion Technikai eszközök adatainak megjelenítése

        #region Kimutatás technikai eszközökről

        /// <summary>Lekérdezés - Kimutatás technikai eszközökről - Összesített kimutatás lista</summary>
        [Description("Lekérdezés - Kimutatás technikai eszközökről - Összesített kimutatás lista")]
        ALKiTeEsOsKi,

        /// <summary>Lekérdezés - Kimutatás technikai eszközökről - Technikai eszköz tartás lista</summary>
        [Description("Lekérdezés - Kimutatás technikai eszközökről - Technikai eszköz tartás lista")]
        ALKiTeEsTeEsTa,

        #endregion Kimutatás technikai eszközökről

        #region Lista tisztálkodási cikkek igényléséről

        /// <summary>Lekérdezés - Lista tisztálkodási cikkek igényléséről</summary>
        [Description("Lekérdezés - Lista tisztálkodási cikkek igényléséről")]
        ALLiTiCiIg,

        #endregion Lista tisztálkodási cikkek igényléséről

        #region Lista tisztálkodási cikkek kiadásáról
        /// <summary>Lekérdezés - Lista tisztálkodási cikkek kiadásáról</summary>
        [Description("Lekérdezés - Lista tisztálkodási cikkek kiadásáról")]
        ALLiTiCiKi,
        #endregion Lista tisztálkodási cikkek kiadásáról

        #region Kiétkezéssel kapcsolatos ügyek

        /// <summary>Lekérdezés - Kiétkezéssel kapcsolatos ügyek - Fogvatartottak listája</summary>
        [Description("Lekérdezés - Kiétkezéssel kapcsolatos ügyek - Fogvatartottak listája")]
        ALKiKaUgFo,

        /// <summary>Lekérdezés - Kiétkezéssel kapcsolatos ügyek - Ügyek listája</summary>
        [Description("Lekérdezés - Kiétkezéssel kapcsolatos ügyek - Ügyek listája")]
        ALKiKaUgKi,

        #endregion Kiétkezéssel kapcsolatos ügyek



        #endregion Fogvatartott csoportra vonatkozó nevelési adatok

        #region Elhelyezéssel kapcsolatos lekérdezések

        #region Fogvatartott zárkatársainak lekérdezése

        /// <summary>Lekérdezés - Fogvatartottak zárkatársai - Fogvatartott lista</summary>
        [Description("Lekérdezés - Fogvatartottak zárkatársai - Fogvatartott lista")]
        ALAdIdZaHF,

        /// <summary>Lekérdezés - Fogvatartottak zárkatársai - Fogvatartott elhelyezései lista</summary>
        [Description("Lekérdezés - Fogvatartottak zárkatársai - Fogvatartott elhelyezései lista")]
        ALFoZaHeAI,

        /// <summary>Lekérdezés - Fogvatartottak zárkatársai - Fogvatartott zárkatársai lista</summary>
        [Description("Lekérdezés - Fogvatartottak zárkatársai - Fogvatartott zárkatársai lista")]
        ALFoZaTaAI,

        #endregion Fogvatartott zárkatársainak lekérdezése

        #region Zárkába helyezések lekérdezése

        /// <summary>Lekérdezés - Új zárkába helyezések - Fogvatartottak</summary>
        [Description("Lekérdezés - Új zárkába helyezések - Fogvatartottak")]
        ALUjZaHe,

        #endregion Zárkába helyezések lekérdezése

        #region Adott zárka fogvatartottjai

        /// <summary>Lekérdezés - Adott zárka fogvatartottjai</summary>
        [Description("Lekérdezés - Adott zárka fogvatartottjai")]
        ALAdZaFo,

        #endregion Adott zárka fogvatartottjai

        #region Reintegrációs részleg vagy zárka adat hiány

        /// <summary>Lekérdezés - Reintegrációs részleg vagy zárka adat hiányok - Fogvatartottak</summary>
        [Description("Lekérdezés - Reintegrációs részleg vagy zárka adat hiányok - Fogvatartottak")]
        ALNeCsVZAH,

        #endregion Reintegrációs részleg vagy zárka adat hiány

        #region Szabad zárkahelyek

        /// <summary>Lekérdezés - Szabad zárkahelyek</summary>
        [Description("Lekérdezés - Szabad zárkahelyek")]
        ALSzZaHe,

        #endregion Szabad zárkahelyek

        #region Reintegrációs részleg tagjai

        /// <summary>Lekérdezés - Reintegrációs részlegbe helyezettek</summary>
        [Description("Lekérdezés - Reintegrációs részlegbe helyezettek")]
        ALNeCSoHe,

        #endregion Reintegrációs részleg tagjai

        #region Dohányzási szokás lekérdezése

        /// <summary>Lekérdezés - Fogvatartottak dohányzási szokásai</summary>
        [Description("Lekérdezés - Fogvatartottak dohányzási szokásai")]
        ALFoDoSz,

        #endregion Dohányzási szokás lekérdezése

        #region Zárkák dohányzási jellegének története

        /// <summary>Lekérdezés - Zárkák dohányzás jellegének története</summary>
        [Description("Lekérdezés - Zárkák dohányzás jellegének története")]
        ALZaDoJeTo,

        /// <summary>Lekérdezés - Zárkalakók</summary>
        [Description("Lekérdezés - Zárkalakók")]
        ALZaLaAdId,

        #endregion Zárkák dohányzási jellegének története

        #endregion Elhelyezéssel kapcsolatos lekérdezések

        #region Határidő figyelés

        #region Előzetes letartóztatás lejár

        /// <summary>Lekérdezés - Előzetes letartóztatások</summary>
        [Description("Lekérdezés - Előzetes letartóztatások")]
        ALLeElLe,

        #endregion Előzetes letartóztatás lejár

        #region Nem jogerős ítélete lejár

        /// <summary>Lekérdezés - Lejáró nem jogerős ítéletek</summary>
        [Description("Lekérdezés - Lejáró nem jogerős ítéletek")]
        ALLeNeJoIt,

        #endregion Nem jogerős ítélete lejár

        #region IKGYK lejár,KGYK felülv. esedékes

        /// <summary>Lekérdezés - Lejáró gyógykezelések</summary>
        [Description("Lekérdezés - Lejáró gyógykezelések")]
        ALLeGyKe,

        #endregion IKGYK lejár,KGYK felülv. esedékes

        #region Elévültté tehető elzár.hat.-Tájékoztatás

        /// <summary>Lekérdezés - Elévültté tehető elzárási határozatok</summary>
        [Description("Lekérdezés - Elévültté tehető elzárási határozatok")]
        ALElTeElHa,

        #endregion Elévültté tehető elzár.hat.-Tájékoztatás

        #region Bf értesítés esedékes

        /// <summary>Lekérdezés - Bf. értesítés esedékes</summary>
        [Description("Lekérdezés - Bf. értesítés esedékes")]
        ALBuFeErEs,

        #endregion Bf értesítés esedékes

        #region Érvényes csomagküldési engedély

        /// <summary>Lekérdezés - Érvényes csomagküldési engedélyek - Fogvatartott lista</summary>
        [Description("Lekérdezés - Érvényes csomagküldési engedélyek - Fogvatartott lista")]
        ALErCsEnFo,

        /// <summary>Lekérdezés - Érvényes csomagküldési engedélyek - Kapcsolattartó lista</summary>
        [Description("Lekérdezés - Érvényes csomagküldési engedélyek - Kapcsolattartó lista")]
        ALErCsEnKaLi,

        /// <summary>Lekérdezés - Érvényes csomagküldési engedélyek - Csomagküldési engedély lista</summary>
        [Description("Lekérdezés - Érvényes csomagküldési engedélyek - Csomagküldési engedély lista")]
        ALErCsEnCsEn,

        /// <summary>Lekérdezés - Érvényes csomagküldési engedélyek - Csomagküldési engedélyre érkezett csomag lista</summary>
        [Description("Lekérdezés - Érvényes csomagküldési engedélyek - Csomagküldési engedélyre érkezett csomag lista")]
        ALErCsEnCsEnErCs,


        #endregion Érvényes csomagküldési engedély

        #region Érvényes látogatási engedély

        /// <summary>Lekérdezés - Érvényes látogatási engedélyek - Fogvatartott lista</summary>
        [Description("Lekérdezés - Érvényes látogatási engedélyek - Fogvatartott lista")]
        ALErLaEnFo,

        /// <summary>Lekérdezés - Érvényes látogatási engedélyek - Engedélyezett látogató lista</summary>
        [Description("Lekérdezés - Érvényes látogatási engedélyek - Engedélyezett látogató lista")]
        ALErLaEnEnLa,

        #endregion Érvényes látogatási engedély

        #region Befogadások
        /// <summary>Lekérdezés - Befogadások</summary>
        [Description("Lekérdezés - Befogadások")]
        ALBe,
        #endregion Befogadások

        #region Távozások lekérdezése

        #region Szabadulók, szakterületek értesítése

        /// <summary>Lekérdezés - Szabadulók</summary>
        [Description("Lekérdezés - Szabadulók")]
        ALSz,

        #endregion Szabadulók, szakterületek értesítése

        #region Jóváhagyott szállítási előjegyzések

        /// <summary>Lekérdezés - Jóváhagyott szállítási előjegyzések</summary>
        [Description("Lekérdezés - Jóváhagyott szállítási előjegyzések")]
        ALJoSzEl,

        #endregion Jóváhagyott szállítási előjegyzések

        #region Várható előállítandók, szakterületek értesítése

        /// <summary>Lekérdezés - Várható előállítandók</summary>
        [Description("Lekérdezés - Várható előállítandók")]
        ALVaElAl,

        #endregion Várható előállítandók, szakterületek értesítése

        #region Jóváhagyott eltávozási és bf. előjegyzések

        /// <summary>Lekérdezés - Jóváhagyott Eltávozás, Bf. Előjegyzések</summary>
        [Description("Lekérdezés - Jóváhagyott Eltávozás, Bf. Előjegyzések")]
        ALJoElBfEl,

        #endregion Jóváhagyott eltávozási és bf. előjegyzések

        #endregion Távozások lekérdezése

        #region Esedékes ügyek

        #region Befogadási Bizottság elé állítandók

        /// <summary>Lekérdezés - Befogadó Bizottsági ülésre kijelöltek</summary>
        [Description("Lekérdezés - Befogadó Bizottsági ülésre kijelöltek")]
        ALBeBiUlKi,

        #endregion Befogadási Bizottság elé állítandók

        #region Feltételes szabadulás

        /// <summary>Lekérdezés - Feltételesen szabadulók</summary>
        [Description("Lekérdezés - Feltételesen szabadulók")]
        ALFeSz,

        #endregion Feltételes szabadulás

        #region Elutasított feltételes előterjesztése

        /// <summary>Lekérdezés - Elutasított feltételes előterjesztések</summary>
        [Description("Lekérdezés - Elutasított feltételes előterjesztések")]
        ALElFeEl,

        #endregion Elutasított feltételes előterjesztése

        #region Biztonsági csoport felülvizsgálata

        /// <summary>Lekérdezés - Biztonsági csoport felülvizsgálat esedékes</summary>
        [Description("Lekérdezés - Biztonsági csoport felülvizsgálat esedékes")]
        ALBiCsFeEs,

        #endregion Biztonsági csoport felülvizsgálata

        #region Rezsim kategória felülvizsgálata

        /// <summary>Lekérdezés - Rezsim kategória felülvizsgálat esedékes</summary>
        [Description("Lekérdezés - Rezsim kategória felülvizsgálat esedékes")]
        ALRezsKatFeEs,

        #endregion Rezsim kategória felülvizsgálata

        #region Átmeneti részlegbe helyezés esedékes

        /// <summary>Lekérdezés - Átmeneti részlegbe helyezés esedékes</summary>
        [Description("Lekérdezés - Átmeneti részlegbe helyezés esedékes")]
        ALAtCsHeEsFo,

        #endregion Átmeneti részlegbe helyezés esedékes

        #region Kiadás lejár

        /// <summary>Lekérdezés - Kiadás lejár</summary>
        [Description("Lekérdezés - Kiadás lejár")]
        ALKiLe,

        #endregion Kiadás lejár

        #region Fényképük nem készült el

        /// <summary>Lekérdezés - Fényképük nem készült el</summary>
        [Description("Lekérdezés - Fényképük nem készült el")]
        ALFeNeKeElFe,

        #endregion Fényképük nem készült el

        #region Elintézendő ügyek listája

        /// <summary>Lekérdezés - Elintézendő ügyek - Fogvatartott lista</summary>
        [Description("Lekérdezés - Elintézendő ügyek - Fogvatartott lista")]
        ALElUgFo,

        /// <summary>Lekérdezés - Elintézendő ügyek - Elintézendő ügyek lista</summary>
        [Description("Lekérdezés - Elintézendő ügyek - Elintézendő ügyek lista")]
        ALElUgElUg,

        #endregion Elintézendő ügyek listája

        #endregion Esedékes ügyek

        #endregion Határidő figyelés

        #region Listák

        #region Beszámítások lekérdezése
        /// <summary>Lekérdezés - Ítélet beszámítások</summary>
        [Description("Lekérdezés - Ítélet beszámítások")]
        ALItBe,
        #endregion Beszámítások lekérdezése

        #region Élelmezési norma adatok lekérdezése
        /// <summary>Lekérdezés - Élelmezési norma adatok - Fogvatartott lista</summary>
        [Description("Lekérdezés - Élelmezési norma adatok - Fogvatartott lista")]
        ALElNoAdFo,

        /// <summary>Lekérdezés - Élelmezési norma adatok - Élelmezési norma lista</summary>
        [Description("Lekérdezés - Élelmezési norma adatok - Élelmezési norma lista")]
        ALElNoAdElNo,
        #endregion Élelmezési norma adatok lekérdezése

        #region Bv. ügyek leválogatása különböző szempontok szerint
        /// <summary>Lekérdezés - Büntetés végrehajtási ügyek leválogatása</summary>
        [Description("Lekérdezés - Büntetés végrehajtási ügyek leválogatása")]
        ALBvUgLKSS,
        #endregion Bv. ügyek leválogatása különböző szempontok szerint

        #region Idézések státusz szerinti listája

        /// <summary>Lekérdezés - Idézések státusz szerint</summary>
        [Description("Lekérdezés - Idézések státusz szerint")]
        ALIdStSz,

        #endregion Idézések státusz szerinti listája

        #region EVSZ-be helyezettek

        /// <summary>Lekérdezés - Enyhébb végrehajtási szabályok szerint elhelyezettek</summary>
        [Description("Lekérdezés - Enyhébb végrehajtási szabályok szerint elhelyezettek")]
        ALEnVeSzSE,

        #endregion EVSZ-be helyezettek

        #region Fogvatartottak szállítási előjegyzései

        /// <summary>Lekérdezés - Fogvatartottak szállítási előjegyzései</summary>
        [Description("Lekérdezés - Fogvatartottak szállítási előjegyzései")]
        ALFoSzEl,

        #endregion Fogvatartottak szállítási előjegyzései

        #region Foglalkoztatással kapcsolatos

        #region Jelenleg dolgozók listája

        /// <summary>Lekérdezés - Jelenleg dolgozók</summary>
        [Description("Lekérdezés - Jelenleg dolgozók")]
        ALJeDo,

        #endregion Jelenleg dolgozók listája

        #region Dolgozók és munkára előjegyzettek

        /// <summary>Lekérdezés - Dolgozók és munkára előjegyzettek</summary>
        [Description("Lekérdezés - Dolgozók és munkára előjegyzettek")]
        ALDoEsMuEl,

        #endregion Dolgozók és munkára előjegyzettek

        #region Nem dolgozók névsora

        /// <summary>Lekérdezés - Nemdolgozók</summary>
        [Description("Lekérdezés - Nemdolgozók")]
        ALNeDo,

        #endregion Nem dolgozók névsora

        #region Beiskolázott fogvatartottak listája

        /// <summary>Lekérdezés - Beiskolázottak</summary>
        [Description("Lekérdezés - Beiskolázottak")]
        ALBei,

        #endregion Beiskolázott fogvatartottak listája

        #region Munkáltatási adatok lekérdezése

        /// <summary>Lekérdezés - Munkáltatási adatok</summary>
        [Description("Lekérdezés - Munkáltatási adatok")]
        ALMuAd,

        #endregion Munkáltatási adatok lekérdezése

        #endregion Foglalkoztatással kapcsolatos

        #region Engedélyezett kilépők

        /// <summary>Lekérdezés - Engedélyezett kilépők</summary>
        [Description("Lekérdezés - Engedélyezett kilépők")]
        ALEnKi,

        #endregion Engedélyezett kilépők

        #region Fogvatartottak távozási előjegyzései

        /// <summary>Lekérdezés - - Fogvatartott távozási előjegyzései</summary>
        [Description("Lekérdezés - Fogvatartott távozási előjegyzései")]
        ALFoTaEl,

        #endregion Fogvatartottak távozási előjegyzései

        #region Folyamatban lévő egyéb ügyek, függő ügyek, új eljárások

        /// <summary>Lekérdezés - Folyamatban lévő egyéb ügyek lekérdezése - Fogvatartott lista</summary>
        [Description("Lekérdezés - Folyamatban lévő egyéb ügyek lekérdezése - Fogvatartott lista")]
        ALEgUgFo,

        /// <summary>Lekérdezés - Folyamatban lévő egyéb ügyek lekérdezése - Folyamatban levő -,függő ügyek, új eljárások lista</summary>
        [Description("Lekérdezés - Folyamatban lévő egyéb ügyek lekérdezése - Folyamatban levő -,függő ügyek, új eljárások lista")]
        ALEgUgFoLeFuUgUjEl,

        #endregion Folyamatban lévő egyéb ügyek, függő ügyek, új eljárások

        #region Nyomozásra kikérés lekérdezése

        /// <summary>Lekérdezés - Nyomozásra kikérés</summary>
        [Description("Lekérdezés - Nyomozásra kikérés")]
        ALNyKiPo,

        #endregion Nyomozásra kikérés lekérdezése

        #region Nem jogerősen elítéltek - nje. ítéletek

        /// <summary>Lekérdezés - Nem jogerős ítéletek</summary>
        [Description("Lekérdezés - Nem jogerős ítéletek")]
        ALNeJoIt,

        #endregion Nem jogerősen elítéltek - nje. ítéletek



        #endregion Listák

        #region Fegyelmi ügyek

        #region Folyamatban lévő fegyelmi ügyek

        /// <summary>Lekérdezés - Fegyelmi ügyek folyamatban - Fegyelmi ügy lista</summary>
        [Description("Lekérdezés - Fegyelmi ügyek folyamatban - Fegyelmi ügy lista")]
        ALFeUgFoFeUg,

        /// <summary>Lekérdezés - Fegyelmi ügyek folyamatban - Kapcsolódó adat lista</summary>
        [Description("Lekérdezés - Fegyelmi ügyek folyamatban - Kapcsolódó adat lista")]
        ALFeUgFoUgKaAd,

        /// <summary>Lekérdezés - Fegyelmi ügyek folyamatban - Fegyelmi ügyhöz kapcsolódó nyomtatványok lista</summary>
        [Description("Lekérdezés - Fegyelmi ügyek folyamatban - Fegyelmi ügyhöz kapcsolódó nyomtatványok lista")]
        ALFeUgFoFeUgKaNy,

        #endregion Folyamatban lévő fegyelmi ügyek

        #region Fegyelmi lapok események alapján

        /// <summary>Lekérdezés - Fegyelmi ügyek események alapján - Rendkívüli esemény lista</summary>
        [Description("Lekérdezés - Fegyelmi ügyek események alapján - Rendkívüli esemény lista")]
        ALFeUgEsAlReEs,

        /// <summary>Lekérdezés - Fegyelmi ügyek események alapján - Fegyelmi ügy lista</summary>
        [Description("Lekérdezés - Fegyelmi ügyek események alapján - Fegyelmi ügy lista")]
        ALFeUgEsAlFeUg,

        #endregion Fegyelmi lapok események alapján

        #region Fegyelmi lapok ft. névsor alapján

        /// <summary>Lekérdezés - Fogvatartottak fegyelmi ügyei - Fogvatartott lista</summary>
        [Description("Lekérdezés - Fogvatartottak fegyelmi ügyei - Fogvatartott lista")]
        ALFoFeUgFo,

        /// <summary>Lekérdezés - Fogvatartottak fegyelmi ügyei - Fegyelmi ügy lista</summary>
        [Description("Lekérdezés - Fogvatartottak fegyelmi ügyei - Fegyelmi ügy lista")]
        ALFoFeUgFeUg,

        #endregion Fegyelmi lapok ft. névsor alapján

        #region Fegyelmi ügyek elintézési módok szerint

        /// <summary>Lekérdezés - Fegyelmi ügyek elintézési mód (státusz) szerint - Fegyelmi ügy lista</summary>
        [Description("Lekérdezés - Fegyelmi ügyek elintézési mód (státusz) szerint - Fegyelmi ügy lista")]
        ALFeUgElMoSzFeUg,

        #endregion Fegyelmi ügyek elintézési módok szerint

        #region Fegyelmi üggyel kapcsolatos feljelentések

        /// <summary>Lekérdezés - Fegyelmi üggyel kapcsolatos feljelentések - Fogvatartott lista</summary>
        [Description("Lekérdezés - Fegyelmi üggyel kapcsolatos feljelentések - Fogvatartott lista")]
        ALFeUgKaFeFo,

        /// <summary>Lekérdezés - Fegyelmi üggyel kapcsolatos feljelentések - fegyelmi ügy lista</summary>
        [Description("Lekérdezés - Fegyelmi üggyel kapcsolatos feljelentések - fegyelmi ügy lista")]
        ALFeUgKaFeFe,

        #endregion Fegyelmi üggyel kapcsolatos feljelentések

        #region Kényszerítő eszköz alkalmazás esetei

        /// <summary>Lekérdezés - Kényszerítő eszköz alkalmazás eseteinek lekérdezése</summary>
        [Description("Lekérdezés - Kényszerítő eszköz alkalmazás eseteinek lekérdezése")]
        ALKeEsAlEs,

        #endregion Kényszerítő eszköz alkalmazás esetei

        #endregion Fegyelmi ügyek

        #region Jutalmak listája

        /// <summary>Lekérdezés - Jutalmak listája - Jutalmazás lista</summary>
        [Description("Lekérdezés - Jutalmak listája - Jutalmazás lista")]
        ALJuLiJu,

        /// <summary>Lekérdezés - Jutalmak listája - Összevont jutalmak</summary>
        [Description("Lekérdezés - Jutalmak listája - Összevont jutalmak")]
        ALJuLiOsJu,

        #endregion Jutalmak listája

        #region Intézeti névsor

        /// <summary>Lekérdezés - Intézeti névsor</summary>
        [Description("Lekérdezés - Intézeti névsor")]
        ALInNeFo,

        #endregion Intézeti névsor

        #region Intézeti létszám készítése névsorral és objektummal

        /// <summary>Létszám adatok lekérdezése</summary>
        [Description("Létszám adatok lekérdezése")]
        ALLetszam,

        /// <summary>Létszám adatok lekérdezése - Fogvatartottak listája</summary>
        [Description("Létszám adatok lekérdezése - Fogvatartottak listája")]
        ALLetszamFtLi,

        /// <summary>Létszám lista</summary>
        [Description("Létszám lista")]
        ALLetszamFeLi,

        #endregion Intézeti létszám készítése névsorral és objektummal

        #region Összevont névsor

        #region Összesített létszámjelentés készítése névsorral



        #endregion Összesített létszámjelentés készítése névsorral

        #endregion Összevont névsor


        #region Alkalmazás oldal elérés

        /// <summary>Lekérdezés - Alkalmazás oldal elérés</summary>
        [Description("Lekérdezés - Alkalmazás oldal elérés")]
        ALAlOlEl,

        /// <summary>Alkalmazás oldal elérés részletek</summary>
        [Description("Alkalmazás oldal elérés részletek")]
        AFAlOlElR,

        #endregion Alkalmazás oldal elérés


        #endregion lekérdezés jogosultságok

        #region Rendszerfunkciók

        #region Jogosultság

        /// <summary>Személyzeti csoport felvitele</summary>
        [Description("Személyzeti csoport felvitele")]
        ATSzCsFe,

        /// <summary>Személyzeti csoport lista</summary>
        [Description("Személyzet lista")]
        ATSzLi,

        /// <summary>Szemelyzeti csoport részletes adatai</summary>
        [Description("Szemelyzet részletes adatai")]
        ATSzRe,

        /// <summary>Személyzeti csoport módosítás</summary>
        [Description("Személyzet módosítás")]
        ATSzMo,

        /// <summary>Személyzeti szerepkör felvitele</summary>
        [Description("Személyzet felvitele")]
        ATSzFe,

        /// <summary>Személyzeti csoport lista</summary>
        [Description("Személyzeti csoport lista")]
        ATSzCsLi,

        /// <summary>Személyzeti csoport módosítás</summary>
        [Description("Személyzeti csoport módosítás")]
        ATSzCsMo,

        /// <summary>Szemelyzeti csoport részletes adatai</summary>
        [Description("Szemelyzeti csoport részletes adatai")]
        ATSzCsRe,

        /// <summary>Személyzeti szerepkör felvitele</summary>
        [Description("Személyzeti szerepkör felvitele")]
        ATSzSzFe,

        /// <summary>Személyzeti szerepkör lista</summary>
        [Description("Személyzeti szerepkör lista")]
        ATSzSzLi,

        /// <summary>Személyzeti szerepkör módosítás</summary>
        [Description("Személyzeti szerepkör módosítás")]
        ATSzSzMo,

        /// <summary>Személyzeti szerepkör részletes adatai</summary>
        [Description("Személyzeti szerepkör részletes adatai")]
        ATSzSzRe,

        #endregion Jogosultság

        #region Funkcióhasználat napló

        /// <summary>Alkalmazás funkció lista</summary>
        [Description("Alkalmazás funkció lista")]
        ATAlFuLi,

        /// <summary>Alkalmazás napló lista</summary>
        [Description("Alkalmazás napló lista")]
        ATAlNaLi,

        /// <summary>Alkalmazás napló részletek</summary>
        [Description("Alkalmazás napló részletek")]
        ATAlNaRe,

        /// <summary>Tranzakció lista</summary>
        [Description("Tranzakció lista")]
        ATTrLi,

        #endregion Funkcióhasználat napló

        #region Fogvatartott nyilvántartási szám adminisztráció

        /// <summary>
        /// Fogvatartott nyilvántartási szám kiadás adatok karbantartása - Nyilvántartási szám lista
        /// </summary>
        [Description("Fogvatartott nyilvántartási szám kiadás adatok karbantartása - Nyilvántartási szám lista")]
        AFFtAzKiadKaNySzLi,

        /// <summary>
        /// Fogvatartott nyilvántartási szám kiadás adatok karbantartása - Nyilvántartási szám felvitel
        /// </summary>
        [Description("Fogvatartott nyilvántartási szám kiadás adatok karbantartása - Nyilvántartási szám felvitel")]
        AFFtAzKiadKaNySzFe,

        /// <summary>
        /// Fogvatartott nyilvántartási szám kiadás adatok karbantartása - Nyilvántartási szám módosítás
        /// </summary>
        [Description("Fogvatartott nyilvántartási szám kiadás adatok karbantartása - Nyilvántartási szám módosítás")]
        AFFtAzKiadKaNySzMo,

        /// <summary>
        /// Fogvatartott nyilvántartási szám kiadás adatok karbantartása - Nyilvántartási szám részletek
        /// </summary>
        [Description("Fogvatartott nyilvántartási szám kiadás adatok karbantartása - Nyilvántartási szám részletek")]
        AFFtAzKiadKaNySzRe,

        #endregion Fogvatartott nyilvántartási szám adminisztráció

        #region alkalmazás kereső
        /// <summary>Kereső - Minta lista"</summary>
        [Description("Kereső - Minta lista")]
        AFAKMiLi,

        /// <summary>Kereső - Minta felvitele</summary>
        [Description("Kereső - Minta felvitele")]
        AFAKMiFe,

        /// <summary>Kereső - Kereső oldal</summary>
        [Description("Kereső - Kereső oldal")]
        AFAKKeres,

        /// <summary>Kereső - Keresés eredménye</summary>
        [Description("Kereső - Keresés eredménye")]
        AFAKKerEr,

        /// <summary>Kereső - Megoszlás megadása</summary>
        [Description("Kereső - Megoszlás megadása")]
        AFAKKerMM,

        /// <summary>Kereső - Megoszlás eredménye</summary>
        [Description("Kereső - Megoszlás eredménye")]
        AFAKKerME,
        #endregion  alkalmazás kereső

        #region Befogadási adatok karbantartása

        /// <summary>
        /// Befogadási adatok karbantartása - fogvatartott lista
        /// </summary>
        [Description("Befogadási adatok karbantartása - fogvatartott lista")]
        AFBakFl,

        /// <summary>
        /// Befogadási adatok karbantartása - befogadás lista
        /// </summary>
        [Description("Befogadási adatok karbantartása - befogadás lista")]
        AFBakBl,

        /// <summary>
        /// Befogadási adatok karbantartása - befogadás részletek
        /// </summary>
        [Description("Befogadási adatok karbantartása - befogadás részletek")]
        AFBakBr,

        /// <summary>
        /// Befogadási adatok karbantartása - befogadás felvitel
        /// </summary>
        [Description("Befogadási adatok karbantartása - befogadás felvitel")]
        AFBakBf,

        /// <summary>
        /// Befogadási adatok karbantartása - befogadás módosítás
        /// </summary>
        [Description("Befogadási adatok karbantartása - befogadás módosítás")]
        AFBakBm,

        /// <summary>
        /// Befogadási adatok karbantartása - befogadás törlés
        /// </summary>
        [Description("Befogadási adatok karbantartása - befogadás törlés")]
        AFBakBt,

        #endregion  Befogadási adatok karbantartása

        #region Zárkába helyezés adatok karbantartása

        /// <summary>
        /// Zárkába helyezés adatok karbantartása - fogvatartott lista
        /// </summary>
        [Description("Zárkába helyezés adatok karbantartása - fogvatartott lista")]
        AFZhadFl,

        /// <summary>
        /// Zárkába helyezés adatok karbantartása - zárkába helyezés lista
        /// </summary>
        [Description("Zárkába helyezés adatok karbantartása - zárkába helyezés lista")]
        AFZhadZhl,

        /// <summary>
        /// Zárkába helyezés adatok karbantartása - zárkába helyezés részletek
        /// </summary>
        [Description("Zárkába helyezés adatok karbantartása - zárkába helyezés részletek")]
        AFZhadZhr,

        /// <summary>
        /// Zárkába helyezés adatok karbantartása - zárkába helyezés törlés
        /// </summary>
        [Description("Zárkába helyezés adatok karbantartása - zárkába helyezés törlés")]
        AFZhadZht,

        /// <summary>
        /// Zárkába helyezés adatok karbantartása - zárkába helyezés felvitel
        /// </summary>
        [Description("Zárkába helyezés adatok karbantartása - zárkába helyezés felvitel")]
        AFZhadZhf,

        #endregion Zárkába helyezés adatok karbantartása

        #region Törzsadatok lekérdezése
        #endregion Törzsadatok lekérdezése

        #region Intézeti törzsadatok karbantartása

        #region Intézeti-objektum, körlet, zárka

        /// <summary>Intézeti objektum felvitel</summary>
        [Description("Intézeti objektum felvitel")]
        ATInObFe,

        /// <summary>Intézeti objektum lista</summary>
        [Description("Intézeti objektum lista")]
        ATInObLi,

        /// <summary>Intézeti objektum módosítása</summary>
        [Description("Intézeti objektum módosítása")]
        ATInObMo,

        /// <summary>Intézeti objektum törlése</summary>
        [Description("Intézeti objektum törlése")]
        ATInObT,

        /// <summary>Intézeti objektum részletes adatai</summary>
        [Description("Intézeti objektum részletes adatai")]
        ATInObRe,

        /// <summary>Körlet felvitel</summary>
        [Description("Körlet felvitel")]
        ATKoFe,

        /// <summary>Körlet lista</summary>
        [Description("Körlet lista")]
        ATKoLi,

        /// <summary>Körlet módosítás</summary>
        [Description("Körlet módosítás")]
        ATKoMo,

        /// <summary>Körlet törlése</summary>
        [Description("Körlet törlése")]
        ATKoT,

        /// <summary>Körlet részletes adatai</summary>
        [Description("Körlet részletes adatai")]
        ATKoRe,

        /// <summary>Zárka felvitel</summary>
        [Description("Zárka felvitel")]
        ATZaFe,

        /// <summary>Zárka lista</summary>
        [Description("Zárka lista")]
        ATZaLi,

        /// <summary>Zárka módosítás</summary>
        [Description("Zárka módosítás")]
        ATZaMo,

        /// <summary>Zárka törlése</summary>
        [Description("Zárka törlése")]
        ATZaT,

        /// <summary>Zárka részletes adatai</summary>
        [Description("Zárka részletes adatai")]
        ATZaRe,

        #endregion Intézeti-objektum, körlet, zárka

        #region Reintegrációs részleg

        /// <summary>Reintegrációs részleg felvitel</summary>
        [Description("Reintegrációs részleg felvitel")]
        ATNeCsFe,

        /// <summary>Reintegrációs részleg lista</summary>
        [Description("Reintegrációs részleg lista")]
        ATNeCsLi,

        /// <summary>Reintegrációs részleg módosítás</summary>
        [Description("Reintegrációs részleg módosítás")]
        ATNeCsMo,

        /// <summary>Reintegrációs részleg törlése</summary>
        [Description("Reintegrációs részleg törlése")]
        ATNeCsT,

        /// <summary>Reintegrációs részleg részletes adatai</summary>
        [Description("Reintegrációs részleg részletes adatai")]
        ATNeCsRe,

        #endregion Reintegrációs részleg

        #region Munkahely

        /// <summary>Munkahely felvitel</summary>
        [Description("Munkahely felvitel")]
        ATMhFe,

        /// <summary>Munkahely lista</summary>
        [Description("Munkahely lista")]
        ATMhLi,

        /// <summary>Munkahely módosítás</summary>
        [Description("Munkahely módosítás")]
        ATMhMo,

        /// <summary>Munkahely részletes adatai</summary>
        [Description("Munkahely részletes adatai")]
        ATMhRe,

        /// <summary>Munkahely törlése</summary>
        [Description("Munkahely törlése")]
        ATMhTo,

        #endregion Munkahely

        #region Munkakör
        /// <summary>Munkakör felvitel</summary>
        [Description("Munkakör felvitel")]
        ATMkrFe,

        /// <summary>Munkakör lista</summary>
        [Description("Munkakör lista")]
        ATMkrLi,

        /// <summary>Munkakör módosítás</summary>
        [Description("Munkakör módosítás")]
        ATMkrMo,

        /// <summary>Munkakör részletes adatai</summary>
        [Description("Munkakör részletes adatai")]
        ATMkrRe,

        /// <summary>Munkakör törlése</summary>
        [Description("Munkakör törlése")]
        ATMkrTo,

        #endregion Munkakör

        #region Szabad szöveg sablonok

        /// <summary>Szabad szöveg sablon lista</summary>
        [Description("Szabad szöveg sablon lista")]
        ATSzaSaLi,

        /// <summary>Szabad szöveg sablon részletek</summary>
        [Description("Szabad szöveg sablon részletek")]
        ATSzaSaRe,

        /// <summary>Szabad szöveg sablon felvétele</summary>
        [Description("Szabad szöveg sablon felvétele")]
        ATSzaSaFe,

        /// <summary>Szabad szöveg sablon módosítása</summary>
        [Description("Szabad szöveg sablon módosítása")]
        ATSzaSaMo,

        /// <summary>Szabad szöveg sablon törlése</summary>
        [Description("Szabad szöveg sablon törlése")]
        ATSzaSaTo,

        #endregion Szabad szöveg sablonok

        #endregion Intézeti törzsadatok karbantartása

        #region Mintatár

        /// <summary>Mintatár - Dokumentumsablon lista</summary>
        [Description("Mintatár - Dokumentumsablon lista")]
        AFMinDsL,

        /// <summary>Mintatár - Dokumentumsablon részletek</summary>
        [Description("Mintatár - Dokumentumsablon részletek")]
        AFMinDsR,

        /// <summary>Mintatár - Dokumentumsablon módosítás</summary>
        [Description("Mintatár - Dokumentumsablon módosítás")]
        AFMinDsM,

        #endregion Mintatár

        #endregion Rendszerfunkciók

        #region Központi nyilvántartó

        #region Fogvatartott adatai


        #region Összes személy

        /// <summary>Lekérdezés - Összes személy lista</summary>
        [Description("Lekérdezés - Összes személy lista")]
        ALOsSz,

        /// <summary>Lekérdezés - Összes fogvatartás lista</summary>
        [Description("Lekérdezés - Összes fogvatartás lista")]
        ALOsFo,

        /// <summary>Fogvatartotti információk</summary>
        [Description("Fogvatartotti információk")]
        AFFtInfo,

        #endregion Összes személy

        #endregion Fogvatartott adatai

        #region Fegyelmi fenyítések

        /// <summary>
        /// Fegyelmi statisztika - Fenyítések
        /// </summary>
        [Description("Fegyelmi statisztika - Fenyítések")]
        ASFeStatFeny,

        #endregion Fegyelmi fenyítések

        #region Adatszolgáltatások

        /// <summary>
        /// Adatszolgáltatás nyomozó ügyészségnek
        /// </summary>
        [Description("Adatszolgáltatás nyomozó ügyészségnek")]
        AFAdASzolgNyomUgy,

        /// <summary>
        /// Külföldiek változott adatai (BÁH-nak)
        /// </summary>
        [Description("Külföldiek változott adatai (BÁH-nak)")]
        AFJelKulAdatValt,

        /// <summary>
        /// Jogerős ítéletet kezdő külföldiek (KIM-nek)
        /// </summary>
        [Description("Jogerős ítéletet kezdő külföldiek (KIM-nek)")]
        AFJelKulJeItKezd,

        /// <summary>
        /// Jogerős elítéltek változott adatai (BÁH-nak)
        /// </summary>
        [Description("Jogerős elítéltek változott adatai (BÁH-nak)")]
        AFJelKulJeItValt,

        /// <summary>
        /// Jelentés a nem magyar állampolgárságú fogvatartottak várható szabadulásáról
        /// </summary>
        [Description("Jelentés a nem magyar állampolgárságú fogvatartottak várható szabadulásáról")]
        AFJelKulVarSzab,

        /// <summary>
        /// Választási névjegyzékhez adatszolgáltatás
        /// </summary>
        [Description("Választási névjegyzékhez adatszolgáltatás")]
        AFAdSzolgValNevj,

        /// <summary>
        /// Személyi adatok a KEKKH-tól az intézeteknek
        /// </summary>
        [Description("Személyi adatok a KEKKH-tól az intézeteknek")]
        AFSzemAdatKekkhKny,

        /// <summary>
        /// Fogvatartott személy adatai KEKKH szerint
        /// </summary>
        [Description("Fogvatartott személy adatai KEKKH szerint")]
        AFSzemAdatKekkhInt,

        /// <summary>
        /// Fogvatartott személy adatai KEKKH szerint - Fogvatartott lista
        /// </summary>
        [Description("Fogvatartott személy adatai KEKKH szerint - Fogvatartott lista")]
        AFSzemAdatKekkhIFtLi,

        /// <summary>
        /// Adatszolgáltatás a hadköteles fogvatartottakról
        /// </summary>
        [Description("Adatszolgáltatás a hadköteles fogvatartottakról")]
        AFAdSzolgHadKotFt,

        /// <summary>
        /// Adatszolgáltatás az Európa Tanács SPACE kérdőívéhez
        /// </summary>
        [Description("Adatszolgáltatás az Európa Tanács SPACE kérdőívéhez")]
        AFAdSzolgEtSpace,

        /// <summary>
        /// Munkaügyi központoknak szabadulók adatai
        /// </summary>
        [Description("Munkaügyi központoknak szabadulók adatai")]
        AFAdSzolgMuKoSzabAd,

        #endregion Adatszolgáltatások

        #region Statisztika

        #region Létszám

        /// <summary>
        /// Létszám statisztika
        /// </summary>
        [Description("Létszám statisztika")]
        ASLetszam,

        /// <summary>
        /// Létszám statisztika
        /// </summary>
        [Description("Létszám statisztika leíró lista")]
        ATLetszamLeLi,

        /// <summary>
        /// Létszám statisztik
        /// a leíró felvitel
        /// </summary>
        [Description("Létszám statisztika leíró felvitel")]
        ATLetszamLeFe,

        /// <summary>
        /// Létszám statisztika leíró módosítás
        /// </summary>
        [Description("Létszám statisztika leíró módosítás")]
        ATLetszamLeMo,

        /// <summary>
        /// Létszám statisztika leíró törlés
        /// </summary>
        [Description("Létszám statisztika leíró törlés")]
        ATLetszamLeTo,

        #endregion Létszám

        #region Létszám Fejléc

        /// <summary>
        /// Létszám fejléc lista
        /// </summary>
        [Description("Létszám fejléc lista")]
        ASLetszFeLi,

        /// <summary>
        /// Létszám fejléc felvitel
        /// </summary>
        [Description("Létszám fejléc felvitel")]
        ASLetszFeFe,

        /// <summary>
        /// Létszám fejléc módosítás
        /// </summary>
        [Description("Létszám fejléc módosítás")]
        ASLetszFeMo,

        /// <summary>
        /// Létszám fejléc törlés
        /// </summary>
        [Description("Létszám fejléc törlés")]
        ASLetszFeTo,

        /// <summary>
        /// Létszám fejléc részletek
        /// </summary>
        [Description("Létszám fejléc részletek")]
        ASLetszFeRe,

        #endregion Létszám Fejléc

        #region Mentett Létszám

        /// <summary>
        /// Mentett létszám lista
        /// </summary>
        [Description("Mentett létszám lista")]
        ASMeLeLi,

        /// <summary>
        /// Mentett létszám felvitel
        /// </summary>
        [Description("Mentett létszám felvitel")]
        ASMeLeFe,

        /// <summary>
        /// Mentett létszám módosítás
        /// </summary>
        [Description("Mentett létszám módosítás")]
        ASMeLeMo,

        /// <summary>
        /// Mentett létszám törlés
        /// </summary>
        [Description("Mentett létszám törlés")]
        ASMeLeTo,

        /// <summary>
        /// Mentett létszám részletek
        /// </summary>
        [Description("Mentett létszám részletek")]
        ASMeLeRe,

        #endregion Mentett Létszám

        #region Fegyelmi vétségek
        /// <summary>
        /// Fegyelmi statisztika – Fegyelmi vétségek
        /// </summary>
        [Description("Fegyelmi statisztika – Fegyelmi vétségek")]
        ASFeStatFeVets,
        #endregion Fegyelmi vétségek

        #region Fegyelemsértések
        /// <summary>
        /// Fegyelmi statisztika - Fegyelemsértések
        /// </summary>
        [Description("Fegyelmi statisztika - Fegyelemsértések")]
        ASFeStatFeSert,
        #endregion Fegyelemsértések

        #region  Jutalmazás
        /// <summary>
        /// Jutalmazási statisztika
        /// </summary>
        [Description("Jutalmazási statisztika")]
        ASJutStat,
        #endregion Jutalmazás

        #endregion Statisztika

        #region Ítélet adatokból kigyűjtés

        /// <summary>Lekérdezés - Ítélet adatok - Fogvatartott lista</summary>
        [Description("Lekérdezés - Ítélet adatok - Fogvatartott lista")]
        ALItAdFo,

        /// <summary>Lekérdezés - Ítélet adatok - Fogvatartott lista</summary>
        [Description("Lekérdezés - Ítélet adatok - Ítélet lista")]
        ALItAdIt,

        #endregion Ítélet adatokból kigyűjtés

        #region Jelentés a szabálysértőkről

        /// <summary>
        /// Jelentés nyomtatása szabálysértőkről
        /// </summary>
        [Description("Jelentés nyomtatása szabálysértőkről")]
        AFJelSzabsertNyom,

        #endregion Jelentés a szabálysértőkről

        #region Előzetes/elítéltekkel kapcsolatos lekérdezések

        #region Előzetes letartóztatást elrendelők megye/város szerint

        /// <summary>Lekérdezés - Előzetes letartóztatások - Megyék</summary>
        [Description("Lekérdezés - Előzetes letartóztatások - Megyék")]
        ALElLeEMMS,

        /// <summary>Lekérdezés - Előzetes letartóztatások - Városok</summary>
        [Description("Lekérdezés - Előzetes letartóztatások - Városok")]
        ALElLeMeLS,

        #endregion Előzetes letartóztatást elrendelők megye/város szerint

        #region Bűncselekmények azonos paragrafus szerint

        /// <summary>Lekérdezés - Bűncselekmény - Paragrafus csoport</summary>
        [Description("Lekérdezés - Bűncselekmény - Paragrafus csoport")]
        ALBuAPSBFC,

        /// <summary>Lekérdezés - Bűncselekmény - Azonos paragrafus szerint fogvatartottak</summary>
        [Description("Lekérdezés - Bűncselekmény - Azonos paragrafus szerint fogvatartottak")]
        ALBuAzPaSF,

        /// <summary>Lekérdezés - Bűncselekmény - Paragrafusok részletezése</summary>
        [Description("Lekérdezés - Bűncselekmény - Paragrafusok részletezése")]
        ALBuAzPSPR,

        #endregion Bűncselekmények azonos paragrafus szerint

        #region Előállítások partnerenként/intézetenként

        /// <summary>Lekérdezés - Előállítandók partnerenként, intézetenként - Fogvatartottak</summary>
        [Description("Lekérdezés - Előállítandók partnerenként, intézetenként - Fogvatartottak")]
        ALElPaIF,

        /// <summary>Lekérdezés - Előállítandók partnerenként, intézetenként - Előállítások</summary>
        [Description("Lekérdezés - Előállítandók partnerenként, intézetenként - Előállítások")]
        ALElPaIE,

        #endregion Előállítások partnerenként/intézetenként

        #endregion Előzetes/elítéltekkel kapcsolatos lekérdezések

        #region Fogvatartottak találkozása
        /// <summary>Lekérdezés - Fogvatartottak találkozása</summary>
        [Description("Lekérdezés - Fogvatartottak találkozása")]
        ALFoTa,
        #endregion Fogvatartottak találkozása

        #region Nevelési adatok

        #region Kapcsolattartók
        /// <summary>Lekérdezés - Kapcsolattartók - Kapcsolattartó</summary>
        [Description("Lekérdezés - Kapcsolattartók - Kapcsolattartó")]
        ALKaKa,

        /// <summary>Lekérdezés - Kapcsolattartók - Kiválasztott kapcsolattartóhoz tartozó fogvatartottak</summary>
        [Description("Lekérdezés - Kapcsolattartók - Kiválasztott kapcsolattartóhoz tartozó fogvatartottak")]
        ALKaFo,

        /// <summary>Lekérdezés - Kapcsolattartók - Kiválasztott kapcsolattartó korlátozásai</summary>
        [Description("Lekérdezés - Kapcsolattartók - Kiválasztott kapcsolattartó korlátozásai")]
        ALKaKaKo,
        #endregion Kapcsolattartók

        #endregion Nevelési adatok

        #region Távollétek adott időszakban

        /// <summary>Lekérdezés - Távollétek adott időszakban - Távollévő fogvatartottak</summary>
        [Description("Lekérdezés - Távollétek adott időszakban - Távollévő fogvatartottak")]
        ALTaAdIdTaFo,

        /// <summary>Lekérdezés - Távollétek adott időszakban - Távollétek listája</summary>
        [Description("Lekérdezés - Távollétek adott időszakban - Távollétek listája")]
        ALTaAdIdTaLi,

        #endregion Távollétek adott időszakban

        #region Befogadások, szabadulások lekérdezése

        /// <summary>Lekérdezés - Befogadások, szabadulások - Fogvatartott lista</summary>
        [Description("Lekérdezés - Befogadások, szabadulások - Fogvatartott lista")]
        ALBeSzFo,

        /// <summary>Lekérdezés - Befogadások, szabadulások - Befogadás lista</summary>
        [Description("Lekérdezés - Befogadások, szabadulások - Befogadás lista")]
        ALBeSzBe,

        #endregion Befogadások, szabadulások lekérdezése

        #region Fogvatartottak zárkatársai
        /// <summary>Lekérdezés - Fogvatartott jelenlegi zárkatársai - Fogvatartott zárkatársai</summary>
        [Description("Lekérdezés - Fogvatartott jelenlegi zárkatársai - Fogvatartott zárkatársai")]
        ALZaTa,

        #endregion Fogvatartottak zárkatársai

        #region Munkáltatási, oktatási adatok lekérdezése

        #region Munkáltatási események (adott időszakban)



        #endregion Munkáltatási események (adott időszakban)

        #endregion Munkáltatási, oktatási adatok lekérdezése

        #region Határidős feladatok lekérdezés

        /// <summary>Lekérdezés - Határidős feladatok - Fogvatartott lista</summary>
        [Description("Lekérdezés - Határidős feladatok - Fogvatartott lista")]
        ALHaFeFo,

        /// <summary>Lekérdezés - Határidős feladatok - Elintézendő ügyek listája</summary>
        [Description("Lekérdezés - Határidős feladatok - Elintézendő ügyek listája")]
        ALHaFeElUg,

        #endregion Határidős feladatok lekérdezés

        #region Kérelem panasz intézése BvOP-n

        /// <summary>
        /// Kérelem panasz intézése BvOP-n - Fogvatartott lista
        /// </summary>
        [Description("Kérelem panasz intézése BvOP-n - Fogvatartott lista")]
        AFKPIBvFL,

        /// <summary>
        /// Kérelem panasz intézése BvOP-n - Kérelem panasz lista
        /// </summary>
        [Description("Kérelem panasz intézése BvOP-n - Kérelem panasz lista")]
        AFKPIBvKPL,

        /// <summary>
        /// Kérelem panasz intézése BvOP-n - Kérelem panasz részletek
        /// </summary>
        [Description("Kérelem panasz intézése BvOP-n - Kérelem panasz részletek")]
        AFKPIBvKPR,

        /// <summary>
        /// Kérelem panasz intézése BvOP-n - Kérelem panasz módosítása
        /// </summary>
        [Description("Kérelem panasz intézése BvOP-n - Kérelem panasz módosítása")]
        AFKPIBvKPM,

        #endregion  Kérelem panasz intézése BvOP-n

        #region Szállítás

        #region Szállítási igények lekérdezése

        /// <summary>Lekérdezés - Szállítási igények lekérdezése</summary>
        [Description("Lekérdezés - Szállítási igények lekérdezése")]
        ALSzalIgLek,

        #endregion Szállítási igények lekérdezése

        #region Szállítási adatok lekérdezése
        /// <summary>Lekérdezés - Szállítás összesítés lista</summary>
        [Description("Lekérdezés - Szállítás összesítés lista")]
        ALSzalOsszLi,

        /// <summary>Lekérdezés - Összes szállítás lista</summary>
        [Description("Lekérdezés - Összes szállítás lista")]
        AlOsszesSzallLi,

        /// <summary>Lekérdezés - Elmenő fogvatartott létszám lista</summary>
        [Description("Lekérdezés - Elmenő fogvatartott létszám lista")]
        ALElmFtLetszLi,

        /// <summary>Lekérdezés - Érkező fogvatartott létszám lista</summary>
        [Description("Lekérdezés - Érkező fogvatartott létszám lista")]
        ALErkFtLetszLi,

        /// <summary>Lekérdezés - Szállítás összesítés lista - Fogvatartott lista</summary>
        [Description("Lekérdezés - Szállítás összesítés lista - Fogvatartott lista")]
        ALSzalOsszFtLi,

        #endregion Szállítási adatok lekérdezése

        #region Szállítás célintézetének megadása

        /// <summary>
        /// Szállítás célintézetének megadása - fogvatarott lista
        /// </summary>
        [Description("Szállításszervezés - Fogvatartott lista")]
        AFSzcmFl,
        /// <summary>
        /// Szállításszervezés - megőrzésre/megőrzésről vissza szállításra előjegyzett fogvatartottak
        /// </summary>
        [Description("Szállításszervezés - megőrzésre/megőrzésről vissza szállításra előjegyzett fogvatartottak")]
        AFSzcmMoSzl,
        /// <summary>
        /// Szállításszervezés - Jelentés ellenőzés lista
        /// </summary>
        [Description("Szállításszervezés - Jelentés ellenőzés lista")]
        AFSzcmJel,
        /// <summary>
        /// Szállítás célintézetének megadása - célintézet megadása
        /// </summary>
        [Description("Szállítás célintézetének megadása - célintézet megadása")]
        AFSzcmF,

        #endregion  Szállítás célintézetének megadása

        #endregion Szállítás

        #region Rendszerfunkciók

        #region Irattár nyíltvántartás, belég készítés

        #region Fogvatartás rögzítése irattárból

        /// <summary>
        /// Fogvatartás rögzítése irattárból - Fogvatartott személy lista
        /// </summary>
        [Description("Fogvatartás rögzítése irattárból - Fogvatartott személy lista")]
        AFFtRogIrFoSzLi,

        /// <summary>
        /// Fogvatartás rögzítése irattárból - Irattári anyag ellenőrzésének beállítása
        /// </summary>
        [Description("Fogvatartás rögzítése irattárból - Irattári anyag ellenőrzésének beállítása")]
        AFFtRogIrEllBe,

        /// <summary>
        /// Fogvatartás rögzítése irattárból - Személy fogvatartásai lista
        /// </summary>
        [Description("Fogvatartás rögzítése irattárból - Személy fogvatartásai lista")]
        AFFtRogIrFtLi,

        /// <summary>
        /// 'Fogvatartás rögzítése irattárból - Részletek
        /// </summary>
        [Description("'Fogvatartás rögzítése irattárból - Részletek")]
        AFFtRogIrRe,

        /// <summary>
        /// 'Fogvatartás rögzítése irattárból - Felvitel
        /// </summary>
        [Description("'Fogvatartás rögzítése irattárból - Felvitel")]
        AFFtRogIrFe,

        /// <summary>
        /// 'Fogvatartás rögzítése irattárból - Módosítás
        /// </summary>
        [Description("'Fogvatartás rögzítése irattárból - Módosítás")]
        AFFtRogIrMo,

        /// <summary>
        /// 'Fogvatartás rögzítése irattárból - Törlés
        /// </summary>
        [Description("'Fogvatartás rögzítése irattárból - Törlés")]
        AFFtRogIrTo,

        #endregion Fogvatartás rögzítése irattárból

        #region Belég készítése

        /// <summary>
        /// Belég készítése - Fogvatartott lista
        /// </summary>
        [Description("Belég készítése - Fogvatartott lista")]
        AFBelKeFtLi,

        /// <summary>
        /// Belég készítése - Előző fogvatartások lista
        /// </summary>
        [Description("Belég készítése - Előző fogvatartások lista")]
        AFBelKeElFtLi,

        /// <summary>
        /// Belég készítése - Belég nyomtatás
        /// </summary>
        [Description("Belég készítése - Belég nyomtatás")]
        AFBelKeNyo,

        /// <summary>
        /// Belég készítése - Belég küldés
        /// </summary>
        [Description("Belég készítése - Belég küldés")]
        AFBelKeKul,

        /// <summary>
        /// Belég készítése - Névsor nyomtatása
        /// </summary>
        [Description("Belég készítése - Névsor nyomtatása")]
        AFBelKeNevNyo,

        /// <summary>
        /// Belég készítése - Belég adatai
        /// </summary>
        [Description("Belég készítése - Belég adatai")]
        AFBelKeAdLi,

        /// <summary>
        /// Belég készítése - Felvitel
        /// </summary>
        [Description("Belég készítése - Felvitel")]
        AFBelKeFe,

        /// <summary>
        /// Belég készítése - Választás vélhetően azonos személyek közül
        /// </summary>
        [Description("Belég készítése - Választás vélhetően azonos személyek közül")]
        AFBelKeVaVelAzonSz,

        /// <summary>
        /// Belég készítése - Választás a végleg távozottak közül
        /// </summary>
        [Description("Belég készítése - Választás a végleg távozottak közül")]
        AFBelKeVaVegTav,

        /// <summary>
        /// Belég készítése - Törlés
        /// </summary>
        [Description("Belég készítése - Törlés")]
        AFBelKeTo,

        #endregion Belég készítése

        #endregion Irattár nyíltvántartás, belég készítés

        #region Időigazolás készítése

        /// <summary>
        /// Időigazolás készítése - Fogvatartott lista
        /// </summary>
        [Description("Időigazolás készítése - Fogvatartott lista")]
        AFIdoIgKeszFszLi,

        /// <summary>
        /// Időigazolás készítése - Előző fogvatartások lista
        /// </summary>
        [Description("Időigazolás készítése - Előző fogvatartások lista")]
        AFIdoIgKeszFtLi,

        /// <summary>
        /// Időigazolás készítése - Nyomtatás
        /// </summary>
        [Description("Időigazolás készítése - Nyomtatás")]
        AFIdoIgKeszNyom,

        #endregion Időigazolás készítése

        #region Törzsadat karbantartás

        #region Bv. Intézet

        /// <summary>Intézet felvitel</summary>
        [Description("Intézet felvitel")]
        ATInFe,

        /// <summary>Intézet lista</summary>
        [Description("Intézet lista")]
        ATInLi,

        /// <summary>Intézet módosítása</summary>
        [Description("Intézet módosítása")]
        ATInMo,

        /// <summary>Intézet törlése</summary>
        [Description("Intézet törlése")]
        ATInTo,

        /// <summary>Intézet részletes adatai</summary>
        [Description("Intézet részletes adatai")]
        ATInRe,

        #endregion Bv. Intézet

        #region Partner szervezetek

        /// <summary>Partner szervezet felvitele</summary>
        [Description("Partner szervezet felvitele")]
        ATPaSzFe,

        /// <summary>Partner szervezet lista</summary>
        [Description("Partner szervezet lista")]
        ATPaSzLi,

        /// <summary>Partner szervezet módosítása</summary>
        [Description("Partner szervezet módosítása")]
        ATPaSzMo,

        /// <summary>Partner szervezet törlése</summary>
        [Description("Partner szervezet törlése")]
        ATPaSzT,

        /// <summary>Partner szervezet részletes adatai</summary>
        [Description("Partner szervezet részletes adatai")]
        ATPaSzRe,


        #endregion Partner szervezetek

        #region Bűncselekmények

        /// <summary>Bűncselekmény felvitele</summary>
        [Description("Bűncselekmény felvitele")]
        ATBuFe,

        /// <summary>Bűncselekmény lista</summary>
        [Description("Bűncselekmény lista")]
        ATBuLi,

        /// <summary>Bűncselekmény törlése</summary>
        [Description("Bűncselekmény törlése")]
        ATBuT,

        /// <summary>Bűncselekmény módosítása</summary>
        [Description("Bűncselekmény módosítása")]
        ATBuMo,

        /// <summary>Bűncselekmény részletes adatai</summary>
        [Description("Bűncselekmény részletes adatai")]
        ATBuRe,

        #endregion Bűncselekmények

        #region Helység

        /// <summary>Helység felvitele</summary>
        [Description("Helység felvitele")]
        ATHeFe,

        /// <summary>Helység lista</summary>
        [Description("Helység lista")]
        ATHeLi,

        /// <summary>Helység módosítása</summary>
        [Description("Helység módosítása")]
        ATHeMo,

        /// <summary>Helység törlése</summary>
        [Description("Helység törlése")]
        ATHeTo,

        /// <summary>Helység részletes adatai</summary>
        [Description("Helység részletes adatai")]
        ATHeRe,

        #endregion Helység

        #region Kódszótár

        /// <summary>Kódszótár csoport felvitel</summary>
        [Description("Kódszótár csoport felvitel")]
        ATKsCsFe,

        /// <summary>Kódszótár csoport lista</summary>
        [Description("Kódszótár csoport lista")]
        ATKsCsLi,

        /// <summary>Kódszótár csoport módosítása</summary>
        [Description("Kódszótár csoport módosítása")]
        ATKsCsMo,

        /// <summary>Kódszótár csoport törlése</summary>
        [Description("Kódszótár csoport törlése")]
        ATKsCsT,

        /// <summary>Kódszótár csoport részletes adatai</summary>
        [Description("Kódszótár csoport részletes adatai")]
        ATKsCsRe,

        /// <summary>Kódszótár felvitel</summary>
        [Description("Kódszótár felvitel")]
        ATKsFe,

        /// <summary>Kódszótár lista</summary>
        [Description("Kódszótár lista")]
        ATKsLi,

        /// <summary>Kódszótár módosítás</summary>
        [Description("Kódszótár módosítás")]
        ATKsMo,

        /// <summary>Kódszótár törlése</summary>
        [Description("Kódszótár törlése")]
        ATKsT,

        /// <summary>Kódszótár részletes adatai</summary>
        [Description("Kódszótár részletes adatai")]
        ATKsRe,


        #endregion Kódszótár

        #region Szállító járművek

        /// <summary>Szállító járművek listája lista</summary>
        [Description("Szállító járművek listája lista")]
        AFFASzJaLiViMo,

        /// <summary>Szállító járművek listája részletek</summary>
        [Description("Szállító járművek listája részletek")]
        AFFASzJaR,

        /// <summary>Szállító jármű felvétele</summary>
        [Description("Szállító jármű felvétele")]
        AFFASzJaFe,

        /// <summary>Szállító jármű módosítása</summary>
        [Description("Szállító jármű módosítása")]
        AFFASzJaMo,

        /// <summary>Szállító jármű törlése</summary>
        [Description("Szállító jármű törlése")]
        AFFASzJaTo,

        #endregion Szállító járművek



        #endregion Törzsadat karbantartás

        #endregion Rendszerfunkciók

        #endregion Központi nyilvántartó

        #region Maradék

        /// <summary>Fogvatartott részletek</summary>
        [Description("Fogvatartott részletek")]
        AFFoAlRe,

        /// <summary>Engedély - Globális jogosultságot adhat</summary>
        [Description("Engedély - Globális jogosultságot adhat")]
        AEGlbJogAd,

        /// <summary>Engedély - Védett személyeket láthat</summary>
        [Description("Engedély - Védett személyeket láthat")]
        AEVedSzLat,

        /// <summary>Engedély - Szabad szöveget olvashat</summary>
        [Description("Engedély - Szabad szöveget olvashat")]
        AESzSzoLat,

        /// <summary>Engedély - Személyzeti szerepkör kezelés adminisztrátorként</summary>
        [Description("Engedély - Személyzeti szerepkör kezelés adminisztrátorként")]
        AESzSzKeAd,

        #region Nyomtatvány funkciók

        /// <summary>
        /// Fegyelmi lap nyomtatása
        /// </summary>
        [Description("Fegyelmi lap nyomtatása")]
        AFFegyLapNyom,

        /// <summary>
        /// Fogvatartotti információk nyomtatása
        /// </summary>
        [Description("Fogvatartotti információk nyomtatása")]
        AFFogvInfNyom,


        /// <summary>
        /// Kérelem/panasz nyomtatása
        /// </summary>
        [Description("Kérelem/panasz nyomtatása")]
        AfKerPanNyom,

        /// <summary>
        /// Kimaradási/Eltávozási engedélyek nyomtatása
        /// </summary>
        [Description("Kimaradási/Eltávozási engedélyek nyomtatása")]
        AFKimEltErtNyom,







        #endregion

        #region Lekérdezesek


        /// <summary>Lekérdezés - Fogvatartott jelenlegi zárkatársai - Fogvatartottak</summary>
        [Description("Lekérdezés - Fogvatartott jelenlegi zárkatársai - Fogvatartottak")]
        ALZaLeFo,



        /// <summary>Lekérdezés - Fogvatartott munkáltatásai</summary>
        [Description("Lekérdezés - Fogvatartott munkáltatásai")]
        ALFoMu,

        /// <summary>Lekérdezés - Fogvatartottak adatai</summary>
        [Description("Lekérdezés - Fogvatartottak adatai")]
        ALFoAd,







        /// <summary>Lekérdezés - Előállítási utasítás nyomtatása</summary>
        [Description("Lekérdezés - Előállítási utasítás nyomtatása")]
        ALElUt,













        /// <summary>Lekérdezés - Szállító jármű adatok lekérdezése</summary>
        [Description("Lekérdezés - Szállító jármű adatok lekérdezése")]
        ALSzJaAdSzJa,

        /// <summary>Lekérdezés - Összes jelenlegi fogvatartott lista</summary>
        [Description("Lekérdezés - Összes jelenlegi fogvatartott lista")]
        ALOsJeFo,

        /// <summary>Lekérdezés - Vélhetőleg azonos személyek - Fogvatartott lista</summary>
        [Description("Lekérdezés - Vélhetőleg azonos személyek - Fogvatartott lista")]
        ALVeAzSzFo,



        /// <summary>Lekérdezés - Munkakör betöltésére alkalmas fogvatartottak</summary>
        [Description("Lekérdezés - Munkakör betöltésére alkalmas fogvatartottak")]
        ALMuKoBeAlkFt,



        /// <summary>Lekérdezés - Bf-re engedett fogvatartottak</summary>
        [Description("Lekérdezés - Bf-re engedett fogvatartottak")]
        ALBfEnFo,



        #endregion Lekérdezesek









        [Description("Befogadási Bizottsági megjelenés előjegyzése több fogvatartottnak")]
        AFBefBizMegjTobb,



        #region Statisztika









        /// <summary>
        /// Kimutatás szállításról
        /// </summary>
        [Description("Kimutatás szállításról")]
        ASSzall,

        /// <summary>
        /// Statisztika – Fogvatartott lista
        /// </summary>
        [Description("Statisztika – Fogvatartott lista")]
        ASFtLi,


        #endregion Statisztika

        [Description("Szállítási igények - Fogvatartott lista")]
        ALSzalIgLekFtLi,

        #endregion Maradék

        #region KEKKH feladás

        /// <summary>KEKKH feladás</summary>
        [Description("KEKKH feladás")]
        AFKEKKH,

        #endregion KEKKH feladás


        /// <summary>Új CsatolmanyTipus felvitele</summary>
        [Description("Új CsatolmanyTipus felvitele")]
        ATCsTiFe,

        /// <summary>Csatolmány típus lista</summary>
        [Description("Csatolmány típus lista")]
        ATCsTiLi,

        /// <summary>Csatolmány típus módosítása</summary>
        [Description("Csatolmány típus módosítása")]
        ATCsTiMo,

        /// <summary>CsatolmanyTipusReszletek</summary>
        [Description("CsatolmanyTipusReszletek")]
        ATCsTiRe,

        /// <summary>Dokumentumok sablon - Fogvatartotti lista</summary>
        [Description("Dokumentumok sablon - Fogvatartotti lista")]
        AFDFoLi,

        /// <summary>Sablonelem felvitele</summary>
        [Description("Sablonelem felvitele")]
        ATDoSaElFe,

        /// <summary>Sablonelem lista</summary>
        [Description("Sablonelem lista")]
        ATDoSaElLi,

        /// <summary>Sablonelem módosítása</summary>
        [Description("Sablonelem módosítása")]
        ATDoSaElMo,

        /// <summary>Sablonelem részletes adatai</summary>
        [Description("Sablonelem részletes adatai")]
        ATDoSaElRe,

        /// <summary>Nyomtatvány felvitele</summary>
        [Description("Nyomtatvány felvitele")]
        ATDoSaFe,

        /// <summary>Nyomtatványok lista</summary>
        [Description("Nyomtatványok lista")]
        ATDoSaLi,

        /// <summary>Nyomtatvány módosítása</summary>
        [Description("Nyomtatvány módosítása")]
        ATDoSaMo,

        /// <summary>Nyomtatvány részletes adatai</summary>
        [Description("Nyomtatvány részletes adatai")]
        ATDoSaRe,


        /// <summary>Nyomtatvány lekérése</summary>
        [Description("Nyomtatvány lekérése")]
        ATNyLe,




        #region fogvatartott adatai
        /// <summary>Átmeneti részlegbe helyezések listája lista</summary>
        [Description("Átmeneti részlegbe helyezések listája lista")]
        AFFAAtCsHeLi101ViMo,

        /// <summary>Átmeneti részlegbe helyezések listája részletek</summary>
        [Description("Átmeneti részlegbe helyezések listája részletek")]
        AFFAAtCsHeR,

        /// <summary>Befogadások listája lista</summary>
        [Description("Befogadások listája lista")]
        AFFABeLi101ViMo,

        /// <summary>Befogadások listája részletek</summary>
        [Description("Befogadások listája részletek")]
        AFFABeR,

        /// <summary>Befogadás Távollétekkel lista</summary>
        [Description("Befogadás Távollétekkel lista")]
        AFFABeTavViMo,

        /// <summary>Biztonsági csorotba sorolás lista</summary>
        [Description("Biztonsági csorotba sorolás lista")]
        AFFABiCsSoLi100ViMo,

        /// <summary>Biztonsági csorotba sorolás részletek</summary>
        [Description("Biztonsági csorotba sorolás részletek")]
        AFFAFoBiCsSoR,

        /// <summary>Bűntársi csoport tagok lista</summary>
        [Description("Bűntársi csoport tagok lista")]
        AFFABuCsTaLi101ViMo,

        /// <summary>Bűntársi csoport tagok részletek</summary>
        [Description("Bűntársi csoport tagok részletek")]
        AFFABuCsTaR,

        /// <summary>Büntetés félbeszakítások lista</summary>
        [Description("Büntetés félbeszakítások lista")]
        AFFABuFeLi101ViMo,

        /// <summary>Büntetés félbeszakítások részletek</summary>
        [Description("Büntetés félbeszakítások részletek")]
        AFFABuFeR,

        /// <summary>Büntetés végrehajtási ügyek lista</summary>
        [Description("Büntetés végrehajtási ügyek lista")]
        AFFABuVeUgLi101ViMo,

        /// <summary>Büntetés végrehajtási ügyek részletek</summary>
        [Description("Büntetés végrehajtási ügyek részletek")]
        AFFABuVeUgR,

        /// <summary>Elkövetett bűncselekmények listája lista</summary>
        [Description("Elkövetett bűncselekmények listája lista")]
        AFFAElBuLi100ViMo,

        /// <summary>Elkövetett bűncselekmények listája részletek</summary>
        [Description("Elkövetett bűncselekmények listája részletek")]
        AFFAElBuR,

        /// <summary>Előállítások listája lista</summary>
        [Description("Előállítások listája lista")]
        AFFAElLi101ViMo,

        /// <summary>Előállítások listája részletek</summary>
        [Description("Előállítások listája részletek")]
        AFFAElR,

        /// <summary>Előzetes letartóztatások listája lista</summary>
        [Description("Előzetes letartóztatások listája lista")]
        AFFAElLeLi100ViMo,

        /// <summary>Előzetes letartóztatások listája részletek</summary>
        [Description("Előzetes letartóztatások listája részletek")]
        AFFAElLeR,

        /// <summary>Eltávozások listája lista</summary>
        [Description("Eltávozások listája lista")]
        AFFAEltLi101ViMo,

        /// <summary>Eltávozások listája részletek</summary>
        [Description("Eltávozások listája részletek")]
        AFFAEltR,

        /// <summary>Elzárási határozat beszámítások listája lista</summary>
        [Description("Elzárási határozat beszámítások listája lista")]
        AFFAElHaBeLi100ViMo,

        /// <summary>Elzárási határozat beszámítások listája részletek</summary>
        [Description("Elzárási határozat beszámítások listája részletek")]
        AFFAElHaBeR,

        /// <summary>Elzárási határozatok listája lista</summary>
        [Description("Elzárási határozatok listája lista")]
        AFFAElHaLi101ViMo,

        /// <summary>Elzárási határozat részletek</summary>
        [Description("Elzárási határozat részletek")]
        AFFAElHaR,

        /// <summary>Fegyelmi fenyítések listája lista</summary>
        [Description("Fegyelmi fenyítések listája lista")]
        AFFAFeFeLi101ViMo,

        /// <summary>Fegyelmi fenyítések listája részletek</summary>
        [Description("Fegyelmi fenyítések listája részletek")]
        AFFAFeFeR,

        /// <summary>Fegyelmi ügyek listája lista</summary>
        [Description("Fegyelmi ügyek listája lista")]
        AFFAFeLi105ViMo,

        /// <summary>Fegyelmi ügyek listája részletek</summary>
        [Description("Fegyelmi ügyek listája részletek")]
        AFFAFeUgR,

        /// <summary>Fenyítés megszakítások listája lista</summary>
        [Description("Fenyítés megszakítások listája lista")]
        AFFAFeMeLi100ViMo,

        /// <summary>Fenyítés megszakítások listája részletek</summary>
        [Description("Fenyítés megszakítások listája részletek")]
        AFFAFeMeR,

        /// <summary>Fogvatartott állampolgárságainak listája lista</summary>
        [Description("Fogvatartott állampolgárságainak listája lista")]
        AFFAFoAlLi100ViMo,

        /// <summary>Fogvatartott állampolgárságainak listája részletek</summary>
        [Description("Fogvatartott állampolgárságainak listája részletek")]
        AFFAFoAlR,

        /// <summary>Fogvatartott szakképzettségeinek listája lista</summary>
        [Description("Fogvatartott szakképzettségeinek listája lista")]
        AFFAFoSzLi101ViMo,

        /// <summary>Fogvatartott szakképzettségeinek listája részletek</summary>
        [Description("Fogvatartott szakképzettségeinek listája részletek")]
        AFFAFoSzR,

        /// <summary>Fogvatartott további neveinek listája lista</summary>
        [Description("Fogvatartott további neveinek listája lista")]
        AFFAFoToNeLi100ViMo,

        /// <summary>Fogvatartott további neveinek listája részletek</summary>
        [Description("Fogvatartott további neveinek listája részletek")]
        AFFAFoToNeR,

        /// <summary>Idezesek listája lista</summary>
        [Description("Idezesek listája lista")]
        AFFAIdLi101ViMo,

        /// <summary>Idezesek listája részletek</summary>
        [Description("Idezesek listája részletek")]
        AFFAIdR,

        /// <summary>Intézkedések, rendelkezések listája lista</summary>
        [Description("Intézkedések, rendelkezések listája lista")]
        AFFAInReLi100ViMo,

        /// <summary>Intézkedések, rendelkezések listája részletek</summary>
        [Description("Intézkedések, rendelkezések listája részletek")]
        AFFAInReR,

        /// <summary>Ítélet beszámítások listája lista</summary>
        [Description("Ítélet beszámítások listája lista")]
        AFFAItBeLi101ViMo,

        /// <summary>Ítélet beszámítások listája részletek</summary>
        [Description("Ítélet beszámítások listája részletek")]
        AFFAItBeR,

        /// <summary>Ítélet lista</summary>
        [Description("Ítélet lista")]
        AFFAItLi101ViMo,

        /// <summary>Ítélet részletek</summary>
        [Description("Ítélet részletek")]
        AFFAItR,

        /// <summary>Itélet megszakítások listája lista</summary>
        [Description("Itélet megszakítások listája lista")]
        AFFAItMeLi100ViMo,

        /// <summary>Itélet megszakítások listája részletek</summary>
        [Description("Itélet megszakítások listája részletek")]
        AFFAItMeR,

        /// <summary>Kapcsolattartás módjainak listája lista</summary>
        [Description("Kapcsolattartás módjainak listája lista")]
        AFFAKaMoLi100ViMo,

        /// <summary>Kapcsolattartás módjainak listája részletek</summary>
        [Description("Kapcsolattartás módjainak listája részletek")]
        AFFAKaMoR,

        /// <summary>Kapcsolattartók listája lista</summary>
        [Description("Kapcsolattartók listája lista")]
        AFFAKaLi102ViMo,

        /// <summary>Kapcsolattartók listája részletek</summary>
        [Description("Kapcsolattartók listája részletek")]
        AFFAKaR,

        /// <summary>Kéremek/panaszok listája</summary>
        [Description("Kéremek/panaszok listája")]
        AFFAKePaLi105ViMo,

        /// <summary>Kérelem/panasz részletes adatok</summary>
        [Description("Kérelem/panasz részletes adatok")]
        AFFAKePaR,

        /// <summary>Korlátozások listája lista</summary>
        [Description("Korlátozások listája lista")]
        AFFAKoLi101ViMo,

        /// <summary>Korlátozások listája részletek</summary>
        [Description("Korlátozások listája részletek")]
        AFFAKoR,

        /// <summary>Látogatási engedélyek listája (100)</summary>
        [Description("Látogatási engedélyek listája (100)")]
        AFFALaEnLi100ViMo,

        /// <summary>Látogatási engedélyek listája (101)</summary>
        [Description("Látogatási engedélyek listája (101)")]
        AFFALaEnLi101ViMo,

        /// <summary>Látogatási engedély részletek</summary>
        [Description("Látogatási engedély részletek")]
        AFFALaEnR,

        /// <summary>Mellékbüntetések beszámításának listája lista</summary>
        [Description("Mellékbüntetések beszámításának listája lista")]
        AFFAMeBeLi101ViMo,

        /// <summary>Mellékbüntetések beszámításának listája részletek</summary>
        [Description("Mellékbüntetések beszámításának listája részletek")]
        AFFAMeBeR,

        /// <summary>Mellékbüntetések listája lista</summary>
        [Description("Mellékbüntetések listája lista")]
        AFFAMeLi101ViMo,

        /// <summary>Mellékbüntetések listája részletek</summary>
        [Description("Mellékbüntetések listája részletek")]
        AFFAMeR,

        /// <summary>Munkára való alkalmasság lista</summary>
        [Description("Munkára való alkalmasság lista")]
        AFFAMuVaAl100ViMo,

        /// <summary>Munkára való alkalmasság részletek</summary>
        [Description("Munkára való alkalmasság részletek")]
        AFFAMuVaAlR,

        /// <summary>Reintegrációs részlegbe helyezések listája lista</summary>
        [Description("Reintegrációs részlegbe helyezések listája lista")]
        AFFANeCsHeLi100ViMo,

        /// <summary>Reintegrációs részlegbe helyezések listája részletek</summary>
        [Description("Reintegrációs részlegbe helyezések listája részletek")]
        AFFANeCsHeR,

        /// <summary>Őrizetbevételek listája lista</summary>
        [Description("Őrizetbevételek listája lista")]
        AFFAOrVeLi100ViMo,

        /// <summary>Őrizetbevételek listája részletek</summary>
        [Description("Őrizetbevételek listája részletek")]
        AFFAOrVeR,

        /// <summary>Pótnyomozások listája lista</summary>
        [Description("Pótnyomozások listája lista")]
        AFFAPoLi102ViMo,

        /// <summary>Pótnyomozások listája részletek</summary>
        [Description("Pótnyomozások listája részletek")]
        AFFAPoR,

        /// <summary>Rendkívüli eseményeken résztvevők listája lista</summary>
        [Description("Rendkívüli eseményeken résztvevők listája lista")]
        AFFAReEsReLi100ViMo,

        /// <summary>Rendkívüli eseményeken résztvevők listája részletek</summary>
        [Description("Rendkívüli eseményeken résztvevők listája részletek")]
        AFFAReEsReR,

        /// <summary>Szabadítások listája lista</summary>
        [Description("Szabadítások listája lista")]
        AFFASzLi100ViMo,

        /// <summary>Szabadítások listája részletek</summary>
        [Description("Szabadítások listája részletek")]
        AFFASzR,

        /// <summary>Szabadságvesztési jegyzék lista</summary>
        [Description("Szabadságvesztési jegyzék lista")]
        AFFASzJeSzLi100ViMo,

        /// <summary>Szabadságvesztési jegyzék részletek</summary>
        [Description("Szabadságvesztési jegyzék részletek")]
        AFFASzJeSzR,

        /// <summary>Szállítások listája lista</summary>
        [Description("Szállítások listája lista")]
        AFFASzallLi102ViMo,

        /// <summary>Szállítás részletek</summary>
        [Description("Szállítás részletek")]
        AFFASzallR,

        /// <summary>Számított szabadulás lista</summary>
        [Description("Számított szabadulás lista")]
        AFFASzSzLi100ViMo,

        /// <summary>Számított szabadulás részletek</summary>
        [Description("Számított szabadulás részletek")]
        AFFASzSzR,

        /// <summary>Távollétek listája lista</summary>
        [Description("Távollétek listája lista")]
        AFFATaLi100ViMo,

        /// <summary>Távollétek listája részletek</summary>
        [Description("Távollétek listája részletek")]
        AFFATaR,

        /// <summary>Befogadó bizottsági megjelenések listája lista</summary>
        [Description("Befogadó bizottsági megjelenések listája lista")]
        AFFABeBiMe101ViMo,

        /// <summary>Befogadó bizottsági megjelenések listája részletek</summary>
        [Description("Befogadó bizottsági megjelenések listája részletek")]
        AFFABeBiMeR,

        ///<summary>Érkezett csomagok listája lista</summary>
        [Description("Érkezett csomagok listája lista")]
        AFFACsErLi101ViMo,

        /// <summary>Érkezett csomagok listája részletek</summary>
        [Description("Érkezett csomagok listája részletek")]
        AFFACsErR,



        ///<summary>Csomagküldési engedélyek listája lista</summary>
        [Description("Csomagküldési engedélyek listája lista")]
        AFFACsEnLi101ViMo,

        /// <summary>Csomagküldési engedélyek listája részletek</summary>
        [Description("Csomagküldési engedélyek listája részletek")]
        AFFACsEnR,


        ///<summary>Dohányzások listája lista</summary>
        [Description("Dohányzások listája lista")]
        AFFADoLi100ViMo,

        /// <summary>Dohányzások listája részletek</summary>
        [Description("Dohányzások listája részletek")]
        AFFADoR,



        ///<summary>Egyéb hivatalos ügyek listája lista</summary>
        [Description("Egyéb hivatalos ügyek listája lista")]
        AFFAEgUgLi100ViMo,

        /// <summary>Egyéb hivatalos ügyek listája részletek</summary>
        [Description("Egyéb hivatalos ügyek listája részletek")]
        AFFAEgUgR,


        ///<summary>Biztonsági elkülönítés listája lista</summary>
        [Description("Biztonsági elkülönítés listája lista")]
        AFFABiElLi101ViMo,

        /// <summary>Biztonsági elkülönítés listája részletek</summary>
        [Description("Biztonsági elkülönítés listája részletek")]
        AFFABiElR,


        ///<summary>Értesítések listája lista</summary>
        [Description("Értesítések listája lista")]
        AFFAErLi101ViMo,

        /// <summary>Értesítések listája részletek</summary>
        [Description("Értesítések listája részletek")]
        AFFAErR,

        /// <summary>Foglalkozás részletek</summary>
        [Description("Foglalkozás részletek")]
        AFFAFoglR,

        ///<summary>Foglalkozáson résztvevőek listája lista</summary>
        [Description("Foglalkozáson résztvevőek listája lista")]
        AFFAFoReLi100ViMo,

        /// <summary>Foglalkozáson résztvevőek listája részletek</summary>
        [Description("Foglalkozáson résztvevőek listája részletek")]
        AFFAFoReR,


        ///<summary>Fogvatartott egészségügyi állapotainak listája lista</summary>
        [Description("Fogvatartott egészségügyi állapotainak listája lista")]
        AFFAFoEgAlLi100ViMo,

        /// <summary>Fogvatartott egészségügyi állapotainak listája részletek</summary>
        [Description("Fogvatartott egészségügyi állapotainak listája részletek")]
        AFFAFoEgAlR,


        ///<summary>Fpogvatartott fontosabb élelmezéi adatainak listája lista</summary>
        [Description("Fpogvatartott fontosabb élelmezéi adatainak listája lista")]
        AFFAFoElNoLi100ViMo,

        /// <summary>Fpogvatartott fontosabb élelmezéi adatainak listája részletek</summary>
        [Description("Fpogvatartott fontosabb élelmezéi adatainak listája részletek")]
        AFFAFoElNoR,


        /// <summary>Fogvatartott fontosabb adatai - részletek</summary>
        [Description("Fogvatartott fontosabb adatai - részletek")]
        AFFAFoAR,


        ///<summary>Fogvatartott fényképeinek listája lista</summary>
        [Description("Fogvatartott fényképeinek listája lista")]
        AFFAFoFeLi100ViMo,

        /// <summary>Fogvatartott fényképeinek listája részletek</summary>
        [Description("Fogvatartott fényképeinek listája részletek")]
        AFFAFoFeR,


        ///<summary>Fogvatartott tisztségeinek listája lista</summary>
        [Description("Fogvatartott tisztségeinek listája lista")]
        AFFAFoTiLi100ViMo,

        /// <summary>Fogvatartott tisztségeinek listája részletek</summary>
        [Description("Fogvatartott tisztségeinek listája részletek")]
        AFFAFoTiR,

        ///<summary>Gondnokság alá helyezések listája lista</summary>
        [Description("Gondnokság alá helyezések listája lista")]
        AFFAGoAlHeLi100ViMo,

        /// <summary>Gondnokság alá helyezések listája részletek</summary>
        [Description("Gondnokság alá helyezések listája részletek")]
        AFFAGoAlHeR,


        ///<summary>Gyógyító terápiás részlegba helyezés lista</summary>
        [Description("Gyógyító terápiás részlegba helyezés lista")]
        AFFAGyCsHeLi101ViMo,

        /// <summary>Gyógyító terápiás részlegba helyezés részletek</summary>
        [Description("Gyógyító terápiás részlegba helyezés részletek")]
        AFFAGyCsHeR,


        ///<summary>Jutalmazás lista</summary>
        [Description("Jutalmazás lista")]
        AFFAJuLi100ViMo,

        /// <summary>Jutalmazás részletek</summary>
        [Description("Jutalmazás részletek")]
        AFFAJuR,


        ///<summary>Kényszerítő eszközök használatainak listája lista</summary>
        [Description("Kényszerítő eszközök használatainak listája lista")]
        AFFAKeEsHaLi102ViMo,

        /// <summary>Kényszerítő eszközök használatainak listája részletek</summary>
        [Description("Kényszerítő eszközök használatainak listája részletek")]
        AFFAKeEsHaR,

        ///<summary>Könyvkölcsönzések listája lista</summary>
        [Description("Könyvkölcsönzések listája lista")]
        AFFAKonyvKoLi101ViMo,

        /// <summary>Könyvkölcsönzések listája részletek</summary>
        [Description("Könyvkölcsönzések listája részletek")]
        AFFAKonyvKoR,



        ///<summary>Különleges biztonságú elhelyezések listája lista</summary>
        [Description("Különleges biztonságú elhelyezések listája lista")]
        AFFAKuBiElLi100ViMo,

        /// <summary>Különleges biztonságú elhelyezések listája részletek</summary>
        [Description("Különleges biztonságú elhelyezések listája részletek")]
        AFFAKuBiElR,


        ///<summary>Látogatások listája lista</summary>
        [Description("Látogatások listája lista")]
        AFFALaLi100ViMo,

        /// <summary>Látogatások listája részletek</summary>
        [Description("Látogatások listája részletek")]
        AFFALaR,


        ///<summary>Látogato engedélyek listája lista</summary>
        [Description("Látogato engedélyek listája lista")]
        AFFALaEngLi101ViMo,

        /// <summary>Látogato engedélyek listája részletek</summary>
        [Description("Látogato engedélyek listája részletek")]
        AFFALaEngR,


        ///<summary>Látogató engedélyek listája lista</summary>
        [Description("Látogató engedélyek listája lista")]
        AFFALatMeLi101ViMo,

        /// <summary>Látogató engedélyek listája részletek</summary>
        [Description("Látogató engedélyek listája részletek")]
        AFFALatEnR,


        ///<summary>Munkáltatások listája lista</summary>
        [Description("Munkáltatások listája lista")]
        AFFAMuLi104ViMo,

        /// <summary>Munkáltatások listája részletek</summary>
        [Description("Munkáltatások listája részletek")]
        AFFAMuR,


        ///<summary>Különleges biztonságú elhelyezések listája lista</summary>
        [Description("Különleges biztonságú elhelyezések listája lista")]
        AFFANeDoLi101ViMo,

        /// <summary>Különleges biztonságú elhelyezések listája részletek</summary>
        [Description("Különleges biztonságú elhelyezések listája részletek")]
        AFFANeDoR,


        ///<summary>Oktatáson résztvevők listája lista</summary>
        [Description("Oktatáson résztvevők listája lista")]
        AFFAOkReLi100ViMo,

        /// <summary>Oktatáson résztvevők listája részletek</summary>
        [Description("Oktatáson résztvevők listája részletek")]
        AFFAOkReR,


        ///<summary>Őrizetek listája lista</summary>
        [Description("Őrizetek listája lista")]
        AFFAOrLi100ViMo,

        /// <summary>Őrizetek listája részletek</summary>
        [Description("Őrizetek listája részletek")]
        AFFAOrR,


        ///<summary>Szállító járműre való beosztások listája lista</summary>
        [Description("Szállító járműre való beosztások listája lista")]
        AFFASzJaBeLi100ViMo,

        /// <summary>Szállító járműre való beosztások listája részletek</summary>
        [Description("Szállító járműre való beosztások listája részletek")]
        AFFASzJaBeR,



        ///<summary>Szökések listája lista</summary>
        [Description("Szökések listája lista")]
        AFFASzokLi100ViMo,

        /// <summary>Szökések listája részletek</summary>
        [Description("Szökések listája részletek")]
        AFFASzokR,


        ///<summary>Technikai eszközök tartásának listája lista</summary>
        [Description("Technikai eszközök tartásának listája lista")]
        AFFATeEsTaLi100ViMo,




        ///<summary>Különleges biztonságú elhelyezések listája lista</summary>
        [Description("Különleges biztonságú elhelyezések listája lista")]
        AFFATiCiEnLi100ViMo,

        /// <summary>Különleges biztonságú elhelyezések listája részletek</summary>
        [Description("Különleges biztonságú elhelyezések listája részletek")]
        AFFATiCiEnR,


        ///<summary>Tisztálkodási cikkek kérelmeinek listája lista</summary>
        [Description("Tisztálkodási cikkek kérelmeinek listája lista")]
        AFFATiCiKeLi100ViMo,

        /// <summary>Tisztálkodási cikkek kérelmeinek listája részletek</summary>
        [Description("Tisztálkodási cikkek kérelmeinek listája részletek")]
        AFFATiCiKeR,


        ///<summary>Tisztálkodási cikkek kiadásainak listája lista</summary>
        [Description("Tisztálkodási cikkek kiadásainak listája lista")]
        AFFATiCiKiLi100ViMo,

        /// <summary>Tisztálkodási cikkek kiadásainak listája részletek</summary>
        [Description("Tisztálkodási cikkek kiadásainak listája részletek")]
        AFFATiCiKiR,

        /// <summary>Többletinformációk listája lista</summary>
        [Description("Többletinformációk listája lista")]
        AFFAToLi100ViMo,

        /// <summary>Többletinformációk listája részletek</summary>
        [Description("Többletinformációk listája részletek")]
        AFFAFoToInR,

        /// <summary>Újság rendelések listája lista</summary>
        [Description("Újság rendelések listája lista")]
        AFFAUjReLi100ViMo,

        /// <summary>Újság rendelések listája részletek</summary>
        [Description("Újság rendelések listája részletek")]
        AFFAUjReR,

        /// <summary>Vélemények listája lista</summary>
        [Description("Vélemények listája lista")]
        AFFAVeLi102ViMo,

        /// <summary>Vélemények listája részletek</summary>
        [Description("Vélemények listája részletek")]
        AFFAVeR,

        /// <summary>Zárkába helyezések listája lista</summary>
        [Description("Zárkába helyezések listája lista")]
        AFFAZaHeLi102ViMo,

        /// <summary>Zárkába helyezések listája részletek</summary>
        [Description("Zárkába helyezések listája részletek")]
        AFFAZaHeR,



        /// <summary>Látogató megjelenés lista</summary>
        [Description("Látogató megjelenés lista")]
        AFFALaMeLi101ViMo,

        /// <summary>Látogató megjelenés részletek</summary>
        [Description("Látogató megjelenés részletek")]
        AFFALaMeR,

        /// <summary>Látogató megjelenés részletek</summary>
        [Description("Rendkívüli esemény részletek")]
        AFFAReEsR,


        /// <summary>Fogvatartott diétáinak listája lista</summary>
        [Description("Fogvatartott diétáinak listája lista")]
        AFFADiLi101ViMo,

        /// <summary>Fogvatartott diétáinak listája részletek</summary>
        [Description("Fogvatartott diétáinak listája részletek")]
        AFFAFoDiR,

        /// <summary>Fogvatartott EU engedélyeinek listája lista</summary>
        [Description("Fogvatartott EU engedélyeinek listája lista")]
        AFFAFoEUEnLi100ViMo,

        /// <summary>Fogvatartott EU engedélyeinek listája részletek</summary>
        [Description("Fogvatartott EU engedélyeinek listája részletek")]
        AFFAFoEUEnR,

        /// <summary>Fogvatartott részletek</summary>
        [Description("Fogvatartott részletek")]
        AFFAFoR,

        /// <summary>Fogvatartott személyes adatai részletek</summary>
        [Description("Fogvatartott személyes adatai részletek")]
        AFFAFoSzAdR,

        /// <summary>Végrehajtási fok lista</summary>
        [Description("Végrehajtási fok lista")]
        AFFAVeFokViMo,

        #endregion fogvatartott adatai


        #endregion AlapNyilvantarto

        #region Biometria

        /// <summary>Index</summary>
        [Description("Index")]
        BIOIndex,

        /// <summary>Fényképezések listázása</summary>
        [Description("Fényképezések listázása")]
        BIOFenyList,

        /// <summary>Fogvatartotti kártyák listázása</summary>
        [Description("Fogvatartotti kártyák listázása")]
        BIOFogvKarList,

        /// <summary>Fényképek megtekintése</summary>
        [Description("Fényképek megtekintése")]
        BIOFenyMeg,

        /// <summary>Új fényképek rögzítése</summary>
        [Description("Új fényképek rögzítése")]
        BIOUjFenyRogz,

        /// <summary>Összes fogvatartotti fénykép törlése</summary>
        [Description("Összes fogvatartotti fénykép törlése")]
        BIOOsszFogvFenyTor,

        /// <summary>Új kártya készítése</summary>
        [Description("Új kártya készítése")]
        BIOUjKarKesz,

        /// <summary>Kártya adatainak megtekintése</summary>
        [Description("Kártya adatainak megtekintése")]
        BIOKarAdaMeg,

        /// <summary>Kártya letiltása</summary>
        [Description("Kártya letiltása")]
        BIOKarLetilt,

        /// <summary>Kártya nyomtatása</summary>
        [Description("Kártya nyomtatása")]
        BIOKarNyom,

        /// <summary>Baleseti jegyzőkönyv lista megtekintése</summary>
        [Description("Baleseti jegyzőkönyv lista megtekintése")]
        BalesetiJkvLista,

        /// <summary>Baleseti jegyzőkönyv felvitele</summary>
        [Description("Baleseti jegyzőkönyv felvitele")]
        BalesetiJkvFelvitel,

        /// <summary>Baleseti jegyzőkönyv részleteinek megtekintése</summary>
        [Description("Baleseti jegyzőkönyv részleteinek megtekintése")]
        BalesetiJkvReszletek,

        /// <summary>Baleseti jegyzőkönyv módosítása</summary>
        [Description("Baleseti jegyzőkönyv módosítása")]
        BalesetiJkvModositas,

        /// <summary>Baleseti jegyzőkönyvhöz fotó feltöltése</summary>
        [Description("Baleseti jegyzőkönyv fotó feltöltése")]
        BalesetiJkvFotoFelt,

        #endregion

        #region TOLAdmin

        /// <summary>Index</summary>
        [Description("Index")]
        TOLAdminIndex,

        /// <summary>szervezetek listázása</summary>	
        [Description("TOL szervezetek listázása")]
        TOLSZervList,


        /// <summary>Napló</summary>
        [Description("Napló")]
        TOLAdminNaplo,

        /// <summary>OEP jelentés</summary>
        [Description("OEP jelentés")]
        OEPJelentes,

        /// <summary>Allapotvalatozatato</summary>
        [Description("Allapot valtozas")]
        AllapotValtozas,

        #endregion

        #region OET

        /// <summary>Megtekintő</summary>
        OET_Megtekinto,

        /// <summary>Csomagosztó </summary>
        Csomagoszto,

        /// <summary>Talált tárgy kezelő  </summary>
        Talalttargy,

        /// <summary>Okmánykezelő  </summary>
        Okmanykezelo,

        /// <summary>Értékkezelő  </summary>
        Ertekkezelo,

        /// <summary>Tárgyraktáros   </summary>
        Targyraktaros,

        /// <summary>Másodletéző  </summary>
        Masodlet,

        /// <summary>Első letétező  </summary>
        Befog_elsolet,


        //OETSzerkeszto,
        ///// <summary>Index</summary>
        //OETIndex,

        ///// <summary>Technikai eszköz bevizsgálásának rögzítése</summary>	
        //OETTechEBRog,

        ///// <summary>Szelvények létrehozása</summary>	
        //OETSzelvLetre,

        ///// <summary>Szelvények módosítása</summary>	
        //OETSzelvMod,

        ///// <summary>Tárgyszelvény kezelése</summary>	
        //OETTargySzelvKez,

        ///// <summary>Raktári helyek létrehozása, módosítása</summary>	
        //OETRaktarkezelo,

        ///// <summary>Tárgyletétek szállításának kezelése</summary>	
        //OETTargySzallKez,

        ///// <summary>Értékletétek szállításának kezelése</summary>	
        //OETErtSzallKez,

        ///// <summary>Értékszelvény kezelése</summary>	
        //OETErtSzelvKez,

        ///// <summary>Okmányszelvény kezelése</summary>	
        //OETOkmSzelvKez,

        ///// <summary>Okmányletétek szállításának kezelése</summary>	
        //OETOkmSzallKez,

        ///// <summary>Talált cikk kezelése</summary>	
        //OETTalaltCikkKez,


        ///// <summary>OÉT fogvatartottak listázása</summary>	
        //[Description("OÉT fogvatartottak listázása")]
        //OETFogvList,

        ///// <summary>Fogvatartott OÉT letéteinek megtekintése</summary>	
        //[Description("Fogvatartott OÉT letéteinek megtekintése")]
        //OETFogvLetMegt,

        ///// <summary>Okmányszelvény létrehozása</summary>	
        //[Description("Okmányszelvény létrehozása")]
        //OETOkmLetr,

        ///// <summary>Okmányszelvény átvétele</summary>	
        //[Description("Okmányszelvény átvétele")]
        //OETOkmAtve,

        ///// <summary>Talált értékek listázása</summary>	
        //[Description("Talált értékek listázása")]
        //OETTalErtList,

        ///// <summary>Dokumentum generálás - Okmányszelvény</summary>	
        //[Description("Dokumentum generálás - Okmányszelvény")]
        //OETOkmszDokGen,

        ///// <summary>Okmányszelvény részleteinek megtekintése</summary>	
        //[Description("Okmányszelvény részleteinek megtekintése")]
        //OETOkmszReszMeg,

        ///// <summary>Okmányletét részleteinek megtekintése</summary>	
        //[Description("Okmányletét részleteinek megtekintése")]
        //OETOkmlReszMeg,

        ///// <summary>Szabadítás / elhalálozás kezelése okmányletéteknél</summary>	
        //[Description("Szabadítás / elhalálozás kezelése okmányletéteknél")]
        //OETSzabELhKezOkm,

        ///// <summary>Okmányletét módosítása</summary>	
        //[Description("Okmányletét módosítása")]
        //OETOkmlMod,

        ///// <summary>Értékszelvény létrehozása</summary>	
        //[Description("Értékszelvény létrehozása")]
        //OETErtszLetr,

        ///// <summary>Dokumentum generálás - Befogadáskori pénz átvételi elismervény</summary>	
        //[Description("Dokumentum generálás - Befogadáskori pénz átvételi elismervény")]
        //OETBefPAEDokGen,

        ///// <summary>Dokumentum generálás - Jegyzőkönyv hamis gyanús külföldi fizetőeszköz átvételéről (6 függ.)</summary>	
        //[Description("Dokumentum generálás - Jegyzőkönyv hamis gyanús külföldi fizetőeszköz átvételéről (6 függ.)")]
        //OETJegyHGyKFADokG,

        ///// <summary>Értékszelvény átvétele</summary>	
        //[Description("Értékszelvény átvétele")]
        //OETErtszAtv,

        ///// <summary>Dokumentum generálás - Értékszelvény (2. függ.)</summary>	
        //[Description("Dokumentum generálás - Értékszelvény (2. függ.)")]
        //OETErtszDokGen,

        ///// <summary>Értékszelvény részleteinek megtekintése</summary>	
        //[Description("Értékszelvény részleteinek megtekintése")]
        //OETErtszReszMeg,

        ///// <summary>Értékletét részleteinek megtekintése</summary>	
        //[Description("Értékletét részleteinek megtekintése")]
        //OETErtlReszMeg,

        ///// <summary>Szabadítás / elhalálozás kezelése értékletéteknél</summary>	
        //[Description("Szabadítás / elhalálozás kezelése értékletéteknél")]
        //OETSZElKErtl,

        ///// <summary>Értékletét módosítása</summary>	
        //[Description("Értékletét módosítása")]
        //OETErtlMod,

        ///// <summary>Tárgyszelvény létrehozása</summary>	
        //[Description("Tárgyszelvény létrehozása")]
        //OETTargyszLetr,

        ///// <summary>Dokumentum generálás - Kimutatás megőrzés céljából átvett, letétbe nem helyezhető tárgyakról (4 függ.)</summary>	
        //[Description("Dokumentum generálás - Kimutatás megőrzés céljából átvett, letétbe nem helyezhető tárgyakról (4 függ.)")]
        //OETKMCALNHTDOKG,

        ///// <summary>Dokumentum generálás - Jegyzőkönyv a fogvatartottól befrogadáskor átvett lőfegyverről, lőszerről, vélhetően robbanó, sugárzó anyagról, kábítószerről (5 függ.)</summary>	
        //[Description("Dokumentum generálás - Jegyzőkönyv a fogvatartottól befrogadáskor átvett lőfegyverről, lőszerről, vélhetően robbanó, sugárzó anyagról, kábítószerről (5 függ.)")]
        //OETJFBALLVRSAKDOKG,

        ///// <summary>Dokumentum generálás - Megsemmisítési jegyzőkönyv</summary>	
        //[Description("Dokumentum generálás - Megsemmisítési jegyzőkönyv")]
        //OETMegJegyDokGen,

        ///// <summary>Tárgyszelvény átvétele</summary>	
        //[Description("Tárgyszelvény átvétele")]
        //OETTargyszAtv,

        ///// <summary>Talált tárgyak listázása</summary>	
        //[Description("Talált tárgyak listázása")]
        //OETTalTargyList,

        ///// <summary>Dokumentum generálás - Tárgyszelvény (3 függ.)</summary>	
        //[Description("Dokumentum generálás - Tárgyszelvény (3 függ.)")]
        //OETTargyszDokGen,

        ///// <summary>Tárgyszelvény részleteinek megtekintése</summary>	
        //[Description("Tárgyszelvény részleteinek megtekintése")]
        //OETTargyszReszMeg,

        ///// <summary>Tárgyletét részleteinek megtekintése</summary>	
        //[Description("Tárgyletét részleteinek megtekintése")]
        //OETTargylReszMeg,

        ///// <summary>Szabadítás / elhalálozás kezelése tárgyletéteknél</summary>	
        //[Description("Szabadítás / elhalálozás kezelése tárgyletéteknél")]
        //OETSzabELhKezTargy,

        ///// <summary>Tárgyletét módosítása</summary>	
        //[Description("Tárgyletét módosítása")]
        //OETTargylMod,

        ///// <summary>Raktári elhelyezések megtekintése</summary>	
        //[Description("Raktári elhelyezések megtekintése")]
        //OETRakElhMeg,

        ///// <summary>Raktári elhelyezés részleteinek megtekintése</summary>	
        //[Description("Raktári elhelyezés részleteinek megtekintése")]
        //OETRakElhReszMeg,

        ///// <summary>Raktári hely törlése, hozzárendelése tárgyszelvényhez</summary>	
        //[Description("Raktári hely törlése, hozzárendelése tárgyszelvényhez")]
        //OETRakHTHTargysz,

        ///// <summary>Dokumentum generálás - Kimutatás a fogvatartott által magával hozott forint összegről(13 függ.)</summary>	
        //[Description("Dokumentum generálás - Kimutatás a fogvatartott által magával hozott forint összegről(13 függ.)")]
        //OETKFAMHFODokG,

        ///// <summary>Átvett forint összegek listázása</summary>	
        //[Description("Átvett forint összegek listázása")]
        //OETAtvForOsszList,

        ///// <summary>Dokumentum generálása -  Átadás-átvételi jegyzék szállításhoz</summary>	
        //[Description("Dokumentum generálása -  Átadás-átvételi jegyzék szállításhoz")]
        //OETAAJSZDokGen,

        ///// <summary>Dokumentum generálása - Jegyzőkönyv szállítás közben sérült letéti cikkről</summary>	
        //[Description("Dokumentum generálása - Jegyzőkönyv szállítás közben sérült letéti cikkről")]
        //OETJSzKSLcikkDokG,

        ///// <summary>Okmányletétek összeállítása szállításhoz</summary>	
        //[Description("Okmányletétek összeállítása szállításhoz")]
        //OETOkmlOsszSzall,

        ///// <summary>Okmányletét befogadása szállításból</summary>	
        //[Description("Okmányletét befogadása szállításból")]
        //OETOkmlBefSzall,

        ///// <summary>Értékletétek összeállítása szállításhoz</summary>	
        //[Description("Értékletétek összeállítása szállításhoz")]
        //OETErtlOsszSzall,

        ///// <summary>Értékletét befogadása szállításból</summary>	
        //[Description("Értékletét befogadása szállításból")]
        //OETErtlBefSzall,

        ///// <summary>Tárgyletétek összeállítása szállításhoz</summary>	
        //[Description("Tárgyletétek összeállítása szállításhoz")]
        //OETTargylOsszSzall,

        ///// <summary>Tárgyletét befogadása szállításból</summary>	
        //[Description("Tárgyletét befogadása szállításból")]
        //OETTargylBefSzall,

        ///// <summary>Talált cikkek listázása</summary>	
        //[Description("Talált cikkek listázása")]
        //OETTalCikkList,

        ///// <summary>Talált cikk rögzítése</summary>	
        //[Description("Talált cikk rögzítése")]
        //OETTalCikkRogz,

        ///// <summary>Talált cikk részleteinek megtekintése</summary>	
        //[Description("Talált cikk részleteinek megtekintése")]
        //OETTalCikkReszMeg,

        ///// <summary>Talált cikk módosítása</summary>	
        //[Description("Talált cikk módosítása")]
        //OETTalCikkMod,

        ///// <summary>Raktárak listázása</summary>	
        //[Description("Raktárak listázása")]
        //OETRakList,

        ///// <summary>Új raktár felvitele</summary>	
        //[Description("Új raktár felvitele")]
        //OETUjRakFelv,

        ///// <summary>Raktár részleteinek megtekintése</summary>	
        //[Description("Raktár részleteinek megtekintése")]
        //OETRakReszMeg,

        ///// <summary>Raktár módosítása</summary>	
        //[Description("Raktár módosítása")]
        //OETRakMod,

        ///// <summary>Raktári helyek listázása</summary>	
        //[Description("Raktári helyek listázása")]
        //OETRakHelyList,

        ///// <summary>Raktári hely felvitele</summary>	
        //[Description("Raktári hely felvitele")]
        //OETRakHelyFelv,

        ///// <summary>Raktári hely részleteinek megtekintése</summary>	
        //[Description("Raktári hely részleteinek megtekintése")]
        //OETRakHelyReszMeg,

        ///// <summary>Szállításban lévő letétek megtekintése</summary>	
        //[Description("Szállításban lévő letétek megtekintése")]
        //OETSzallLLMeg,

        ///// <summary>Szállítás irányának megváltoztatása</summary>	
        //[Description("Szállítás irányának megváltoztatása")]
        //OETSzallIranyMeg,

        ///// <summary>Okmányletét kiadása</summary>	
        //[Description("Okmányletét kiadása")]
        //OETOkmlKiad,

        ///// <summary>Értékletét kiadása</summary>	
        //[Description("Értékletét kiadása")]
        //OETErtlKiad,

        ///// <summary>Tárgyletét kiadása</summary>	
        //[Description("Tárgyletét kiadása")]
        //OETTargylKiad,



        ///// <summary>Talált cikk törlése</summary>	
        //[Description("Talált cikk törlése")]
        //OETTalCikkTor,

        ///// <summary>Talált cikk letétezésbe adása</summary>	
        //[Description("Talált cikk letétezésbe adása")]
        //OETTalCikkLetAd,

        ///// <summary>Raktár, raktári hely törlése</summary>	
        //[Description("Raktár, raktári hely törlése")]
        //OETRakHelyRTor,

        #endregion OET

        #region MD

        /// <summary>Index</summary>
        [Description("Index")]
        MDIndex,

        /// <summary>Országos paraméterek megtekintése</summary>
        [Description("Országos paraméterek megtekintése")]
        MDOrszParamMegtek,

        /// <summary>Országos paraméterek módosítása</summary>
        [Description("Országos paraméterek módosítása")]
        MDOrszParamModositas,

        /// <summary>Intézeti paraméterek megtekintése</summary>
        [Description("Intézeti paraméterek megtekintése")]
        MDIntParamMegtek,

        /// <summary>Intézeti paraméterek módosítása</summary>
        [Description("Intézeti paraméterek módosítása")]
        MDIntParamModositas,

        /// <summary>Munkanap-áthelyezések megtekintése</summary>
        [Description("Munkanap-áthelyezések megtekintése")]
        MDMnapAthelyezMegtek,

        /// <summary>Munkanap-áthelyezések szerkesztése</summary>
        [Description("Munkanap-áthelyezések szerkesztése")]
        MDMnapAthelyezSzerk,

        /// <summary>Munkahelyek megtekintése</summary>
        [Description("Munkahelyek megtekintése")]
        MDMunkahelyekMegtek,

        /// <summary>Munkahelyek szerkesztése</summary>
        [Description("Munkahelyek szerkesztése")]
        MDMunkahelyekSzerk,

        /// <summary>Dok - Havi fogvatartotti munkadíjösszesítő (munkahelyenként)</summary>
        [Description("Dok - Havi fogvatartotti munkadíjösszesítő (munkahelyenként)")]
        MDDocHaviFogvBerossz,

        /// <summary>Munkakörök megtekintése</summary>
        [Description("Munkakörök megtekintése")]
        MDMunkakorokMegtek,

        /// <summary>Munkakörök szerkesztése</summary>
        [Description("Munkakörök szerkesztése")]
        MDMunkakorokSzerk,

        /// <summary>Munkakört betöltő fogvatartottak listája</summary>
        [Description("Munkakört betöltő fogvatartottak listája")]
        MDMkortBetFogvLista,

        /// <summary>Dok - Tájékoztató a besorolásról és a munkadíjról</summary>
        [Description("Dok - Tájékoztató a besorolásról és a munkadíjról")]
        MDDocTajekBesorMdij,

        /// <summary>Munkáltatások megtekintése</summary>
        [Description("Munkáltatások megtekintése")]
        MDMunkaltatasMegtek,

        /// <summary>Munkáltatás rögzítése</summary>
        [Description("Munkáltatás rögzítése")]
        MDMunkaltatasRogz,

        /// <summary>Munkáltatás megszüntetése</summary>
        [Description("Munkáltatás megszüntetése")]
        MDMunkaltatasMegszun,

        /// <summary>Munkaidő-nyilvántartások megtekintése</summary>
        [Description("Munkaidő-nyilvántartások megtekintése")]
        MDMunkaidoNyilvMtek,

        /// <summary>Munkaidő-nyilvántartás rögzítése</summary>
        [Description("Munkaidő-nyilvántartás rögzítése")]
        MDMunkaidoNyilvRogz,

        /// <summary>Elszámolások megtekintése</summary>
        [Description("Elszámolások megtekintése")]
        MDElszamolasMegtek,

        /// <summary>Elszámolás készítése</summary>
        [Description("Elszámolás készítése")]
        MDElszamolasKeszites,

        /// <summary>Dok - Havi munkadíj jegyzék (fogvatartottanként)</summary>
        [Description("Dok - Havi munkadíj jegyzék (fogvatartottanként)")]
        MDDocHaviMdijJegyzek,

        /// <summary>Szabadság nyilvántartás megtekintése</summary>
        [Description("Szabadság nyilvántartás megtekintése")]
        MDSzabiNyilvMegtek,

        /// <summary>OP jelentések</summary>
        [Description("OP jelentések")]
        MDStatOPJelentesek,

        /// <summary>Havi munkahelyenkénti munkadíjösszesítő</summary>
        [Description("Havi munkahelyenkénti munkadíjösszesítő")]
        MDStatHavMhelyBerosz,

        /// <summary>Kimutatás a fogvatartottak foglalkoztatásának és társadalombiztosítási viszonyairól</summary>
        [Description("Kimutatás a fogvatartottak foglalkoztatásának és társadalombiztosítási viszonyairól")]
        MDStatKimFoglTBvisz,

        #endregion

        #region Ruhazat

        /// <summary>Index</summary>
        [Description("Index")]
        RuhazatIndex,

        /// <summary>Ruházati adatok felvitele</summary>
        [Description("Ruházati adatok felvitele")]
        RuhazatFelvitel,

        /// <summary>Ruházati adatok módosítása</summary>
        [Description("Ruházati adatok módosítása")]
        RuhazatModositas,

        /// <summary>Ruházati adatok részletei</summary>
        [Description("Ruházati adatok részletei")]
        RuhazatReszletek,

        /// <summary>Ruházati adatok lista</summary>
        [Description("Ruházati adatok lista")]
        RuhazatLista,

        Ruhazat_megtekinto,

        Ruhazat_szerkeszto,

        #endregion

        #region Penzugy

        /// <summary>Index</summary>
        [Description("Index")]
        PLIndex,

        /// <summary>Pénzügyi adatok felvitele</summary>
        [Description("Bemutatóhoz")]
        PLBemutato,

        /// <summary>Pénzügyi adatok felvitele</summary>
        [Description("Bemutatóhoz tilos")]
        PLBemutatoTilos,

        /// <summary>Pénzügyi adatok felvitele</summary>
        [Description("Kártérítés kezelő")]
        PLKarteritesKez,

        /// <summary>Pénzügyi adatok felvitele</summary>
        [Description("Letiltás kezelő")]
        PLLetiltasKez,

        /// <summary>Pénzügyi adatok felvitele</summary>
        [Description("Számlalap megtekintő")]
        PLSzamlaLapMegtek,

        /// <summary>Pénzügyi adatok felvitele</summary>
        [Description("Utalási helyek")]
        PLUtalasiHely,

        /// <summary>Pénzügyi adatok felvitele</summary>
        [Description("Utalási helyek fogvatartotthoz")]
        PLUtalasiHelyFogv,

        /// <summary>Pénzügyi adatok felvitele</summary>
        [Description("Intézeti paraméterek")]
        PLIntezetiParam,

        /// <summary>Pénzügyi adatok felvitele</summary>
        [Description("Országos paraméterek")]
        PLOrszagosParam,

        /// <summary>Pénzügyi adatok felvitele</summary>
        [Description("Átfutó számlák könyvelése")]
        PLAtfutoKonyveles,

        /// <summary>Pénzügyi adatok felvitele</summary>
        [Description("Előjegyzés könyvelése")]
        PLElojegyzesKonyv,

        /// <summary>Pénzügyi adatok felvitele</summary>
        [Description("Előjegyzés megtekintése")]
        PLElojegyzesMegtek,

        /// <summary>Pénzügyi adatok felvitele</summary>
        [Description("Letiltás előjegyzés")]
        PLLetiltElojegyzes,

        /// <summary>Pénzügyi adatok felvitele</summary>
        [Description("Letiltás előjegyzés megtekintés")]
        PLLetiltElojMegtek,

        /// <summary>Pénzügyi adatok felvitele</summary>
        [Description("Letiltás részletek")]
        PLLetiltReszletek,

        /// <summary>Pénzügyi adatok felvitele</summary>
        [Description("Kártérítés előjegyzés")]
        PLKarterElojegyzes,

        /// <summary>Pénzügyi adatok felvitele</summary>
        [Description("Kártérítés előjegyzés megtekintés")]
        PLKarterElojMegtek,

        /// <summary>Pénzügyi adatok felvitele</summary>
        [Description("Egyéni számla részletek")]
        PLEgyeniSzamlaMegtek,

        /// <summary>Pénzügyi adatok felvitele</summary>
        [Description("Belső tranzakciók részletek")]
        PLReszletek,

        /// <summary>Pénzügyi adatok felvitele</summary>
        [Description("Pénzügyi tétel felvitel")]
        PLFelvitel,

        /// <summary>Könyvelés</summary>
        [Description("Könyvelés")]
        PLKonyveles,

        /// <summary>Paraméter szerkesztő</summary>
        [Description("Paraméter szerkesztő")]
        PLParamSzerkeszto,

        /// <summary>Paraméter megtekintő</summary>
        [Description("Paraméter megtekintő")]
        PLParamMegtekinto,

        /// <summary>Kártérítés bejelentő</summary>
        [Description("Kártérítés bejelentő")]
        PLKarteritBejelento,

        /// <summary>Kártérítés lefolytató</summary>
        [Description("Kártérítés lefolytató")]
        PLKarteritLefolytato,

        /// <summary>Kártérítés megtekintő</summary>
        [Description("Kártérítés megtekintő")]
        PLKarteritMegtekinto,

        /// <summary>Főoldal megtekintő</summary>
        [Description("Főoldal megtekintő")]
        PLFooldalMegtekinto,

        /// <summary>Pénzügyi letét szerkesztő</summary>
        [Description("Pénzügyi letét szerkesztő")]
        PLPenzugyiSzerkeszto,


        #endregion

        #region Bolt

        [Description("Index")]
        KBIndex,

        [Description("Bemutatóhoz")]
        KBBemutato,

        [Description("Bemutatóhoz tilos")]
        KBBemutatoTilos,

        [Description("Cikktörzs kezelő")]
        KBCikktorzsKezelo,

        [Description("Bolti paraméterkezelő")]
        KBBoltParamKezelo,

        #endregion

        #region FTTR

        /// <summary>
        /// Üdvözlő és kezdőoldal megnyitás
        /// </summary>
        [Description("FTTR Index")]
        FttrIndex,

        /// <summary>
        /// Elővezetés szerkesztése
        /// </summary>
        FttrElovezetesSzerk,

        /// <summary>
        /// Felhívási lap szerkesztése
        /// </summary>
        FttrFelhLapSzerk,

        /// <summary>
        /// Felhívási lap részleteinek megtekintése
        /// </summary>
        FttrFelhLapMegt,

        /// <summary>
        /// Ítélet szerkesztés
        /// </summary>
        FttrIteletSzerk,

        /// <summary>
        /// Lakcím szerkesztése
        /// </summary>
        FttrLakcimSzerk,

        /// <summary>
        /// Felhíváshoz komment hozzáadás
        /// </summary>
        FttrFelhCommentSzerk,

        /// <summary>
        /// Hatóságok szerkesztése
        /// </summary>
        FttrHatosagSzerk,

        /// <summary>
        /// Hatóságok listázása
        /// </summary>
        FttrHatosagMegt,

        /// <summary>
        /// Tartózkodási hely megállapítás szerkesztése
        /// </summary>
        FttrTartHelyMegSzerk,

        /// <summary>
        /// Tértivevény szerkesztése
        /// </summary>
        FttrTervivevenySzerk,

        /// <summary>
        /// Felhívási lap státusz állítása
        /// </summary>
        FttrFelhStatuszSzerk,

        /// <summary>
        /// Saját linkek, kedvencek, saját adatok szerkesztése
        /// </summary>
        FttrSzemAdatokSzerk,

        /// <summary>
        /// Ügyek megtekintése, keresése
        /// </summary>
        FttrUgyekMegt,

        /// <summary>
        /// Telítettség térkép megtekintése
        /// </summary>
        FttrTelitettsegMegt,

        /// <summary>
        /// Felhívási intézet és időpont módosítás
        /// </summary>
        FttrIntezetKijeloles,

        /// <summary>
        /// Intézeti paraméterek szerkesztése
        /// </summary>
        FttrIntezParamSzerk,

        //-------

        FttrAlkParamSzerk,

        FttrAlkParamMegt,

        FttrMunkKivSzerk,

        FttrMunkKivMegt,

        FttrStatLink,

        #endregion FTTR

        #region PTTR

        PttrUgyintezo,
        PttrSzerkeszto,
        #endregion

        #region PME

        PmeIntezetiEmail,
        PmePontozasSzerk,
        PmeMegjPonthSzerk,
        PmeAdatLetoltes,
        PmeErtekelolapMegt,
        PmeFogvListaMegt,
        PmeMagasBesorMegt,
        PmeHianyosMegt,
        PmeKockazatTortenet,
        PmeFigyelmSzerk,
        PmeEgeszsegSzerk,
        PmePszichoSzerk,
        PmeReintegSzerk,
        PmeNyilvantartSzerk,
        PmeFigyelmMegt,
        PmeOsszesitoSzerk,
        PmeVeszgomb,
        PmeEgeszsegMegt,
        PmePszichoMegt,
        PmeReintegMegt,
        PmeNyilvantartMegt,

        #endregion

        #region Bv bolt

        BvBoltCTKezelo,
        BvBoltCTAdmin,

        #endregion

        #region Foglalkoztatas

        [Description("Fogl. besorolási adatok szerkesztése")]
        FoglBesorolasSzerk,
        [Description("Fogl. munkáltató szerkesztése")]
        FoglMunkaltatoSzerk,
        [Description("Fogl. munkáltató info")]
        FoglMunkaltatoInfo,
        [Description("Fogl. munkakör szerkesztése")]
        FoglMunkakorSzerk,
        [Description("Fogl. munkakörök info")]
        FoglMunkakorInfo,
        [Description("Fogl. munkanap áthelyezés szerkesztése")]
        FoglNapAthelySzerk,
        [Description("Fogl. munkanap áthelyezés info")]
        FoglNapAthelyInfo,
        [Description("Fogl. munkahely szerkesztése")]
        FoglMunkahelySzerk,
        [Description("Fogl. munkahely info")]
        FoglMunkahelyInfo,
        [Description("Fogl. oktatási hely szerkesztése")]
        FoglOktHelySzerk,
        [Description("Fogl. oktatási hely info")]
        FoglOktHelyInfo,
        [Description("Fogl. oktatási eredmény szerkesztése")]
        FoglOktEredSzerk,
        [Description("Fogl. oktatás szerkesztése")]
        FoglOktSzerk,
        [Description("Fogl. munkába állítása/levaltasa")]
        FoglMunkAllitLevalt,
        [Description("Fogl. fogvatartott lista")]
        FoglFogvLista,
        [Description("Fogl. személyi karton megnyitás")]
        FoglSzemKartMegny,
        [Description("Fogl. munkahelyvezeto szerkesztes")]
        FoglMhelyvezSzerk,
        [Description("Fogl. elszamolas szerkesztes")]
        FoglElszamolasSzerk,
        [Description("Fogl. besorolási adatok szerkesztése")]
        FoglBesorAdatokSzerk,
        [Description("Fogl. jelenléti ív info")]
        FoglJelIvInfo,
        [Description("Fogl. jelenléti ív szerkesztése")]
        FoglJelIvSzerk,
        [Description("Fogl. okt. jelenléti ív info")]
        FoglOktJelIvInfo,
        [Description("Fogl. okt. jelenléti ív szerkesztése")]
        FoglOktJelIvSzerk,
        [Description("Fogl. teljesítmény alapú cikk info")]
        FoglTeljCikkInfo,
        [Description("Fogl. teljesítmény alaú cikk szerkesztése")]
        FoglTeljCikkSzerk,
        [Description("Fogl. munkahelyvezető szerkesztése")]
        FoglMunkhelyVezSzerk,
        [Description("Fogl. okt. elszámolás szerkesztés")]
        FoglOktElszamSzerk,
        [Description("Fogl. szabadság info")]
        FoglSzabadsagInfo,
        [Description("Fogl. szabadság szerkesztés")]
        FoglSzabadsagSzerk,
        [Description("Fogl. jelenléti íhv jóváhagyás")]
        FoglJelIvJovahagy,
        [Description("Fogl. egyedi zárás")]
        FoglEgyediZaras,
        [Description("Fogl. okt. jelenléti ív jóváhagyás")]
        FoglOktJelIvJovahagy,

        #endregion

        #region Kartalanitas

        [Description("Kart. statisztikai adatok megjelenítése")]
        KartStatAdatInfo,
        [Description("Kart. statisztikai adatok szerkesztése")]
        KartStatAdatSzerk,
        [Description("Kart. beállítások megjelenítése")]
        KartBeallitasInfo,

        [Description("Kart. megjelenítése")]
        KartMegjel,
        [Description("Kart. saját beállítások szerkesztése")]
        KartSajatBeallSzerk,
        [Description("Kart. országos beállítások szerkesztése")]
        KartOrszBeallSzerk,
        [Description("Kart. intézeti beállítások szerkesztése")]
        KartIntezBeallSzerk,
        [Description("Kart. panasz szerkesztése")]
        KartPanaszSzerk,
        [Description("Kart. kártalanitás szerkesztése")]
        KartKartalSzerk,
        [Description("Kart. egyéb panasz vélemenyezése")]
        KartEgyebPanaszVel,
        [Description("Kart. reintegrációs tiszti vélemény szerkesztése")]
        KartReintVelSzerk,
        #endregion

        #region Online

        [Description("Online lekérdező")]
        OnlineLekerdezo,
        [Description("Online napló megtekintő")]
        OnlineNaploMegtekinto,
        [Description("Online központi napló megtekintő")]
        OnlineKozpontiNaploMegtekinto,


        #endregion

        #region Telefon

        [Description("Telefon alapjog")]
        TelefonAlapjog,

        [Description("Telefon kiadas")]
        TelefonTelKiad,

        [Description("Hívás napló megtekintés")]
        HivasNaploMegtek,

        [Description("Hívás engedélyezés megtekintés")]
        HivasEngMegtek,

        [Description("Telefon kiadas napló megtekintés")]
        NaploTelKiad,

        [Description("Egyedi telefonálási szabály beállítása")]
        TelSzabBeallEgyedi,


        #endregion

        #region BFB
        [Description("Bfb kezdőoldal")]
        BfbIndex,
        [Description("Bfb adat megtekintő")]
        BfbInfo,
        [Description("Bfb adat szerkesztő")]
        BfbSzerk,
        [Description("Bfb ülés szerkesztő")]
        BfbElojegyzesKezelo,
        #endregion

        #region BvShop
        BvShop_kietkezobolt,
        BvShop_allomanyibolt,
        #endregion BvShop

        #region Keszletnyilvántartó
        KeszletNyilvantarto_admin,
        KeszletNyilvantarto_megtekinto,
        KeszletNyilvantarto_szerkeszto,

        #endregion Keszletnyilvántartó

        #region Mobil készletnyilványtató
        Mobil_KeszletNyilv_megtekinto,
        Mobil_KeszletNyilv_szerkeszto,
        Mobil_KeszletNyilv_leltarozo,
        #endregion Mobil készletnyilványtató

        #region REBEKA


        Rebeka_Felhasznalo,
        Safe_admin,

        #endregion

        #region RIN


        Rin_Adatfeldolgozo,
        Rin_efopEsetmenedzser,
        Rin_oepKezelo,
        Rin_KfMunkatars,

        #endregion

        #region Navigator

        Navigator_megtekinto,
        [Description("Előállítások nyilvántartása")]
        Navigator_eloallitasnaplo,
        [Description("Jelentésre kötelezett események naplója")]
        Navigator_jelentesrekotnaplo,
        [Description("Ellenőrzési napló")]
        Navigator_ellenorzesinaplo,
        [Description("Belépő személyek nyilvántartása")]
        Navigator_belepesinaplo,
        [Description("Adatszolgáltatás")]
        Navigator_ejfeli_adatszolgaltatas,
        [Description("Ellenőrzési napló olvasása")]
        Navigator_ellenorzesinaplo_jovahagyo,
        [Description("Körletfelügyelői ellenőrzési napló")]
        Navigator_korletfelugyeloiellenorzesinaplo,
        [Description("Körletfelügyelői ellenőrzési napló olvasó jóváhagyó")]
        Navigator_korletfelugyeloiellenorzesinaplo_jovahagyo,
        [Description("BIT Napló szerkesztő")]
        Navigator_bitnaplo,
        [Description("BIT Napló parancsnok")]
        Navigator_bitnaplo_jovahagyo,
        [Description("BIT Napló osztályvezető")]
        Navigator_bitnaplo_osztalyvezeto,
        [Description("BIT Napló körletfőfelügyelő")]
        Navigator_bitnaplo_korletfofelugyelo,
        [Description("Körletfelügyelői Napló")]
        Navigator_kfenaplo,
        [Description("Körletfelügyelői Napló jóváhagyó")]
        Navigator_kffenaplo_jovahagyo,
        [Description("Körletfőfelügyelői Napló")]
        Navigator_kffenaplo,
        [Description("Munkahelyi szolgálati Napló")]
        Navigator_munkahelyiszolgalatinaplo,
        [Description("Munkahelyi szolgálati Napló jóváhagyó")]
        Navigator_munkahelyiszolgalatinaplo_jovahagyo,
        [Description("Esemény jelentő Napló rögzítő")]
        Navigator_Jelento_rogzito,
        [Description("Esemény jelentő Napló agglomeració")]
        Navigator_Jelento_agglomeracio,
        [Description("Esemény jelentő Napló főügyelet")]
        Navigator_Jelento_fougyelet,
        #endregion

        #region JFK                    

        Jfk_fegyjutmegtekinto,

        #endregion

        #region Időrend                    

        Idorend_megtekinto,
        Idorend_tavozas_kezelo,
        #endregion

        #region Fegyelmi

        Fegyelmi_egyeb_szakterulet,
        Fegyelmi_jogkor_gyakorloja,
        Fegyelmi_reintegracios_tiszt,
        Fegyelmi_fofelugyelo,

        #endregion

        #region Szállításkezelő

        Szallitaskezelo_megtekinto,
        Szallitaskezelo_szerkeszto,

        #endregion
    }
}


//public static class JogosultsagokD
//{
//    public static string GetDescription(this Enum value)
//    {
//        FieldInfo field = value.GetType().GetField(value.ToString());

//        DescriptionAttribute attribute
//                = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute))
//                    as DescriptionAttribute;

//        return attribute == null ? value.ToString() : attribute.Description;
//    }
//}
