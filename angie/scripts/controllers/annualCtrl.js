
(function(){

    var app = angular.module("timeKeeper");

    var AnnualCtrl = function($scope,$rootScope, $http, DataService){

        $scope.change_year = function(year){
            $scope.year = year;
            loadAnnualReport();
        }

        if (typeof $scope.year == "undefined") $scope.year = 2014;

        $scope.range = function(n){
            return new Array(n);
        }

        $scope.months = [{id: '1', name: 'Jan'}, {id: '2', name: 'Feb'}, {id: '3', name: 'March'},
            {id: '4', name: 'April'}, {id: '5', name: 'May'}, {id: '6', name: 'June'},
            {id: '7', name: 'July'}, {id: '8', name: 'August'}, {id: '9', name: 'Sept'},
            {id: '10', name: 'Oct'}, {id: '11', name: 'Nov'}, {id: '12', name: 'Dec'}];

        loadAnnualReport();


        function loadAnnualReport() {
            $rootScope.showLoader = true;
            var promise = DataService.list("annual/" + $scope.year);
            promise.then(function(response){ $scope.annual = response.data;
                    $scope.year = response.data.year;
                    $rootScope.showLoader = false; },
                function(reason){ $scope.message = "Error fetching data"; $rootScope.showLoader = false; });
        };


    };

    app.controller("AnnualCtrl",AnnualCtrl);

}());