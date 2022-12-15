$(document).ready(function () {
    $("#remove-slide").click(function () {
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
                var lastElId = $(".slides-pane #slide-frame").eq(-2).attr("slide-id")
                $('.slide-edit').load(`/Slide/GetById/?id=${lastElId}`);
                setTimeout(function () {
                    $(".slides-pane #slide-frame").eq(-1).addClass("active");
                }, 500);
            },
        })
    })

    $(document).on("focusout", "#slide-title, #slide-text", function () {
        var h1 = $("#slide-title")
        var p = $("#slide-text")

        if (h1.text() !== slideTitle || p.text() !== slideText) {
            $.ajax({
                type: "POST",
                url: editUrl,
                dataType: "json",
                data: {
                    id: slideId,
                    title: h1.text(),
                    text: p.text(),
                },
                success: function (data) {
                    $(".slides-pane").load(" .slides-pane", function () {
                        $(this).children().unwrap();
                    });

                    setTimeout(function () {
                        $(`[slide-id=${slideId}]`).addClass("active");
                    }, 500);
                }
            })
        }
    })
});
