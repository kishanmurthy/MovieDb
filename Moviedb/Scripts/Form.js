function SubmitProducer() {
        $('#MyModalProducer').modal('toggle');

    $.ajax({
        type: "POST",
            url: "/Producers/CreateProducer",
            data: $("#ProducerForm").serialize(),
            success: function (response) {
                var x = document.getElementById("ProducersDropDown");

                document.myResponse = response;

                var option = document.createElement("option");
                option.text = response.Name;
                option.value = response.Id;
                x.add(option);
            }
        });
    }
function SubmitActor() {
        $('#MyModalActor').modal('toggle');

    $.ajax({
        type: "POST",
            url: "/Actors/CreateActor",
            data: $("#ActorForm").serialize(),
            success: function (response) {
        console.log("Success submit actor");
    document.response = response;
                var x = document.getElementsByClassName("actor_dropdown")[0];
                var option = document.createElement("option");
                option.text = response.Name;
                option.value = response.Id;
                x.add(option);

                $('.actor_dropdown').trigger("chosen:updated");
            }

        });

    }