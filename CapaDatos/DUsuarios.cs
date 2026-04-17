using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using CapaEntidad.Entidades;
using CapaEntidad.Responses;

namespace CapaDatos
{
    public class DUsuarios
    {
        #region "PATRON SINGLETON"
        private static DUsuarios conexion = null;

        private DUsuarios() { }

        public static DUsuarios GetInstance()
        {
            if (conexion == null)
            {
                conexion = new DUsuarios();
            }
            return conexion;
        }
        #endregion

        public Respuesta<List<ERoles>> ListarRoles()
        {
            try
            {
                List<ERoles> rptLista = new List<ERoles>();
                using (SqlConnection con = ConexionBD.GetInstance().ConexionDB())
                {
                    //sp_ListarRoles
                    using (SqlCommand comando = new SqlCommand("sp_ListarRoles", con))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        using (SqlDataReader dr = comando.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                rptLista.Add(new ERoles
                                {
                                    IdRol = Convert.ToInt32(dr["IdRol"]),
                                    NombreRol = dr["NombreRol"].ToString(),
                                    Estado = Convert.ToBoolean(dr["Estado"])
                                });
                            }
                        }
                    }
                }
                return new Respuesta<List<ERoles>>
                {
                    Estado = true,
                    Data = rptLista,
                    Mensaje = "Lista obtenida correctamente"
                };
            }
            catch (Exception ex)
            {
                return new Respuesta<List<ERoles>>
                {
                    Estado = false,
                    Data = null,
                    Mensaje = $"Error al obtener la lista: {ex.Message}"
                };
            }
        }
    }
}
