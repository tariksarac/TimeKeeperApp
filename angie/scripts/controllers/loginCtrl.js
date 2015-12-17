/**
 * Created by Work on 7.5.2015..
 */
(function() {

    var app = angular.module("timeKeeper");

    var LoginCtrl = function($scope, $rootScope, $location, LoginService) {

        $scope.user = LoginService.getCredentials();

        //tryLogin($scope.user);

        $scope.message = "Provide your credentials please";

        $scope.identify = function(user) {

            tryLogin(user);
        };

        $scope.cancel = function() {

            location.href = "http://www.angularjs.org";

        };

        function tryLogin(user) {

            LoginService.login(user).then(

                function(response) {
                    LoginService.clearCredentials();
                    if (user.remember) LoginService.setCredentials(user);
                    $rootScope.currentUser = response.data;
                    $rootScope.authenticated = true;
                    $scope.message = "";
                    $location.path("/main");
                },

                function(reason) {
                    LoginService.clearCredentials();
                    $rootScope.currentUser = null;
                    $rootScope.authenticated = null;
                    $scope.message = "Bad credentials! Please try again";
                }
            )}
    };

    app.controller("LoginCtrl", LoginCtrl);

    var LogoutCtrl = function($scope, $rootScope, $location, LoginService) {

        var clearAll = function(obj) {
            $rootScope.currentUser = null;
            $rootScope.authenticated = null;
            $location.path("/login");
        };

        LoginService.logout().then(clearAll, clearAll);
    };

    app.controller("LogoutCtrl", LogoutCtrl);

}());


//-------------------------login bez usera----------------------------
//(function() {
//
//    var app = angular.module("timeKeeper");
//
//    var LoginCtrl = function($scope, $rootScope, $location, LoginService) {
//
//        $scope.user = LoginService.getCredentials();
//        //tryLogin($scope.user);
//
//        $scope.message = "Provide your credentials please";
//
//        $scope.identify = function(user) {
//
//            tryLogin(user);
//        };
//
//        function tryLogin(user) {
//            $rootScope.currentUser = {
//                id: 6,
//                fullName: "Amar Bajrovic",
//                userName: "amar",
//                team: "Tramontana",
//                teamId: 1,
//                role: "Team Lead"
//            };
//            $rootScope.authenticated = true;
//            $scope.message = "";
//            $location.path("/main");
//        };
//
//
//        /*
//         LoginService.login(user).then(
//
//         function(response) {
//         LoginService.clearCredentials()
//         if (user.remember) LoginService.setCredentials(user);
//         //$rootScope.currentUser = response.data;
//         $rootScope.currentUser = {
//         id: 6,
//         fullName: "Ana Balta",
//         userName: "ana",
//         team: "Tramontana",
//         role: "Team Lead"
//         };
//         $rootScope.authenticated = true;
//         $scope.message = "";
//         $location.path("/main");
//         },
//
//         function(reason) {
//         LoginService.clearCredentials();
//         $rootScope.currentUser = null;
//         $rootScope.authenticated = null;
//         $scope.message = "Bad credentials! Please try again";
//         }
//         )}
//         */
//    };
//
//    app.controller("LoginCtrl", LoginCtrl);
//
//    var LogoutCtrl = function($scope, $rootScope, $location, LoginService) {
//
//        var clearAll = function(obj) {
//            $rootScope.currentUser = null;
//            $rootScope.authenticated = null;
//            $location.path("/login");
//        };
//
//        LoginService.logout().then(clearAll, clearAll);
//    };
//
//    app.controller("LogoutCtrl", LogoutCtrl);
//
//}());
