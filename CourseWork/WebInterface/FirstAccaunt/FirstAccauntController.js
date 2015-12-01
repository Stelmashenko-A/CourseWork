twittyApp.controller('FirstAccauntController',
    function FirstAccauntController($scope, $http,$window) {
        $scope.addFirstAccaunt = function() {
            var req = {
                method: 'Post',
                url: 'http://127.0.0.1:12008/twitter/authentification/authorizationUri',
                headers: {
                    'Access-Control-Allow-Origin': "http://127.0.0.1:12008/twitter/authentification/authorizationUri",
                    'Authorization': 'Token ' + $scope.Token
                }
            }

            $http(req).success(function(url) {

                $window.location.href = url;
            });
        }
    }
)