import $ from 'jquery';
import DriveMgr from '../src/Scripts/GetDrivesScript.js';

var sandbox;
var recentDriveMgr;
var container;


describe("GetDrives jasmine test", function() {
	var value, flag;


	beforeEach(function() {
		sandbox = sinon.sandbox.create();
		recentDriveMgr = new DriveMgr();
		debugger;

		//jasmine.setFixtures().set();

		container = $(`<div id="main_container">           

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

		$(document.body).append(container);
	});

	afterEach(function() {
		sandbox.restore();
		recentDriveMgr = undefined;
		container = null; 
		$('#main_container').remove();
	});

    it("hangDblClick() method must hang dblclick and has correct respond", function() {
    	var callback = sandbox.spy();
    	recentDriveMgr.hangDblClick(callback);


    	sandbox.stub($, 'ajax');

    	$('.drive').eq(1).trigger('dblclick');

	    expect($.ajax.calledOnce).toBe(true);
	    expect($.ajax.calledWithMatch({ url: '/Directory/GetAllDirectory/D' })).toBe(true);
    });

    it("getPath() method must return correct string",function(){
	    var getPathSpy = sandbox.spy(recentDriveMgr,"getPath");

	    expect(getPathSpy("item-C")).toBe("/Directory/GetAllDirectory/C");
	    expect(getPathSpy.calledOnce).toBe(true);
    });


   // it("async",function(){
    //	
    //	flag = true; 

    //   var errorCallback = function(){
    //		flag = false;
    		//expect(flag).toBe(true); 
    //	};

    //	var onErrorSpy =  sandbox.spy(errorCallback);

    //	recentDriveMgr.hangDblClick(null,errorCallback);

    //	$('.drive').eq(1).trigger('dblclick');
        

      //  waitsFor(function() {
      //     return flag; // ждет когда флаг переключится в true
     //   }, "Message for timeout case", 1000);

    //    runs(function() {
    //        expect(flag).toBe(false);
    //    });
    //});


   describe("Asynchronous tests", function() {
        var flag, originalTimeout;

        beforeEach(function() {
           originalTimeout = jasmine.DEFAULT_TIMEOUT_INTERVAL;
           jasmine.DEFAULT_TIMEOUT_INTERVAL = 10000;
        });

        afterEach(function() {
            jasmine.DEFAULT_TIMEOUT_INTERVAL = originalTimeout;
        });

        it("asdfync",function(done){
    	    flag = true;  
    	    debugger;

    	    var errorCallback = function(){
    		    flag = false;
    		    done();
    		    expect(flag).toBe(false);
    		    expect(onErrorSpy.calledOnce).toBe(true); 
    	    };

    	    var onErrorSpy =  sandbox.spy(errorCallback);

    	    recentDriveMgr.hangDblClick(null,onErrorSpy);

    	    $('.drive').eq(1).trigger('dblclick');
    	
        });
    });
}); 