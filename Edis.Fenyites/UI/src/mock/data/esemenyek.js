import faker, { name, random, image, date, lorem, address } from 'faker';
import { mockIntezetek } from './common/intezetek';
import {
  mockEsemenyJellege,
  mockEsemenyNapszaka,
  mockEsemenyHelyszine,
} from './common/cimke';

faker.seed(1337);

let esemenyJellege = mockEsemenyJellege();

export function mockEsemenyek(num = 1000) {
  faker.seed(1337);
  let esemenyek = [];
  let napszakok = mockEsemenyNapszaka();
  let helyszin = mockEsemenyHelyszine();
  for (let i = 0; i < num; i++) {
    let esemenyDatum = date.between(date.recent(40), date.recent(-40));
    let rNev = name.firstName() + ' ' + name.lastName();
    const esemeny = {
      EsemenyId: i,
      LetrehozasDatuma: esemenyDatum.toISOString(),
      Megnevezes: lorem.paragraph(1),
      Bizonyitek: lorem.paragraph(1),
      Leiras: lorem.sentences(random.number({ min: 1, max: 40 })),
      Resztvevok: mockEsemenyResztvevo(i),
      Jelleg: random.arrayElement(esemenyJellege).Nev,
      Napszak: random.arrayElement(napszakok).Nev,
      Hely: random.arrayElement(helyszin).Nev,
      RogzitoSzemely: rNev,
      RogzitoIntezet: random.arrayElement(mockIntezetek()).RovidNev,
    };
    esemenyek.push(esemeny);
  }
  return esemenyek;
}
function mockEsemenyResztvevo(esemenyId) {
  let resztvevok = [];
  let num = random.number({ min: 1, max: 15 });

  for (let i = 0; i < num; i++) {
    let rNev = name.firstName() + ' ' + name.lastName();
    const resztvevo = {
      FogvatartottId: i,
      Nev: rNev,
      NyilvantartasiAzonosito:
        random.alphaNumeric(2).toUpperCase() +
        '-' +
        random.number({ min: 1000, max: 9999 }),
      Ugyszam: random.number({ min: 100, max: 12230 }),
      ErintettsegFokaCimId: i,
      EsemenyId: esemenyId,
    };
    resztvevok.push(resztvevo);
  }
  return resztvevok;
}
