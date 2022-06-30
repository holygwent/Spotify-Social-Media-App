let connection = new signalR.HubConnectionBuilder().withUrl("/Chat/Index").build();

//connect to hub
connection.start().then(function () {
    console.log("Connected");
    let group = document.querySelector("[id=SongId]").getAttribute('alt');
    connection.invoke("JoinGroup", group).catch(function (err) {
        return console.error(err.toString());
       
    });
    console.log("joined group " + group);
})
    .catch((err) => console.log(err));



//subscribe to an event method
connection.on("ReceivedMessage", (data) => {
   
    
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
    let message = document.querySelector("[name=msg]");
    let group = document.querySelector("[id=SongId]").getAttribute('alt');
    let songId = document.querySelector("[name=songId]").value;
    let parent = document.querySelector("[name=parent]").value;

    if (username != "null" && message != "" && songId != "") {
        connection.invoke("SendMessageToGroup", group, username, message.value,songId,parent).catch((err) => console.log(err));
    }

    message.value = "";
   
};


////trigger server method leave user from group
window.onbeforeunload = function () {

    let song = document.querySelector("[id=SongId]").getAttribute('alt');
    connection.invoke("LeaveGroup", song).catch(function (err) {
        return console.error(err.toString());
    });
}

function showReplies(element) {
    let parent = element.getAttribute('alt');
    let replies = document.getElementById(`${parent}`);
    if (element.innerHTML == "Show replies") {
        replies.style.display = "block";
        element.innerHTML = "Hide replies"
    }
    else {
        replies.style.display = "none";
        element.innerHTML = "Show replies"
    }

    let spr = document.getElementById(`test`);
    let liEle = document.createElement("p");
    liEle.innerHTML = ` <span class="change"> ${parent} </span>`;

     spr.appendChild(liEle);
}