$(document).ready(function () {

    $(".drive").dblclick(function () {
        var url = getPath.call(this);
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

    function getPath() {
        var path = $(".pathPlace").text();
        path = path.substr(13, path.length);
        var index = this.id.indexOf("-") + 1;
        return "/Directory/GetAllDirectory/" + path + this.id.substr(index, this.id.length);
    };
});
