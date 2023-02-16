let connection = new signalR.HubConnectionBuilder()
    .withUrl(`/hubs/presentation?presentationId=${presentationId}`)
    .build();

connection.on("UpdateSlide", function (indexh, indexv) {
    Reveal.slide(indexh, indexv);
});

connection.on("UserJoined", function (users) {
    $("#users-container").empty();
    $.each(users, function (user, image) {
        console.log(user)
        console.log(image)
        $("#users-container").append(`
                    <div class="avatar">
                        <img src="${image}" />
                        <p>${user}</p>
                    </div>
`);
    });
});

connection.on("UserLeft", function (users) {
    $("#users-container").empty();
    $.each(users, function (user, image) {
        console.log(user)
        console.log(image)
        $("#users-container").append(`
                    <div class="avatar">
                        <img src="${image}" />
                        <p>${user}</p>
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