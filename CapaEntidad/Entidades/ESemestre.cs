namespace CapaEntidad.Entidades
{
    public class ESemestre

    {
        public int IdSemestre { get; set; }

        public int IdGestion { get; set; }
        public string NombreSemestre { get; set; }
        public int NumeroSemestre { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
        public bool Estado { get; set; }
    }
}
