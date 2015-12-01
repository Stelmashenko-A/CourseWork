twittyApp.controller('IndexController',
    function IndexController($scope, $http, $cookies, $location) {
        var absUrl = $location.absUrl();
        if (absUrl.contains('/?'))
            {
           var o = $cookies.getAll();
                var email = o.email;
                var token = o.token;
            var params = absUrl.split('?');
            var urlWithTokens = 'http://127.0.0.1:12008/authTwitterAccaunt/?' + params[1];
            var reqWithTokens =
            {
                method: 'Post',
                url: urlWithTokens,
                headers: {
                    'Access-Control-Allow-Origin': urlWithTokens,
                    'Content-Type': 'application/x-www-form-urlencoded',
                    'Authorization': 'Token ' + token,
                    'Email': email
                }
                
            }
            $http(reqWithTokens).success(function (resp) {

                $cookies["email"] = resp.Email;
                $cookies["token"] = resp.Token;
            });
      
            }
        $scope.init = function() {
            var cookies = $cookies.getAll();

            var email = cookies.email;
            var Token = cookies.token;
            if (angular.isUndefined(email) || angular.isUndefined(Token)) {
                $location.path("/login");
            } else {
                $scope.email = email;
                $scope.Token = Token;

            }

        }
    }
)