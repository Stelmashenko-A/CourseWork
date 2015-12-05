twittyApp.controller('TimeLineController',
    function TimeLineController($scope, $http, $routeParams, $q) {
        $scope.page = 0;
        //$scope.lineHead = 99999999999;
        if (!angular.isDefined($scope.lineHead)) {
            var arr = [];
            arr.push($http.post('http://127.0.0.1:12008/tweets/user-time-line/lineHead/2765688547'));
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
            $q.all(arr).then(function (ret) {
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
            });
        }


       

           
    }
)