twittyApp.factory('Reddit', function ($http) {
    var Reddit = function () {
        this.items = [];
        this.busy = false;
        this.page = 0;
        this.after = -1;
    };

    Reddit.prototype.nextPage = function () {
        if (this.busy) return;
        this.busy = true;
        //

            $http({
                method: 'POST',
                url: 'http://127.0.0.1:12008/tweets/user-time-line/2765688547',
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded',
                    'Page': this.page,
                    'LineHead': this.after
                }
            }).success(function (data) {
            
                var items = data.Statuses;
                for (var i = 0; i < items.length; i++) {
                    this.items.push(items[i]);
                }
                //     this.after = "t3_" + this.items[this.items.length - 1].id;

                this.after = this.items[this.items.length - 1].StatusID;
                this.page++;
                this.busy = false;
            }.bind(this));


        //
       /* var url = "http://api.reddit.com/hot?after=" + this.after + "&jsonp=JSON_CALLBACK";
        $http.jsonp(url).success(function (data) {
            var items = data.data.children;
            for (var i = 0; i < items.length; i++) {
                this.items.push(items[i].data);
            }
            this.after = "t3_" + this.items[this.items.length - 1].id;
            this.busy = false;
        }.bind(this));*/
    };

    return Reddit;
});
twittyApp.controller('TimeLineController', function ($scope, Reddit) {
    $scope.reddit = new Reddit();
       // $scope.page = 0;
        //$scope.lineHead = 99999999999;
      //  if (!angular.isDefined($scope.lineHead)) {
        //    var arr = [];
          //  arr.push($http.post('http://127.0.0.1:12008/tweets/user-time-line/lineHead/2765688547'));
            /* $http.post({
                    url: 'http://127.0.0.1:12008/tweets/user-time-line/lineHead/2765688547',
                    headers: {
                        'Content-Type': 'application/x-www-form-urlencoded'
                    }
                })
           .success(function (data) {
               $scope.lineHead = data;
               $scope.page = 0;


           });*/
            /*$q.all(arr).then(function (ret) {
                var t = ret[0].data;
                $scope.lineHead = t;
                

               $scope.load =  function(page) {
                    var isTerminal = $scope.pagination &&
                            $scope.pagination.current_page >= $scope.pagination.total_pages &&
                            $scope.pagination.current_page <= 1;

                    // Determine if there is a need to load a new page
                    if (!isTerminal) {
                        // Flag loading as started
                        $scope.loading = true;

                        // Make an API request
                        $http({
                                method: 'POST',
                                url: 'http://127.0.0.1:12008/tweets/user-time-line/2765688547',
                                headers: {
                                    'Content-Type': 'application/x-www-form-urlencoded',
                                    'Page': $scope.page,
                                    'LineHead': $scope.lineHead
                                }
                            })
                            .success(function(data, status, headers) {
                                // Parse pagination data from the response header
                                $scope.pagination = angular.fromJson(headers('x-pagination'));

                                // Create an array if not already created
                                $scope.items = $scope.items || [];


                                // Append new items (or prepend if loading previous pages)
                                $scope.items.push.apply($scope.items, data.Statuses);
                                $scope.page++;

                            })
                            .finally(function() {
                                // Flag loading as complete
                                $scope.loading = false;
                            });
                    }
               }
               $scope.$on('endlessScroll:next', function () {
                   // Determine which page to load
                 

                   // Load page
                   $scope.load($scope.page);
               });

                // Load initial page (first page or from query param)

               $scope.load($routeParams.page ? parseInt($routeParams.page, 10) : 1); // Register event handler
            });*/
    //    }


       

           
    }
)