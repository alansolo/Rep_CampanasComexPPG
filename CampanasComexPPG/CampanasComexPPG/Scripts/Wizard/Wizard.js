var app = angular.module("MyApp", [])

document.addEventListener('DOMContentLoaded', function () {
    document.querySelector('main').className += 'loaded';
});

//funcion inicial para agregar las empresas
app.controller("MyController", function ($scope, $http, $window) {

    var urlPathSystem = "";

    //Paso actual
    var pasoActual = 0;

    //Inicializar fecha
    var date_input = $(".fecha");
    date_input.datepicker({
        format: 'dd/mm/yyyy',
        todayHighlight: true,
        autoclose: true,
        language: "es"
    });

    var d = new Date();

    date_input.datepicker('update', d.getDate() + '/' + d.getMonth() + 1 + '/' + d.getFullYear());

    // Smart Wizard
    $('#smartwizard').smartWizard({
        selected: 0,  // Initial selected step, 0 = first step 
        keyNavigation: true, // Enable/Disable keyboard navigation(left and right keys are used if enabled)
        autoAdjustHeight: true, // Automatically adjust content height
        cycleSteps: false, // Allows to cycle the navigation of steps
        backButtonSupport: true, // Enable the back button support
        useURLhash: true, // Enable selection of the step based on url hash
        lang: {  // Language variables
            next: 'Siguiente',
            previous: 'Anterior'
        },
        toolbarSettings: {
            toolbarPosition: 'bottom', // none, top, bottom, both
            toolbarButtonPosition: 'right', // left, right
            showNextButton: true, // show/hide a Next button
            showPreviousButton: true, // show/hide a Previous button
            toolbarExtraButtons: [
        $('<button id="btnFinalizar"></button>').text('Finalizar')
                      .addClass('btn btn-info d-none')
                      .on('click', function () {
                          alert('Finsih button click');
                      }),
        $('<button id="btnCancelar"></button>').text('Cancelar')
                      .addClass('btn btn-danger')
                      .on('click', function () {
                          alert('Cancel button click');
                      })
            ]
        },
        anchorSettings: {
            anchorClickable: true, // Enable/Disable anchor navigation
            enableAllAnchors: false, // Activates all anchors clickable all times
            markDoneStep: true, // add done css
            enableAnchorOnDoneStep: true // Enable/Disable the done steps navigation
        },
        contentURL: null, // content url, Enables Ajax content loading. can set as data data-content-url on anchor
        disabledSteps: [],    // Array Steps disabled
        errorSteps: [],    // Highlight step with errors
        theme: 'arrows',
        transitionEffect: 'fade', // Effect on navigation, none/slide/fade
        transitionSpeed: '400'
    });

    $(".sw-btn-next").addClass('btn btn-primary');
    $(".sw-btn-prev").addClass('d-none btn btn-primary');


    $scope.ValidarPasoWizard = function () {

        if(pasoActual == 0)
        {

        }
        else if(pasoActual == 1)
        {

        }
        else if(pasoActual == 2)
        {

        }
        else if(pasoActual == 3)
        {

        }
        else if(pasoActual == 4)
        {

        }

    }

    $("#smartwizard").on("showStep", function (e, anchorObject, stepNumber, stepDirection) {
        pasoActual = stepNumber;

        if (pasoActual == 0)
        {
            $(".sw-btn-prev").addClass('d-none');
            $(".sw-btn-prev").removeClass('d-block');           
        }
        else
        {
            $(".sw-btn-prev").addClass('d-block');
            $(".sw-btn-prev").removeClass('d-none');
        }

        if (pasoActual == 4) {
            $("#btnFinalizar").addClass('d-block');
            $("#btnFinalizar").removeClass('d-none');

            $(".sw-btn-next").addClass('d-none');
            $(".sw-btn-next").removeClass('d-block');
        }
        else {
            $("#btnFinalizar").addClass('d-none');
            $("#btnFinalizar").removeClass('d-block');

            $(".sw-btn-next").addClass('d-block');
            $(".sw-btn-next").removeClass('d-none');
        }

    });


    //CARGAR INFORMACION INICIAL    
    $('#myModalLoader').modal('show');

    $http
    ({
        method: "POST",
        url: urlPathSystem + "/Wizard/GetInformacionWizard",
        datatype: 'json',
        contentType: 'application/json; charset=utf-8',
        header: { "contentType": "application/json" }
    }).then(function (datos) {

        $("#myModalLoader").modal('hide');
    }, function (error) {

        $("#myModalLoader").modal('hide');
    });


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