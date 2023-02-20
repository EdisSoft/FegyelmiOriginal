export const mockJutalmazasStatusz = function() {
  return [
    { Id: 5, Nev: 'Elutasítva' },
    { Id: 2, Nev: 'Engedélyezve' },
    { Id: 6, Nev: 'Kihirdetve' },
    { Id: 3, Nev: 'Osztályvezetőnek elküldve' },
    { Id: 4, Nev: 'Parancsnoknak elküldve' },
    { Id: 1, Nev: 'Reintegációs tisztnek elküldve' },
  ];
};

export const mockBVNKivul = function() {
  return [
    { Id: 8, Nev: 'Büntetés félbeszakítás' },
    { Id: 7, Nev: 'Előállítás' },
    { Id: 9, Nev: 'Eltávozás' },
    { Id: 10, Nev: 'Nyomozásra kiadás' },
  ];
};

export const mockIntezetiEgyeb = function() {
  return [
    { Id: 17, Nev: 'BFB megjelenés' },
    { Id: 12, Nev: 'Fegyelmi elkülönítés' },
    { Id: 11, Nev: 'Fegyelmi ügy - tárgyalás' },
    { Id: 24, Nev: 'Foglalkoztatás' },
    { Id: 18, Nev: 'Látogatás' },
    { Id: 13, Nev: 'Magánelzárás' },
    { Id: 14, Nev: 'Szállítás' },
    { Id: 16, Nev: 'Zárkába helyezés' },
  ];
};

export const mockIntezetiFix = function() {
  return [{ Id: 19, Nev: 'Szabadítás' }, { Id: 20, Nev: 'Távtárgyalás' }];
};

export const mockJutalmazasJogkor = function() {
  return [
    { Id: 22, Nev: 'Osztályvezető' },
    { Id: 23, Nev: 'Parancsnok' },
    { Id: 21, Nev: 'Reintegrációs tiszt' },
  ];
};

export const mockElkovetesEszkoze = function() {
  return [
    { Id: 25, Nev: 'Borotvapenge' },
    { Id: 28, Nev: 'Bútor alkatrész' },
    { Id: 26, Nev: 'Fűrész' },
    { Id: 27, Nev: 'Kés' },
    { Id: 30, Nev: 'Nincs meghatározva' },
    { Id: 29, Nev: 'Zárka felszerelés' },
  ];
};

export const mockEsemenyJellege = function() {
  return [
    { Id: 38, Nev: 'Egyéb vétség' },
    { Id: 34, Nev: 'Fogvatartottak közötti vétség' },
    { Id: 31, Nev: 'Munkavégzéssel kapcsolatos vétségek' },
    { Id: 35, Nev: 'Ön-, egészségkárosító vétség' },
    { Id: 37, Nev: 'Rendkívüli események' },
    { Id: 33, Nev: 'Rendszabályok megsértése' },
    { Id: 32, Nev: 'Személyzettel kapcsolatos vétségek' },
    { Id: 36, Nev: 'Visszatéréssel összefüggő vétség' },
  ];
};

export const mockEsemenyModja = function() {
  return [
    { Id: 39, Nev: 'Falbontás' },
    { Id: 44, Nev: 'Nincs meghatározva' },
    { Id: 42, Nev: 'Orvtámadás' },
    { Id: 43, Nev: 'Öncsonkítás' },
    { Id: 40, Nev: 'Rácsfűrészelés' },
    { Id: 41, Nev: 'Zárka eltorlaszolás' },
  ];
};

export const mockEsemenyHelyszine = function() {
  return [
    { Id: 62, Nev: 'Bíróság, ügyészség' },
    { Id: 57, Nev: 'Egészségügyi részleg' },
    { Id: 51, Nev: 'Előállítás helye' },
    { Id: 55, Nev: 'Foglalk.okt.helyiség' },
    { Id: 64, Nev: 'Folyosó' },
    { Id: 54, Nev: 'Int.kívüli terület' },
    { Id: 52, Nev: 'Iroda,irodaépület' },
    { Id: 61, Nev: 'KFT-i mh. int.kívül' },
    { Id: 60, Nev: 'KFT-i mh.int.belül' },
    { Id: 49, Nev: 'Kórház' },
    { Id: 46, Nev: 'Körlet' },
    { Id: 58, Nev: 'Kv-i mh int.belül' },
    { Id: 59, Nev: 'Kv-i mh int.kívül' },
    { Id: 63, Nev: 'Látogatási helyiség' },
    { Id: 47, Nev: 'Munkahely' },
    { Id: 53, Nev: 'Őrhely' },
    { Id: 48, Nev: 'Sétaudvar' },
    { Id: 56, Nev: 'Szállítás' },
    { Id: 50, Nev: 'Tárgyalás' },
    { Id: 45, Nev: 'Zárka' },
  ];
};

export const mockErintettsegFoka = function() {
  return [
    { Id: 65, Nev: 'Elkövető' },
    { Id: 68, Nev: 'Észlelő' },
    { Id: 67, Nev: 'Segítő' },
    { Id: 66, Nev: 'Sértett' },
    { Id: 187, Nev: 'Tanú' },
  ];
};

export const mockEsemenyNapszaka = function() {
  return [
    { Id: 70, Nev: 'Ébresztő-nyitás között' },
    { Id: 76, Nev: 'Folytatólagosan' },
    { Id: 74, Nev: 'Hivatali idő után - zárás között' },
    { Id: 72, Nev: 'Hivatali időben' },
    { Id: 73, Nev: 'Napközben munkaszüneti napon' },
    { Id: 77, Nev: 'Nem volt megállapítható' },
    { Id: 71, Nev: 'Nyitás-munkakezdés között' },
    { Id: 69, Nev: 'Zárás-ébresztő között' },
    { Id: 75, Nev: 'Zárás-takarodó között' },
  ];
};

export const mockFegyelmiUgyStatusza = function() {
  return [
    { Id: 141, Nev: 'Fenyítés kiszabva' },
    { Id: 149, Nev: 'Fenyítés végrehajtás alatt' },
    { Id: 156, Nev: 'Fenyítés végrehajtás megszakítva' },
    { Id: 150, Nev: 'Fenyítés végrehajtva' },
    { Id: 152, Nev: 'Fogvatartott kioktatva' },
    { Id: 142, Nev: 'I. fokú tárgyalás' },
    { Id: 157, Nev: 'II. fokú tárgyalás' },
    { Id: 86, Nev: 'Kezdeményezve' },
    { Id: 87, Nev: 'Kivizsgálás folyamatban' },
    { Id: 177, Nev: 'Közvetítői eljárás alatt' },
    { Id: 161, Nev: 'Megszüntetve' },
    { Id: 145, Nev: 'Megszüntetve, új eljárással' },
    { Id: 88, Nev: 'Megtagadva' },
    { Id: 175, Nev: 'Nem hajtható végre' },
    { Id: 148, Nev: 'Összevonva' },
    { Id: 89, Nev: 'Reintegrációs tiszti jogkörben' },
    { Id: 174, Nev: 'Végrehajthatósága elévült' },
    { Id: 219, Nev: 'I fokú tárgyalás előkészítése' },
  ];
};

export const mockFenyitesTipusa = function() {
  return [
    { Id: 94, Nev: 'Bv. int. programokon való részvétel korlátozása' },
    { Id: 95, Nev: 'Bv. int. programokon való részvétel tiltása' },
    { Id: 90, Nev: 'Feddés' },
    { Id: 91, Nev: 'Kiétkezés csökkentés' },
    { Id: 97, Nev: 'Kimaradás megvonás' },
    { Id: 93, Nev: 'Magánál tartható tárgyak körének korlátozása' },
    { Id: 92, Nev: 'Magánelzárás' },
    { Id: 96, Nev: 'Többletszolgáltatások megvonása' },
  ];
};

export const mockFegyelmiVetsegTipusa = function() {
  return [
    { Id: 115, Nev: 'Állami v.magántulajdon rongálása, lopása' },
    { Id: 135, Nev: 'Bf-ről késés' },
    { Id: 136, Nev: 'Bf-ről visszatérés elmulasztása' },
    { Id: 137, Nev: 'Egyéb vétség' },
    { Id: 134, Nev: 'Eltávozásról visszatérés elmulasztása' },
    { Id: 128, Nev: 'Étkezés megtagadás' },
    { Id: 117, Nev: 'Fajtalanságra kényszerítés' },
    { Id: 105, Nev: 'Felügyelet megtévesztése, félrevezetése' },
    { Id: 99, Nev: 'Fogolyszökés' },
    { Id: 100, Nev: 'Fogolyszökés kísérlete, előkészülete' },
    { Id: 101, Nev: 'Fogolyszökéshez segítség nyújtása' },
    { Id: 98, Nev: 'Fogolyzendülés' },
    { Id: 119, Nev: 'Fogvatartott társ bántalmazása' },
    { Id: 120, Nev: 'Fogvatartott társ kihasználása, zsarolása' },
    { Id: 123, Nev: 'Fogvatartott társ sanyargatása' },
    { Id: 121, Nev: 'Fogvatartott társ sértegetése' },
    { Id: 122, Nev: 'Fogvatartott társ tulajdonának rongálása, lopása' },
    { Id: 126, Nev: 'Gyógykezelés megtagadása' },
    { Id: 116, Nev: 'Kapcsolattartás szabályainak megsértése' },
    { Id: 129, Nev: 'Kimaradásról késés' },
    { Id: 130, Nev: 'Kimaradásról visszatérés elmulasztása' },
    { Id: 124, Nev: 'Közreműködés szándékos egészségkárosításban' },
    { Id: 110, Nev: 'Meghatározott feladat elvégzése alóli kibúvás' },
    { Id: 107, Nev: 'Meghatározott feladat elvégzésének megtagadása' },
    { Id: 108, Nev: 'Meghatározott feladat hanyag végzése' },
    { Id: 133, Nev: 'Megszüntetve' },
    { Id: 109, Nev: 'Munkahelyi rend megsértése' },
    { Id: 132, Nev: 'Rövidtartamú eltáv.-ról visszatérés elmulasztása' },
    { Id: 131, Nev: 'Rövidtartamú eltávozásról késés' },
    { Id: 112, Nev: 'Szabadlevegőn tartózkodás megzavarása' },
    { Id: 125, Nev: 'Szándékos egészségkárosítás' },
    { Id: 102, Nev: 'Személyzet tagja elleni erőszak' },
    { Id: 103, Nev: 'Személyzet tagjának megsértése' },
    { Id: 104, Nev: 'Személyzet utasítása végrehajtásának megtagadása' },
    { Id: 127, Nev: 'Szeszesital, bódítószer fogyasztása, használata' },
    { Id: 113, Nev: 'Tiltott szerencsejáték' },
    { Id: 114, Nev: 'Tiltott tárgy készítése, tartása, cseréje' },
    { Id: 106, Nev: 'Tiszteletlen magatartás' },
    { Id: 118, Nev: 'Verekedés' },
    { Id: 138, Nev: 'Vesztegetés' },
    { Id: 111, Nev: 'Zárka, körletrend megsértése' },
  ];
};

export const mockNaploTipus = function() {
  return [
    { Id: 184, Nev: 'Elrendelem az eljárás lefolytatását' },
    { Id: 188, Nev: 'Feddés' },
    { Id: 186, Nev: 'Fogvatartott jogi képviseletet kér' },
    { Id: 190, Nev: 'Kioktatás' },
    { Id: 185, Nev: 'Reintegrációs tiszti jogkörbe utalom' },
    { Id: 183, Nev: 'Tanú meghallgatás' },
    { Id: 192, Nev: 'Ügy kezdeményezése' },
    { Id: 191, Nev: 'Ügy megtagadása' },
    { Id: 189, Nev: 'Visszaküldés' },
  ];
};
