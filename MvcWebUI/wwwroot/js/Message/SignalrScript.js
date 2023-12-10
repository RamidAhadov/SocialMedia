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
        url: 'http://localhost:5015/api/Connection/recordConnectionId',
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
connection.on("ReceiveMessage", function (user,message,messageId,connectionId) {
    SetMessages(user,friendName,message,messageId);
    connection.invoke("ConfirmMessageReceived", friendName,messageId,connectionId).catch(err => console.error(err));
});

connection.on("MessageReceivedConfirmation", (notification,messageId) => {
    console.log(notification);
    const messageStatusContainer = document.getElementById(`messageStatus-${messageId}`)
    messageStatusContainer.innerHTML = `<i class="fa-solid fa-check-double"></i>`;

    $.ajax({
        url: 'http://localhost:5015/api/Chat/updateMessageStatus',
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(messageId),
        success: function (response) {
            console.log('Successful response:', response);
        },
        error: function (x,y,z){

        }
    });
});

connection.on("ReceiveReceiptMessageConfirmation", (messageId) => {
    const messageStatusContainer = document.getElementById(`messageStatus-${messageId}`)
    messageStatusContainer.innerHTML = `<i class="fa-solid fa-check-double"></i>`;
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
