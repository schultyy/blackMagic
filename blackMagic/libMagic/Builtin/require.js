function require(moduleName) {

    var path = "./lib/" + moduleName + ".js";

    var fileContent = nativeModule.readFileSync(path);

    var exports = {};

    var module = eval(fileContent);

    return exports;
}