$("#modal-button-file").click(function () {
    $("#modDialog-file").modal("show");
});


function hideFileDialog() {
    $("#modDialog-file").modal("hide");
};


function OnSuccessFile(result) {
    if (result.Status == 'Exist') {
        alert("Such file is already exists");
    } else {
        hideFileDialog();
        $('.item').unbind();
        $('#explorer-table').html(result);
    }

};