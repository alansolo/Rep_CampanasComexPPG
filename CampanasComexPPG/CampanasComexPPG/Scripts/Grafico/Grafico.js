var app = angular.module("MyApp", []);

document.addEventListener('DOMContentLoaded', function () {
    document.querySelector('main').className += 'loaded';
});

//funcion inicial para agregar las empresas
app.controller("MyController", function ($scope, $http, $window) {

    var urlPathSystem = "";

    google.charts.load("current", { packages: ["corechart"] });
    google.charts.load('current', { packages: ['corechart', 'bar'] });

    $('#myModalLoader').modal('show');

    //$.ajax({
    //    type: "POST",
    //    url: urlPathSystem + "/Grafico/GetCampanaAlcance",
    //    contentType: 'application/json; charset=utf-8',
    //    dataType: 'json',
    //    success: function (data) {

    //        if (data.OK == 0) {
    //            MessageInfo(datos.data.Mensaje);

    //            return;
    //        }

    //        $scope.ListCampanaCrono = data.ListCampanaCrono;
    //        $scope.ListGraficoBar = data.ListGraficoBar;

    //        drawBarChart($scope.ListGraficoBar);

    //        $("#myModalLoader").on('shown.bs.modal', function (e) {
    //            $("#myModalLoader").modal('hide');
    //        });
    //    },
    //    error: function (xhr) {

    //        $("#myModalLoader").on('shown.bs.modal', function (e) {
    //            $("#myModalLoader").modal('hide');
    //        });
    //    }
    //});

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
            url: urlPathSystem + "/Grafico/GetCampanaAlcance",
            datatype: 'json',
            contentType: 'application/json; charset=utf-8',
            header: { "contentType": "application/json" }
        }).then(function (datos) {

            if (datos.data.OK == 0) {
                MessageDanger(datos.data.Mensaje);

                return;
            }
            //else if (datos.data.OK == 2) {
            //    MessageInfo(datos.data.Mensaje);

            //    //return;
            //}

            $scope.ListCampanaCrono = datos.data.ListCampanaCrono;
            $scope.ListGraficoBar = datos.data.ListGraficoBar;

            google.charts.setOnLoadCallback(drawBarChart);

            //drawBarChart($scope.ListGraficoBar);


            $scope.ListCampana = datos.data.ListCampana;
            $scope.ListGraficoPieAlcance = datos.data.ListGraficoPieAlcance;

            google.charts.setOnLoadCallback(drawPieChartAlcance);

            //drawPieChartAlcance($scope.ListGraficoPieAlcance);


            $scope.ListGraficoPieEstatus = datos.data.ListGraficoPieEstatus;

            google.charts.setOnLoadCallback(drawPieChartEstatus);

            //drawPieChartEstatus($scope.ListGraficoPieEstatus);


            $scope.ListGraficoPieEjecucion = datos.data.ListGraficoPieEjecucion;

            google.charts.setOnLoadCallback(drawPieChartEjecucion);

            //drawPieChartEjecucion($scope.ListGraficoPieEjecucion);


            $scope.ListGraficoPieProgreso = datos.data.ListGraficoPieProgreso;

            google.charts.setOnLoadCallback(drawPieChartProgreso);


            MostrarMenu(datos.data);

            $("#myModalLoader").on('shown.bs.modal', function (e) {
                $("#myModalLoader").modal('hide');
            });

            $("#myModalLoader").modal('hide');

            //$("#myModalLoader").modal('hide');
        }, function (error) {

            $("#myModalLoader").on('shown.bs.modal', function (e) {
                $("#myModalLoader").modal('hide');
            });

            $("#myModalLoader").modal('hide');
        });

    function drawBarChart() {

        var ListGraficoBar = $scope.ListGraficoBar;

        var heightChar = ListGraficoBar.length * 24;

        var data = new google.visualization.DataTable();

        data.addColumn('string', 'Campaña');
        data.addColumn('number', '% Real');
        data.addColumn('number', '% Esperado');
        //data.addColumn('number', '% Avance Sistema Real');

        $.each(ListGraficoBar, function (key, value) {

            data.addRow([value.Titulo, value.PorcUsuario, value.PorcSistema]);//, value.PorcSistemaReal]);
        });

        var options = {
            //title: '',
            is3D: true,
            chartArea: { width: '50%', height: '90%' },
            hAxis: {
                viewWindow: {
                    min: 0,
                    max: 100
                },
                title: 'Porcentaje',
                minValue: 0
            },
            vAxis: {
                title: ''
            },
            height: heightChar,
            width: 1100
        };

        var chart = new google.visualization.BarChart(document.getElementById('barchart'));
        chart.draw(data, options);
    }

    function drawPieChartAlcance() {

        var ListGraficoPie = $scope.ListGraficoPieAlcance;

        var data = new google.visualization.DataTable();

        data.addColumn('string', 'Titulo');
        data.addColumn('number', 'Valor');

        $.each(ListGraficoPie, function (key, value) {

            data.addRow([value.Titulo, value.Valor]);
        });

        //var data = google.visualization.arrayToDataTable(JsonListGraficoPie, false);

        var options = {
            //title: '',
            is3D: true,
            pieSliceText: 'value',
            chartArea: { left: "40%", top: 0, width: "100%", height: "100%" },
            height: 390,
            width: 900
        };

        var chart = new google.visualization.PieChart(document.getElementById('piechartAlcance'));

        chart.draw(data, options);
    }

    function drawPieChartEstatus() {

        var ListGraficoPie = $scope.ListGraficoPieEstatus;

        var data = new google.visualization.DataTable();

        data.addColumn('string', 'Titulo');
        data.addColumn('number', 'Valor');

        $.each(ListGraficoPie, function (key, value) {

            data.addRow([value.Titulo, value.Valor]);
        });

        //var data = google.visualization.arrayToDataTable(JsonListGraficoPie, false);

        var options = {
            //title: '',
            is3D: true,
            pieSliceText: 'value',
            chartArea: { left: "40%", top: 0, width: "100%", height: "100%" },
            height: 390,
            width: 900
        };

        var chart = new google.visualization.PieChart(document.getElementById('piechartEstatus'));

        chart.draw(data, options);
    }

    function drawPieChartEjecucion() {

        var ListGraficoPie = $scope.ListGraficoPieEjecucion;

        var data = new google.visualization.DataTable();

        data.addColumn('string', 'Titulo');
        data.addColumn('number', 'Valor');

        $.each(ListGraficoPie, function (key, value) {

            data.addRow([value.Titulo, value.Valor]);
        });

        //var data = google.visualization.arrayToDataTable(JsonListGraficoPie, false);

        var options = {
            //title: '',
            is3D: true,
            pieSliceText: 'value',
            chartArea: { left: "40%", top: 0, width: "100%", height: "100%" },
            height: 390,
            width: 900
        };

        var chart = new google.visualization.PieChart(document.getElementById('piechartEjecucion'));

        chart.draw(data, options);
    }

    function drawPieChartProgreso() {

        var ListGraficoPie = $scope.ListGraficoPieProgreso;

        var data = new google.visualization.DataTable();

        data.addColumn('string', 'Titulo');
        data.addColumn('number', 'Valor');

        $.each(ListGraficoPie, function (key, value) {

            data.addRow([value.Titulo, value.Valor]);
        });

        //var data = google.visualization.arrayToDataTable(JsonListGraficoPie, false);

        var options = {
            //title: '',
            is3D: true,
            pieSliceText: 'value',
            chartArea: { left: "40%", top: 0, width: "100%", height: "100%" },
            height: 390,
            width: 900
        };

        var chart = new google.visualization.PieChart(document.getElementById('piechartProgreso'));

        chart.draw(data, options);
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
