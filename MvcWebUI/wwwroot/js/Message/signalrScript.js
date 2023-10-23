var connection = new signalR.HubConnectionBuilder().withUrl("http://localhost:5015/chathub").build();

var receivedToken = JSON.parse(localStorage.getItem("token"));
var token = receivedToken.token.token;

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
    console.log(connection.connectionId);
    
    var data = {
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
}).catch(function (err) {
    return console.error(err.toString());
});

connection.on("ReceiveMessage", function (message) {
    var li = document.createElement("li");
    li.textContent = message;
    document.getElementById("messagesList").appendChild(li);
});
function StartChat(friendId){
    const chatContainer = document.getElementById('chatContainer');
    const chatTopName = document.getElementById('chat-top-name');
    const data = {
        id: friendId
    };


    $.ajax({
        url: 'http://localhost:5015/api/User/GetUser',
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(data),
        success: function (response) {
            console.log('Successful response:', response);
            if (chatContainer.style.display === 'none'){
                chatContainer.style.display = 'block';
            }
            const friend = response;
            const friendInfoDiv = document.createElement('div');
            
            friendInfoDiv.style.display = 'flex';

            const img = document.createElement('img');
            img.src = friend.profilePhoto;
            img.className = 'messaging-container-pp';
            img.style.marginTop = '2.5px';
            img.style.marginRight = '5px';

            const div = document.createElement('div');
            div.className = 'chat-friend-name';
            div.textContent = friend.firstName + ' ' + friend.lastName;

            friendInfoDiv.appendChild(img);
            friendInfoDiv.appendChild(div);

            chatTopName.appendChild(friendInfoDiv);
            function Refresh() {
                $.ajax({
                    url: 'http://localhost:5015/api/Chat/getConnectionId',
                    type: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    data: JSON.stringify(friend.userName),
                    success: function (response) {
                        console.log('Successful response:', response);

                        connection.invoke("AddUsersToGroup", token, friend.userName, response);
                    },
                    error: function (x, y, z) {

                    }
                });
            }
            setInterval(Refresh,5000);

            document.getElementById("send-message-async").addEventListener("click", function () {
                var message = document.getElementById("message-text").value;
                connection.invoke("SendMessageToGroup", token, friend.userName, message);
            });
        },
        error: function (x,y,z){

        }
    });
}

