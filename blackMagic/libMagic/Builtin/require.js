function require(moduleName) {

    var path = "./lib/" + moduleName + ".js";

    var fileContent = nativeModule.readFileSync(path);

    var module = eval(fileContent);
    return module.exports;
}