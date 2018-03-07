"use strict";
(function () {
    var colorSection = document.getElementById("color-section");
    var addColorBtn = document.getElementById("add-color-button");
    var colorCount = document.getElementById("color-count");

    var colorTemplate = '<select class="form-control" id="ThisDog.Colors[{{id}}].Name" name="ThisDog.Colors[{{id}}].Name">' +
        ' <option value=""> --select-- </option>' +
        ' <option value="White">White</option>' +
        ' <option value="Black">Black</option>' +
        ' <option value="Tan">Tan</option> ' +
        ' <option value="Brown">Brown</option>' +
        ' <option value="Gray">Gray</option> ' +
        ' </select >';
     //   '<button type="button" id="remove-color-btn-{{id}}" class="btn"> - </button>';


    addColorBtn.addEventListener("click", function () {

        if (colorCount === null || colorCount.value === null) {
            colorCount.value = 0;
        }

        var newColorButton = document.createElement("button");
        newColorButton.type = "button";
        newColorButton.innerText = " - ";
        newColorButton.classList.add("btn");
        newColorButton.id = "remove-color-" + colorCount.value;
        newColorButton.addEventListener("click", function () {
            removeColor(newColorButton);
        });

        var newColorDiv = document.createElement("div");
        newColorDiv.classList.add("form-group");

        newColorDiv.innerHTML = colorTemplate.replace(new RegExp("{{id}}", 'g'), colorCount.value++);
        newColorDiv.appendChild(newColorButton);       

        colorSection.appendChild(newColorDiv);


    });


    var removeColor = function removeColor(colorButton) {
        // alert("remove color, dude");
        var colorParent = colorButton.parentElement;
        var selectColorChild = colorParent.firstElementChild;

        if (selectColorChild.tagName == "SELECT") {
            selectColorChild.value = "";
        }

        colorParent.classList.add("hideColor");
     //   var parentParent = colorParent.parentElement;
     //   parentParent.removeChild(colorParent);

    };

  

})();

