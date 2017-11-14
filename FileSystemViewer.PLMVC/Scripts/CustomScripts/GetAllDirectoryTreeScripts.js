function signForEvents(itemId, itemName, lastPath) {
    $(document).ready(function () {
        $('#treeItemFolder-' + itemId).dblclick(function () {
            var name = itemName;
            var path = lastPath;
            var newPath = path + name + "\\";
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

        $('#treeChangerFolder-' + itemId).click(function () {
            debugger;
            var name = itemName;
            if ($('#treeConteiner-' + itemId).hasClass('hidden').toString() == "false") {
                $('#treeConteiner-' + itemId).empty();
                $('#treeConteiner-' + itemId).toggleClass('hidden');
            } else {
                $('#treeConteiner-' + itemId).toggleClass('hidden');
                $('#treeConteiner-' + itemId).css('margin-left', '+15px');

                var path = lastPath;
                var newPath = path + name + "\\";
                var url = "/FolderTree/GetAllDirectory/" + newPath;
                url = encodeURI(url);
                $.ajax({
                    url: url,
                    type: "GET",
                    success: function (result) {
                        if (result.Status == 'NotAcceptable') {
                            alert("You don't have access to the directory");
                        } else {
                            $('#treeConteiner-' + itemId).html(result);
                        }
                    },
                    error: OnError
                });
            }
        });

    });

}