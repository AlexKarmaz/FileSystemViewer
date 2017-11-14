var webpackConfig = require('./webpack.config.js');

module.exports = function (config) {
  config.set({

    asePath : __dirname + '/',

    frameworks: ['qunit','sinon'],

    plugins: ['karma-qunit',
            'karma-chrome-launcher',
            'karma-babel-preprocessor',
            'karma-sinon',
            'karma-webpack',
            'karma-sourcemap-loader'
            /*,
            'karma-firefox-launcher'*/
    ],

    files: [
      'src/*.js', 
      'test/*.js',
      'src/Scripts/*.js'
    ],

    exclude: [
    ],

    webpack: webpackConfig, 

   preprocessors: {
     'src/*.js': ['webpack','sourcemap'],
     'test/*.js': ['webpack','sourcemap'],
     'src/Scripts/*.js': ['webpack','sourcemap']
    },

    logLevel: config.LOG_DEBUG,

   // webpack: {
   //   devtool: 'inline-source-map'
   // },

   // webpackMiddleware: {
   //   stats: 'errors-only'
   // },

  //  babelPreprocessor: {
  //    options: {
  //      presets: ['env'],
   //     sourceMap: 'inline'
   //   },
   //   filename: function (file) {
   //     return file.originalPath.replace(/\.js$/, '.es5.js');
   //   },
   //   sourceFileName: function (file) {
   //     return file.originalPath;
   //   }
   // }, 
    // client configuration 
   browsers: ['Chrome'/*, 'Firefox'*/],

   autoWatch: true
  })
};

