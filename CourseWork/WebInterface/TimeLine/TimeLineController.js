twittyApp.factory('Shown', function ($http, $cookies) {
    var Shown = function () {
        this.items = [];
        this.busy = false;
        this.page = 0;
        this.after = Number($cookies.get("lastReadedTweetId"))+1;

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
                //     this.after = "t3_" + this.items[this.items.length - 1].id;

                this.after = this.items[this.items.length - 1].IdStr;
                this.page++;
            }
            this.busy = false;
        }.bind(this));
    };
    Shown.prototype.scrolling=function()
    {
        var t =0;
        var i=t;
    }
    return Shown;
});

twittyApp.controller('TimeLineController', function ($scope, $http, $cookies,  $timeout, $q,Shown) {
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
        })


});