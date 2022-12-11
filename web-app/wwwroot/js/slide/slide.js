$(document).ready(function () {
    $(document).on("click", ".slide-preview", function () {
        $(".slide-preview").removeClass("active");
        $(this).addClass("active");
    });

    $(document).on("click", ".add-slide", function () {
        $(".slide-preview").removeClass("active");
    })
});