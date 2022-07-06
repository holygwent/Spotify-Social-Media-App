
$(".ratingStar").hover(function () {
    $(".ratingStar").addClass("far").removeClass("fas");

    $(this).addClass("fas").removeClass("far");
    $(this).prevAll(".ratingStar").addClass("fas").removeClass("far");
});



//function SendRate(starValue) {
//    let inputs = document.getElementById("rate").elements;
//    let user = inputs["userEmailRate"].value;
//    let songId = inputs["songIdRate"].value;


//    $.ajax({
//        url: `/Rate/AddRate?songId=${songId}&user=${user}&starValue=${starValue}`,
//        method: "GET",
//        success: function (data) {
          
//            document.getElementById("averageValue").innerHTML = data.averageValue;
//            document.getElementById("NumberOfEvaluators").innerHTML = data.numberOfEvaluators;
//        },
//        error: function (err) {
//            console.log(err)
//        }
//    });

//}
//setInterval(function () {
//    let songId = document.getElementById("SongId").getAttribute("alt");
  
//    let slicedSongId = songId.replace("Song", "");
//    console.log(slicedSongId);
//    $.ajax({
//        url: `/Rate/GetAverageRate?songId=${slicedSongId}`,
//        method: "GET",
//        success: function (data) {
//            console.log(data);
//            document.getElementById("averageValue").innerHTML = data.averageValue;
//            document.getElementById("NumberOfEvaluators").innerHTML = data.numberOfEvaluators;
//        },
//        error: function (err) {
//            console.log(err)
//        }
//    });


//}, 15000);
