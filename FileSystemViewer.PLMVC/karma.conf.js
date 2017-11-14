var webpackConfig = require('./webpack.config.js');

module.exports = function (config) {
  config.set({

    basePath : __dirname + '/',

    frameworks: ['jasmine','qunit','sinon'],

    plugins: ['karma-qunit',
            'karma-jasmine',
            'karma-chrome-launcher',
            'karma-babel-preprocessor',
            'karma-sinon',
            'karma-webpack',
            'karma-sourcemap-loader',
            'karma-spec-reporter'
            /*,
            'karma-firefox-launcher'*/
    ],

    files: [
      'src/*.js', 
      'test/*.js',
      'src/Scripts/*.js',
      'test/*Spec.js'
    ],

    exclude: [
    ],

    webpack: webpackConfig, 

   preprocessors: {
     'src/*.js': ['webpack','sourcemap'],
     'test/*.js': ['webpack','sourcemap'],
     'src/Scripts/*.js': ['webpack','sourcemap'],
     'test/*Spec.js': ['webpack','sourcemap']
    },


    reporters: ['spec'],

    // configuring [spec] reporter
    specReporter: {
      maxLogLines: 5,         // limit number of lines logged per test 
      suppressErrorSummary: true,  // do not print error summary 
      suppressFailed: false,  // do not print information about failed tests 
      suppressPassed: true,  // do not print information about passed tests 
      suppressSkipped: true,  // do not print information about skipped tests 
      showSpecTiming: false // print the time elapsed for each spec 
    },

    logLevel: config.LOG_DEBUG,

    colors: true,

   singleRun: false,

    // Concurrency level
    // how many browser should be started simultaneous
    concurrency: Infinity,

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

