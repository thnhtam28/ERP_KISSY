
//GetAccessToken();
//var urlpage = window.location.href;
//$(".btnLoginFacebook").click(function () {

//    window.location.href = urlpage + 'api/Account/ExternalLogin?provider=Facebook&response_type=token&client_id=self&redirect_uri=' + urlpage+'&state=wGMJI5MQwtoTauSFxM8oqZUXgNEI0qdPBvV4LU1hdmY1';
//})
//$(".btnLoginGoogle").click(function () {
//    var urlpage = window.location.href;
//    window.location.href = urlpage + 'api/Account/ExternalLogin?provider=Google&response_type=token&client_id=self&redirect_uri=' + urlpage+'&state=wGMJI5MQwtoTauSFxM8oqZUXgNEI0qdPBvV4LU1hdmY1';
//})


//function GetAccessToken() {
//    if (location.hash) {
//        if (location.hash.split('access_token=')) {
//            var accesstoken = location.hash.split('access_token=')[1].split('&')[0];
//            //alert(accesstoken);
//            //isUserRegistered(accesstoken);
//            if (accesstoken) {
                
//                isUserRegistered(accesstoken);
//            }
//        }
//    }
   
//}
//function isUserRegistered(accesstoken) {
//    //alert("vao day")
//    $.ajax({
//        url: 'api/Account/UserInfo',
//        method: 'GET',
//        headers: {
//            'content-type': 'application/JSON',
//            'Authorization': 'Bearer ' + accesstoken,
//        },
//        success: function (response) {
//            if (response.HasRegistered) {
//                localStorage.setItem('accessToken', accesstoken);
//                localStorage.setItem('UserName', response.Email);
//                window.location.href = 'home/index';
//            }
//            else {
//                signupExternalUser(accesstoken, response.LoginProvider);
//            }
//        }
//    });
//}
function signupExternalUser(accesstoken, LoginProvide) {
    var url = $("#urlID").val() + "api/Account/RegisterExternal";
    $.ajax({
        url: url,
        method: 'POST',
        headers: {
            'content-type': 'application/JSON',
            'Authorization': 'Bearer ' + accesstoken
        },
        success: function () {
            GetProvide(LoginProvide);
        }
    });
}

GetAccessToken();
$(".btnLoginFacebook").click(function () {
    GetProvide("Facebook");
    
})
$(".btnLoginGoogle").click(function () {
    GetProvide("Google");

})
function GetProvide(provide) {
    var url = $("#urlID").val() + "View_LoginSocial/GetProvide";
    $.ajax({
        url: url,
        method: 'GET',
        headers: {
            'content-type': 'application/JSON',
        },
        data: { "provide": provide },
        success: function (response) {

            window.location.href = response;
        }
    });
}

function GetAccessToken() {
    if (location.hash) {
        if (location.hash.split('access_token=')) {
            var accesstoken = location.hash.split('access_token=')[1].split('&')[0];
            if (accesstoken) {
                isUserRegistered(accesstoken);
            }
        }
    }

}
//function isUserRegistered(accesstoken) {
//    //alert("vao day")
//    $.ajax({
//        url: 'api/Account/UserInfo',
//        method: 'GET',
//        headers: {
//            'content-type': 'application/JSON',
//            'Authorization': 'Bearer ' + accesstoken,
//        },
//        success: function (response) {
//            if (response.HasRegistered) {
//                localStorage.setItem('accessToken', accesstoken);
//                localStorage.setItem('UserName', response.Email);
//                window.location.href = 'home/index';
//            }
//            else {
//                signupExternalUser(accesstoken, response.LoginProvide);
//            }
//        }
//    });
//}

function isUserRegistered(accesstoken) {
    var url = $("#urlID").val() + "View_LoginSocial/GetUserInfo";
    //alert("vao day")
    $.ajax({
        url: url,
        method: 'GET',
        type: "JSON",
        headers: {
            'content-type': 'application/JSON',
        },
        data: { "token": accesstoken },
        success: function (response) {
            if (response.HasRegistered) {

                localStorage.setItem('accessToken', accesstoken);
                localStorage.setItem('UserName', response.Email);
                window.location.href = 'home/index';
            }
            else {

                signupExternalUser(accesstoken, response.LoginProvider);
            }
        }
    });
}
//function signupExternalUser(accesstoken, LoginProvide) {
//    $.ajax({
//        url: '/View_LoginSocial/SingupUser',
//        method: 'GET',
//        headers: {
//            'content-type': 'application/JSON',
//        },
//        data: { "token": accesstoken, "provide": LoginProvide },
//        success: function (result) {
//            alert(result);
//            window.location.href = result;
//        }
//    });
//}