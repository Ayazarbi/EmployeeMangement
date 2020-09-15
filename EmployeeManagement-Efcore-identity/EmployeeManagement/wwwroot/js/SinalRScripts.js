"use strict";

var count = 0;
var counter = document.getElementById("Counter");
var employeerow = document.getElementById("employeerow");
counter.style.display = "none";
var Notificationmenu = document.querySelector("#Notificationmenu");
var connection = new signalR.HubConnectionBuilder().withUrl("/MyHub").build();
var tr="";
connection.start().then(function () {
    next();
    //loaddata();


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

//function loaddata() {
   
//    $.ajax({
//        url: 'Employee/Getallemployees',
//        method: 'GET',
//        success: (result) => {
//            $.each(result, (k, v) => {
//                console.log(v);
//                buildcard(v);

//            })
//        },
//        error: (error) => {
//            console.log(error)
//        }
//        })

//}

//function buildcard(Employee) {

//     tr =tr+`<tr>
//<td>${Employee.name}</td>
//<td>${Employee.surname}</td>
//<td>${Employee.address}</td>
//<td>${Employee.email}</td>
//<td><a href="Employee/Deleteemployee/${Employee.employeeId}" class="btn btn-danger">Delete</a>
//                    <a href="Employee/Editemployee/${Employee.employeeId}" class="btn btn-primary">Edit</a>
//</tr>`
//employeerow.innerHTML= tr;

   


//}


