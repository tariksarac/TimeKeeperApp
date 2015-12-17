/**
 * Created by Zinajda on 31.5.2015.
 */

(function(){

    var app = angular.module("timeKeeper");

    var MonthlyCtrl = function($scope, $rootScope, DataService, $http){

        setMonth();

        loadMonthlyReport();

        $scope.monthIsChanged = function(){
            loadMonthlyReport();
        }

        $scope.range = function(n){
            return new Array(n);
        }


        function loadMonthlyReport() {

            $rootScope.showLoader = true;
            var promise = DataService.list("month/" + $scope.currentYear + "/" + $scope.selectedMonth.id);
            promise.then(function(response){ $scope.monthlyReport = response.data;
                    $scope.year= response.data.year;
                       console.log($scope.year);
                    $scope.numberOf = response.data.people[0].projects.length;
                    $rootScope.showLoader = false; },
                function(reason){ $scope.message = "Error fetching data"; $rootScope.showLoader = false; });

        }

        function setMonth() {

            var date = new Date();

            $scope.currentMonth = date.getMonth() + 1;

            //$scope.currentYear = date.getFullYear();


            $scope.months = [{id: '1', name: 'January'}, {id: '2', name: 'February'}, {id: '3', name: 'March'},
                {id: '4', name: 'April'}, {id: '5', name: 'May'}, {id: '6', name: 'June'},
                {id: '7', name: 'July'}, {id: '8', name: 'August'}, {id: '9', name: 'September'},
                {id: '10', name: 'October'}, {id: '11', name: 'November'}, {id: '12', name: 'December'}];

            //$scope.monthsToShow = $scope.months.slice(0, $scope.currentMonth);
            // if($scope.year = 2015) {
            //     $scope.monthsToShow = $scope.months.slice(0, $scope.currentMonth);}
            //else {
            //     $scope.monthsToShow =$scope.months;
            // }
            $scope.monthsToShow =$scope.months;
            if($scope.year = 2015) {
                     $scope.monthsToShow = $scope.months.slice(0, $scope.currentMonth);}
            $scope.selectedMonth = $scope.monthsToShow[0];
        }
        $scope.change_year = function(year){
            $scope.currentYear = year;
            loadMonthlyReport();
        }

        if (typeof $scope.currentYear == "undefined") $scope.currentYear = 2014;




    }

    app.controller("MonthlyCtrl", MonthlyCtrl);

}());