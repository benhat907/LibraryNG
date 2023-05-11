// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$("#btnLogin").click(() => {
    $.ajax({
        type: "POST",
        url: "/Account/LoginMethod",
        data: {
            Email: $("#email_login").val(),
            Password: $("#password_login").val(),
        },
        dataType: "json",
        success: function (res) {
            if (res.resault == "ok") {
                window.location.href = "/Home/Index";
            }
            else {
                alert("Đăng nhập không thành công");
            }
        },
        error: function (res) { console.log(res); }
    }).then((res) => {
        console.log(res);
    })
})
$("#btn_save").click(() => {
    $.ajax({
        type: "POST",
        url: "/Home/SaveMethod",
        data: {
            MaLoi: $("#maLoi").val(),
            TenLoi: $("#tenLoi").val(),
            PhanLoaiNG: $("#plng").val(),
            Chuthich: $("#chuThich").val()
        },
        dataType: "json",
        success: function (res) {
            if (res.resault == "ok") {
                window.alert("Success");
                window.location.href = "/Home/DashBoard";
            }
            else {
                alert("Thêm thất bại");
            }
        },  
        error: function (res) { console.log(res.resault) },
    })
});




