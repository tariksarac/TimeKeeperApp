

(function(){

    var app = angular.module("timeKeeper", ["ngRoute", "ngCookies", "ui.bootstrap" ]);

    app.constant("timeConfig", {
        source: "http://localhost:18855/api/",
        months: [ 'jan', 'feb', 'mar', 'apr', 'may', 'jun', 'jul', 'aug', 'sep', 'oct', 'nov', 'dec' ],
        week: [ 'sun', 'mon', 'tue', 'wed', 'thu', 'fri', 'sat' ],
        year: 2014
    });

    app.config(function($routeProvider, $locationProvider) {

        $routeProvider
            .when("/main", {template: "<div style='width:720px; height:360px; position:absolute; left:50%; top:50%; margin: -160px 0 0 -360px'><img src='images/crowd.png'></div>" })
            .when("/login", {templateUrl: "views/login.html", controller: "LoginCtrl"})
            .when("/logout", {templateUrl: "views/login.html", controller: "LogoutCtrl"})
            .when("/diary", {templateUrl: "views/diary.html", controller: "DiaryCtrl"})
            .when("/diary/:id", {templateUrl: "views/diary.html", controller: "DiaryCtrl"})
            .when("/projects", {templateUrl: "views/projectView.html", controller: "ProjectCtrl"})
            .when("/persons", {templateUrl: "views/personView.html", controller: "PersonCtrl"})
            .when("/teams", {templateUrl: "views/teamView.html", controller: "TeamCtrl"})
            .when("/roles", {templateUrl: "views/roleView.html", controller: "RoleCtrl"})
            .when("/annual", {templateUrl: "views/annual.html", controller: "AnnualCtrl"})
            .when("/monthly", {templateUrl: "views/monthlyReport.html", controller: "MonthlyCtrl"})
            .when("/personal", {templateUrl: "views/personalReport.html", controller: "DiaryCtrl"})
            .when("/dashboard", {templateUrl: "views/dashboard.html", controller: "DashboardCtrl"})
            .otherwise({redirectTo: "/main"});
    }).run(function($rootScope, $location){
        $rootScope.$on("$routeChangeStart", function(event, next, current){
            if ($rootScope.authenticated == null) {
                if (next.templateUrl != "views/login.html")
                    $location.path("/login");
            }
        })
    });

}());
