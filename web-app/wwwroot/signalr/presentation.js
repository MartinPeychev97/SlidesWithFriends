﻿let connection = new signalR.HubConnectionBuilder()
    .withUrl(`/hubs/presentation?presentationId=${presentationId}`)
    .build();

connection.on("UpdateSlide", function (indexh, indexv) {
    Reveal.slide(indexh, indexv);
});

connection.on("DisplayUsers", function (users) {
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

connection.on("React", function (username, reaction) {
    reactionsListEl.classList.remove("show");
    const img = document.createElement("img");
    const userName = document.createElement("p");
    const div = document.createElement("div")

    img.src = reaction;
    userName.innerText = username;

    div.appendChild(img);
    div.appendChild(userName);
    reactionsEl.appendChild(div);

    setTimeout(() => {
        reactionsEl.removeChild(div);
    }, 5000);
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

const reactionsBtn = document.getElementById("reactions-btn");
const reactionsListEl = document.getElementById("reactions-list");
const reactionsEl = document.getElementById("reactions");
const like = document.getElementById("like");
const wave = document.getElementById("wave");
const love = document.getElementById("love");
const clap = document.getElementById("clap");
const laughter = document.getElementById("laughter");

const reactions = {
    like: "/images/reactions/like.png",
    wave: "/images/reactions/wave.png",
    love: "/images/reactions/love.png",
    clap: "/images/reactions/clap.png",
    laughter: "/images/reactions/laughter.png",
};

reactionsBtn.addEventListener("click", () => {
    reactionsListEl.classList.toggle("show");
});

like.addEventListener("click", () => {
    connection.invoke("React", username, reactions.like);
});

wave.addEventListener("click", () => {
    connection.invoke("React", username, reactions.wave);
});

love.addEventListener("click", () => {
    connection.invoke("React", username, reactions.love);
});

clap.addEventListener("click", () => {
    connection.invoke("React", username, reactions.clap);
});

laughter.addEventListener("click", () => {
    connection.invoke("React", username, reactions.laughter);
});

//Test
const submitBtn = document.getElementById("answer-btn");

submitBtn.addEventListener("click", () => {
    var url = window.location.href;
    var answer = document.getElementById("submit_answer").value;
    connection.invoke("Submit", answer, url);
});

connection.on("Update", function () {
    window.location.reload
});