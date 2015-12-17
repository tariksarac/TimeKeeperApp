/**
 * Created by Work on 7.5.2015..
 */
(function() {

    var app = angular.module("timeKeeper");

    var MainCtrl = function($scope) {

        $scope.roles = ['Team Lead', 'Developer', 'QA Engineer', 'UI/UX Designer'];
        $scope.selection = ['Team Lead', 'QA Engineer'];

        $scope.toggleSelection = function toggleSelection(role) {

            var idx = $scope.selection.indexOf(role);

            if (idx > -1) {
                $scope.selection.splice(idx, 1);
            }
            else {
                $scope.selection.push(role);
            }

            console.log($scope.selection)
        };

    };

    app.controller("MainCtrl", MainCtrl);

}());










