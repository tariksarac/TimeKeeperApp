

(function() {

    var app = angular.module("timeKeeper");

    var TeamCtrl = function($scope, $rootScope, $http, DataService, $modal, $log, $window) {   //dependency injection. $scope... kontroler koristi za svoju funkciju

        loadTeams();

        $scope.modalUpdate = function (size) {

            var modalInstance = $modal.open({
                templateUrl: 'myModalContent.html',
                controller: function($scope, $modalInstance, team ) {
                    $scope.edit= team;
                    if ($scope.edit.id == 0)
                        $scope.doing = "Add team";
                    else $scope.doing= "Editing team: " + $scope.edit.name;

                    $scope.ok = function () {
                        $modalInstance.close($scope.edit);                       
                    };

                    $scope.cancel = function () {
                        $modalInstance.dismiss('cancel');
                    };

                    loadMembers();
                    function loadMembers() {

                        $rootScope.showLoader = true;

                        DataService.list("teams/"+ $scope.edit.id).then(function(response) { $scope.members = response.data; $rootScope.showLoader = false },
                            function(reason) { $scope.message = "Error fetching data!"; $rootScope.showLoader = false});

                    };


                },
                size: size,
                resolve: { 
                       team: function () {
                          return $scope.edit;
                       }
                }
            });
            modalInstance.result.then(function (editedTeam) {
                $scope.edit = editedTeam;
                $scope.saveData($scope.edit); 

            }, function () {
                $log.info('Modal dismissed at: ' + new Date());
            });
        };
        


        function loadTeams() {

            $rootScope.showLoader = true;


            DataService.list("teams").then(function(response) { $scope.teams = response.data; $rootScope.showLoader = false },
                function(reason) { $scope.message = "Error fetching data!"; $rootScope.showLoader = false});
        };

        $scope.saveData = function() {

            if ($scope.edit.id == 0)
            {
                var promise = DataService.post("teams", $scope.edit);
            }
            else
            {
                var promise = DataService.put("teams", $scope.edit.id, $scope.edit);
            }
            promise.then(function(response) { loadTeams(); },
                function(reason) { $scope.message = "Error saving data!"; });
        };

        $scope.editTeam = function(team) {

            $scope.edit = team;

        };

        $scope.clearData = function() {

            $scope.edit = {
                id: 0,
                name: ""

            };

        };


        $scope.deleteObj = function(set){
            $scope.edit = set;

            if($scope.edit.size == 0 || $scope.edit.size == null)
            {
                var promise = DataService.delete("teams", $scope.edit.id);
                promise.then(function(response){ loadTeams(); }, $scope.clearData(),
                    function(reason){$scope.message = "Error!!!!!!!!"});
            }
            else
            {
                $window.alert("Unable to delete object, delete child objects first!!");
            }
        };


    };

    app.controller("TeamCtrl", TeamCtrl);

}());

