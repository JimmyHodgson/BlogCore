'use strict'
const path = require('path');

module.exports = function(grunt){
    require('load-grunt-tasks')(grunt);
    require('time-grunt')(grunt);

    grunt.initConfig({
        timestamp: '<%= new Date().getTime() %>',
        pkg: grunt.file.readJSON('package.json'),
        src: {
            path: 'assets/src',
            sass: '**/*.{scss,sass}'
        },
        dist: {
            path: 'assets/dist'
        },
        babel: {
            options: {
                sourceMap:true,
                presets: ['@babel/preset-env']
            },
            dist: {
                files: {
                    '<%= dist.path %>/js/project.js': '<%= dist.path %>/js/project.combined.js',
                    '<%= dist.path %>/js/common.js': '<%= src.path %>/js/common.js',
                    '<%= dist.path %>/js/components.js': '<%= dist.path %>/js/components.combined.js'
                }
            }
        },
        clean: {
            all: {
                files: [{
                    src: [
                        '<%= dist.path %>/**/*.css',
                        '<%= dist.path %>/**/*.js',
                        '<%= dist.path %>/**/*.map',
                        '<%= dist.path %>/**/*.flow',
                        '<%= dist.path %>/**/*.{png,jpg,gif,jpeg}',
                        'csslint_report'
                    ]
                }]
            },
            css: {
                files: [{
                    src: [
                        '<%= dist.path %>/**/*.css',
                    ]
                }]
            },
            images: {
                files: [{
                    src: [
                        '<%= dist.path %>/**/*.{png,jpg,gif,jpeg}'
                    ]
                }]
            }
        },
        cssmin: {
            minify: {
                expand: true,
                cwd: '<%= dist.path %>/css/',
                src: ['*.css', '!*.min.css'],
                dest: '<%= dist.path %>/css/',
                ext: '.min.css',
                extDot: 'last'
            }
        },
        concat: {
            main: {
                src: ['<%= src.path %>/js/**/*.js','!<%= src.path %>/js/common.js','!<%= src.path %>/js/components/**/*'],
                dest: '<%= dist.path %>/js/project.combined.js'
            },
            components: {
                src: ['<%= src.path %>/js/components/**/*.js'],
                dest: '<%= dist.path %>/js/components.combined.js'
            }
        },
        concurrent: {
            dist: {
                tasks: ['watch:styles', 'watch:js', 'watch:images'],
                options: {
                    logConcurrentOutput: true
                }
            }
        },
        copy: {
            dist: {
                files: [
                    { expand: true, src: ['node_modules/bootstrap/dist/js/*'], dest: 'assets/dist/lib/', flatten: true, filter: 'isFile' },
                    { expand: true, src: ['node_modules/jquery/dist/*'], dest: 'assets/dist/lib/', flatten: true, filter: 'isFile' },
                    { expand: true, src: ['node_modules/popper.js/dist/umd/*'], dest: 'assets/dist/lib/', flatten: true, filter: 'isFile' },
                    { expand: true, src: ['node_modules/vue/dist/vue.min.js'], dest: 'assets/dist/lib/', flatten: true, filter: 'isFile' },
                    { expand: true, src: ['node_modules/vue/dist/vue.js'], dest: 'assets/dist/lib/', flatten: true, filter: 'isFile' },
                    { expand: true, src: ['node_modules/apexcharts/dist/apexcharts.min.js'], dest: 'assets/dist/lib', flatten: true, filter: 'isFile' },
                    { expand: true, src: ['node_modules/vue-apexcharts/dist/vue-apexcharts.js'], dest: 'assets/dist/lib', flatten: true, filter: 'isFile' },
                    { expand: true, cwd: 'node_modules/@fortawesome/fontawesome-pro/webfonts', src: '**', dest: 'assets/dist/fonts/fontawesome' },
                    { expand: true, cwd: 'assets/src/fonts', src: '**', dest: 'assets/dist/fonts/' }
                ]
            }
        },
        imagemin: {
            dist: {
                options: {
                    optimizationLevel: 4,
                    progressive: true
                },
                files: [
                    { expand: true, cwd: 'assets/src/images', src: ['**/*.{png,jpg,gif,jpeg,svg}', '!fonts/*', '!sprite/*.*'], dest: 'assets/dist/images' }
                ]
            }
        },
        postcss:{
            options:{
                map:true,
                processors:[
                    require('pixrem')(),
                    require('autoprefixer')()
                ]
            },
            dist:{
                src:'<%= dist.path %>/css/*.css'
            }
        },
        sass: {
            options: {
                implementation: require('node-sass'),
                outputStyle: 'nested'
            },
            dist: {
                files: [{
                    expand: true,
                    cwd: '<%= src.path %>/sass',
                    src: ['*.scss'],
                    dest: '<%= dist.path %>/css/',
                    ext: '.css'
                }]
            }
        },
        uglify: {
            options: {
                report: 'gzip',
                warnings: true,
                mangle: true,
                compress: true
            },
            dist: {
                files: {
                    '<%= dist.path %>/js/project.min.js' : '<%= dist.path %>/js/project.js',
                    '<%= dist.path %>/js/common.min.js': '<%= dist.path %>/js/common.js',
                    '<%= dist.path %>/js/components.min.js': '<%= dist.path %>/js/components.js'
                }
            }
        },
        watch: {
            options: {
                spawn: false
            },
            styles: {
                files: ['<%= src.path %>/**/*.{scss,sass}'],
                tasks: ['sass','postcss', 'cssmin']
            },
            images: {
                files: ['<%= src.path %>/**/*.{png,jpg,gif,jpeg}'],
                tasks: ['clean:images', 'imagemin']
            },
            js: {
                files: ['<%= src.path %>/**/*.js'],
                tasks: ['concat', 'babel', 'uglify']
            }
        }
    });

    grunt.registerTask('build','',function(){
        grunt.task.run('clean:all'); // Deletes everything from the dest folder
        grunt.task.run('copy'); // Copies select files to the dest folder
        grunt.task.run('sass'); // Compiles the sass files to css files
        grunt.task.run('concat'); // Combines our javascript files
        grunt.task.run('babel'); // transpiles ES6 javascript
        grunt.task.run('uglify'); // Makes it so our javascript isn't easily readable
        grunt.task.run('postcss'); // adds browser prefixes to our css
        grunt.task.run('cssmin'); // minimizes our css
        grunt.task.run('newer:imagemin');//optimizes our images and moves them to the dest folder
    });

    grunt.registerTask('default','',function(){
        grunt.task.run('build');
        grunt.task.run('concurrent'); // watches for changes
    });

}