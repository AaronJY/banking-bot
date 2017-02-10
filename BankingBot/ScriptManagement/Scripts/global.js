var global = (function (module) {

    module.testFunction = function () {
        console.log("Test function works!");
    };

    return module;

})(global || {});