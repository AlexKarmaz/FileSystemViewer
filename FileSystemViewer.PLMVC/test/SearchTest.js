import $ from 'jquery';
import SearchMgr from '../src/Scripts/SearchScripts.js';

var sandbox;
var getPathSpy;
var getLastPathSpy;
var mainContainer;
var recentSearchMgr;


QUnit.module('SearchModule', {
	beforeEach: function () {

		sandbox = sinon.sandbox.create();
		recentSearchMgr = new SearchMgr();

		getPathSpy = sandbox.spy(recentSearchMgr,"getPath");
        getLastPathSpy = sandbox.spy(recentSearchMgr,"getLastPath");
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
        Path
        </th>
    </tr>

            <tr class="item folder" id="item-NewProduct">
                <td>
                    NewProduct
                </td>
                <td>
                    Folder
                </td>
                <td id="path">
                    D\\data\\Cobalt Document\\Development\\NewProduct
                </td>
            </tr>
            <tr class="item folder" id="item-NewDocument">
                <td>
                    NewDocument
                </td>
                <td>
                    Folder
                </td>
                <td id="path">
                    D\\data\\Cobalt Document\\Development\\NewProduct\\NewDocument
                </td>
            </tr>
</tbody></table>

</div>`);
		mainContainer = document.getElementById('main_container'); 							
	},

	afterEach: function () {
		sandbox.restore();

		recentSearchMgr = undefined; 
		mainContainer = null;
	}
});

QUnit.test("getPath() method must return correct string", function (assert) {	
	assert.strictEqual(getPathSpy("item-NewProduct"), "/Directory/GetAllDirectory/D\\data\\Cobalt Document\\Development\\NewProduct", "result must equals to expect result");
	assert.ok(getPathSpy.calledOnce, "Get path method called once");

});

QUnit.test("getLastPath() method must return correct string", function (assert) {   
    assert.strictEqual(getLastPathSpy(), "/Directory/GetAllDirectory/D", "result must equals to expect result");
    assert.ok(getLastPathSpy.calledOnce, "Get path method called once");

});

QUnit.test("hangDblClick() method must hang dblclick and has correct respond", function (assert) {	
	var callback = sandbox.spy();

	recentSearchMgr.hangDblClick(callback);

    sandbox.stub($, 'ajax');
	$('.folder').eq(1).trigger('dblclick');

	assert.ok($.ajax.calledOnce, "ajax call made");
	assert.ok($.ajax.calledWithMatch({ url: '/Directory/GetAllDirectory/D%5Cdata%5CCobalt%20Document%5CDevelopment%5CNewProduct%5CNewDocument' }), "Ajax must works good");

});

QUnit.test("backArrowClick() method must hang backArrowClick and has correct respond", function (assert) {  
    var callback = sandbox.spy();

    recentSearchMgr.backArrowClick(callback);

    sandbox.stub($, 'ajax');
    $('#back-image').trigger('click');

    assert.ok($.ajax.calledOnce, "ajax call made");
    assert.ok($.ajax.calledWithMatch({ url: '/Directory/GetAllDirectory/D' }), "Ajax must works good");

});





