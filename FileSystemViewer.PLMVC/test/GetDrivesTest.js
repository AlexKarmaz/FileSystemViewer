import $ from 'jquery';
import DriveMgr from '../src/Scripts/GetDrivesScript.js';
//import GetDrivesHtml from '../test/Templates/AllDrivesTemplate.html';

var sandbox;
var getPathStub;
var mainContainer;
var recentDriveMgr;
var hangDblClickSpy;
var server;

QUnit.module('GetDrivesModule', {
	beforeEach: function () {

		sandbox = sinon.sandbox.create();
		recentDriveMgr = new DriveMgr();
		server = sinon.createFakeServer();

		getPathStub = sandbox.spy(recentDriveMgr,"getPath");
		hangDblClickSpy = sandbox.spy(recentDriveMgr,"hangDblClick");
		$('#qunit-fixture').append(`<div id="main_container">
                

    <div>
        <div class="pathPlace">The computer\\</div>
    </div>


    <table class="table">
        <tbody>
            <tr>
                 <th>
                    Drive name
                 </th>
                 <th>
                    DriveType
                 </th>
                 <th>
                    TotalSize
                 </th>
                 <th>
                     TotalFreeSpace
                 </th>
                 <th></th>
            </tr>
            <tr class="item drive" id="item-C\">
                <td>
                    C\
                </td>
                <td>
                    Fixed
                </td>
                <td>
                    249GB
                </td>
                <td>
                    195GB
                </td>
            </tr>
            <tr class="item drive" id="item-D\">
                <td>
                    D\
                </td>
                <td>
                    Fixed
                </td>
                <td>
                    681GB
                </td>
                <td>
                    671GB
                </td>
            </tr>
        </tbody>
    </table>
</div>`);
		mainContainer = document.getElementById('main_container'); 							
	},

	afterEach: function () {
		debugger;
		sandbox.restore();
		server.restore();
		recentDriveMgr = undefined; 
		mainContainer = null;
	}
});

QUnit.test("getPath() method must return correct string", function (assert) {	
	assert.strictEqual(getPathStub("item-C"), "/Directory/GetAllDirectory/C", "result must equals to expect result");
	assert.ok(getPathStub.calledOnce, "Get path method called once");

});

QUnit.test("hangDblClick() method must hang dblclick and has correct respond", function (assert) {	
	var callback = sinon.spy();
//	server.respondWith("GET", "/Directory/GetAllDirectory/C",
  //        [200, { "Content-Type": "application/json" },
 //          '[{ "id": 12, "comment": "Hey there" }]']);

	recentDriveMgr.hangDblClick(callback);
//	$('.drive').eq(0).trigger('dblclick');

   // assert.ok(callback.called, "ajax success callback called once");

    sinon.stub($, 'ajax');
	//server.respondWith("GET", "/Directory/GetAllDirectory/D",
         // [200, { "Content-Type": "application/json" },
          // '[{ "id": 12, "comment": "Hey there" }]']);


	$('.drive').eq(1).trigger('dblclick');

	assert.ok($.ajax.calledOnce, "ajax call made");
	assert.ok($.ajax.calledWithMatch({ url: '/Directory/GetAllDirectory/D' }), "Ajax must works good");

});


//QUnit.test("onSuccess() method must to rewrite main container ", function (assert) {	
	//var expectedResult = '<div> <div class="pathPlace">The computer\\D</div> </div>  <div>Good work</div>';
	//var = GetDrivesHtml = ' <div id="main_container"><div> <div class="pathPlace">The computer\\D</div> </div>  <div>Good work</div> </div>';
//	recentDriveMgr.onSuccess(expectedResult);
  //  assert.equal(mainContainer, GetDrivesHtml, "Recent Items template must be GetDrivesHtml");
//});




