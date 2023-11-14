function LikeComment() {
    var button = event.target;
    var commentId = button.getAttribute("data-comment-id");
    var authorId = button.getAttribute("data-comment-author-id")
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
            if (response === 'Comment liked'){
                
                var notificationData = {
                    Token: token,
                    ReceiverId: authorId,
                    Template: 'LKC'
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
                            url: 'http://localhost:5015/api/Connection/getConnectionIdById',
                            type: 'POST',
                            contentType: 'application/json; charset=utf-8',
                            data: JSON.stringify(authorId),
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