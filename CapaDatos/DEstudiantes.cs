using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class DEstudiantes
    {
        #region "PATRON SINGLETON"
        private static DEstudiantes conexion = null;

        private DEstudiantes() { }

        public static DEstudiantes GetInstance()
        {
            if (conexion == null)
            {
                conexion = new DEstudiantes();
            }
            return conexion;
        }
    }
}
