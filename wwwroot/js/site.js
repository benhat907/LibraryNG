// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
let selectLB = null;
let selectLBDelete = null;
let isAdd = true; 
const ldel = (maloi) => {
    $("#modalDelete").modal("show");
    selectLBDelete = maloi;
};
const ledit  = (maloi, tenloi, phanloaing, chuthich) => {
    $("#myModal").modal("show");
    selectLB = maloi;
    $("#maLoi").val(maloi);
    $("#tenLoi").val(tenloi);
    $("#plng").val(phanloaing);
    $("#chuThich").val(chuthich);
    isAdd = false;
};
$("#btn_add").click(() => {
    $("#myModal").modal("show");
    isAdd = true;
});
$("#btnLogin").click(() => {
    $.ajax({
        type: "POST",
        url: "/Account/LoginMethod",
        data: {
            Email: $("#email_login").val(),
            MatKhau: $("#password_login").val(),    
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
    if(isAdd === true)
    {
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
            },  
            error: function (res) { console.log(res.resault) },
        })
    }
    else
    {
        $.ajax({
            type: "POST",
            url: "/Home/UpdateMethod",
            data:{
                MaLoi: selectLB,
                TenLoi: $("#tenLoi").val(),
                PhanLoaiNG: $("#plng").val(),
                Chuthich: $("#chuThich").val()
            },
            dataType: "json",
            success: function (res) {
                if (res.resault == "ok"){
                    $("#myModal").modal("hide");
                    ShowNotification("Lưu thành công");
                    window.location.href = "/Home/DashBoard";
                }
                else {
                    $("#myModal").modal("hide");
                    ShowNotification("Lưu thất bại");
                    window.location.href = "/Home/DashBoard";
                }
            },
            error: function (res) { console.log(res.resault) },
        })
    }
});
$("#btn_delete").click(()=>{
    $("#modalDelete").modal("show");
});
$("#btn_delete_ok").click(()=> {
    $.ajax({
        type: "POST",
        url: "/Home/DeleteMethod",
        data: {
            MaLoi: selectLBDelete
        },
        dataType: "json",
        success: function(res){
            if(res.resault == "ok"){
                window.location.href = "/Home/DashBoard";
            }
            else {
                window.location.href = "/Home/DashBoard";
            }
        },
        error: function (res) { console.log(res.resault) },
    })
});
$("#btn_search").click(()=>{
    let url = "/Home/DashBoard?giatritimkiem="+$("#txt_search").val();
    location.href = url;
});
