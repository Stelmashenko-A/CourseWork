twittyApp.controller('TwitterTokensController',
    function TwitterTokensController($scope, $http, $location) {
        var params = absUrl.split('?');
        var urlWithTokens = 'http://127.0.0.0:12008/authTwitterAccaunt/?' + params[1];
        var reqWithTokens =
        {
            method: 'Post',
            url: urlWithTokens,
            headers: {
                'Access-Control-Allow-Origin': urlWithTokens,
                'Authorization': 'Token ' + $scope.Token
            }

        }
        $http(reqWithTokens).success(function(resp) {
            $location.path("/time-line");

        });
    }
)