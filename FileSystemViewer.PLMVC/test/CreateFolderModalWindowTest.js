import $ from 'jquery';
import CreateFolderModalMgr from '../src/Scripts/CreateFolderModalWindow.js';

var sandbox;
var mainContainer;
var recentCreateFolderModalMgr;


QUnit.module('CreateFolderModalWindowModule', {
	beforeEach: function () {

		sandbox = sinon.sandbox.create();
		recentCreateFolderModalMgr = new CreateFolderModalMgr();

		$('#qunit-fixture').append(`<button type="button" class="btn btn-success modal-button" id="modal-button-folder">Create folder</button>


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
</div>`);
		mainContainer = document.getElementById('main_container'); 							
	},

	afterEach: function () {
		sandbox.restore();

		recentCreateFolderModalMgr = undefined; 
		mainContainer = null;
	}
});

QUnit.test("hideFolderDialog() method must hide modal window", function (assert) {
    var hideFolderDialogSpy = sandbox.spy(recentCreateFolderModalMgr,"hideFoldereDialog");

    $("#modDialog-folder").show();
    hideFolderDialogSpy();

	assert.ok(hideFolderDialogSpy.calledOnce, "hideFoldereDialog method called once");
});

QUnit.test("showFolderDialog() method must show modal window", function (assert) {
    var showFolderDialogSpy = sandbox.spy(recentCreateFolderModalMgr,"createFolderBtnClick");

    showFolderDialogSpy();
    $('#modal-button-folder').trigger('click');

    assert.ok(showFolderDialogSpy.calledOnce, "showFolderDialog method called once");
});






