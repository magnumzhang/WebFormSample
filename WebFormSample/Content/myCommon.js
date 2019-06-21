function ajax_call_action(ActionURL, JSonPostDataStr, CallBackMethod) {
    var paraName = "JSonPostDataStr";
                    
    $.ajax({
        url: ActionURL,
        async: false,
        type: "POST",
        data: "{'" + paraName + "':" + " '" + JSonPostDataStr + "' " + "}",
        contentType: "application/json; charset=utf-8",
        //# MUST # declare data type is json,otherise can not call the server method.
        dataType: "json",

        success: function (data) {
            CallBackMethod(data);
        }
    });
}

