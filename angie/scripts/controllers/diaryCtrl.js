

(function(){

    var app = angular.module("timeKeeper");

    var DiaryCtrl = function($scope, $rootScope, $routeParams, DataService, $modal, timeConfig) {

        $scope.week = timeConfig.week;
        $scope.months = timeConfig.months;
        $scope.year = timeConfig.year;

        $scope.month = $routeParams.id;
        $scope.memberId = $rootScope.currentUser.id;

        LoadProjects();
        LoadDiary($scope.memberId, $scope.month, $scope.year);
        if ($rootScope.currentUser.role == "Team Lead" && $scope.members == null)
        {
            $scope.isTeamLead = true;
            LoadMembers();
        }

        function LoadProjects() {
            var promise = DataService.list("projects");
            promise.then(function(response) { $rootScope.projects = response.data },
                function(reason) { $rootScope.message = "Error fetching data!"});
        };

        function LoadDiary(memberId, month, year) {

            var route = "personal/" + memberId + "/" + year + "/" + month;
            var promise = DataService.list(route);

            promise.then(
                function(response) {
                    $scope.monthData = response.data;
                    $scope.selMonth = $scope.monthData.month - 1;
                },
                function(reason) {
                    $scope.message = "Error reading data from server!";
                }
            );
        };

        $rootScope.RefreshDiary = function() {
            LoadDiary($scope.memberId, $scope.month, $scope.year);
        };

        function LoadMembers() {
            var promise = DataService.get("teams", $rootScope.currentUser.teamId );
            promise.then(function(response) { $scope.members = response.data.members },
                function(reason) { $scope.message = "Error fetching data!"});
        }

        $scope.chgMember = function(memberId) {
            $scope.memberId = memberId;
            LoadDiary($scope.memberId, $scope.month, $scope.year);
        };

        $scope.chgMonth= function(choice) {
            $scope.month = choice + 1;
            LoadDiary($scope.memberId, $scope.month, $scope.year);
        };

        $scope.open = function(day) {
            $rootScope.currentMonth = $scope.monthData;
            $rootScope.currentDay = day;
            var modalInstance = $modal.open({
                templateUrl: 'views/diaryPart.html',
                controller: 'DiaryPartCtrl',
                windowClass: 'app-modal-window',
                backdrop: 'static',
                scope: $scope
            });
        }
    };

    app.controller("DiaryCtrl", DiaryCtrl);

    var DiaryPartCtrl = function($scope, $rootScope, $modalInstance, DataService) {

        LoadDetails();

        function LoadDetails() {
            var promise;
            if ($rootScope.currentDay.id == 0) {
                var obj = {
                    id: 0,
                    date: $rootScope.currentDay.day + "." + $rootScope.currentMonth.month + "." + $rootScope.currentMonth.year,
                    type: "1",
                    time: 0,
                    note: " ",
                    personId: $rootScope.currentMonth.personId
                };
                promise = DataService.post("days", obj);
            }
            else {
                promise = DataService.get("days", $rootScope.currentDay.id);
            }
            promise.then(
                function(response) {
                    $rootScope.currentDay = response.data;
                    $scope.newdet = {
                        id: 0,
                        time: 0,
                        note: " ",
                        projectId: 0
                    };
                    $scope.vacationButton = ($rootScope.currentDay.details == null || $rootScope.currentDay.details.length == 0)
                },
                function(reason) {
                    $scope.message = "Server communication error!";
                }
            )
        };

        $scope.close = function () {
            $rootScope.RefreshDiary();
            $modalInstance.close();
        };

        $scope.insert = function(detail) {
            var obj = {
                id: 0,
                time: detail.time,
                note: detail.note,
                status: "1",
                projectId: detail.projectId,
                dayId: $rootScope.currentDay.id
            };
            var promise = DataService.post("details", obj);
            promise.then(
                function(response) {
                    LoadDetails();
                },
                function(reason) {
                    $scope.message = "Server communication error!";
                }
            )
        };

        $scope.update = function(detail) {
            var obj = {
                id: detail.id,
                time: detail.time,
                note: detail.note,
                status: "1",
                projectId: detail.projectId,
                dayId: $rootScope.currentDay.id
            };
            var promise = DataService.put("details", detail.id, obj);
            promise.then(
                function(response) {
                    LoadDetails();
                },
                function(reason) {
                    $scope.message = "Server communication error!";
                }
            )
        };

        $scope.delete = function(detail) {
            var promise = DataService.delete("details", detail.id);
            promise.then(
                function(response) {
                    LoadDetails();
                },
                function(reason) {
                    $scope.message = "Server communication error!";
                }
            )
        };

        $scope.vacation = function(detail) {
            var datum = $rootScope.currentDay.date;
            var promise;
            var obj = {
                id: $rootScope.currentDay.id,
                date: datum,
                type: "2",
                time: 0,
                note: $rootScope.currentDay.note,
                person: $rootScope.currentMonth.person,
                personId: $rootScope.currentMonth.personId
            };
            if ($rootScope.currentDay.id == 0)
                promise = DataService.post("days", obj);
            else
                promise = DataService.put("days", $rootScope.currentDay.id, obj);
            promise.then(
                function(response){
                    $rootScope.RefreshDiary();
                    $modalInstance.close();
                },
                function(reason){
                    $modalInstance.close();
                });
        };
    };

    app.controller("DiaryPartCtrl", DiaryPartCtrl);

}());
