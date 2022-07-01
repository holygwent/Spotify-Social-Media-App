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
                         <div class="comment-body">
                            <h5 class="commenter-name">${data.user}</h5>
                            <div class="comment-date">${data.shortDate} ${data.shortTime}</div>
                            <p class="comment-message">${data.message}</p>
                            <a alt="${data.commentId}"  onclick="showReplies(this)" href="#${data.commentId}"  class="btn-primary btn">Show replies</a>
                            
                        </div>
                        <ul class="comment-list comment-replies " id="${data.commentId}"  style="margin-left:30px;top:0;">
                         </ul>`;

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

   
}