function UpdateNotReceivedMessages(token){
    $.ajax({
        url: 'http://localhost:5015/api/Chat/updateMessageStatusOnLogin',
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(token),
        success: function (response) {
            console.log('Successful response:', response);
            
            response.forEach(data=>{
                const connectionId = data.connectionId;
                const messageIdList = data.messageIds;
                
                messageIdList.forEach(messageId=>{
                    connection.invoke("SendMessageReceiptConfirmation", connectionId,messageId.toString()).catch(err => console.error(err));
                })
            })
        },
        error: function (x,y,z){

        }
    });
}


//-------------------------------------------------------------------------------//
UpdateNotReceivedMessages(token);
//-------------------------------------------------------------------------------//