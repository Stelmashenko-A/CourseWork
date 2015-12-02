twittyApp.controller('TimeLineController',
    function TimeLineController($scope, $http, $window, $cookies) {
        $scope.numbers = [];
        $scope.counter = 0;

        $scope.loadMore = function () {
            $http({
                method: 'POST',
                url: 'http://127.0.0.1:12008/tweets/user-time-line/2765688547',
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded',
                    'UserName': $scope.email,
                    'Password': $scope.password
                }
            }).success(function (resp) {
                var token = resp.Token;
                $cookies.put('token', token);
                $cookies.put('email', $scope.email);
            });
        }
        $scope.loadMore();
    }
)