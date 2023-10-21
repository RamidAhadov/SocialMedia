var connection = new signalR.HubConnectionBuilder().withUrl("http://localhost:5015/chathub").build();

connection.on("ReceiveMessage", function (user, message) {
    var encodedMessage = user + " says: " + message;
    var li = document.createElement("li");
    li.textContent = encodedMessage;
    document.getElementById("messagesList").appendChild(li);
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});