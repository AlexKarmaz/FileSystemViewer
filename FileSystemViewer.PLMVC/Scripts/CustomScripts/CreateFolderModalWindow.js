$("#modal-button-folder").click(function () {
    $("#modDialog-folder").modal("show");
});


function hideFolderDialog() {
    $("#modDialog-folder").modal("hide");
};

function OnSuccessFolder(result) {
    if (result.Status == 'Exist') {
        alert("Such folder is already exists");
    } else {
        hideFolderDialog();
        $('.item').unbind();
        $('#explorer-table').html(result);
    }
};
