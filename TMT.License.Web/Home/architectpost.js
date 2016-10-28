
var app = angular.module('truongthanhApp', []);
app.config(function ($locationProvider) {
    $locationProvider.html5Mode({
        enabled: true,
        requireBase: false
    });
});

app.controller('architectctrlr', function ($scope, $http, $location,$sce) {
    //menu
    
    var archid = $location.search().archid;
    var req2 = {
        method: 'GET',
        url: '../ServiceHandler.ashx',
        params: { method: 'GetArchByID', archid: archid }

    };
    $http(req2)
    .then(function (response) {

        $scope.archs = response.data;
        $scope.renderHtml = function (html_code) {
            return $sce.trustAsHtml(html_code);
        };
        $scope.body = $scope.archs[0].ArchDetail;
        
    });

    


});

app.run(['$window', '$rootScope',
    function ($window, $rootScope) {
        $rootScope.goBack = function () {
            $window.history.back();
        }
    }]);