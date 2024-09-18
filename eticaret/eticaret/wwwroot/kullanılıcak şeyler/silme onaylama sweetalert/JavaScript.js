Swal.fire({
    title: "Are you sure?",
    text: "You won't be able to revert this!",
    icon: "warning",
    showCancelButton: true,
    confirmButtonColor: "#3085d6",
    cancelButtonColor: "#d33",
    confirmButtonText: "Yes, delete it!"
}).then((result) => {
    if (result.isConfirmed) {
        Swal.fire({
            title: "Deleted!",
            text: "Your file has been deleted.",
            icon: "success"
        });
    }
});




//body {
//    font - family: "Open Sans", -apple - system, BlinkMacSystemFont, "Segoe UI", Roboto, Oxygen - Sans, Ubuntu, Cantarell, "Helvetica Neue", Helvetica, Arial, sans - serif;
//}