"use strict";
(function() {
    var mixedPercent = document.getElementById("mixed-percent");
    var purePercent = document.getElementById("pure-percent");

  
    var data = [{
        values: [mixedPercent.value, purePercent.value],
        labels: ['Mixed Breed', 'Pure Breed'],
        type: 'pie'
    }];

    var layout = {
        height: 400,
        width: 500
    };

    Plotly.newPlot('mixed-pie-chart', data, layout);


})();