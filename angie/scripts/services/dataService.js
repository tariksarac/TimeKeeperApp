

(function() {

    var app = angular.module("timeKeeper");

    var DataService = function($http, timeConfig) {

        var source = timeConfig.source;

        return {
            list: function(set) {
                return $http.get(source + set)
            },

            get: function(set, id) {
                return $http.get(source + set + "/" + id)
            },

            post: function(set, obj) {
                return $http({ method:"post",
                    url:source+set,
                    data:obj })},

            put: function(set, id, obj) {
                return $http({ method:"post",
                    url:source+set+"/"+id,
                    data:obj })},

            delete: function(set, id) {
                //return $http({ method:"delete",
                //    url:source+set+"/"+id })}
                return $http({ method:"post",
                    url:source+set+"/-"+id })}
        };

    };

    app.factory("DataService", DataService);

}());