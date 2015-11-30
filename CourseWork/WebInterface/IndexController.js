twittyApp.controller('IndexController',
    function IndexController($scope, $http, $cookies, $location) {
        $scope.init = function() {
            var cookies = $cookies.getAll();

                var email = cookies.email;
                var Token = cookies.token;
                if (email.length===0 || Token.length===0)
                {
                $location.path("/login");
                }

        }
    }
)