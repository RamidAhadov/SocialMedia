function LikeComment() {
    var button = event.target;
    var commentId = button.getAttribute("data-comment-id");
    var likeCommentButton = document.getElementById(commentId);
    var className = likeCommentButton.className;
    var buttonColor = likeCommentButton.style.color;

    var receivedToken = JSON.parse(localStorage.getItem("token"));
    var token = receivedToken.token.token;

    var data = {
        CommentLikedUser: {
            Id: 5,
            UserId: 19,
            CommentId: commentId
        },
        Token: token
    };
    $.ajax({
        url: 'http://localhost:5015/api/Like/likeComment',
        type: 'POST',
        headers: {
            "Content-Type": "application/json"
        },
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(data),
        success: function (response) {
            console.log('Comment liked!');
            if (className === 'like-comment fa-solid fa-heart fa-xl' && buttonColor === 'red'){
                likeCommentButton.className = 'like-comment fa-regular fa-heart fa-xl';
                likeCommentButton.style.color = 'black';
            }
            if(className === 'like-comment fa-regular fa-heart fa-xl' && buttonColor === 'black'){
                likeCommentButton.className = 'like-comment fa-solid fa-heart fa-xl';
                likeCommentButton.style.color = 'red';
            }
        },
        error: function (x, y, z) {

        }
    });
}