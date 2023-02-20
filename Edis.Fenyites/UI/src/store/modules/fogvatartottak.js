//@ts-check
import {
  makeVuexList,
  makeVuexProp,
  mergeModules,
} from '../../utils/vuexUtils';

const moduleName = 'fogvatartottak';

let fogvatartottakList = makeVuexList({
  namespace: moduleName,
  listName: 'intezetiFogvatartottak',
  key: 'id',
});
let fogvatartottKeresesProp = makeVuexProp({
  namespace: moduleName,
  propName: 'fogvatartottKereses',
});

export const FogvatartottakStoreTypes = {
  intezetiFogvatartottak: fogvatartottakList.storeTypes,
  fogvatartottKereses: fogvatartottKeresesProp.storeTypes,
};

export const fogvatartottak = mergeModules([
  fogvatartottakList.module,
  fogvatartottKeresesProp.module,
]);
