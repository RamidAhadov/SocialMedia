function GetConnectionId(friendId){
    $.ajax({
        url: 'http://localhost:5015/api/Connection/getConnectionIdById?id=' + friendId,
        type: 'GET',
        contentType: 'application/json; charset=utf-8',
        success: function (response) {
            console.log('Successful response:', response);
            
            return response;
        },
        error: function (x, y, z) {

        }
    });
}