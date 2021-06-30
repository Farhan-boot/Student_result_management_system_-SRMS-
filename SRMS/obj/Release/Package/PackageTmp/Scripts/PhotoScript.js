//for photo
var loadFile = function(event) {
    var output = document.getElementById('output');
    output.src = URL.createObjectURL(event.target.files[0]);
};

//Form post
function jQueryAjaxPost(form) {
    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        var ajaxConfig = {
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            success: function (response) {
                if (response.success) {
                   //alert(response.responseText);
                    $.notify(response.responseText, "success");
                    $('#MyForm')[0].reset();         
                    //$("#firstTab").html(response.html);
                  //if (typeof activatejQueryTable !== 'undefined' && $.isFunction(activatejQueryTable))
                  //activatejQueryTable();
                   
                }
                else {
                    $.notify(response.message,"error");
                }
            }
        }
        if ($(form).attr('enctype') == "multipart/form-data") {
            ajaxConfig["contentType"] = false;
            ajaxConfig["processData"] = false;
        }
        $.ajax(ajaxConfig);

    }
    return false;
}

//Form Delete
function Delete(url) {
    $.ajax({
        type: 'POST',
        url: url,

        success: function (result) {
           // window.location.reload(true);
            if (result.success) {
                LoadList();
                $.notify(result.responseText, "info");
            }
            // console.log("Jesy test" + result);
        }

    });
}

//AjaxAtten
function AjaxAtten(url) {
    $.ajax({
        type: 'GET',
        url: url,
         success: function (result) {
          //  window.location.reload(true);
            alert("Success Attendence");
           
        }

    });
}

$(document).ready(function () {
    LoadList();
});


//Load List
var LoadList = function (data) { 
    $.get('../Student/GetStdList', function (result) {
        console.log(result);
        $.each(result, function (i, row) {
            console.log(row);
           // var rowTr = "<tr id='std'" + row.ID + "><td>" + (i + 1) + "</td><td>" + (row.ID + 1) + "</td><td>" + (row.Name + 1) + "</td><td>" + row.Dept + "</td></tr>";
           // $("#list").append(rowTr);
            });
    })
    //$.getJSON("Student/GetStdList", function (result) {
    //    $.each(result, function (i, field) {
    //        $("div").append(field + " ");
    //    });
    //});
    //$.ajax({
    //    type: "GET",
    //    url: "../Student/GetStdList",
    //    contentType: "application/json; charset=utf-8",
    //    dataType: "json",
    //    success: function (data) {

    //        console.log(stringify(data));
    //       //alert("Why Faild " + stringify(result));
    //       // alert("Success " + result);
    //    },
    //    error: function (ex) {
    //        alert("exseption " + ex.message);
    //    }
    //});

}

var requrl = '@Url.Action("GetStdList", "Student", null, Request.Url.Scheme, null)';
//$.ajax({
//    type: "POST",
//    url: requrl,
//    data: "{queryString:'" + searchVal + "'}",
//    contentType: "application/json; charset=utf-8",
//    dataType: "JSON",
//    success: function (data) {
//        alert("here" + data.d.toString());
//    }
//});



function LoadStd(id) {
    document.getElementById('modalid').value = id;

  
}






