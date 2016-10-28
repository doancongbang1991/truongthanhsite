(function () {
    'use strict';
    var app = angular.module('app', []);
    app.config(function ($locationProvider) {
        $locationProvider.html5Mode({
            enabled: true,
            requireBase: false
        });
    });
    app.factory('PagerService', PagerService)
    .controller('PageController', function ($scope, $http, $location, PagerService) {
        //menu

        
        
        
        var typeid = $location.search().typeid;
        
        var req3 = {
            method: 'GET',
            url: '../ServiceHandler.ashx',
            params: { method: 'GetArchByType', typeid: typeid }

        };
        $http(req3)
        .then(function (response) {

            $scope.archbytype = response.data;

            vm.dummyItems = $scope.archbytype
            vm.pager = {};
            vm.setPage = setPage;

            initController();
        });

        var vm = this;



        function initController() {
            // initialize to page 1
            vm.setPage(1);
        }

        function setPage(page) {
            if (page < 1 || page > vm.pager.totalPages) {
                return;
            }

            // get pager object from service
            vm.pager = PagerService.GetPager(vm.dummyItems.length, page);

            // get current page of items
            vm.items = vm.dummyItems.slice(vm.pager.startIndex, vm.pager.endIndex + 1);
        }
        function range(start, count) {
            return Array.apply(0, Array(count))
              .map(function (element, index) {
                  return index + start;
              });
        }

    });
    function PagerService() {
        // service definition
        var service = {};

        service.GetPager = GetPager;

        return service;

        // service implementation
        function GetPager(totalItems, currentPage, pageSize) {
            // default to first page
            pageSize = 3;
            currentPage = currentPage || 1;

            // default page size is 10
            pageSize = pageSize || 10;

            // calculate total pages
            var totalPages = Math.ceil(totalItems / pageSize);

            var startPage, endPage;
            if (totalPages <= 10) {
                // less than 10 total pages so show all
                startPage = 1;
                endPage = totalPages;
            } else {
                // more than 10 total pages so calculate start and end pages
                if (currentPage <= 6) {
                    startPage = 1;
                    endPage = 10;
                } else if (currentPage + 4 >= totalPages) {
                    startPage = totalPages - 9;
                    endPage = totalPages;
                } else {
                    startPage = currentPage - 5;
                    endPage = currentPage + 4;
                }
            }

            // calculate start and end item indexes
            var startIndex = (currentPage - 1) * pageSize;
            var endIndex = Math.min(startIndex + pageSize - 1, totalItems - 1);

            // create an array of pages to ng-repeat in the pager control
            var pages = range(startPage, endPage);
            //var pages = [1, 2, 3, 4];

            // return object with all pager properties required by the view
            return {
                totalItems: totalItems,
                currentPage: currentPage,
                pageSize: pageSize,
                totalPages: totalPages,
                startPage: startPage,
                endPage: endPage,
                startIndex: startIndex,
                endIndex: endIndex,
                pages: pages
            };
            function range(start, count) {
                return Array.apply(0, Array(count))
                  .map(function (element, index) {
                      return index + start;
                  });
            }
        }
    }
})();