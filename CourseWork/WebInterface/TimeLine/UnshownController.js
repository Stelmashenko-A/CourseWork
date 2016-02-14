twittyApp.controller('UnshownController', function UnshownController($scope, $timeout, $q, $http, $cookies) {
    $scope.items = [];
    $scope.busy = false;
    $scope.page = 0;
    $scope.after = Number($cookies.get("lastReadedTweetId")) + 1;
    $scope.status = {
        loading: false,
        loaded: false
    };
    
    $scope.unShown={
        page:0,
        after:-1,
        busy:false,
        items:[]
    }
    var flag=true;
    $scope.unshownFunc = function() {
        
    var id = $cookies.get("userId");
        $http({
            method: 'POST',
            //url: 'http://192.168.0.9:12008/tweets/user-time-line/' + id,
             url: 'http://127.0.0.1:12008/tweets/user-time-line/' + id,
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded',
                'Page': $scope.unShown.page,
                'LineHead': $scope.unShown.after,
                'Authorization': "Token " + $cookies.get("token")
            }
        }).success(function (data) {
            if (data != "") {
                if($scope.unShown.after===-1)
                {
                    
                }
                var items = data.Statuses;
                if($scope.unShown.after===-1)
                {
                    $scope.unShown.after=data.Statuses[0].IdStr;
                }
                for (var i = 0; i < items.length; i++) {
                    if(items[i].IdStr===Number($cookies.get("lastReadedTweetId")))
                    {
                        flag=false;
                        break;
                    }
                    $scope.unShown.items.push(items[i]);
                }

                $scope.unShown.after = $scope.unShown.items[$scope.unShown.items.length - 1].IdStr;
                $scope.unShown.page++;
            }
            $scope.unShown.busy = false;
            $scope.unshownFunc();
        });
    }
    $scope.unshownFunc();
});

twittyApp.directive('whenScrolled', function ($timeout, $window, $document) {
    return function (scope, elm, attr) {
        angular.element($window).bind('scroll', function () {
            if ($window.pageYOffset <= 1000) {
                var sh = elm[0].scrollHeight;
                scope.$apply(attr.whenScrolled).then(function () {
                    var shift = elm[0].scrollHeight - sh;
                    $window.scrollTo(0, shift);
                });
            }
        });


    };
});