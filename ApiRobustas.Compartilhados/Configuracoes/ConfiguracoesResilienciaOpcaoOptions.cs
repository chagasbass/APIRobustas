namespace ApiRobustas.Compartilhados.Configuracoes
{
    public class ConfiguracoesResilienciaOpcaoOptions
    {
        public const string ConfiguracoesBase = "Resiliencia";
        public int QuantidadeDeTentativas { get; set; }
        public int PausaEntreAsFalhas { get; set; }

        public ConfiguracoesResilienciaOpcaoOptions() { }

    }
}
