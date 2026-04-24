
$("#btnNuevo").on("click", function () {


    //$("#myModalLabel").text("Nuevo Registro");

    console.log('Se presiono')

    $("#mdData").modal("show");

})

$("#btnRegistro").on("click", function () {

    idEditar = 0;

    $("#cboCarrera").val("");
    $("#cboSemestre").val("");
    $("#txtNombre").val("");
    $("#txtApellidos").val("");
    $("#txtNro").val("");
    $("#txtTelefono").val("");
    $("#cboEstado").val("");

    $('#imgDirectReg').attr('src', "Imagenes/images.png");
    $("#txtFotoUr").val("");
    $(".custom-file-label").text('Ningún archivo seleccionado');

    $("#myModalLabel").text("Nuevo Usuario");

    $("#mdData").modal("show");

})