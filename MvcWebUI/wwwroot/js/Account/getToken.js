/*document.getElementById('sendRequestButton').addEventListener('click', function() {
    var alinanVeri = localStorage.getItem("myToken");

    var jsonData = { myToken: alinanVeri };

    fetch('http://localhost:5129/post/sendtoken', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(jsonData)
    })
        .then(response => response.json())
        .then(data => {
            console.log('Controller Yanıtı:', data);
        })
        .catch(error => {
            console.error('Hata:', error);
        });
});*/