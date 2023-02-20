const localStoragePlugin = store => {
  store.subscribe((mutation, state) => {
    let moduleName = mutation.type.split('/')[0];
    let moduleData = state[moduleName];
    if (moduleData && moduleData.localStorageKey) {
      let storageKey = moduleData.localStorageKey;
      window.localStorage.setItem(storageKey, JSON.stringify(moduleData));
    }
  });
};

export default localStoragePlugin;
