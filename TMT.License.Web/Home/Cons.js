
        function CallHandler() {
            $.ajax({
                url: "../ServiceHandler.ashx",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                responseType: "json",
                data: { method: 'GetCon' },
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
    var req = {
        method: 'GET',
        url: '../ServiceHandler.ashx',
        params: { method: 'GetCon' }
    };
    $http(req)
    .then(function (response) {
        $scope.cons = response.data;
       
    });
});
