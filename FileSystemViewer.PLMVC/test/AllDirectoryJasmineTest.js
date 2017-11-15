import $ from 'jquery';
import DirectoryMgr from '../src/Scripts/AllDirectoryScripts.js';

var sandbox;
var container;
var recentDirectoryMgr;

describe("AllDirectory jasmine test", function() {
	beforeEach(function() {
		sandbox = sinon.sandbox.create();
		recentDirectoryMgr = new DirectoryMgr();


		container = $(`<div id="main_container">

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

		$(document.body).append(container);
	});

	afterEach(function() {
		sandbox.restore();
		recentDirectoryMgr = undefined;
		container = null; 
		$('#main_container').remove();
	});

    it("getLastPath() method must return correct string", function() {
        var getLastPathSpy = sandbox.spy(recentDirectoryMgr,"getLastPath");


	    expect(getLastPathSpy()).toBe("/Directory/GetAllDirectory/D");
	    expect(getLastPathSpy.calledOnce).toBe(true);
    });

    it("getPath() method must return correct string",function(){
	    var getPathSpy = sandbox.spy(recentDirectoryMgr,"getPath");

	    expect(getPathSpy("item-data")).toBe("/Directory/GetAllDirectory/D\\data");
	    expect(getPathSpy.calledOnce).toBe(true);
    });

    it("hangDblClick() method must hang dblclick and has correct respond", function(){
    	var callback = sandbox.spy();

	    recentDirectoryMgr.hangDblClick(callback);

        sandbox.stub($, 'ajax');
	    $('.folder').eq(1).trigger('dblclick');

	    expect($.ajax.calledOnce).toBe(true);
	    expect($.ajax.calledWithMatch({ url: '/Directory/GetAllDirectory/D%5Cdata' })).toBe(true);
    });

	it("backArrowClick() method must hang backArrowClick and has correct respond", function(){
		var callback = sandbox.spy();

        recentDirectoryMgr.backArrowClick(callback);

        sandbox.stub($, 'ajax');
        $('#back-image').trigger('click');

        expect($.ajax.calledOnce).toBe(true);
	    expect($.ajax.calledWithMatch({ url: '/Directory/GetAllDirectory/D' })).toBe(true);
	});
}); 