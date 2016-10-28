
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
app.controller('indexctrlr', function ($scope, $http) {
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
        params: { method: 'GetProject' }
    };
    $http(req1)
    .then(function (response) {
        $scope.projects = response.data;

    });
    
});
