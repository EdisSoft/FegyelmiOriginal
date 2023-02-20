const webpack = require('webpack');
const path = require('path');
const CKEditorWebpackPlugin = require('@ckeditor/ckeditor5-dev-webpack-plugin');
const { styles } = require('@ckeditor/ckeditor5-dev-utils');

module.exports = {
  publicPath: './',
  pwa: {
    name: 'Teszt',
    themeColor: '#a496da',
    workboxOptions: {
      skipWaiting: true,
      importWorkboxFrom: 'local',
      clientsClaim: true,
    },
  },
  css: {
    sourceMap: true,
  },
  transpileDependencies: [
    'vue-tables-2',
    'vue2-dropzone',
    /ckeditor5-[^/\\]+[/\\]src[/\\].+\.js$/,
    /ckeditor5-/,
    '@ckeditor/ckeditor5-vue',
    'lodash-es',
    'summernote',
  ],
  configureWebpack: {
    plugins: [
      new webpack.ProvidePlugin({
        $: 'jquery',
        jquery: 'jquery',
        'window.jQuery': 'jquery',
        jQuery: 'jquery',
      }),
      new CKEditorWebpackPlugin({
        // See https://ckeditor.com/docs/ckeditor5/latest/features/ui-language.html
        language: 'hu',
      }),
    ],
    performance: {
      hints: false,
    },
  },
  chainWebpack: config => {
    // (1.) To handle editor icons, get the default rule for *.svg files first:
    const svgRule = config.module.rule('svg');

    // Then you can either:
    //
    // * clear all loaders for existing 'svg' rule:
    //
    //		svgRule.uses.clear();
    //
    // * or exclude ckeditor directory from node_modules:
    svgRule.exclude.add(path.join(__dirname, 'node_modules', '@ckeditor'));
    svgRule.exclude.add(path.join(__dirname + '/src/assets/images/ckeditor'));

    // Add an entry for *.svg files belonging to CKEditor. You can either:
    //
    // * modify the existing 'svg' rule:
    //
    //		svgRule.use( 'raw-loader' ).loader( 'raw-loader' );
    //
    // * or add a new one:
    config.module
      .rule('cke-svg')
      .test(/ckeditor5-[^/\\]+[/\\]theme[/\\]icons[/\\][^/\\]+\.svg$/)
      .use('raw-loader')
      .loader('raw-loader');
    config.module
      .rule('cke-plugin-browser')
      .test(/src[/\\]assets[/\\]images[/\\]ckeditor[/\\][^/\\]+\.svg$/)
      .use('raw-loader')
      .loader('raw-loader');

    // (2.) Transpile the .css files imported by the editor using PostCSS.
    // Make sure only the CSS belonging to ckeditor5-* packages is processed this way.
    config.module
      .rule('cke-css')
      .test(/ckeditor5-[^/\\]+[/\\].+\.css$/)
      .use('postcss-loader')
      .loader('postcss-loader')
      .tap(() => {
        return styles.getPostCssConfig({
          themeImporter: {
            themePath: require.resolve('@ckeditor/ckeditor5-theme-lark'),
          },
          minify: true,
        });
      });
    // config.optimization.minimize(false);
  },
};
