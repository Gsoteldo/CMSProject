$(document).ready(function () {

    $(".btnCancel").click(function () {
        window.location.href = "/Publisher/PublisherDashboard";
    })

    $("#btnPost").click(function () {
        var params = {
            id: $("#PostId").val()
        };
        $.ajax({
            type: "POST",
            url: "/Publisher/PostPublicationConfirmed",
            data: params,
            success: function () {
                window.location.href = "/Publisher/PublisherDashboard";
            },
            error: function (result) {
                alert("Algo salió mal");
            }
        })
    })
})

var Confirm = function (id) {
    $.ajax({
        dataType: "json",
        url: "/Publisher/PostPublication",
        data: { id: id },
        success: function (result) {
            if (result == null || result == undefined) {
                alert("no existen datos leibles");
            }
            else {
                $("#PostId").val(result.Id);
                $("#postPublication").modal('show');
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
        url: "/Publisher/DetailPost",
        data: { id: id },
        success: function (result) {
            if (result == null || result == undefined) {
                alert("no existen datos leibles");
            }
            else {
                if (!result.hasOwnProperty('Title') || !result.hasOwnProperty('Concept') ||
                    !result.hasOwnProperty('PublicationDate') || !result.hasOwnProperty('IdUserNavigation')) {
                    alert("No se guardó bien el Json");
                }
                var html = '<ul>';
                html += '<li><h4>Título:</h4>\n ' + result.Title + '</li>';
                html += '<li><h4>Creador:</h4>\n ' + result.IdUserNavigation.Name + '</li>';
                html += '<li><h4>Fecha de publicación:</h4>\n ' + result.PublicationDate + '</li>';
                html += '<li><h4>Concepto:</h4>\n ' + result.Concept + '</li>';
                html += '</ul>';

                $("#ReadMoreModal1").html(html);
                $("#ReadMoreModal").modal('show');
            }
        },
        error: function () {
            alert("Imposible leer los datos");
        }
    })
}