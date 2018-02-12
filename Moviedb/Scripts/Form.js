function submitProducer() {
        $('#modal-producer').modal('toggle');

    $.ajax({
        type: "POST",
            url: "/Producers/CreateProducer",
            data: $("#producer-form").serialize(),
            success: function (response) {
                var x = document.getElementById("producers-drop-down");

                document.myResponse = response;

                var option = document.createElement("option");
                option.text = response.Name;
                option.value = response.Id;
                x.add(option);
            }
        });
    }
function submitActor() {
        $('#modal-actor').modal('toggle');

    $.ajax({
        type: "POST",
            url: "/Actors/CreateActor",
            data: $("#actor-form").serialize(),
            success: function (response) {
        console.log("Success submit actor");
    document.response = response;
                var x = document.getElementsByClassName("actor-dropdown")[0];
                var option = document.createElement("option");
                option.text = response.Name;
                option.value = response.Id;
                x.add(option);

                $('.actor-dropdown').trigger("chosen:updated");
            }

        });

    }