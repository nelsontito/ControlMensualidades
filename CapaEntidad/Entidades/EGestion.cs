using System;

namespace CapaEntidad.Entidades
{
    public class EGestion
    {
        public int IdGestion { get; set; }
        public string NombreGestion { get; set; }
        public bool Anio { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
