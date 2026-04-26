using CapaDatos;
using CapaEntidad.Entidades;
using CapaEntidad.Responses;
using System.Collections.Generic;

namespace CapaNegocio
{
    public class NEstudiantes
    {
        #region "PATRON SINGLETON"
        private static NEstudiantes conexion = null;

        private NEstudiantes() { }

        public static NEstudiantes GetInstance()
        {
            if (conexion == null)
            {
                conexion = new NEstudiantes();
            }
            return conexion;
        }
        #endregion}

        public Respuesta<List<ESemestre>> ListarSemestre()
        {
            return DEstudiantes.GetInstance().ListarSemestre();
        }

        public Respuesta<int> GuardarOrEditEstudiantes(EEstudiantes objeto)
        {
            return DEstudiantes.GetInstance().GuardarOrEditEstudiantes(objeto);
        }

        public Respuesta<List<EEstudiantes>> ListarEstudiantes()
        {
            return DEstudiantes.GetInstance().ListarEstudiantes();
        }
    }
}
