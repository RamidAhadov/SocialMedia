function SendRequestFromProfile(userData,requestData,userId){
    $.ajax({
        url: 'http://localhost:5015/api/FriendRequest/requestFriend',
        type: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(requestData),
        success: function (response) {
            console.log(response);
            //Send online request

            //connection.invoke("SendNotification",response,profilePhoto, notificationContent,notificationDate, senderId,receiverId);

            $.ajax({
                url: 'http://localhost:5015/api/Connection/getConnectionIdById',
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(userData.id),
                success: function (response) {
                    console.log('Successful response:', response);

                    const profilePhoto = userData.profilePhotoUrl;
                    const notificationContent = `${userData.firstName} ${userData.lastName} has sent you a friend request.`;
                    const notificationDate = 'Just now.'
                    const senderId = userId.toString();
                    const receiverId = userData.id.toString();

                    //Bu ishe dusmur. Yegin ki datalar uygun gelmir.
                    connection.invoke("SendFriendRequest",response,profilePhoto, notificationContent,notificationDate, senderId,receiverId);
                },
                error: function (x, y, z) {

                }
            });
        },
        error: function (x, y, z) {

        }
    });
}