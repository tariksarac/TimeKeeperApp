
(function() {

    var app = angular.module("timeKeeper");

    var ProjectCtrl = function($scope, $rootScope, $http, DataService, $modal, $log, $window) {

        loadProjects();

        $scope.modalUpdate = function (size) {

            var modalInstance = $modal.open({
                templateUrl: 'myModalContent.html',
                controller: function($scope, $modalInstance, project ) {
                    $scope.edit= project;
                    if ($scope.edit.id == 0)
                        $scope.doing = "Add project";
                    else $scope.doing= "Editing project: " + $scope.edit.name;

                    $scope.ok = function () {
                        $modalInstance.close($scope.edit);
                        ;
                    };

                    $scope.cancel = function () {
                        $modalInstance.dismiss('cancel');
                    };
                },
                size: size,
                resolve: {                      
                    project: function () {
                        return $scope.edit;
                    }
                }
            });
            modalInstance.result.then(function (editedProject) {
                $scope.edit = editedProject;
                $scope.saveData($scope.edit); 
            }, function () {
                $log.info('Modal dismissed at: ' + new Date());
            });
        };

		
        function loadProjects() {
            $rootScope.showLoader = true;


            DataService.list("projects").then(function(response) { $scope.projects = response.data; $rootScope.showLoader = false },
                function(reason) { $scope.message = "Error fetching data!"; $rootScope.showLoader = false});
        };

        $scope.saveData = function() {

            if ($scope.edit.id == 0)
            {
                var promise = DataService.post("projects", $scope.edit);
            }
            else
            {
                var promise = DataService.put("projects", $scope.edit.id, $scope.edit);
            }
            promise.then(function(response) { loadProjects(); },
                function(reason) { $scope.message = "Error saving data!"; });
        };

        $scope.editProject = function(project) {

            $scope.edit = project;

        };

        $scope.clearData = function() {

            $scope.edit = {
                id: 0,
                name: ""

            };

        };

        //===============================DELETE PROJECT=================================
        $scope.deleteObj = function(set){
            $scope.edit = set;
            if($scope.edit.details_Size == 0 || $scope.edit.details_Size == null)
            {
                var promise = DataService.delete("projects", $scope.edit.id);
                promise.then(function(){ loadProjects(); }, $scope.clearData(),
                    function(){$scope.message = "Error!!!!!!!!"});
            }
            else
            {
                $window.alert("Unable to delete object, delete child objects first!!");
            }
        };

    };

    app.controller("ProjectCtrl", ProjectCtrl);

}());
