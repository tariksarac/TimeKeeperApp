/**
 * Created by Zinajda on 17.5.2015.
 */

(function() {

    var app = angular.module("timeKeeper");

    var PersonCtrl = function ($scope, $rootScope, $http, DataService,$modal,$log,$anchorScroll, $location) {

        $scope.currentPage = 1;

        $scope.pageChanged = function (pageNo) {
            $scope.currentPage = pageNo;

            loadPersons();
        };

        $scope.gotoTop = function(){
            $rootScope.showLoader = true;
            // set the location.hash to the id of
            // the element you wish to scroll to.
            $location.hash('top');

            // call $anchorScroll()
            $anchorScroll();
            $rootScope.showLoader = false;
        };
        loadRoles();
        loadTeams();
        loadPersons();

        function loadPersons() {
            $rootScope.showLoader = true;
            var promise = DataService.list("persons/page/" + ($scope.currentPage-1));

            promise.then(function(response){
                    $scope.totalItems = response.data.people.length;
                    $scope.maxSize = response.data.pageSize;
                    $scope.persons = response.data;
                    $scope.people = response.data.people;
                    $scope.pageCount = response.data.totalPages;
                    $scope.totalItems = response.data.pageSize * response.data.totalPages;

                    $scope.gotoTop();
                    $rootScope.showLoader = false;},

                function(reason){ $scope.message = "Error fetching data"; $rootScope.showLoader = false;});

        }

        function loadRoles() {
            var promise = DataService.list("roles");
            promise.then(function(response){ $scope.roles = response.data; },
                function(reason){ $scope.message = "Error fetching data"});
        }

        function loadTeams() {
            $rootScope.showLoader = true;
            DataService.list("teams").then(function(response){ $scope.teams = response.data; $rootScope.showLoader = false; },
                function(reason){ $scope.message = "Error fetching data"; $rootScope.showLoader = false;});
        }



        $scope.modalUpdate = function (size,selectedPerson) {
            var modalInstance = $modal.open({
                templateUrl: 'myModalContent.html',
                controller: 'ModalInstanceCtrl',
                resolve: {
                    person: function () {
                        return selectedPerson;
                    },
                    teams: function () {
                        return $scope.teams;
                    },
                    roles: function() {
                        return $scope.roles;
                    }
                }
            });
        };

        $scope.deletePerson = function(updatedPerson){

            $scope.edit = updatedPerson;
            var promise = DataService.delete("persons", $scope.edit.id, $scope.edit);
            promise.then(function(response){ loadPersons() },
                function(reason){ $scope.message = "Error fetching data"});

        };


        $scope.updatePerson = function(updatedPerson){

            $scope.edit = updatedPerson;

            if($scope.edit.id == 0)
            {
                var promise = DataService.post("persons", $scope.edit);
            }
            else
            {
                var promise = DataService.put("persons", $scope.edit.id, $scope.edit);
            }

            promise.then(function(response){ loadPersons() },
                function(reason){ $scope.message = "Error fetching data"});
        };


        $scope.clearData = function(person){
            $scope.person = {
                id: 0,
                firstName: "",
                lastName: "",
                email: "",
                phone: "",
                team: "",
                role: "",
                teamId: 0,
                roleId: 0
            };
        };

        $scope.editPerson = function(person){
            $scope.edit = person;
        };
    };

    app.controller("PersonCtrl",PersonCtrl);

    var ModalInstanceCtrl = function($scope, $modalInstance, person, teams, roles, DataService) {

        //$scope.master= angular.copy(person);
        $scope.person = person;
        $scope.teams= teams;
        $scope.roles = roles;

        if ($scope.person.id == 0 ) {
            $scope.doing = "Add person";
        }
        else{
            $scope.doing = "Editing " + person.firstName + " " +  person.lastName;
        }

        $scope.ok = function (person) {
            var promise;
            if (person.id==0) {
                promise = DataService.post("persons", person);
            }
            else {
                promise = DataService.put("persons", person.id, person);
            };
            promise.then(
                function(response) { console.log(response.data.id) },
                function(reason) {}
            );
            $modalInstance.close($scope.person);
        };
        $scope.cancel = function () {
            //angular.copy($scope.master, $scope.person);
            $modalInstance.dismiss('cancel');
        };

    };


    app.controller("ModalInstanceCtrl", ModalInstanceCtrl);


}());