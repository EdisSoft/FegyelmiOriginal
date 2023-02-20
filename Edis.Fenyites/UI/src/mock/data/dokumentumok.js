export const getMockDokumentumok = function() {
  var dokumentumok = [];
  for (let i = 0; i < 14; i++) {
    dokumentumok.push({ DisplayName: 'Dokumentum ' + (i + 1), Id: 1000 + i });
  }
  return dokumentumok;
};
