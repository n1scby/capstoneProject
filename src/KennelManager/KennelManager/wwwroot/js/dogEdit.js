"use strict";
(function () {
    var colorSection = document.getElementById("color-section");
    var addColorBtn = document.getElementById("add-color-button");
    var colorCount = document.getElementById("color-count");

    var colorTemplate = '<select class="form-control" id="ThisDog.Colors[{{id}}]" name="ThisDog.Colors[{{id}}]">' +
        '< option value= "" > --select--</option >' +
        ' <option value="White">White</option>' +
        ' <option value="Black">Black</option>' +
        ' <option value="Tan">Tan</option> ' +
        ' <option value="Brown">Brown</option>' +
        ' <option value="Gray">Gray</option> ' +
        ' </select >';


    addColorBtn.addEventListener("click", function () {

        if (colorCount === null || colorCount.value === null) {
            colorCount.value = 0;
        }

        var newColorDiv = document.createElement("div");
        newColorDiv.classList.add("form-group");

        newColorDiv.innerHTML = colorTemplate.replace(new RegExp("{{id}}", 'g'), colorCount.value++);

        colorSection.appendChild(newColorDiv);


    });

})();