$(function () {
    $(function () {
        $('#top-border').mouseenter(function () {
            $('#top-header').slideDown('slow');
        });
        $('#body').mouseenter(function () {
            $('#top-header').slideUp('slow');
        });
    });
});