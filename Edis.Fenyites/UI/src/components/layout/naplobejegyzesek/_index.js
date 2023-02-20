import Vue from 'vue';

import './kozvetitoiEljaras/_index';
import './fegyelmiElkulonites/_index';
import './jutalom/_index';

import { registerComponent } from '../../../utils/vueUtils';
import EsemenyFelvetelNaplobejegyzes from './EsemenyFelvetelNaplobejegyzes.vue';
import UgyKezdemenyezesNaplobejegyzes from './UgyKezdemenyezesNaplobejegyzes.vue';
import UgyMegtagadasaNaplobejegyzes from './UgyMegtagadasaNaplobejegyzes.vue';
import UgyElrendeleseNaplobejegyzes from './UgyElrendeleseNaplobejegyzes.vue';
import JogiKepviseletNaplobejegyzes from './JogiKepviseletNaplobejegyzes.vue';
import UgyReintegraciosJogkorbeUtalasNaplobejegyzes from './UgyReintegraciosJogkorbeUtalasNaplobejegyzes.vue';
import FeddesNaplobejegyzes from './FeddesNaplobejegyzes.vue';
import KioktatasNaplobejegyzes from './KioktatasNaplobejegyzes.vue';
import VisszakuldesNaplobejegyzes from './VisszakuldesNaplobejegyzes.vue';
import OsszefogaloJelentesNaplobejegyzes from './OsszefogaloJelentesNaplobejegyzes.vue';
import TanuMeghallgatasiJegyzokonyvNaplobejegyzes from './TanuMeghallgatasiJegyzokonyvNaplobejegyzes.vue';
import SzemelyiAllomanyiTanuMeghallgatasiJegyzokonyvNaplobejegyzes from './SzemelyiAllomanyiTanuMeghallgatasiJegyzokonyvNaplobejegyzes.vue';
import FelfuggesztesiJavaslatNaplobejegyzes from './FelfuggesztesiJavaslatNaplobejegyzes.vue';
import HataridoModositasiJavaslatNaplobejegyzes from './HataridoModositasiJavaslatNaplobejegyzes.vue';
import HelysziniSzemleNaplobejegyzes from './HelysziniSzemleNaplobejegyzes.vue';
import SzakteruletiVelemenyNaplobejegyzes from './SzakteruletiVelemenyNaplobejegyzes.vue';
import ElsoFokuTargyalasKituzeseNaploBejegyzes from './ElsoFokuTargyalasKituzeseNaploBejegyzes.vue';
import ElsoFokuTargyalasiJegyzokonyvNaploBejegyzes from './ElsoFokuTargyalasiJegyzokonyvNaploBejegyzes.vue';
import FegyelmiUgyOsszevonasNaploBejegyzes from './FegyelmiUgyOsszevonasNaplobejegyzes.vue';
import MasodFokuTargyalasKituzeseNaploBejegyzes from './MasodFokuTargyalasKituzeseNaploBejegyzes.vue';
import HatarozatRogziteseNaploBejegyzes from './HatarozatRogziteseNaploBejegyzes.vue';
import KirendeltVedoKereseNaplobejegyzes from './KirendeltVedoKereseNaplobejegyzes.vue';
import MeghatalmazottVedoKereseNaplobejegyzes from './MeghatalmazottVedoKereseNaplobejegyzes.vue';
import HataridoModositasNaplobejegyzes from './HataridoModositasNaplobejegyzes.vue';
import FelfuggesztesNaploBejegyzes from './FelfuggesztesNaploBejegyzes.vue';
import VedoTelefonosTajekoztatasaNaplobejegyzes from './VedoTelefonosTajekoztatasaNaplobejegyzes.vue';
import FelfuggesztesMegszunteteseNaploBejegyzes from './FelfuggesztesMegszunteteseNaploBejegyzes.vue';
import SzakteruletiVelemenyKereseNaplobejegyzes from './SzakteruletiVelemenyKereseNaplobejegyzes.vue';
import MasodFokuTargyalasiJegyzokonyvNaploBejegyzes from './MasodFokuTargyalasiJegyzokonyvNaploBejegyzes.vue';
import HatarozatRogziteseMasodFokonNaploBejegyzes from './HatarozatRogziteseMasodFokonNaploBejegyzes.vue';
import UjEljarasLefolytatasaNaplobejegyzes from './UjEljarasLefolytatasaNaplobejegyzes.vue';
import SzembesitesiJegyzokonyvNaplobejegyzes from './SzembesitesiJegyzokonyvNaplobejegyzes.vue';
import JogiKepviseletVisszavonasaNaploBejegyzes from './JogiKepviseletVisszavonasaNaploBejegyzes.vue';
import MaganelzarasMegkezdesenekRogziteseNaplobejegyzes from './MaganelzarasMegkezdesenekRogziteseNaplobejegyzes.vue';
import FenyitesVegrehajthatatlannaTeteleNaplobejegyzes from './FenyitesVegrehajthatatlannaTeteleNaplobejegyzes.vue';
import MaganelzarasIdeiglenesenEllenjavalltNaplobejegyzes from './MaganelzarasIdeiglenesenEllenjavalltNaplobejegyzes.vue';
import MaganelzarasMegszakitasaNaplobejegyzes from './MaganelzarasMegszakitasaNaplobejegyzes.vue';
import MaganelzarasVegrehajtvaNaplobejegyzes from './MaganelzarasVegrehajtvaNaplobejegyzes.vue';
import HataridoTullepesMiattiMegszuntetesNaplobejegyzes from './HataridoTullepesMiattiMegszuntetesNaplobejegyzes.vue';
import BuntetoFeljelentesRogziteseNaploBejegyzes from './BuntetoFeljelentesRogziteseNaploBejegyzes.vue';
import MaganelzarasElrendeleseNaplobejegyzes from './MaganelzarasElrendeleseNaplobejegyzes.vue';
import FenyitesVegrehajtasaKeszNaplobejegyzes from './FenyitesVegrehajtasaKeszNaplobejegyzes.vue';
import FegyelmiUgyVegrehajthatosagElevultNaploBejegyzes from './FegyelmiUgyVegrehajthatosagElevultNaploBejegyzes.vue';
import AutomatikusVegrehajtasBefejezeseNaploBejegyzes from './AutomatikusVegrehajtasBefejezeseNaploBejegyzes.vue';
import AutomatikusVegrehajtasMegkezdeseNaploBejegyzes from './AutomatikusVegrehajtasMegkezdeseNaploBejegyzes.vue';
import KivizsgaloModositasaNaplobejegyzes from './KivizsgaloModositasaNaplobejegyzes.vue';
import AutomatikusLezarasNaplobejegyzes from './AutomatikusLezarasNaploBejegyzes.vue';
import SzabadszovegesFegyelmiNaplobejegyzes from './SzabadszovegesFegyelmiNaplobejegyzes.vue';

Vue.component(
  EsemenyFelvetelNaplobejegyzes.name,
  EsemenyFelvetelNaplobejegyzes
);
Vue.component(
  UgyKezdemenyezesNaplobejegyzes.name,
  UgyKezdemenyezesNaplobejegyzes
);
Vue.component(UgyMegtagadasaNaplobejegyzes.name, UgyMegtagadasaNaplobejegyzes);
Vue.component(UgyElrendeleseNaplobejegyzes.name, UgyElrendeleseNaplobejegyzes);
Vue.component(JogiKepviseletNaplobejegyzes.name, JogiKepviseletNaplobejegyzes);
Vue.component(
  OsszefogaloJelentesNaplobejegyzes.name,
  OsszefogaloJelentesNaplobejegyzes
);
Vue.component(
  UgyReintegraciosJogkorbeUtalasNaplobejegyzes.name,
  UgyReintegraciosJogkorbeUtalasNaplobejegyzes
);
Vue.component(FeddesNaplobejegyzes.name, FeddesNaplobejegyzes);
Vue.component(KioktatasNaplobejegyzes.name, KioktatasNaplobejegyzes);
Vue.component(VisszakuldesNaplobejegyzes.name, VisszakuldesNaplobejegyzes);
Vue.component(
  ElsoFokuTargyalasKituzeseNaploBejegyzes.name,
  ElsoFokuTargyalasKituzeseNaploBejegyzes
);
Vue.component(
  HataridoModositasiJavaslatNaplobejegyzes.name,
  HataridoModositasiJavaslatNaplobejegyzes
);
Vue.component(
  FelfuggesztesiJavaslatNaplobejegyzes.name,
  FelfuggesztesiJavaslatNaplobejegyzes
);
Vue.component(
  SzemelyiAllomanyiTanuMeghallgatasiJegyzokonyvNaplobejegyzes.name,
  SzemelyiAllomanyiTanuMeghallgatasiJegyzokonyvNaplobejegyzes
);
Vue.component(
  TanuMeghallgatasiJegyzokonyvNaplobejegyzes.name,
  TanuMeghallgatasiJegyzokonyvNaplobejegyzes
);
Vue.component(
  HelysziniSzemleNaplobejegyzes.name,
  HelysziniSzemleNaplobejegyzes
);
Vue.component(
  SzakteruletiVelemenyNaplobejegyzes.name,
  SzakteruletiVelemenyNaplobejegyzes
);
Vue.component(
  ElsoFokuTargyalasiJegyzokonyvNaploBejegyzes.name,
  ElsoFokuTargyalasiJegyzokonyvNaploBejegyzes
);
Vue.component(
  FegyelmiUgyOsszevonasNaploBejegyzes.name,
  FegyelmiUgyOsszevonasNaploBejegyzes
);
Vue.component(
  FegyelmiUgyVegrehajthatosagElevultNaploBejegyzes.name,
  FegyelmiUgyVegrehajthatosagElevultNaploBejegyzes
);
Vue.component(
  AutomatikusVegrehajtasBefejezeseNaploBejegyzes.name,
  AutomatikusVegrehajtasBefejezeseNaploBejegyzes
);
Vue.component(
  AutomatikusVegrehajtasMegkezdeseNaploBejegyzes.name,
  AutomatikusVegrehajtasMegkezdeseNaploBejegyzes
);
Vue.component(
  KivizsgaloModositasaNaplobejegyzes.name,
  KivizsgaloModositasaNaplobejegyzes
);
Vue.component(
  SzabadszovegesFegyelmiNaplobejegyzes.name,
  SzabadszovegesFegyelmiNaplobejegyzes
);

registerComponent(MasodFokuTargyalasKituzeseNaploBejegyzes);
registerComponent(KirendeltVedoKereseNaplobejegyzes);
registerComponent(MeghatalmazottVedoKereseNaplobejegyzes);
registerComponent(HataridoModositasNaplobejegyzes);
registerComponent(VedoTelefonosTajekoztatasaNaplobejegyzes);
registerComponent(FelfuggesztesNaploBejegyzes);
registerComponent(FelfuggesztesMegszunteteseNaploBejegyzes);
registerComponent(SzakteruletiVelemenyKereseNaplobejegyzes);
registerComponent(MasodFokuTargyalasiJegyzokonyvNaploBejegyzes);
registerComponent(HatarozatRogziteseMasodFokonNaploBejegyzes);
registerComponent(UjEljarasLefolytatasaNaplobejegyzes);
registerComponent(SzembesitesiJegyzokonyvNaplobejegyzes);
registerComponent(JogiKepviseletVisszavonasaNaploBejegyzes);
registerComponent(MaganelzarasMegkezdesenekRogziteseNaplobejegyzes);
registerComponent(FenyitesVegrehajthatatlannaTeteleNaplobejegyzes);
registerComponent(MaganelzarasIdeiglenesenEllenjavalltNaplobejegyzes);
registerComponent(HatarozatRogziteseNaploBejegyzes);
registerComponent(MaganelzarasMegszakitasaNaplobejegyzes);
registerComponent(MaganelzarasVegrehajtvaNaplobejegyzes);
registerComponent(HataridoTullepesMiattiMegszuntetesNaplobejegyzes);
registerComponent(BuntetoFeljelentesRogziteseNaploBejegyzes);
registerComponent(MaganelzarasElrendeleseNaplobejegyzes);
registerComponent(FenyitesVegrehajtasaKeszNaplobejegyzes);
registerComponent(FegyelmiUgyVegrehajthatosagElevultNaploBejegyzes);
registerComponent(KivizsgaloModositasaNaplobejegyzes);
registerComponent(AutomatikusLezarasNaplobejegyzes);
registerComponent(SzabadszovegesFegyelmiNaplobejegyzes);
