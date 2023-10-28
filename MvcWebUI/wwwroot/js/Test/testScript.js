const connection = new signalR.HubConnectionBuilder().withUrl("http://localhost:5015/chathub").build();

connection.start().then(function () {
    console.log(connection.connectionId);
    var il = document.createElement("li")
    il.textContent = connection.connectionId;
    document.getElementById("connection").appendChild(il);
}).catch(function (err) {
    return console.error(err.toString());
});


connection.on("ReceiveM", function (message) {
    var li = document.createElement("li");
    li.textContent = "Deyir ki " + message;
    document.getElementById("messages").appendChild(li);
});

document.getElementById("addGroup").addEventListener("click", function () {
    
    var connectionId = document.getElementById("connectionInput").value;

    var groupName = document.getElementById("groupName").value;
    
    connection.invoke("AddToGroup", groupName, connectionId)
        .then(function () {
            console.log("Success");
        })
        .catch(function (error) {
            console.error("Error:", error);
        });
});

document.getElementById("sendMessage").addEventListener("click",function (){
    var groupName = document.getElementById("groupName").value;
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendToGroup",groupName, message);
});