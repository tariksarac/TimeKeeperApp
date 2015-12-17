//(function(){
//    var app = angular.module("timeKeeper");
//
//    var currentDay = function() {
//        return {
//            restrict: "E",
//            scope: true,
//            replace: true,
//            transclude: true,
//            /*link: function(scope, element, attrs){
//             elem.bind("click", function(){
//
//             })
//             },*/
//            template: "<div ng-transclude>This is a template</div>"
//        };
//    };
//
//    app.directive("currentDay", currentDay);
//}());

(function(){
    var app = angular.module("timeKeeper");

    var currentDay = function(){

        return{
            restrict:"E",
            scope:true,
            replace: true,
            transclude:true,
            link: function (scope, element, attributes) {
                element.bind("mouseover", function(){
                    element.css("cursor", "pointer");
                });
                element.bind("click", function(){
                    element.css("background-color", (attributes.type == "workday" ? "green" : "red"));
                    console.log(scope.currentUser);
                });

            },
            template: "<div ng-transclude>This is template</div>"

        };
    };
    app.directive("currentDay", currentDay)
}());