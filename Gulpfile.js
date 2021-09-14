const gulp = require('gulp');
const watch = require('gulp-watch');
const paths = require('./Gulp/paths');
const scssTargets = require('../../Utilities/Lombiq.Gulp.Extensions/Tasks/scss-targets');

gulp.task('build:styles', scssTargets.build(paths.styles.base, paths.dist.css));

gulp.task('clean', gulp.series(scssTargets.clean(paths.dist.css)));

gulp.task('watch:styles', () => watch(paths.styles.all, { verbose: true }, gulp.series('build:styles')));


gulp.task('watch', () => {
    watch(paths.styles.all, { verbose: true }, gulp.series('build:styles'));
});

gulp.task('default', gulp.parallel('build:styles'));
