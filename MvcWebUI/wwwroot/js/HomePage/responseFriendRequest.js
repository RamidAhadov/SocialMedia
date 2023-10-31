function AcceptRequest() {
    const listItem = document.querySelector('.accept-button');

    const senderId = listItem.getAttribute('data-sender-id');
    const receiverId = listItem.getAttribute('data-receiver-id');
    
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

                },
                error: function (x, y, z) {

                }
            });
        },
        error: function (x, y, z) {

        }
    });
}

function DeclineRequest() {
    const listItem = document.querySelector('.decline-button');

    const senderId = listItem.getAttribute('data-sender-id');
    const receiverId = listItem.getAttribute('data-receiver-id');

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
        },
        error: function (x, y, z) {

        }
    });
}