import Vue from 'vue';
const requireComponents = require.context('.', true, /\.vue$/);

requireComponents.keys().forEach(fileName => {
  const componentConfig = requireComponents(fileName);
  const componentToRegister = componentConfig.default || componentConfig;
  var componentName = componentToRegister.name;
  if (componentName) {
    Vue.component(componentName, componentToRegister);
  } else {
    console.error('Nincs komponens név: ', fileName);
  }
});
