using CapaEntidad.Entidades;
using CapaEntidad.Responses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
        #endregion

        ///crear listar semestre MAPEAR
        public Respuesta<List<ESemestre>> ListarSemestre()
        {
            try
            {
                List<ESemestre> rptLista = new List<ESemestre>();
                using (SqlConnection con = ConexionBD.GetInstance().ConexionDB())
                {
                    //sp_ListarRoles
                    using (SqlCommand comando = new SqlCommand("usp_ListarSemestre", con))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        using (SqlDataReader dr = comando.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                rptLista.Add(new ESemestre
                                {
                                    IdSemestre = Convert.ToInt32(dr["IdSemestre"]),
                                    IdGestion = Convert.ToInt32(dr["IdGestion"]),
                                    NombreSemestre = dr["NombreSemestre"].ToString(),
                                    NumeroSemestre = Convert.ToInt32(dr["NumeroSemestre"]),
                                    FechaInicio = Convert.ToDateTime(dr["FechaInicio"]).ToString("dd/MM/yyyy"),
                                    FechaFin = Convert.ToDateTime(dr["FechaFin"]).ToString("dd/MM/yyyy"),
                                    Estado = Convert.ToBoolean(dr["Estado"])
                                });
                            }
                        }
                    }
                }
                return new Respuesta<List<ESemestre>>
                {
                    Estado = true,
                    Data = rptLista,
                    Mensaje = "Lista obtenida correctamente"
                };
            }
            catch (Exception ex)
            {
                return new Respuesta<List<ESemestre>>
                {
                    Estado = false,
                    Data = null,
                    Mensaje = $"Error al obtener la lista: {ex.Message}"
                };
            }
        }


        public Respuesta<int> GuardarOrEditEstudiantes(EEstudiantes oModel)
        {
            Respuesta<int> response = new Respuesta<int>();
            int resultadoCodigo = 0;
            try
            {
                using (SqlConnection con = ConexionBD.GetInstance().ConexionDB())
                {
                    using (SqlCommand cmd = new SqlCommand("usp_GuardarOrEditEstudiantes", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdEstudiante", oModel.IdEstudiante);
                        cmd.Parameters.AddWithValue("@IdCarrera", oModel.IdCarrera);
                        cmd.Parameters.AddWithValue("@IdSemestre", oModel.IdSemestre);
                        cmd.Parameters.AddWithValue("@NombreEstudiante", oModel.NombreEstudiante);
                        cmd.Parameters.AddWithValue("@ApellidosEstudiante", oModel.ApellidosEstudiante);
                        cmd.Parameters.AddWithValue("@CiEstudiante", oModel.CiEstudiante);
                        cmd.Parameters.AddWithValue("@Codigo", oModel.Codigo);
                        cmd.Parameters.AddWithValue("@CiEstudiante", oModel.CiEstudiante);
                        // Blindaje contra nulos en la Clave (Si es Update, puede que venga nula. La mandamos vacía para que el SP la ignore)
                        cmd.Parameters.AddWithValue("@FotoEstudianteUrl", string.IsNullOrEmpty(oModel.FotoEstudianteUrl) ? "" : oModel.FotoEstudianteUrl);
                        cmd.Parameters.AddWithValue("@Telefono", oModel.Telefono);

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
        public Respuesta<List<EEstudiantes>> ListarEstudiantes()
        {
            try
            {
                List<EEstudiantes> rptLista = new List<EEstudiantes>();

                using (SqlConnection con = ConexionBD.GetInstance().ConexionDB())
                {
                    using (SqlCommand comando = new SqlCommand("usp_ListarUsuarios", con))
                    {
                        comando.CommandType = CommandType.StoredProcedure;

                        con.Open();

                        using (SqlDataReader dr = comando.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                rptLista.Add(new EEstudiantes
                                {
                                    IdEstudiante = Convert.ToInt32(dr["IdEstudiante"]),
                                    IdCarrera = Convert.ToInt32(dr["IdCarrera"]),
                                    IdSemestre = Convert.ToInt32(dr["IdSemestre"]),
                                    NombreEstudiante = dr["NombreEstudiante"].ToString(),
                                    ApellidosEstudiante = dr["ApellidosEstudiante"].ToString(),
                                    CiEstudiante = dr["CiEstudiante"].ToString(),
                                    Codigo = dr["Codigo"].ToString(),
                                    Telefono = dr["Telefono"].ToString(),
                                    FotoEstudianteUrl = dr["FotoEstudianteUrl"].ToString()    
                                });
                            }
                        }
                    }
                }
                return new Respuesta<List<EEstudiantes>>()
                {
                    Estado = true,
                    Data = rptLista,
                    Mensaje = "Lista obtenidos correctamente"
                };
            }
            catch (Exception ex)
            {
                // Maneja cualquier error inesperado
                return new Respuesta<List<EEstudiantes>>()
                {
                    Estado = false,
                    Mensaje = "Ocurrió un error: " + ex.Message,
                    Data = null
                };
            }
        }

        
    }
}
