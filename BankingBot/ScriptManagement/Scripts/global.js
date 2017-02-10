var global = (function (module) {

    function testFunction() {
        console.log("test!");
    }

    return {
        testFunction: testFunction
    };

})(global || {});