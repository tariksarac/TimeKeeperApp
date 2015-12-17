///**
// * Created by Zinajda on 21.5.2015.
// */
//(function() {
//
//    var app = angular.module("timeKeeper");
//
//    var DashboardCtrl = function($scope, $rootScope, DataService) {
//
//        $rootScope.showLoader = true;
//        var promise = DataService.list("dashboard");
//        promise.then(function(response){
//                $scope.dashboard = response.data;
//                $scope.month = $scope.dashboard.month;
//                $scope.year = $scope.dashboard.year;
//                drawChart();
//                drawGauge();
//                drawColumn();
//                drawBar(0);
//                $rootScope.showLoader = false;
//            },
//            function(reason){});
//
//        function drawChart() {
//
//            var data = new google.visualization.DataTable();
//            data.addColumn('string', 'Team');
//            data.addColumn('number', 'Hours');
//            angular.forEach ($scope.dashboard.teamHours, function(item) {
//                data.addRow([ item.team, item.hours ]);
//            });
//            var options = {
//                'title': 'Working Hours by Teams',
//                'width': 420,
//                'height': 240
//            };
//
//            var chart = new google.visualization.PieChart(document.getElementById('chart_div'));
//            chart.draw(data, options);
//        };
//
//        function drawColumn() {
//
//            var data = new google.visualization.DataTable();
//            data.addColumn('string', 'Person');
//            data.addColumn('number', 'Missing Entries');
//            angular.forEach ($scope.dashboard.misEntries, function(item) {
//                data.addRow([ item.person, item.entries ]);
//            });
//            var options = {
//                'chart': {
//                    'title': 'Missing Entries by Person',
//                    'legend': null
//                },
//                'legend': { 'position': 'none' },
//                'width': 480,
//                'height': 240,
//                'bars': 'horizontal'
//            };
//
//            var chart = new google.charts.Bar(document.getElementById('colum_div'));
//            chart.draw(data, options);
//        };
//
//        $scope.showTeam = function(ind) {
//            console.log(ind);
//            drawBar(ind);
//        };
//
//        function drawBar(ind) {
//
//            var mime = [ "jan", "feb", "mar", "apr", "may", "jun", "jul", "aug", "sep", "oct", "nov", "dec" ];
//            var data = new google.visualization.DataTable();
//            data.addColumn('string', 'Month');
//            data.addColumn('number', 'Hours');
//            var X = $scope.dashboard.teamYear[ind];
//            console.log(X);
//            var i = 0;
//            angular.forEach(X.hours, function(item) {
//                data.addRow([ mime[i++], item ]);
//            });
//            var options = {
//                'width': 900,
//                'height': 320,
//                chart: {
//                    'title': X.team,
//                    'subtitle': 'Working Hours by Month'
//                },
//                format:'#,###'
//            };
//
//            var chart = new google.charts.Bar(document.getElementById('bar_div'));
//            chart.draw(data, options);
//        };
//
//        function drawGauge() {
//
//            var data1 = google.visualization.arrayToDataTable([
//                ['Label', 'Value'],
//                ['Work', Math.round(100 * $scope.dashboard.work / $scope.dashboard.total)]
//            ]);
//            var options1 = {
//                width: 180, height: 150,
//                redFrom: 0, redTo: 50,
//                yellowFrom: 50, yellowTo: 80,
//                greenFrom: 80, greenTo: 100,
//                minorTicks: 5
//            };
//            var data2 = google.visualization.arrayToDataTable([
//                ['Label', 'Value'],
//                ['PTO', Math.round(100 * $scope.dashboard.pto / $scope.dashboard.total)],
//                ['Empty', Math.round(100 * $scope.dashboard.empty / $scope.dashboard.total)]
//            ]);
//            var options2 = {
//                width: 300, height: 150,
//                redFrom: 75, redTo: 100,
//                yellowFrom:50, yellowTo: 75,
//                greenFrom:0, greenTo:50,
//                minorTicks: 5
//            };
//
//            var chart = new google.visualization.Gauge(document.getElementById('gauge_div1'));
//            chart.draw(data1, options1);
//            var charu = new google.visualization.Gauge(document.getElementById('gauge_div2'));
//            charu.draw(data2, options2);
//        };
//    };
//
//    app.controller("DashboardCtrl", DashboardCtrl);
//
//}());

(function() {

    var app = angular.module("timeKeeper");

    var DashboardCtrl = function($scope, $rootScope, DataService) {

        $rootScope.showLoader = true;
        var promise = DataService.list("dashboard");
        promise.then(function(response){
                $scope.dashboard = response.data;
                $scope.month = $scope.dashboard.month;
                $scope.year = $scope.dashboard.year;
                drawChart();
                drawGauge();
                drawColumn();
                drawBar(0);
                $rootScope.showLoader = false;
            },
            function(reason){});

        function drawChart() {

            var data = new google.visualization.DataTable();
            data.addColumn('string', 'Project');
            data.addColumn('number', 'Hours');
            angular.forEach ($scope.dashboard.projectHours, function(item) {
                data.addRow([ item.project, item.hours ]);
            });
            var options = {
                'title': 'Working Hours by Projects',
                'width': 420,
                'height': 240
            };

            var chart = new google.visualization.PieChart(document.getElementById('chart_div'));
            chart.draw(data, options);
        };

        function drawColumn() {

            var data = new google.visualization.DataTable();
            data.addColumn('string', 'Person');
            data.addColumn('number', 'Missing Entries');
            angular.forEach ($scope.dashboard.misEntries, function(item) {
                data.addRow([ item.person, item.entries ]);
            });
            var options = {
                'chart': {
                    'title': 'Missing Entries by Person',
                    'legend': null
                },
                'legend': { 'position': 'none' },
                'width': 480,
                'height': 240,
                'bars': 'horizontal'
            };

            var chart = new google.charts.Bar(document.getElementById('colum_div'));
            chart.draw(data, options);
        };

        $scope.showTeam = function(ind) {
            drawBar(ind);
        };

        function drawBar(ind) {

            var mime = [ "jan", "feb", "mar", "apr", "may", "jun", "jul", "aug", "sep", "oct", "nov", "dec" ];
            var data = new google.visualization.DataTable();
            data.addColumn('string', 'Month');
            data.addColumn('number', 'Hours');
            var X = $scope.dashboard.teamYear[ind];
            var i = 0;
            angular.forEach(X.hours, function(item) {
                data.addRow([ mime[i++], item ]);
            });
            var options = {
                'width': 900,
                'height': 320,
                chart: {
                    'title': X.team,
                    'subtitle': 'Working Hours by Month'
                },
                format:'#,###'
            };

            var chart = new google.charts.Bar(document.getElementById('bar_div'));
            chart.draw(data, options);
        };

        function drawGauge() {

            var data1 = google.visualization.arrayToDataTable([
                ['Label', 'Value'],
                ['Work', Math.round(100 * $scope.dashboard.work / $scope.dashboard.total)]
            ]);
            var options1 = {
                width: 180, height: 150,
                redFrom: 0, redTo: 50,
                yellowFrom: 50, yellowTo: 80,
                greenFrom: 80, greenTo: 100,
                minorTicks: 5
            };
            var data2 = google.visualization.arrayToDataTable([
                ['Label', 'Value'],
                ['PTO', Math.round(100 * $scope.dashboard.pto / $scope.dashboard.total)],
                ['Empty', Math.round(100 * $scope.dashboard.empty / $scope.dashboard.total)]
            ]);
            var options2 = {
                width: 300, height: 150,
                redFrom: 75, redTo: 100,
                yellowFrom:50, yellowTo: 75,
                greenFrom:0, greenTo:50,
                minorTicks: 5
            };

            var chart = new google.visualization.Gauge(document.getElementById('gauge_div1'));
            chart.draw(data1, options1);
            var charu = new google.visualization.Gauge(document.getElementById('gauge_div2'));
            charu.draw(data2, options2);
        };
    };

    app.controller("DashboardCtrl", DashboardCtrl);

}());