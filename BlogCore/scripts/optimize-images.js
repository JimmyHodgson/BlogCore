const sharp = require('sharp');
const glob = require('glob');
const path = require('path');
const fs = require('fs');

// File paths
const srcDir = 'assets/src/images/**/*.{jpg,jpeg,png,ico,svg,webmanifest}';
const destDir = 'assets/dist/images';

// Ensure destination directory exists
if (!fs.existsSync(destDir)) {
  fs.mkdirSync(destDir, { recursive: true });
}

// Exclude patterns
const excludePatterns = ['fonts/*', 'sprite/*'];

// Function to process images
function processImages(pattern, outputOptions, extension) {
  glob(pattern, { ignore: excludePatterns.map((dir) => path.join('assets/src/images', dir)) }, (err, files) => {
    if (err) throw err;

    files.forEach((file) => {
      // Change the output file extension if necessary
      const outputFile = path.join(
        destDir,
        `${path.basename(file, path.extname(file))}.${extension || path.extname(file).slice(1)}`
      );

      sharp(file)
        .toFormat(outputOptions.format, outputOptions.options)
        .toFile(outputFile)
        .then(() => console.log(`Processed: ${file} -> ${outputFile}`))
        .catch((err) => console.error(`Error processing ${file}:`, err));
    });
  });
}

// Function to copy files without processing
function copyFiles(pattern) {
  glob(pattern, { ignore: excludePatterns.map((dir) => path.join('assets/src/images', dir)) }, (err, files) => {
    if (err) throw err;

    files.forEach((file) => {
      const outputFile = path.join(destDir, path.basename(file));
      fs.copyFile(file, outputFile, (err) => {
        if (err) console.error(`Error copying ${file}:`, err);
        else console.log(`Copied: ${file} -> ${outputFile}`);
      });
    });
  });
}

// JPEG Optimization
processImages(
  'assets/src/images/**/*.{jpg,jpeg}',
  { format: 'jpeg', options: { quality: 80, progressive: true } },
  'jpg'
);

// PNG Optimization
processImages(
  'assets/src/images/**/*.png',
  { format: 'png', options: { compressionLevel: 8 } },
  'png'
);

// WebP Conversion
processImages(
  'assets/src/images/**/*.{jpg,jpeg,png}',
  { format: 'webp', options: { quality: 80 } },
  'webp'
);

// Copy .ico, .svg, and .webmanifest files
copyFiles('assets/src/images/**/*.{ico,svg,webmanifest}');