(function () {

    jQuery.support.cors = true;

    var app = angular.module('pesquisaCep', []);

    app.controller('BuscaEnderecoPeloCep', function ($scope, $http) {

        
        $scope.buscaCep = function () {
            $http.get('http://webservice.kinghost.net/web_cep.php?auth=3374bf016d2a052bef06e546bec592a0&formato=json&cep=' + $scope.cep).success(function (data) {
                $scope.endereco = data;
            });
        };
    });
})();