import Vue from 'vue';
import { registerComponent } from '../../../../utils/vueUtils';

import FegyelmiElkulonitesElrendeleseNaploBejegyzes from './FegyelmiElkulonitesElrendeleseNaploBejegyzes.vue';
import FegyelmiElkulonitesFelulvizsgalataNaploBejegyzes from './FegyelmiElkulonitesFelulvizsgalataNaploBejegyzes.vue';
import FegyelmiElkulonitesMegszunteteseNaploBejegyzes from './FegyelmiElkulonitesMegszunteteseNaploBejegyzes.vue';
import FegyelmiElkulonitesVegrehajtvaNaploBejegyzes from './FegyelmiElkulonitesVegrehajtvaNaploBejegyzes.vue';

registerComponent(FegyelmiElkulonitesElrendeleseNaploBejegyzes);
registerComponent(FegyelmiElkulonitesFelulvizsgalataNaploBejegyzes);
registerComponent(FegyelmiElkulonitesMegszunteteseNaploBejegyzes);
registerComponent(FegyelmiElkulonitesVegrehajtvaNaploBejegyzes);
