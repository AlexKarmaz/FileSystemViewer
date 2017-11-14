import $ from 'jquery';


var createFileModalMgr = new CreateFileModalMgr();

function CreateFileModalMgr(){
    var self = this;

    self.onSuccessCreateFile = function (result){
    	if (result.Status == 'Exist') {
            alert("Such file is already exists");
        } else {
            self.hideFileDialog();
            $('.item').unbind();
            $('#explorer-table').html(result);
        }
    };

    self.hideFileDialog = function (){
    	 $("#modDialog-file").hide();
    };

    self.createFileBtnClick = function(){
    	$("#modal-button-file").click(function () {
             $("#modDialog-file").show();
        });
    };
};


export default CreateFileModalMgr;