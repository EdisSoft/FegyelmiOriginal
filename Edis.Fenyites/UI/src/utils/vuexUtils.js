function capitalize(s) {
  if (typeof s !== 'string') return '';
  return s.charAt(0).toUpperCase() + s.slice(1);
}

var removeNamespace = function(namespace, types) {
  var result = {};
  namespace = namespace.toLowerCase();
  for (var exprName in types) {
    var expr = types[exprName];
    result[exprName] = expr.replace(namespace + '/', '');
  }
  return result;
};

function validatePayload(payload) {
  if (!payload) {
    throw 'The payload parameter is null';
  }
  if (!payload.value && payload.value != 0) {
    throw 'The value parameter in the payload is ' + payload.value;
  }
  return true;
}

export function mergeModules(additionalModuls, baseModul = null) {
  if (!baseModul) {
    baseModul = {
      namespaced: true,
      state: {},
      getters: {},
      actions: {},
      mutations: {},
    };
  }
  additionalModuls.forEach(additionalModul => {
    baseModul.state = { ...baseModul.state, ...additionalModul.state };
    baseModul.getters = { ...baseModul.getters, ...additionalModul.getters };
    baseModul.actions = { ...baseModul.actions, ...additionalModul.actions };
    baseModul.mutations = {
      ...baseModul.mutations,
      ...additionalModul.mutations,
    };
  });
  return baseModul;
}

function vuexListStoreTypes(namespace, listName) {
  let name = capitalize(listName);
  namespace = namespace.toLowerCase();
  let obj = {
    getAll: `${namespace}/get${name}`,
    isloaded: `${namespace}/isLoaded_${name}`,
    getByKey: `${namespace}/getFrom${name}ByKey`,

    set: `${namespace}/set${name}`,
    add: `${namespace}/add${name}`,
    addRange: `${namespace}/addRange${name}`,
    remove: `${namespace}/remove${name}`,
    removeRange: `${namespace}/removeRange${name}`,
    clear: `${namespace}/clear${name}`,
  };

  return obj;
}

function vuexListMutations(namespace, listName) {
  let name = capitalize(listName);
  let nameUpper = listName.toUpperCase();
  namespace = namespace.toLowerCase();
  let obj = {
    SET: `${namespace}/SET_${nameUpper}`,
    ADD: `${namespace}/ADD_${nameUpper}`,
    ADD_RANGE: `${namespace}/ADD_RANGE_${nameUpper}`,
    REMOVE: `${namespace}/REMOVE_${nameUpper}`,
    REMOVE_RANGE: `${namespace}/REMOVE_RANGE_${nameUpper}`,
    CLEAR: `${namespace}/CLEAR_${nameUpper}`,
  };

  return obj;
}

let isNewerVersionByTimeStamp = function(oldValue, value, timeStampKey) {
  return (
    !timeStampKey ||
    !oldValue[timeStampKey] ||
    !value[timeStampKey] ||
    new Date(oldValue[timeStampKey]).getTime() <
      new Date(value[timeStampKey]).getTime()
  );
};
/**
 *
 * @param {VuexUtils.VuexListParams} obj
 */
export function makeVuexList({
  namespace,
  listName,
  key = 'Id',
  value = new Map(),
  timeStampKey = null,
  beforeCallBacks = {},
}) {
  let name = capitalize(listName);
  let nameUpper = listName.toUpperCase();
  let nameLower = listName.toLowerCase();

  if (!key) {
    throw 'Error: key parameter is required!';
  }

  const state = {};
  state[nameLower] = value;
  state[`isLoaded_${name}`] = false;
  state[`version_${name}`] = 1;

  const getters = {
    [`get${name}`]: state => {
      let v = state[`version_${name}`];
      return Array.from(state[nameLower].values());
    },
    [`isLoaded_${name}`]: state => {
      return state[`isLoaded_${name}`];
    },
    [`getFrom${name}ByKey`]: state => val => {
      let v = state[`version_${name}`];
      return state[nameLower].get(val);
    },
  };

  const actions = {
    async [`set${name}`]({ commit, state }, payload) {
      validatePayload(payload);
      if (beforeCallBacks.set) {
        await beforeCallBacks.set(payload);
      }
      commit(`SET_${nameUpper}`, payload);
    },

    async [`add${name}`]({ commit, state }, payload) {
      validatePayload(payload);
      if (beforeCallBacks.add) {
        await beforeCallBacks.add(payload);
      }

      commit(`ADD_${nameUpper}`, payload);
    },
    async [`addRange${name}`]({ commit, state }, payload) {
      validatePayload(payload);
      if (beforeCallBacks.addRange) {
        await beforeCallBacks.addRange(payload);
      }

      commit(`ADD_RANGE_${nameUpper}`, payload);
    },
    async [`remove${name}`]({ commit, state }, payload) {
      validatePayload(payload);
      if (beforeCallBacks.remove) {
        await beforeCallBacks.remove(payload);
      }
      commit(`REMOVE_${nameUpper}`, payload);
    },
    async [`removeRange${name}`]({ commit, state }, payload) {
      validatePayload(payload);
      if (beforeCallBacks.removeRange) {
        await beforeCallBacks.removeRange(payload);
      }

      commit(`REMOVE_RANGE_${nameUpper}`, payload);
    },
    async [`clear${name}`]({ commit }, payload) {
      if (beforeCallBacks.clear) {
        await beforeCallBacks.clear(payload);
      }
      commit(`CLEAR_${nameUpper}`, payload);
    },
  };

  const mutations = {
    [`SET_${nameUpper}`](state, { value }) {
      let map = new Map();
      value.forEach(element => {
        map.set(element[key], element);
      });
      if (!state[`isLoaded_${name}`]) {
        state[`isLoaded_${name}`] = value;
      }
      state[nameLower] = map;
      state[`version_${name}`] = state[`version_${name}`] + 1;
    },
    ['SET_IS_LOADED'](state, { value }) {
      state[`isLoaded_${name}`] = value;
    },
    [`ADD_${nameUpper}`](state, { value }) {
      let oldValue = state[nameLower].get(value[key]);
      if (
        !oldValue ||
        isNewerVersionByTimeStamp(oldValue, value, timeStampKey)
      ) {
        state[nameLower].set(value[key], value);
        state[`version_${name}`] = state[`version_${name}`] + 1;
      }
    },
    [`ADD_RANGE_${nameUpper}`](state, { value }) {
      let mapObj = state[nameLower];
      let modifiedCt = 0;
      value.forEach(newValue => {
        let oldValue = mapObj.get(newValue[key]);
        if (
          oldValue &&
          !isNewerVersionByTimeStamp(oldValue, newValue, timeStampKey)
        ) {
          return false;
        }
        state[nameLower].set(newValue[key], newValue);
        modifiedCt++;
      });
      if (modifiedCt > 0) {
        state[`version_${name}`] = state[`version_${name}`] + 1;
      }
    },
    [`REMOVE_${nameUpper}`](state, { value }) {
      let oldValue = state[nameLower].get(value[key]);
      if (
        oldValue &&
        isNewerVersionByTimeStamp(oldValue, value, timeStampKey)
      ) {
        state[nameLower].delete(value[key]);
        state[`version_${name}`] = state[`version_${name}`] + 1;
      }
    },
    [`REMOVE_RANGE_${nameUpper}`](state, { value }) {
      let mapObj = state[nameLower];
      let modifiedCt = 0;
      value.forEach(newValue => {
        let oldValue = mapObj.get(newValue[key]);
        if (
          oldValue &&
          isNewerVersionByTimeStamp(oldValue, newValue, timeStampKey)
        ) {
          state[nameLower].delete(newValue[key]);
          modifiedCt++;
        }
      });
      if (modifiedCt > 0) {
        state[`version_${name}`] = state[`version_${name}`] + 1;
      }
    },
    [`CLEAR_${nameUpper}`](state) {
      state[nameLower] = new Map();
      state[`version_${name}`] = state[`version_${name}`] + 1;
    },
  };
  let storeTypes = vuexListStoreTypes(namespace, listName);
  /**
   * @type {VuexUtils.VuexMutations}
   */
  let storeMutationsN = vuexListMutations(namespace, listName);
  /**
   * @type {VuexUtils.VuexMutations}
   */
  let storeMutations = removeNamespace(namespace, storeMutationsN);

  let module = {
    namespaced: true,
    state,
    getters,
    actions,
    mutations,
  };
  return { module, storeTypes, storeMutationsN, storeMutations };
}
let a = new Map();
/**
 *
 * @param {string} name
 * @returns {VuexUtils.BeforeListCallbacks}
 */
export function vuexListTestCallbacks(name) {
  let fakeTypes = vuexListStoreTypes('', '');

  let obj = {};

  for (const key in fakeTypes) {
    obj[key] = async val => {
      console.log(`${name}(${key}): `, val);
    };
  }
  return obj;
}

function vuexPropStoreTypes(namespace, propName) {
  let name = capitalize(propName);
  namespace = namespace.toLowerCase();
  return {
    get: `${namespace}/get${name}`,
    set: `${namespace}/set${name}`,
  };
}

export function makeVuexProp({
  namespace,
  propName,
  value = null,
  beforeCallBacks = {},
}) {
  if (!propName) {
    throw 'Error: propName parameter is required!';
  }
  let name = capitalize(propName);
  let nameUpper = name.toUpperCase();
  let nameLower = name.toLowerCase();

  const state = {};
  state[nameLower] = value;
  state[`version_${name}`] = 0;

  const getters = {
    [`get${name}`]: state => {
      return state[nameLower];
    },
  };

  const actions = {
    async [`set${name}`]({ commit }, payload) {
      if (beforeCallBacks.set) {
        await beforeCallBacks.set(payload);
      }
      commit(`SET_${nameUpper}`, payload);
    },
  };

  const mutations = {
    [`SET_${nameUpper}`]: (state, { value }) => {
      state[`version_${name}`] = state[`version_${name}`] + 1;
      state[nameLower] = value;
    },
  };

  let storeTypes = vuexPropStoreTypes(namespace, name);
  let module = {
    namespaced: true,
    state,
    getters,
    actions,
    mutations,
  };
  return { module, storeTypes };
}
