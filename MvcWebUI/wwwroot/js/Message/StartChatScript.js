var friendConnectionId;
var friendName;
var userName = localStorage.getItem("userName");
async function StartChat(friendId,friendUserName,friendFirstName,friendLastName){

    const currentUserData = await GetCurrentUserInformation();
    const currentUser = JSON.parse(currentUserData);
    const currentUserId = currentUser.id;

    const existingContainers = document.querySelectorAll('.messaging-container');

    existingContainers.forEach(container => {
        container.remove();
        connection.invoke("RemoveUserFromGroup",token,friendUserName);
    });

    let existingContainer = document.getElementById(`friendChatContainer${friendId}`);

    if (!existingContainer) {
        existingContainer = CreateChatContainer(friendId, friendUserName,friendFirstName,friendLastName);
    }

    existingContainer.style.display = 'block';

    const newFriendContainer = document.getElementById(`friendChatContainer${friendId}`);
    if (newFriendContainer) {
    }

    if (document.getElementById(`messaging-messages-${friendUserName}`)) {
        if (document.getElementById(`messaging-messages-${friendUserName}`).innerHTML.trim() === '') {
            //To get chat messages
            const getChatData = {
                SenderId: friendId,
                ReceiverId: currentUserId
            }
            $.ajax({
                url: 'http://localhost:5015/api/Chat/getChatMessages',
                type: 'POST',
                headers: {
                    "Content-Type": "application/json",
                    "Authorization": "bearer " + token
                },
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(getChatData),
                success: function (response) {
                    console.log('Successful response:', response);
                    response.forEach(function (chatMessage) {

                        const senderId = chatMessage.senderId;
                        const receiverId = chatMessage.receiverId
                        const message = chatMessage.messageText;

                        const div = document.createElement("div");
                        if (parseInt(currentUserId) === parseInt(senderId)) {
                            div.innerHTML = "<span class='user-message'>" + message + "</span>";
                        }
                        if (parseInt(currentUserId) === parseInt(receiverId)) {
                            div.innerHTML = "<span class='sender-message'>" + message + "</span>";
                        }
                        document.getElementById(`messaging-messages-${friendUserName}`).appendChild(div);
                        document.getElementById(`messaging-messages-${friendUserName}`).scrollTop = document.getElementById(`messaging-messages-${friendUserName}`).scrollHeight;

                    });
                },
                error: function (x, y, z) {

                }
            });
        }
        $.ajax({
            url: 'http://localhost:5015/api/Connection/getConnectionId',
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(friendUserName),
            success: function (response) {
                console.log('Successful response:', response, friendUserName);
                friendConnectionId = response;
                friendName = friendUserName;

                connection.invoke("AddUsersToGroup",userName,friendUserName,friendConnectionId)
                    .then(function () {
                        console.log("Success");
                    })
                    .catch(function (error) {
                        console.error("Error:", error);
                    });
            },
            error: function (x, y, z) {

            }
        });
    }


    if(document.getElementById(`send-message-async-${friendUserName}`)) {
        document.getElementById(`send-message-async-${friendUserName}`).addEventListener("click", function () {
            var message = document.getElementById(`message-text-${friendUserName}`).value;
            connection.invoke("SendMessageToGroup", token, friendUserName, message);
            var data = {
                SenderId: currentUserId,
                ReceiverId: friendId,
                MessageText: message
            }
            $.ajax({
                url: 'http://localhost:5015/api/Chat/recordMessage',
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(data),
                success: function (response) {
                    console.log('Successful response:', response);
                    document.getElementById('message-text').textContent = '';
                },
                error: function (x, y, z) {
                }
            });
        });
    }
}

function CreateChatContainer(containerId, friendUserName,friendFirstName,friendLastName) {
    // Create a new message container
    const chatContainer = document.createElement('div');
    chatContainer.id = 'friendChatContainer' +containerId;
    chatContainer.classList.add('messaging-container');
    chatContainer.style.display = 'block';

    // Create all HTML settings and add them
    chatContainer.innerHTML = `
        <div class="messaging-container-top">
            <div class="messaging-container-top-name">
                <div class="chat-friend-name" href="#" onclick="GoToUserPage('${friendUserName}')">${friendFirstName} ${friendLastName}</div>
            </div>
            <div class="messaging-container-top-dash" onclick="Minimize(${containerId})">
                <i id='icon${containerId}' class="fa-solid fa-window-minimize"></i>
            </div>
            <div class="messaging-container-top-x" onclick="CloseChat('${friendUserName}', ${containerId})">
                <i class="fa-solid fa-x"></i>
            </div>
        </div>
        <div class="messaging-container-messages" id="messaging-messages-${friendUserName}"></div>
        <div class="messaging-container-send-area" id="messaging-send-${friendUserName}">
            <input id="message-text-${friendUserName}" class="messaging-container-text-area">
            <i id="send-message-async-${friendUserName}" class="messaging-container-send-message fa-regular fa-paper-plane"></i>
        </div>
    `;

    // Add container to the page
    document.body.appendChild(chatContainer);

    return chatContainer;
}
function SetMessages(user,friendName,message){
    const div = document.createElement("div");
    if (user === userName) {
        div.innerHTML = "<span class='user-message'>"+ message +"</span>";
    } if(user === friendName) {
        div.innerHTML = "<span class='sender-message'>" + message + "</span>";
    }
    document.getElementById(`messaging-messages-${friendName}`).appendChild(div);
}

async function GetCurrentUserInformation() {
    try {
        const response = await $.ajax({
            url: 'http://localhost:5015/api/User/getUserByToken?token=' + token,
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