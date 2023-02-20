import Vue from 'vue';
import Vuelidate from 'vuelidate';
import vuelidateErrorExtractor, { templates } from 'vuelidate-error-extractor';

Vue.use(Vuelidate);
Vue.use(vuelidateErrorExtractor, {
  template: templates.singleErrorExtractor.bootstrap4,
  messages: {
    required: 'Kötelező mező',
    sameAs: 'Nem egyezik a két jelszó',
    email: 'Az email formátuma nem megfelelő',
    minLength: 'A mezőnek legalább {min} karakternek kell lennie',
    minValue: 'Minimum érték: {min}',
    maxValue: 'Maximum érték: {max}',
    minLength: 'Minimum hossz: {min}',
    maxLength: 'Maximum hossz: {max}',
    minValueSelect: 'Kötelező mező',
    minTanu: 'Legalább {min} tanú kíválasztása kötelező',
    maxTanu: 'Maximum {max} tanút lehet kiválasztani',
    minFogvatartott: 'Legalább {min} fogvatartott kiválasztása kötelező',
    maxFogvatartott: 'Maximum {max} fogvatartottat lehet kiválasztani',
    nagyobbMintElrendeles:
      'A megszüntetésnek később kell történnie, mint az elrendelésnek',
  },
});
