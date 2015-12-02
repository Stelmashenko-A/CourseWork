var twittyApp = angular.module('twittyApp', ['ngRoute', 'ngCookies'])
    .config(function($routeProvider) {
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
        $routeProvider.when('/firstAccaunt',
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
        
    });