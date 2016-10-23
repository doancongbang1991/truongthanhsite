
        function CallHandler() {
            $.ajax({
                url: "../ServiceHandler.ashx",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                responseType: "json",
                data: { method: 'GetFur' },
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
app.controller('furniturectrlr', function ($scope, $http) {
    var req = {
        method: 'GET',
        url: '../ServiceHandler.ashx',
        params: { method: 'GetFur' }
    };
    $http(req)
    .then(function (response) {
        $scope.furs = response.data;
    
    });
});
