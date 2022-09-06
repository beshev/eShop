// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function isValid() {
    let inputElemets = [...document.querySelectorAll("form input")];
    let isNotValid = inputElemets.some(x => x.value === '')

    var button = document.getElementById("submitButton");

    if (isNotValid) {
        button.setAttribute('disabled', 'disabled');
    }
    else {
        button.removeAttribute('disabled');
    }
}