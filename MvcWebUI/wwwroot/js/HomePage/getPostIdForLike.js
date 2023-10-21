function Like() {
    var button = event.target;
    var postId = button.getAttribute("data-post-id");

    var receivedToken = JSON.parse(localStorage.getItem("token"));
    var token = receivedToken.token.token;

    var data = {
        PostLikedUser: {
            Id: 0,
            UserId: 0,
            PostId: postId
        },
        Token: token
    };
    $.ajax({
        url: 'http://localhost:5015/api/Like/likePost',
        type: 'POST',
        headers: {
            "Content-Type": "application/json"
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
