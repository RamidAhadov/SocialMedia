document.getElementById('showNotifications').addEventListener('click',function (){
    var bell = document.getElementById('notificationsContainer');
    if (bell.style.visibility === 'hidden'){
        bell.style.visibility = 'visible';
    }
    else if(bell.style.visibility === 'visible'){
        bell.style.visibility = 'hidden'
    }
    else{
        bell.style.visibility = 'visible'
    }
    //document.getElementById('notificationsContainer').style.visibility = 'visible';
});

connection.on("ReceiveNotification", function (profilePhoto,notificationContent,notificationDate,senderId,receiverId) {
    
    var model = {
        profilePhoto: profilePhoto,
        notificationContent: notificationContent,
        notificationDate:notificationDate,
        senderId: senderId,
        receiverId:receiverId
    }
    
    GetNotifications(JSON.stringify(model));
    GetCount();
});

connection.on("ReceiveFriendRequest", function (profilePhoto,notificationContent,notificationDate,senderId,receiverId) {

    var model = {
        requestId : senderId,
        profilePhoto: profilePhoto,
        requestContent: notificationContent,
        requestDate:notificationDate,
        senderId: senderId,
        userId:receiverId
    }

    GetFriendRequests(JSON.stringify(model));
    GetCount();
});

FillNotificationContainer(token)

function FillNotificationContainer(token) {
    const firstRequest = new Promise((resolve, reject) => {
        $.ajax({
            url: 'http://localhost:5015/api/Notification/getFriendRequests?token=' + token,
            method: 'GET',
            success: function (response) {
                if (response && response.length > 0) {
                    response.forEach((data) => {
                        GetFriendRequests(JSON.stringify(data));
                    });
                }
                resolve();
            },
            error: function (error) {
                reject(error);
            }
        });
    });

    const secondRequest = new Promise((resolve, reject) => {
        $.ajax({
            url: 'http://localhost:5015/api/Notification/getNotifications?token=' + token,
            method: 'GET',
            success: function (response) {
                if (response && response.length > 0) {
                    response.forEach((data) => {
                        GetNotifications(JSON.stringify(data));
                    })
                }
                resolve();
            },
            error: function (error) {
                reject(error);
            }
        });
    });

    firstRequest
        .then(() => secondRequest)
        .then(() => {
            const friendRequestsContainer = document.getElementById('friendRequests');
            const notificationsContainer = document.getElementById('activeNotifications');
            if(notificationsContainer.innerHTML.trim() === ''){
                const emptyNotification = document.getElementById('emptyNotifications');
                if(friendRequestsContainer.innerHTML.trim() !== ''){
                    const hrContainer = document.getElementById('notificationHrContainer');
                    hrContainer.innerHTML = '<hr/>';
                    emptyNotification.innerHTML = `<p style="visibility: visible; text-align: center">You have not new notifications yet.</p>`;
                }
                else if (friendRequestsContainer.innerHTML.trim() === ''){
                    emptyNotification.innerHTML = `<p>You have not new notifications yet.</p>`;
                }
            }
            else if (friendRequestsContainer.innerHTML.trim() !== '') {
                if (notificationsContainer.innerHTML.trim() !== '') {
                    const hrContainer = document.getElementById('notificationHrContainer');
                    hrContainer.innerHTML = '<hr/>';
                }
            }
        })
        .catch((error) => {
            console.log(error);
        });
}

function GetFriendRequests(request) {
    
    if (request != null) {
        var data = JSON.parse(request);
        
        const profilePhoto = data.profilePhoto;
        const requestContent = data.requestContent;
        const requestDate = data.requestDate;
        const senderId = data.senderId;
        const receiverId = data.userId;

        const friendRequestsContainer = document.getElementById('friendRequests');
        const friendRequest = document.createElement('div');
        friendRequest.id = 'friend-request-' + senderId;
        friendRequest.classList.add('friend-request');
        friendRequest.innerHTML = `
        <div class="requester-info">
            <div class="requester-pp">
                <img class="pp-image" src="${profilePhoto}">
            </div>
            <div class="requester">
                <div class="requester-name">
                    ${requestContent}
                </div>
                <div class="request-date">
                    ${requestDate}
                </div>
            </div>
        </div>
        <div class="answer-buttons">
            <i class="fa-solid fa-check accept" onclick="AcceptRequest(${senderId},${receiverId})"></i>
            <i class="fa-solid fa-x decline" onclick="DeclineRequest(${senderId},${receiverId})"></i>
        </div>
        `;
        friendRequestsContainer.appendChild(friendRequest);

        return friendRequestsContainer;
    }
}

function GetNotifications(request) {

    //if(request !== '') {
        var data = JSON.parse(request);
        
        
        const profilePhoto = data.profilePhoto;
        const notificationContent = data.notificationContent;
        const notificationDate = data.notificationDate;

        const notificationContainer = document.getElementById('activeNotifications');
        const notification = document.createElement('div');
        notification.id = data.notificationId;
        notification.classList.add('notification');
        notification.innerHTML = `
        <div class="notification-image">
            <img class="pp-image" src="${profilePhoto}">
        </div>
        <div class="notification-content">
            <div class="notification-text">
                ${notificationContent}
            </div>
            <div class="notification-date">
                ${notificationDate}
            </div>
        </div>
        `;
        notificationContainer.appendChild(notification);

        return notificationContainer;
    //}
}