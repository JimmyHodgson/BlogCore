const fs = require('fs');
const path = require('path');
const glob = require('glob');

// Paths (mimicking the Grunt src and dist paths)
const srcPath = 'assets/src'; // Replace with your actual src path
const distPath = 'assets/dist'; // Replace with your actual dist path

// Ensure destination directories exist
function ensureDirExists(filePath) {
  const dir = path.dirname(filePath);
  if (!fs.existsSync(dir)) {
    fs.mkdirSync(dir, { recursive: true });
  }
}

// Concatenate and trim files, with an optional "priorityFile" to place at the top
function concatFiles(patterns, excludePatterns, destFile, priorityFile = null) {
    // Get all files matching the pattern and excluding specific files/folders
    let files = glob.sync(patterns, { ignore: excludePatterns });

    // Move the priority file to the top if it's provided
    let priorityContent = '';
    if (priorityFile) {
        // Ensure the priority file is included and read its content
        const priorityFilePath = files.find(file => file.endsWith(priorityFile));
        if (priorityFilePath) {
            priorityContent = fs.readFileSync(priorityFilePath, 'utf8').trim(); // Read and trim the priority file
            files = files.filter(file => file !== priorityFilePath); // Remove the priority file from the rest
        }
    }

    // Concatenate contents of all remaining matched files, trimming whitespace for each file
    let combinedContent = files.map((file) => {
        const content = fs.readFileSync(file, 'utf8');
        return content.trim();  // Trim whitespace from each file
    }).join('\n');  // Add a newline between concatenated files

    // Combine the priority content and the rest of the files
    const finalContent = priorityFile ? `${priorityContent}\n${combinedContent}` : combinedContent;

    // Ensure the destination directory exists
    ensureDirExists(destFile);

    // Write the combined content to the destination file
    fs.writeFileSync(destFile, finalContent, 'utf8');
    console.log(`Concatenated and trimmed files into ${destFile}`);
}

// Main Task: Concatenate JavaScript files, excluding common.js and components
concatFiles(
  `${srcPath}/js/**/*.js`, // Pattern to match all JS files
  [`${srcPath}/js/common.js`, `${srcPath}/js/components/**/*`], // Exclude these files
  `${distPath}/js/project.combined.js` // Destination file
);

// Components Task: Concatenate all JS files in the components directory
concatFiles(
  `${srcPath}/js/components/**/*.js`, // Pattern for JS files in components folder
  [], // No exclusion here
    `${distPath}/js/components.combined.js`, // Destination file
  'main.component.js'
);
