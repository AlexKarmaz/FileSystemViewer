import $ from 'jquery';

var driveMgr = new DriveMgr();


//$(document).ready(function () {

  //  driveMgr.hangDblClick(driveMgr.onSuccess,function(){
    //    alert('Oops, something bad happened. We workikg with the problem. Try it again. Description: ' + errorThrown + '.');
   // });
//});

function DriveMgr() {
    var self = this;

    self.hangDblClick = function (callbackSucess, onError){
        debugger;
        $(".drive").dblclick(function () {
        var url = self.getPath(this.id);
        url = encodeURI(url);

        $.ajax({
            url: url,
            type: "GET",
            success: function (result) {
               callbackSucess(result);
            },
            error: function (){
                onError();
            }
        });
    });
    };

    self.getPath = function(id) {
        var path = $(".pathPlace").text();
        path = path.substr(13, path.length);
        var index = id.indexOf("-") + 1;
        return "/Directory/GetAllDirectory/" + path + id.substr(index, id.length);
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

export default DriveMgr;
