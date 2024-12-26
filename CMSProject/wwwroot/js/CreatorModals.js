
$(document).ready(function () {

    //Boton Guardar
    $("#btnSave").click(function () {
        var taskForm = $("#saveForm").serialize();
        $.ajax({
            type: "POST",
            url: "/Creator/CreatePublication",
            data: taskForm,
            success: function () {
                window.location.href = "/Creator/CreatorDashboard";
            }
        })
    })

    //Boton Cancelar
    $(".btnCancel").click(function () {
        window.location.href = "/Creator/CreatorDashboard";
    })

    //Boton Borrar
    $("#btnDelete").click(function () {
        var idD = $("#PublicationId").val();
        $.ajax({
            type: "POST",
            url: "/Creator/DeletePublicationConfirmed",
            data: { id: idD },
            success: function (result) {
                if (result) {
                    $("#PublicationId").val(null);
                    window.location.href = "/Creator/CreatorDashboard";
                }
                else {
                    alert("Algo salió mal");
                }
            }
        })
    })
})

var Confirm = function (id) {
    $("#PublicationId").val(id);
    $("#DeletePublication").modal('show');
}

var EditPub = function (id) {
    $.ajax({
        dataType: "json",
        url: "/Creator/GetEditPublication",
        data: { id: id },
        success: function (publication) {
            if (publication == null || publication == undefined) {
                alert("no existen datos leibles");
            }
            else if (publication.length == 0) {
                alert("Datos no disponibles");
            }
            else {
                $("#publicationTitle").val(publication.Title);
                $("#publicationConcept").val(publication.Concept);
                $("#publicationDate").val(publication.PublicationDate);
                $(".updateId").val(publication.Id);
                $("#EditPublication").modal('show');
            }
        },
        error: function () {
            alert("Imposible leer los datos");
        }
    })
}

var ReadMore = function (id) {
    $.ajax({
        /*dataType: "json",*/
        url: "/Creator/DetailsPublication",
        data: { id: id },
        success: function (result) {
            if (result == null || result == undefined) {
                alert("no existen datos leibles");
            }
            else {
                if (!result.hasOwnProperty('Title') ||
                    !result.hasOwnProperty('Concept') ||
                    !result.hasOwnProperty('PublicationDate') ||
                    !result.hasOwnProperty('IdUserNavigation')) {
                    alert("No se guardó bien el Json");

                }
                var html = '<ul>';
                html += '<li><h4>Título:</h4>\n ' + result.Title + '</li>';
                html += '<li><h4>Creador:</h4>\n ' + result.IdUserNavigation.Name + '</li>';
                html += '<li><h4>Fecha de publicación:</h4>\n ' + result.PublicationDate + '</li>';
                html += '<li><h4>Concepto:</h4>\n ' + result.Concept + '</li>';
                html += '</ul>';

                // Insertar la lista HTML en el modal
                $("#CreatorDetailsModal1").html(html);
                $("#CreatorDetailsModal").modal('show');
            }
        },
        error: function () {
            alert("Imposible leer los datos");
        }
    })
}
