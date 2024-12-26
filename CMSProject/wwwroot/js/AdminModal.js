var ReadMore = function (id) {
    $.ajax({
        url: "/Admin/DetailsPublication",
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

                $("#AdminModal1").html(html);
                $("#AdminModal").modal('show');
            }
        },
        error: function () {
            alert("Imposible leer los datos");
        }
    })
}