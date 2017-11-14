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
		this.container = null; 
		$('#main_container').remove();
	});

    it("and so is a spec", function() {
    	debugger;
    	var callback = sandbox.spy();
    	recentDriveMgr.hangDblClick(callback);


    	sinon.stub($, 'ajax');

    	$('.drive').eq(1).trigger('dblclick');

	    expect($.ajax.calledOnce).toBe(true);
	    expect($.ajax.calledWithMatch({ url: '/Directory/GetAllDirectory/D' })).toBe(true);

    	//runs(function() {
    	//	flag = false;  // начальные значения
         //   value = 0;

    	//    $('.drive').eq(1).trigger('dblclick');
       // });

      //  waitsFor(function() {
      //      return flag; // ждет когда флаг переключится в true
      //  }, "Message for timeout case", 750);

      //  runs(function() {
      //      expect(callback.calledOnce).toBe(true);
      //  });

    });
}); 