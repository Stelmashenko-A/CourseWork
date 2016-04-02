
twittyApp.controller('TimeLineController', function($interval,$scope, $http, $cookies, Shown, TimelineSynchronizationBlock, TweetViewBuilder) {
    $http(
        {
            method: 'POST',
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded',
                'Authorization': "Token " + $cookies.get("token")
            },
            //url: 'http://192.168.0.9:12008/user/' + $cookies.get("userId")
            url: 'http://127.0.0.1:12008/user/' + $cookies.get("userId")
        }).success(function(user) {
            $scope.user = user;
            $cookies.put('lastReadedTweetId', $scope.user.LastReadedTweetId);
            $scope.Shown = new Shown();
            //$scope.Unshown = new Unshown();
            $scope.TimelineSynchronizationBlock = new TimelineSynchronizationBlock();

            function updateLastReadedTweetId() {
                var elemId = $scope.TimelineSynchronizationBlock.maxShownId - 1;
                var tweet = document.getElementById(elemId);
                var tweetId = tweet.dataset.item;
                var id = $cookies.get("userId");
                $http({
                    method: 'POST',
                    //url: 'http://192.168.0.9:12008/tweets/user-time-line/' + id,
                    url: 'http://127.0.0.1:12008/tweets/user-time-line/last-shown/' + id,
                    headers: {
                        'Content-Type': 'application/x-www-form-urlencoded',
                        'LastShown': tweetId,
                        'Authorization': "Token " + $cookies.get("token")
                    }
                }).success(function(data) {
                    $scope.TimelineSynchronizationBlock.sendedToServer = $scope.TimelineSynchronizationBlock.maxShownId;
                });
            }

$interval(updateLastReadedTweetId, 5000);

            window.onscroll = function() {
                var updateScrolling = true;
                var wasUpdated = false;
                while (updateScrolling) {
                    updateScrolling = false;
                    if ($scope.TimelineSynchronizationBlock.maxShownId < $scope.TimelineSynchronizationBlock.tweetCounter) {
                        if (document.getElementById($scope.TimelineSynchronizationBlock.maxShownId).offsetTop > window.pageYOffset) {
                            updateScrolling = true;
                            $scope.TimelineSynchronizationBlock.maxShownId++;
                            wasUpdated = true;
                        }
                    }
                }

                var scrolled = window.pageYOffset || document.documentElement.scrollTop;
                if ($scope.TimelineSynchronizationBlock.scrollMarker > scrolled && !$scope.TimelineSynchronizationBlock.loading && !$scope.TimelineSynchronizationBlock.allIsLoaded) {
                    $scope.TimelineSynchronizationBlock.loading = true;
                    var elemId = $scope.TimelineSynchronizationBlock.maxShownId - 1;
                    var tweetId = $cookies.get("lastReadedTweetId");
                    if (elemId != -1) {
                        var tweet = document.getElementById(elemId);
                        tweetId = tweet.dataset.item;
                    }
                    var id = $cookies.get("userId");
                    $http({
                        method: 'POST',
                        //url: 'http://192.168.0.9:12008/tweets/user-time-line/' + id,
                        url: 'http://127.0.0.1:12008/tweets/user-time-line/unshown/' + id,
                        headers: {
                            'Content-Type': 'application/x-www-form-urlencoded',
                            'Page': $scope.TimelineSynchronizationBlock.loadedUnshownPages,
                            'LineHead': $scope.TimelineSynchronizationBlock.timeLineHiader,
                            'Authorization': "Token " + $cookies.get("token"),
                            'LastShown': tweetId
                        }
                    }).success(function(data) {
                        $scope.TimelineSynchronizationBlock.sendedToServer = $scope.TimelineSynchronizationBlock.maxShownId;
                        var offset = document.getElementById('anchor' + $scope.TimelineSynchronizationBlock.loadedUnshownPages).offsetTop;
                        $scope.TimelineSynchronizationBlock.loadedUnshownPages++;
                        var items = data.Statuses;
                        if (items.length == 0) {
                            $scope.TimelineSynchronizationBlock.loading = false;
                            $scope.TimelineSynchronizationBlock.allIsLoaded = true;
                            return;
                        }
                        var innerHtml = "<div id='anchor" + $scope.TimelineSynchronizationBlock.loadedUnshownPages + "'></div>";
                        $scope.TimelineSynchronizationBlock.tweetCounter += items.length;
                        for (var i = 0; i < items.length; i++) {
                            innerHtml += TweetViewBuilder.buildHtml(items[i].Text, $scope.TimelineSynchronizationBlock.tweetCounter - i, items[i].IdStr);
                        }

                        document.getElementById('unshown').innerHTML = innerHtml + document.getElementById('unshown').innerHTML;
                        offset += document.getElementById('anchor' + ($scope.TimelineSynchronizationBlock.loadedUnshownPages - 1)).offsetTop;
                        window.scrollTo(0, offset);
                        $scope.TimelineSynchronizationBlock.loading = false;
                    });
                }
            }
        })
});