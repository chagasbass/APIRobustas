namespace ApiRobustas.Compartilhados.DetalhesDosProblemas
{
    /// <summary>
    /// Detalhes do problemas seguindo a 
    /// </summary>
    public class DetalhesDoProblema
    {
        public string Titulo { get; set; }
        public int CodigoHttp { get; set; }
        public string Detalhe { get; set; }
        public string Tipo { get; set; }
        public string Instancia { get; set; }
        public string TipoDeDado { get; set; }

        public DetalhesDoProblema()
        {
            TipoDeDado = @"application/problem+json";
        }
    }
}
