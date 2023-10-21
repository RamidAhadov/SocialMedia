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