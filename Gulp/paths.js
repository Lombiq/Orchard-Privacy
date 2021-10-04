const assetsBasePath = './Assets/';
const stylesBasePath = assetsBasePath + 'Styles/';
const distBasePath = './wwwroot/';

module.exports = {
    styles: {
        base: stylesBasePath,
        all: stylesBasePath + '**/*.scss',
    },
    dist: {
        base: distBasePath,
        css: distBasePath + 'css/',
    },
};
