import * as webpack from "webpack";
import * as WebpackDevServer from "webpack-dev-server";
import * as config from "../webpack.config";
let opn = require("opn")

//const output = config.output as webpack.Output;
let serverConfig:webpack.Configuration = {...config,
  mode: "development",
  output:{
    ...config.output,
    pathinfo:true
}};

const compiler = webpack(serverConfig);

const SERVER_PORT = process.env.SERVER_PORT ? parseInt(process.env.SERVER_PORT as string, 10) : 8080;
const SERVER_HOSTNAME = process.env.SERVER_HOSTNAME || "localhost";

const server = new WebpackDevServer(compiler, {
  //publicPath: "/dist/",
  contentBase: "./dist/",
  hot: true,
  historyApiFallback: true,
  //progress: true,
  stats: {
    colors: true,
    hash: false,
    timings: true,
    chunks: true,
    chunkModules: true,
    modules: true,
  },
});

server.listen(SERVER_PORT, SERVER_HOSTNAME, (err: Error) => {
  if (err) {
    console.log(err);
    return;
  } 

  const url = `http://${SERVER_HOSTNAME}:${SERVER_PORT}`;
  console.log(`Server listening at ${url}`);
  opn(url+"/index.html");
});
