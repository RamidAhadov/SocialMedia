const connection = new signalR.HubConnectionBuilder().withUrl("http://localhost:5015/chathub").build();

const receivedToken = JSON.parse(localStorage.getItem("token"));
const token = receivedToken.token.token;

connection.start().then(function () {
    console.log(connection.connectionId);
    const data = {
        Token: token,
        ConnectionId: connection.connectionId
    }
    $.ajax({
        url: 'http://localhost:5015/api/Chat/recordConnectionId',
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(data),
        success: function (response) {
            console.log('Successful response:', response);
        },
        error: function (x,y,z){

        }
    });
    IndicatorSetter();
}).catch(function (err) {
    return console.error(err.toString());
});

connection.on("ReceiveMessage", function (user,message) {
    SetMessages(user,friendName,message)
});

window.addEventListener('beforeunload', function (event) {
    UpdateStatus();
});

document.getElementById("closeConnection").addEventListener("click",function (){
    connection.stop()
        .then(() => {
            console.log('Connection closed.');
            UpdateStatus();
        })
        .catch((error) => {
            console.error('Connection lost: ', error);
            UpdateStatus();
        });
});

var friendConnectionId;
var friendName;
var userName = localStorage.getItem("userName");
function StartChat(userId,friendId,friendUserName,friendFirstName,friendLastName){

    const existingContainers = document.querySelectorAll('.messaging-container');

    existingContainers.forEach(container => {
        container.remove();
    });

    let existingContainer = document.getElementById(friendId);

    if (!existingContainer) {
        existingContainer = createChatContainer(friendId, friendUserName,friendFirstName,friendLastName);
    }

    existingContainer.style.display = 'block';

    const newFriendContainer = document.getElementById(friendId);
    if (newFriendContainer) {
    }
    
    if (document.getElementById(`messaging-messages-${friendUserName}`).innerHTML.trim() === '') {
        //To get chat messages
        const getChatData = {
            SenderId: friendId,
            ReceiverId: userId
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
                    if (parseInt(userId) === parseInt(senderId)) {
                        div.innerHTML = "<span class='user-message'>" + message + "</span>";
                    }
                    if (parseInt(userId) === parseInt(receiverId)) {
                        div.innerHTML = "<span class='sender-message'>" + message + "</span>";
                    }
                    document.getElementById(`messaging-messages-${friendUserName}`).appendChild(div);
                });
            },
            error: function (x, y, z) {

            }
        });
    }
    
    $.ajax({
        url: 'http://localhost:5015/api/Chat/getConnectionId',
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
    
    document.getElementById(`send-message-async-${friendUserName}`).addEventListener("click",function (){
        var message = document.getElementById(`message-text-${friendUserName}`).value;
        connection.invoke("SendMessageToGroup",token,friendUserName, message);
        var data = {
            SenderId: userId,
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

function createChatContainer(containerId, friendUserName,friendFirstName,friendLastName) {
    // Create a new message container
    const chatContainer = document.createElement('div');
    chatContainer.id = containerId;
    chatContainer.classList.add('messaging-container');
    chatContainer.style.display = 'block';

    // Create all HTML settings and add them
    chatContainer.innerHTML = `
        <div class="messaging-container-top">
            <div class="messaging-container-top-name">
                <div class="chat-friend-name" href="#">${friendFirstName} ${friendLastName}</div>
            </div>
            <div class="messaging-container-top-dash" onclick="Minimize()">
                <i id="icon" class="fa-solid fa-window-minimize"></i>
            </div>
            <div class="messaging-container-top-x" onclick="CloseChat()">
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
 
 function UpdateStatus(){
     $.ajax({
         url: 'http://localhost:5015/api/Chat/updateStatus',
         type: 'POST',
         contentType: 'application/json; charset=utf-8',
         data: JSON.stringify(token),
         success: function (response) {
             console.log('Successful response:', response);
         },
         error: function (x,y,z){

         }
     });
 }
 
 function IndicatorSetter(){
    var indicators = document.querySelectorAll(".indicator");
    indicators.forEach((indicator)=>{
        const indicatorId = indicator.getAttribute("data-indicator-id");
        $.ajax({
            url: 'http://localhost:5015/api/Chat/checkStatus',
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(indicatorId),
            success: function (response) {
                console.log('Successful response:', response);
                if (response === 'Online'){
                    indicator.classList.add('online');
                    var status = document.getElementById(`status-${indicatorId}`)
                    status.textContent = "Online";
                }
                if (response === 'Offline'){
                    indicator.classList.add('offline')
                    LastSeenSetter(indicatorId);
                }
            },
            error: function (x,y,z){

            }
        });
    });
 }
 
 function LastSeenSetter(userNameForLastSeen){
    var lastSeens = document.querySelectorAll(".friend-lastSeen")
     
     lastSeens.forEach((lastSeen)=>{
         $.ajax({
             url: 'http://localhost:5015/api/Friend/getLastSeen?userName=' + userNameForLastSeen,
             method: 'GET',
             success: function (data) {
                 if (lastSeen.id === `status-${userNameForLastSeen}`){
                     lastSeen.textContent = data
                 }
             },
             error: function (error) {
                 console.log(error);
             }
         });
     })
 }
