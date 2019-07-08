var app = angular.module("MyApp", []);

document.addEventListener('DOMContentLoaded', function () {
    document.querySelector('main').className += 'loaded';
});

//funcion inicial para agregar las empresas
app.controller("MyController", function ($scope, $http, $window) {

    var urlPathSystem = "";

    $('#myModalLoader').modal('show');

    //Inicializar fecha
    var date_input = $(".fecha");
    date_input.datepicker({
        format: 'dd/mm/yyyy',
        todayHighlight: true,
        autoclose: true,
        language: "es"
    });

    //Inicializa cronograma
    $('.tree').treegrid();

    $(document).tooltip({
        tooltipClass: "uitooltip",
        position: {
            my: "left top",
            at: "right+5 top-5"
        }
    });

    //var acordeonRecargarCronograma = $("#accordionRecargarCronograma");
    //acordeonRecargarCronograma.remove();  

    $http
        ({
            method: "POST",
            url: urlPathSystem + "/Campana/GetCampana",
            datatype: 'json',
            contentType: 'application/json; charset=utf-8',
            header: { "contentType": "application/json" }
        }).then(function (datos) {

            if (datos.data.OK == 0) {
                MessageInfo("Recargar Cronograma",datos.data.Mensaje);

                return;
            }

            $scope.ListCampana = datos.data.ListCampana;
            $scope.ListCampanaTemp = datos.data.ListCampanaTemp;

            $scope.ListTipoSell = datos.data.ListTipoSell;
            $scope.ListTipoCampania = datos.data.ListTipoCampania;
            $scope.ListTipoAlcance = datos.data.ListTipoAlcance;
            $scope.ListTipoUrgente = datos.data.ListTipoUrgente;

            $scope.EsEditarCronograma = false;

            MostrarMenu(datos.data);

            $("#myModalLoader").on('shown.bs.modal', function (e) {
                $("#myModalLoader").modal('hide');
            });

            $("#myModalLoader").modal('hide');

        }, function (error) {

            $("#myModalLoader").on('shown.bs.modal', function (e) {
                $("#myModalLoader").modal('hide');
            });

            $("#myModalLoader").modal('hide');
        });

    $scope.GetCronograma = function (Campana) {

        $('#myModalLoader').modal('show');

        $.ajax({
            type: "POST",
            url: urlPathSystem + "/Campana/GetCronograma",
            data: JSON.stringify(Campana),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (datos) {

                if (datos.OK == 0) {
                    MessageDanger("Cronograma", datos.Mensaje);
                }
                else if (datos.OK == 2)
                {
                    MessageInfo("Cronograma", datos.Mensaje);
                }

                $scope.Campana = Campana;

                $scope.ListCronograma = datos.ListCronograma;

                $scope.EstatusGeneral = datos.EstatusGeneral;
                $scope.EstatusAvance = datos.EstatusAvance;

                $scope.PorcentajeGeneralSistema = datos.PorcentajeGeneralSistema;
                $scope.PorcentajeGeneralUsuario = datos.PorcentajeGeneralUsuario;
                $scope.PorcentajeEsfuerzoUsuario = datos.PorcentajeEsfuerzoUsuario;

                $scope.EstatusSemaforoVerde = datos.EstatusSemaforoVerde;
                $scope.EstatusSemaforoAmarillo = datos.EstatusSemaforoAmarillo;
                $scope.EstatusSemaforoRojo = datos.EstatusSemaforoRojo;

                $scope.MinFechaCronograma = datos.MinFechaCronograma;
                $scope.MaxFechaCronograma = datos.MaxFechaCronograma; 

                $scope.RolAdministrador = datos.RolAdministrador;

                $scope.EsEditarCronograma = datos.EsEditarCronograma;

                $scope.Correcto = false;

                $scope.$apply();

                $(".fecha").datepicker({
                    format: 'dd/mm/yyyy',
                    todayHighlight: true,
                    autoclose: true,
                    language: "es",
                    startDate: $scope.MinFechaCronograma,
                    endDate: $scope.MaxFechaCronograma
                });

                $('.tree').treegrid();

                $('#collapseCronograma').collapse('show');

                $("#myModalLoader").on('shown.bs.modal', function (e) {
                    $("#myModalLoader").modal('hide');
                });

                $("#myModalLoader").modal('hide');

            },
            error: function (error) {

                $("#myModalLoader").on('shown.bs.modal', function (e) {
                    $("#myModalLoader").modal('hide');
                });

                $("#myModalLoader").modal('hide');
            }
        });
    };

    $scope.SearchCampana = function (varCamp_Number, varNombre_Camp, varTipoUrgente, varTipoSell, varTipoCampania, varTipoAlcance) {

        $('#myModalLoader').modal('show');

        $http
            ({
                method: "POST",
                url: urlPathSystem + "/Campana/SearchCampana",
                datatype: 'json',
                contentType: 'application/json; charset=utf-8',
                header: { "contentType": "application/json" },
                data:
                {
                    camp_Number: varCamp_Number,
                    nombre_Camp: varNombre_Camp,
                    tipoUrgente: varTipoUrgente,
                    tipoSell: varTipoSell,
                    tipoCampania: varTipoCampania,
                    tipoAlcance: varTipoAlcance
                }
            }).then(function (datos) {

                $scope.ListCampanaTemp = datos.data.ListCampanaTemp;

                if (datos.data.OK == 0) {
                    MessageDanger("Buscar Campaña", datos.data.Mensaje);
                }
                else if (datos.data.OK == 2)
                {
                    MessageInfo("Buscar Campaña", datos.data.Mensaje);
                }

                $('#collapseCampania').collapse('show');

                $("#myModalLoader").on('shown.bs.modal', function (e) {
                    $("#myModalLoader").modal('hide');
                });

                $("#myModalLoader").modal('hide');

            }, function (error) {

                $("#myModalLoader").on('shown.bs.modal', function (e) {
                    $("#myModalLoader").modal('hide');
                });

                $("#myModalLoader").modal('hide');

            });

    };

    $scope.SeleccionarArchivoActividades = function (varfile) {
        $scope.file = null;

        $scope.Correcto = false;

        if (varfile.files[0] != null) {
            $scope.file = varfile.files[0];

            //MessageInfo("Recargar Cronograma", "Se selecciono correctamente el archivo.");
        }
        else {
            MessageInfo("Recargar Cronograma", "No se pudo cargar correctamente el archivo, intentelo de nuevo o consulte al adminstrador de sistemas.");
        }

        $scope.$apply();
    };

    $scope.RecargaCronograma = function () {

        $('#myModalLoader').modal('show');

        var file = $("#myFile").get(0).files;
        var data = new FormData();
        data.append("myFile", file[0]);

        $.ajax({
            type: "POST",
            url: urlPathSystem + "/Campana/RecargaCronograma",
            data: data,
            contentType: false,
            processData: false,
            success: function (datos) {
                if (datos.OK == 1)
                {
                    MessageSuccess("Recargar Cronograma", datos.Mensaje);
                }
                else if (datos.OK == 0) {
                    MessageDanger("Recargar Cronograma", datos.Mensaje);
                }

                $scope.ListCronogramaRecargar = datos.ListCronogramaRecargar;

                $scope.Correcto = datos.Correcto;

                $scope.$apply();

                $(".fecha").datepicker({
                    format: 'dd/mm/yyyy',
                    todayHighlight: true,
                    autoclose: true,
                    language: "es",
                    startDate: $scope.MinFechaCronograma,
                    endDate: $scope.MaxFechaCronograma
                });

                $('.tree').treegrid();



                $("#myModalLoader").on('shown.bs.modal', function (e) {
                    $("#myModalLoader").modal('hide');
                });

                $("#myModalLoader").modal('hide');

            },
            error: function (error) {

                $("#myModalLoader").on('shown.bs.modal', function (e) {
                    $("#myModalLoader").modal('hide');
                });

                $("#myModalLoader").modal('hide');
            }
        });

    };

    $scope.GetDetalleCampana = function (varCampana) {

        $('#myModalLoader').modal('show');

        $http
            ({
                method: "POST",
                url: urlPathSystem + "/Campana/GetDetalleCampana",
                datatype: 'json',
                contentType: 'application/json; charset=utf-8',
                header: { "contentType": "application/json" },
                data:
                {
                    Campana: varCampana
                }
            }).then(function (datos) {

                if (datos.data.OK == 0) {
                    MessageDanger("Detalle Campaña", datos.data.Mensaje);

                    return;
                }

                $scope.ListMecanicaRegalo = datos.data.ListMecanicaRegalo;

                $scope.ListMecanicaMultiplo = datos.data.ListMecanicaMultiplo;

                $scope.ListMecanicaDescuento = datos.data.ListMecanicaDescuento;

                $scope.ListMecanicaVolumen = datos.data.ListMecanicaVolumen;

                $scope.ListMecanicaKit = datos.data.ListMecanicaKit;

                $scope.ListMecanicaCombo = datos.data.ListMecanicaCombo;

                $scope.ListMecanicaTiendas = datos.data.ListMecanicaTiendas;

                $('#myModalLoader').on('show', function () {
                    $(this).find('.modal-body').css({
                        width: 'auto', //probably not needed
                        height: 'auto', //probably not needed 
                        'max-height': '100%'
                    });
                });

                $("#myModalLoader").on('shown.bs.modal', function (e) {
                    $("#myModalLoader").modal('hide');
                });

                $("#myModalLoader").modal('hide');

            }, function (error) {

                $("#myModalLoader").on('shown.bs.modal', function (e) {
                    $("#myModalLoader").modal('hide');
                });

                $("#myModalLoader").modal('hide');

            });
    };

    $scope.ShowEditUsuarioLdap = function (varCronograma) {
        $scope.UsuarioLdap = null;
        $scope.SeleccionUsuarioLdap = false;

        $scope.PPGIDLdap = null;
        $scope.NombreLdap = null;
        $scope.CorreoLdap = null;

        $scope.ListUsuarioLdapTemp = null;

        $scope.TipoUduarioLdap = 1;

        $scope.Cronograma = varCronograma;
    };

    $scope.ShowEditUsuarioLdap_2 = function (varCronograma) {
        $scope.UsuarioLdap = null;
        $scope.SeleccionUsuarioLdap = false;

        $scope.PPGIDLdap = null;
        $scope.NombreLdap = null;
        $scope.CorreoLdap = null;

        $scope.ListUsuarioLdapTemp = null;

        $scope.TipoUduarioLdap = 2;

        $scope.Cronograma = varCronograma;
    };

    $scope.SearchUsuarioLdap = function (varPPGID, varCorreo, varNombre) {

        $('#myModalLoader').modal('show');

        $scope.SeleccionUsuarioLdap = false;

        $http
            ({
                method: "POST",
                url: urlPathSystem + "/Campana/SearchUsuarioLdap",
                datatype: 'json',
                contentType: 'application/json; charset=utf-8',
                header: { "contentType": "application/json" },
                data:
                {
                    PPGID: varPPGID,
                    Correo: varCorreo,
                    Nombre: varNombre
                }
            }).then(function (datos) {

                $scope.ListUsuarioLdapTemp = datos.data.ListUsuarioLdapTemp;

                if (datos.data.OK == 0) {
                    MessageDanger("Buscar Usuario", datos.data.Mensaje);
                }
                else if (datos.data.OK == 2) {
                    MessageInfo("Buscar Usuario", datos.data.Mensaje);
                }

                $("#myModalLoader").on('shown.bs.modal', function (e) {
                    $("#myModalLoader").modal('hide');
                });

                $("#myModalLoader").modal('hide');

            }, function (error) {

                $("#myModalLoader").on('shown.bs.modal', function (e) {
                    $("#myModalLoader").modal('hide');
                });

                $("#myModalLoader").modal('hide');

            });
    };

    $scope.SeleccionarUsuarioLdap = function (varUsuarioLdap) {
        $scope.SeleccionUsuarioLdap = true;
        $scope.UsuarioLdap = varUsuarioLdap;
    };

    $scope.EditUsuarioLdap = function (varUsuarioLdap, varCronograma, varTipoUsuarioLdap)
    {
        //$('#myModalLoader').modal('show');

        if (varTipoUsuarioLdap == 1)
        {
            varCronograma.PPGID = varUsuarioLdap.PPGID;
            varCronograma.NombreResponsable = varUsuarioLdap.Nombre;
            varCronograma.Correo = varUsuarioLdap.Email;
            varCronograma.Update = true;
        }
        else if (varTipoUsuarioLdap == 2)
        {
            varCronograma.PPGID_2 = varUsuarioLdap.PPGID;
            varCronograma.NombreResponsable_2 = varUsuarioLdap.Nombre;
            varCronograma.Correo_2 = varUsuarioLdap.Email;
            varCronograma.Update = true;
        }

        //$("#myModalLoader").on('shown.bs.modal', function (e) {
        //    $("#myModalLoader").modal('hide');
        //});

        //$http
        //    ({
        //        method: "POST",
        //        url: urlPathSystem + "/Campana/EditUsuarioLdap",
        //        datatype: 'json',
        //        contentType: 'application/json; charset=utf-8',
        //        header: { "contentType": "application/json" },
        //        data:
        //        {
        //            UsuarioLdap: varUsuarioLdap,
        //            Cronograma: varCronograma
        //        }
        //    }).then(function (datos) {

        //        $scope.ListUsuarioLdapTemp = datos.data.ListUsuarioLdapTemp;

        //        if (datos.data.OK == 2) {
        //            MessageInfo("Buscar Usuario", datos.data.Mensaje);
        //        }

        //        $("#myModalLoader").on('shown.bs.modal', function (e) {
        //            $("#myModalLoader").modal('hide');
        //        });
        //    }, function (error) {

        //        $("#myModalLoader").on('shown.bs.modal', function (e) {
        //            $("#myModalLoader").modal('hide');
        //        });
        //    });
    };

    $scope.ValidateEditUsuarioLdap = function (varSeleccionUsuarioLdap) {
        if (!varSeleccionUsuarioLdap) {
            return true;
        }
        else {
            return false;
        }
    };

    $scope.EditTextoCronograma = function (varCronograma) {
        varCronograma.Update = true;
    };

    $scope.EditCronograma = function (varListCronograma) {
        $('#myModalLoader').modal('show');

        var varListCronogramaFilter = //varListCronograma.filter(n => n.Update >= 0);// || n.Padre);
            varListCronograma.filter(function (n) {
                return n.Update == true || n.Padre == true;
            });

        $.ajax({
            type: "POST",
            url: urlPathSystem + "/Campana/EditCronograma",
            data: JSON.stringify(varListCronogramaFilter),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (datos) {

                $scope.ListCronograma = datos.ListCronograma;
                $scope.PorcentajeGeneralUsuario = datos.PorcentajeGeneralUsuario;

                $scope.PorcentajeGeneralSistema = datos.PorcentajeGeneralSistema;
                $scope.PorcentajeGeneralUsuario = datos.PorcentajeGeneralUsuario;

                $scope.EstatusGeneral = datos.EstatusGeneral;

                $scope.EstatusSemaforoVerde = datos.EstatusSemaforoVerde;
                $scope.EstatusSemaforoAmarillo = datos.EstatusSemaforoAmarillo;
                $scope.EstatusSemaforoRojo = datos.EstatusSemaforoRojo;

                $scope.MinFechaCronograma = datos.MinFechaCronograma;
                $scope.MaxFechaCronograma = datos.MaxFechaCronograma;

                $scope.$apply();

                $('.tree').treegrid();

                if (datos.OK == 1)
                {
                    MessageSuccess("Actualizar Cronograma", datos.Mensaje);
                }
                else if (datos.OK == 0){
                    MessageDanger("Actualizar Cronograma", datos.Mensaje);
                }

                $("#myModalLoader").on('shown.bs.modal', function (e) {
                    $("#myModalLoader").modal('hide');
                });

                $("#myModalLoader").modal('hide');

            },
            error: function (error) {

                $("#myModalLoader").on('shown.bs.modal', function (e) {
                    $("#myModalLoader").modal('hide');
                });

                $("#myModalLoader").modal('hide');

            }
        });

    };

    $scope.ReSaveCronograma = function () {
        $('#myModalLoader').modal('show');

        $.ajax({
            type: "POST",
            url: urlPathSystem + "/Campana/ReSaveCronograma",
            //data: JSON.stringify(varListCronograma),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (datos) {

                $scope.ListCronograma = datos.ListCronograma;

                $scope.ListCronogramaRecargar = datos.ListCronogramaRecargar;

                $scope.PorcentajeGeneralUsuario = datos.PorcentajeGeneralUsuario;

                $scope.PorcentajeGeneralSistema = datos.PorcentajeGeneralSistema;
                $scope.PorcentajeGeneralUsuario = datos.PorcentajeGeneralUsuario;

                $scope.EstatusGeneral = datos.EstatusGeneral;

                $scope.EstatusSemaforoVerde = datos.EstatusSemaforoVerde;
                $scope.EstatusSemaforoAmarillo = datos.EstatusSemaforoAmarillo;
                $scope.EstatusSemaforoRojo = datos.EstatusSemaforoRojo;

                $scope.MinFechaCronograma = datos.MinFechaCronograma;
                $scope.MaxFechaCronograma = datos.MaxFechaCronograma;

                $scope.$apply();

                $('.tree').treegrid();

                if (datos.OK == 1) {
                    MessageSuccess("Actualizar Cronograma", datos.Mensaje);
                }
                else if (datos.OK == 2) {
                    MessageInfo("Actualizar Cronograma", datos.Mensaje);
                }
                else if(datos.OK == 0){
                    MessageDanger("Actualizar Cronograma", datos.Mensaje);
                }

                $('#collapseCronograma').collapse('show');

                $("#myModalLoader").on('shown.bs.modal', function (e) {
                    $("#myModalLoader").modal('hide');
                });

                $("#myModalLoader").modal('hide');

            },
            error: function (error) {

                $("#myModalLoader").on('shown.bs.modal', function (e) {
                    $("#myModalLoader").modal('hide');
                });

                $("#myModalLoader").modal('hide');

            }
        });
    };

    $scope.MostrarCampana = function (varCampana) {
        if (varCampana == null) {
            return false;
        }

        return true;
    };

    $scope.MostrarPorcentajeGeneralUsuario = function (varPorcentajeGeneralUsuario) {
        if (varPorcentajeGeneralUsuario == null) {
            return false;
        }

        return true;
    };

    $scope.MostrarPorcentajeGeneralSistema = function (varPorcentajeGeneralSistema) {
        if (varPorcentajeGeneralSistema == null) {
            return false;
        }

        return true;
    };

    $scope.MostrarPorcentajeGeneralSisReal = function (varPorcentajeGeneralSisReal) {
        if (varPorcentajeGeneralSisReal == null) {
            return false;
        }

        return true;
    };

    $scope.SoloLecturaPorcentaje = function (varPadre, varConcluida) {
        if (varPadre || varConcluida)
        {
            return true;
        }
        else
        {
            return false;
        }
    };

    $scope.MostrarEstatusGeneral = function (varEstatusGeneral) {
        if (varEstatusGeneral == null) {
            return false;
        }

        return true;
    };

    $scope.MostrarEstatusAvance = function (varEstatusAvance) {
        if (varEstatusAvance == null) {
            return false;
        }

        return true;
    };

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

    function MostrarMenu(varDatos) {
        var menuUsuario = $("#MenuUsuario");
        var menuCronograma = $("#MenuCronograma");
        var menuGrafico = $("#MenuGraficos");
        var menuConfiguracion = $("#MenuConfiguracion")

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
});

app.directive('validDecimal', function () {
    return {
        require: '?ngModel',
        link: function (scope, element, attrs, ngModelCtrl) {
            if (!ngModelCtrl) {
                return;
            }

            ngModelCtrl.$parsers.push(function (val) {
                if (angular.isUndefined(val)) {
                    var val = '';
                }

                var clean = val.replace(/[^-0-9\.]/g, '');
                var negativeCheck = clean.split('-');
                var decimalCheck = clean.split('.');
                if (!angular.isUndefined(negativeCheck[1])) {
                    negativeCheck[1] = negativeCheck[1].slice(0, negativeCheck[1].length);
                    clean = negativeCheck[0] + '-' + negativeCheck[1];
                    if (negativeCheck[0].length > 0) {
                        clean = negativeCheck[0];
                    }

                }

                if (!angular.isUndefined(decimalCheck[1])) {
                    decimalCheck[1] = decimalCheck[1].slice(0, 2);
                    clean = decimalCheck[0] + '.' + decimalCheck[1];
                }

                if (val !== clean) {
                    ngModelCtrl.$setViewValue(clean);
                    ngModelCtrl.$render();
                }
                return clean;
            });

            element.bind('keypress', function (event) {
                if (event.keyCode === 32) {
                    event.preventDefault();
                }
            });
        }
    };
});

app.directive('validNumber', function () {
    return {
        require: '?ngModel',
        link: function (scope, element, attrs, ngModelCtrl) {
            if (!ngModelCtrl) {
                return;
            }

            ngModelCtrl.$parsers.push(function (val) {
                if (angular.isUndefined(val)) {
                    var val = '';
                }

                var clean = val.replace(/[^-0-9]/g, '');
                var negativeCheck = clean.split('-');
                var decimalCheck = clean.split('.');
                if (!angular.isUndefined(negativeCheck[1])) {
                    negativeCheck[1] = negativeCheck[1].slice(0, negativeCheck[1].length);
                    clean = negativeCheck[0] + '-' + negativeCheck[1];
                    if (negativeCheck[0].length > 0) {
                        clean = negativeCheck[0];
                    }

                }

                if (!angular.isUndefined(decimalCheck[1])) {
                    decimalCheck[1] = decimalCheck[1].slice(0, 2);
                    clean = decimalCheck[0] + '.' + decimalCheck[1];
                }

                if (val !== clean) {
                    ngModelCtrl.$setViewValue(clean);
                    ngModelCtrl.$render();
                }
                return clean;
            });

            element.bind('keypress', function (event) {
                if (event.keyCode === 32) {
                    event.preventDefault();
                }
            });
        }
    };
});

app.directive("limitToMax", function () {
    return {
        link: function (scope, element, attributes) {
            element.on("keydown keyup", function (e) {
                if (Number(element.val()) > Number(attributes.max) &&
                    e.keyCode != 46 // delete
                    &&
                    e.keyCode != 8 // backspace
                ) {
                    e.preventDefault();
                    element.val(attributes.max);
                }
            });
        }
    };
});

app.directive("limitToMin", function () {
    return {
        link: function (scope, element, attributes) {
            element.on("keydown keyup", function (e) {
                if (Number(element.val()) < Number(attributes.min) &&
                    e.keyCode != 46 // delete
                    &&
                    e.keyCode != 8 // backspace
                ) {
                    e.preventDefault();
                    element.val(attributes.min);
                }
            });
        }
    };
});

app.directive("deletToCero", function () {
    return {
        link: function (scope, element, attributes) {
            element.on("keydown keyup", function (e) {
                if ((element.val().substring(0, 2) == "01" ||
                    element.val().substring(0, 2) == "02" ||
                    element.val().substring(0, 2) == "03" ||
                    element.val().substring(0, 2) == "04" ||
                    element.val().substring(0, 2) == "05" ||
                    element.val().substring(0, 2) == "06" ||
                    element.val().substring(0, 2) == "07" ||
                    element.val().substring(0, 2) == "08" ||
                    element.val().substring(0, 2) == "09"
                    ) &&
                    e.keyCode != 46 // delete
                    &&
                    e.keyCode != 8 // backspace
                ) {
                    e.preventDefault();
                    element.val(element.val().substring(1, element.val().length));
                }
            });
        }
    };
});