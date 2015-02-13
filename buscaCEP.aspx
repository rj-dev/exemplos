<%@ Page Language="C#" AutoEventWireup="true" CodeFile="buscaCEP.aspx.cs" Inherits="buscaCEP" %>

<!DOCTYPE html>

<html ng-app="pesquisaCep">
<head runat="server">
    <title></title>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.3/jquery.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/angularjs/1.3.11/angular.min.js"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/angularjs/1.2.8/angular-route.js"></script>
    <script type="text/javascript" src="js/app.js"></script>

    <script type="text/javascript">
        function buscaCep(cep) {

            $('#resultado').html("<img src=\"images/reload_v2.svg\"/>");

            $.ajax({
                type: "POST",
                url: "buscaCep.aspx/WsBuscaCep",
                data: '{cep:"' + cep + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var result = JSON.parse(response.d);
                    //$('#resultado').text(result);
                    $('#resultado').html('UF: ' + result.uf + '</br>Cidade: ' + result.cidade + '<br>Bairro: ' + result.bairro + '<br>Tipo Logradouro: ' + result.tipo_logradouro + '<br>Logradouro: ' + result.logradouro + '<br>Resultado: ' + result.resultado + '<br>Resultado texto: ' + result.resultado_txt);

                },
                failure: function (response) {
                    alert(response.d);
                }
            });
        }
    </script>
</head>
<body ng-controller="BuscaEnderecoPeloCep as endereco">
    <form id="form1" runat="server">
        <div>
            <asp:TextBox runat="server" ID="txtCep" ng-model="cep"></asp:TextBox>
            <input type="button" value="Pesquisar" onclick="buscaCep($('[id$=txtCep]').val())" />
            <input type="button" value="Pesquisar Angular" ng-click="buscaCep()" />
            <div id="resultado"></div>
            {{ endereco.uf }}
        </div>
    </form>
</body>
</html>
