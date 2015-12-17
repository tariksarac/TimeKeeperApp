/**
 * Created by Zinajda on 31.5.2015.
 */
(function () {

    var app = angular.module("timeKeeper");

    var PersonalCtrl = function ($scope, $rootScope, $routeParams, DataService, $filter) {


        $scope.selectedMonth = [{id: '1', name: 'January'}, {id: '2', name: 'February'}, {id: '3', name: 'March'},
            {id: '4', name: 'April'}, {id: '5', name: 'May'}, {id: '6', name: 'June'},
            {id: '7', name: 'July'}, {id: '8', name: 'August'}, {id: '9', name: 'September'},
            {id: '10', name: 'October'}, {id: '11', name: 'November'}, {id: '12', name: 'December'}];

        $scope.daysInWeek = [{id: '1', name: 'Sunday'}, {id: '2', name: 'Monday'}, {id: '3', name: 'Tuesday'}, {id: '4', name: 'Wednesday'},
            {id: '5', name: 'Thursday'}, {id: '6', name: 'Friday'}, {id: '7', name: 'Saturday'},
        ];

        if ($rootScope.currentUser.role == "Team Lead") {
            $scope.isTeamLead = true;
            loadDiary();
            getPersons();
        }
        else loadDiary();

        function loadDiary(){

            //var route = "personalReport/" + $rootScope.currentUser.id + "/0/" + $routeParams.id;
            $rootScope.showLoader = true;
            var route = null;

            if ($rootScope.edit != undefined) {
                route = "personal/" + $rootScope.edit.id + "/0/" + $scope.cMonth;
            }
            else {
                route = "personal/" + $rootScope.currentUser.id + "/0/" + $scope.cMonth;
            }

            var promise = DataService.list(route);

            promise.then(
                function(response) {
                    $scope.month = response.data;
                    $scope.day = new Date();
                    $scope.day.year = response.data.year;
                    $scope.day.month = response.data.month;
                    $scope.day.setDate(1);
                    $scope.number = $scope.day.getDay() - 1;
                    $rootScope.showLoader = false;

                },

                function(reason){
                    $scope.message = "Error reading data from server!";
                }
            );

        }

        $scope.getNumber = function() {
            loadDiary();
            $scope.array = new Array($scope.number);
        }

        function getPersons() {
            $rootScope.showLoader = true;

            var personRoute = "persons/" + $rootScope.currentUser.id;
            var promise = DataService.list(personRoute);
            promise.then(
                function (response) {
                    $scope.persona = response.data;
                    getTeam();
                    $rootScope.showLoader = false;
                },
                function (reason) {
                    $scope.message = "Error reading data from server";
                });

        }

        function getTeam() {
            $rootScope.showLoader = true;
            DataService.get("teams", $scope.persona.teamId).then(function (response) {
                    return  $scope.team = response.data;
                    $rootScope.showLoader = false;
                },
                function () {
                    return   $scope.message = "Error fetching data"
                });
        }

        $scope.changeMonth = function(m){

            $scope.cMonth = m;
            loadDiary();
            $scope.getNumber();
            $scope.currentDay = null;  //da ne zadrzi stare detalje
        };


        $scope.moveDetails = function(day) {
            $scope.currentDay = day;
        };

        $scope.updateReport = function (member) {
            $rootScope.edit = member;

            loadDiary();
        }
    };

    app.controller("PersonalCtrl", PersonalCtrl);

}());
