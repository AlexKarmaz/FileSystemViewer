import $ from 'jquery';
import CreateFileModalMgr from '../src/Scripts/CreateFileModalWindow.js';

var sandbox;
var container;
var recentCreateFileModalMgr;

describe("CreateFileModal jasmine test", function() {

	beforeEach(function() {
		sandbox = sinon.sandbox.create();
		recentCreateFileModalMgr = new CreateFileModalMgr();

		container = $(`<div id="main_container"> 
			<button type="button" class="btn btn-success modal-button" id="modal-button-folder">Create folder</button>


<div id="modDialog-folder" class="modal fade" tabindex="-1" role="dialog" aria-hidden="true">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button class="close" data-dismiss="modal">x</button>
                <h4>Create new folder</h4>
            </div>
<form action="/Directory/CreateFolder/D%5C%5C" data-ajax="true" data-ajax-failure="OnError" data-ajax-loading="#loading" data-ajax-method="POST" data-ajax-success="OnSuccessFolder" id="form0" method="post" novalidate="novalidate">                <div class="modal-body custom-modal">
                    <input name="__RequestVerificationToken" type="hidden" value="yMBv6UWAyxnYnRK0t7-gAPaP6LcA2pBZ8wDUENcKr7ID3tzDM-E_qQWctFdTvzCXC8Ode-4oZ5pMUkf6vTIW7nVgIB4Fb2i5U06qZ0Z3sqU1">
                    <br>
                    
                    <label class="col-md-2 control-label" for="Name">Folder name</label>
                    <div class="col-md-10">
                        <input class="form-control text-box single-line" data-val="true" data-val-regex="Invalid folder name" data-val-regex-pattern="(^(?! ))(^[^\\/?*:|&quot;<>]+$)" data-val-required="The field can not be empty!" id="Name" name="Name" type="text" value="">
                        <span class="field-validation-valid text-danger" data-valmsg-for="Name" data-valmsg-replace="true"></span>
                    </div>
                    <input id="ParentDirectoryPath" name="ParentDirectoryPath" type="hidden" value="D:\\/">
                    <div class="form-group">
                        <div class="col-md-offset-2 col-md-10">
                            <input type="submit" value="Create" class="btn btn-success">
                        </div>
                    </div>
                </div>
</form>        </div>
    </div>
</div>
</div>`);

		$(document.body).append(container);
	});

	afterEach(function() {
		sandbox.restore();
		recentCreateFileModalMgr = undefined;
		container = null; 
		$('#main_container').remove();
	});

    it("hideFileDialog() method must hide modal window", function() {
        var hideFileDialogSpy = sandbox.spy(recentCreateFileModalMgr,"hideFileDialog");

        $("#modDialog-file").show();
        hideFileDialogSpy();

	    expect(hideFileDialogSpy.calledOnce).toBe(true);
    });

    it("showFileDialog() method must show modal window",function(){
	    var showFileDialogSpy = sandbox.spy(recentCreateFileModalMgr,"createFileBtnClick");

        showFileDialogSpy();
        $('#modal-button-file').trigger('click');

	    expect(showFileDialogSpy.calledOnce).toBe(true);
    });
}); 