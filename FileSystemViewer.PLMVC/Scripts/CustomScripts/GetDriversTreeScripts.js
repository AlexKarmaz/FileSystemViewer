$(document).ready(function () {
    $(".treeDriveItem").dblclick(function () {
        var path = $('#lastTreePath').text().trim();
        var index = this.id.indexOf("-") + 1;
        var newPath = path + this.id.substr(index, this.id.length) + "\\";
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

    $('.treeDriveChanger').click(function () {
        var name = this.id.toString();
        if ($('#treeConteiner-' + name).hasClass('hidden').toString() == "false") {
            $('#treeConteiner-' + name).empty();
            $('#treeConteiner-' + name).toggleClass('hidden');
        } else {
            $('#treeConteiner-' + name).toggleClass('hidden');
            $('#treeConteiner-' + name).css('margin-left', '+15px');

            var path = $('#lastTreePath').text().trim();
            var newPath = path + name;
            var url = "/FolderTree/GetAllDirectory/" + newPath;
            url = encodeURI(url);

            $.ajax({
                url: url,
                type: "GET",
                success: function (result) {
                    if (result.Status == 'NotAcceptable') {
                        alert("You don't have access to the directory");
                    } else {
                        $('#treeConteiner-' + name).html(result);
                    }
                },
                error: OnError
            });
        }
    });

});