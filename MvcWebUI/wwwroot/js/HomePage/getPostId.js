function AddComment() {
    var commentButton = event.target;
    var postId = commentButton.getAttribute("data-post-id");

    var inputId = "comment-" + postId;
    var commentInput = document.getElementById(inputId);
    var commentText = commentInput.value;

    var receivedToken = JSON.parse(localStorage.getItem("token"));
    var token = receivedToken.token.token;

    var data = {
        postId: postId,
        CommentText: commentText,
        Token: token
    };
    $.ajax({
        url: 'http://localhost:5015/api/post/addComment',
        type: 'POST',
        headers: {
            "Content-Type": "application/json",
            "Authorization": "Bearer " + token
        },
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(data),
        success: function (response) {
            console.log('Successful response:', response);
        },
        error: function (x, y, z) {

        }
    });
}


