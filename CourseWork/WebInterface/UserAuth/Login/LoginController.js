twittyApp.controller('LoginController',
    function LoginController($scope, $http, $cookies, $location) {
        $scope.password = '';
        $scope.email = '';
        $scope.login = function () {
            $http({
                method: 'POST',
                url: 'http://127.0.0.1:12008/auth',
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded',
                    'UserName': $scope.email,
                    'Password': $scope.password
                }
            }).success(function (resp) {
                var token = resp.Token;
                $cookies.put('token', token);
                $cookies.put('email', $scope.email);
                $cookies.put('userId', resp.UserId);

                $location.path("/time-line");
            });
        }
    }
    )