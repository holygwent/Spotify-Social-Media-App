let connection = new signalR.HubConnectionBuilder().withUrl("/Chat/Index").build();


$(".ratingStar").click(function () {
    var starValue = $(this).attr("td-value");
    $("#ratingValue").val(starValue);
    SendRateToGroup(starValue);

})
//connect to hub
connection.start().then(function () {

    let group = document.querySelector("[id=SongId]").getAttribute('alt');
    connection.invoke("JoinGroup", group).catch(function (err) {
        return console.error(err.toString());
    });
    console.log("joined group " + group);

})
    .catch((err) => console.log(err));

connection.on("ReceivedMessage", (data) => {


    let commentList = document.querySelector("[id=CommentList]");
    let liEle = document.createElement("li");
    liEle.classList.add('comment-item');
    liEle.innerHTML = `
                         <div class="comment-body">
                            <h5 class="commenter-name">${data.user}</h5>
                            <div class="comment-date">${data.shortDate} ${data.shortTime}</div>
                            <p class="comment-message">${data.message}</p>
                            <b alt="${data.commentId}"  onclick="showReplies(this)"   class="btn-primary btn">Show replies</b>
                        </div>
                        <ul class="comment-list comment-replies " id="${data.commentId}"  style="margin-left:30px;top:0;">
                            <form id="replyForm${data.commentId}">
                            <input type="hidden"  name="ReplyAuthorId" value="">
                            <input type="hidden" name="ReplySongId" value="${data.songId}">
                            <input type="hidden" name="ReplyParent" value="${data.commentId}">
                            <div class="form-group">
                                 <h6>Comment</h6>
                                 <textarea  name="ReplyMessage"></textarea>
                             </div>
                                <b alt="replyForm${data.commentId}"  class="btn btn-primary" onclick="sendReply(this)">Send</b>
                             </form>
                         </ul>
`;

    commentList.appendChild(liEle);
});

connection.on("ReceiveAverageRate", (data) => {
    document.getElementById("averageValue").innerHTML = data.averageValue;
    document.getElementById("NumberOfEvaluators").innerHTML = data.numberOfEvaluators;
});
connection.on("ReceivedReply", (data) => {
    let replyList = document.getElementById(data.parent);
    let liEle = document.createElement("li");
    liEle.classList.add('comment');
    liEle.innerHTML = `
                                <div class="comment-body">
                                     <h5 class="commenter-name">${data.user}</h5>
                                    <div class="comment-date">${data.shortDate} ${data.shortTime}</div>
                                     <p class="comment-message">${data.message}</p>
                                </div>`;
    replyList.insertBefore(liEle, replyList.children[replyList.children.length - 1]);

});

connection.on("ReceiveNotify", (data) => {
    let group = document.querySelector("[id=SongId]").getAttribute('alt');
    if (group != data.group) {
        toastr.info(`<a href="https://localhost:7115/Song/${data.songId}">See</a>`, data.communicat);
    }
});
//trigger server method to send message
function send() {
    let inputs = document.getElementById("commentForm").elements;
    let username = inputs["AuthorEmail"].value;
    let message = inputs["msg"];
    let group = document.querySelector("[id=SongId]").getAttribute('alt');
    let songId = inputs["songId"].value;
    let parent = inputs["parent"].value;
    if (username != "null" && message != "" && songId != "") {
        connection.invoke("SendMessageToGroup", group, username, message.value, songId, parent).catch((err) => console.log(err));
    }
    message.value = "";
};

function sendReply(element) {
    let replyForm = element.getAttribute('alt');
    let group = document.querySelector("[id=SongId]").getAttribute('alt');
    let inputs = document.getElementById(replyForm).elements;
    let inputAuthorEmail = getAuthorEmail();
    let inputSongId = inputs["ReplySongId"].value;
    let inputParent = inputs["ReplyParent"].value;
    let message = inputs["ReplyMessage"].value;
    if (inputAuthorEmail != "null" && message != "" && inputSongId != "" && inputParent != "") {
        connection.invoke("SendReplyToGroup", group, inputAuthorEmail, message, inputSongId, inputParent).catch((err) => console.log(err));
    }
    if (inputAuthorEmail != "null" && message != "" && inputSongId != "" && inputParent != "") {
        connection.invoke("SendNotyfication", group, inputAuthorEmail, message, inputSongId, inputParent).catch((err) => console.log(err));
    }
}
function SendRateToGroup(value) {

    let group = document.querySelector("[id=SongId]").getAttribute('alt');
    let inputs = document.getElementById("rate").elements;
    let user = inputs["userEmailRate"].value;
    let songId = inputs["songIdRate"].value;
    if (user != "null" && songId != "") {
        connection.invoke("SendRate", group, user, songId, parseInt(value)).catch((err) => console.log(err));
    }
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

function getAuthorEmail() {
    let myForm = document.getElementById('commentForm');
    let child = myForm.querySelector('input[name="AuthorEmail"]');
    return child.value;
}