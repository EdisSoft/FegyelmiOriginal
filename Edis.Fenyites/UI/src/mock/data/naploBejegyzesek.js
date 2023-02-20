import faker, { name, random, image, date, lorem, address } from 'faker';
import moment from 'moment';
import { mockFegyelmiUgyek } from './fegyelmiugyek';
import { mockUsers } from './users';
import {
  mockEsemenyHelyszine,
  mockNaploTipus,
  mockEsemenyJellege,
} from './common/cimke';
import { mockIntezetek } from './common/intezetek';

export function getMockNaploBejegyzesek(n = 1000) {
  var result = [];
  let fegyelmiUgyek = mockFegyelmiUgyek(1000);
  let helyszin = mockEsemenyHelyszine();
  let intezetek = mockIntezetek();
  let naploTipusok = mockNaploTipus();
  let jellegek = mockEsemenyJellege();

  for (let i = 0; i < n; i++) {
    let fegyelmiUgy = random.arrayElement(fegyelmiUgyek);
    let jegyzokonyvVezetoSzemely = random.arrayElement(mockUsers);
    let fogv = random.arrayElement(mockUsers);
    let meghallgatoSzemely = random.arrayElement(mockUsers);
    let kivizsgaloSzemely = random.arrayElement(mockUsers);
    let rogzitoSzemely = random.arrayElement(mockUsers);
    let hely = random.arrayElement(helyszin);
    let reintegraciosReszleg = random.arrayElement(helyszin);
    let intezet = random.arrayElement(intezetek);
    let naploTipus = random.arrayElement(naploTipusok);
    let jelleg = random.arrayElement(jellegek);
    let tovabbiJelenlevok = [];
    let jelenlevokSzama = random.number({ min: 0, max: 10 });
    for (let z = 0; z < jelenlevokSzama; z++) {
      tovabbiJelenlevok.push(random.arrayElement(mockUsers));
    }
    let naploBejegyzes = {
      Id: i,
      FegyelmiUgyId: fegyelmiUgy.FegyelmiUgyId,

      JegyzokonyvVezetoSzemely: jegyzokonyvVezetoSzemely.text,
      JegyzokonyvVezetoSzemelyId: jegyzokonyvVezetoSzemely.id,

      Fogvatartott: fogv.text,
      FogvatartottId: fogv.id,
      AktNyilvantartasiSzam:
        random.alphaNumeric(2).toUpperCase() +
        '-' +
        random.number({ min: 1000, max: 9999 }),
      FogvatartottNyelve: 'Magyar',

      JegyzokonyvLezarasDatuma: date
        .between(date.recent(40), date.recent(-40))
        .toISOString(),
      LetrehozasDatuma: date
        .between(date.recent(40), date.recent(-40))
        .toISOString(),
      FenyitesKiszabasDatuma: date
        .between(date.recent(40), date.recent(-40))
        .toISOString(),

      TipusCimke: naploTipus.Nev,
      TipusCimkeId: naploTipus.Id,

      AlairtaFl: random.boolean(),

      TovabbiJelenlevok: tovabbiJelenlevok.map(m => m.text).join(', '),

      KihallgatasIntezetId: intezet.Nev,

      KihallgatasHelye: hely.Nev,

      JegyzokonyvTartalma: lorem.paragraphs(random.number({ min: 1, max: 25 })),

      MeghallgatoSzemely: meghallgatoSzemely.text,
      MeghallgatoSzemelyId: meghallgatoSzemely.id,

      KivizsgaloSzemely: kivizsgaloSzemely.text,
      KivizsgaloSzemelyId: kivizsgaloSzemely.id,

      RogzitoSzemely: rogzitoSzemely.text,
      RogzitoSzemelyId: rogzitoSzemely.id,

      ReintegraciosReszleg: reintegraciosReszleg.Nev,
      FegyelmiVetsegTipusaCimke: jelleg.Nev,

      IsFogvatartottElfogadta: random.boolean(),
    };
    result.push(naploBejegyzes);
  }
  return result;
}
