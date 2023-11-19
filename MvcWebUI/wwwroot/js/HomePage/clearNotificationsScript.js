document.getElementById("clearNotifications").addEventListener('click',function (){
    const notificationContainers = document.querySelectorAll('.notification');

    notificationContainers.forEach(container => {
        container.remove();

        $.ajax({
            url: 'http://localhost:5015/api/Notification/deleteNotifications',
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(token),
            success: function (response) {
                console.log('Successful response:', response);
                GetCount();
            },
            error: function (x, y, z) {

            }
        });
        //connection.invoke("RemoveUserFromGroup",token,friendUserName);
    });
});