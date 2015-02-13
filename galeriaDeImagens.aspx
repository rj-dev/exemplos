<%@ Page Language="C#" AutoEventWireup="true" CodeFile="galeriaDeImagens.aspx.cs" Inherits="galeriaDeImagens" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/galeryGI.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <section class="image-grid">
                <div class="image__cell is-collapsed">
                    <div class="image--basic">
                        <a href="#expand-jump-1">
                            <img id="expand-jump-1" class="basic__img" src="http://lorempixel.com/250/250/fashion/1" alt="Fashion 1" />
                        </a>
                        <div class="arrow--up"></div>
                    </div>
                    <div class="image--expand">
                        <a href="#close-jump-1" class="expand__close"></a>
                        <img class="image--large" src="http://lorempixel.com/400/400/fashion/1" alt="Fashion 1" />
                    </div>
                </div>
            </section>
        </div>
    </form>

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.3/jquery.min.js"></script>

    <script src="js/galeryGI.js"></script>

</body>
</html>
