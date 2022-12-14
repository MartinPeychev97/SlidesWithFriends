$(document).ready(function () {
    $(document).on("click", ".slide-preview", function () {
        $(".slides-pane").find(".active").removeClass("active");
        $(this).addClass("active");
    });
});