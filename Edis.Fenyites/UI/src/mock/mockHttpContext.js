import faker, { name, random, image, date, lorem, address } from 'faker';
faker.seed(1337);
import settings from '../data/settings';
import { getMockLoginSuccess, getMockloginError } from './data/auth';
import { getMockDokumentumok } from './data/dokumentumok';
import { getMockNyomtatottDokumentumok } from './data/nyomtatottDokumentumok';
import { getMockFenyitesek } from './data/fenyitesek';
import { mockEsemenyek } from './data/esemenyek';
import { mockFegyelmiUgyek } from './data/fegyelmiugyek';
import {
  mockFegyelmiVetsegTipusa,
  mockEsemenyJellege,
  mockEsemenyHelyszine,
  mockEsemenyNapszaka,
} from './data/common/cimke';
import { mockUsers } from './data/users';
import { getMockNaploBejegyzesek } from './data/naploBejegyzesek';

let fenyitesek = getMockFenyitesek(2000);
let esemenyek = mockEsemenyek(1000);
let fegyelmiUgyek = mockFegyelmiUgyek(1000);

/**
 *
 * @typedef {HttpContext} HttpContext
 */

class HttpContext {
  delay = 150;
  constructor() {}
  /**
   *
   * @param {Object} payload
   * @param {String} payload.url
   * @param {Object} payload.data
   * @param {Object} payload.options
   * @param {Boolean} payload.options.fileUpload
   * @param {String}  payload.options.dataType
   * @param {Boolean} payload.options.preloader
   * @param {Boolean} payload.mock
   */
  post({ url, data, options, mock }) {
    if (options && options.preloader) {
      // Preloader ON
    }

    console.log(
      `${url} - mock: ${mock},`,
      `Delay: ${this.delay},`,
      'Data: ',
      data || 'Nincs adat'
    );

    var result = new Promise((resolve, reject) => {
      setTimeout(() => {
        switch (url) {
          case settings.baseUrl + 'Api/Home/GetToolbarData':
            if (mock) {
              resolve(getMockLoginSuccess());
            } else {
              reject(getMockloginError());
            }
            break;
          // case settings.baseUrl + 'Api/Fenyites/GetFenyitesek':
          //   if (mock) {
          //     resolve(fenyitesek);
          //   } else {
          //     reject(null);
          //   }
          //   break;
          case settings.baseUrl + 'Api/Home/GetAppData':
            if (mock) {
              var result = getMockLoginSuccess();
              result.Dokumentumok = getMockDokumentumok();
              result.VedoDokumentumok = getMockDokumentumok();
              resolve(result);
            } else {
              reject(getMockloginError());
            }
            break;
          case settings.baseUrl + 'Api/Nyomtatvany/GetNyomtatvanyLetoltesek':
            if (mock) {
              resolve(getMockNyomtatottDokumentumok());
            } else {
              reject(null);
            }
            break;
          case settings.baseUrl + 'Api/Esemeny/GetEsemenyek':
            if (mock) {
              resolve(esemenyek);
            } else {
              reject(null);
            }
            break;
          case settings.baseUrl + 'Api/FegyelmiUgy/GetFegyelmiUgyek':
            if (mock) {
              resolve(fegyelmiUgyek);
            } else {
              reject(null);
            }
            break;
          case settings.baseUrl +
            'Api/FegyelmiUgy/GetReintegraciosTisztDontesModalData':
            if (mock) {
              let fegyelmiUgyTipusok = mockFegyelmiVetsegTipusa();
              resolve({
                ReintegraciosReszlegOptions: [
                  { id: 1, text: 'Jó részleg' },
                  { id: 2, text: 'Nem jó részleg' },
                  { id: 3, text: 'Okés részleg' },
                  { id: 4, text: 'Menő részleg' },
                ],
                FegyelmiVetsegTipusaOptions: fegyelmiUgyTipusok.map(m => {
                  return { id: m.Id, text: m.Nev };
                }),

                ReintegraciosReszlegId: 3,
                FenyitesKiszabasIdeje: new Date().toISOString(),
                FenyitesKiszabasIdejeMinDate: date.recent(10).toISOString(),
                FegyelmiVetsegTipusaCimkeId: 101,
                Indoklas: 'Indoklás mock szerverről...',
              });
            } else {
              reject(null);
            }
            break;
          case settings.baseUrl + 'Api/Esemeny/GetEsemenyRogzitesModalData':
            if (mock) {
              let esemenyJellege = mockEsemenyJellege();
              let esemenyHelyszin = mockEsemenyHelyszine();
              let esemenyNapszak = mockEsemenyNapszaka();
              let res = {
                EsemenyJellegOptions: esemenyJellege.map(m => {
                  return { id: m.Id, text: m.Nev };
                }),
                HelyOptions: esemenyHelyszin.map(m => {
                  return { id: m.Id, text: m.Nev };
                }),
                NapszakOptions: esemenyNapszak.map(m => {
                  return { id: m.Id, text: m.Nev };
                }),
                EszleloDefault: null,
                TanukDefault: [],
                SertettekDefault: [],
                ElkovetokDefault: [],
                TanukOptions: [],
                EszleloOptions: [],
                SertettekOptions: [],
                ElkovetokOptions: [],

                EsemenyJellegCimkeId: random.arrayElement(esemenyJellege).Id,
                EsemenyRovidLeirasa: '',
                Leiras: '',
                Bizonyitek: '',
                NapszakCimkeId: random.arrayElement(esemenyNapszak).Id,
                EsemenyHelyCimkeId: random.arrayElement(esemenyHelyszin).Id,
                EsemenyDatuma: new Date().toISOString(),
                EsemenyRogzitesIdeje: new Date().toISOString(),
                Tanuk: [],
                Sertettek: [],
                Elkovetok: [],
                EszleloId: null,
              };
              if (data.esemenyId) {
                let dupe = random.arrayElement(mockUsers);
                res.EszleloDefault = random.arrayElement(mockUsers);
                res.TanukDefault = [random.arrayElement(mockUsers), dupe, dupe];
                res.SertettekDefault = [
                  random.arrayElement(mockUsers),
                  random.arrayElement(mockUsers),
                ];
                res.ElkovetokDefault = [
                  random.arrayElement(mockUsers),
                  random.arrayElement(mockUsers),
                ];
                res.EsemenyRovidLeirasa = 'EsemenyRovidLeirasa mock szerver';
                res.Leiras = 'Leírás mock szerver';
                res.Bizonyitek = 'Bizonyitek mock szerver';
              }
              resolve(Object.freeze(res));
            } else {
              reject(null);
            }
            break;
          case settings.baseUrl +
            'Api/FegyelmiUgy/ReintegraciosTisztDontesModalMentes':
            if (mock) {
              resolve({ success: true });
            } else {
              reject(null);
            }
            break;
          case settings.baseUrl + 'Api/Esemeny/FindFogvatartottakForSelect':
            if (mock) {
              let users = mockUsers.filter(f => f.text.includes(data.term));
              resolve(users);
            } else {
              reject(null);
            }
            break;
          case settings.baseUrl +
            'Api/NaploBejegyzes/GetNaplobejegyzesekByFegyelmiUgyId':
            if (mock) {
              let naplobejegyzesek = getMockNaploBejegyzesek(
                random.number({ min: 5, max: 30 })
              );
              let esemeny = random.arrayElement(esemenyek);
              resolve({ naplobejegyzesek, esemeny });
            } else {
              reject(null);
            }
            break;
          default:
            reject(`HttpFunctions - Mock function not implemented (${url})`);
            break;
        }
        // Preloader OFF
      }, this.delay);
    });
    return result;
  }
}

export default new HttpContext();
