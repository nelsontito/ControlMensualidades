<%@ Page Title="" Language="C#" MasterPageFile="~/HomeMaster.Master" AutoEventWireup="true" CodeBehind="PageEstudiantes.aspx.cs" Inherits="CapaPresentacion.PageEstudiantes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
    <link href="assets/plugins/datatables/jquery.dataTables.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/plugins/datatables/buttons.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/plugins/datatables/fixedHeader.bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/plugins/datatables/responsive.bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/plugins/datatables/dataTables.bootstrap4.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/plugins/datatables/scroller.bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/css/inpfile.css" rel="stylesheet"/>
    <style>
    .est-perfil {
        width: 125px;
        height: 125px;
        object-fit: cover; /* Evita que la imagen se estire o aplaste */
        object-position: center; /* Asegura que se vea el centro de la foto */
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="titulo" runat="server">
    Panel de Estudiantes de la EMI...

</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="body" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <div class="card">
                <div class="card-header bg-primary py-2 px-4">
                    <h3 class="card-title m-0"><i class="fas fa-user-friends mr-2"></i>Estudiantes Emilianos</h3>
                </div>
                <div class="card-body">

                    <div class="form-row align-items-end mb-3">

                        <div class="form-group col-sm-8">
                            <label for="cboUnidadEdGe">Seleccione CARRERA</label>
                            <select class="form-control form-control-sm form-new" id="cboUnidadEdGe">
                            </select>
                        </div>

                        <div class="form-group col-sm-4">
                            <button type="button" id="btnNuevo" class="btn btn-primary btn-sm"><i class="fas fa-plus-circle mr-2"></i>Nuevo Registro</button>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-lg-12 col-sm-12 col-12">
                            <table class="table table-sm table-striped table-bordered" id="tbData" cellspacing="0" width="100%">
                                <thead>
                                    <tr>
                                        <th>Id</th>
                                        <th>Nombre</th>
                                        <th>Apellidos</th>
                                        <th>CI</th>
                                        <th>Codigo</th>
                                        <th>Telefono</th>
                                        <%--<th>Foto</th> --%>
                                        <th>Acciones</th>
                                    </tr>
                                </thead>
                                <tbody></tbody>
                            </table>

                        </div>
                    </div>
                </div>
            </div>
        </div>
       
    </div>


</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="footer" runat="server">

    <script src="assets/plugins/datatables/jquery.dataTables.min.js"></script>
    <script src="assets/plugins/datatables/dataTables.bootstrap4.min.js"></script>

    <script src="assets/plugins/datatables/dataTables.buttons.min.js"></script>
    <script src="assets/plugins/datatables/buttons.bootstrap4.min.js"></script>

    <script src="assets/plugins/datatables/jszip.min.js"></script>
    <script src="assets/plugins/datatables/pdfmake.min.js"></script>
    <script src="assets/plugins/datatables/vfs_fonts.js"></script>
    <script src="assets/plugins/datatables/buttons.html5.min.js"></script>
    <script src="assets/plugins/datatables/buttons.print.min.js"></script>
    <script src="assets/plugins/datatables/dataTables.fixedHeader.min.js"></script>
    <script src="assets/plugins/datatables/dataTables.keyTable.min.js"></script>
    <script src="assets/plugins/datatables/dataTables.scroller.min.js"></script>

    <script src="assets/plugins/datatables/dataTables.responsive.min.js"></script>
    <script src="assets/plugins/datatables/responsive.bootstrap4.min.js"></script>

    <script src="assets/pluginzero/select2/select2.min.js"></script>

    <script src="js/PageEstudiantes.js?v=<%= DateTime.Now.ToString("yyyyMMddHHmmss") %>" type="text/javascript"></script>

</asp:Content>
