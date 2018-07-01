import * as webpack from "webpack";
import * as WebpackDevServer from "webpack-dev-server";

//declare interface TypedWpCfg extends webpack.Configuration { }
declare var typedWpCfg:webpack.Configuration;

export = typedWpCfg;
