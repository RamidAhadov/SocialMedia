document.getElementById('executeFindRequest').addEventListener('click',function (){
    FillNotificationContainer(token)
});

function FillNotificationContainer(token){
    $.ajax({
        url: 'http://localhost:5015/api/Notification/getFriendRequests?token=' + token,
        method: 'GET',
        success: function (response) {
            if (response && response.length > 0) {
                response.forEach((data) => {
                    GetFriendRequests(JSON.stringify(data));
                });
            }
        },
        error: function (error) {
            console.log(error);
        }
    });

    $.ajax({
        url: 'http://localhost:5015/api/Notification/getNotifications?token=' + token,
        method: 'GET',
        success: function (response) {
            if (response && response.length > 0) {
                response.forEach((data) => {
                    GetNotifications(JSON.stringify(data));
                });
            }
            // else {
            //     const friendRequestsContainer = document.getElementById('friendRequests');
            //     if (friendRequestsContainer.innerHTML.trim() !== '') {
            //         const hrContainer = document.getElementById('notificationHr');
            //         hrContainer.style.visibility = 'visible';
            //     }
            // }
        },
        error: function (error) {
            console.log(error);
        }
    });
}

function GetFriendRequests(request) {
    
    if (request != null) {
        var data = JSON.parse(request);
        
        const profilePhoto = data.profilePhoto;
        const requestContent = data.requestContent;
        const requestDate = data.requestDate;

        const friendRequestsContainer = document.getElementById('friendRequests');
        const notificationsContainer = document.getElementById('notificationsContainer');
        const friendRequest = document.createElement('div');
        friendRequest.id = data.requestId;
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
            <i class="fa-solid fa-check accept"></i>
            <i class="fa-solid fa-x decline"></i>
        </div>
        `;
        friendRequestsContainer.appendChild(friendRequest);
        notificationsContainer.appendChild(friendRequestsContainer);

        return friendRequestsContainer;
    }
}

function GetNotifications(request) {

    if(request != null) {
        var data = JSON.parse(request);
        
        // const friendRequestsContainer = document.getElementById('friendRequests');
        // if (friendRequestsContainer.innerHTML.trim() !== ''){
        //     const hrContainer = document.getElementById('notificationHr');
        //     hrContainer.style.visibility = 'visible';
        // }
        const profilePhoto = data.profilePhoto;
        const notificationContent = data.notificationContent;
        const notificationDate = data.notificationDate;

        const notificationContainer = document.getElementById('activeNotifications');
        const notificationsContainer = document.getElementById('notificationsContainer');
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
        notificationsContainer.appendChild(notificationContainer);

        return notificationContainer;
    }
}