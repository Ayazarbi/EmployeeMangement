var employeerow = document.getElementById("employeerow");
var user = document.getElementById("User");
var role = document.getElementById("role");
var dept = document.getElementById("department");

connection.on("Refresh", () => {
    employeerow.innerHTML = "";
    loaddata();
});


loaddata()

function loaddata() {

    $.ajax({
        url: '/Employee/Getallemployees',
        method: 'GET',
        success: (result) => {
            $.each(result, (k, v) => {
                console.log(v);
                buildcard(v);

            })
        },
        error: (error) => {
            console.log(error)
        }
    })

}

function buildcard(Employee) {



    if (role.value == "Admin" || role.value == "HR") {

        employeerow.innerHTML += `<div class="card" style="width: 18rem;">
   <img class="card-img-top" src="/Employee.png" alt="Card image cap">
  <div class="card-body">
    <h5 class="card-title">${Employee.name + " " + Employee.surname}</h5>
    <h6 class="card-subtitle mb-2 text-muted">${Employee.email}</h6>
     <h6 class="card-title mb-2 text-muted">${Employee.department.department_name}</h6>
    <p class="card-text">${Employee.address}</p>
    <div id="buttons+${Employee.employeeId}"></div>
 
    </div >
    </div>`
        document.getElementById(`buttons+${Employee.employeeId}`).innerHTML += `  <a href="Employee/Deleteemployee/${Employee.employeeId}" class="btn btn-danger">Delete</a>
            <a href="Employee/Editemployee/${Employee.employeeId}" class="btn btn-primary">Edit</a>`
    }


    if (role.value == "Employee") {
        
        if (Employee.departmentId == 21879) {

            employeerow.innerHTML += `<div class="card" style="width: 18rem;">
   <img class="card-img-top" src="/Employee.png" alt="Card image cap">
  <div class="card-body">
    <h5 class="card-title">${Employee.name + " " + Employee.surname}</h5>
    <h6 class="card-subtitle mb-2 text-muted">${Employee.email}</h6>
     <h6 class="card-title mb-2 text-muted">${Employee.department.department_name}</h6>
    <p class="card-text">${Employee.address}</p>
    <div id="buttons+${Employee.employeeId}"></div>
    

    </div >
    </div>`

            if (Employee.email == user.value) {

                document.getElementById(`buttons+${Employee.employeeId}`).innerHTML += ` <a href="Employee/Editemployee/${Employee.employeeId}" class="btn btn-primary">Edit</a>`

            }















            //     tr =tr+`<tr>
            //<td>${Employee.name}</td>
            //<td>${Employee.surname}</td>
            //<td>${Employee.address}</td>
            //<td>${Employee.email}</td>
            //<td><a href="Employee/Deleteemployee/${Employee.employeeId}" class="btn btn-danger">Delete</a>
            //                    <a href="Employee/Editemployee/${Employee.employeeId}" class="btn btn-primary">Edit</a>
            //</tr>`
            //employeerow.innerHTML= tr;




        }
    }
}
    