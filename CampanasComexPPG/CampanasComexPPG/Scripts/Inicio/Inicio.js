var app = angular.module("MyApp", []);

document.addEventListener('DOMContentLoaded', function () {
    document.querySelector('main').className += 'loaded';
});

//funcion inicial para agregar las empresas
app.controller("MyController", function ($scope, $http, $window) {

    var urlPathSystem = "";

    var Correo = "";

    $(document).tooltip({
        tooltipClass: "uitooltip",
        position: {
            my: "left top",
            at: "right+5 top-5"
        }
    });

    //var event = new Event("message");

    //window.removeEventListener("message", displayMessage);

    //window.addEventListener("message", displayMessage, false);

    //window.dispatchEvent(Event);

    //function displayMessage(evt) {
    //    var message;
    //    if (evt.origin == "https://one.web.ppg.com") {
    //        //message = "You are not worthy";
    //        $('#CorreoOtro').val(evt.data);
    //        $('#CorreoSesion').val(evt.data);

    //        Correo = $('#CorreoSesion').val();

    //        CargarCorreo();
    //    }
    //}

    //if (window.addEventListener) {
    //    window.addEventListener("message", displayMessage, false);
    //}
    //else {
    //    window.attachEvent("onmessage", displayMessage);
    //}

    $('#CorreoSesion').val("ASolorzanoTapia@ppg.com");  
    Correo = $('#CorreoSesion').val();

    CargarCorreo();

    //$scope.MostrarMenuUsuario = false;
    //$scope.MostrarMenuCronograma = false;
    //$scope.MostrarMenuGraficos = false;

    //$('#myModalLoader').modal('show');

    //$http
    //    ({
    //        method: "POST",
    //        url: urlPathSystem + "/Inicio/ValidUsuario",
    //        datatype: 'json',
    //        contentType: 'application/json; charset=utf-8',
    //        header: { "contentType": "application/json" },
    //        data:
    //        {
    //            Correo: Correo
    //        }
    //    }).then(function (datos) {           

    //        if (datos.data.ID_RolCronograma <= 0) {
    //            $window.location.href = urlPathSystem + "/Permiso/Permiso";

    //            return;
    //        }

    //        MostrarMenu(datos.data);

    //        $("#myModalLoader").on('shown.bs.modal', function (e) {
    //            $("#myModalLoader").modal('hide');
    //        });

    //        $('#myModalLoader').modal('hide');

    //    }, function (error) {

    //        $("#myModalLoader").on('shown.bs.modal', function (e) {
    //            $("#myModalLoader").modal('hide');
    //        });

    //        $('#myModalLoader').modal('hide');
    //    });

    function CargarCorreo() {
        $('#myModalLoader').modal('show');

        Correo = $('#CorreoSesion').val();

        $http
            ({
                method: "POST",
                url: urlPathSystem + "/Inicio/ValidUsuario",
                datatype: 'json',
                contentType: 'application/json; charset=utf-8',
                header: { "contentType": "application/json" },
                data:
                {
                    //PPGID: PPGID,
                    //Usuario: Usuario,
                    Correo: Correo
                }
            }).then(function (datos) {

                if (datos.data.OK == 0) {
                    //$window.location.href = urlPathSystem + "/Permiso/Permiso";

                    MessageDanger("Inicio", datos.data.Mensaje);

                    return;
                }

                if (datos.data.ID_RolCronograma <= 0) {
                    $window.location.href = urlPathSystem + "/Permiso/Permiso";

                    return;
                }

                $window.location.href = urlPathSystem + "/Campana/Campana";

                //MostrarMenu(datos.data);

                $("#myModalLoader").on('shown.bs.modal', function (e) {
                    $("#myModalLoader").modal('hide');
                });

                $('#myModalLoader').modal('hide');

            }, function (error) {

                $("#myModalLoader").on('shown.bs.modal', function (e) {
                    $("#myModalLoader").modal('hide');
                });

                $('#myModalLoader').modal('hide');
            });
    }

    function MostrarMenu(varDatos) {
        var menuUsuario = $("#MenuUsuario");
        var menuCronograma = $("#MenuCronograma");
        var menuGrafico = $("#MenuGraficos");
        var menuConfiguracion = $("#MenuConfiguracion");

        if (varDatos.MenuUsuario) {
            menuUsuario.addClass("d-block");
        }
        else {
            menuUsuario.remove();
        }

        if (varDatos.MenuCronograma) {
            menuCronograma.addClass("d-block");
        }
        else {
            menuCronograma.remove();
        }

        if (varDatos.MenuGrafico) {
            menuGrafico.addClass("d-block");
        }
        else {
            menuGrafico.remove();
        }

        if (varDatos.MenuConfiguracion) {
            menuConfiguracion.addClass("d-block");
        }
        else {
            menuConfiguracion.remove();
        }
    }

    function MessageInfo(titulo, message) {
        $.notify({
            // options
            icon: 'fa fa-info-circle fa-lg',
            title: "<span class='title-notify'><strong>" + titulo + "</strong></span><br/>",
            message: "<span class='message-notify'>" + message + "</span><br/>"
        }, {
                // settings
                type: 'info',
                delay: 8000
            });
    }

    function MessageSuccess(titulo, message) {
        $.notify({
            // options
            icon: 'fa fa-check-circle fa-lg',
            title: "<span class='title-notify'><strong>" + titulo + "</strong></span><br/>",
            message: "<span class='message-notify'>" + message + "</span><br/>"
        }, {
                // settings
                type: 'success',
                delay: 8000
            });
    }

    function MessageDanger(titulo, message) {
        $.notify({
            // options
            icon: 'fa fa-window-close fa-lg',
            title: "<span class='title-notify'><strong>" + titulo + "</strong></span><br/>",
            message: "<span class='message-notify'>" + message + "</span><br/>"
        }, {
                // settings
                type: 'danger',
                delay: 8000
            });
    }
});