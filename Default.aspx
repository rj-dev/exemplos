<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Meu endereço:<br />
            Pais:
        <asp:TextBox ID="txbPais" runat="server" MaxLength="100" Columns="20"></asp:TextBox>
            UF:
        <asp:TextBox ID="txbUF" runat="server" MaxLength="2" Columns="3"></asp:TextBox>
            Cidade:
        <asp:TextBox ID="txbCidade" runat="server" MaxLength="100" Columns="40"></asp:TextBox>
            <br />
            Logradouro:
        <asp:TextBox ID="txbLogradouro" runat="server" MaxLength="200" Columns="50"></asp:TextBox>
            Numero:
        <asp:TextBox ID="txbNumero" runat="server" MaxLength="10" Columns="6"></asp:TextBox>
            CEP:
            <asp:TextBox ID="txtCep" runat="server" MaxLength="8" Columns="15"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="btnProcurar" runat="server" Text="Procurar Amigo mais próximo"
                OnClick="btnProcurar_Click" />
        </div>

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [Table]"></asp:SqlDataSource>

        <br />

    </form>
</body>
</html>
