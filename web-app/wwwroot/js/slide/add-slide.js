$(document).ready(function () {
    $(document).on("click", "#slide-frame", function () {
        $('.slide-edit').load(`/Slide/GetById/?id=${$(this).attr('slide-id') }`);
    })

    $(document).on("click", "#add-slide", function () {
        console.log(slidePartialUrl)
        $.ajax({
            type: "POST",
            url: addUrl,
            dataType: "json",
            data: {
                presentationId: presentationId,
            },
            success: function (data) {
                $('.slide-edit').load(`/Slide/GetById/?id=${data.value.id}`);
                $(".slides-pane").load(" .slides-pane", function () {
                    $(this).children().unwrap();
                });

                setTimeout(function () {
                    $(".slides-pane #slide-frame").last().addClass("active");
                }, 500);
            },
        })
    })
});