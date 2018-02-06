function deleteMovie(id) {

    var delMovie = confirm("Do you want to delete the movie");

    if (delMovie === true) {
        $.ajax({
            type: "POST",
            url: "/Movies/DeleteAjax",
            data: { id },
            success: function (response) {
                document.MyResponseDelete = response;
                console.log("Success");
                var row = document.getElementById(id);
                row.parentNode.removeChild(row);
            }
        });
    }
}