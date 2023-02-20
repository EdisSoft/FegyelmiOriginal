import Vue from 'vue';
import Router from 'vue-router';
import Home from './views/Home.vue';
import About from './views/About.vue';
import Fenyites from './views/Fenyites.vue';
import FegyelmiEsemenyek from './views/FegyelmiEsemenyek.vue';
import FegyelmiUgyek from './views/FegyelmiUgyek.vue';
import JutalomUgyek from './views/JutalomUgyek.vue';
import ArchivUgyek from './views/ArchivUgyek.vue';
import JutalomArchivUgyek from './views/JutalomArchivUgyek.vue';
import NotFound from './views/NotFound.vue';
import Forbidden from './views/Forbidden.vue';
import FogvatartottUgyei from './views/FogvatartottUgyei.vue';
import FogvatartottJutalomai from './views/FogvatartottJutalomai.vue';
import Teszt from './views/Teszt.vue';
import $ from 'jquery';

Vue.use(Router);

var router = new Router({
  routes: [
    {
      path: '/',
      name: 'FegyelmiEsemenyek',
      component: FegyelmiEsemenyek,
      meta: { layout: 'fenyites', navbar: true },
    },
    {
      path: '/FegyelmiUgyek/',
      name: 'FegyelmiUgyek',
      component: FegyelmiUgyek,
      meta: { layout: 'fenyites', navbar: true },
    },
    {
      path: '/JutalomUgyek/',
      name: 'JutalomUgyek',
      component: JutalomUgyek,
      meta: { layout: 'jutalom', navbar: true },
    },
    {
      path: '/FogvatartottUgyei/',
      name: 'FogvatartottUgyei',
      component: FogvatartottUgyei,
      meta: { layout: 'fenyites', navbar: true },
    },
    {
      path: '/fogvatartottJutalomai/',
      name: 'FogvatartottJutalomai',
      component: FogvatartottJutalomai,
      meta: { layout: 'jutalom', navbar: true },
    },
    {
      path: '/ArchivUgyek/',
      name: 'ArchivUgyek',
      component: ArchivUgyek,
      meta: { layout: 'fenyites', navbar: true },
    },
    {
      path: '/JutalomArchivUgyek/',
      name: 'JutalomArchivUgyek',
      component: JutalomArchivUgyek,
      meta: { layout: 'jutalom', navbar: true },
    },
    {
      path: '/Fenyites/',
      name: 'Fenyites',
      component: Fenyites,
      meta: { layout: 'kifuto-ugyek', navbar: true },
    },
    {
      path: '/Teszt/',
      name: 'Teszt',
      component: Teszt,
      meta: { layout: 'default', navbar: true },
    },
    {
      path: '/home',
      name: 'home',
      component: Home,
    },
    {
      path: '/about/',
      name: 'about',
      component: About,
    },
    {
      path: '/nopermission/',
      name: '403',
      meta: {
        displayName: '403',
      },
      component: Forbidden,
    },
    {
      path: '*',
      name: '404',
      meta: {
        displayName: '404',
      },
      component: NotFound,
    },
  ],
});
router.beforeEach((to, from, next) => {
  if (to.meta.navbar) {
    $('body').removeClass('layout-full');
  } else {
    $('body').addClass('layout-full');
  }
  next();
  var description = to.name;
  if (to.query.ev) {
    description += ' - ev: ' + to.query.ev;
  } else if (to.name == 'ArchivUgyek') {
    description += ' - ev: nincs kivalasztva';
  }
});

export default router;
