const searchInput = document.getElementById('searchInput');
const dropdownMenu = document.querySelector('.dropdown-menu');

searchInput.addEventListener('input', function () {
    const searchTerm = searchInput.value.trim();

    if (searchTerm === '') {
        dropdownMenu.innerHTML = '';
        dropdownMenu.classList.remove('show');
    } else {
        var data = JSON.stringify(searchTerm);

        $.ajax({
            url: 'http://localhost:5015/api/User/getSearchedUsers?userName=' + data,
            type: 'GET',
            headers: {
                "Content-Type": "application/json"
            },
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(data),
            success: function (response) {
                console.log(response);
                updateDropdownContent(response);
            },
            error: function (x, y, z) {

            }
        });
    }
});

searchInput.addEventListener('blur', function () {
    if (searchInput.value.trim() === '') {
        dropdownMenu.innerHTML = '';
        dropdownMenu.classList.remove('show');
    }
});

function updateDropdownContent(response) {
    const results = response.slice(0, 8);

    dropdownMenu.innerHTML = '';

    results.forEach(result => {
        const item = document.createElement('div');
        item.classList.add('dropdown-item');
        item.style.position = 'relative';
        item.style.width = '280px';
        item.style.touchAction = 'none';
        item.style.paddingLeft = '10px'

        const profileImage = document.createElement('img');
        profileImage.src = result.profilePhoto;
        profileImage.alt = result.firstName;
        profileImage.style.width = '25px';
        profileImage.style.height = '25px';
        profileImage.style.borderRadius = '50%';
        profileImage.style.marginRight = '10px';
        profileImage.style.border = '#ff7400 1px';
        item.appendChild(profileImage);

        const textContainer = document.createElement('span');
        textContainer.textContent = result.firstName + ' ' + result.lastName;
        item.appendChild(textContainer);

        var element = document.getElementById('dropdown');

        var userId = element.getAttribute('data-user-id');

        var friends;

        $.ajax({
            url: `http://localhost:5015/api/Friend/checkFriend?userId=${userId}&friendId=${result.id}`,
            type: 'GET',
            contentType: 'application/json; charset=utf-8',
            success: function (response) {
                friends = response;

                if (userId != result.id) {
                    var createButton = true;

                    if (createButton) {
                        const button = document.createElement('button');
                        button.style.background = 'linear-gradient(to bottom, #f15805, #b6511b, #623d2f)';
                        button.style.borderRadius = '10px';
                        button.style.color = 'white';
                        button.style.border = 'none';
                        button.style.position = 'absolute';
                        button.style.height = '30px';
                        button.style.width = '30px';
                        button.style.right = '0';
                        button.style.top = '0';
                        button.style.marginRight = '10px'
                        button.innerHTML = '<i class="fa-solid fa-user-plus"></i>';

                        item.appendChild(button);

                        button.addEventListener('click', function (event) {
                            event.stopPropagation();

                            var token = JSON.parse(localStorage.getItem('token'));

                            var requestData = {
                                ReceiverId: result.id,
                                Token: token.token.token
                            };

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
                                        data: JSON.stringify(result.id),
                                        success: function (response) {
                                            console.log('Successful response:', response);

                                            const profilePhoto = result.profilePhoto;
                                            const notificationContent = `${result.firstName} ${result.lastName} has sent you a friend request.`;
                                            const notificationDate = 'Just now.'
                                            const senderId = userId.toString();
                                            const receiverId = result.id.toString();

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
                        });
                    }
                }
            },
            error: function (x, y, z) {

            }
        });


        dropdownMenu.appendChild(item);

        textContainer.style.cursor = 'pointer';

        textContainer.addEventListener('click', function () {
            console.log('İçeriğe tıklandı: ' + result.firstName + ' ' + result.lastName);
            console.log('PP link: '+result.profilePhoto);
        });
    });

    dropdownMenu.classList.add('show');
}