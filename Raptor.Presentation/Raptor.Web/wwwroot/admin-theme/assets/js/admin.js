$(window).scroll(function () {
    if ($(window).scrollTop() >= 50) {
        $('#adminbar').addClass('sticky');
    }
    else {
        $('#adminbar').removeClass('sticky');
    }
});