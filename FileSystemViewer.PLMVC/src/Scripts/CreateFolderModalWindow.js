import $ from 'jquery';

var createFolderModalMgr = new CreateFolderModalMgr();

function CreateFolderModalMgr(){
    var self = this;

    self.onSuccessCreateFolder = function (result){
    	if (result.Status == 'Exist') {
            alert("Such file is already exists");
        } else {
            self.hideFolderDialog();
            $('.item').unbind();
            $('#explorer-table').html(result);
        }
    };

    self.hideFoldereDialog = function (){
    	$("#modDialog-folder").hide();
    };

    self.createFolderBtnClick = function(){
    	$("#modal-button-folder").click(function () {
            $("#modDialog-folder").show();
        });
    };
};


export default CreateFolderModalMgr;


