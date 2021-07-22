using System.Text.Json.Serialization;

namespace ApiRobustas.Dominio.Contextos.Usuarios.Queries
{
    public class EnderecoQuery
    {
        [JsonPropertyName("cep")]
        public string Cep { get; set; }
        [JsonPropertyName("logradouro")]
        public string Logradouro { get; set; }

        [JsonPropertyName("bairro")]
        public string Bairro { get; set; }
        [JsonPropertyName("localidade")]
        public string Localidade { get; set; }
        [JsonPropertyName("uf")]
        public string Uf { get; set; }
        [JsonPropertyName("ibge")]
        public string Ibge { get; set; }
        [JsonInclude]
        [JsonPropertyName("gia")]
        public string Gia { get; set; }
        [JsonPropertyName("ddd")]
        public string DDD { get; set; }
        [JsonPropertyName("siafi")]
        public string Siafi { get; set; }

        public EnderecoQuery()
        {

        }
    }
}
