let connection = new signalR.HubConnectionBuilder().withUrl("/Chat/Index").build();

//connect to hub
connection.start().then(() => console.log("Connected"))
    .catch((err) => console.log(err));

//subscribe to an event method
connection.on("ReceivedMessage", (data) => {
    console.log(data);

    let messageEle = document.querySelector("[id=CommentList]");
    let liEle = document.createElement("li");
    liEle.classList.add('comment-item');

   liEle.innerHTML = `                       
                       <div style=\"  line-height:1.7;font-weight:400;\">
                          <h5 style=\" font-size:20px;\">${data.user}</h5>
                           <div style=\"color:#ccc !important;font-size:13px;letter-spacing:.1em;text-transform:uppercase;\">${data.shortDate} ${data.shortTime}</div>
                           <p style=\" margin-top:0;\">${data.message}</p>
                      </div>`;


    messageEle.appendChild(liEle);
 

});

//trigger server method to send message
function send() {
    let username = document.querySelector("[name=AuthorId]").value;
    let message = document.querySelector("[name=msg]").value;
    let group = document.querySelector("[id=SongId]").getAttribute('alt');
    let songId = document.querySelector("[name=songId]").value;

    if (username != "null" && message != "" && songId != "") {
        connection.invoke("SendMessageToGroup", group, username, message,songId).catch((err) => console.log(err));
    }
    message.value = "";
   
};

//trigger server method join user to group
function join(element) {
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
////trigger server method leave user from group
window.onbeforeunload = function () {

    let song = document.querySelector("[id=SongId]").getAttribute('alt');
    connection.invoke("LeaveGroup", song).catch(function (err) {
        return console.error(err.toString());
    });
}
