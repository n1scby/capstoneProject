"use strict";
(function () {
    var colorSection = document.getElementById("color-section");
    var addColorBtn = document.getElementById("add-color-button");
    var colorCount = document.getElementById("color-count");
    var imageCount = document.getElementById("image-count");
    var mixedCheckBox = document.getElementById("ThisDog_MixedBreed");
    var secondaryBreedSelect = document.getElementById("ThisDog_SecondaryBreed");
    
   
    

    var colorTemplate = '<select class="form-control color-select" id="ThisDog.Colors[{{id}}].Name" name="ThisDog.Colors[{{id}}].Name">' +
        ' <option value=""> --select-- </option>' +
        ' <option value="White">White</option>' +
        ' <option value="Black">Black</option>' +
        ' <option value="Tan">Tan</option> ' +
        ' <option value="Brown">Brown</option>' +
        ' <option value="Gray">Gray</option> ' +
        ' </select >';
     //   '<button type="button" id="remove-color-btn-{{id}}" class="btn"> - </button>';


    mixedCheckBox.addEventListener("click", function () {
        if (mixedCheckBox.checked) {
            secondaryBreedSelect.disabled = false;
        } else {
            secondaryBreedSelect.disabled = true;
            secondaryBreedSelect.value = "";
        }
    })

    addColorBtn.addEventListener("click", function () {

        if (colorCount === null || colorCount.value === null) {
            colorCount.value = 0;
        }

        var newColorButton = document.createElement("button");
        newColorButton.type = "button";
        newColorButton.innerText = " - ";
        newColorButton.classList.add("btn");
        newColorButton.classList.add("btn-primary");
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

        if (selectColorChild.tagName === "SELECT") {
            selectColorChild.value = "";
        }

        colorParent.classList.add("hideSection");
     //   var parentParent = colorParent.parentElement;
     //   parentParent.removeChild(colorParent);

    };


    var initialColorBtn = function initialColorBtn(i) {

   //     for (var i = 1; i < colorCount.value; i++) {
            var newClrBtn = document.getElementById("colorBtn-" + i);
            newClrBtn.addEventListener("click", function () {
                removeColor(newClrBtn);
            });
    //    }

    };

    var removeImage = function removeImage(imageButton) {
        var imageParent = imageButton.parentElement;
        var inputChild = imageParent.firstElementChild;

        if (inputChild.tagName === "INPUT") {
            inputChild.value = "";
        }

        imageParent.classList.add("hideSection");
     

    };

    var initialImageBtn = function initialImageBtn(i) {

        var newImgBtn = document.getElementById("imageBtn-" + i);
        newImgBtn.addEventListener("click", function () {
            removeImage(newImgBtn);
        });

    };

    // Preview Image on Upload
    document.getElementById("file-image").onchange = function () {
        var reader = new FileReader();

        reader.onload = function (e) {
            // get loaded data and render thumbnail.
            document.getElementById("upload-image").src = e.target.result;
        };

        // read the image file as a data URL.
        reader.readAsDataURL(this.files[0]);
    };

    var initialCheckBox = function initialCheckBox() {
        if (!mixedCheckBox.checked) {
            secondaryBreedSelect.disabled = true;
            secondaryBreedSelect.value = "";
        }
    }



    for (var i = 1; i < colorCount.value; i++){
        initialColorBtn(i);
    }

    for (var i = 0; i < imageCount.value; i++) {
        initialImageBtn(i);
    }

    initialCheckBox();
    

})();

