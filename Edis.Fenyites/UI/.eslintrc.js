module.exports = {
  root: true,
  env: { node: true },
  extends: [
    'plugin:vue/essential',
    //'@vue/prettier'
  ],
  rules: {
    'no-console': 'off',
    'no-debugger': 'warn',
    semi: [2, 'always'],
    'comma-dangle': 0,
    'space-before-function-paren': ['warn', 'never'],
    quotes: ['warn', 'single'],
    'linebreak-style': 0,
    'no-unused-vars': 0,
    'space-before-function-paren': 0,
    endOfLine: 'auto',
    'eol-last': 0,
    indent: 0,
    'no-undef': 'error',
  },
  parserOptions: { parser: 'babel-eslint' },
};
