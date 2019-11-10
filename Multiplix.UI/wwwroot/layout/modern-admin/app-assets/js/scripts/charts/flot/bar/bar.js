/*=========================================================================================
    File Name: bar.js
    Description: Flot bar chart
    ----------------------------------------------------------------------------------------
    Item Name: Modern Admin - Clean Bootstrap 4 Dashboard HTML Template
   Version: 3.0
    Author: PIXINVENT
    Author URL: http://www.themeforest.net/user/pixinvent
==========================================================================================*/

// Bar chart
// ------------------------------
$(window).on("load", function(){

    var data = [["Janeiro", 10], ["Fevereiro", 8], ["Mar", 4], ["Abril", 13], ["Maio", 17], ["Junho", 9], ["Julho", 4], ["Agosto", 7], ["Setembro", 11], ["Outubro", 14], ["Novembro", 5] ];

    $.plot("#bar-chart", [ data ], {
        series: {
            bars: {
                show: true,
                barWidth: 0.6,
                align: "center",
                lineWidth: 0,
                fill: true,
                fillColor: { colors: [ { opacity: 0.2 }, { opacity: 0.8 } ] }
            }
        },
        xaxis: {
            mode: "categories",
            tickLength: 0
        },
        yaxis: {
            tickSize: 4
        },
        grid: {
            borderWidth: 1,
            borderColor: "transparent",
            color: '#999',
            minBorderMargin: 20,
            labelMargin: 10,
            margin: {
                top: 8,
                bottom: 20,
                left: 20
            },
        },
        colors: ['#5175E0']
    });

});