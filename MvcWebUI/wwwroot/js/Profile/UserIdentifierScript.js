const receivedToken = JSON.parse(localStorage.getItem("token"));
const token = receivedToken.token.token;

function GetUrlParameter(){
    const queryString = window.location.search;
    const urlParams = new URLSearchParams(queryString);
    return urlParams.get('userName');
}

async function GetCurrentUserInformation(token) {
    try {
        const response = await $.ajax({
            url: 'http://localhost:5015/api/User/getUserByToken?token=' + token,
            method: 'GET',
            headers: {
                "Content-Type": "application/json",
                "Authorization": "Bearer " + token
            }
        });

        return JSON.stringify(response);
    } catch (error) {
        console.log(error);
        return null;
    }
}

async function GetUserInformation(userName, token) {
    try {
        const response = await $.ajax({
            url: 'http://localhost:5015/api/User/getUserByUserName?userName=' + userName,
            method: 'GET',
            headers: {
                "Content-Type": "application/json",
                "Authorization": "Bearer " + token
            }
        });

        return JSON.stringify(response);
    } catch (error) {
        console.log(error);
        return null;
    }
}


function SetProfileFullName(firstName, lastName){
    const fullNameContainer = document.getElementById('profileChoicesFullName');
    fullNameContainer.textContent = `${firstName} ${lastName}`;
}

function GetProfileSettings(currentId,userId){
    if (currentId === userId){
        const profileSettingsContainer = document.getElementById('profileSettings');
        profileSettingsContainer.innerHTML = 
            `
                <button type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                        <i class="fa-solid fa-gear"></i>
                    </button>
                    <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                        <a class="dropdown-item" href="#">Action</a>
                        <a class="dropdown-item" href="#">Another action</a>
                        <a class="dropdown-item" href="#">Something else here</a>
                    </div>
            `;
    }
}

function GetProfileChoices(currentId,userId){
    if (currentId !== userId){
        const profileChoices = document.getElementById('profileChoices');
        $.ajax({
            url: `http://localhost:5015/api/FriendRequest/getRequestStatus?senderId=${currentId}&receiverId=${userId}`,
            method: 'GET',
            success: function (response) {
                if (response === '0'){
                    profileChoices.innerHTML =
                        `
                    <button>
                        Send Message
                    </button>
                    <div>

                    </div>
                    <button id="friendRequestButton">
                        Send Request
                    </button>
                    `;
                }
                if (response === '1'){
                    profileChoices.innerHTML =
                        `
                    <button>
                        Send Message
                    </button>
                    <div>

                    </div>
                    <button id="friendRequestButton">
                        Cancel Request
                    </button>
                    `;
                }
                if (response === '2'){
                    profileChoices.innerHTML =
                        `
                    <button>
                        Send Message
                    </button>
                    <div>

                    </div>
                    <button id="friendRequestButton">
                        Delete Friend
                    </button>
                    `;
                }
            },
            error: function (error) {
                console.log(error);
            }
        });
    }
}

function GetProfilePhoto(photoUrl){
    const photoContainer = document.getElementById('mainProfilePhoto');
    photoContainer.src = photoUrl;
}

async function SetAllIdentifiers(){
    const currentUserData = await GetCurrentUserInformation(token);
    const currentUser = JSON.parse(currentUserData);
    const currentUserId = currentUser.id;
    
    const userData = await GetUserInformation(GetUrlParameter(),token);
    const user = JSON.parse(userData);
    const userId = user.id;
    const firstName = user.firstName;
    const lastName = user.lastName;
    const photoUrl = user.profilePhoto;
    
    SetProfileFullName(firstName, lastName);
    
    GetProfileSettings(currentUserId, userId);
    
    GetProfileChoices(currentUserId, userId);
    
    GetProfilePhoto(photoUrl);
    
    GetPosts(userId);
}

SetAllIdentifiers();