using System;

namespace ApiRobustas.Compartilhados.Saude
{
    /// <summary>
    /// Classe com os dados do log estruturado
    /// </summary>
    public class InformacaoLog
    {
        public string IpAddress { get; private set; }
        public string Endpoint { get; private set; }
        public string Controller { get; private set; }
        public string RequestUri { get; private set; }
        public string RequestQueryParams { get; private set; }
        public string Usuario { get; private set; }
        public string RequestBody { get; private set; }
        public string Method { get; private set; }
        public string ResponseBody { get; private set; }
        public int ResponseStatusCode { get; private set; }
        public string TraceId { get; private set; }
        public Exception Exception { get; private set; }
        public string ExceptionMessageError { get; private set; }
        public DateTime Timestamp { get; private set; }
        public string AssemblyInfo { get; private set; }
        public InformacaoExternaLog InformacaoExternaLog { get; private set; }

        public InformacaoLog()
        {
            Timestamp = DateTime.Now;
            InformacaoExternaLog = new InformacaoExternaLog();
        }

        public InformacaoLog InserirEndpoint(string endpoint)
        {
            Endpoint = endpoint;
            return this;
        }

        public InformacaoLog InserirIp(string ip)
        {
            IpAddress = ip ?? "Ip não informado";
            return this;
        }

        public InformacaoLog InserirController(string controller)
        {
            Controller = controller;
            return this;
        }

        public InformacaoLog InserirUsuario(string usuario)
        {
            Usuario = usuario ?? "Usuário não está logado.";
            return this;
        }

        public InformacaoLog InserirRequestUri(string requestUri)
        {
            RequestUri = requestUri ?? "Sem request Uri";
            return this;
        }

        public InformacaoLog InserirRequestMethod(string method)
        {
            Method = method;
            return this;
        }

        public InformacaoLog InserirRequestBody(string requestBody)
        {
            RequestBody = requestBody ?? "Sem request body";
            return this;
        }

        public InformacaoLog InserirResponseBody(string responseBody)
        {
            ResponseBody = responseBody ?? "Sem response body";
            return this;
        }

        public InformacaoLog InserirInformacoesExternas(InformacaoExternaLog informacaoExternaLog)
        {
            InformacaoExternaLog = informacaoExternaLog;
            return this;
        }

        public InformacaoLog InserirResponseStatusCode(int statusCode)
        {
            ResponseStatusCode = statusCode;
            return this;
        }

        public InformacaoLog InserirTraceId(string traceId)
        {
            TraceId = traceId;
            return this;
        }

        public InformacaoLog InserirException(Exception exception)
        {
            Exception = exception;
            return this;
        }

        public InformacaoLog InserirExceptionMessageError(string mensagem)
        {
            ExceptionMessageError = mensagem;
            return this;
        }

        public InformacaoLog InserirAssemblyInfo(string info)
        {
            AssemblyInfo = info;
            return this;
        }

        public InformacaoLog InserirRequestQueryParams(string requestQueryParams)
        {
            RequestQueryParams = requestQueryParams ?? "sem parâmetros";
            return this;
        }

    }
}
