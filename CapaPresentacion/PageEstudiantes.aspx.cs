using CapaEntidad.Entidades;
using CapaEntidad.Responses;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaPresentacion
{
    public partial class PageEstudiantes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            

    }

        [WebMethod]
        public static Respuesta<List<ESemestre>> ListarSemestre()
        {
            return NEstudiantes.GetInstance().ListarSemestre();
        }

        [WebMethod]
        public static Respuesta<int> GuardarOrEditEstudiantes(EEstudiantes objeto, string base64Image)
        {
            try
            {
                var utilidades = Utilidades.GetInstance();

                // 1. Manejo de la foto
                if (!string.IsNullOrEmpty(base64Image))
                {
                    byte[] imageBytes = Convert.FromBase64String(base64Image);
                    using (var stream = new MemoryStream(imageBytes))
                    {
                        string folder = "/Imagenes/";
                        objeto.FotoEstudianteUrl = utilidades.UploadPhoto(stream, folder);
                    }
                }
                else
                {
                    objeto.FotoEstudianteUrl = "";
                }

                return NEstudiantes.GetInstance().GuardarOrEditEstudiantes(objeto);
            }
            catch (Exception ex)
            {
                return new Respuesta<int> { Estado = false, Valor = "error", Mensaje = "Error en el servidor: " + ex.Message };
            }
        }

        [WebMethod]
        public static Respuesta<List<EEstudiantes>> ListarEstudiantes()
        {
            return NEstudiantes.GetInstance().ListarEstudiantes();
        }

    }
}