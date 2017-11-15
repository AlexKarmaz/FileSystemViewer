import $ from 'jquery';
import DirectoryMgr from '../src/Scripts/AllDirectoryScripts.js';

var sandbox;
var getPathSpy;
var getLastPathSpy;
var mainContainer;
var recentDirectoryMgr;


QUnit.module('AllDirectoryModule', {
	beforeEach: function () {

		sandbox = sinon.sandbox.create();
		recentDirectoryMgr = new DirectoryMgr();

		getPathSpy = sandbox.spy(recentDirectoryMgr,"getPath");
        getLastPathSpy = sandbox.spy(recentDirectoryMgr,"getLastPath");
		$('#qunit-fixture').append(`<div id="main_container">

<div class="hidden" id="lastPath">
    D\\
</div>

<div class="url-row">
    <div id="back-image"></div>
    <div class="pathPlace">The computer\\D\\</div>
</div>

<table class="table" id="explorer-table">
    <tbody><tr>
        <th class="tableHederitem">
        Name
        </th>
        <th class="tableHederitem">
        Type
        </th>
        <th style="color: #337ab7">
            LastAccessTime
        </th>
        <th style="color: #337ab7">
            Size
        </th>
    </tr>

            <tr class="item folder" id="item-AppData">
                <td>
                    AppData
                </td>
                <td>
                    Folder
                </td>
                <td>
                    9/21/2017 11:59:51 AM
                </td>
                <td>
                    
                </td>
            </tr>
            <tr class="item folder" id="item-data">
                <td>
                    data
                </td>
                <td>
                    Folder
                </td>
                <td>
                    10/9/2017 3:08:18 PM
                </td>
                <td>
                    
                </td>
            </tr>
</tbody></table>

</div>`);
		mainContainer = document.getElementById('main_container'); 							
	},

	afterEach: function () {
		sandbox.restore();

		recentDirectoryMgr = undefined; 
		mainContainer = null;
	}
});

QUnit.test("getPath() method must return correct string", function (assert) {	
	assert.strictEqual(getPathSpy("item-data"), "/Directory/GetAllDirectory/D\\data", "result must equals to expect result");
	assert.ok(getPathSpy.calledOnce, "Get path method called once");

});

QUnit.test("getLastPath() method must return correct string", function (assert) {   
    assert.strictEqual(getLastPathSpy(), "/Directory/GetAllDirectory/D", "result must equals to expect result");
    assert.ok(getLastPathSpy.calledOnce, "Get path method called once");
});

QUnit.test("hangDblClick() method must hang dblclick and has correct respond", function (assert) {	
	var callback = sandbox.spy();

	recentDirectoryMgr.hangDblClick(callback);

    sandbox.stub($, 'ajax');
	$('.folder').eq(1).trigger('dblclick');

	assert.ok($.ajax.calledOnce, "ajax call made");
	assert.ok($.ajax.calledWithMatch({ url: '/Directory/GetAllDirectory/D%5Cdata' }), "Ajax must works good");
});

QUnit.test("backArrowClick() method must hang backArrowClick and has correct respond", function (assert) {  
    var callback = sandbox.spy();

    recentDirectoryMgr.backArrowClick(callback);

    sandbox.stub($, 'ajax');
    $('#back-image').trigger('click');

    assert.ok($.ajax.calledOnce, "ajax call made");
    assert.ok($.ajax.calledWithMatch({ url: '/Directory/GetAllDirectory/D' }), "Ajax must works good");
});





