$(document).ajaxStart(function () {
    $("#loading").show();
});

$(document).ajaxComplete(function () {
    $("#loading").hide();
});

function OnError(XMLHttpRequest, textStatus, errorThrown) {
    alert('Oops, something bad happened. We workikg with the problem. Try it again. Description: ' + errorThrown + '.');
}