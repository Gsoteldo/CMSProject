
$(document).ready(function () {

    $(".btnCancel").click(function () {
        window.location.href = "/File/FileIndex";
    })

})

    var Show = function () {
        $("#DownloadModal").modal('show');
    }

function UploadFile(e) {
        e.preventDefault();
        var file = fileInput.files[0];
        $.ajax({
            type: "POST",
            url: "/File/FileIndex",
            data: { id: id },
            success: function (file) {
                alert("Entra en Success");
                alert(file);
                $("#ModalUploadFile").modal("show");
            },
            error: function () {
                alert("Imposible leer los datos");
            }
        })
    }
