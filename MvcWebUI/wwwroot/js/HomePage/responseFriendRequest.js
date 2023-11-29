function AcceptRequest(senderId,receiverId) {
    const listItem = document.querySelector('.accept-button');

    //const senderId = listItem.getAttribute('data-sender-id');
    //const receiverId = listItem.getAttribute('data-receiver-id');
    
    var data = {
        SenderId: senderId,
        ReceiverId: receiverId
    }

    $.ajax({
        url: 'http://localhost:5015/api/FriendRequest/acceptRequest',
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(data),
        success: function (response) {
            console.log('Successful response:', response);
            
            GetCount();
            
            document.getElementById(`friend-request-${senderId}`).remove();
            
            //I used senderId instead of receiverId. Because if user accepts friend request then user
            //will send notification itself. But at here notification must send to requester.
            var notificationData = {
                Token:token,
                ReceiverId:senderId,
                Template: 'ACF'
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

                    //---------------------------//
                    const recordedNotificationModel = JSON.parse(JSON.stringify(response));
                    $.ajax({
                        url: 'http://localhost:5015/api/Connection/getConnectionIdById',
                        type: 'POST',
                        contentType: 'application/json; charset=utf-8',
                        data: JSON.stringify(senderId),
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
                    //---------------//

                },
                error: function (x, y, z) {

                }
            });
        },
        error: function (x, y, z) {

        }
    });
}

function DeclineRequest(senderId,receiverId) {
    const listItem = document.querySelector('.decline-button');

    var data = {
        SenderId: senderId,
        ReceiverId: receiverId
    }

    $.ajax({
        url: 'http://localhost:5015/api/FriendRequest/declineRequest',
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(data),
        success: function (response) {
            console.log('Successful response:', response);

            document.getElementById(`friend-request-${senderId}`).remove();
        },
        error: function (x, y, z) {

        }
    });
}