﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

removeShowClass();
buttonSpinner();
formSubmitSpinner();
templatesCarousel();
productsCarousel();

function formSubmitSpinner() {
    let forms = [...document.querySelectorAll('.spinner-form')];
    forms.forEach(form => {
        form.onsubmit = (e) => {
            e.preventDefault();
            addSpinnerToElement(form);
            form.submit();
        }
    })
}

function buttonSpinner() {
    let elements = [...document.querySelectorAll('.spinner-element')];
    elements.forEach(element => {
        element.onclick = () => {
            addSpinnerToElement(element);
            return true;
        }
    })
}

function addSpinnerToElement(element) {
    let divElement = document.createElement('div');
    divElement.classList.add('center-element');
    divElement.style.margin = '10px';

    let divChildElement = document.createElement('div');
    divChildElement.classList.add('loader');

    divElement.append(divChildElement);
    element.querySelector('.form-button').replaceWith(divElement);
}

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
    let fontStyleInput = document.querySelector('.font-input');

    if (fontStyleInput) {
        let number = parseInt(fontStyleInput.value);
        var isNumberValid = number > 0 && number <= 13;
    }

    let isNotValid =
        inputElemets.some(x => x.value === '')
        || inputElemets.some(x => x.value === '0')
        || isNumberValid == false;

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

function removeShowClass() {
    document
        .querySelector('#inner-body')
        .addEventListener('click', function () {
            let element = document.querySelector('.show');
            if (element) {
                console.log(element);
                element.classList.remove('show');
            }
        });
}