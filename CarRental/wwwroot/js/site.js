function togglePassword() {
    var passwordInput = document.getElementById("password");
    var eyeIcon = document.getElementById("eyeIcon");
    if (passwordInput.type === "password") {
        passwordInput.type = "text";
        eyeIcon.classList.remove("bi-eye-slash");
        eyeIcon.classList.add("bi-eye");
    } else {
        passwordInput.type = "password";
        eyeIcon.classList.remove("bi-eye");
        eyeIcon.classList.add("bi-eye-slash");
    }
}

// Navbar transparent on scroll
$(function () {
    $(document).scroll(function () {
        var $nav = $("#mainNav");
        $nav.toggleClass('scrolled', $(this).scrollTop() > 50);
    });
});
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    // Load Bookings in Navbar
    function loadBookings() {
        $.get("/Booking/IndexPartial", function (data) {
            $("#sessionBookingsDropdown").html(data);
        });
    }

    // Load on Page Load
    loadBookings();

    // Reload bookings every 30 sec
    setInterval(loadBookings, 30000);
});


