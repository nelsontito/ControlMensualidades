using CapaDatos;
using CapaEntidad.Entidades;
using CapaEntidad.Responses;
using System.Collections.Generic;

namespace CapaNegocio
{
    public class NUsuarios
    {
        #region "PATRON SINGLETON"
        private static NUsuarios conexion = null;

        private NUsuarios() { }

        public static NUsuarios GetInstance()
        {
            if (conexion == null)
            {
                conexion = new NUsuarios();
            }
            return conexion;
        }
        #endregion}

        public Respuesta<List<ERoles>> ListarRoles()
        {
            return DUsuarios.GetInstance().ListarRoles();
        }
        public Respuesta<int> GuardarOrEditUsuarios(EUsuarios objeto)
        {
            return DUsuarios.GetInstance().GuardarOrEditUsuarios(objeto);
        }

        public Respuesta<List<EUsuarios>> ListarUsuarios()
        {
            return DUsuarios.GetInstance().ListarUsuarios();
        }
    }
}


