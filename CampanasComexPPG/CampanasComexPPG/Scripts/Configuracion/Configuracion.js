var app = angular.module("MyApp", []);

document.addEventListener('DOMContentLoaded', function () {
    document.querySelector('main').className += 'loaded';
});

//funcion inicial para agregar las empresas
app.controller("MyController", function ($scope, $http, $window) {

    var urlPathSystem = "";

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
            url: urlPathSystem + "/Configuracion/GetUsuarioPasswordShareP",
            datatype: 'json',
            contentType: 'application/json; charset=utf-8',
            header: { "contentType": "application/json" }
        }).then(function (datos) {

            if (datos.data.OK == 0) {
                MessageDanger("Usuario y Password Share Point", datos.data.Mensaje);

                return;
            }

            $scope.UsuarioSharePoint = datos.data.UsuarioShareP;

            $scope.PasswordSharePoint = datos.data.PasswordShareP;

            $scope.ConfirmPasswordSharePoint = datos.data.PasswordShareP;

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

    $scope.EditUsuarioPasswordShareP = function (varUsuarioShareP, varPasswordShareP) {

        $('#myModalLoader').modal('show');

        $http
            ({
                method: "POST",
                url: urlPathSystem + "/Configuracion/EditUsuarioPasswordShareP",
                datatype: 'json',
                contentType: 'application/json; charset=utf-8',
                header: { "contentType": "application/json" },
                data:
                {
                    UsuarioShareP: varUsuarioShareP,
                    PasswordShareP: varPasswordShareP
                }
            }).then(function (datos) {

                if (datos.data.OK == 0) {
                    MessageDanger("Editar Usuario y Password Share Point", datos.data.Mensaje);

                    return;
                }
                else if (datos.data.OK == 1)
                {
                    MessageInfo("Editar Usuario y Password Share Point", datos.data.Mensaje);
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

    $scope.ValidarEditUsuarioPasswordShareP = function (varUsuarioShareP, varPasswordShareP, varConfirmPasswordShareP) {
        if (varUsuarioShareP == null || varUsuarioShareP.length < 5 ||
            varPasswordShareP == null || varPasswordShareP.length < 5 ||
            varConfirmPasswordShareP == null || varConfirmPasswordShareP.length < 5 ||
            varPasswordShareP != varConfirmPasswordShareP) {
            return true;
        }
        else {
            return false;
        }
    };

    $scope.ValidarPasswordShareP = function (varPasswordShareP, varConfirmPasswordShareP)
    {
        if (varPasswordShareP == varConfirmPasswordShareP)
        {
            return false;
        }
        else {
            return true;
        }
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