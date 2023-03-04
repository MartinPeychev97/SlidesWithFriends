let connection = new signalR.HubConnectionBuilder()
    .withUrl(`/hubs/presentation?presentationId=${presentationId}`)
    .build();

connection.on("UpdateSlide", function (indexh, indexv) {
    Reveal.slide(indexh, indexv);
});

connection.on("UpdateHostRating", function (newRating) {
    //alert(newRating);
    for (var i = 1; i <= newRating; i++) {
        $('.star_' + i).css('color', 'yellow');
    }
});



connection.on("DisplayUsers", function (users) {
    console.log(users)
    $("#users-container").empty();
    $.each(users, function (index, user) {
        $("#users-container").append(`
                    <div class="avatar">
                        <img src="${user.image}" />
                        <p>${user.username}</p>
                    </div>
        `);
    });
});

connection.start().then(function () {
    console.log("connected");

    Reveal.on('slidechanged', event => {
        connection.invoke("UpdateSlide", event.indexh, event.indexv)
            .catch(function (err) {
                return console.error(err.toString());
            });
    });

    connection.invoke("Join", username, image);
}).catch(function (err) {
    return console.error(err.toString());
});

window.addEventListener("beforeunload", function () {
    connection.invoke("Leave", username);
});

$('.starRatingEvent').click(function () {
    $.ajax({
        type: "POST",
        url: "/rating/AddRating",
        data: { presentationId: presentationId, rating: $(this).data('starindex') }

    });

    alert('Your message......' + $(this).data('starindex'));
});

/*$("#star_1").click(function () {
    const rating = $("#rating_1").val()
    $.ajax({
        type: "POST",
        url: "/rating/AddRating",
        data: { presentationId: presentationId, rating:rating}
        
    })
    //connection.invoke("AddRating", rating)
})

$("#star_2").click(function () {
    const rating = $("#rating_2").val()
})

$("#star_3").click(function () {
    const rating = $("#rating_3").val()
})

$("#star_4").click(function () {
    const rating = $("#rating_4").val()
})

$("#star_5").click(function () {
    const rating = $("#rating_5").val()
})*/