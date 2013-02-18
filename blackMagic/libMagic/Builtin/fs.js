exports.readFileSync = function(filename) {
    return nativeModule.readFileSync(filename);
};
exports.writeFileSync = function(filename, content) {
    nativeModule.writeFileSync(path, content);
};
exports.exists = function(path) {
    return nativeModule.fileExists(path) || nativeModule.directoryExists(path);
};

exports.combine = function() {
    var i = 0, args = arguments, argLength = args.length;

    var paths = new Array();

    while (i < argLength) {
        paths.push(args[i++]);
    }

    return nativeModule.combine(paths);
};

exports.createDirectory = function (directoryName) {
    nativeModule.createDirectory(directoryName);
};