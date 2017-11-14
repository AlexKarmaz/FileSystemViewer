$(document).ready(function () {
    $(".folder").dblclick(function () {
        var path = $(".pathPlace").text();
        path = path.substr(13, path.length);
        var index = this.id.indexOf("-") + 1;
        var newPath = path + this.id.substr(index, this.id.length);
        var url = "/Directory/GetAllDirectory/" + newPath;
        url = encodeURI(url);
        $.ajax({
            url: url,
            type: "GET",
            success: function (result) {
                if (result.Status == 'NotAcceptable') {
                    alert("You don't have access to the directory");
                } else {
                    $('.item').unbind();
                    $('#main_container').html(result);
                }
            },
            error: OnError
        });
    });

    $("#back-image").click(function () {
        var path = $('#lastPath').text().trim();
        path = path.substr(0, path.length - 2);
        var lastPathIndex = path.lastIndexOf("\\");
        path = path.substr(0, lastPathIndex);
        var url = "/Directory/GetAllDirectory/" + path;
        url = encodeURI(url);
        $.ajax({
            url: url,
            type: "GET",
            success: function (result) {
                if (result.Status == 'NotAcceptable') {
                    alert("You don't have access to the directory");
                } else {
                    $('.item').unbind();
                    $('#main_container').html(result);
                }
            },
            error: OnError
        });
    });
});