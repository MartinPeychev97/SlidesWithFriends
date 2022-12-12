$(document).ready(function () {
    $(document).on("click", "#slide-frame, #add-slide", function () {
        $.ajax({
            type: 'GET',
            url: slidePartialUrl,
            contentType: 'application/html; charset=utf-8',
            dataType: 'html',
            data: {
                slideId: $(this).attr('slide-id'),
            },
        }).done(function (result) {
            $('.slide-edit').html(result);
        });
    })
});