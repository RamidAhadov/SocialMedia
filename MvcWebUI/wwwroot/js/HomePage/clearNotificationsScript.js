document.getElementById("clearNotifications").addEventListener('click',function (){
    const notificationContainers = document.querySelectorAll('.notification');

    notificationContainers.forEach(container => {
        container.remove();
        //connection.invoke("RemoveUserFromGroup",token,friendUserName);
    });
});