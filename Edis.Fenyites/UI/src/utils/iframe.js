/**
 *
 * @param {String[]} origins
 * @param {Object[]} eventHandlers
 * @param {String} eventHandlers.eventName
 * @param {Function} eventHandlers.handler
 */
export const createIframeListener = function(origins, eventHandlers) {
  const isProd = process.env.NODE_ENV === 'production';
  window.addEventListener('load', e => {
    window.addEventListener(
      'message',
      iframeEvent => {
        if (
          isProd &&
          !origins.some(
            origin =>
              iframeEvent.origin.startsWith('http://' + origin) ||
              iframeEvent.origin.startsWith('https://' + origin)
          )
        ) {
          console.warn('Origin nem engedélyezett', iframeEvent.origin);
          return;
        }
        eventHandlers.forEach(eventHandler => {
          if (
            iframeEvent.data &&
            iframeEvent.data.eventName == eventHandler.eventName
          ) {
            eventHandler.handler(iframeEvent);
          }
        });
      },
      false
    );
  });
};

/**
 *
 * @param {String} eventName
 * @param {Object} data
 */
export const emitIframeEvent = function(eventName, data) {
  window.parent.postMessage({ eventName, value: data }, '*');
};
