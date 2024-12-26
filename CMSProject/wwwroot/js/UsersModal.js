$(document).ready(function () {

    //Boton Guardar
    $('#btnSave').click(function (e) {
        e.preventDefault();

        var formData = new FormData($('#saveForm')[0]);

        $.ajax({
            type: 'POST',
            url: '/Admin/CreateUser',
            data: formData,
            processData: false,
            contentType: false,
            success: function () {
                window.location.href = "/Admin/UserManagement";
            },
            error: function () {
                alert("Imposible leer los datos");
            }
        });
    });

    //Boton Cancelar
    $(".btnCancel").click(function () {
        window.location.href = "/Admin/UserManagement";
    })

    //Boton Borrar
    $("#btnDelete").click(function () {
        var idD = $("#UsersId").val();
        $.ajax({
            type: "POST",
            url: "/Admin/DeleteUserConfirmed",
            data: { id: idD },
            success: function (result) {
                if (result) {
                    $("#UsersId").val(null);
                    window.location.href = "/Admin/UserManagement";
                }
                else {
                    alert("Algo salió mal");
                }
            }
        })
    })
})

var Confirm = function (id) {
    $("#UsersId").val(id);
    $("#DeleteUser").modal('show');
}

var EditUs = function (id) {
    $.ajax({
        dataType: "json",
        url: "/Admin/GetEditUser",
        data: { id: id },
        success: function (user) {
            if (user == null || user == undefined) {
                alert("no existen datos leibles");
            }
            else if (user.length == 0) {
                alert("Datos no disponibles");
            }
            else {
                $("#nameUser").val(user.Name);
                $("#emailUser").val(user.Email);
                $("#passwdUser").val(user.Password);
                $("#roleUser").val(user.IdRole);
                $("#companyUser").val(user.IdClient);
                $("#ImageUser").val(user.Imagen);
                $(".updateId").val(user.Id);
                $("#EditUsers").modal('show');
            }
        },
        error: function () {
            alert("Imposible leer los datos");
        }
    })
}

