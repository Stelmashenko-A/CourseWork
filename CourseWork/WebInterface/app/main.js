(function () {
    "use strict";

    function main() {
        var vm = this;
        vm.food = "pizza";
    }

    angular
        .module("app")
        .controller("Main", main);
})();