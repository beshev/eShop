// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


dashboardCounter();

function isValid() {
    let inputElemets = [...document.querySelectorAll("form input.validator")];

    let isNotValid = inputElemets.some(x => x.value === '');

    var button = document.getElementById("submitButton");

    if (isNotValid) {
        button.setAttribute('disabled', 'disabled');
    }
    else {
        button.removeAttribute('disabled');
    }
}

function dashboardCounter() {
    $(document).ready(function () {

        $('.counter').each(function () {
            $(this).prop('Counter', 0).animate({
                Counter: $(this).text()
            }, {
                duration: 4000,
                easing: 'swing',
                step: function (now) {
                    $(this).text(Math.ceil(now));
                }
            });
        });
    });
}