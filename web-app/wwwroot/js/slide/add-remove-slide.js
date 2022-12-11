$(document).ready(function () {
    $("#remove").click(function () {
        $.ajax({
            type: "POST",
            url: removeUrl,
            dataType: "json",
            data: {
                id: slideId,
            },
            success: function () {
                $(".slides-pane").load(" .slides-pane", function () {
                    $(this).children().unwrap();
                });
                $('.slide-actions, .slide-frame').remove();
            },
        })
    })

    $("#add").click(function () {
        $.ajax({
            type: "POST",
            url: addUrl,
            dataType: "json",
            data: {
                title: $(".title").text(),
                text: $(".text").text(),
                presentationId: presentationId,
            },
            success: function () {
                $(".slides-pane").load(" .slides-pane", function () {
                    $(this).children().unwrap();
                });
                $('.slide-actions, .slide-frame').remove();
            },
        })
    })
});
