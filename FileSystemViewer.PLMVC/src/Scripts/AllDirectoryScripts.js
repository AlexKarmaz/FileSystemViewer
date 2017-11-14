import $ from 'jquery';


var directoryeMgr = new DirectoryMgr();

$(document).ready(function () {
    directoryeMgr.hangDblClick(directoryeMgr.onSuccess);

    directoryeMgr.backArrowClick(directoryeMgr.onSuccess);
});

function DirectoryMgr(){
     var self = this;

    self.hangDblClick = function (callback){
        $(".folder").dblclick(function () {
            var url = self.getPath(this.id);
            url = encodeURI(url);

            $.ajax({
                url: url,
                type: "GET",
                success: function (result) {
                    callback(result);
                },
                error: function (){
                    alert('Oops, something bad happened. We workikg with the problem. Try it again. Description: ' + errorThrown + '.');
                }
            });
        });
     };

    self.getPath  = function(id){
        var path = $(".pathPlace").text();
        path = path.substr(13, path.length);
        var index = id.indexOf("-") + 1;
        var newPath = path + id.substr(index, id.length);
        return "/Directory/GetAllDirectory/" + newPath;
     };

    self.onSuccess = function(result){
        if (result.Status == 'NotAcceptable') {
             alert("You don't have access to the directory");
        } else {
            $('.item').unbind();
            $('#main_container').html(result);
        }
    };

    self.getLastPath = function (){
        var path = $('#lastPath').text().trim();
        path = path.substr(0, path.length - 1);
        return "/Directory/GetAllDirectory/" + path;
    };

    self.backArrowClick = function(callback){
        $("#back-image").click(function () {
            var url = self.getLastPath();
            url = encodeURI(url);

            $.ajax({
                url: url,
                type: "GET",
                success: function (result) {
                    callback(result);
                },
                error: function (){
                    alert('Oops, something bad happened. We workikg with the problem. Try it again. Description: ' + errorThrown + '.');
                }
            });
        });
    };
};

export default DirectoryMgr;