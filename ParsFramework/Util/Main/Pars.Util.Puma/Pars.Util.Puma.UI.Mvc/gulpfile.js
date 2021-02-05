var gulp = require('gulp');
var del = require('del');
var typescript = require('gulp-typescript');
var tscConfig = require('./tsconfig.json');
var sourcemaps = require('gulp-sourcemaps');
var newer = require('gulp-newer');
var imagemin = require('gulp-imagemin');
var htmlclean = require('gulp-htmlclean');
var concat = require('gulp-concat');
var deporder = require('gulp-deporder');
var stripdebug = require('gulp-strip-debug');
var uglify = require('gulp-uglify');
var Builder = require('systemjs-builder');
var inlineNg2Template = require('gulp-inline-ng2-template');
var minifyJS = require('gulp-minify');
var babel = require('gulp-babel');
var rename = require('gulp-rename');

gulp.task('bundle', ['bundle-app', 'uglify-bundle'], function () { });

gulp.task('cleanfiles', ['cleanbundle'], function () {
    return del('dist/**/*');
});
gulp.task('cleanbundle', function () {
    return del('bundles/**/*');
});

gulp.task('inline-templates', ['cleanfiles'], function () {
    return gulp.src(['app/**/*.ts', '!app/**/*envProd.ts', , '!app/**/*envTest.ts'])
      .pipe(inlineNg2Template({ useRelativePaths: true, indent: 0, removeLineBreaks: true, base: '/app' }))
      .pipe(typescript({
          "target": "es5",
          "module": "commonjs",
          "moduleResolution": "node",
          "sourceMap": true,
          "emitDecoratorMetadata": true,
          "experimentalDecorators": true,
          "removeComments": false,
          "noImplicitAny": false
      }))
        .pipe(stripdebug())
        .pipe(uglify())
        .pipe(htmlclean())
      .pipe(gulp.dest('dist/app'));
});

gulp.task('environmentfiles', ['inline-templates'],
    function () {
        var env = process.argv[3].replace("--", "");
        var src = 'app/**/webapiendpoints.' + env + (env === '' ? 'ts' : '.ts');
        return gulp.src(src)
            .pipe(typescript(tscConfig.compilerOptions))
            .pipe(rename('webapiendpoints.js'))
            .pipe(gulp.dest('dist/app/settings'));
        // console.log(env);
    }
);

gulp.task('bundle-app', ['environmentfiles'], function () {
    // optional constructor options
    // sets the baseURL and loads the configuration file
    var builder = new Builder('', 'dist-systemjs.config.js');

    return builder
      //.bundle('dist/app/**/* - [@angular/**/*.js] - [rxjs/**/*.js]', 'bundles/app.bundle.js', { minify: true })
      //  .bundle('dist/app/**/*', 'bundles/app.bundle.js', { minify: true })
        .buildStatic('dist/app/**/*', 'bundles/app.bundle.js')
      .then(function () {
          console.log('Build complete');
      })
      .catch(function (err) {
          console.log('Build error');
          console.log(err);
      });
});

gulp.task('uglify-bundle', ['bundle-app'],
    function () {
        return gulp.src('bundles/app.bundle.js')
            //.pipe(minifyJS().on('error', function (e) {
            //    console.log(e);
            //}))
            .pipe(babel({
                compact: true, comments: false
            }))
            .pipe(htmlclean())
            .pipe(rename('app.bundle.min.js'))
            //.pipe(minifyJS())
            .pipe(uglify())
            .pipe(gulp.dest('bundles/'));
    });

gulp.task('bundletest',
    function () {
        return gulp.src('bundles/app.bundle.js')
           .pipe(rename('app.bundle.min.js'))
           .pipe(uglify().on('error', function (e) {
               console.log(e);
           }))
           .pipe(gulp.dest('bundles/'));
    });

gulp.task('bundle-dependencies', function () {
    // optional constructor options
    // sets the baseURL and loads the configuration file
    var builder = new Builder('', 'dist-systemjs.config.js');

    return builder
      .bundle('dist/app/**/*.js - [dist/app/**/*.js]', 'bundles/dependencies.bundle.js', { minify: true })
      .then(function () {
          console.log('Build complete');
      })
      .catch(function (err) {
          console.log('Build error');
          console.log(err);
      });
});

///////////

// clean the contents of the distribution directory


//TypeScript compile
gulp.task('compile', ['clean'], function () {
    return gulp
      .src("app/**/*.ts")
      .pipe(sourcemaps.init())          // <--- sourcemaps
      .pipe(typescript(tscConfig.compilerOptions))
      .pipe(sourcemaps.write('.'))      // <--- sourcemaps
      .pipe(gulp.dest('app1'));
});

// HTML processing
gulp.task('html', function () {
    return gulp.src('app/**/*.html')
            .pipe(newer('app1'))
            .pipe(htmlclean())
            .pipe(gulp.dest('app1'));
});

gulp.task('jsmap', function () {
    return gulp.src('app/**/*.map')
            .pipe(newer('app1'))
            .pipe(htmlclean())
            .pipe(gulp.dest('app1'));
});

// JavaScript processing
gulp.task('js', function () {

    var jsbuild = gulp.src('app/**/*.js')
      .pipe(deporder())
      .pipe(concat('main.js'));

    jsbuild = jsbuild
          .pipe(stripdebug())
          .pipe(uglify());


    return jsbuild.pipe(gulp.dest('app1/'));

});



gulp.task('build', ['clean', 'html', 'jsmap', 'js']);


//copy static assets - i.e. non TypeScript compiled source
gulp.task('copy:assets', ['clean'], function () {
    return gulp.src(['app/**/*', 'index.html', 'styles.css', '!app/**/*.ts'], { base: './' })
        .pipe(gulp.dest('dist'));
});

gulp.task("resources", ['clean'], function () {
    return gulp.src("app/**/*.html")
         .pipe(sourcemaps.init())          // <--- sourcemaps
      .pipe(typescript(tscConfig.compilerOptions))
      .pipe(sourcemaps.write('.'))      // <--- sourcemaps
      .pipe(gulp.dest('dist'));
});

