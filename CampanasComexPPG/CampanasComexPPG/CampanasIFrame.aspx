<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CampanasIFrame.aspx.cs" Inherits="CampanasComexPPG.CampanasIFrame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="Content/bootstrap.css" rel="stylesheet" />
    <script src="Scripts/bootstrap.js"></script>
    <script src="Scripts/jquery-3.3.1.js"></script>

    <script src="Scripts/ui-bootstrap-tpls-0.12.0.js"></script>

    <script type="text/javascript">

        //$(function(){
        //    var f = $('#frame')
        //    f.contents().find('div').hide();
        //    f.load(function () {
        //        f.contents().find('div').hide();
        //    });
        //})

        //$('#frame').load(function () {
        //    $('#frame').contents().find('html').css("display", "none");
        //});

        $(window).on('load', function () {

            angular.module('app', ['tooltips'])

            // code here
            //$('#frame').contents().find('html').css("overflow", "hidden");

     //       var iframe = document.getElementById("frame");
  			//var elmnt = iframe.contentWindow.document.getElementById("Correo");
     //           elmnt.value = 'prueba';

     //           var uno = 'dps';
        });

        //var frame = $('#frame');

        //$('#body').css("overflow", "hidden !important");

        //$('#body').addClass("scroll-page");

        //var uno = frame.find("body");

        //uno.css("overflow", "hidden");

        var dos = 0;
    </script>
    <style>
        .scroll-page
        {
            overflow:hidden;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="embed-responsive embed-responsive-16by9" style="width:900px; height:400px; overflow:hidden;">
            <iframe id="frame" class="embed-responsive-item frame" src="http://localhost:50610/" allowfullscreen>
            </iframe>
        </div>
    </form>

    <button class="btn-primary" tooltip="Hola Mundo" tooltip-placement="top">Ejemplo</button>
</body>
</html>
