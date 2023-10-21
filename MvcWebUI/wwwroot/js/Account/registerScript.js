function Signup(){
    var data = {
    UserForRegisterDto: {
    FirstName: $('#firstName').val(),
    LastName: $('#lastName').val(),
    UserName: $('#userName').val(),
    Email: $('#email').val(),
    Password: $('#password').val()
},
    ConfirmedPassword: $('#confirmPassword').val()
};

    $.ajax({
    url: 'http://localhost:5015/api/Auth/register',
    type: 'POST',
        contentType: 'application/json; charset=utf-8',
    data: JSON.stringify(data),
    success: function (response) {
    console.log('Successful response:', response);

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
