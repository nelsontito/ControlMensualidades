

let tablaData;
let idEditar = 0;

$(document).ready(function () {
    cargarSemestre();
    ListarEstudiantes();
});






function cargarSemestre() {

    // Mostramos un texto de "Cargando..." mientras esperamos la respuesta
    $("#cboSemestre").html('<option value="">Cargando...</option>');

    $.ajax({
        url: "PageEstudiantes.aspx/ListarSemestre",
        type: "POST",
        data: "{}", // <-- Mejor compatibilidad con WebMethods sin parámetros
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        success: function (response) {
            if (response.d.Estado) {

                // 1. Empezamos con la opción por defecto
                let opcionesHTML = '<option value="">Seleccione Semestre</option>';

                // 2. Concatenamos todas las opciones en la variable (en memoria)
                $.each(response.d.Data, function (i, row) {
                    opcionesHTML += `<option value="${row.IdSemestre}">${row.NombreSemestre}</option>`;                });

                //$.each(response.d.Data, function (i, row) {
                //    if (row.Estado === true) {
                //        opcionesHTML += `<option value="${row.IdGestion}">${row.NombreGestion}</option>`;
                //    }
                //});

                // 3. Inyectamos todo al DOM en un solo movimiento
                $("#cboSemestre").html(opcionesHTML);

            } else {
                $("#cboSemestre").html('<option value="">Error al cargar</option>');
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.status + " \n" + xhr.responseText, "\n" + thrownError);
            $("#cboSemestre").html('<option value="">Error de conexión</option>');
        }
    });

}

function ListarEstudiantes() {
    if ($.fn.DataTable.isDataTable("#tbData")) {
        $("#tbData").DataTable().destroy();
        $('#tbData tbody').empty();
    }

    tablaData = $("#tbData").DataTable({
        responsive: true,
        ajax: {
            url: 'PageEstudiantes.aspx/ListarEstudiantes',
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: function (d) {
                return JSON.stringify(d);
            },
            dataSrc: function (json) {
                if (json.d.Estado) {
                    return json.d.Data;
                } else {
                    return [];
                }
            }
        },
        columns: [
            { data: "IdEstudiante", visible: false, searchable: false },

            {
                data: "FotoUrl",
                className: "text-center align-middle",
                render: function (data) {
                    let foto = data && data !== "" ? data : "Imagenes/images.png";
                    return `<img src="${foto}" alt="Foto" class="rounded-circle" style="width:40px;height:40px;object-fit:cover;" onerror="this.src='Imagenes/images.png';">`;
                }
            },

            {
                data: null,
                className: "align-middle",
                render: function (data, type, row) {
                    return `${row.NombreEstudiante} ${row.ApellidosEstudiante}`;
                }
            },

            { data: "CiEstudiante", className: "align-middle" },

            { data: "Codigo", className: "align-middle" },
            { data: "Telefono", className: "align-middle" },

            {
                data: "Estado",
                className: "text-center align-middle",
                render: function (data) {
                    return data
                        ? '<span class="badge badge-primary">Activo</span>'
                        : '<span class="badge badge-danger">No Activo</span>';
                }
            },

            {
                defaultContent:
                    '<button class="btn btn-primary btn-editar btn-sm mr-2"><i class="fas fa-pencil-alt"></i></button>' +
                    '<button class="btn btn-info btn-detalle btn-sm"><i class="fas fa-address-book"></i></button>',
                orderable: false,
                searchable: false,
                width: "100px",
                className: "text-center align-middle"
            }
        ],
        order: [[0, "desc"]],
        language: {
            url: "https://cdn.datatables.net/plug-ins/1.11.5/i18n/es-ES.json"
        }
    });
}

$('#tbData tbody').on('click', '.btn-editar', function () {

    let data = tablaData.row($(this).closest('tr')).data();

    idEditar = data.IdEstudiante;

    // llenar el modal
    $("#txtNombres").val(data.Nombres);
    $("#txtApellidos").val(data.Apellidos);
    $("#txtNroCI").val(data.NroCI);
    $("#txtCodigo").val(data.Codigo);
    $("#txtTelefono").val(data.Telefono);
    $("#txtTelefono").val(data.Telefono);
    $("#cboIdGradoAcademico").val(data.IdGradoAcademico);
    $("#cboCarrera").val(data.IdCarrera);
    $("#cboSemestre").val(data.IdSemestre); 
    $("#cboEstado").val(data.Estado ? 1 : 0);

    $('#imgDirectReg').attr('src', data.FotoUrl || "Imagenes/images.png");

    $("#cboEstado").prop("disabled", false);

    $("#myModalLabel").text("Editar Usuario");

    $("#mdData").modal("show");
});

$('#tbData tbody').on('click', '.btn-detalle', function () {

    let data = tablaData.row($(this).closest('tr')).data();

    alert("Estudiante: " + data.NombreEstudiante + "\nCodigo: " + data.Codigo);
});

$("#btnNuevo").on("click", function () {

    idEditar = 0;

    $("#txtNombreEstudiante").val("");
    $("#txtApellidosEstudiante").val("");
    $("#txtCiEstudiante").val("");
    $("#txtCodigo").val("");
    $("#txtCiEstudiante").val("");
    $("#txtTelefono").val("");
    $("#cboIdGradoAcademico").val("");
    $("#cboCarrera").val("");
    $("#cboSemestre").val("");

    $("#cboEstado").val(1).prop("disabled", true);

    $('#imgDirectReg').attr('src', "Imagenes/images.png");
    $("#txtFotoUr").val("");
    $(".custom-file-label").text('Ningún archivo seleccionado');

    $("#myModalLabel").text("Nuevo Usuario");

    $("#mdData").modal("show");

})

const TAMANO_MAXIMO = 2 * 1024 * 1024; // 2 MB en bytes

function esImagen(file) {
    return file && file.type.startsWith("image/");
}

function mostrarImagenSeleccionada(input) {
    let file = input.files[0];
    let reader = new FileReader();

    // Si NO se seleccionó archivo (ej: presionaron "Cancelar")
    if (!file) {
        resetearVistaFoto(input);
        return;
    }

    // Validación: si no es imagen, mostramos error
    if (!esImagen(file)) {
        //MostrarToastZer("El archivo seleccionado no es una imagen válida.", "Atención", "error");
        toastr.error("El archivo seleccionado no es una imagen válida.");
        resetearVistaFoto(input);
        return;
    }

    // 3. Validación: Tamaño máximo
    if (file.size > TAMANO_MAXIMO) {
        toastr.error("La imagen supera el tamaño máximo permitido de 2 MB.");
        resetearVistaFoto(input);
        return;
    }

    // Si todo es válido → mostrar vista previa
    reader.onload = (e) => $('#imgDirectReg').attr('src', e.target.result);
    reader.readAsDataURL(file);

    // Mostrar nombre del archivo
    $(input).next('.custom-file-label').text(file.name);
}
// Función auxiliar para limpiar (DRY - Don't Repeat Yourself)
function resetearVistaFoto(input) {
    $('#imgDirectReg').attr('src', "Imagenes/images.png");
    $(input).next('.custom-file-label').text('Ningún archivo seleccionado');
    input.value = ""; // Limpia el input file
}

$('#txtFotoUr').change(function () {
    mostrarImagenSeleccionada(this);
});

function AlertaTimerTipo(titulo, mensaje, tipo, timer) {
    swal({
        title: titulo,
        text: mensaje,
        type: tipo,
        // Si le pasas un valor a timer lo usa; si no, usa 2000 por defecto
        timer: timer || 3000,
        showConfirmButton: false
    });
}

function habilitarBoton() {
    $('#btnGuardarCambios').prop('disabled', false);
}

$("#btnGuardarCambios").on("click", function () {
    // Bloqueo inmediato
    $('#btnGuardarCambios').prop('disabled', true);

    let IdCarrera = $("#cboCarrera").val();
    let IdSemestre = $("#cboSemestre").val();

    if (IdCarrera === "") {
        toastr.warning("", "Por favor, seleccione una Carrera.");
        $("#cboCarrera").focus();
        habilitarBoton();
        return;
    }
    const inputs = $("#mdData input.model").serializeArray();
    const inputs_sin_valor = inputs.filter(item => item.value.trim() === "");

    if (inputs_sin_valor.length > 0) {
        const mensaje = `Debe completar el campo: "${inputs_sin_valor[0].name}"`;
        //MostrarToastZer(mensaje, "Atención", "warning");
        toastr.warning("", mensaje)
        $(`input[name="${inputs_sin_valor[0].name}"]`).focus();
        habilitarBoton();
        return;
    }


    if (IdSemestre === "") {
        toastr.warning("", "Por favor, seleccione un Semestre.")
        $("#cboSemestre").focus();
        habilitarBoton();
        return;
    }

    // 2. ARMAR EL OBJETO
    const objeto = {
        IdEstudiante: idEditar,
        IdCarrera: parseInt(IdCarrera),
        IdSemestre: parseInt(IdSemestre),
        NombreEstudiante: $("#txtNombreEstudiante").val().trim(),
        ApellidosEstudiante: $("#txtApellidosEstudiante").val().trim(),
        Codigo: $("#txtCodigo").val().trim(),
        CiEstudiante: $("#txtCiEstudiante").val().trim(),
        Telefono: $("#txtTelefono").val().trim(),
        Estado: ($("#cboEstado").val() === "1" ? true : false),
        FotoEstudianteUrl: "" // Lo enviamos siempre vacío. Si hay foto nueva, el Base64 la reemplazará en C#.
    };

    const fileInput = document.getElementById('txtFotoUr');
    const file = fileInput.files[0];

    if (file) {
        const reader = new FileReader();
        reader.onload = function (e) {
            const base64String = e.target.result.split(',')[1];

            enviarAjaxEstudiantes(objeto, base64String);
        };
        reader.readAsDataURL(file);
    } else {
        enviarAjaxEstudiantes(objeto, "");
    }

});

function enviarAjaxEstudiantes(objeto, base64String) {
    $("#mdData").find("div.modal-content").LoadingOverlay("show");

    $.ajax({
        type: "POST",
        url: "PageEstudiantes.aspx/GuardarOrEditEstudiantes",
        data: JSON.stringify({ objeto: objeto, base64Image: base64String }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $("#mdData").find("div.modal-content").LoadingOverlay("hide");

            AlertaTimerTipo(
                response.d.Estado ? '¡Excelente!' : 'Atención', // Título dinámico
                response.d.Mensaje, // Texto del servidor
                response.d.Valor // Icono (success/error/warning)
            );

            if (response.d.Estado) {
                $("#mdData").modal("hide");
                idEditar = 0;
                ListarEstudiantes();
            }
        },
        error: function () {
            $("#mdData").find("div.modal-content").LoadingOverlay("hide");
            toastr.error("No se pudo conectar con el servidor.");
        },
        complete: function () {
            habilitarBoton();
        }
    });

}
