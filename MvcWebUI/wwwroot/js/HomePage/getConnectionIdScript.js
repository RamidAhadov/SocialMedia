function GetConnectionId(friendId){
    $.ajax({
        url: 'http://localhost:5015/api/Connection/getConnectionIdById',
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(friendId),
        success: function (response) {
            console.log('Successful response:', response);
            
            return response;
        },
        error: function (x, y, z) {

        }
    });
}