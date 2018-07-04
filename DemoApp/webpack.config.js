//var webpack = require("webpack");

const CleanWebpackPlugin = require('clean-webpack-plugin');
const CopyWebpackPlugin = require('copy-webpack-plugin');
//import CleanWebpackPlugin from "clean-webpack-plugin";
//import CopyWebpackPlugin  from "copy-webpack-plugin"

var core = [
    "core-js/fn/array/find",
    "core-js/fn/array/find-index",
    "core-js/fn/map",
    "core-js/fn/promise",
    "core-js/fn/set"
]
module.exports = {
    entry: {
        "app": core.concat("./src/index.tsx"),
    },
    output: {
        filename: "[name].js",
        path: __dirname + "/dist"
    },

    // Enable sourcemaps for debugging webpack's output.
    devtool: "source-map",

    resolve: {
        // Add '.ts' and '.tsx' as resolvable extensions.
        extensions: [".ts", ".tsx", ".js", ".json"]
    },

    module: {
        rules: [
            // All files with a '.ts' or '.tsx' extension will be handled by 'ts-loader'.
            { test: /\.tsx?$/, loaders: ["babel-loader", "ts-loader"] },

            // All output '.js' files will have any sourcemaps re-processed by 'source-map-loader'.
            { enforce: "pre", test: /\.js$/, loader: "source-map-loader" },

            // use  scss-loader -> css-loader -> style-loader for scss files...
            { 
                test: /\.s[ac]ss$/, loaders: [{
                    loader:"style-loader"
                },{
                    loader: "css-loader", options: {
                        sourceMap: true
                    }
                }, {
                    loader: "sass-loader", options: {
                        sourceMap: true
                    }
                }], 
                exclude:'/node_modules' },
        ]
    },

    // When importing a module whose path matches one of the following, just
    // assume a corresponding global variable exists and use that instead.
    // This is important because it allows us to avoid bundling all of our
    // dependencies, which allows browsers to cache those libraries between builds.
    externals: { /* no externals - include ALL! */ },

    plugins: [
        /*new webpack.DefinePlugin({
          'process.env': {
            NODE_ENV: JSON.stringify('"production"')
          }
        }),*/
        new CleanWebpackPlugin([ 'dist' ], {verbose: true}),
        new CopyWebpackPlugin([
            { from: 'index.html' }
          ])
        //new webpack.optimize.UglifyJsPlugin(),
        //new webpack.optimize.OccurenceOrderPlugin(),
        //new webpack.optimize.DedupePlugin()
      ]
};