﻿twittyApp.controller('RegistrationController',
    function RegistrationController($scope, $http, $cookies, $location) {

        var cookies = $cookies.getAll();
        var email = cookies.email;
        var Token = cookies.token;
        var lastReadedTweetId = cookies.lastReadedTweetId;
        if (!angular.isUndefined(email) || !angular.isUndefined(Token) || !angular.isUndefined(lastReadedTweetId)) {
            $location.path("/time-line");
        }

        $scope.password = '';
        $scope.email = '';
        $scope.regitration = function() {
            $http({
                method: 'POST',
                url: 'http://192.168.0.9:12008/registration',  // url: 'http://127.0.0.1:12008/registration',
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded',
                    'UserName': $scope.email,
                    'Password': $scope.password
                }
            }).success(function(resp) {
                var token = resp.Token;
                $cookies.put('token', token);
                $cookies.put('email', $scope.email);
                $location.path("first-accaunt");
            });
        }
    }
)