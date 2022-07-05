﻿
$(".ratingStar").hover(function () {
    $(".ratingStar").addClass("far").removeClass("fas");

    $(this).addClass("fas").removeClass("far");
    $(this).prevAll(".ratingStar").addClass("fas").removeClass("far");
});

$(".ratingStar").click(function () {
    var starValue = $(this).attr("td-value");
    $("#ratingValue").val(starValue);
    SendRate(starValue);
})

function SendRate(starValue) {
    let inputs = document.getElementById("rate").elements;
    let user = inputs["userEmailRate"].value;
    let songId = inputs["songIdRate"].value;


    $.ajax({
        url: `/Rate/AddRate?songId=${songId}&user=${user}&starValue=${starValue}`,
        method: "GET",
        success: function (data) {
          
            document.getElementById("averageValue").innerHTML = data.averageValue;
            document.getElementById("NumberOfEvaluators").innerHTML = data.numberOfEvaluators;
        },
        error: function (err) {
            console.log(err)
        }
    });

}
