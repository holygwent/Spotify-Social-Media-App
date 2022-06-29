let connection = new signalR.HubConnectionBuilder().withUrl("/Chat/Index").build();

//connect to hub
connection.start().then(() => console.log("Connected"))
    .catch((err) => console.log(err));

//subscribe to an event method
connection.on("ReceivedMessage", (data) => {
    console.log(data);
    var messageEle = document.querySelector("[class=messages]");
    var liEle = document.createElement("li");

    liEle.innerHTML = `<b>Username: ${data.user} </b> ${data.message}`;
    messageEle.appendChild(liEle);
    //use data;

});

//trigger server method
function send() {
    let username = document.querySelector("[name=username]").value;
    let message = document.querySelector("[name=msg]").value;
    let song = document.querySelector("[id=SongId]").getAttribute('alt');
    if (username != "" && message != "") {
        connection.invoke("SendMessageToGroup", song, username, message).catch((err) => console.log(err));
    }
};

//przy przyciśnięciu zobacz komentarze doda do grupy
function join(element) {
    console.log("join element:" + element.getAttribute('alt'))
    connection.invoke("JoinGroup", element.getAttribute('alt')).catch(function (err) {
        return console.error(err.toString());
    });
    element.visibility = "hidden";
    element.style.display = "none";
    var header = document.getElementById('CommentsHeader');
    var list = document.getElementById('CommentList');
    var commentSong = document.getElementById('CommentSong');
    header.style.visibility = "visible";
    list.style.visibility = "visible";
    commentSong.style.visibility = "visible";
};

window.onbeforeunload = function () {

    let song = document.querySelector("[id=SongId]").getAttribute('alt');
    console.log('leave'+song);
    connection.invoke("LeaveGroup", song).catch(function (err) {
        return console.error(err.toString());
    });
}
