
GetURl();
function GetURl() {
    $.ajax({
        url: '/View_LoginSocial/GetUrl',
        method: 'GET',
        success: function (response) {
            $("#urlID").val(response);
        }
    });

}
$(".btnLogin").click(function () {

    var data={
        username: $("#txtUserNameLogin").val(),
        password: $("#txtPasswordLogin").val(),
        grant_type: 'password'
    }
    Login(data)
})
$("#btnLoginP").click(function () {
    var data = {
        username: $("#txtUserNameLoginP").val(),
        password: $("#txtPasswordLoginP").val(),
        grant_type: 'password'
    }
    Login(data)
})
function Login(data) {
    var url = $("#urlID").val() + "token";
    $.ajax({
        url: url,
        method: "POST",
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: data,
        success: function (response) {
            localStorage.setItem('accessToken', response.access_token);
            window.location.href = "/";
        },
        error: function (jqXHR) {
            alert(jqXHR.responseText);
        }
    })

}
function gettokenLogin(accessToken) {
    var url = $("#urlID").val() + "/View_Account/GetTokenLogin";
    $.ajax({
        url: url,
        method: "GET",
        data: { "Token": accessToken},
        success: function (response) {
        
        },
        error: function (jqXHR) {
            alert(jqXHR.responseText);
        }
    })
}
$("#btnRegister").click(function () {
    var url = $("#urlID").val() + "api/Account/register";
    var data = {
        Email: $("#txtEmailRegister").val(),
        Password: $("#txtPasswordRegister").val(),
        ConfirmPassword: $("#txtCFPasswordRegister").val(),
        LastName: $("#txtLastNameRegister").val(),
        FirstName: $("#txtFirstNameRegister").val(),
    }
    $.ajax({
        url: url,
        method: "POST",
        data: data,
        success: function (response) {
            alert("Tạo mới thành công")
            window.location.href = "/View_PLayout/login";
        },
        error: function (jqXHR) {
            alert(jqXHR.responseText);
        }
    })
})
$("#btnLogoff").click(function () {
    var url = $("#urlID").val() + "api/Account/logout";
    $.ajax({
        url: url,
        method: "POST",
        success: function (response) {
            window.location.href = "/";
        },
        error: function (jqXHR) {
            alert(jqXHR.responseText);
        }
    })
})
$("#btnLogoffmobile").click(function () {

    var url = $("#urlID").val() + "api/Account/logout";
    $.ajax({
        url: url,
        method: "POST",
        success: function (response) {
            window.location.href = "/";
        },
        error: function (jqXHR) {
            alert(jqXHR.responseText);
        }
    })
})


$("#btnshowChangePassword").click(function () {
    $("#lishowChangepassword").removeClass('hide');
})
$("#btnHuyChangePassword").click(function () {
    $("#lishowChangepassword").addClass('hide');
})
$("#btnChangePassword").click(function () {
    var tk = localStorage.accessToken;
    var data = {
        OldPassword: $("#txtPasswordOld").val(),
        NewPassword: $("#txtPasswordNew").val(),
        ConfirmPassword: $("#txtConfirmPassword").val(),
    }
    var url = $("#urlID").val() + "/View_Account/ChangePassword";
    $.ajax({
        url: url,
        method: "POST",
        data: { "model": data, "token": tk },
        success: function (response) {
            $("#messageChangePass").html(response);
        },
        error: function (jqXHR) {
            alert(jqXHR.responseText);
        }
    })
})