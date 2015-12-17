(function() {

    var app = angular.module("timeKeeper");

    var RoleCtrl = function($scope, $rootScope, $http, DataService, $modal, $log, $window) {

        loadRoles();


        //MODAL==========open a modal window to update a single project
        $scope.modalUpdate = function (size) {

            var modalInstance = $modal.open({
                templateUrl: 'myModalContent.html',
                controller: function($scope, $modalInstance, role ) {
                    $scope.edit= role;
                    if ($scope.edit.id == 0)
                        $scope.doing = "Add role";
                    else $scope.doing= "Editing role: " + $scope.edit.name;

                    $scope.ok = function () {
                        $modalInstance.close($scope.edit);
                        ;
                    };

                    $scope.cancel = function () {
                        $modalInstance.dismiss('cancel');
                    };


                    loadMembers();
                    function loadMembers() {

                        $rootScope.showLoader = true;

                        DataService.list("roles/"+ $scope.edit.id).then(function(response) { $scope.members = response.data; $rootScope.showLoader = false },
                            function(reason) { $scope.message = "Error fetching data!"; $rootScope.showLoader = false});

                    };


                },
                size: size,
                resolve: {                      // RESOLVEEEE   - samo da parametar (project) u controlleru uzme vrijednost $scope.edit-a, vidi da odmah na pocetku controllera $scope.edit ponovo tu istu vrijednost uzima nazad! :)
                    role: function () {
                        return $scope.edit;
                    }
                }
            });
            modalInstance.result.then(function (editedRole) {
                $scope.edit = editedRole;
                $scope.saveData($scope.edit); // THEN editedProject proslijedi metodi updateProject() da ga upise u bazu!

            }, function () {

                $log.info('Modal dismissed at: ' + new Date());
            });
        };
        //MODAL END===============================================================



        function loadRoles() {

            $rootScope.showLoader = true;

            DataService.list("roles").then(function(response) { $scope.roles = response.data; $rootScope.showLoader = false },
                function(reason) { $scope.message = "Error fetching data!"; $rootScope.showLoader = false});

        };

        $scope.saveData = function() {

            if ($scope.edit.id == 0)
            {
                var promise = DataService.post("roles", $scope.edit);
            }
            else
            {
                var promise = DataService.put("roles", $scope.edit.id, $scope.edit);
            }
            promise.then(function(response) { loadRoles(); },
                function(reason) { $scope.message = "Error saving data!"; });
        };

        $scope.editRole = function(role) {

            $scope.edit = role;

        };

        $scope.clearData = function() {

            $scope.edit = {
                id: 0,
                name: ""

            };

        };

        //===============================DELETE ROLE=================================
        $scope.deleteObj = function(set) {
            $scope.edit = set;
            if($scope.edit.size == 0)
            {
                var promise = DataService.delete("roles", $scope.edit.id);
                promise.then(function(){ loadRoles(); }, $scope.clearData(),
                    function(){$scope.message = "Error!!!!!!!!"});
            }
            else
            {
                $window.alert("Unable to delete object, delete child objects first!!");
            }
        };

    };

    app.controller("RoleCtrl", RoleCtrl);

}());



