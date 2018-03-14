"use strict";
(function () {
    var breedList = [];
    var breedCount = [];
    
    var parentElement = document.getElementById("breed-data");

    var breedData = parentElement.children;

    for (var i = 0; i < breedData.length; i++) {
        breedList.push(breedData[i].id);
        breedCount.push(breedData[i].value);
    }
        
    var breedCountData = [
        {
            x: breedList,
            y: breedCount,
            type: 'bar'
        }
    ]
    

  
    Plotly.newPlot('graph-breed-count', breedCountData);

})();

