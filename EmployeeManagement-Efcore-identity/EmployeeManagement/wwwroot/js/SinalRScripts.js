"use strict";

var count = 0;
var counter = document.getElementById("Counter");
counter.style.display = "none";
var Notificationmenu = document.querySelector("#Notificationmenu");
var connection = new signalR.HubConnectionBuilder().withUrl("/MyHub").build();
connection.start().then(function () {
    next();


}).catch(function (err) {
    return console.error(err.toString());
});


function next() {
    //connection.invoke("Sendnotification", "message").catch(function (err) {
    //    return console.error(err.toString());
    //});



    connection.on("Receive", function (message) {

        Notificationmenu.appendChild(Createmenuitem(message));
        count += 1;
        counter.textContent = count;
        counter.style.display = "inline";
    });
}

function Createmenuitem(message){

    let li = document.createElement("li");
    li.textContent = message;
    return li;

}