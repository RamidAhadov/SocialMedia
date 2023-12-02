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
        success: async function (response) {
            console.log('Successful response:', response);
            
            GetCount();
            
            document.getElementById(`friend-request-${senderId}`).remove();
            
            const userData = JSON.parse(await GetUserInformationByUserId(senderId));
            
            const friendId = userData.id;
            const friendFirstName = userData.firstName;
            const friendLastName = userData.lastName;
            const friendUserName = userData.userName;
            const friendProfilePhoto = userData.profilePhoto;

            AddFriendContainer(friendId,friendUserName,friendFirstName,friendLastName,friendProfilePhoto)
            
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
                        url: 'http://localhost:5015/api/Connection/getConnectionIdById?id=' + senderId,
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
//friendId,friendUserName,friendFirstName,friendLastName
function AddFriendContainer(friendId,friendUserName,friendFirstName,friendLastName,friendProfilePhoto){
    const friendsContainer = document.getElementById('friendsContainerDiv');
    const friendContainer = document.createElement('div');
    //Inner HTML add
    friendContainer.innerHTML = 
        `
            <div class="friend-pp">
                <img class="pp-image" src=${friendProfilePhoto}>
            <div data-indicator-id ="${friendUserName}" class="indicator"></div>
            </div>
            <div class="friend-name">
                <h5>
                    ${friendFirstName} ${friendLastName}
                </h5>
                <div id="status-${friendUserName}" data-lastseen-id="${friendUserName}" class="friend-lastSeen">
                                    
                </div>
            </div>
        `;
    friendContainer.classList.add('friends-container');
    friendContainer.onclick = async function() {
        await StartChat(friendId, friendUserName, friendFirstName, friendLastName);
    };
    
    friendsContainer.appendChild(friendContainer);
    IndicatorSetter();
}
//!!!!!!!!!!!CLEARING TEXT AFTER SEND MESSAGE (RECEIVED ERROR)!!!!!!!!!!
async function GetUserInformationByUserId(userId){
    try {
        const response = await $.ajax({
            url: 'http://localhost:5015/api/User/getUserById?id=' + userId,
            method: 'GET',
            headers: {
                "Content-Type": "application/json",
                "Authorization": "Bearer " + token
            }
        });

        return JSON.stringify(response);
    } catch (error) {
        console.log(error);
        return null;
    }
}