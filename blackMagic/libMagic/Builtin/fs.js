exports.fs = {
    readFileSync: function (filename) {
        return nativeModule.readFileSync(path);
    },
    writeFileSync: function (filename, content) {
        nativeModule.writeFileSync(path, content);
    },
    exists: function (path) {
        return nativeModule.fileExists(path) || nativeModule.directoryExists(path);
    }
}