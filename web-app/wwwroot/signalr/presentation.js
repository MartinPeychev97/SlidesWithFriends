let connection = new signalR.HubConnectionBuilder()
    .withUrl(`/hubs/presentation?presentationId=${presentationId}`)
    .build();

connection.on("UpdateSlide", function (indexh, indexv) {
    Reveal.slide(indexh, indexv);
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