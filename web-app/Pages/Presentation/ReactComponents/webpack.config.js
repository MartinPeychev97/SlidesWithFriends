const path = require("path");

module.exports = {
  mode: "development",
  entry: {
    bundle: path.resolve(__dirname, "src/index.jsx"),
  },
  output: {
    path: __dirname + "/../../../wwwroot/edit-presentation",
    filename: "[name].js",
  },
  watch: true,
  module: {
    rules: [
      {
        test: /\.js|jsx$/,
        exclude: /(node_modules)/,
        use: {
          loader: "babel-loader",
          options: {
            presets: ["@babel/preset-env", "@babel/preset-react"],
          },
        },
      },
      {
        test: /\.css$/,
        use: [
          "style-loader",
          {
            loader: "css-loader",
            options: {
              importLoaders: 1,
              modules: true,
            },
          },
        ],
      },
    ],
  },
  resolve: {
    extensions: [".js", ".jsx"],
  },
};
