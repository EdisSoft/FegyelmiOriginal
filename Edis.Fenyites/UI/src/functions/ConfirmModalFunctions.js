import { camelCaseString } from '../utils/common';
import { NotificationFunctions } from './notificationFunctions';
import { apiService, eventBus } from '../main';
import { FegyelmiUgyFunctions } from './FegyelmiUgyFunctions';
import moment from 'moment';
import { JutalmiUgyFunctions } from './JutalmiUgyFunctions';
import store from '../store';
import { JutalomUgyStoreTypes } from '../store/modules/jutalomugy';

class ConfirmModal {
  async OpenJogiKepviseletVisszavonasaConfirmModal(fegyelmiUgyIds) {
    let ugyek = FegyelmiUgyFunctions.GetFegyelmiUgyekFromStore(fegyelmiUgyIds);
    let nevek = ugyek.map(
      (m) => `${m.AktNyilvantartasiSzam} ${camelCaseString(m.FogvatartottNev)}`
    );
    if (ugyek.length == 0) {
      NotificationFunctions.ErrorAlert(
        'Jogi képviselet visszavonása',
        'Ügy nem található'
      );
      return;
    }
    let success = await NotificationFunctions.ConfirmModal(
      `<b>${nevek.join(', ')}</b> visszavonta a jogi képviselet kérését?`,
      {
        title: 'Megerősítés',
      }
    );
    if (success) {
      try {
        await apiService.JogiKepviseletVisszavonasa({
          fegyelmiUgyIds: fegyelmiUgyIds,
        });
        // @ts-ignore
        eventBus.$emit('Sidebar:ugyReszletek:refresh');
        eventBus.$emit('Sidebar:jutalmiUgyReszletek:refresh');
        NotificationFunctions.SuccessAlert(
          'Jogi képviselet visszavonása',
          `${nevek.join(', ')} által kért jogi képviselet visszavonva.`
        );
      } catch (error) {
        console.log(error);
        NotificationFunctions.AjaxError({
          title: 'Jogi képviselet visszavonása',
          text: 'Sikertelen mentés',
          errorObj: error,
        });
      }
    }
  }
  async OpenFelfuggesztesMegszunteteseConfirmModal(fegyelmiUgyIds) {
    console.log(
      'OpenFelfuggesztesMegszunteteseConfirmModal fegyelmiUgyIds',
      fegyelmiUgyIds
    );
    let hataridok = null;
    try {
      hataridok = await apiService.GetFegyelmiHatarido({ fegyelmiUgyIds });
    } catch (error) {
      NotificationFunctions.AjaxError({
        title: 'Hiba',
        text: 'Adatok lekérdezése sikertelen',
        errorObj: error,
      });
    }
    var formazottHataridok = hataridok.map((m) =>
      moment(m).format('YYYY.MM.DD.')
    );
    console.log('hataridok', hataridok);
    let ugyek = FegyelmiUgyFunctions.GetFegyelmiUgyekFromStore(fegyelmiUgyIds);
    let nevek = ugyek.map(
      (m) => `${m.AktNyilvantartasiSzam} ${camelCaseString(m.FogvatartottNev)}`
    );
    let statuszok = ugyek.map((m) => m.UgyStatusz);
    if (ugyek.length == 0) {
      NotificationFunctions.ErrorAlert(
        'Felfüggesztés megszüntetése',
        'Ügy nem található'
      );
      return;
    }
    let success = await NotificationFunctions.ConfirmModal(
      `Biztosan megszakítja <b> ${nevek.join(
        ', '
      )}</b> felfüggesztését? A státusz: ${statuszok.join(
        ', '
      )}. Az új határidő <b>${formazottHataridok.join(', ')}</b>`,
      {
        title: 'Megerősítés',
      }
    );
    if (success) {
      try {
        await apiService.FelfuggesztesMegszunteteseConfrimModal({
          fegyelmiUgyIds: fegyelmiUgyIds,
        });
        // @ts-ignore
        eventBus.$emit('Sidebar:ugyReszletek:refresh');
        eventBus.$emit('Sidebar:jutalmiUgyReszletek:refresh');
        NotificationFunctions.SuccessAlert(
          'Felfüggesztés megszüntetve',
          `${nevek.join(', ')} felfüggesztése megszüntetve.`
        );
      } catch (error) {
        console.log(error);
        NotificationFunctions.AjaxError({
          title: 'Felfüggesztés megszüntetése',
          text: 'Sikertelen mentés',
          errorObj: error,
        });
      }
    }
  }
  async OpenFenyitesVegrahajtasaKeszConfirmModal(fegyelmiUgyIds) {
    let success = await NotificationFunctions.ConfirmModal(
      'Biztosan végrehajtásra került a kiszabott fenyítés?',
      {
        title: 'Megerősítés',
      }
    );
    if (success) {
      try {
        await apiService.FenyitesVegrahajtasaKesz({
          fegyelmiUgyIds: fegyelmiUgyIds,
        });
        // @ts-ignore
        eventBus.$emit('Sidebar:ugyReszletek:refresh');
        eventBus.$emit('Sidebar:jutalmiUgyReszletek:refresh');
        NotificationFunctions.SuccessAlert(
          'Fenyítés végrehajtása kész',
          'Az ügy lezárása megtörtént.'
        );
      } catch (error) {
        console.log(error);
        NotificationFunctions.AjaxError({
          title: 'Fenyítés végrehajtása kész',
          text: 'Az ügyet nem sikerült lezárni.',
          errorObj: error,
        });
      }
    }
  }
  async OpenJutalomKihirdetesConfirmModal(jutalomIds) {
    let ugyek = JutalmiUgyFunctions.GetJutalomUgyekFromStore(jutalomIds);
    if (jutalomIds.length == 0) {
      NotificationFunctions.ErrorAlert('Kihírdetés', 'Ügy nem található');
      return;
    }
    // TODO
    let success = await NotificationFunctions.ConfirmModal(
      `Biztosan kihirdettük a kijelölt ügyeket (${jutalomIds.length} db)?`,
      {
        title: 'Megerősítés',
      }
    );
    if (success) {
      try {
        await apiService.KihirdetesMentes({
          UgyIds: jutalomIds,
        });
        // @ts-ignore
        NotificationFunctions.SuccessAlert(
          'Kihírdetés',
          `${jutalomIds.length} db ügy kihírdetve.`
        );
        store.dispatch(JutalomUgyStoreTypes.actions.setJutalomUgyekSelected, {
          value: [],
        });
      } catch (error) {
        console.log(error);
        NotificationFunctions.AjaxError({
          title: 'Kihírdetés',
          text: 'Nem sikerült az ügyek kihírdetése',
          errorObj: error,
        });
      }
    }
  }
  async OpenNyitottFegyelmiVagyJutalomConfirmModal(fogvatartasIds) {
    let figyelmeztetoSzoveg = null;
    try {
      figyelmeztetoSzoveg = await apiService.VanNyitottFegyelmiVagyJutalom({
        fogvatartasIds: fogvatartasIds,
      });
    } catch (error) {
      NotificationFunctions.AjaxError({
        title: 'Hiba',
        text: 'Adatok lekérdezése sikertelen',
        errorObj: error,
      });
      return false;
    }
    if (figyelmeztetoSzoveg.length != 0) {
      let success = await NotificationFunctions.ConfirmModal(
        `${figyelmeztetoSzoveg}`,
        {
          title: 'Megerősítés',
        }
      );
      return success;
    }

    return true;
  }

  async OpenJutalomVisszavonasConfirmModal(jutalomIds) {
    let success = await NotificationFunctions.ConfirmModal(
      'Biztosan visszavonja az előterjesztést? ',
      {
        title: 'Téves rögzítés',
      }
    );
    if (success) {
      try {
        await apiService.VisszavonasMentes({
          UgyIds: jutalomIds,
        });
        JutalmiUgyFunctions.CloseModalsAndPanels();
        // @ts-ignore
        NotificationFunctions.SuccessAlert(
          'Téves rögzítés',
          'Sikeresen visszavonta az előterjesztést.'
        );
        store.dispatch(JutalomUgyStoreTypes.actions.setJutalomUgyekSelected, {
          value: [],
        });
      } catch (error) {
        console.log(error);
        NotificationFunctions.AjaxError({
          title: 'Téves rögzítés',
          text: 'Nem sikerült az előterjesztés visszavonása.',
          errorObj: error,
        });
      }
    }
  }
}

export const ConfirmModalFunctions = new ConfirmModal();
