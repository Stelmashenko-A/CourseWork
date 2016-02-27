twittyApp.factory('Shown', function ($http, $cookies) {
    var Shown = function () {
        this.items = [];
        this.busy = false;
        this.page = 0;
        this.after = Number($cookies.get("lastReadedTweetId")) + 1;

    };

    Shown.prototype.nextPage = function () {
        if (this.busy) return;
        this.busy = true;
        //
        var id = $cookies.get("userId");
        $http({
            method: 'POST',
            //url: 'http://192.168.0.9:12008/tweets/user-time-line/' + id,
            url: 'http://127.0.0.1:12008/tweets/user-time-line/' + id,
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded',
                'Page': this.page,
                'LineHead': this.after++,
                'Authorization': "Token " + $cookies.get("token")
            }
        }).success(function (data) {
            if (data != "") {
                var items = data.Statuses;

                for (var i = 0; i < items.length; i++) {
                    this.items.push(items[i]);
                }

                this.after = this.items[this.items.length - 1].IdStr;
                this.page++;
            }
            this.busy = false;
        }.bind(this));
    };
    Shown.prototype.scrolling = function () {
        var t = 0;
        var i = t;
    }
    return Shown;
});

twittyApp.factory('Unshown', function (TweetViewBuilder) {
    function Unshown() {
        this.pattern1 = "<div class=\"well tweet-well\">" +
        "<div class=\"row\">" +
        "<div class=\"twitt\">" +
        "<div class=\"twit-text\">" +
        "<span>";
        this.pattern2 = "</span>" +
        "</div>" +
        "<div class=\"row\">" +
        "<div class=\"col-xs-1 tweet-btm\">" +
        "<p><i class=\"glyphicon glyphicon-bullhorn\"></i></p>" +
        "</div>" +
        "<div class=\"col-xs-1 tweet-btm\">" +
        "<p><i class=\"glyphicon glyphicon-retweet\"></i></p>" +
        "</div>" +
        "<div class=\"col-xs-1 tweet-btm\">" +
        "<p><i class=\"glyphicon glyphicon-star\"></i></p>" +
        "</div>" +
        "</div>" +
        "</div>" +
        "</div>" +
        "</div>";
    }
    Unshown.prototype.scrollHandler = function () {
        var scrolled = window.pageYOffset || document.documentElement.scrollTop;
        document.getElementById('showScroll').innerHTML = scrolled + 'px';
    };
    return Unshown;
});

twittyApp.controller('TimeLineController', function ($scope, $http, $cookies, $timeout, $q, Shown, Unshown, TimelineSynchronizationBlock, TweetViewBuilder) {
    $http(
        {
            method: 'POST',
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded',
                'Authorization': "Token " + $cookies.get("token")
            },
            //url: 'http://192.168.0.9:12008/user/' + $cookies.get("userId")
            url: 'http://127.0.0.1:12008/user/' + $cookies.get("userId")
        }).success(function (user) {
            $scope.user = user;
            $cookies.put('lastReadedTweetId', $scope.user.LastReadedTweetId);
            $scope.Shown = new Shown();
            $scope.Unshown = new Unshown();
            $scope.TimelineSynchronizationBlock = new TimelineSynchronizationBlock();
            window.onscroll = function () {

                var scrolled = window.pageYOffset || document.documentElement.scrollTop;
                if ($scope.TimelineSynchronizationBlock.scrollMarker > scrolled && !$scope.TimelineSynchronizationBlock.loading && !$scope.TimelineSynchronizationBlock.allIsLoaded) {
                    $scope.TimelineSynchronizationBlock.loading = true;

                    var id = $cookies.get("userId");
                    $http({
                        method: 'POST',
                        //url: 'http://192.168.0.9:12008/tweets/user-time-line/' + id,
                        url: 'http://127.0.0.1:12008/tweets/user-time-line/unshown/' + id,
                        headers: {
                            'Content-Type': 'application/x-www-form-urlencoded',
                            'Page': $scope.TimelineSynchronizationBlock.loadedUnshownPages,
                            'LineHead': $scope.TimelineSynchronizationBlock.timeLineHiader,
                            'Authorization': "Token " + $cookies.get("token")
                        }
                    }).success(function (data) {
                        var offset =  document.getElementById('anchor'+$scope.TimelineSynchronizationBlock.loadedUnshownPages).offsetTop;
                        $scope.TimelineSynchronizationBlock.loadedUnshownPages++;
                        var items = data.Statuses;
                        if (items.length == 0) {
                            $scope.TimelineSynchronizationBlock.loading = false;
                            $scope.TimelineSynchronizationBlock.allIsLoaded = true;
                            return;
                        }
                        var innerHtml = "<div id='anchor"+$scope.TimelineSynchronizationBlock.loadedUnshownPages+"'></div>";
                        for (var i = 0; i < items.length; i++) {
                            innerHtml += TweetViewBuilder.buildHtml(items[i].Text);
                        }

                        document.getElementById('unshown').innerHTML = innerHtml + document.getElementById('unshown').innerHTML;
                        offset += document.getElementById('anchor'+($scope.TimelineSynchronizationBlock.loadedUnshownPages-1)).offsetTop;
                        window.scrollTo(0, offset);
                        $scope.TimelineSynchronizationBlock.loading = false;
                    });


                }
            }
        })
});