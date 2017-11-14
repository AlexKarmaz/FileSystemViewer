const path = require('path');

module.exports = {
	// Defining JavaScript files, which act as entry points to application
	// > usually each is responsible for a separate sub-page
	// > Values listed here are used in [plugin] section, where we link subpages
	//   to coresponding entry points - search for [excludeChunks] & [chunks]
	entry: {
		app: 'src/Scripts/*.js'
	},

	devtool: 'inline-source-map',

	output: {
		// here we need to set an absolute path - we're resolve path at runtime
		path: path.resolve(__dirname, "build/"),
		filename: '[name].bundle.js' // the [name] will be replaced by the name of entry JavaScript File
	},

	module: {
		rules: [ ]
	},

	plugins: [ ]
}