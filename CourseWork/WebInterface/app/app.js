﻿var twittyApp = angular.module('twittyApp', ["ngRoute"])
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
    });