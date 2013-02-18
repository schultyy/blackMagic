exports.readFileSync = function(filename) {
    return nativeModule.readFileSync(path);
};
exports.writeFileSync = function(filename, content) {
    nativeModule.writeFileSync(path, content);
};
exports.exists = function(path) {
    return nativeModule.fileExists(path) || nativeModule.directoryExists(path);
};