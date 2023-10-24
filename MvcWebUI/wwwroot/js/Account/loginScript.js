function Login(){
    var data = {
        UserForLoginDto: {
            LoginInfo: $('#loginInfo').val(),
            Password: $('#loginPassword').val()
        }
    };
    $.ajax({
        url: 'http://localhost:5015/api/Auth/login',
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(data),
        success: function (response) {
            console.log('First successful response:', response);
            localStorage.setItem("token",JSON.stringify(response))
            
            $.ajax({
                url: 'http://localhost:5129/Token/sendtoken',
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(localStorage.getItem("token")),
                success: function (secondResponse) {
                    console.log('Second successful response:', secondResponse);
                    window.location.href = "http://localhost:5129/Post/Index";
                },
                error: function (secondResponse) {
                    console.error('Second unsuccessful response:', secondResponse);
                }
            });
        },
        error: function (x,y,z){

        }
    });
}
