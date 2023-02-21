let connection = new signalR.HubConnectionBuilder()
    .withUrl("/hubs/presentation")
    .build();

connection.on("UpdateSlide", function (indexh, indexv) {
    Reveal.slide(indexh, indexv);
});

connection.start().then(function () {
    console.log("connected");

    Reveal.on('slidechanged', event => {
        connection.invoke("UpdateSlide", event.indexh, event.indexv)
            .catch(function (err) {
                return console.error(err.toString());
            });
    });
}).catch(function (err) {
    return console.error(err.toString());
});