using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad.Entidades
{
    public class EEstudiantes
    {
        public int IdEstudiante { get; set; }
        public int IdCarrera { get; set; }
        public int IdSemestre { get; set; }
        public string NombreEstudiante { get; set; }
        public string ApellidosEstudiante { get; set; }
        public string CiEstudiante { get; set; }
        public string Codigo { get; set; }
        public string Telefono { get; set; }
        public string FotoEstudianteUrl { get; set; }
        
    }
}
