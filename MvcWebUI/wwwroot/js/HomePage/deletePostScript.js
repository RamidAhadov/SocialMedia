function DeletePost(postId){
    $.ajax({
        url: 'http://localhost:5015/api/Post/deletePost',
        type: 'POST',
        headers: {
            "Content-Type": "application/json",
            "Authorization": "bearer " + token
        },
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(postId),
        success: function (response) {
            console.log('Successful response:', response);
            const post = document.getElementById(`post-${postId}`);
            post.remove();
        },
        error: function (x,y,z){

        }
    });
}