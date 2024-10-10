namespace GestaoDeResiduos.Models
{
    public class Residencia
    {
        public int Id { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Cep { get; set; }
        public bool LixoParaColeta { get; set; }
        public DateTime? DataProximaColeta { get; set; }
    }
}
