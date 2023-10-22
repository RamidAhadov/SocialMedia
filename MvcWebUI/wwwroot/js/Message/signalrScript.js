


// var connection = new signalR.HubConnectionBuilder().withUrl("http://localhost:5015/chathub").build();
//
// connection.on("ReceiveSpecificMessage", function (user, message) {
//     var encodedMessage = user + " says: " + message;
//     var li = document.createElement("li");
//     li.textContent = encodedMessage;
//     document.getElementById("messagesList").appendChild(li);
// });
//
// connection.start().catch(function (err) {
//     return console.error(err.toString());
// });
//
// document.getElementById("sendButton").addEventListener("click", function (event) {
//     var user = document.getElementById("userInput").value;
//     var message = document.getElementById("messageInput").value;
//     connection.invoke("SendMessageToUser", user, message).catch(function (err) {
//         return console.error(err.toString());
//     });
//     event.preventDefault();
// });

var connection = new signalR.HubConnectionBuilder().withUrl("http://localhost:5015/chathub").build();

connection.start().then(function () {
    // Bağlantı başarılı oldu, istemci ile iletişime geçebilirsiniz

    console.log(connection.connectionId);
}).catch(function (err) {
    console.error(err);
});
//Burada olanda error verir

function StartChat(friendId){
    const chatContainer = document.getElementById('chatContainer');
    const chatTopName = document.getElementById('chat-top-name');
    const data = {
        id: friendId
    };

    var receivedToken = JSON.parse(localStorage.getItem("token"));
    var token = receivedToken.token.token;

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
            
            //SignalR connection start
            

            //connection.invoke("AddUsersToGroup", token, friend.userName);

            // document.getElementById("sendMessageButton").addEventListener("click", function () {
            //     var message = document.getElementById("messageInput").value;
            //     connection.invoke("SendMessageToGroup", token, friend.userName, message);
            //});

            
            
            document.getElementById("addUsersButton").addEventListener("click", function () {
                const userId = document.getElementById("userInput1").value;
                const message = document.getElementById("userInput2").value;
                connection.invoke("SendMessage", userId, message).catch(function (err) {
                    console.error(err);
                });
            });

            connection.on("ReceiveMessage", function (user, message) {
                var encodedMessage = user + " says: " + message;
                var li = document.createElement("li");
                li.textContent = encodedMessage;
                document.getElementById("messagesList").appendChild(li);
            });
            //---oo-------------00---\\
        },
        error: function (x,y,z){

        }
    });
}

