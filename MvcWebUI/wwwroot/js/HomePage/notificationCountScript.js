function GetCount(){
    $.ajax({
        url: 'http://localhost:5015/api/Notification/getNotificationsCount?token=' + token,
        method: 'GET',
        success: function (response) {
            const existingCircle = document.querySelectorAll('.notification-circle');

            existingCircle.forEach(circle => {
                circle.remove();
            });
            const navbarItem = document.getElementById('rightNavbarItemsDiv');
            const circle = document.createElement('div');
            circle.classList.add('notification-circle');
            circle.innerHTML = `<div class="notification-count">${response}</div>`;
            navbarItem.appendChild(circle);
        },
        error: function (error) {
            reject(error);
        }
    });
}

GetCount();

// document.getElementById('executeFindRequest').addEventListener('click',function (){
//     GetCount();
// })


