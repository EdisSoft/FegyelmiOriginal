import Vue from 'vue';
var removeNamespace = function(namespace, types) {
  var result = {};
  for (var mutationType in types) {
    if (!result.hasOwnProperty(mutationType)) result[mutationType] = {};
    for (var exprName in types[mutationType]) {
      var expr = types[mutationType][exprName];
      result[mutationType][exprName] = expr.replace(namespace, '');
    }
  }
  return result;
};

export default removeNamespace;

export function hidrateForm(vm, form, entity) {
  for (const key in form) {
    if (form.hasOwnProperty(key) && entity.hasOwnProperty(key)) {
      vm.$set(form, key, entity[key]);
    }
  }
}
export function registerComponent(component) {
  Vue.component(component.name, component);
}
