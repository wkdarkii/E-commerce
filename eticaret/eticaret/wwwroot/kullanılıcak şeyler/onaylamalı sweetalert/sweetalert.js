Swal.fire({
    title: "Do you want to save the changes?",
    showDenyButton: true,
    showCancelButton: true,
    confirmButtonText: "Save",
    denyButtonText: `Don't save`
}).then((result) => {
    /* Read more about isConfirmed, isDenied below */
    if (result.isConfirmed) {
        Swal.fire("Saved!", "", "success");
    } else if (result.isDenied) {
        Swal.fire("Changes are not saved", "", "info");
    }
});





/*body kısmına koyulucak css kısmı=





body {
  font-family: "Open Sans", -apple-system, BlinkMacSystemFont, "Segoe UI", Roboto, Oxygen-Sans, Ubuntu, Cantarell, "Helvetica Neue", Helvetica, Arial, sans-serif; 
}



*/