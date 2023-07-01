function getEmployeeByManagerId(managerId) {
    $.ajax({
        async: true,
        url: `https://localhost:7046/api/Employee/GetEmployeeByManagerId?managerId=${managerId}`,
        method: 'GET',

        success: function (data) {
            // Handle the successful response here
            console.log(JSON.parse(data));
        },
        error: function (error) {
            // Handle the error here
            console.log(error);
        }
    });
}

