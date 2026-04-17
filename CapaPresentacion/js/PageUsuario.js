
$(document).ready(function () {

    cargarRoles();
});
function cargarRoles() {

    $.ajax({
        type: "POST",
        url: "PageUsuario.aspx/ListarRoles",
        data: "{}",
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        success: function (response) {
            console.log(response)
            if (response.d.Estado) {

                const lista = response.d.Data;
                console.log(lista)



            } else {
                console.log("Estado en Falso")
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.status + " \n" + xhr.responseText, "\n" + thrownError);
        }
    });
}






$("#btnRegistro").on("click", function () {


    $("#myModalLabel").text("Nuevo Registro");

    $("#mdData").modal("show");

})

//fin