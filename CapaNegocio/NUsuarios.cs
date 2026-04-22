using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad.Entidades;
using CapaEntidad.Responses;

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
    }
}
