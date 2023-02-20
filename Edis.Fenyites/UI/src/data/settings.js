const isProd = process.env.NODE_ENV === 'production';
const settings = {
  isProd: isProd,
  baseUrl: isProd
    ? location.pathname.replace('/app/', '/')
    : location.protocol + '//' + location.hostname + ':' + 13300 + '/',
  baseUrlCsatolmanyKezelo: isProd
    ? location.origin + '/CsatolmanyKezelo/'
    : location.protocol + '//' + location.hostname + ':' + 7931 + '/',
  timeStamp: +new Date(),
  isMock: false,
};

export default settings;
