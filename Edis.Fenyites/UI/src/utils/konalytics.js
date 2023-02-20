import $ from 'jquery';

export function KonalyticsInit(appData) {
  window.ka =
    window.ka ||
    function() {
      (window.ka.q = window.ka.q || []).push(arguments);
    };

  window.ka('login', {
    product: appData.Product,
    userId: appData.UserId,
    sessionId: appData.SessionId,
    userName: appData.SzemelyzetNev,
    beosztas: appData.Beosztas,
    noemiId: appData.NoemiId,
  });
  if (appData.KonalyticsFrameworkUrl) {
    $.getScript(appData.KonalyticsFrameworkUrl, function() {
      console.log('Konalytics loaded', appData.KonalyticsFrameworkUrl);
    });
  }
}
/**
 * @function konaLog
 * @param  {string} tipus
 * @param  {Object} adat
 * @param  {string} adat.category
 * @param  {string} adat.action
 * @param  {string} adat.label
 */
export function ka(tipus, adat) {
  try {
    window.ka(tipus, adat);
  } catch (error) {
    console.log('Konalytics hiba: ' + error);
    console.log(tipus, adat);
  }
}
