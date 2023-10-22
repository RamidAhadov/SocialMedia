//For message scroll

window.addEventListener('load', function () {

  var div = this.document.getElementById('messaging-messages')

  div.scrollTop = div.scrollHeight;
});

//For minimize chat window

var isMinimized = false;

function Minimize() {
  var chatDiv = document.getElementById('chatContainer');
  var icon = document.getElementById('icon');
  
  if (isMinimized) {
    chatDiv.style.bottom = '60px';
    icon.className = 'fa-solid fa-window-minimize'
  } else {
    chatDiv.style.bottom = '-275px';
    icon.className = 'fa-solid fa-window-maximize'
  }

  isMinimized = !isMinimized;
}

var isHidden = true;

function CloseChat(){
  var chatDiv = document.getElementById('chatContainer');

  if(chatDiv.style.display === 'block'){
    chatDiv.style.display = 'none'
  }
}

//Comment like button hover

const elements = document.querySelectorAll('.like-comment');

function handleMouseOver(event) {
  const element = event.target;
  const elementId = element.id;
  const hoverElement = document.getElementById(elementId);
  var className = hoverElement.className;
  var buttonColor = hoverElement.style.color;

  if(className === 'like-comment fa-solid fa-heart fa-xl' && buttonColor === 'red'){
    hoverElement.className = 'like-comment fa-regular fa-heart fa-xl';
    hoverElement.style.color = 'black';
  }
  else{
    hoverElement.className = 'like-comment fa-solid fa-heart fa-xl';
    hoverElement.style.color = 'red';
  }
}

// Mouse üzerine gelme olayını dinle
elements.forEach(element => {
  element.addEventListener('mouseover', handleMouseOver);
});

function handleMouseOut(event) {
  const element = event.target;
  const elementId = element.id;
  const hoverElement = document.getElementById(elementId);
  var className = hoverElement.className;
  var buttonColor = hoverElement.style.color;

  if(className === 'like-comment fa-solid fa-heart fa-xl' && buttonColor === 'red'){
    hoverElement.className = 'like-comment fa-regular fa-heart fa-xl';
    hoverElement.style.color = 'black';
  }
  else{
    hoverElement.className = 'like-comment fa-solid fa-heart fa-xl';
    hoverElement.style.color = 'red';
  }
}

// Mouse üzerinden çıkma olayını dinle
elements.forEach(element => {
  element.addEventListener('mouseout', handleMouseOut);
});

//User is liked or not
function ShowIsLiked() {
  const receivedToken = JSON.parse(localStorage.getItem("token"));
  const token = receivedToken.token.token;

  elements.forEach(element => {
    const elementId = element.id;
    var data = {
      CommentId: elementId,
      Token: token
    }
    var likeButton = document.getElementById(elementId);
      $.ajax({
        url: 'http://localhost:5015/api/Like/likeCheck',
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(data),
        success: function (response) {
          console.log(elementId + response);
          if (response === "Liked") {
            likeButton.className = 'like-comment fa-solid fa-heart fa-xl';
            likeButton.style.color = 'red';
          }
          if (response === "Not liked") {
            likeButton.className = 'like-comment fa-regular fa-heart fa-xl';
            likeButton.style.color = 'black';
          }
        },
        error: function (x, y, z) {

        }
      });
  });
}




