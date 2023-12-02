function Like() {
    var button = event.target;
    var postId = button.getAttribute("data-post-id");
    var authorId = button.getAttribute("data-author-id");

    // var receivedToken = JSON.parse(localStorage.getItem("token"));
    // var token = receivedToken.token.token;

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
            if(response === 'Post liked'){
                var notificationData = {
                    Token: token,
                    ReceiverId: authorId,
                    Template: 'LKP'
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
                        const recordedNotificationModel = JSON.parse(JSON.stringify(response));
                            $.ajax({
                                url: 'http://localhost:5015/api/Connection/getConnectionIdById?id=' +authorId,
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
                    },
                    error: function (x, y, z) {

                    }
                });
            }
        },
        error: function (x, y, z) {

        }
    });
}
