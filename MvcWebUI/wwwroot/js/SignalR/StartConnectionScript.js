const connection = new signalR.HubConnectionBuilder().withUrl("http://localhost:5015/chathub").build();

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
    //IndicatorSetter();
}).catch(function (err) {
    return console.error(err.toString());
});

window.addEventListener('beforeunload', function (event) {
    UpdateStatus();
});