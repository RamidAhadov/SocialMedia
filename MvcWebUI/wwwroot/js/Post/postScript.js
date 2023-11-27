function AddPost(){
    
    var receivedToken = JSON.parse(localStorage.getItem("token"));
    var token = receivedToken.token.token;
    var postText = document.getElementById("postArea").value;
    
    var data = {
    PostText: postText,
    Token: token
    };

    $.ajax({
        url: 'http://localhost:5015/api/Post/addPost',
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
