// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

templatesCarousel();
productsCarousel();

function templatesCarousel() {
    Scroll("#carousel-control-next-temp", "#carousel-control-prev-temp", ".carousel-temp");
}

function productsCarousel() {
    Scroll("#carousel-control-next-prod", "#carousel-control-prev-prod", ".carousel-prod");
}

function Scroll(nextCarouselSelector, prevCarouselSelector, carouselInner) {
    var carouselWidth = $(carouselInner)[0].scrollWidth;
    var cardWidth = $(`${carouselInner} .carousel-item`).width();
    var scrollPosition = 0;

    $(nextCarouselSelector).on("click", function () {
        if (scrollPosition < (carouselWidth - cardWidth * 4)) { //check if you can go any further
            scrollPosition += cardWidth;  //update scroll position
            $(carouselInner).animate({ scrollLeft: scrollPosition }, 400); //scroll left
        }
        else {
            scrollPosition = 0;
            $(carouselInner).animate({ scrollLeft: scrollPosition }, 400);
        }

    });

    $(prevCarouselSelector).on("click", function () {
        if (scrollPosition > 0) {
            scrollPosition -= cardWidth;
            $(carouselInner).animate({ scrollLeft: scrollPosition }, 400);
        }
    });
}

function isValid() {
    let inputElemets = [...document.querySelectorAll("form input.validator")];

    let isNotValid = inputElemets.some(x => x.value === '') || inputElemets.some(x => x.value === '0');

    var button = document.getElementById("submitButton");

    if (isNotValid) {
        button.setAttribute('disabled', 'disabled');
    }
    else {
        button.removeAttribute('disabled');
    }
}

function myFunction() {
    var x = document.getElementById("togle");
    if (x.style.display === "none") {
        x.style.display = "block";
    } else {
        x.style.display = "none";
        removeValueFromInputs(x)
    }
}

function removeValueFromInputs(element) {
    [...element.querySelectorAll('input')].forEach(i => i.value = '');
}