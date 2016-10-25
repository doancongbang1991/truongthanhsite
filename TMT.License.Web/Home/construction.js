
        function CallHandler() {
            $.ajax({
                url: "../ServiceHandler.ashx",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                responseType: "json",
                data: { method: 'GetSite' },
                success: OnComplete,
                error: OnFail
            });
            return false;
        }

function OnComplete(result) {
    //var obj = JSON.parse(result);
    console.log(result);
}
function OnFail(result) {
    alert('Request Failed');
}

$(document).ready(function () {
    //CallHandler();
});

var app = angular.module('truongthanhApp', []);
app.controller('constructionctrlr', function ($scope, $http) {
    //menu
    var req = {
        method: 'GET',
        url: '../ServiceHandler.ashx',
        params: { method: 'GetSite' }
    };
    $http(req)
    .then(function (response) {
        $scope.sites = response.data;
       
    });
    var req1 = {
        method: 'GET',
        url: '../ServiceHandler.ashx',
        params: { method: 'GetCon' }
    };
    $http(req1)
    .then(function (response) {
        $scope.cons = response.data;

    });
    var name = "Construction";
    var req2 = {
        method: 'GET',
        url: '../ServiceHandler.ashx',
        params: { method: 'GetSiteByName', name: name }

    };
    $http(req2)
    .then(function (response) {

        $scope.sitedetails = response.data;
    });
    var req3 = {
        method: 'GET',
        url: '../ServiceHandler.ashx',
        params: { method: 'GetConType' }
    };
    $http(req3)
    .then(function (response) {
        $scope.contypes = response.data;

    });
    var req4 = {
        method: 'GET',
        url: '../ServiceHandler.ashx',
        params: { method: 'GetConByType' }
    };
    $http(req4)
    .then(function (response) {
        $scope.conbytypes = response.data;

    });
});
