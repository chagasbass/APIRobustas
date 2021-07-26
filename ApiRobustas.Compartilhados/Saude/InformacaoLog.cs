using System;

namespace ApiRobustas.Compartilhados.Saude
{
    /// <summary>
    /// Classe com os dados do log estruturado
    /// </summary>
    public class InformacaoLog
    {
        public string RequestUri { get; set; }
        public string Usuario { get; set; }
        public string RequestBody { get; set; }
        public string Method { get; set; }
        public string ResponseBody { get; set; }
        public string ExternalUri { get; set; }
        public int ExternalStatusCode { get; set; }
        public string ExternalRequestBody { get; set; }
        public string ExternalResponseBody { get; set; }
        public string ExternalQueryParams { get; set; }
        public string RequestQueryParams { get; set; }
        public int ResponseStatusCode { get; set; }
        public string TraceId { get; set; }
        public Exception Exception { get; set; }
        public string ExceptionMessageError { get; set; }
        public DateTime Timestamp { get; set; }
        public string AssemblyInfo { get; set; }

        public InformacaoLog()
        {
            Timestamp = DateTime.Now;
        }
    }
}
