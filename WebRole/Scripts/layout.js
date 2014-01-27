$(function () {
    $(function () {
        $('#top-header').mouseenter(function () {
            $('.header').slideDown('slow');
        });
        $('#body').mouseenter(function () {
            $('.header').slideUp('slow');
        });
    });
});