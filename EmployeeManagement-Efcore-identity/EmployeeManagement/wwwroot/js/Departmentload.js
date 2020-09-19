var Depaermentrow = document.getElementById("Departmentrow")
connection.on("Refresh", () => {
    Departmentrow.innerHTML = "";
    loadDepaertment();
});

loadDepaertment();                                                                 

function loadDepaertment() {
    $.ajax({
        url: 'Department/GetallDepartments',
        method: 'GET',
        success: (result) => {
            $.each(result, (k, v) => {
               
                buildcard(v);

            })
        },
        error: (error) => {
            console.log(error)
        }
    })

}

function buildcard(Department) {

    Depaermentrow.innerHTML += `<div class="card" style="width: 18rem;">
 <img class="card-img-top" src="/Department.png" alt="Card image cap">
  <div class="card-body">
    <h5 class="card-title">${Department.department_name}</h5>
     <p class="card-text"></p>
    <a href="Department/DeleteDepartment/${Department.departmentId}" class="btn btn-danger">Delete</a>
        <a href="Department/EditDepartment/${Department.departmentId}" class="btn btn-primary">Edit</a>
</div >
</div>`

}