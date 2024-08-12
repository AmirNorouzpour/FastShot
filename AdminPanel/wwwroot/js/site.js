// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.


function changeDisplay(element) {
    var otherpopup = ''
    if (element.id === 'profilePopup') {
        otherpopup = document.getElementById("notificationPopup");
    }
    if (element.id === 'notificationPopup') {
        otherpopup = document.getElementById("profilePopup");
    }
    if (element.style.display == "none" || element.style.display == '') {
        element.style.display = "block";
        otherpopup.style.display = "none";
    } else {
        element.style.display = "none";
    }
}


