function AddComment(){

    var button = event.target;
    var postId = button.getAttribute("data-post-id");
    var authorId = button.getAttribute("data-post-author-id");
    
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
            
            var notificationData = {
                Token:token,
                ReceiverId:authorId,
                Template: 'WRC'
            }
            $.ajax({
                url: 'http://localhost:5015/api/Notification/recordNotification',
                type: 'POST',
                headers: {
                    "Content-Type": "application/json",
                    "Authorization": "bearer " + token
                },
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(notificationData),
                success: function (response) {
                    console.log('Successful response:', response);
                    //--------------------//
                    const recordedNotificationModel = JSON.parse(JSON.stringify(response));
                    $.ajax({
                        url: 'http://localhost:5015/api/Connection/getConnectionIdById?id=' + authorId,
                        type: 'GET',
                        contentType: 'application/json; charset=utf-8',
                        success: function (response) {
                            console.log('Successful response:', response);

                            const profilePhoto = recordedNotificationModel.profilePhoto;
                            const notificationContent = recordedNotificationModel.notificationContent;
                            const notificationDate = recordedNotificationModel.notificationDate;
                            const senderId = recordedNotificationModel.senderId.toString();
                            const receiverId = recordedNotificationModel.receiverId.toString();

                            //Bu ishe dusmur. Yegin ki datalar uygun gelmir.
                            connection.invoke("SendNotification",response,profilePhoto, notificationContent,notificationDate, senderId,receiverId);
                        },
                        error: function (x, y, z) {

                        }
                    });
                    //----------------//

                },
                error: function (x, y, z) {

                }
            });

        },
        error: function (x,y,z){

        }
    });
}
