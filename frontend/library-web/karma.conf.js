// Karma configuration
// Generated on Wed Jul 16 2025 18:07:52 GMT-0300 (Horário Padrão de Brasília)

module.exports = function(config) {
  config.set({

    // base path that will be used to resolve all patterns (eg. files, exclude)
    basePath: '',


    // frameworks to use
    // available frameworks: https://www.npmjs.com/search?q=keywords:karma-adapter
    frameworks: ['jasmine'],


    // list of files / patterns to load in the browser
    files: [
      'src/app/core/interceptors/*.spec.ts',
      'src/app/core/services/author/*.spec.ts',
      'src/app/core/services/book/*.spec.ts',
      'src/app/core/services/genre/*.spec.ts',
      'src/app/core/state/author/*.spec.ts',
      'src/app/core/state/book/*.spec.ts',
      'src/app/core/state/genre/*.spec.ts',
      'src/app/features/authors/*.spec.ts',
      'src/app/features/books/*.spec.ts',
      'src/app/features/genres/genre-list/*.spec.ts',
      'src/app/shared/components/navbar/*.spec.ts',
      'src/app/*.spec.ts',
    ],


    // list of files / patterns to exclude
    exclude: [
    ],


    // preprocess matching files before serving them to the browser
    // available preprocessors: https://www.npmjs.com/search?q=keywords:karma-preprocessor
    preprocessors: {
    },


    // test results reporter to use
    // possible values: 'dots', 'progress'
    // available reporters: https://www.npmjs.com/search?q=keywords:karma-reporter
    reporters: ['progress'],


    // web server port
    port: 9876,


    // enable / disable colors in the output (reporters and logs)
    colors: true,


    // level of logging
    // possible values: config.LOG_DISABLE || config.LOG_ERROR || config.LOG_WARN || config.LOG_INFO || config.LOG_DEBUG
    logLevel: config.LOG_INFO,


    // enable / disable watching file and executing tests whenever any file changes
    autoWatch: true,


    // start these browsers
    // available browser launchers: https://www.npmjs.com/search?q=keywords:karma-launcher
    browsers: ['Opera', 'Firefox'],


    // Continuous Integration mode
    // if true, Karma captures browsers, runs the tests and exits
    singleRun: false,

    // Concurrency level
    // how many browser instances should be started simultaneously
    concurrency: Infinity
  })
}
