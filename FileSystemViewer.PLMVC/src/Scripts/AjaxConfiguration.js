import $ from 'jquery';

function AjaxConfigurator() {
    var self = this;

    self.StartAjax = function (){
    	$(document).ajaxStart(function () {
            $("#loading").show();
        });
    };

    self.CompleteAjax = function (){
     	$(document).ajaxComplete(function () {
            $("#loading").hide();
        });
    };

    self.OnErrorAjax = function (){
    	alert('Oops, something bad happened. We workikg with the problem. Try it again. Description: ' + errorThrown + '.');
    };

};


export default AjaxConfigurator;