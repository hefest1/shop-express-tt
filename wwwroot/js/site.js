function updateNotificationsClickHandler() {
    $(".notification-item").off("click");

    $(".notification-item").each(function() {
        $(this).on("click", function() { $(this).remove() });
    });
}

function processServerErrors(req) {
    var notificationView = document.createElement("div");
    notificationView.className = "notification-item";
    var messageElement = document.createElement("p");
    messageElement.innerText = req.statusText;
    $(messageElement).prependTo(notificationView);
    $(notificationView).prependTo($("#notifications"));

    setTimeout(function() { $(notificationView).remove() }, 5000);

    updateNotificationsClickHandler();
}

function sendRequest(url, type, data, successHandler, errorHandler) {
    $.ajax({
        type: type,
        url: url,
        data: data,
        success: function (result) {
            if (successHandler != null)
                successHandler(result);
        },
        error: function (req) {
            if (errorHandler != null)
                errorHandler(req);
            processServerErrors(req);
        }
    });
}

function formatDate(date) {
    return date.getDate()
        + "." +
        date.getMonth()
        + "." +
        date.getYear()
        + " " +
        date.getHours()
        + ":" +
        date.getMinutes()
        + ":" +
        date.getSeconds();
}