export function mockRezsimek() {
  return [
    {
      Id: 1002618,
      Nev: 'Enyhébb',
    },
    {
      Id: 1002619,
      Nev: 'Általános',
    },
    {
      Id: 1002620,
      Nev: 'Szigorúbb',
    },
    {
      Id: 1002621,
      Nev: 'Nincs',
    },
  ];
}
export function mockBiztKock() {
  return [
    {
      Id: 4809,
      Nev: 'Alacsony',
    },
    {
      Id: 4810,
      Nev: 'Közepes',
    },
    {
      Id: 4811,
      Nev: 'Magas',
    },
    {
      Id: 4812,
      Nev: 'Nincs',
    },
  ];
}

export function mockVegrehajtasiFokozatok(params) {
  return [
    {
      Id: 2750,
      Nev: 'Fogház',
    },
    {
      Id: 2751,
      Nev: 'Börtön',
    },
    {
      Id: 2752,
      Nev: 'Fegyház',
    },
    {
      Id: 2754,
      Nev: 'Pénzbünt.hely.szab.vesztés (fogház)',
      KodszotarCsoportId: 341,
    },
    {
      Id: 2755,
      Nev: 'Közérdekű munka átváltoztatása (fogház)',
      KodszotarCsoportId: 341,
    },
    {
      Id: 2756,
      Nev: 'Javító intézeti nevelés',
    },
    {
      Id: 2757,
      Nev: 'Fk. fogház',
    },
    {
      Id: 2758,
      Nev: 'Fk. börtön',
    },
    {
      Id: 2759,
      Nev: 'Ideiglenes kényszergyógykezelés',
      KodszotarCsoportId: 341,
    },
    {
      Id: 2760,
      Nev: 'Kényszergyógykezelés',
    },
    {
      Id: 2761,
      Nev: 'Előzetes I. fokú ítéletig',
    },
    // {
    //   Id: 2762,
    //   Nev: 'Előzetes nem jogerősen elítélt',
    //   KodszotarCsoportId: 341,
    // },
    // { Id: 2763, Nev: 'Elzárás' },
    // { Id: 2764, Nev: 'Őrizet' },
    // { Id: 2765, Nev: 'Fk. előzetes I. fokú ítéletig' },
    // {
    //   Id: 2766,
    //   Nev: 'Fk. előzetes nem jogerősen elítélt',
    //   KodszotarCsoportId: 341,
    // },
    // { Id: 2767, Nev: 'Idegenrendészeti őrizet' },
    // {
    //   Id: 2768,
    //   Nev: 'Átadási/átvételi letartóztatott',
    //   KodszotarCsoportId: 341,
    // },
    // { Id: 2769, Nev: 'Bv. átvételi letartóztatott' },
    // { Id: 4780, Nev: 'Büntetőjogi elzárás' },
    // { Id: 4781, Nev: 'Foglalkozástól eltiltás' },
    // { Id: 4782, Nev: 'Járművezetéstől eltiltás' },
    // { Id: 4783, Nev: 'Kitiltás' },
    // {
    //   Id: 4784,
    //   Nev: 'Sportrendezvények látogatás. való eltiltás',
    //   KodszotarCsoportId: 341,
    // },
    // { Id: 4785, Nev: 'Kiutasítás' },
    // { Id: 1002615, Nev: 'Fk. büntetőjogi elzárás' },
  ];
}

export function mockNyilvantartasiStatuszok() {
  return [
    {
      Id: 2807,
      Nev: '1/2 felt.',
      KodszotarCsoportId: 284,
    },
    {
      Id: 2812,
      Nev: '1/3 felt.',
      KodszotarCsoportId: 284,
    },
    {
      Id: 2813,
      Nev: '1/4 felt.',
      KodszotarCsoportId: 284,
    },
    // { Id: 2814, Nev: '1/5 felt.', KodszotarCsoportId: 284 },
    // { Id: 2782, Nev: 'Adapt.szab', KodszotarCsoportId: 284 },
    // { Id: 2804, Nev: 'Át.let.sz.', KodszotarCsoportId: 284 },
    // { Id: 2793, Nev: 'Bf.Elz-fsz', KodszotarCsoportId: 284 },
    // { Id: 2805, Nev: 'Bv.átv.sz.', KodszotarCsoportId: 284 },
    // { Id: 2826, Nev: 'Elévült', KodszotarCsoportId: 284 },
    // { Id: 2778, Nev: 'Előáll.-on', KodszotarCsoportId: 284 },
    // { Id: 2829, Nev: 'Előz.lejár', KodszotarCsoportId: 284 },
    // { Id: 2831, Nev: 'Előz.megsz', KodszotarCsoportId: 284 },
    // { Id: 2828, Nev: 'Ft-nak átf', KodszotarCsoportId: 284 },
    // { Id: 2785, Nev: 'Eltáv.-on', KodszotarCsoportId: 284 },
    // { Id: 2800, Nev: 'Elz.kif.sz', KodszotarCsoportId: 284 },
    // { Id: 2799, Nev: 'Elz.kit.sz', KodszotarCsoportId: 284 },
    // { Id: 2798, Nev: 'Elz.m.szak', KodszotarCsoportId: 284 },
    // { Id: 2836, Nev: 'Elz-nak át', KodszotarCsoportId: 284 },
    // { Id: 2783, Nev: 'EU tago-ba', KodszotarCsoportId: 284 },
    // { Id: 2820, Nev: 'Fegyz.katf', KodszotarCsoportId: 284 },
    {
      Id: 3186,
      Nev: 'Felt.szab.',
      KodszotarCsoportId: 284,
    },
    {
      Id: 2811,
      Nev: 'Hátr.időve',
      KodszotarCsoportId: 284,
    },
    {
      Id: 2802,
      Nev: 'Kij.tart.h',
      KodszotarCsoportId: 284,
    },
    {
      Id: 1002652,
      Nev: 'Idr. átad.',
      KodszotarCsoportId: 284,
    },
    {
      Id: 1002635,
      Nev: 'Id.őr.lej',
      KodszotarCsoportId: 284,
    },
    {
      Id: 2801,
      Nev: 'Id.őr.msz.',
      KodszotarCsoportId: 284,
    },
    {
      Id: 2803,
      Nev: 'Kiutasítva',
      KodszotarCsoportId: 284,
    },
    // { Id: 2808, Nev: 'Id.bef.msz', KodszotarCsoportId: 284 },
    // { Id: 2825, Nev: 'Id.kgy.msz', KodszotarCsoportId: 284 },
    // { Id: 2838, Nev: 'Irattárból', KodszotarCsoportId: 284 },
    // { Id: 2822, Nev: 'Javító int', KodszotarCsoportId: 284 },
    // { Id: 2775, Nev: 'Jelenlevő', KodszotarCsoportId: 284 },
    // { Id: 2788, Nev: 'Jogell.bf', KodszotarCsoportId: 284 },
    // { Id: 2789, Nev: 'Jogell.elt', KodszotarCsoportId: 284 },
    // { Id: 2791, Nev: 'Jogell.táv', KodszotarCsoportId: 284 },
    {
      Id: 2790,
      Nev: 'Jogell.szö',
      KodszotarCsoportId: 284,
    },
    {
      Id: 2815,
      Nev: 'Kegy.szab.',
      KodszotarCsoportId: 284,
    },
    {
      Id: 2833,
      Nev: 'Kgyk megsz',
      KodszotarCsoportId: 284,
    },
    {
      Id: 2786,
      Nev: 'Kimarad.on',
      KodszotarCsoportId: 284,
    },
    {
      Id: 2810,
      Nev: 'Kitöltve',
      KodszotarCsoportId: 284,
    },
    {
      Id: 2781,
      Nev: 'Külkór.kih',
      KodszotarCsoportId: 284,
    },
    // { Id: 2823, Nev: 'Más orsz.', KodszotarCsoportId: 284 },
    // { Id: 2819, Nev: 'Meghalt', KodszotarCsoportId: 284 },
    // { Id: 4788, Nev: 'MegőrzSzál', KodszotarCsoportId: 284 },
    // { Id: 2832, Nev: 'Nje.ít.lej', KodszotarCsoportId: 284 },
    // { Id: 2779, Nev: 'Nyomozásra', KodszotarCsoportId: 284 },
    // { Id: 2806, Nev: 'Óv.szabadl', KodszotarCsoportId: 284 },
    // { Id: 2834, Nev: 'Őrz.lejárt', KodszotarCsoportId: 284 },
    // { Id: 2816, Nev: 'Pb.kif.sz.', KodszotarCsoportId: 284 },
    // { Id: 2817, Nev: 'Pb.kit.sz.', KodszotarCsoportId: 284 },
    // { Id: 2837, Nev: 'Postázott', KodszotarCsoportId: 284 },
    // { Id: 4823, Nev: 'ReIntŐr.', KodszotarCsoportId: 284 },
    // { Id: 2795, Nev: 'Mő-ről vis', KodszotarCsoportId: 284 },
    // { Id: 2821, Nev: 'Rend.átad', KodszotarCsoportId: 284 },
    // { Id: 2784, Nev: 'Rövid eltá', KodszotarCsoportId: 284 },
    // { Id: 2797, Nev: 'Szab.bvügy', KodszotarCsoportId: 284 },
    // { Id: 2830, Nev: 'Szab.láb.', KodszotarCsoportId: 284 },
    {
      Id: 2809,
      Nev: 'Tárgy.szab',
      KodszotarCsoportId: 284,
    },
    {
      Id: 2827,
      Nev: 'Tév.hib.ad',
      KodszotarCsoportId: 284,
    },
    {
      Id: 2794,
      Nev: 'Végl.átsz.',
      KodszotarCsoportId: 284,
    },
  ];
}

export function mockFogvatartasJellege() {
  return [
    {
      Id: 1,
      Nev: 'Elítélt',
    },
    {
      Id: 2,
      Nev: 'Elzárásos',
    },
    {
      Id: 3,
      Nev: 'Letartóztatott',
    },
    {
      Id: 4,
      Nev: 'K. Gyógykezelés',
    },
  ];
}
export function mockElojegyzesekKsz() {
  return [
    {
      Id: 7,
      Nev: 'Előállítás',
      FelhoId: 2,
    },
    {
      Id: 8,
      Nev: 'Büntetés félbeszakítás',
      FelhoId: 2,
    },
    {
      Id: 9,
      Nev: 'Eltávozás',
      FelhoId: 2,
    },
    {
      Id: 10,
      Nev: 'Nyomozásra kiadás',
      FelhoId: 2,
    },
    {
      Id: 11,
      Nev: 'Fegyelmi ügy - tárgyalás',
      FelhoId: 3,
    },
    {
      Id: 12,
      Nev: 'Fegyelmi elkülönítés',
      FelhoId: 3,
    },
    {
      Id: 13,
      Nev: 'Magánelzárás',
      FelhoId: 3,
    },
    {
      Id: 14,
      Nev: 'Szállítás',
      FelhoId: 3,
    },
    {
      Id: 15,
      Nev: 'Átmeneti részlegre helyezés és még hosszabb név hogy ne férjen ki',
      FelhoId: 3,
    },
    {
      Id: 16,
      Nev: 'Zárkába helyezés',
      FelhoId: 3,
    },
    {
      Id: 17,
      Nev: 'BFB megjelenés',
      FelhoId: 3,
    },
    {
      Id: 18,
      Nev: 'Látogatás',
      FelhoId: 3,
    },
    {
      Id: 19,
      Nev: 'Szabadítás',
      FelhoId: 4,
    },
    {
      Id: 20,
      Nev: 'Távtárgyalás',
      FelhoId: 4,
    },
  ];
}
export function mockElojegyzesKategoriak() {
  return [
    {
      Id: 2,
      Nev: 'BV-n kívül',
    },
    {
      Id: 3,
      Nev: 'Intézeti, egyéb',
    },
    {
      Id: 4,
      Nev: 'Intézeti, fix',
    },
  ];
}
export function mockEsedekesseg() {
  return [
    {
      Id: 1,
      Nev: 'Ma',
    },
    {
      Id: 2,
      Nev: 'Holnap',
    },
    {
      Id: 3,
      Nev: 'Holnapután',
    },
    {
      Id: 4,
      Nev: 'Héten',
    },
    {
      Id: 5,
      Nev: 'Jövő héten',
    },
  ];
}
export function mockElojegyzesOka() {
  return [
    {
      Id: 1,
      Nev: 'Tárgyalás',
    },
    {
      Id: 2,
      Nev: 'Hivatalból - perújítás',
    },
    {
      Id: 3,
      Nev: 'Munkába állítás',
    },
    {
      Id: 4,
      Nev: 'Rezsim felülvizsgálat',
    },
    {
      Id: 5,
      Nev: 'Orvosszakértői vizsgálat',
    },
  ];
}
export function mockTenyTav() {
  return [
    {
      Id: 0,
      Nev: 'Tervezett távozás',
    },

    {
      Id: 1,
      Nev: 'Távollévő',
    },
    null,
  ];
}

export function mockNeme() {
  return [
    {
      Id: 823,
      Nev: 'Férfi',
    },

    {
      Id: 824,
      Nev: 'Nő',
    },
  ];
}
