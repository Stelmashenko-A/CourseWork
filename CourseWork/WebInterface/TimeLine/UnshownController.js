twittyApp.controller('UnshownController', function UnshownController($scope, $timeout, $q, $http, $cookies) {
    $scope.items = [];
    $scope.busy = false;
    $scope.page = 0;
    $scope.after = Number($cookies.get("lastReadedTweetId")) + 1;
    $scope.status = {
        loading: false,
        loaded: false
    };


    $scope.loadMore = function () {
        var deferred = $q.defer();
        if (!$scope.status.loading) {
            $scope.status.loading = true;
            // simulate an ajax request

                var id = $cookies.get("userId");
                $http({
                    method: 'POST',
                    //url: 'http://192.168.0.9:12008/tweets/user-time-line/' + id,
                    url: 'http://127.0.0.1:12008/tweets/user-time-line/unshown/' + id,
                    headers: {
                        'Content-Type': 'application/x-www-form-urlencoded',
                        'Page': $scope.page,
                        'LineHead': $scope.after,
                        'Authorization': "Token " + $cookies.get("token")
                    }
                }).success(function (data) {
                    if (data != "") {
                        var items = data.Statuses;

                        for (var i = items.length - 1; i >= 0; i--) {
                            $scope.items.unshift(items[i]);
                        }

                        $scope.page++;
                    }
                    $scope.busy = false;
                });

                $scope.status.loading = false;
                $scope.status.loaded = ($scope.items.length > 0);
                deferred.resolve();
        } else {
            deferred.reject();
        }
        return deferred.promise;
    };
});

twittyApp.directive('whenScrolled', function ($timeout, $window, $document) {
    return function (scope, elm, attr) {
        angular.element($window).bind('scroll', function () {
            if ($window.pageYOffset <= 1000) {
                var sh = elm[0].scrollHeight;
                scope.$apply(attr.whenScrolled).then(function () {
                    var shift = elm[0].scrollHeight - sh;
                    $window.scrollTo(0, shift);
                });
            }
        });


    };
});