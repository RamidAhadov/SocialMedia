function UpdateNotReceivedMessages(token){
    $.ajax({
        url: 'http://localhost:5015/api/Chat/updateMessageStatusOnLogin',
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