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

        public Respuesta<int> GuardarOrEditUsuarios(EUsuarios oModel)
        {
            Respuesta<int> response = new Respuesta<int>();
            int resultadoCodigo = 0;
            try
            {
                using (SqlConnection con = ConexionBD.GetInstance().ConexionDB())
                {
                    using (SqlCommand cmd = new SqlCommand("usp_GuardarOrEditUsuarios", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdUsuario", oModel.IdUsuario);
                        cmd.Parameters.AddWithValue("@IdRol", oModel.IdRol);
                        cmd.Parameters.AddWithValue("@NombreUsuario", oModel.NombreUsuario);
                        cmd.Parameters.AddWithValue("@ApellidosUsuario", oModel.ApellidosUsuario);
                        cmd.Parameters.AddWithValue("@CiUsuario", oModel.CiUsuario);
                        cmd.Parameters.AddWithValue("@Correo", oModel.Correo);
                        // Blindaje contra nulos en la Clave (Si es Update, puede que venga nula. La mandamos vacía para que el SP la ignore)
                        cmd.Parameters.AddWithValue("@Contrasena", string.IsNullOrEmpty(oModel.Contrasena) ? "" : oModel.Contrasena);
                        cmd.Parameters.AddWithValue("@FotoUrl", string.IsNullOrEmpty(oModel.FotoUrl) ? "" : oModel.FotoUrl);
                        cmd.Parameters.AddWithValue("@Estado", oModel.Estado);

                        SqlParameter outputParam = new SqlParameter("@Resultado", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(outputParam);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        resultadoCodigo = Convert.ToInt32(outputParam.Value);
                    }
                }
                response.Data = resultadoCodigo;
                switch (resultadoCodigo)
                {
                    case 1: // Duplicado
                        response.Estado = false;
                        response.Valor = "warning";
                        response.Mensaje = "Ocurrio un problema el Correo o Ci ya existe.";
                        break;

                    case 2: // Registro Nuevo
                        response.Estado = true;
                        response.Valor = "success";
                        response.Mensaje = "Registrado correctamente.";
                        break;

                    case 3: // Actualización
                        response.Estado = true;
                        response.Valor = "success";
                        response.Mensaje = "Actualizado correctamente.";
                        break;

                    case 0: // Error
                    default:
                        response.Estado = false;
                        response.Valor = "error";
                        response.Mensaje = "No se pudo completar la operación.";
                        break;
                }
            }
            catch (Exception ex)
            {
                response.Estado = false;
                response.Valor = "error";
                response.Mensaje = $"Error interno: {ex.Message}";
            }
            return response;
        }
    }
}
