import $ from 'jquery';

var searchMgr = new SearchMgr();

$(document).ready(function () {
    searchMgr.hangDblClick(searchMgr.onSuccess);
    searchMgr.backArrowClick(searchMgr.onSuccess);
});


function SearchMgr() {
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
                error:function (){
                    alert('Oops, something bad happened. We workikg with the problem. Try it again. Description: ' + errorThrown + '.');
                }
            });
        });
    };

 
    self.getLastPath = function (){
        var path = $('#lastPath').text().trim();;
        path = path.substr(0, path.length - 1);
        return "/Directory/GetAllDirectory/" + path;
    };

    self.backArrowClick = function(callback){
       $("#back-image").click(function () {
            var url = self.getLastPath();;
            url = encodeURI(url);

            $.ajax({
                url: url,
                type: "GET",
                success:function (result) {
                    callback(result);
                },
                error: function (){
                    alert('Oops, something bad happened. We workikg with the problem. Try it again. Description: ' + errorThrown + '.');
                }
            });
        });
    };

    self.getPath  = function(id){
        var newPath = $('#'+id).find('#path').text().trim();
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
};

export default SearchMgr;