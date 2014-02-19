$(function () {
    $(function () {
        $('#top-border').mouseenter(function () {
            $('#top-header').slideDown('slow');
            $('#top-border').removeClass('uparrow');
        });
        $('#body').mouseenter(function () {
            $('#top-header').slideUp('slow');
            $('#top-border').addClass('uparrow');
        });
    });
});