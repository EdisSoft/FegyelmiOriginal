import faker, { name, random, image, date, lorem } from 'faker';
import moment from 'moment';

faker.seed(1337);

var intezetek = [
  { Id: 112, Nev: 'Budapest' },
  { Id: 113, Nev: 'Szeged' },
  { Id: 114, Nev: 'Debrecen' },
  { Id: 115, Nev: 'Eger' },
  { Id: 116, Nev: 'Vác' },
  { Id: 117, Nev: 'S. Újhely' },
  { Id: 118, Nev: 'Nagyfa' },
  { Id: 119, Nev: 'Tököl' },
  { Id: 120, Nev: 'Miskolc' },
  { Id: 121, Nev: 'Szolnok' },
  { Id: 122, Nev: 'Kecskemét' },
];

var fenyitesStatuszok = [
  { Id: 1, Nev: 'Végrehajtás alatt' },
  { Id: 2, Nev: 'Fenyítés kiszabva' },
  { Id: 3, Nev: 'Lejárt' },
  { Id: 4, Nev: 'Visszavont' },
  { Id: 5, Nev: 'Törölt' },
];

var fenyitesTipusok = [
  { Id: 1, Nev: 'Kiétkezés csökkentés' },
  { Id: 2, Nev: 'Magánelzárás' },
  { Id: 3, Nev: '-' },
];

var ugyStatuszok = [
  { Id: 1, Nev: 'Kitűzöm a fegy.tárgy.időpontját' },
  { Id: 2, Nev: 'Kitűztem a fegy.tárgy.időpontját' },
  { Id: 3, Nev: 'Végrehajtottam' },
  { Id: 4, Nev: 'Még gondolkozom' },
  { Id: 5, Nev: 'Visszavontam' },
];
var nyilvantartottStatuszok = [
  { Id: 1, Nev: 'Jelenlevő' },
  { Id: 2, Nev: 'Szabadult' },
  { Id: 3, Nev: 'Áthelyezés alatt' },
];
export function getMockFenyitesek(n) {
  var result = [];

  var now = Date.now();
  console.log(now);
  for (let i = 0; i < n; i++) {
    var intezet =
      intezetek[random.number({ min: 0, max: intezetek.length - 1 })];

    var fenyitesStatusz =
      fenyitesStatuszok[
        random.number({ min: 0, max: fenyitesStatuszok.length - 1 })
      ];

    var fenyitesTipus =
      fenyitesTipusok[
        random.number({ min: 0, max: fenyitesTipusok.length - 1 })
      ];

    var ugyStatusz =
      ugyStatuszok[random.number({ min: 0, max: ugyStatuszok.length - 1 })];

    var nyilvantartottStatusz =
      nyilvantartottStatuszok[
        random.number({ min: 0, max: nyilvantartottStatuszok.length - 1 })
      ];
    var hatarIdo = date.between(date.past(1), '2019-07-28');
    var data = {
      FogvatartottId: random.number({ min: 100, max: 10000 }),
      FegyelmiUgyId: i,
      SzuletesiNev: name.lastName() + ' ' + name.firstName(),
      AktNyilvantartasiSzam: 'E -' + random.number({ min: 100, max: 1000 }),
      Elhelyezes: '107-12-6F-101',
      NyilvantartottStatusz: nyilvantartottStatusz.Nev,
      UgyEve: '2019',
      UgySzama: '985',
      UgyStatuszId: 86,
      RelevansIntezet: 'Teszt I.',
      KivizsgaloSid: 86,
      UgyStatusz: ugyStatusz.Nev,
      FenyitesStatusz: fenyitesStatusz.Nev,
      FenyitesTipus: fenyitesTipus.Nev,
      UgyIntezet: intezet.Nev,
      UgyIntezetAzon: intezet.Id,
      KezdemenyezesDatum: date.past(1).toISOString(),
      HataridoDatum: hatarIdo,
      Kivizsgalo: name.lastName() + ' ' + name.firstName(),
      UtolsoModositoSzemely: name.lastName() + ' ' + name.firstName(),
      UtolsoModositasDatum: date.recent(60).toISOString(),
      UgyIndoklas: lorem.lines(3),
      Kep: image.avatar(),
      Lejart: hatarIdo.getTime() < now,
      AHetenJarLe: moment(hatarIdo).format('W') == moment().format('W'),
    };
    result.push(data);
  }
  result[0].KivizsgaloSid = 'S-1-5-21-3684209801-65848465-2457220517-3674';
  return result;
}
