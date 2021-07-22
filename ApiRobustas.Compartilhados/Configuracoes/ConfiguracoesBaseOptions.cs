namespace ApiRobustas.Compartilhados.Configuracoes
{
    /// <summary>
    /// Classe para configurações do appSettings
    /// usando o Options pattern
    /// </summary>
    public class ConfiguracoesBaseOptions
    {
        public const string ConfiguracoesBase = "ConfiguracoesBase";
        public string ServicoCep { get; set; }
        public string BaseDeDados { get; set; }

        public ConfiguracoesBaseOptions() { }

    }
}
