var twittyApp = angular.module('twittyApp', ['ngRoute', 'ngCookies', 'infinite-scroll'])
    .config(function ($routeProvider) {
        $routeProvider.when('/registration',
            {
                templateUrl: 'UserAuth/Registration/registration.html',
                controller: 'RegistrationController'
            });
        $routeProvider.when('/login',
            {
                templateUrl: 'UserAuth/Login/login.html',
                controller: 'LoginController'
            });
        $routeProvider.when('/first-accaunt',
            {
                templateUrl: 'FirstAccaunt/firstAccaunt.html',
                controller: 'FirstAccauntController'
            });
        $routeProvider.when('/twitter-tokens',
            {
                templateUrl: 'FirstAccaunt/twitterTokens.html',
                controller: 'TwitterTokensController'
            });
        $routeProvider.when('/time-line',
            {
                templateUrl: 'TimeLine/timeLine.html',
                controller: 'TimeLineController'
            });
         $routeProvider.when('/user',
            {
                templateUrl: 'User/user.html',
                controller: 'UserController'
            });   
           
    });