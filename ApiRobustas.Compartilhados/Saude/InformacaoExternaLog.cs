namespace ApiRobustas.Compartilhados.Saude
{
    public class InformacaoExternaLog
    {
        public string ExternalRequestMethod { get; private set; }
        public string ExternalRequestUri { get; private set; }
        public string ExternalRequestBody { get; private set; }
        public string ExternalRequestParams { get; private set; }
        public string ExternalResponseBody { get; private set; }
        public int ExternalResponseStatusCode { get; private set; }

        public bool TemInformacaoExterna { get; private set; }

        public InformacaoExternaLog() { }

        #region Methods

        public bool VerficarSeInformacoesExternasExistem() => TemInformacaoExterna;

        public InformacaoExternaLog InserirInformacaoExterna(bool hasLog)
        {
            TemInformacaoExterna = hasLog;
            return this;
        }

        public InformacaoExternaLog InserirResponseStatusCode(int statusCode)
        {
            ExternalResponseStatusCode = statusCode;
            return this;
        }

        public InformacaoExternaLog InserirUriRequest(string uri)
        {
            ExternalRequestUri = uri;
            return this;
        }

        public InformacaoExternaLog InserirRequestParams(string requestParams)
        {
            ExternalRequestParams = string.IsNullOrEmpty(requestParams) ? "sem request params  na requisição externa" : requestParams;
            return this;
        }

        public InformacaoExternaLog InserirRequestBody(string requestBody)
        {
            ExternalRequestBody = string.IsNullOrEmpty(requestBody) ? "Sem body na requisição externa" : requestBody;
            return this;
        }

        public InformacaoExternaLog InserirResponseBody(string responseBody)
        {
            ExternalResponseBody = string.IsNullOrEmpty(responseBody) ? "Sem response body na requisição externa" : responseBody;
            return this;
        }

        public InformacaoExternaLog InserirRequestMethod(string requestMethod)
        {
            ExternalRequestMethod = requestMethod;
            return this;
        }

        public InformacaoExternaLog InserirExternalRequestUri(string uri)
        {
            ExternalRequestUri = uri ?? "Sem Uri  na requisição externa";
            return this;
        }

        #endregion
    }
}
