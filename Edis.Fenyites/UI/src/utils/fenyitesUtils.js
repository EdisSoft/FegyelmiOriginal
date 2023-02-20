export function getUgyszam(fenyites) {
  if (!fenyites) {
    return '';
  }
  return (
    fenyites.UgyIntezetAzon + '/' + fenyites.UgyEve + '/' + fenyites.UgySzama
  );
}
