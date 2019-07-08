var app = angular.module("MyApp", []);

document.addEventListener('DOMContentLoaded', function () {
    document.querySelector('main').className += 'loaded';
});

//funcion inicial para agregar las empresas
app.controller("MyController", function ($scope, $http, $window) {

    var urlPathSystem = "";

    //CARGAR INFORMACION INICIAL    
    $('#myModalLoader').modal('show');

    $(document).tooltip({
        tooltipClass: "uitooltip",
        position: {
            my: "left top",
            at: "right+5 top-5"
        }
    });

    $http
        ({
            method: "POST",
            url: urlPathSystem + "/Usuario/GetUsuario",
            datatype: 'json',
            contentType: 'application/json; charset=utf-8',
            header: { "contentType": "application/json" }
        }).then(function (datos) {

            if (datos.data.OK == 0) {
                MessageDanger("Usuario", datos.data.Mensaje);
            }

            $scope.ListUsuario = datos.data.ListUsuario;

            $scope.ListUsuarioTemp = datos.data.ListUsuario;

            $scope.ListRolCronograma = datos.data.ListRolCronograma;

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

    $scope.SearchUsuario = function (varPPGID, varCorreo, varUsuario, varRolCronograma) {

        $('#myModalLoader').modal('show');

        $http
            ({
                method: "POST",
                url: urlPathSystem + "/Usuario/SearchUsuario",
                datatype: 'json',
                contentType: 'application/json; charset=utf-8',
                header: { "contentType": "application/json" },
                data:
                {
                    PPGID: varPPGID,
                    Correo: varCorreo,
                    Usuario: varUsuario,
                    RolCronograma: varRolCronograma
                }
            }).then(function (datos) {

                $scope.ListUsuarioTemp = datos.data.ListUsuarioTemp;

                if (datos.data.OK == 0)
                {
                    MessageDanger("Buscar Usuario", datos.data.Mensaje);
                }
                else if (datos.data.OK == 2) {
                    MessageInfo("Buscar Usuario", datos.data.Mensaje);
                }

                $('#collapseUsuario').collapse('show');

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

    $scope.ShowAddUsuario = function () {
        $scope.UsuarioAdd = null;

        $scope.PPGIDLdap = null;
        $scope.NombreLdap = null;
        $scope.CorreoLdap = null;

        $scope.RolCronogramaAdd = null;

        //$scope.ListUsuarioTemp = null;

        $scope.TitleModal = "Agregar Usuario";

        $scope.CssDivEditModalButton = false;
        $scope.CssDivSaveModalButton = true;

        $scope.ReadOnlyCampos = false;
        $scope.SeleccionUsuarioLdap = false;

        $scope.ShowTablaUsuario = true;
        $scope.ModalHeigth = "modal-heigth-add";
    };

    $scope.AddUsuario = function (varUsuarioLdap, varRolCronograma) {

        $('#myModalLoader').modal('show');

        $http
            ({
                method: "POST",
                url: urlPathSystem + "/Usuario/AddUsuario",
                datatype: 'json',
                contentType: 'application/json; charset=utf-8',
                header: { "contentType": "application/json" },
                data:
                {
                    UsuarioLdap: varUsuarioLdap,
                    Rol: varRolCronograma
                }
            }).then(function (datos) {

                $scope.ListUsuarioTemp = datos.data.ListUsuarioTemp;

                if (datos.data.OK == 1) {
                    MessageSuccess("Agregar Usuario", datos.data.Mensaje);
                }
                else if (datos.data.OK == 0)
                {
                    MessageDanger("Agregar Usuario", datos.data.Mensaje);
                }
                else if (datos.data.OK == 2)
                {
                    MessageInfo("Agregar Usuario", datos.data.Mensaje);
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

    $scope.ShowEditUsuario = function (varUsuario, varRolCronograma) {
        $scope.UsuarioAdd = varUsuario;

        $scope.PPGIDLdap = varUsuario.PPGID;
        $scope.NombreLdap = varUsuario.Nombre;
        $scope.CorreoLdap = varUsuario.Correo;

        $scope.RolCronogramaAdd = varRolCronograma;
        $scope.RolCronogramaAdd.ID = varUsuario.ID_RolCronograma;
        $scope.RolCronogramaAdd.Rol = varUsuario.Rol;

        $scope.TitleModal = "Editar Usuario";

        $scope.CssDivEditModalButton = true;
        $scope.CssDivSaveModalButton = false;

        $scope.ReadOnlyCampos = true;

        $scope.ShowTablaUsuario = false;
        $scope.ModalHeigth = "modal-heigth-edit";
    };

    $scope.EditUsuario = function (varUsuario, varRolCronograma) {

        $('#myModalLoader').modal('show');

        $http
            ({
                method: "POST",
                url: urlPathSystem + "/Usuario/EditUsuario",
                datatype: 'json',
                contentType: 'application/json; charset=utf-8',
                header: { "contentType": "application/json" },
                data:
                {
                    Usuario: varUsuario,
                    Rol: varRolCronograma
                }
            }).then(function (datos) {

                if (datos.data.OK == 1) {

                    $scope.ListUsuario = datos.data.ListUsuario;

                    $scope.ListUsuarioTemp = datos.data.ListUsuario;

                    MessageSuccess("Editar Usuario", datos.data.Mensaje);
                }
                else if (datos.data.OK == 0) {
                    MessageDanger("Editar Usuario", datos.data.Mensaje);
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

    $scope.ShowRemoveUsuario = function (varUsuario) {
        $scope.UsuarioAdd = varUsuario;

        $scope.TitleModal = "Eliminar Usuario";
    };

    $scope.RemoveUsuario = function (varUsuario) {

        $('#myModalLoader').modal('show');

        $http
            ({
                method: "POST",
                url: urlPathSystem + "/Usuario/RemoveUsuario",
                datatype: 'json',
                contentType: 'application/json; charset=utf-8',
                header: { "contentType": "application/json" },
                data:
                {
                    Usuario: varUsuario
                }
            }).then(function (datos) {

                $scope.ListUsuarioTemp = datos.data.ListUsuarioTemp;

                if (datos.data.OK == 0) {
                    MessageDanger("Eliminar Usuario", datos.data.Mensaje);
                }
                else if (datos.data.OK == 1) {
                    MessageSuccess("Eliminar Usuario", datos.data.Mensaje);
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

    $scope.ValidateSaveUsuario = function (varRolCronograma, varSeleccionUsuarioLdap) {
        if (
            (varRolCronograma == null || varRolCronograma.ID == null || varRolCronograma.ID <= 0) ||
            !varSeleccionUsuarioLdap

           )
        {
            return true;
        }
        else {
            return false;
        }
    };

    $scope.ValidateEditUsuario = function (varRolCronograma) {
        if (varRolCronograma == null || varRolCronograma.ID == null || varRolCronograma.ID <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    };

    $scope.SearchUsuarioLdap = function (varPPGID, varCorreo, varNombre) {

        $('#myModalLoader').modal('show');

        $scope.SeleccionUsuarioLdap = false;

        $http
            ({
                method: "POST",
                url: urlPathSystem + "/Usuario/SearchUsuarioLdap",
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

    $scope.ValidateSearchUsuarioLdap = function (varPPGID, varCorreo, varNombre) {
        if (
            (varPPGID == null || varPPGID == "") &&
            (varCorreo == null || varCorreo == "") &&
            (varNombre == null || varNombre == "")
        )
        {
            return true;
        }
        else {
            return false;
        }
    };

    $scope.SeleccionarUsuarioLdap = function (varUsuarioLdap)
    {
        $scope.SeleccionUsuarioLdap = true;
        $scope.UsuarioLdap = varUsuarioLdap;
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