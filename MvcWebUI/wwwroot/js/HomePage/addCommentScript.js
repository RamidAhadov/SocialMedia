function AddComment(){

    var button = event.target;
    var postId = button.getAttribute("data-post-id");
    
    var textId = "post" + postId;
    
    var text = document.getElementById(textId);

    var receivedToken = JSON.parse(localStorage.getItem("token"));
    var token = receivedToken.token.token;

    var data = {
        PostId: postId,
        CommentText: text.value,
        Token: token
    };

    $.ajax({
        url: 'http://localhost:5015/api/Post/addComment',
        type: 'POST',
        headers: {
            "Content-Type": "application/json",
            "Authorization": "bearer " + token
        },
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(data),
        success: function (response) {
            console.log('Successful response:', response);

        },
        error: function (x,y,z){

        }
    });
}
