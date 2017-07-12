﻿
function AddNews(event) {
    event.preventDefault();
    var NewsTitle = txtNewsTitle.value;
    var newsDesc = txtNewsDesc.value;
    var IsNotify = document.getElementById("IsNotify").checked;
    var file = "";
    var FileUrl = "";
    var isFileFromUrl = false;
    var newsCat = Optcategories.value;
    //========================================================================================
    var x = document.getElementById("newsImg");
    if (x != null) {
        if ('files' in x) {
            if (x.files.length == 0) {
                isFileFromUrl = true;
                FileUrl = document.getElementById("newsImgUrl").value;
            } else {
                file = x.files[0];
                var FileName = file.name;
                var size = file.size;
            }
        }
        else {
            isFileFromUrl = true;
            FileUrl = document.getElementById("newsImgUrl").value;

            //getfile from URL
        }
    } else {
        isFileFromUrl = true;
        FileUrl = document.getElementById("newsImgUrl").value;

        //getfile from URL
    }

    var fileData = new FormData();
    fileData.append(FileName, file);
    fileData.append("CategoryId", newsCat);
    fileData.append("NewsTitle", NewsTitle);
    fileData.append("NewsDescription", newsDesc);
    fileData.append("FileUrl", FileUrl);
    fileData.append("IsNotify", IsNotify);

    $.ajax({
        url: "/Home/AddNews",
        cache: false,
        type: "POST",
        contentType: false, // Not to set any content header
        processData: false, // Not to process data
        data: fileData,
        beforeSend: function () {
            $("#loaddingModal").show();
        },
        complete: function () {
            $("#loaddingModal").hide();
        },
        success: function (data) {
            if (data == null) {
                alert("Error While adding news!");
            } else {
                alert("News added successfully!");
                $("#addNewsForm").get(0).reset();
            }
        },
        error: function (xhr, status) {
            alert("Exception While adding news!");

        }
    })
}

function LoadNewsList() {
    $.ajax({
        url: "/Home/NewsList",
        cache: false,
        type: "POST",

        beforeSend: function () {
            $("#loaddingModal").show();
        },
        complete: function () {
            $("#loaddingModal").hide();
        },
        success: function (data) {
            $("#menu_NewsList").toggleClass("active");
            $("#menu_AddNews").removeClass("active");
            AddNewsDiv.innerHTML = "";
            PartialViewDiv.innerHTML = data;
        },
        error: function (xhr, status) {
            alert("Exception While Populating news list!");

        }
    })
}

function ApproveNews(NewsId) {
    debugger;
    $.ajax({
        url: "/Home/ApproveNews/",
        cache: false,
        type: "POST",
        data:{
            NewsId:NewsId
        },
        beforeSend: function () {
            $("#loaddingModal").show();
        },
        complete: function () {
            $("#loaddingModal").hide();
        },
        success: function (data) {
            debugger;
            if (data == 'True') {
                $("#NewsButtons_" + NewsId).html('<span style="color:green">Approved</span>');
            }
        },
        error: function (xhr, status) {
            alert("Exception While Populating news list!");

        }
    })
}

function LoadCreateUserView() {
    $.ajax({
        url: "/Home/CreateUser",
        cache: false,
        type: "POST",

        beforeSend: function () {
            $("#loaddingModal").show();
        },
        complete: function () {
            $("#loaddingModal").hide();
        },
        success: function (data) {
            $("#menu_NewsList").removeClass("active");
            $("#menu_AddNews").removeClass("active");
            $("#menu_AddAdmin").toggleClass("active");

            AddNewsDiv.innerHTML = "";
            PartialViewDiv.innerHTML = data;
        },
        error: function (xhr, status) {
            alert("Exception While Populating news list!");

        }
    })
}
function CreateNewUser(e) {
    event.preventDefault();
    var UserId = txtUserId.value;
    var Password = txtPassword.value;
    var ConfirmPassword = txtConfirmePassword.value;
    var FirstName = txtFirstName.value;
    var LastName = txtFirstName.value;
    var Email = txtEmail.value;
    var Mobile = txtMobile.value;
    var Gender = $("#OptGender").val();
    var UserType = $("#OptUserType").val();
    
    debugger;
    switch (Gender) {
        case '1': Gender = 'Male'; break;
        case '2': Gender = 'Female'; break;
        default: Gender = 'Male';
    }

    $.ajax({
        url: "/Home/CreateNewUser",
        cache: false,
        type: "POST",
        data: {
            UserId: UserId,
            Password: Password,
            FirstName: FirstName,
            LastName: LastName,
            Email: Email,
            Mobile: Mobile,
            Gender: Gender,
            UserType:UserType
        },
        beforeSend: function () {
            $("#loaddingModal").show();
        },
        complete: function () {
            $("#loaddingModal").hide();
        },
        success: function (data) {
            $("#menu_NewsList").removeClass("active");
            $("#menu_AddNews").removeClass("active");
            $("#menu_AddAdmin").toggleClass("active");

            AddNewsDiv.innerHTML = "";
            PartialViewDiv.innerHTML = data;
        },
        error: function (xhr, status) {
            alert("Exception While Populating news list!");

        }
    })
}