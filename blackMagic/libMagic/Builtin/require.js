function require(moduleName) {

    var path = "./Builtin/" + moduleName + ".js";

    var fileContent = nativeModule.readFileSync(path);

    var exports = {};

    var module = eval(fileContent);

    return exports;
}