namespace ApiRobustas.Compartilhados.Configuracoes
{
    /// <summary>
    /// Classe responsável pelas configurações de cache da aplicação
    /// </summary>
    public class ConfiguracoesDeCacheOptions
    {
        public const string ConfiguracoesBase = "Cache";
        public string ChaveProdutoCache { get; set; }
        public int TempoDeExpiracaoRelativo { get; set; }
        public int TempoOcioso { get; set; }

        public ConfiguracoesDeCacheOptions() { }
    }
}
