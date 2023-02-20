import faker, { name, random, image, date, lorem, address } from 'faker';
import moment from 'moment';
import { mockIntezetek } from './common/intezetek';
import { mockNyilvantartasiStatuszok } from './common/kodszotar';
import { mockFegyelmiUgyStatusza } from './common/cimke';
import { mockUsers } from './users';

faker.seed(1337);

let ugyStatusz = mockFegyelmiUgyStatusza();

export function mockFegyelmiUgyek(num = 1000) {
  faker.seed(1337);
  let result = [];

  for (let i = 0; i < num; i++) {
    let esemenyDatum = date.between(date.recent(40), date.recent(-40));
    let hatDatum = date.between(date.recent(40), date.recent(-40));
    let fNev = name.firstName() + ' ' + name.lastName();
    let eNev = name.firstName() + ' ' + name.lastName();
    let kNev = name.firstName() + ' ' + name.lastName();
    let ugyIntezet = random.arrayElement(mockIntezetek());
    let ugyStatuszObj = random.arrayElement(ugyStatusz);
    const row = {
      FegyelmiUgyId: i,
      FogvatartottNev: fNev,
      AktNyilvantartasiSzam:
        random.alphaNumeric(2).toUpperCase() +
        '-' +
        random.number({ min: 1000, max: 9999 }),
      UgyIntezetAzon: ugyIntezet.Id,
      UgyEve: random.number({ min: 2012, max: 2019 }),
      UgySzama: random.number({ min: 1000, max: 9999 }),
      UgyStatuszId: ugyStatuszObj.Id,
      UgyStatusz: ugyStatuszObj.Nev,
      Elrendelo: eNev,
      Kivizsgalo: kNev,
      Jelleg: random.word(),
      FegyelmiUgyTipus: random.word(),
      NyilvantartottStatusz: random.arrayElement(mockNyilvantartasiStatuszok())
        .Nev,
      Intezet: ugyIntezet.Nev,
      Hatarido: hatDatum,
      Sertettek: [
        random.arrayElement(mockUsers).text,
        random.arrayElement(mockUsers).text,
      ],
      Elhelyezes: 'elhelyezés: ' + random.word(),
      EsemenyDatuma: esemenyDatum,
      Csuszas: random.number({ min: 1, max: 40 }),
      TenylegesSzabadulasDatumaSzoveg: 'Várható szabadulás: 2021.01.21',
      ToltottIdoSzazalekban: random.number({ min: 0, max: 100 }),
      IteletIdoSzovegesen: 'Összesen: 8 év 9 hónap 17 nap ',
    };
    result.push(row);
  }
  return result;
}
