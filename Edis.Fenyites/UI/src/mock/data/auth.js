export const getMockLoginSuccess = function() {
  return {
    UserData: {
      RogzitoIntezet: { Id: 135, Nev: 'Teszt intézet' },
      ValaszthatoIntezetek: [
        { Id: 135, Nev: 'Teszt intézet 1' },
        { Id: 113, Nev: 'Teszt intézet 2' },
      ],
      SzemelyzetSid: 'S-1-5-21-3684209801-65848465-2457220517-3674',
      SzemelyzetNev: 'KONASOFT\\teszt',
      IntezetNev: 'Teszt intézet',
      IntezetRovidNev: 'Teszt I.',
      IntezetAzon: 'TI',
      Jogosultsagok: [
        'jfk_fegyjutmegtekinto',
        'fegyelmi_egyeb_szakterulet',
        'fegyelmi_jogkor_gyakorloja',
        'fegyelmi_reintegracios_tiszt',
      ],
    },
    Title: null,
    Message: 'Sikeres bejelentkezés',
    ResponseCode: 0,
    OriginalError: null,
    ResponseData: null,
  };
};
export const getMockloginError = function() {
  return {
    Email: null,
    DisplayName: null,
    Avatar: null,
    Id: -1,
    Title: null,
    Message: 'Sikertelen bejelentkezés',
    ResponseCode: 4,
    OriginalError: null,
    ResponseData: null,
  };
};

export const getMocklogoutSuccess = function() {
  return {
    Title: null,
    Message: 'Sikeres kijelentkezés',
    ResponseCode: 0,
    OriginalError: null,
    ResponseData: null,
  };
};
