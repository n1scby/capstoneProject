"use strict";
(function () {
    var countDetailsBtn = document.getElementById("count-details-button");
    var breedCountTable = document.getElementById("breed-count-table");
    var showDetails = false;

    var breedList = [];
    var breedCount = [];
    
    var parentElement = document.getElementById("breed-data");

    var breedData = parentElement.children;

    for (var i = 0; i < breedData.length; i++) {
        breedList.push(breedData[i].id);
        breedCount.push(breedData[i].value);
    }

    var d3 = Plotly.d3;

    var WIDTH_IN_PERCENT_OF_PARENT = 60,
        HEIGHT_IN_PERCENT_OF_PARENT = 80;

    var gd3 = d3.select('section')
        .append('div')
        .style({
            width: WIDTH_IN_PERCENT_OF_PARENT + '%',
            'margin-left': (100 - WIDTH_IN_PERCENT_OF_PARENT) / 2 + '%',

            height: HEIGHT_IN_PERCENT_OF_PARENT + 'vh',
            'margin-top': (100 - HEIGHT_IN_PERCENT_OF_PARENT) / 2 + 'vh'
        });

    var gd = gd3.node();

    Plotly.plot(gd, [{
        type: 'bar',
        x: breedList,
        y: breedCount,
        marker: {
            color: '#2c85c9',
            line: {
                width: 2.5
            }
        }
    }], {
            title: 'Breed Counts in Shelter',
            font: {
                size: 12
            }
           
        });

    window.onresize = function () {
        Plotly.Plots.resize(gd);
    };


    countDetailsBtn.addEventListener("click", function () {
        if (showDetails) {
            breedCountTable.classList.add("hideSection");
            countDetailsBtn.innerText = "Show Details";
            showDetails = false;
        } else {
            breedCountTable.classList.remove("hideSection");
            countDetailsBtn.innerText = "Hide Details";
            showDetails = true;
        }
    })

})();

