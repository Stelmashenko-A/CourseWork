twittyApp.controller('UnshownController', function UnshownController($scope, $timeout, $q) {
    $scope.items = [];
    $scope.status = {
        loading: false,
        loaded: false
    };

    var counter = 0;
    $scope.loadMore = function () {
        var deferred = $q.defer();
        if (!$scope.status.loading) {
            $scope.status.loading = true;
            // simulate an ajax request
            $timeout(function () {
                for (var i = 0; i < 50; i++) {
                    $scope.items.unshift({
                        id: counter
                    });
                    counter += 10;
                }
                $scope.status.loading = false;
                $scope.status.loaded = ($scope.items.length > 0);
                deferred.resolve();
            }, 1000);
        } else {
            deferred.reject();
        }
        return deferred.promise;
    };

    $scope.loadMore();
});

twittyApp.directive('whenScrolled', function ($timeout, $window, $document) {
    return function (scope, elm, attr) {
        
        /* .bind("scroll", function() {
             if (this.pageYOffset >= 100) {
                 scope.boolChangeClass = true;
             } else {
                 scope.boolChangeClass = false;
             }
            scope.$apply();
        });
        
        var raw = elm[0];*/

        angular.element($window).bind('scroll', function () {
            if ($window.pageYOffset <= 100) {
                var sh = elm[0].scrollHeight;
                scope.$apply(attr.whenScrolled).then(function () {
                    $timeout(function () {
                        var t = elm[0].scrollHeight - sh;
                        $window.scrollTo(0, t);
                    })
                });
                ;
            }
        });
    };
}).directive('scrollBottomOn', ['$timeout', function ($timeout) {
    return function (scope, elm, attr) {
        scope.$watch(attr.scrollBottomOn, function (value) {
            if (value) {
                $timeout(function () {
                    elm[0].scrollTop = elm[0].scrollHeight;
                });
            }
        });
    }
}]);