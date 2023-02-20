import { name, date, system } from 'faker';

export function getMockNyomtatottDokumentumok() {
  var dokumentumok = [];
  for (let i = 0; i < 11; i++) {
    dokumentumok.push({
      Id: i,
      Alszam: i,
      Nyomtatvany: system.commonFileName('doc'),
      Rogzito: name.lastName() + ' ' + name.firstName(),
      Datum: date.past(1).toISOString(),
    });
  }
  return dokumentumok;
}
