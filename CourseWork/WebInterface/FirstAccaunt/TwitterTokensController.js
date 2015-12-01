twittyApp.controller('TwitterTokensController',
    function TwitterTokensController($scope, $http) {
        var params = absUrl.split('?');
        var urlWithTokens = 'http://127.0.0.0:12008/authTwitterAccaunt/?' + params[1];
        var reqWithTokens =
        {
            method: 'Post',
            url: urlWithTokens,
            headers: {
                'Access-Control-Allow-Origin': urlWithTokens,
                'Authorization': 'Token ' + $scope.Token,
                'Email': $scope.email
            }

        }
        $http(reqWithTokens).success(function(url) {

            var i;
        });
    }
)