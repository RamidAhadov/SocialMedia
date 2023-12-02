async function GetDataForRequestSend(){
    const userName = GetUsernameFromUrlParameter();
    const currentUserData = await GetCurrentUserInformation(token);
    const currentUser = JSON.parse(currentUserData);
    const currentUserId = currentUser.id;

    const userData = await GetUserInformation(userName,token);
    const user = JSON.parse(userData);
    const userId = user.id;
    const firstName = user.firstName;
    const lastName = user.lastName;
    const photoUrl = user.profilePhoto;

    const requestData = {
        ReceiverId: userId,
        Token: token
    };
    
    SendRequestFromProfile(userData,requestData,currentUserId)
    GetProfileChoices(currentUserId,userId);
}

//userData- the user who we sending request (UserDto)
//requestData - ReceiverId and Token
//userId - Current user's ID
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
            
            const data = JSON.parse(userData);

            $.ajax({
                url: 'http://localhost:5015/api/Connection/getConnectionIdById?id=' + data.id,
                type: 'GET',
                contentType: 'application/json; charset=utf-8',
                success: function (response) {
                    console.log('Successful response:', response);

                    const profilePhoto = data.profilePhoto;
                    const notificationContent = `${data.firstName} ${data.lastName} has sent you a friend request.`;
                    const notificationDate = 'Just now.'
                    const senderId = userId.toString();
                    const receiverId = data.id.toString();

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